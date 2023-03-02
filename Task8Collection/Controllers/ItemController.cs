using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Task8Collection.Data.Entities;
using Task8Collection.Data.Repository.Intefaces;
using Task8Collection.Hubs;
using Task8Collection.Models.Collection;
using Task8Collection.Models.Item;

namespace Task8Collection.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IHubContext<CommentHub> _commentContext;
        private readonly UserManager<User> _userManager;

        public ItemController(IRepositoryWrapper repository, IHubContext<CommentHub> commentContext, UserManager<User> userManager)
        {
            _repository = repository;
            _commentContext = commentContext;
            _userManager = userManager;
        }
        [AllowAnonymous]
        public async Task<IActionResult> ItemPage(Guid id)
        {
            
            var item = _repository.ItemRepository.GetItemById(id);
            var model = new ItemPageViewModel()
            {
                Item = item,
            };
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var isLiked = _repository.LikedItemsRepository
                    .GetElementByTableId(l => l.UserGuid == user.Id && l.ItemIdGuid == id).FirstOrDefault();
                model.IsLiked = isLiked != null;
            }
            
            await _commentContext.Clients.Group(id.ToString()).SendAsync("joinUser");
            return View(model);
        }

        public IActionResult AddItem(CollectionEditViewModel model)
        {
            var fields = new List<Field>();
            var collectionFields =
                _repository.CollectionFieldsRepository
                    .GetCollectionFieldByCollectionId(model.CollectionModel.CollectionId).ToList();
            foreach (var field in collectionFields)
            {
                fields.Add(field.DataTypeField switch
                {
                    0 => new IntField { Value = 0, Name = field.Name },
                    1 => new StringField { Value = "", Name = field.Name, IsLong = false },
                    2 => new DateField { Value = DateTime.Now, Name = field.Name },
                    3 => new BoolField { Value = false, Name = field.Name },
                    4 => new StringField { Value = "", Name = field.Name, IsLong = true},
                    _ => new Field() { Name = field.Name }
                });
            }
            _repository.FieldRepository.CreateAll(fields);
            _repository.ItemRepository.Create(new Item()
            {
                CollectionId = model.CollectionModel.CollectionId,
                Fields = fields,
                Name = model.CollectionItem.Name
            });
            _repository.Save();
            return RedirectToAction("CollectionEdit",
                "Collection",
                new { collectionId = model.CollectionModel.CollectionId, userName = model.UserName });
        }

        public IActionResult EditItem(CollectionEditViewModel model)
        {
            EditFieldValues(model.Item);
            var item = _repository.ItemRepository.GetElementByTableId(i => i.Id == model.Item.Id).First();
            item.Name = model.Item.Name;
            _repository.ItemRepository.Update(item);
            _repository.Save();
            return RedirectToAction("CollectionEdit",
                "Collection",
                new { collectionId = model.CollectionModel.CollectionId, userName = model.UserName });
        }

        public async Task<IActionResult> Like(Guid itemId)
        {
            _repository.ItemRepository
                .ChangeField(i => i.Id == itemId, 
                    i => i.SetProperty(item => item.Likes, item => item.Likes+1));
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            _repository.LikedItemsRepository.Create(new LikedItems()
            {
                ItemIdGuid = itemId,
                UserGuid = user.Id
            });
            _repository.Save();
            return RedirectToAction("ItemPage", new { id = itemId });
        }
        public async Task<IActionResult> Unlike(Guid itemId)
        {
            _repository.ItemRepository
                .ChangeField(i => i.Id == itemId,
                    i => i.SetProperty(item => item.Likes, item => item.Likes - 1));
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var likeditem = _repository.LikedItemsRepository
                .GetElementByTableId(item => item.UserGuid == user.Id && item.ItemIdGuid == itemId).First();
            _repository.LikedItemsRepository.Delete(likeditem);
            _repository.Save();
            return RedirectToAction("ItemPage", new { id = itemId });
        }
        [HttpPost]
        public IActionResult DeleteItem(CollectionEditViewModel model)
        {
            var collection = _repository
                .ItemRepository
                .FindByTableElement(c => c.Id == model.Item.Id);
            _repository.ItemRepository.Delete(collection);
            _repository.Save();
            return RedirectToAction("UserProfile", "Account", new { userName = model.UserName });
        }

        public void EditFieldValues(ItemModel item)
        {
            foreach (var field in item.IntFields)
            {
                _repository
                    .IntFieldRepository
                    .ChangeField(f => f.ItemId == item.Id && f.Id == field.Id
                        , i => i.SetProperty(f => f.Value, f => field.Value));
            }

            foreach (var field in item.StringFields)
            {
                _repository
                    .StringFieldRepository
                    .ChangeField(f => f.ItemId == item.Id && f.Id == field.Id
                        , i => i.SetProperty(f => f.Value, f => field.Value));
            }

            foreach (var field in item.DateFields)
            {
                _repository
                    .DateFieldRepository
                    .ChangeField(f => f.ItemId == item.Id && f.Id == field.Id
                        , i => i.SetProperty(f => f.Value, f => field.Value));
            }

            foreach (var field in item.BoolFields)
            {
                _repository
                    .BoolFieldRepository
                    .ChangeField(f => f.ItemId == item.Id && f.Id == field.Id
                        , i => i.SetProperty(f => f.Value, f => field.Value));
            }
        }

        public async Task<IActionResult> AddComment(Guid itemId, string commentBody, string userName)
        {
            _repository.CommentRepository.Create(new Comment()
            {
                CommentBody = commentBody,
                ItemId = itemId,
                UserName = userName
            });
            _repository.Save();
            await _commentContext.Clients.Group(itemId.ToString()).SendAsync("sendMessage", userName, commentBody);
            return RedirectToAction("ItemPage", new { id =  itemId });
        }
    }
}
