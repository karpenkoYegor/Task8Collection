using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Task8Collection.Data;
using Task8Collection.Data.Entities;
using Task8Collection.Data.Repository.Intefaces;

namespace Task8Collection.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchEngine<Item> _searchEngine;
        private readonly IRepositoryWrapper _repository;

        public SearchController(ISearchEngine<Item> searchEngine, IRepositoryWrapper repository)
        {
            _searchEngine = searchEngine;
            _repository = repository;
        }
        public IActionResult FullTextSearch(string searchTerm)
        {
            var items = _repository.ItemRepository
                .GetAll()
                .Include(i => i.Collection)
                .Include(i => i.Tags)
                .Include(i => i.Fields)
                .ToList();
            _searchEngine.AddItemsToIndex(items);
            var searchedItems = _searchEngine.Search(searchTerm).ToList();
            var searchedFullItems = searchedItems.Join(items,
                serachedItem => serachedItem.Id,
                item => item.Id,
                (serachedItem, item) => item).ToList();
            return View("SearchPage", searchedFullItems);
        }

        public IActionResult SearchPage(Guid tagId)
        {
            var tag = _repository.TagRepository.GetElementByTableId(t => t.Id == tagId).Include(t => t.Items).ThenInclude(i => i.Collection).Single();
            return View(tag.Items);
        }
    }
}
