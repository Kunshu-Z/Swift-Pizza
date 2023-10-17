using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SwiftPizza.Data;
using SwiftPizza.Views.Home;
using SwiftPizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UnitTesting1.Helpers;


namespace UnitTestSP
{
    [TestClass]


    public class LoginModelTests
    {



        [TestMethod]
        public async Task T1_1_LoginWithCorrectUsernameAndPassword()
        {
            // Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var user = new User
            {
                Email = "Johndo@gmail.com",
                Password = "Johnny",
                FirstName = "John",
                // Add other user properties as needed
            };

            var mockDbSet = new Mock<DbSet<User>>();
            mockDbSet.Setup(m => m.FindAsync(user.Email)).Returns(new ValueTask<User>(Task.FromResult(user)));

            dbContextMock.Setup(m => m.Users).Returns(mockDbSet.Object);

            var mockLoginModel = new Mock<LoginModel>(dbContextMock.Object)
            {
                CallBase = true
            };
            mockLoginModel.Setup(m => m.CurrentHttpContext).Returns(new DefaultHttpContext());
            mockLoginModel.Object.Email = "Johndo@gmail.com";
            mockLoginModel.Object.Password = "Johnny";

            // Act
            var result = await mockLoginModel.Object.OnPostAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
        }

        [TestMethod]
        public async Task T1_2_LoginWithCorrectUsernameAndIncorrectPassword()
        {
            // Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var user = new User
            {
                Email = "Johndo@gmail.com",
                Password = "Johnny",
                FirstName = "John",
                // Add other user properties as needed
            };

            var mockDbSet = new Mock<DbSet<User>>();
            mockDbSet.Setup(m => m.FindAsync(user.Email)).Returns(new ValueTask<User>(Task.FromResult(user)));

            dbContextMock.Setup(m => m.Users).Returns(mockDbSet.Object);

            var mockLoginModel = new Mock<LoginModel>(dbContextMock.Object)
            {
                CallBase = true
            };
            mockLoginModel.Setup(m => m.CurrentHttpContext).Returns(new DefaultHttpContext());
            mockLoginModel.Object.Email = "Johndo@gmail.com";
            mockLoginModel.Object.Password = "IncorrectPassword";

            // Act
            var result = await mockLoginModel.Object.OnPostAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(PageResult));
            Assert.AreEqual("Invalid email or password.", mockLoginModel.Object.ViewData["ErrorMessage"]);
        }


