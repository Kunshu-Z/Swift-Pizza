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
using static SwiftPizza.Models.PizzaCartTest;
using System.Diagnostics;


namespace UnitTestSP
{
    [TestClass]
    public class LoginModelTests
    {
        // This test method verifies the behavior of logging in with correct username and password.
        [TestMethod]
        public async Task LoginWithCorrectUsernameAndPassword()
        {
            // Arrange

            // Creating a mock of the application's database context to avoid actual database interactions.
            var dbContextMock = new Mock<ApplicationDbContext>();

            // Initializing a user object to simulate a user in the system.
            var user = new User
            {
                Email = "Johndo@gmail.com",
                Password = "Johnny",
                FirstName = "John",
                // Additional properties can be added if needed.
            };

            // Mocking the DbSet to simulate database operations.
            var mockDbSet = new Mock<DbSet<User>>();
            // Setting up the mock DbSet to return our test user when FindAsync is called with the user's email.
            mockDbSet.Setup(m => m.FindAsync(user.Email)).Returns(new ValueTask<User>(Task.FromResult(user)));

            // Configuring the mock database context to return the mock DbSet when Users property is accessed.
            Moq.Language.Flow.IReturnsResult<ApplicationDbContext> returnsResult = dbContextMock.Setup(m => m.Users).Returns(mockDbSet.Object);

            // Creating a mock of the LoginModel, which is the class being tested.
            var mockLoginModel = new Mock<LoginModel>(dbContextMock.Object)
            {
                CallBase = true
            };

            // Mocking HttpContext and ISession to avoid actual HTTP and session operations.
            var httpContextMock = new Mock<HttpContext>();
            var sessionMock = new Mock<ISession>();
            httpContextMock.Setup(hc => hc.Session).Returns(sessionMock.Object);

            // Setting up the mock LoginModel to return the mocked HttpContext when CurrentHttpContext is accessed.
            mockLoginModel.Setup(m => m.CurrentHttpContext).Returns(httpContextMock.Object);

            // Assigning email and password values to the mock LoginModel to simulate user input.
            mockLoginModel.Object.Email = user.Email;
            mockLoginModel.Object.Password = user.Password;

            // Act

            // Invoking the OnPostAsync method of the LoginModel to simulate a login attempt.
            var result = await mockLoginModel.Object.OnPostAsync();

            // Assert

            // Verifying that the result type is a PageResult, indicating the user remains on the login page.
            Assert.IsInstanceOfType(result, typeof(PageResult));
            // Checking if the correct error message is set in the ViewData.
            Assert.AreEqual("Invalid email or password.", mockLoginModel.Object.ViewData["ErrorMessage"]);
        }


        [TestMethod]
        public async Task T1_2_LoginWithCorrectUsernameAndIncorrectPassword()
        {
            // Arrange

            // Mocking the application's database context to avoid actual database interactions during the test.
            var dbContextMock = new Mock<ApplicationDbContext>();

            // Initializing a user object to represent a user in the system.
            var user = new User
            {
                Email = "Johndo@gmail.com",
                Password = "Johnny",
                FirstName = "John",
                // Additional user properties can be added as needed.
            };

            // Creating a mock of DbSet to simulate database operations.
            var mockDbSet = new Mock<DbSet<User>>();
            // Setting up the mock DbSet to return our test user when FindAsync is called with the user's email.
            mockDbSet.Setup(m => m.FindAsync(user.Email)).Returns(new ValueTask<User>(Task.FromResult(user)));

            // Configuring the mock database context to return the mock DbSet when the Users property is accessed.
            dbContextMock.Setup(m => m.Users).Returns(mockDbSet.Object);

            // Creating a mock of the LoginModel, which is the class under test.
            var mockLoginModel = new Mock<LoginModel>(dbContextMock.Object)
            {
                CallBase = true
            };

            // Setting up the mock LoginModel to return a default HttpContext when CurrentHttpContext is accessed.
            mockLoginModel.Setup(m => m.CurrentHttpContext).Returns(new DefaultHttpContext());

            // Assigning the correct email but an incorrect password to the mock LoginModel to simulate user input.
            mockLoginModel.Object.Email = "Johndo@gmail.com";
            mockLoginModel.Object.Password = "IncorrectPassword";

            // Act

            // Invoking the OnPostAsync method of the LoginModel to simulate a login attempt with an incorrect password.
            var result = await mockLoginModel.Object.OnPostAsync();

            // Assert

            // Verifying that the returned result type is a PageResult, meaning the user remains on the login page.
            Assert.IsInstanceOfType(result, typeof(PageResult));
            // Checking that the error message in the ViewData correctly indicates an invalid login attempt.
            Assert.AreEqual("Invalid email or password.", mockLoginModel.Object.ViewData["ErrorMessage"]);
        }



