using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SwiftPizza.Data;
using SwiftPizza.Views.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;


namespace UnitTestSP
{
    [TestClass]
    public class LoginModelTests
    {
        [TestMethod]
        public async Task T1_1_LoginWithCorrectUsernameAndPassword()
        {
            // Arrange
            var dbContext = new Mock<ApplicationDbContext>();
            var user = new User
            {
                Email = "Johndo@gmail.com",
                Password = "Johnny",
                FirstName = "John",
                // Add other user properties as needed
            };

            var mockDbSet = new Mock<DbSet<User>>();
            mockDbSet.Setup(m => m.FindAsync(user.Email)).Returns(Task.FromResult(user));

            dbContext.Setup(m => m.Users).Returns(mockDbSet.Object);

            var model = new LoginModel(dbContext.Object)
            {
                Email = "Johndo@gmail.com",
                Password = "Johnny",
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var result = await model.OnPostAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
        }

        [TestMethod]
        public async Task T1_2_LoginWithCorrectUsernameAndIncorrectPassword()
        {
            // Arrange
            var dbContext = new Mock<ApplicationDbContext>();
            var user = new User
            {
                Email = "Johndo@gmail.com",
                Password = "Johnny",
                FirstName = "John",
                // Add other user properties as needed
            };

            var mockDbSet = new Mock<DbSet<User>>();
            mockDbSet.Setup(m => m.FindAsync(user.Email)).Returns(Task.FromResult(user));

            dbContext.Setup(m => m.Users).Returns(mockDbSet.Object);

            var model = new LoginModel(dbContext.Object)
            {
                Email = "Johndo@gmail.com",
                Password = "IncorrectPassword",
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var result = await model.OnPostAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(PageResult));
            Assert.AreEqual("Invalid email or password.", model.ViewData["ErrorMessage"]);
        }

        [TestMethod]
        public async Task T1_3_LoginWithIncorrectUsernameAndCorrectPassword()
        {
            // Arrange
            var dbContext = new Mock<ApplicationDbContext>();
            var user = new User
            {
                Email = "Johndo@gmail.com",
                Password = "Johnny",
                FirstName = "John",
                // Add other user properties as needed
            };

            var mockDbSet = new Mock<DbSet<User>>();
            mockDbSet.Setup(m => m.FindAsync(user.Email)).Returns(Task.FromResult(user));

            dbContext.Setup(m => m.Users).Returns(mockDbSet.Object);

            var model = new LoginModel(dbContext.Object)
            {
                Email = "IncorrectUsername",
                Password = "Johnny",
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var result = await model.OnPostAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(PageResult));
            Assert.AreEqual("Invalid email or password.", model.ViewData["ErrorMessage"]);
        }

        [TestMethod]
        public async Task T1_4_LoginWithIncorrectUsernameAndIncorrectPassword()
        {
            // Arrange
            var dbContext = new Mock<ApplicationDbContext>();
            var user = new User
            {
                Email = "Johndo@gmail.com",
                Password = "Johnny",
                FirstName = "John",
                // Add other user properties as needed
            };

            var mockDbSet = new Mock<DbSet<User>>();
            mockDbSet.Setup(m => m.FindAsync(user.Email)).Returns(Task.FromResult(user));

            dbContext.Setup(m => m.Users).Returns(mockDbSet.Object);

            var model = new LoginModel(dbContext.Object)
            {
                Email = "IncorrectUsername",
                Password = "IncorrectPassword",
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var result = await model.OnPostAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(PageResult));
            Assert.AreEqual("Invalid email or password.", model.ViewData["ErrorMessage"]);
        }
    }

    [TestClass]
    public class CartModelTests
    {
        [TestMethod]
        public void LoadPizzas_SearchRelevantKeywords_Success()
        {
            // Arrange
            var dbContext = new Mock<ApplicationDbContext>();
            var cartModel = new CartModel(dbContext.Object);
            var pizzas = new List<Pizza>
        {
            new Pizza { PizzaName = "Pepperoni Pizza" },
            new Pizza { PizzaName = "Margarita Pizza" },
        }.AsQueryable();

            dbContext.Setup(d => d.Pizza).Returns(pizzas);

            // Act
            cartModel.LoadPizzas("Pizza", "asc");

            // Assert
            CollectionAssert.AreEqual(pizzas.ToList(), cartModel.Pizzas.ToList());
        }

