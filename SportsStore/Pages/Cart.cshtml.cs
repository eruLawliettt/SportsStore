using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SportsStore.Infrastructure;
using SportsStore.Models;

namespace SportsStore.Pages
{
    public class CartModel(IStoreRepository repo, Cart cartService) : PageModel
    {
        private readonly IStoreRepository _repository = repo;
        public Cart Cart { get; set; } = cartService;
        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string returnUrl) 
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(long productId, string returnUrl) 
        {
            Product? product = _repository.Products
                .FirstOrDefault(p => p.Id == productId);

            if (product != null)          
                Cart.AddItem(product, 1);

            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(long productId, string returnUrl)
        {
            Cart.RemoveItem(Cart.Lines
                .First(cl => cl.Product.Id == productId).Product);
            return RedirectToPage(new { ReturnUrl = returnUrl } );
        }

    }
}