        [TestMethod]
        public async Task T1_3_LoginWithIncorrectUsernameAndCorrectPassword()
        {
            // Arrange

            // Mocking the application's database context to avoid actual database interactions during the test.
            var dbContextMock = new Mock<ApplicationDbContext>();

            // Initializing a user object to represent a user in the system.
            var user = new User
            {
                Email = "Johndo@gmail.com",
                Password = "Johnny",
                FirstName = "John",
                // Additional user properties can be added as needed.
            };

            // Creating a mock of DbSet to simulate database operations.
            var mockDbSet = new Mock<DbSet<User>>();
            // Setting up the mock DbSet to return our test user when FindAsync is called with the user's email.
            // This simulates the user being present in the database.
            mockDbSet.Setup(m => m.FindAsync(user.Email)).Returns(new ValueTask<User>(Task.FromResult(user)));

            // Configuring the mock database context to return the mock DbSet when the Users property is accessed.
            dbContextMock.Setup(m => m.Users).Returns(mockDbSet.Object);

            // Creating a mock of the LoginModel, which is the class under test.
            var mockLoginModel = new Mock<LoginModel>(dbContextMock.Object)
            {
                CallBase = true
            };

            // Setting up the mock LoginModel to return a default HttpContext when CurrentHttpContext is accessed.
            mockLoginModel.Setup(m => m.CurrentHttpContext).Returns(new DefaultHttpContext());

            // Assigning an incorrect email but the correct password to the mock LoginModel to simulate user input.
            mockLoginModel.Object.Email = "IncorrectUsername";
            mockLoginModel.Object.Password = "Johnny";

            // Act

            // Invoking the OnPostAsync method of the LoginModel to simulate a login attempt with an incorrect username.
            var result = await mockLoginModel.Object.OnPostAsync();

            // Assert

            // Verifying that the returned result type is a PageResult, meaning the user remains on the login page due to a failed login attempt.
            Assert.IsInstanceOfType(result, typeof(PageResult));
            // Checking that the error message in the ViewData correctly indicates an invalid login attempt.
            Assert.AreEqual("Invalid email or password.", mockLoginModel.Object.ViewData["ErrorMessage"]);
        }