        [TestMethod]
        public void LoadPizzas_SearchUnrelatedKeywords_EmptyResult()
        {
            // Arrange
            var dbContext = new Mock<ApplicationDbContext>();
            var cartModel = new CartModel(dbContext.Object);
            var pizzas = new List<Pizza>
        {
            new Pizza { PizzaName = "Pepperoni Pizza" },
            new Pizza { PizzaName = "Margarita Pizza" },
        }.AsQueryable();

            dbContext.Setup(d => d.Pizza).Returns(pizzas);

            // Act
            cartModel.LoadPizzas("Shoes", "asc");

            // Assert
            CollectionAssert.AreEqual(new List<Pizza>(), cartModel.Pizzas.ToList());
        }

        [TestMethod]
        public void LoadPizzas_SearchSpecialCharacters_EmptyResult()
        {
            // Arrange
            var dbContext = new Mock<ApplicationDbContext>();
            var cartModel = new CartModel(dbContext.Object);
            var pizzas = new List<Pizza>
        {
            new Pizza { PizzaName = "Pepperoni Pizza" },
            new Pizza { PizzaName = "Margarita Pizza" },
        }.AsQueryable();

            dbContext.Setup(d => d.Pizza).Returns(pizzas);

            // Act
            cartModel.LoadPizzas("!@#$%^", "asc");

            // Assert
            CollectionAssert.AreEqual(new List<Pizza>(), cartModel.Pizzas.ToList());
        }

        [TestMethod]
        public void LoadPizzas_SearchBlankKeyword_EmptyResult()
        {
            // Arrange
            var dbContext = new Mock<ApplicationDbContext>();
            var cartModel = new CartModel(dbContext.Object);
            var pizzas = new List<Pizza>
        {
            new Pizza { PizzaName = "Pepperoni Pizza" },
            new Pizza { PizzaName = "Margarita Pizza" },
        }.AsQueryable();

            dbContext.Setup(d => d.Pizza).Returns(pizzas);

            // Act
            cartModel.LoadPizzas("", "asc");

            // Assert
            CollectionAssert.AreEqual(new List<Pizza>(), cartModel.Pizzas.ToList());
        }
    }

    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void T4_1_AddingItemsToCart()
        {
            // Arrange
            var cart = new Cart();
            var initialPrice = cart.Price;
            var itemPrice = 10.99; // Price of the item being added

            // Act
            cart.AddToCart(itemPrice);

            // Assert
            // In this scenario, an item is added to the cart.
            Assert.AreNotEqual(initialPrice, cart.Price);
            Assert.AreEqual(itemPrice, cart.Price);
        }

        [TestMethod]
        public void T4_2_ModifyingCartContents_Add()
        {
            // Arrange
            var cart = new Cart();
            var initialPrice = cart.Price;
            var itemPrice = 9.99; // Price of the item being added

            // Act
            cart.AddToCart(itemPrice);
            var updatedPrice = cart.Price;

            // Assert
            // In this scenario, an item is added, and its quantity increases.
            Assert.AreNotEqual(initialPrice, updatedPrice);
            Assert.AreEqual(itemPrice * 2, updatedPrice);
        }

        [TestMethod]
        public void T4_3_ModifyingCartContents_Remove()
        {
            // Arrange
            var cart = new Cart();
            var itemPrice = 11.99; // Price of the item being added

            // Act
            cart.AddToCart(itemPrice);
            var initialPrice = cart.Price;
            cart.RemoveFromCart(itemPrice);
            var updatedPrice = cart.Price;

            // Assert
            // In this scenario, an item is added and then removed from the cart.
            Assert.AreNotEqual(initialPrice, updatedPrice);
            Assert.AreEqual(0, updatedPrice);
        }

        [TestMethod]
        public void T4_4_ModifyingEmptyCart()
        {
            // Arrange
            var cart = new Cart();
            var initialPrice = cart.Price;

            // Act
            cart.RemoveFromCart(0); // Attempt to remove an item from an empty cart
            cart.AddToCart(0); // Attempt to add an item to an empty cart

            // Assert
            // In this scenario, an attempt is made to modify an empty cart.
            // The cart should remain empty, and there should be no errors.
            Assert.AreEqual(0, cart.Price);
            Assert.AreEqual(0, initialPrice);
        }
    }

