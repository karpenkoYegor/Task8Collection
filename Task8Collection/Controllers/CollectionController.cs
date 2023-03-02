
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task8Collection.Data.Entities;
using Task8Collection.Data.Repository.Intefaces;
using Task8Collection.Models.Account;
using Task8Collection.Models.Collection;

namespace Task8Collection.Controllers
{
    public class CollectionController : Controller
    {
        private readonly IRepositoryWrapper _repository;
        private readonly UserManager<User> _userManager;

        public CollectionController(IRepositoryWrapper repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        [HttpPost]
        public IActionResult AddCollection(string userName ,string userId, UserProfileViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("UserProfile", "Account", new { userName = userName });
            _repository.CollectionRepository.Create(new Collection()
            {
                Name = model.NameNewCollection,
                UserId = userId,
                ThemeId = model.ThemeId,
                Description = model.Description
            });
            _repository.Save();
            return RedirectToAction("UserProfile", "Account", new { userName = userName });
        }

        public IActionResult CollectionEdit(Guid collectionId
            ,string userName
            ,Guid sortedById
            ,SortState sortOrder
            ,string fieldType = ""
            ,string searchItem = ""
            ,string sortedBy = "Name")
        {
            var collection = _repository
                .CollectionRepository
                .GetCollectionById(collectionId)
                .Include(c => c.Fields)
                .Include(c => c.Items)
                .ThenInclude(i => i.Fields)
                .Include(c => c.Items)
                .ThenInclude(i => i.Tags)
                .First();
            ViewData[$"{sortedBy}"] = sortOrder == SortState.Asc ? SortState.Desc : SortState.Asc;
            var itemModels = new List<ItemModel>();
            foreach (var item in collection.Items)
            {
                var fields = new List<Field>();
                foreach (var field in collection.Fields)
                {
                    fields.Add(item.Fields.Where(f => f.Name == field.Name).FirstOrDefault(new Field { Name = field.Name }));
                }
                
                itemModels.Add(new ItemModel()
                {
                    Name = item.Name,
                    Id = item.Id,
                    BoolFields = _repository.BoolFieldRepository.GetElementByTableId(f => f.ItemId == item.Id).ToList(),
                    DateFields = _repository.DateFieldRepository.GetElementByTableId(f => f.ItemId == item.Id).ToList(),
                    IntFields = _repository.IntFieldRepository.GetElementByTableId(f => f.ItemId == item.Id).ToList(),
                    StringFields = _repository.StringFieldRepository.GetElementByTableId(f => f.ItemId == item.Id)
                        .ToList(),
                    Fields = fields,
                    Tags = item.Tags.Count == 0 ? new List<Tag>() : item.Tags
                });
            }

            itemModels = new { sortOrder = sortOrder, fieldType = fieldType } switch
            {
                {sortOrder: SortState.Asc, fieldType: "IntField" } => itemModels.OrderBy(i => i.IntFields.First(f => f.Name == sortedBy).Value).ToList(),
                    { sortOrder: SortState.Desc, fieldType: "IntField" } => itemModels
                        .OrderByDescending(i => i.IntFields.First(f => f.Name == sortedBy).Value).ToList(),
                    { sortOrder: SortState.Asc, fieldType: "StringField" } => itemModels
                        .OrderBy(i => i.StringFields.First(f => f.Name == sortedBy).Value).ToList(),
                    { sortOrder: SortState.Desc, fieldType: "StringField" } => itemModels
                        .OrderByDescending(i => i.StringFields.First(f => f.Name == sortedBy).Value).ToList(),
                    { sortOrder: SortState.Asc, fieldType: "DateField" } => itemModels
                        .OrderBy(i => i.DateFields.First(f => f.Value != null && f.Name == sortedBy).Value).ToList(),
                    { sortOrder: SortState.Desc, fieldType: "DateField" } => itemModels
                        .OrderByDescending(i => i.DateFields.First(f => f.Name == sortedBy).Value).ToList(),
                    { sortOrder: SortState.Asc, fieldType: "BoolField" } => itemModels
                        .OrderBy(i => i.BoolFields.First(f => f.Name == sortedBy).Value).ToList(),
                    { sortOrder: SortState.Desc, fieldType: "BoolField" } => itemModels
                        .OrderByDescending(i => i.BoolFields.First(f => f.Name == sortedBy).Value).ToList(),
                    { sortOrder: SortState.Asc, fieldType: "Name" } => itemModels
                        .OrderBy(i => i.Name).ToList(),
                    { sortOrder: SortState.Desc, fieldType: "Name" } => itemModels
                        .OrderByDescending(i => i.Name).ToList(),
                    _ => itemModels
            };
            var model = new CollectionEditViewModel()
            {
                Collection = collection,
                CollectionModel = new CollectionModel()
                {
                    CollectionId = collectionId,
                    Name = collection.Name,
                    Description = collection.Description,
                    ThemeId = collection.ThemeId,
                    Items = itemModels
                },
                Themes = _repository.ThemeRepository.GetAll().ToList(),
                UserName = userName,
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CollectionEdit(CollectionEditViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            var collection = _repository
                .CollectionRepository
                .FindByTableElement(c => c.Id == model.CollectionModel.CollectionId);
            collection.Name = model.CollectionModel.Name;
            collection.Description = model.CollectionModel.Description;
            collection.ThemeId = model.CollectionModel.ThemeId;
            _repository.CollectionRepository.Update(collection);
            _repository.Save();
            return RedirectToAction("UserProfile", "Account", new { userName = model.UserName });
        }

        [HttpPost]
        public IActionResult AddCollectionField(CollectionEditViewModel model)
        {
            _repository.CollectionFieldsRepository.Create(new CollectionFields()
            {
                Name = model.CollectionField.CollectionFieldName,
                CollectionId = model.CollectionModel.CollectionId,
                DataTypeField = model.CollectionField.DataTypeField
            });
            _repository.Save();
            return RedirectToAction("CollectionEdit",
                "Collection",
                new { collectionId = model.CollectionModel.CollectionId, userName = model.UserName });
        }

        [HttpPost]
        public IActionResult UpdateCollectionField(CollectionEditViewModel model)
        {
            var collectionField =
                _repository.CollectionFieldsRepository.GetCollectionFieldById(model.CollectionField.CollectionFieldId).First();
            collectionField.Name = model.CollectionField.CollectionFieldName;
            _repository.CollectionFieldsRepository.Update(collectionField);
            _repository.Save();
            return RedirectToAction("CollectionEdit",
                "Collection",
                new { collectionId = model.CollectionModel.CollectionId, userName = model.UserName });

        }
        [HttpPost]
        public IActionResult DeleteCollectionField(CollectionEditViewModel model)
        {
            var collectionField =
                _repository.CollectionFieldsRepository.GetCollectionFieldById(model.CollectionField.CollectionFieldId).First();
            _repository.CollectionFieldsRepository.Delete(collectionField);
            _repository.Save();
            return RedirectToAction("CollectionEdit",
                "Collection",
                new { collectionId = model.CollectionModel.CollectionId, userName = model.UserName });
        }

        public IActionResult CollectionPage(Guid collectionId)
        {
            var model = _repository.CollectionRepository
                .GetCollectionById(collectionId)
                .Include(c => c.Items)
                .Include(i => i.Theme)
                .First();

            return View(model);
        }
    }
}
