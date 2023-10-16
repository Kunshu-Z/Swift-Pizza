namespace SwiftPizza.Models
{
    public class PizzaCartTest
    {
        public Dictionary<int, CartItem> CartItems { get; private set; } = new Dictionary<int, CartItem>();

        public decimal Price => CartItems.Sum(item => item.Value.Price * item.Value.Quantity);

        public void AddToCart(int id, string name, decimal price)
        {
            if (!CartItems.ContainsKey(id))
            {
                CartItems[id] = new CartItem { Id = id, Name = name, Price = price, Quantity = 0 };
            }

            CartItems[id].Quantity++;
        }

        public void RemoveFromCart(int id)
        {
            if (CartItems.ContainsKey(id) && CartItems[id].Quantity > 0)
            {
                CartItems[id].Quantity--;

                if (CartItems[id].Quantity == 0)
                {
                    CartItems.Remove(id);
                }
            }
        }

        public class CartItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }
    }
}