    [TestClass]
    public class CartModel1Tests
    {
        [TestMethod]
        public void LoadPizzas_SortingAscending_Success()
        {
            // Arrange
            var dbContext = new Mock<ApplicationDbContext>();
            var cartModel = new CartModel(dbContext.Object);
            var pizzas = new List<Pizza>
        {
            new Pizza { PizzaName = "Pepperoni Pizza" },
            new Pizza { PizzaName = "Margarita Pizza" },
        }.AsQueryable();

            dbContext.Setup(d => d.Pizza).Returns(pizzas);

            // Act
            cartModel.LoadPizzas(null, "asc");

            // Assert
            CollectionAssert.AreEqual(pizzas.OrderBy(p => p.PizzaName).ToList(), cartModel.Pizzas.ToList());
        }

        [TestMethod]
        public void LoadPizzas_SortingDescending_Success()
        {
            // Arrange
            var dbContext = new Mock<ApplicationDbContext>();
            var cartModel = new CartModel(dbContext.Object);
            var pizzas = new List<Pizza>
        {
            new Pizza { PizzaName = "Margarita Pizza" },
            new Pizza { PizzaName = "Pepperoni Pizza" },
        }.AsQueryable();

            dbContext.Setup(d => d.Pizza).Returns(pizzas);

            // Act
            cartModel.LoadPizzas(null, "desc");

            // Assert
            CollectionAssert.AreEqual(pizzas.OrderByDescending(p => p.PizzaName).ToList(), cartModel.Pizzas.ToList());
        }

        [TestMethod]
        public void LoadPizzas_SortingEmptyCatalog_Success()
        {
            // Arrange
            var dbContext = new Mock<ApplicationDbContext>();
            var cartModel = new CartModel(dbContext.Object);
            var pizzas = new List<Pizza>().AsQueryable();

            dbContext.Setup(d => d.Pizza).Returns(pizzas);

            // Act
            cartModel.LoadPizzas(null, "asc");

            // Assert
            CollectionAssert.AreEqual(pizzas.ToList(), cartModel.Pizzas.ToList());
        }

