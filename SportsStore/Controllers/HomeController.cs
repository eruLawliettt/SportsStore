using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController(IStoreRepository repository) : Controller
    {
        private readonly IStoreRepository _repository = repository;

        public int PageSize = 4;

        public IActionResult Index(string? category, int productPage = 1)
            => View(new ProductListViewModel
            {
                Products = _repository.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.Id)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null 
                        ? _repository.Products.Count()
                        : _repository.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            });
    }
}
