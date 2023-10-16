using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using SwiftPizza.Models;
using SwiftPizza.Extensions;

namespace SwiftPizza.Controllers
{
    public class TestController : Controller
    {
        private PizzaCartTest _cart => HttpContext.Session.Get<PizzaCartTest>("PizzaCartTest") ?? new PizzaCartTest();

        [HttpPost("addPizza")]
        public IActionResult AddPizza(int id, string name, decimal price)
        {
            _cart.AddToCart(id, name, price);
            HttpContext.Session.Set("PizzaCartTest", _cart);  // Save to session
            return Ok(new { Message = "Pizza added to cart", PizzaCartTest = _cart.CartItems });
        }

        [HttpPost("removePizza")]
        public IActionResult RemovePizza(int id)
        {
            _cart.RemoveFromCart(id);
            HttpContext.Session.Set("PizzaPizzaCartTest", _cart);  // Save to session
            return Ok(new { Message = "Pizza removed from cart", PizzaCartTest = _cart.CartItems });
        }
        [HttpGet("getPizzaCartTest")]
        public IActionResult GetPizzaCartTest()
        {
            return Ok(_cart.CartItems);
        }
    }
}






