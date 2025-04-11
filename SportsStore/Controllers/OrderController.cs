using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class OrderController(IOrderRepository repository, Cart cartService) : Controller
    {
        private readonly IOrderRepository _orderRepository = repository;
        private readonly Cart _cart = cartService;

        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (_cart.Lines.Count == 0)
                ModelState.AddModelError("", "Sorry, your cart is empty!");

            if (ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray();
                _orderRepository.SaveOrder(order);
                _cart.Clear();
                return RedirectToPage("/Completed",
                    new { orderId = order.Id });
            }
            else
                return View();
        }
    }
}