        [TestMethod]
        public void LoadPizzas_SortingWithSpecialCharacters_Error()
        {
            // Arrange
            var dbContext = new Mock<ApplicationDbContext>();
            var cartModel = new CartModel(dbContext.Object);
            var pizzas = new List<Pizza>
        {
            new Pizza { PizzaName = "Pepperoni Pizza" },
            new Pizza { PizzaName = "Special P@zza" },
        }.AsQueryable();

            dbContext.Setup(d => d.Pizza).Returns(pizzas);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => cartModel.LoadPizzas(null, "asc"));
            // You may need to adjust the exception type based on how you handle special characters.
        }
    }

    [TestClass]
    public class BankTests
    {
        [TestMethod]
        public void T5_1_PaymentConfirmation_ValidPaymentDetails()
        {
            // Arrange
            var bank = new Bank
            {
                CardNumber = "1234567890123456", // A valid card number
                Expiry = DateTime.Now.AddYears(2), // Future expiry date
                CVV = 123, // A valid CVV
            };

            // Act and Assert
            // In this scenario, payment confirmation should be successful.
            Assert.IsTrue(bank.IsValidPaymentInformation());
        }

        [TestMethod]
        public void T5_2_InvalidPaymentInformation_InvalidCardNumber()
        {
            // Arrange
            var bank = new Bank
            {
                CardNumber = "1234", // Invalid card number (short)
                Expiry = DateTime.Now.AddYears(2), // Future expiry date
                CVV = 123, // A valid CVV
            };

            // Act and Assert
            // In this scenario, payment confirmation should fail due to an invalid card number.
            Assert.IsFalse(bank.IsValidPaymentInformation());
        }

        [TestMethod]
        public void T5_3_PaymentFailure_SimulatedFailure()
        {
            // Arrange
            var bank = new Bank
            {
                CardNumber = "1234567890123456", // A valid card number
                Expiry = DateTime.Now.AddYears(2), // Future expiry date
                CVV = 123, // A valid CVV
            };

            // Act and Assert
            // In this scenario, payment confirmation should fail due to a simulated payment failure.
            Assert.IsFalse(bank.SimulatePaymentFailure());
        }
    }

    [TestClass]
    public class CartModel2Tests
    {
        [TestMethod]
        public void T6_1_CalculateCartCost()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            var model = new CartModel(dbContext)
            {
                SearchTerm = null,
                SortOrder = null
            };

            var pizza1 = new Pizza
            {
                PizzaName = "Pizza 1",
                Price = 10
            };

            var pizza2 = new Pizza
            {
                PizzaName = "Pizza 2",
                Price = 15
            };

            var pizza3 = new Pizza
            {
                PizzaName = "Pizza 3",
                Price = 20
            };

            dbContext.Pizza.AddRange(pizza1, pizza2, pizza3);
            dbContext.SaveChanges();

            model.LoadPizzas(null, null);

            // Act
            var totalCost = model.Pizzas.Sum(p => p.Price);

            // Assert
            Assert.AreEqual(45, totalCost); // Total cost should be the sum of pizza prices
        }

        [TestMethod]
        public void T6_2_EmptyCartCost()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            var model = new CartModel(dbContext)
            {
                SearchTerm = null,
                SortOrder = null
            };

            // Act
            model.LoadPizzas(null, null);

            // Assert
            Assert.AreEqual(0, model.Pizzas.Sum(p => p.Price)); // Total cost should be $0.00 for an empty cart
        }

        [TestMethod]
        public void T6_3_QuantityCalculation()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            var model = new CartModel(dbContext)
            {
                SearchTerm = null,
                SortOrder = null
            };

            var pizza = new Pizza
            {
                PizzaName = "Pizza 1",
                Price = 10
            };

            dbContext.Pizza.Add(pizza);
            dbContext.SaveChanges();

            // Act
            model.LoadPizzas(null, null);

            // Modify the quantity of items in the cart
            foreach (var cartItem in model.Pizzas)
            {
                cartItem.Quantity = 3;
            }

            // Recalculate the total cost
            var totalCost = model.Pizzas.Sum(p => p.Price * cartItem.Quantity);

            // Assert
            Assert.AreEqual(30, totalCost); // Total cost should reflect the manually modified quantities
        }

        private ApplicationDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }
    }

    [TestClass]
    public class LoginModel1Tests
    {
        [TestMethod]
        public async Task T7_1_SuccessfulLogout()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            var model = new LoginModel(dbContext)
            {
                Email = "johndo@gmail.com",
                Password = "Johnny"
            };

            // Simulate a logged-in user by setting session variables
            var session = new MockHttpSession();
            model.HttpContext = new DefaultHttpContext { Session = session };
            session.SetString("FirstName", "John");

            // Act
            var result = await model.OnPostAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
            Assert.AreEqual("/Index", ((RedirectToPageResult)result).PageName);

            // After logout, the session variables should be cleared
            Assert.IsNull(session.GetString("FirstName"));
        }

        [TestMethod]
        public async Task T7_2_LogoutFromHomepage()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            var model = new LoginModel(dbContext)
            {
                Email = "johndo@gmail.com",
                Password = "Johnny"
            };

            // Simulate a logged-in user by setting session variables
            var session = new MockHttpSession();
            model.HttpContext = new DefaultHttpContext { Session = session };
            session.SetString("FirstName", "John");

            // Act
            var result = await model.OnPostAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
            Assert.AreEqual("/Login", ((RedirectToPageResult)result).PageName);

            // After logout, the session variables should be cleared
            Assert.IsNull(session.GetString("FirstName"));
        }

        [TestMethod]
        public async Task T7_3_LogoutWithoutLogin()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            var model = new LoginModel(dbContext);

            // Act
            var result = await model.OnPostAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(PageResult));
            Assert.AreEqual("Invalid email or password.", model.ViewData["ErrorMessage"]);
        }

        [TestMethod]
        public async Task T7_4_LogoutRedirect()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            var model = new LoginModel(dbContext)
            {
                Email = "johndo@gmail.com",
                Password = "Johnny"
            };

            // Simulate a logged-in user by setting session variables
            var session = new MockHttpSession();
            model.HttpContext = new DefaultHttpContext { Session = session };
            session.SetString("FirstName", "John");

            // Act
            var result = await model.OnPostAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
            Assert.AreEqual("/Index", ((RedirectToPageResult)result).PageName);

            // After logout, the session variables should be cleared
            Assert.IsNull(session.GetString("FirstName"));
        }

        private ApplicationDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }
    }

}