        [TestMethod]
        public async Task T1_3_LoginWithIncorrectUsernameAndCorrectPassword()
        {
            // Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var user = new User
            {
                Email = "Johndo@gmail.com",
                Password = "Johnny",
                FirstName = "John",
                // Add other user properties as needed
            };

            var mockDbSet = new Mock<DbSet<User>>();
            mockDbSet.Setup(m => m.FindAsync(user.Email)).Returns(new ValueTask<User>(Task.FromResult(user)));

            dbContextMock.Setup(m => m.Users).Returns(mockDbSet.Object);

            var mockLoginModel = new Mock<LoginModel>(dbContextMock.Object)
            {
                CallBase = true
            };
            mockLoginModel.Setup(m => m.CurrentHttpContext).Returns(new DefaultHttpContext());
            mockLoginModel.Object.Email = "IncorrectUsername";
            mockLoginModel.Object.Password = "Johnny";

            // Act
            var result = await mockLoginModel.Object.OnPostAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(PageResult));
            Assert.AreEqual("Invalid email or password.", mockLoginModel.Object.ViewData["ErrorMessage"]);
        }

        [TestMethod]
        public async Task T1_4_LoginWithIncorrectUsernameAndIncorrectPassword()
        {
            // Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var user = new User
            {
                Email = "Johndo@gmail.com",
                Password = "Johnny",
                FirstName = "John",
                // Add other user properties as needed
            };

            var mockDbSet = new Mock<DbSet<User>>();
            mockDbSet.Setup(m => m.FindAsync(user.Email)).Returns(new ValueTask<User>(Task.FromResult(user)));

            dbContextMock.Setup(m => m.Users).Returns(mockDbSet.Object);

            var mockLoginModel = new Mock<LoginModel>(dbContextMock.Object)
            {
                CallBase = true
            };
            mockLoginModel.Setup(m => m.CurrentHttpContext).Returns(new DefaultHttpContext());
            mockLoginModel.Object.Email = "IncorrectUsername";
            mockLoginModel.Object.Password = "IncorrectPassword";

            // Act
            var result = await mockLoginModel.Object.OnPostAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(PageResult));
            Assert.AreEqual("Invalid email or password.", mockLoginModel.Object.ViewData["ErrorMessage"]);
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

            var pizzas = new List<Pizza>
        {
            new Pizza { PizzaName = "Pepperoni Pizza" },
            new Pizza { PizzaName = "Margarita Pizza" },
        }.AsQueryable();

            var mockPizzaDbSet = new Mock<DbSet<Pizza>>();
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.Provider).Returns(pizzas.Provider);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.Expression).Returns(pizzas.Expression);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.ElementType).Returns(pizzas.ElementType);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.GetEnumerator()).Returns(pizzas.GetEnumerator());

            dbContext.Setup(d => d.Pizza).Returns(mockPizzaDbSet.Object);

            var cartModel = new CartModel(dbContext.Object);

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

            var pizzas = new List<Pizza>
    {
        new Pizza { PizzaName = "Pepperoni Pizza" },
        new Pizza { PizzaName = "Margarita Pizza" },
    }.AsQueryable();

            var mockPizzaDbSet = new Mock<DbSet<Pizza>>();
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.Provider).Returns(pizzas.Provider);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.Expression).Returns(pizzas.Expression);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.ElementType).Returns(pizzas.ElementType);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.GetEnumerator()).Returns(pizzas.GetEnumerator());

            dbContext.Setup(d => d.Pizza).Returns(mockPizzaDbSet.Object);

            var cartModel = new CartModel(dbContext.Object);

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

            var pizzas = new List<Pizza>
    {
        new Pizza { PizzaName = "Pepperoni Pizza" },
        new Pizza { PizzaName = "Margarita Pizza" },
    }.AsQueryable();

            var mockPizzaDbSet = new Mock<DbSet<Pizza>>();
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.Provider).Returns(pizzas.Provider);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.Expression).Returns(pizzas.Expression);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.ElementType).Returns(pizzas.ElementType);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.GetEnumerator()).Returns(pizzas.GetEnumerator());

            dbContext.Setup(d => d.Pizza).Returns(mockPizzaDbSet.Object);

            var cartModel = new CartModel(dbContext.Object);

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

            var pizzas = new List<Pizza>
    {
        new Pizza { PizzaName = "Pepperoni Pizza" },
        new Pizza { PizzaName = "Margarita Pizza" },
    }.AsQueryable();

            var mockPizzaDbSet = new Mock<DbSet<Pizza>>();
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.Provider).Returns(pizzas.Provider);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.Expression).Returns(pizzas.Expression);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.ElementType).Returns(pizzas.ElementType);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.GetEnumerator()).Returns(pizzas.GetEnumerator());

            dbContext.Setup(d => d.Pizza).Returns(mockPizzaDbSet.Object);

            var cartModel = new CartModel(dbContext.Object);

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
            var cart = new PizzaCartTest();
            var initialPizzaPrice = cart.Price;
            int pizzaId = 1;
            string pizzaName = "Pepperoni";
            int itemPizzaPrice = 11; // Since the price is an integer

            // Act
            cart.AddToCart(pizzaId, pizzaName, itemPizzaPrice);

            // Assert
            // In this scenario, an item is added to the cart.
            Assert.AreNotEqual(initialPizzaPrice, cart.Price);
            Assert.AreEqual(itemPizzaPrice, cart.Price);
        }

        [TestMethod]
        public void T4_2_ModifyingCartContents_Add()
        {
            // Arrange
            var cart = new PizzaCartTest();
            var initialPizzaPrice = cart.Price;
            int pizzaId = 2;
            string pizzaName = "Margarita";
            int itemPizzaPrice = 10; // Adjusted to integer

            // Act
            cart.AddToCart(pizzaId, pizzaName, itemPizzaPrice);
            cart.AddToCart(pizzaId, pizzaName, itemPizzaPrice); // Adding the same pizza again
            var updatedPizzaPrice = cart.Price;

            // Assert
            // In this scenario, the same item is added twice, and its quantity increases.
            Assert.AreNotEqual(initialPizzaPrice, updatedPizzaPrice);
            Assert.AreEqual(itemPizzaPrice * 2, updatedPizzaPrice);
        }

        [TestMethod]
        public void T4_3_ModifyingCartContents_Remove()
        {
            // Arrange
            var cart = new PizzaCartTest();
            int pizzaId = 3;
            string pizzaName = "Hawaiian";
            int itemPizzaPrice = 12; // Adjusted to integer

            // Act
            cart.AddToCart(pizzaId, pizzaName, itemPizzaPrice);
            var initialPizzaPrice = cart.Price;
            cart.RemoveFromCart(pizzaId);
            var updatedPizzaPrice = cart.Price;

            // Assert
            // In this scenario, an item is added and then removed from the cart.
            Assert.AreNotEqual(initialPizzaPrice, updatedPizzaPrice);
            Assert.AreEqual(0, updatedPizzaPrice);
        }


        [TestMethod]
        public void T4_4_ModifyingEmptyCart()
        {
            // Arrange
            var cart = new PizzaCartTest();
            var initialPizzaPrice = cart.Price;

            // Act
            cart.RemoveFromCart(0); // Attempt to remove an item from an empty cart
            cart.AddToCart(0, "Sample Pizza", 9);
            // Attempt to add an item to an empty cart

            // Assert
            // In this scenario, an attempt is made to modify an empty cart.
            // The cart should remain empty, and there should be no errors.
            Assert.AreEqual(0, cart.Price);
            Assert.AreEqual(0, initialPizzaPrice);
        }
    }

    [TestClass]
    public class CartModel1Tests
    {
        [TestMethod]
        public void LoadPizzas_SortingAscending_Success()
        {
            /// Arrange
            var dbContext = new Mock<ApplicationDbContext>();
            var pizzas = new List<Pizza>
{
    new Pizza { PizzaName = "Pepperoni Pizza" },
    new Pizza { PizzaName = "Margarita Pizza" },
}.AsQueryable();

            var mockPizzaDbSet = new Mock<DbSet<Pizza>>();
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.Provider).Returns(pizzas.Provider);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.Expression).Returns(pizzas.Expression);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.ElementType).Returns(pizzas.ElementType);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.GetEnumerator()).Returns(pizzas.GetEnumerator());

            dbContext.Setup(d => d.Pizza).Returns(mockPizzaDbSet.Object);

            var cartModel = new CartModel(dbContext.Object);

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
            var pizzas = new List<Pizza>
    {
        new Pizza { PizzaName = "Margarita Pizza" },
        new Pizza { PizzaName = "Pepperoni Pizza" },
    }.AsQueryable();

            var mockPizzaDbSet = CreateMockDbSet(pizzas);
            dbContext.Setup(d => d.Pizza).Returns(mockPizzaDbSet.Object);

            var cartModel = new CartModel(dbContext.Object);

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
            var pizzas = new List<Pizza>().AsQueryable();

            var mockPizzaDbSet = CreateMockDbSet(pizzas);
            dbContext.Setup(d => d.Pizza).Returns(mockPizzaDbSet.Object);

            var cartModel = new CartModel(dbContext.Object);

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
            var pizzas = new List<Pizza>
    {
        new Pizza { PizzaName = "Pepperoni Pizza" },
        new Pizza { PizzaName = "Special P@zza" },
    }.AsQueryable();

            var mockPizzaDbSet = CreateMockDbSet(pizzas);
            dbContext.Setup(d => d.Pizza).Returns(mockPizzaDbSet.Object);

            var cartModel = new CartModel(dbContext.Object);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => cartModel.LoadPizzas(null, "asc"));
            // You may need to adjust the exception type based on how you handle special characters.
        }
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
                PizzaPrice = 10
            };

            var pizza2 = new Pizza
            {
                PizzaName = "Pizza 2",
                PizzaPrice = 15
            };

            var pizza3 = new Pizza
            {
                PizzaName = "Pizza 3",
                PizzaPrice = 20
            };

            dbContext.Pizza.AddRange(pizza1, pizza2, pizza3);
            dbContext.SaveChanges();

            model.LoadPizzas(null, null);

            // Act
            var totalCost = model.Pizzas.Sum(p => p.PizzaPrice);

            // Assert
            Assert.AreEqual(45, totalCost); // Total cost should be the sum of pizza PizzaPrices
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
            Assert.AreEqual(0, model.Pizzas.Sum(p => p.PizzaPrice)); // Total cost should be $0.00 for an empty cart
        }

        [TestMethod]
        public void T6_3_PizzaQuantityCalculation()
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
                PizzaPrice = 10
            };

            dbContext.Pizza.Add(pizza);
            dbContext.SaveChanges();

            // Act
            model.LoadPizzas(null, null);

            // Modify the PizzaQuantity of items in the cart
            foreach (var cartItem in model.Pizzas)
            {
                cartItem.PizzaQuantity = 3;
            }

            // Recalculate the total cost
            var totalCost = model.Pizzas.Sum(p => p.PizzaPrice * p.PizzaQuantity); // Here's the change

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
            var dbContextMock = new Mock<ApplicationDbContext>();

            var mockLoginModel = new Mock<LoginModel>(dbContextMock.Object)
            {
                CallBase = true // Ensures other methods and properties use the real implementation
            };

            mockLoginModel.Setup(m => m.CurrentHttpContext).Returns(new DefaultHttpContext());
            mockLoginModel.Object.Email = "johndo@gmail.com";
            mockLoginModel.Object.Password = "Johnny";

            // Simulate a logged-in user by setting session variables
            var session = new MockHttpSession();
            mockLoginModel.Object.CurrentHttpContext.Session = session;
            session.SetString("FirstName", "John");

            // Act
            var result = await mockLoginModel.Object.OnPostAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
            Assert.AreEqual("/Index", ((RedirectToPageResult)result).PageName);
            Assert.IsNull(session.GetString("FirstName"));
        }

        [TestMethod]
        public async Task T7_2_LogoutFromHomepage()
        {
            // Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();

            var mockLoginModel = new Mock<LoginModel>(dbContextMock.Object)
            {
                CallBase = true
            };

            mockLoginModel.Setup(m => m.CurrentHttpContext).Returns(new DefaultHttpContext());
            mockLoginModel.Object.Email = "johndo@gmail.com";
            mockLoginModel.Object.Password = "Johnny";

            // Simulate a logged-in user by setting session variables
            var session = new MockHttpSession();
            mockLoginModel.Object.CurrentHttpContext.Session = session;
            session.SetString("FirstName", "John");

            // Act
            var result = await mockLoginModel.Object.OnPostAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
            Assert.AreEqual("/Login", ((RedirectToPageResult)result).PageName);
            Assert.IsNull(session.GetString("FirstName"));
        }

        [TestMethod]
        public async Task T7_3_LogoutWithoutLogin()
        {
            // Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();

            var mockLoginModel = new Mock<LoginModel>(dbContextMock.Object)
            {
                CallBase = true
            };

            mockLoginModel.Setup(m => m.CurrentHttpContext).Returns(new DefaultHttpContext());

            // Act
            var result = await mockLoginModel.Object.OnPostAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(PageResult));
            Assert.AreEqual("Invalid email or password.", mockLoginModel.Object.ViewData["ErrorMessage"]);
        }

        [TestMethod]
        public async Task T7_4_LogoutRedirect()
        {
            // Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();

            var mockLoginModel = new Mock<LoginModel>(dbContextMock.Object)
            {
                CallBase = true
            };

            mockLoginModel.Setup(m => m.CurrentHttpContext).Returns(new DefaultHttpContext());
            mockLoginModel.Object.Email = "johndo@gmail.com";
            mockLoginModel.Object.Password = "Johnny";

            // Simulate a logged-in user by setting session variables
            var session = new MockHttpSession();
            mockLoginModel.Object.CurrentHttpContext.Session = session;
            session.SetString("FirstName", "John");

            // Act
            var result = await mockLoginModel.Object.OnPostAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
            Assert.AreEqual("/Index", ((RedirectToPageResult)result).PageName);
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