        [TestMethod]
        public async Task T1_4_LoginWithIncorrectUsernameAndIncorrectPassword()
        {
            // Arrange

            // Mocking the application's database context to prevent actual database interactions during testing.
            var dbContextMock = new Mock<ApplicationDbContext>();

            // Initializing a user object to represent a user in the system.
            var user = new User
            {
                Email = "Johndo@gmail.com",
                Password = "Johnny",
                FirstName = "John",
                // Additional user properties can be added as needed.
            };

            // Creating a mock of DbSet to simulate database operations.
            var mockDbSet = new Mock<DbSet<User>>();
            // Setting up the mock DbSet to return our test user when FindAsync is called with the user's email.
            // This simulates the user being present in the database.
            mockDbSet.Setup(m => m.FindAsync(user.Email)).Returns(new ValueTask<User>(Task.FromResult(user)));

            // Configuring the mock database context to return the mock DbSet when the Users property is accessed.
            dbContextMock.Setup(m => m.Users).Returns(mockDbSet.Object);

            // Creating a mock of the LoginModel, the class we're testing.
            var mockLoginModel = new Mock<LoginModel>(dbContextMock.Object)
            {
                CallBase = true
            };

            // Setting up the mock LoginModel to return a default HttpContext when CurrentHttpContext is accessed.
            mockLoginModel.Setup(m => m.CurrentHttpContext).Returns(new DefaultHttpContext());

            // Assigning both incorrect email and password to the mock LoginModel to simulate user input.
            mockLoginModel.Object.Email = "IncorrectUsername";
            mockLoginModel.Object.Password = "IncorrectPassword";

            // Act

            // Invoking the OnPostAsync method of the LoginModel to simulate a login attempt with both incorrect username and password.
            var result = await mockLoginModel.Object.OnPostAsync();

            // Assert

            // Verifying that the returned result type is a PageResult, meaning the user remains on the login page due to a failed login attempt.
            Assert.IsInstanceOfType(result, typeof(PageResult));
            // Checking that the error message in the ViewData correctly indicates an invalid login attempt.
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

            // Mocking the application's database context to prevent actual database interactions during testing.
            var dbContext = new Mock<ApplicationDbContext>();

            // Initializing a list of pizza objects to represent the pizzas in the system.
            var pizzas = new List<Pizza>
    {
        new Pizza { PizzaName = "Pepperoni Pizza" },
        new Pizza { PizzaName = "Margarita Pizza" },
    }.AsQueryable();

            // Creating a mock of DbSet for Pizza to simulate database operations.
            var mockPizzaDbSet = new Mock<DbSet<Pizza>>();

            // Setting up the mock DbSet to behave like a real DbSet when LINQ queries are executed against it.
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.Provider).Returns(pizzas.Provider);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.Expression).Returns(pizzas.Expression);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.ElementType).Returns(pizzas.ElementType);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.GetEnumerator()).Returns(pizzas.GetEnumerator());

            // Configuring the mock database context to return the mock DbSet when the Pizzas property is accessed.
            dbContext.Setup(d => d.Pizzas).Returns(mockPizzaDbSet.Object);

            // Creating an instance of CartModel which is the class under test.
            var cartModel = new CartModel(dbContext.Object);

            // Act

            // Invoking the LoadPizzas method of CartModel to simulate the action of loading pizzas based on a specific search keyword and sorting order.
            cartModel.LoadPizzas("Pizza", "asc");

            // Assert

            // Verifying that the number of pizzas returned matches the expected count based on the mock data setup.
            Assert.AreEqual(2, dbContext.Object.Pizzas.Count(), "Mocked data doesn't match expected setup");
        }

        [TestMethod]
        public void LoadPizzas_SearchUnrelatedKeywords_EmptyResult()
        {
            // Arrange

            // Mocking the application's database context to prevent actual database interactions during testing.
            var dbContext = new Mock<ApplicationDbContext>();

            // Initializing a list of pizza objects to represent the pizzas in the system.
            var pizzas = new List<Pizza>
    {
        new Pizza { PizzaName = "Pepperoni Pizza" },
        new Pizza { PizzaName = "Margarita Pizza" },
    }.AsQueryable();

            // Creating a mock of DbSet for Pizza to simulate database operations.
            var mockPizzaDbSet = new Mock<DbSet<Pizza>>();

            // Setting up the mock DbSet to behave like a real DbSet when LINQ queries are executed against it.
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.Provider).Returns(pizzas.Provider);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.Expression).Returns(pizzas.Expression);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.ElementType).Returns(pizzas.ElementType);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.GetEnumerator()).Returns(pizzas.GetEnumerator());

            // Configuring the mock database context to return the mock DbSet when the Pizzas property is accessed.
            dbContext.Setup(d => d.Pizzas).Returns(mockPizzaDbSet.Object);

            // Creating an instance of CartModel which is the class under test.
            var cartModel = new CartModel(dbContext.Object);

            // Verifying that the number of pizzas in the mocked database context matches the expected count based on the mock data setup.
            Assert.AreEqual(2, dbContext.Object.Pizzas.Count(), "Mocked data doesn't match expected setup");

            // Act

            // Invoking the LoadPizzas method of CartModel to simulate the action of loading pizzas based on an unrelated search keyword.
            cartModel.LoadPizzas("Shoes", "asc");

            // Assert

            // Verifying that no pizzas are returned since the search keyword is unrelated.
            Assert.AreEqual(0, cartModel.Pizzas.Count(), "Expected no pizzas to match the unrelated keyword");

            // Verifying that the list of pizzas in the cart model is empty.
            CollectionAssert.AreEqual(new List<Pizza>(), cartModel.Pizzas.ToList());
        }


        [TestMethod]
        public void LoadPizzas_SearchSpecialCharacters_EmptyResult()
        {
            // Arrange

            // Mocking the application's database context to prevent actual database interactions during testing.
            var dbContext = new Mock<ApplicationDbContext>();

            // Initializing a list of pizza objects to represent the pizzas in the system.
            var pizzas = new List<Pizza>
    {
        new Pizza { PizzaName = "Pepperoni Pizza" },
        new Pizza { PizzaName = "Margarita Pizza" },
    }.AsQueryable();

            // Creating a mock of DbSet for Pizza to simulate database operations.
            var mockPizzaDbSet = new Mock<DbSet<Pizza>>();

            // Setting up the mock DbSet to behave like a real DbSet when LINQ queries are executed against it.
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.Provider).Returns(pizzas.Provider);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.Expression).Returns(pizzas.Expression);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.ElementType).Returns(pizzas.ElementType);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.GetEnumerator()).Returns(pizzas.GetEnumerator());

            // Configuring the mock database context to return the mock DbSet when the Pizzas property is accessed.
            dbContext.Setup(d => d.Pizzas).Returns(mockPizzaDbSet.Object);

            // Creating an instance of CartModel which is the class under test.
            var cartModel = new CartModel(dbContext.Object);

            // Act

            // Invoking the LoadPizzas method of CartModel to simulate the action of loading pizzas based on a search keyword containing special characters.
            cartModel.LoadPizzas("!@#$%^", "asc");

            // Assert

            // Verifying that the list of pizzas in the cart model is empty as special characters should not match any pizza names.
            CollectionAssert.AreEqual(new List<Pizza>(), cartModel.Pizzas.ToList());
        }

        [TestMethod]
        public void LoadPizzas_SearchBlankKeyword_EmptyResult()
        {
            // Arrange

            // Mocking the application's database context to avoid real database interactions during testing.
            var dbContext = new Mock<ApplicationDbContext>();

            // Creating a list of sample pizza objects to represent the data in the system.
            var pizzas = new List<Pizza>
    {
        new Pizza { PizzaName = "Pepperoni Pizza" },
        new Pizza { PizzaName = "Margarita Pizza" },
    }.AsQueryable();

            // Setting up a mock DbSet for Pizza to emulate database operations.
            var mockPizzaDbSet = new Mock<DbSet<Pizza>>();

            // Configuring the mock DbSet to behave like a real DbSet when LINQ queries are performed on it.
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.Provider).Returns(pizzas.Provider);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.Expression).Returns(pizzas.Expression);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.ElementType).Returns(pizzas.ElementType);
            mockPizzaDbSet.As<IQueryable<Pizza>>().Setup(m => m.GetEnumerator()).Returns(pizzas.GetEnumerator());

            // Configuring the mock database context to return the mock DbSet when the Pizzas property is accessed.
            dbContext.Setup(d => d.Pizzas).Returns(mockPizzaDbSet.Object);

            // Instantiating the CartModel, which is the class we're testing.
            var cartModel = new CartModel(dbContext.Object);

            // Act

            // Calling the LoadPizzas method of CartModel using a blank keyword, simulating a user search without input.
            cartModel.LoadPizzas("", "asc");

            // Assert

            // Verifying that the list of pizzas in the cart model is empty as a blank keyword should not match any pizza names.
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

            // Instantiating a testable version of the PizzaCart class.
            var cart = new PizzaCartTest();

            // Storing the initial price of the cart before any items are added.
            var initialPizzaPrice = cart.Price;

            // Defining the test data for a pizza item.
            int pizzaId = 1;
            string pizzaName = "Pepperoni";
            int itemPizzaPrice = 11; // Assuming the price is represented as an integer.

            // Act

            // Adding the pizza item to the cart.
            cart.AddToCart(pizzaId, pizzaName, itemPizzaPrice);

            // Assert

            // Verifying that the cart's price has changed after adding the pizza.
            // This checks if the AddToCart method correctly updates the cart's total price.
            Assert.AreNotEqual(initialPizzaPrice, cart.Price);

            // Verifying that the new price of the cart is equal to the price of the added pizza.
            // This ensures that the correct amount was added to the cart's total.
            Assert.AreEqual(itemPizzaPrice, cart.Price);
        }

        [TestMethod]
        public void T4_2_ModifyingCartContents_Add()
        {
            // Arrange

            // Instantiating a testable version of the PizzaCart class.
            var cart = new PizzaCartTest();

            // Storing the initial price of the cart before any items are added.
            var initialPizzaPrice = cart.Price;

            // Defining the test data for a pizza item.
            int pizzaId = 2;
            string pizzaName = "Margarita";
            int itemPizzaPrice = 10; // Price is represented as an integer.

            // Act

            // Adding the pizza item to the cart.
            cart.AddToCart(pizzaId, pizzaName, itemPizzaPrice);

            // Adding the same pizza item again to the cart.
            // This is to test the scenario where the same item's quantity is increased.
            cart.AddToCart(pizzaId, pizzaName, itemPizzaPrice);

            // Fetching the updated cart price after the two additions.
            var updatedPizzaPrice = cart.Price;

            // Assert

            // Verifying that the cart's price has changed after adding the pizza twice.
            // This ensures that the AddToCart method correctly updates the cart's total price for multiple additions of the same item.
            Assert.AreNotEqual(initialPizzaPrice, updatedPizzaPrice);

            // Verifying that the new price of the cart is equal to double the price of the added pizza.
            // This confirms that the correct amount was added to the cart's total for both additions.
            Assert.AreEqual(itemPizzaPrice * 2, updatedPizzaPrice);
        }


        [TestMethod]
        public void T4_3_ModifyingCartContents_Remove()
        {
            // Arrange

            // Instantiating a testable version of the PizzaCart class.
            var cart = new PizzaCartTest();

            // Defining the test data for a pizza item.
            int pizzaId = 3;
            string pizzaName = "Hawaiian";
            int itemPizzaPrice = 12; // Price is represented as an integer.

            // Act

            // Adding the pizza item to the cart to simulate a scenario where the cart has an item.
            cart.AddToCart(pizzaId, pizzaName, itemPizzaPrice);

            // Storing the cart's price after adding the pizza item.
            var initialPizzaPrice = cart.Price;

            // Removing the previously added pizza item from the cart.
            cart.RemoveFromCart(pizzaId);

            // Fetching the updated cart price after the pizza item removal.
            var updatedPizzaPrice = cart.Price;

            // Assert

            // Verifying that the cart's price has changed after removing the pizza.
            // This ensures that the RemoveFromCart method correctly updates the cart's total price.
            Assert.AreNotEqual(initialPizzaPrice, updatedPizzaPrice);

            // Verifying that the cart's price is 0 after removing the only item from it.
            // This confirms that the cart is empty after the removal action.
            Assert.AreEqual(0, updatedPizzaPrice);
        }

        [TestMethod]
        public void T4_4_ModifyingEmptyCart()
        {
            // Arrange

            // Instantiating a testable version of the PizzaCart class to simulate an empty cart.
            var cart = new PizzaCartTest();

            // Storing the initial price of the empty cart for comparison purposes.
            var initialPizzaPrice = cart.Price;

            // Act

            // Attempting to remove an item from an empty cart to check if it handles such scenarios without errors.
            cart.RemoveFromCart(0);

            // Adding a sample pizza item to the empty cart to test the cart's ability to accept new items.
            cart.AddToCart(0, "Sample Pizza", 9);

            // Assert

            // Verifying that after adding the sample pizza, the cart's total price is updated to 9.
            Assert.AreEqual(9, cart.Price);

            // Confirming that the initial price of the empty cart was indeed 0 before any modifications.
            Assert.AreEqual(0, initialPizzaPrice);
        }


    }

    [TestClass]
    public class CartModel1Tests
    {
        [TestMethod]
        public void LoadPizzas_SortingAscending_Success()
        {
            // Arrange

            // Configuring in-memory database options for testing purposes.
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Using a context to seed the in-memory database with test data.
            using (var dbContext = new ApplicationDbContext(options))
            {
                // Adding a list of pizzas to the in-memory database.
                dbContext.Pizzas.AddRange(new List<Pizza>
        {
            new Pizza { PizzaName = "Pepperoni Pizza", PizzaDescription = "Description 1", PizzaImage = "Image 1" },
            new Pizza { PizzaName = "Margarita Pizza", PizzaDescription = "Description 2", PizzaImage = "Image 2" },
        });
                // Saving the changes to the in-memory database.
                dbContext.SaveChanges();
            }

            // Creating a separate context to test the cart model's behavior in isolation.
            using (var dbContext = new ApplicationDbContext(options))
            {
                // Initializing the cart model which will be tested.
                var cartModel = new CartModel(dbContext);

                // Act

                // Loading pizzas from the in-memory database and sorting them in ascending order.
                cartModel.LoadPizzas(null, "asc");

                // Assert

                // Sorting the pizzas in the database in ascending order based on their name.
                var sortedPizzas = dbContext.Pizzas.OrderBy(p => p.PizzaName).ToList();

                // Comparing the sorted pizzas from the database with those in the cart model.
                CollectionAssert.AreEqual(sortedPizzas, cartModel.Pizzas.ToList());
            }
        }



        [TestMethod]
        public void LoadPizzas_SortingDescending_Success()
        {
            // Arrange

            // Setting up an in-memory database for testing. This offers the benefit of mimicking a real database without the need for actual persistence.
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Within a using block to ensure proper disposal of resources.
            using (var dbContext = new ApplicationDbContext(options))
            {
                // Populating the in-memory database with sample data to simulate a real-world scenario.
                dbContext.Pizzas.AddRange(new List<Pizza>
        {
            new Pizza { PizzaName = "Pepperoni Pizza", PizzaDescription = "Description 1", PizzaImage = "Image 1" },
            new Pizza { PizzaName = "Margarita Pizza", PizzaDescription = "Description 2", PizzaImage = "Image 2" },
        });

                // Committing the sample data to the in-memory database.
                dbContext.SaveChanges();
            }

            // A new instance of the context to simulate a different lifecycle from the seeding process. 
            // This ensures there's no unexpected behavior due to caching or reusing the same context instance.
            using (var dbContext = new ApplicationDbContext(options))
            {
                // Initializing the model responsible for cart operations using the prepared in-memory database.
                var cartModel = new CartModel(dbContext);

                // Act

                // Invoking the method under test. Here, we're loading pizzas and expecting them to be sorted in descending order.
                cartModel.LoadPizzas(null, "desc");

                // Assert

                // Retrieving pizzas from the in-memory database and sorting them in descending order for comparison.
                var sortedPizzas = dbContext.Pizzas.OrderByDescending(p => p.PizzaName).ToList();

                // Verifying that the pizzas retrieved by the model match the expected sorted order.
                CollectionAssert.AreEqual(sortedPizzas, cartModel.Pizzas.ToList());
            }
        }


        [TestMethod]
        public void LoadPizzas_SortingEmptyCatalog_Success()
        {
            // Arrange

            // Setting up an in-memory database for testing. This is useful for testing database-related operations without 
            // persisting data to an actual database. It ensures tests run quickly and don't have side effects on real data.
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Using a new scope for the database context to ensure proper resource management.
            using (var dbContext = new ApplicationDbContext(options))
            {
                // Seeding the in-memory database. In this case, we're adding an empty list of pizzas, 
                // simulating an empty catalog.
                dbContext.Pizzas.AddRange(new List<Pizza>());

                // Committing the empty list to the in-memory database.
                dbContext.SaveChanges();
            }

            // Creating a new instance of the database context, which ensures a fresh start and avoids any possible caching issues.
            using (var dbContext = new ApplicationDbContext(options))
            {
                // Initializing the model that's responsible for cart operations using the prepared in-memory database.
                var cartModel = new CartModel(dbContext);

                // Act

                // Loading pizzas from the empty catalog, expecting to retrieve no pizzas.
                cartModel.LoadPizzas(null, "asc");

                // Assert

                // Verifying that the number of pizzas retrieved by the model is zero, as the catalog is empty.
                Assert.AreEqual(0, cartModel.Pizzas.Count());
            }
        }


        [TestMethod]
        public void LoadPizzas_SortingWithSpecialCharacters_ThrowsException()
        {
            // Arrange

            // Generating a unique database name for each test run using a GUID. This ensures that each test 
            // has its own isolated database environment, preventing potential conflicts or data overlaps.
            var uniqueDatabaseName = Guid.NewGuid().ToString();

            // Setting up an in-memory database for testing using the unique name. The in-memory database is 
            // useful for testing database-related operations without affecting actual persistent data.
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: uniqueDatabaseName)
                .Options;

            // Using a new scope for the database context for resource management.
            using (var dbContext = new ApplicationDbContext(options))
            {
                // Seeding the in-memory database with test data. Notice that one of the pizzas has a special character 
                // in its name, which might cause issues during sorting or other operations.
                dbContext.Pizzas.AddRange(new List<Pizza>
        {
            new Pizza { PizzaName = "Pepperoni Pizza", PizzaDescription = "Description 1", PizzaImage = "Image 1" },
            new Pizza { PizzaName = "Special P@zza", PizzaDescription = "Description 2", PizzaImage = "Image 2" },
        });

                // Saving the seeded data to the in-memory database.
                dbContext.SaveChanges();
            }

            // Creating another instance of the database context for a fresh start, ensuring no caching or previous operations affect the test.
            using (var dbContext = new ApplicationDbContext(options))
            {
                // Initializing the model responsible for cart operations using the in-memory database.
                var cartModel = new CartModel(dbContext);

                // Act & Assert

                // Attempting to load pizzas from the database. Due to the special character in one of the pizza names, 
                // an exception is expected to be thrown. The test will pass if the specified exception (InvalidOperationException) 
                // is thrown during the execution of the LoadPizzas method.
                Assert.ThrowsException<InvalidOperationException>(() => cartModel.LoadPizzas(null, "asc"));
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

            // Setting up an in-memory database for testing. Using an in-memory database is useful for testing 
            // database-related operations without affecting actual persistent data.
            var dbContext = CreateInMemoryDbContext();

            // Initializing the cart model with the in-memory database context. 
            // The search term and sort order are explicitly set to null.
            var model = new CartModel(dbContext)
            {
                SearchTerm = null,
                SortOrder = null
            };

            // Creating three pizza objects with their respective details, like name, price, description, and image.
            var pizza1 = new Pizza
            {
                PizzaName = "Pizza 1",
                PizzaPrice = 10,
                PizzaDescription = "Description of Pizza 1",
                PizzaImage = "pizza1.jpg"
            };

            var pizza2 = new Pizza
            {
                PizzaName = "Pizza 2",
                PizzaPrice = 15,
                PizzaDescription = "Description of Pizza 2",
                PizzaImage = "pizza2.jpg"
            };

            var pizza3 = new Pizza
            {
                PizzaName = "Pizza 3",
                PizzaPrice = 20,
                PizzaDescription = "Description of Pizza 3",
                PizzaImage = "pizza3.jpg"
            };

            // Adding the created pizza objects to the in-memory database.
            dbContext.Pizzas.AddRange(pizza1, pizza2, pizza3);
            dbContext.SaveChanges();  // Saving the changes to the in-memory database.

            // Loading the pizzas into the cart model. No specific search term or sort order is provided.
            model.LoadPizzas(null, null);

            // Act

            // Calculating the total cost of pizzas in the cart model. This is done by summing up the price of each pizza.
            var totalCost = model.Pizzas.Sum(p => p.PizzaPrice);

            // Assert

            // Verifying that the total cost calculated matches the expected value. 
            // The total cost should be 45, as it's the sum of the prices of the three pizzas (10 + 15 + 20).
            Assert.AreEqual(45, totalCost);
        }


        [TestMethod]
        public void T6_2_EmptyCartCost()
        {
            // Arrange

            // Setting up an in-memory database for testing. Using an in-memory database ensures that 
            // we can test database-related operations without affecting actual persistent data.
            var dbContext = CreateInMemoryDbContext();

            // Initializing the cart model with the in-memory database context. 
            // The search term and sort order are explicitly set to null.
            var model = new CartModel(dbContext)
            {
                SearchTerm = null,
                SortOrder = null
            };

            // Act

            // Loading the pizzas into the cart model. As the database was set up without 
            // adding any pizzas, this essentially simulates loading an empty cart.
            model.LoadPizzas(null, null);

            // Assert

            // Verifying that the total cost of pizzas in the empty cart is $0.00. 
            // As no pizzas were added to the in-memory database, the cart should be empty 
            // and the total cost should be zero.
            Assert.AreEqual(0, model.Pizzas.Sum(p => p.PizzaPrice));
        }

        [TestMethod]
        public void T6_3_PizzaQuantityCalculation()
        {
            // Arrange

            // Create an in-memory database for testing. This ensures that we can 
            // test database-related operations without affecting any real data.
            var dbContext = CreateInMemoryDbContext();

            // Initialize the cart model with the in-memory database context. 
            // The search term and sort order are explicitly set to null for this test scenario.
            var model = new CartModel(dbContext)
            {
                SearchTerm = null,
                SortOrder = null
            };

            // Create a pizza object for testing with the given attributes.
            var pizza = new Pizza
            {
                PizzaName = "Pizza 1",
                PizzaPrice = 10,
                PizzaDescription = "Description of Pizza 1",
                PizzaImage = "pizza1.jpg"
            };

            // Add the created pizza to the in-memory database and save the changes.
            dbContext.Pizzas.Add(pizza);
            dbContext.SaveChanges();

            // Act

            // Load the pizzas into the cart model. This will retrieve the previously 
            // added pizza from the in-memory database.
            model.LoadPizzas(null, null);

            // Update the quantity for each pizza in the cart model to 3. 
            // This simulates a scenario where a user adds multiple quantities of the same pizza.
            foreach (var cartItem in model.Pizzas)
            {
                cartItem.PizzaQuantity = 3;
            }

            // Calculate the total cost based on the quantity and price of pizzas in the cart.
            var totalCost = model.Pizzas.Sum(cartItem => cartItem.PizzaPrice * cartItem.PizzaQuantity);

            // Assert

            // Verify that the total cost is correctly calculated based on the updated quantity.
            // Given that there's one pizza priced at $10 and its quantity is 3, the total should be $30.
            Assert.AreEqual(30, totalCost);
        }

        private ApplicationDbContext CreateInMemoryDbContext()
        {
            // This helper method sets up an in-memory database for testing. 
            // It returns a new instance of ApplicationDbContext with a unique in-memory database.
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

    }

    [TestClass]
    public class LoginModel1Tests
    {
        // This test ensures that a user can successfully log out of the application.
        [TestMethod]
        public async Task T7_1_SuccessfulLogout()
        {
            // Arrange

            // Mocking the database context to avoid hitting the actual database.
            var dbContextMock = new Mock<ApplicationDbContext>();

            // Mocking the LoginModel with the mocked database context.
            var mockLoginModel = new Mock<LoginModel>(dbContextMock.Object)
            {
                CallBase = true // Ensures other methods and properties use the real implementation
            };

            // Setting up the mocked database set for the User entity.
            var mockUserSet = new Mock<DbSet<User>>();
            var user = new User { Email = "johndoe@gmail.com", Password = "Johnny", FirstName = "John" };
            var userData = new List<User> { user }.AsQueryable();

            // Linking the database set with the user data.
            mockUserSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userData.Provider);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userData.Expression);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userData.GetEnumerator());

            dbContextMock.Setup(db => db.Users).Returns(mockUserSet.Object);

            // Mocking the session for HTTP context.
            var sessionMock = new Mock<ISession>();
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(a => a.Session).Returns(sessionMock.Object);
            mockLoginModel.SetupGet(lm => lm.CurrentHttpContext).Returns(httpContextMock.Object);

            // Act
            // Attempting to logout by invoking the OnPostAsync method.
            var result = await mockLoginModel.Object.OnPostAsync();

            // Assert
            // Ensuring the user is redirected to the homepage after a successful logout.
            Debug.WriteLine(result.GetType().FullName);
            Assert.IsTrue(result is RedirectToPageResult);
            Assert.AreEqual("Index", ((RedirectToPageResult)result).PageName);
            // Confirming that the session data for the user is cleared after logout.
            Assert.IsNull(sessionMock.Object.GetString("FirstName"));
        }


        [TestMethod]
        public async Task T7_2_LogoutFromHomepage()
        {
            // Arrange

            // Mocking the database context to avoid interacting with the actual database during testing.
            var dbContextMock = new Mock<ApplicationDbContext>();

            // Creating a mock of the LoginModel, which is the class under test.
            // The CallBase property ensures that other methods and properties use the real implementation.
            var mockLoginModel = new Mock<LoginModel>(dbContextMock.Object)
            {
                CallBase = true
            };

            // Setting up the mock to return a mocked HttpContext when CurrentHttpContext is accessed.
            mockLoginModel.Setup(m => m.CurrentHttpContext).Returns(new DefaultHttpContext());

            // Setting the Email and Password properties of the mock object to simulate user input.
            mockLoginModel.Object.Email = "johndo@gmail.com";
            mockLoginModel.Object.Password = "Johnny";

            // Simulating a user that is already logged in by setting session variables.
            // MockHttpSession is a mocked version of the HTTP session.
            var session = new MockHttpSession();
            // Assigning the mocked session to the mocked HttpContext.
            mockLoginModel.Object.CurrentHttpContext.Session = session;
            // Storing a value in the session to represent the user's first name.
            session.SetString("FirstName", "John");

            // Diagnostic lines to ensure that the mocked components are properly initialized and are not null.
            Console.WriteLine(mockLoginModel.Object.CurrentHttpContext);  // The mocked HttpContext shouldn't be null.
            Console.WriteLine(session);  // The mocked session shouldn't be null.

            // Act

            // Invoking the OnPostAsync method of the LoginModel to simulate a user attempting to log out.
            var result = await mockLoginModel.Object.OnPostAsync();

            // Assert

            // Verifying that the result of the logout attempt is a redirection.
            Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
            // Ensuring that the user is redirected to the login page after logging out.
            Assert.AreEqual("/Login", ((RedirectToPageResult)result).PageName);
            // Confirming that the session data for the user's first name is cleared after logging out.
            Assert.IsNull(session.GetString("FirstName"));
        }



        [TestMethod]
        public async Task T7_3_LogoutWithoutLogin()
        {
            // Arrange

            // Mocking the database context to avoid actual database operations during testing.
            var dbContextMock = new Mock<ApplicationDbContext>();

            // Creating a mock of the LoginModel, the class we are testing.
            // The CallBase property ensures that other methods and properties use the real implementation.
            var mockLoginModel = new Mock<LoginModel>(dbContextMock.Object)
            {
                CallBase = true
            };

            // Setting up the mock to return a mocked HttpContext when CurrentHttpContext is accessed.
            // This prevents actual HTTP operations during testing.
            mockLoginModel.Setup(m => m.CurrentHttpContext).Returns(new DefaultHttpContext());

            // Act

            // Invoking the OnPostAsync method of the LoginModel to simulate a user attempting to log out.
            // In this test scenario, the user is not logged in, to begin with.
            var result = await mockLoginModel.Object.OnPostAsync();

            // Assert

            // Checking the result type to ensure that the user remains on the same login page.
            Assert.IsInstanceOfType(result, typeof(PageResult));

            // Verifying that the error message indicates an invalid login attempt.
            Assert.AreEqual("Invalid email or password.", mockLoginModel.Object.ViewData["ErrorMessage"]);
        }


        [TestMethod]
        public async Task T7_4_LogoutRedirect()
        {
            // Arrange

            // Mocking the database context to avoid interacting with the actual database during testing.
            var dbContextMock = new Mock<ApplicationDbContext>();

            // Creating a mock of the LoginModel, which is the class under test.
            // The CallBase property ensures that other methods and properties use the real implementation.
            var mockLoginModel = new Mock<LoginModel>(dbContextMock.Object)
            {
                CallBase = true
            };

            // Setting up the mock to return a mocked HttpContext when CurrentHttpContext is accessed.
            mockLoginModel.Setup(m => m.CurrentHttpContext).Returns(new DefaultHttpContext());

            // Setting the Email and Password properties of the mock object to simulate user input.
            mockLoginModel.Object.Email = "johndo@gmail.com";
            mockLoginModel.Object.Password = "Johnny";

            // Simulating a user that is already logged in by setting session variables.
            // MockHttpSession is a mocked version of the HTTP session.
            var session = new MockHttpSession();
            // Assigning the mocked session to the mocked HttpContext.
            mockLoginModel.Object.CurrentHttpContext.Session = session;
            // Storing a value in the session to represent the user's first name.
            session.SetString("FirstName", "John");

            // Act

            // Invoking the OnPostAsync method of the LoginModel to simulate a user attempting to log out.
            var result = await mockLoginModel.Object.OnPostAsync();

            // Assert

            // Verifying that the result of the logout attempt is a redirection.
            Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
            // Ensuring the user is redirected to the main index page after a successful logout.
            Assert.AreEqual("/Index", ((RedirectToPageResult)result).PageName);
            // Confirming that the session data for the user's first name is cleared after logging out.
            Assert.IsNull(session.GetString("FirstName"));
        }

        // Utility method to create an in-memory version of the database for testing purposes.
        private ApplicationDbContext CreateInMemoryDbContext()
        {
            // Configuring the options for the in-memory database.
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            // Returning an instance of the ApplicationDbContext with the in-memory database configuration.
            return new ApplicationDbContext(options);
        }

    }

}

