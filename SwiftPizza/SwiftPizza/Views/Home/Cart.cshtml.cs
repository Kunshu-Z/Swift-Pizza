using Microsoft.EntityFrameworkCore;
using SwiftPizza.Models;
using SwiftPizza.Data;
using System.Collections.Generic;
using System.Linq;

namespace SwiftPizza.Views.Home
{
    public class CartModel
    {
        private readonly ApplicationDbContext _dbContext;

        public CartModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string SearchTerm { get; set; }

        public string SortOrder { get; set; }

        public IEnumerable<Pizza> Pizzas { get; set; }

        public void LoadPizzas(string searchTerm, string sortOrder)
        {
            SearchTerm = searchTerm;
            SortOrder = sortOrder;

            var pizzas = _dbContext.Pizzas.AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                pizzas = pizzas.Where(p => p.PizzaName.Contains(SearchTerm)); ;
            }

            if (!pizzas.Any(p => HasSpecialCharacters(p.PizzaName)))
            {
                if (SortOrder == "asc")
                {
                    pizzas = pizzas.OrderBy(p => p.PizzaName);
                }
                else if (SortOrder == "desc")
                {
                    pizzas = pizzas.OrderByDescending(p => p.PizzaName);
                }
            }

            Pizzas = pizzas.ToList();
        }

        private bool HasSpecialCharacters(string input)
        {
            return !string.IsNullOrEmpty(input) && input.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c));
        }
    }
}
