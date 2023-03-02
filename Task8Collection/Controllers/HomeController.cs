using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Task8Collection.Data.Repository.Intefaces;
using Task8Collection.Models;
using Task8Collection.Models.Home;

namespace Task8Collection.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositoryWrapper _repository;

        public HomeController(ILogger<HomeController> logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var lastAddedItems = _repository.ItemRepository
                .GetAll()
                .Include(i => i.Tags)
                .ToList().TakeLast(3);
            var collectionWithMostAddedItems = _repository.CollectionRepository
                .GetAll()
                .Include(c => c.Items)
                .OrderByDescending(c => c.Items.Count).Take(5).ToList();
            var model = new HomeViewModel()
            {
                Tags = _repository.TagRepository.GetAll().ToList(),
                LastAddedItems = lastAddedItems.ToList(),
                CollectionsWithMostAddedItems = collectionWithMostAddedItems
            };
            return View(model);
        }
    }
}