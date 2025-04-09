
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components
{
    public class CartSummaryViewComponent(Cart cartService) : ViewComponent
    {
        private Cart _cart = cartService;

        public IViewComponentResult Invoke()
        {
            return View(_cart);
        }
    }
}
