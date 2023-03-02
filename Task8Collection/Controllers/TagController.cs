using Microsoft.AspNetCore.Mvc;
using Task8Collection.Data.Entities;
using Task8Collection.Data.Repository.Intefaces;

namespace Task8Collection.Controllers
{
    public class TagController : Controller
    {
        private readonly IRepositoryWrapper _repository;

        public TagController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public IActionResult AddTag(Guid collectionId, Guid itemId, string userName, string tagName)
        {
            try
            {
                Tag tag = _repository.TagRepository.GetTagByName(tagName);
                var item = _repository.ItemRepository.GetItemById(itemId);
                if (tag == null)
                {
                    tag = new Tag()
                    {
                        Name = tagName,
                    };
                    _repository.TagRepository.Create(tag);
                    _repository.Save();
                }

                _repository.ItemTagRepository.Create(new ItemTag()
                {
                    ItemId = itemId,
                    TagId = tag.Id
                });
                _repository.Save();
            }
            catch
            {
                return RedirectToAction("CollectionEdit",
                    "Collection",
                    new { collectionId = collectionId, userName = userName });
            }
            return RedirectToAction("CollectionEdit",
                "Collection",
                new { collectionId = collectionId, userName = userName });
        }
        [HttpPost]
        public IActionResult DeleteTag(Guid itemId, Guid tagId, Guid collectionId, string userName)
        {
            var itemTag = _repository.ItemTagRepository
                .GetElementByTableId(it => it.ItemId == itemId && it.TagId == tagId).Single();
            _repository.ItemTagRepository.Delete(itemTag);
            _repository.Save();
            return RedirectToAction("CollectionEdit",
                "Collection",
                new { collectionId = collectionId, userName = userName });
        }
    }
}
