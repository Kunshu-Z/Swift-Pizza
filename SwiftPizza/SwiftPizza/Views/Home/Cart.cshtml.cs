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

            var pizzas = _dbContext.Pizza.AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                pizzas = pizzas.Where(p => EF.Functions.Like(p.PizzaName, $"%{SearchTerm}%"));
            }

            if (SortOrder == "asc")
            {
                pizzas = pizzas.OrderBy(p => p.PizzaName);
            }
            else if (SortOrder == "desc")
            {
                pizzas = pizzas.OrderByDescending(p => p.PizzaName);
            }

            Pizzas = pizzas.ToList();
        }
    }
}
