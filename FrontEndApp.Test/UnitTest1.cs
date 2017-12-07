using FrontEndApp.Controllers;
using FrontEndApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Claims;

namespace FrontEndApp.Test
{
    [TestClass]
    public class UnitTest1
    {
        AccountController accountController;
        CartController cartcontroller;
        HomeController homeController;
        MessageController messageController;
        ProfileController profileController;
        InvoiceController invoiceController;
        StaffOrderingController staffController;
        BillingController billingController;
        Client client;

        static FakeResponseHandler fakeResponseHandler200;
        static FakeResponseHandler fakeResponseHandler404;

        [AssemblyInitialize]
        public static void AssemblyIntializer(TestContext context)
        {
            #region 200 Http Status
            fakeResponseHandler200 = new FakeResponseHandler();

            #region Client Handler

            //My Messages
            fakeResponseHandler200.AddFakeResponse(
                new Uri("http://localhost:50143/"),
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<String>("Client Success", new JsonMediaTypeFormatter())
                });

            #endregion

            #region Messaging Handler
            //My Messages
            fakeResponseHandler200.AddFakeResponse(
                new Uri("http://localhost:50143/MessagesMVC/MyMessages/1"),
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<String>("Message MyMessages Success", new JsonMediaTypeFormatter())
                });

            //Send Message
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("http://localhost:50143/MessagesMVC/send/1"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Message Send Success", new JsonMediaTypeFormatter())
                 });

            //Save Message
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("http://localhost:50143/MessagesMVC/SaveMessage"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<MessageVM>(new MessageVM()
                     {
                         MessageID = 1,
                         Title = "Title",
                         MessageContent = "Content",
                         ReceiverUserID = "1",
                         SenderUserID = "1",
                         DateSent = DateTime.Now
                     }, new JsonMediaTypeFormatter())
                 });

            //Message Details
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("http://localhost:50143/MessagesMVC/details/1"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Message Details Success", new JsonMediaTypeFormatter())
                 });
            #endregion

            #region Product Handler
            //Product Index
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("http://localhost:54330/api/Product/Views/Index"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Product Index Success", new JsonMediaTypeFormatter())
                 });

            //Product Details
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("http://localhost:54330/api/Product/Views/ProductDetails?EAN=1"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Product Details Success", new JsonMediaTypeFormatter())
                 });
            #endregion

            #region Auth Handler
            //Register Page
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/Register"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Auth Register Success", new JsonMediaTypeFormatter())
                 });

            //Register Post
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/RegisterUser"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {

                 });

            //Login Page
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/Login"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Auth Login Success", new JsonMediaTypeFormatter())
                 });

            //Login Post
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/LoginReturn"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<TokenResponse>(
                         new TokenResponse()
                         {
                             Type = "Type",
                             ExpiresIn = 1.00,
                             AccessToken = "Token",
                             RequestAt = DateTime.Now
                         }, new JsonMediaTypeFormatter())
                 });

            //Logout
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/Logout"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<TokenResponse>(
                         new TokenResponse()
                         {
                             Type = "Type",
                             ExpiresIn = 1.00,
                             AccessToken = "Token",
                             RequestAt = DateTime.Now
                         }, new JsonMediaTypeFormatter())
                 });

            //View Users Page
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/ViewUserRoles"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Auth View User Roles Success", new JsonMediaTypeFormatter())
                 });

            //Edit Role Page
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/EditRole/1"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Auth Edit Role Success", new JsonMediaTypeFormatter())
                 });

            //Save Role
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/SaveRole?userId=1&_SelectedRoleID=1"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {

                 });

            //Edit Permission Page
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/EditPermission/1"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Auth Edit Permission Success", new JsonMediaTypeFormatter())
                 });

            //Save Permission
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/SavePermission?userId=1&_SelectedRoleID=1"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {

                 });

            #endregion

            #region Cart Handler
            //Cart Index
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("http://localhost:54997/api/CustomerOrdering/View/Cart"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Cart Index Success", new JsonMediaTypeFormatter())
                 });
            //Cart Orders
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("http://localhost:54997/api/CustomerOrdering/View/Orders"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Cart Orders Success", new JsonMediaTypeFormatter())
                 });
            #endregion

            #region Profile Handler
            //Profile Index
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("http://localhost:51520/User/Index"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Profile Index Success", new JsonMediaTypeFormatter())
                 });

            //Profile User Profile
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("http://localhost:51520/User/Profile/1"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Profile User Profile Success", new JsonMediaTypeFormatter())
                 });

            //Profile Edit
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("http://localhost:51520/User/Edit/1"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Profile Edit Success", new JsonMediaTypeFormatter())
                 });

            //Profile Edit Post
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("http://localhost:51520/User/EditProfilePost"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {

                 });
            #endregion

            #region Invoice Handler
            //Invoice for approval
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("http://localhost:54349/api/Invoice/Views/InvoicesForApproval"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Invoice for approval Success", new JsonMediaTypeFormatter())
                 });

            //Invoice for users
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("http://localhost:54349/api/Invoice/Views/InvoicesForUsers/1"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Invoice for user Success", new JsonMediaTypeFormatter())
                 });

            //Invoice details
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("http://localhost:54349/api/Invoice/Views/InvoiceDetails/1"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Invoice details Success", new JsonMediaTypeFormatter())
                 });
            #endregion

            #region Staff Ordering Handler

            //Staff Order Index Page
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("http://localhost:50492/api/StaffOrdering/View/Products"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Staff Index Success", new JsonMediaTypeFormatter())
                 });

            //Staff Order Cart Page
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("http://localhost:50492/api/StaffOrdering/View/Cart"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Staff Cart Success", new JsonMediaTypeFormatter())
                 });

            //Staff Order LowStock Page
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("http://localhost:50492/api/StaffOrdering/View/LowStock"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Staff LowStock Success", new JsonMediaTypeFormatter())
                 });

            //Staff Order Orders Page
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("http://localhost:50492/api/StaffOrdering/View/Orders"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Staff Orders Success", new JsonMediaTypeFormatter())
                 });

            //Staff Order Single Order Page
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("http://localhost:50492/api/StaffOrdering/View/Products/Order/1"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Staff Single Order Success", new JsonMediaTypeFormatter())
                 });

            //Staff Order Product Details Page
            fakeResponseHandler200.AddFakeResponse(
                 new Uri("http://localhost:50492/api/StaffOrdering/View/ProductDetails?ean=1"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Staff Product Details Success", new JsonMediaTypeFormatter())
                 });

            #endregion
            #endregion

            #region 404 Http Status

            fakeResponseHandler404 = new FakeResponseHandler();

            #region Client Handler

            //My Messages
            fakeResponseHandler404.AddFakeResponse(
                new Uri("http://localhost:50143/"),
                new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new ObjectContent<String>("Client Success", new JsonMediaTypeFormatter())
                });

            #endregion

            #region Messaging Handler
            //My Messages
            fakeResponseHandler404.AddFakeResponse(
                new Uri("http://localhost:50143/MessagesMVC/MyMessages/1"),
                new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new ObjectContent<String>("Message MyMessages Success", new JsonMediaTypeFormatter())
                });

            //Send Message
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("http://localhost:50143/MessagesMVC/send/1"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Message Send Success", new JsonMediaTypeFormatter())
                 });

            //Save Message
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("http://localhost:50143/MessagesMVC/SaveMessage"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<MessageVM>(new MessageVM()
                     {
                         MessageID = 1,
                         Title = "Title",
                         MessageContent = "Content",
                         ReceiverUserID = "1",
                         SenderUserID = "1",
                         DateSent = DateTime.Now
                     }, new JsonMediaTypeFormatter())
                 });

            //Message Details
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("http://localhost:50143/MessagesMVC/details/1"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Message Details Success", new JsonMediaTypeFormatter())
                 });
            #endregion

            #region Product Handler
            //Product Index
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("http://localhost:54330/api/Product/Views/Index"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Product Index Success", new JsonMediaTypeFormatter())
                 });

            //Product Details
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("http://localhost:54330/api/Product/Views/ProductDetails?EAN=1"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Product Details Success", new JsonMediaTypeFormatter())
                 });
            #endregion

            #region Auth Handler
            //Register Page
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/Register"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Auth Register Success", new JsonMediaTypeFormatter())
                 });

            //Register Post
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/RegisterUser"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {

                 });

            //Login Page
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/Login"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Auth Login Success", new JsonMediaTypeFormatter())
                 });

            //Login Post
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/LoginReturn"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<TokenResponse>(
                         new TokenResponse()
                         {
                             Type = "Type",
                             ExpiresIn = 1.00,
                             AccessToken = "Token",
                             RequestAt = DateTime.Now
                         }, new JsonMediaTypeFormatter())
                 });

            //Logout
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/Logout"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<TokenResponse>(
                         new TokenResponse()
                         {
                             Type = "Type",
                             ExpiresIn = 1.00,
                             AccessToken = "Token",
                             RequestAt = DateTime.Now
                         }, new JsonMediaTypeFormatter())
                 });

            //View Users Page
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/ViewUserRoles"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Auth View User Roles Success", new JsonMediaTypeFormatter())
                 });

            //Edit Role Page
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/EditRole/1"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Auth Edit Role Success", new JsonMediaTypeFormatter())
                 });

            //Save Role
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/SaveRole?userId=1&_SelectedRoleID=1"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {

                 });

            //Edit Permission Page
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/EditPermission/1"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Auth Edit Permission Success", new JsonMediaTypeFormatter())
                 });

            //Save Permission
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/SavePermission?userId=1&_SelectedRoleID=1"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {

                 });

            #endregion

            #region Cart Handler
            //Cart Index
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("http://localhost:54997/api/CustomerOrdering/View/Cart"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Cart Index Success", new JsonMediaTypeFormatter())
                 });
            //Cart Orders
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("http://localhost:54997/api/CustomerOrdering/View/Orders"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Cart Orders Success", new JsonMediaTypeFormatter())
                 });
            #endregion

            #region Profile Handler
            //Profile Index
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("http://localhost:51520/User/Index"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Profile Index Success", new JsonMediaTypeFormatter())
                 });

            //Profile User Profile
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("http://localhost:51520/User/Profile/1"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Profile User Profile Success", new JsonMediaTypeFormatter())
                 });

            //Profile Edit
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("http://localhost:51520/User/Edit/1"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Profile Edit Success", new JsonMediaTypeFormatter())
                 });

            //Profile Edit Post
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("http://localhost:51520/User/EditProfilePost"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {

                 });
            #endregion

            #region Invoice Handler
            //Invoice for approval
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("http://localhost:54349/api/Invoice/Views/InvoicesForApproval"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Invoice for approval Success", new JsonMediaTypeFormatter())
                 });

            //Invoice for users
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("http://localhost:54349/api/Invoice/Views/InvoicesForUsers/1"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Invoice for user Success", new JsonMediaTypeFormatter())
                 });

            //Invoice details
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("http://localhost:54349/api/Invoice/Views/InvoiceDetails/1"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Invoice details Success", new JsonMediaTypeFormatter())
                 });
            #endregion

            #region Staff Ordering Handler

            //Staff Order Index Page
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("http://localhost:50492/api/StaffOrdering/View/Products"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Staff Index Success", new JsonMediaTypeFormatter())
                 });

            //Staff Order Cart Page
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("http://localhost:50492/api/StaffOrdering/View/Cart"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Staff Cart Success", new JsonMediaTypeFormatter())
                 });

            //Staff Order LowStock Page
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("http://localhost:50492/api/StaffOrdering/View/LowStock"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Staff LowStock Success", new JsonMediaTypeFormatter())
                 });

            //Staff Order Orders Page
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("http://localhost:50492/api/StaffOrdering/View/Orders"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Staff Orders Success", new JsonMediaTypeFormatter())
                 });

            //Staff Order Single Order Page
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("http://localhost:50492/api/StaffOrdering/View/Products/Order/1"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Staff Single Order Success", new JsonMediaTypeFormatter())
                 });

            //Staff Order Product Details Page
            fakeResponseHandler404.AddFakeResponse(
                 new Uri("http://localhost:50492/api/StaffOrdering/View/ProductDetails?ean=1"),
                 new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new ObjectContent<String>("Staff Product Details Success", new JsonMediaTypeFormatter())
                 });

            #endregion

            #endregion
        }

        public void Initializer(int StatusExpected)
        {
            var testClaims = new ClaimsPrincipal(new ClaimsIdentity(TokenGenerator.UserToken("Customer").Claims));

            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.tests.json")
                .Build();

            var token = TokenGenerator.UserToken("Staff");
            var testClaim = new ClaimsPrincipal(new ClaimsIdentity(TokenGenerator.UserToken("Staff").Claims));

            client = new Client();

            if (StatusExpected == 200)
            {
                accountController = new AccountController(config, fakeResponseHandler200);
                cartcontroller = new CartController(config, fakeResponseHandler200);
                homeController = new HomeController(config, fakeResponseHandler200);
                messageController = new MessageController(config, fakeResponseHandler200);
                profileController = new ProfileController(config, fakeResponseHandler200);
                invoiceController = new InvoiceController(config, fakeResponseHandler200);
                staffController = new StaffOrderingController(config, fakeResponseHandler200);
            }
            else if (StatusExpected == 404)
            {
                accountController = new AccountController(config, fakeResponseHandler404);
                cartcontroller = new CartController(config, fakeResponseHandler404);
                homeController = new HomeController(config, fakeResponseHandler404);
                messageController = new MessageController(config, fakeResponseHandler404);
                profileController = new ProfileController(config, fakeResponseHandler404);
                invoiceController = new InvoiceController(config, fakeResponseHandler404);
                staffController = new StaffOrderingController(config, fakeResponseHandler404);
            }
            accountController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext
                {
                    User = testClaims
                }
            };
            accountController.ControllerContext.HttpContext.Request.Headers.Add("Authorization", "Bearer " + new JwtSecurityTokenHandler().WriteToken(token));

            cartcontroller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext
                {
                    User = testClaims
                }
            };
            cartcontroller.ControllerContext.HttpContext.Request.Headers.Add("Authorization", "Bearer " + new JwtSecurityTokenHandler().WriteToken(token));

            homeController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext
                {
                    User = testClaims
                }
            };
            homeController.ControllerContext.HttpContext.Request.Headers.Add("Authorization", "Bearer " + new JwtSecurityTokenHandler().WriteToken(token));

            messageController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext
                {
                    User = testClaims
                }
            };
            messageController.ControllerContext.HttpContext.Request.Headers.Add("Authorization", "Bearer " + new JwtSecurityTokenHandler().WriteToken(token));

            profileController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext
                {
                    User = testClaims
                }
            };
            profileController.ControllerContext.HttpContext.Request.Headers.Add("Authorization", "Bearer " + new JwtSecurityTokenHandler().WriteToken(token));

            invoiceController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext
                {
                    User = testClaims
                }
            };
            invoiceController.ControllerContext.HttpContext.Request.Headers.Add("Authorization", "Bearer " + new JwtSecurityTokenHandler().WriteToken(token));

            staffController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext
                {
                    User = testClaims
                }
            };
            staffController.ControllerContext.HttpContext.Request.Headers.Add("Authorization", "Bearer " + new JwtSecurityTokenHandler().WriteToken(token));

        }

        [TestMethod]
        public void ClientSuccess()
        {
            Initializer(200);
            var response = client.GetClient("http://localhost:50143/", "", "token", "Error", fakeResponseHandler200);
            Assert.AreEqual("\"Client Success\"", response.PartialView);
        }

        [TestMethod]
        public void Client404()
        {
            Initializer(404);
            var response = client.GetClient("http://localhost:50143/", "", "token", "Error", fakeResponseHandler404);
            Assert.AreEqual("<h2>Error</h2>", response.PartialView);
        }

        #region Auth Tests
        [TestMethod]
        public void AccountLoginSuccess()
        {
            Initializer(200);
            var response = (ViewResult)accountController.Login();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Auth Login Success\"", model.PartialView);
        }

        [TestMethod]
        public void AccountLogin404()
        {
            Initializer(404);
            var response = (ViewResult)accountController.Login();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>Login Service Down</h2>", model.PartialView);
        }

        [TestMethod]
        public void AccountLoginReturnSuccess()
        {
            Initializer(200);
            LoginViewModel vm = new LoginViewModel()
            {
                Email = "test@email.com",
                Password = "12345678",
                RememberMe = false
            };
            var response = (RedirectToActionResult)accountController.LoginReturn(vm);
            Assert.AreEqual("Home", response.ControllerName);
        }

        [TestMethod]
        public void AccountLoginReturn404()
        {
            Initializer(404);
            LoginViewModel vm = new LoginViewModel()
            {
                Email = "test@email.com",
                Password = "12345678",
                RememberMe = false
            };
            var response = (NotFoundResult)accountController.LoginReturn(vm);
            Assert.AreEqual(404, response.StatusCode);
        }

        [TestMethod]
        public void AccountRegisterSuccess()
        {
            Initializer(200);
            var response = (ViewResult)accountController.Register();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Auth Register Success\"", model.PartialView);
        }

        [TestMethod]
        public void AccountRegister404()
        {
            Initializer(404);
            var response = (ViewResult)accountController.Register();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>Register Service Down</h2>", model.PartialView);
        }

        [TestMethod]
        public void AccountRegisterUserSuccess()
        {
            Initializer(200);
            RegisterViewModel vm = new RegisterViewModel()
            {
                Email = "test@email.com",
                Password = "12345678",
                ConfirmPassword = "12345678"
            };
            var response = (RedirectToActionResult)accountController.RegisterUser(vm);
            Assert.AreEqual("Account", response.ControllerName);
        }

        [TestMethod]
        public void AccountRegisterUser404()
        {
            Initializer(404);
            RegisterViewModel vm = new RegisterViewModel()
            {
                Email = "test@email.com",
                Password = "12345678",
                ConfirmPassword = "12345678"
            };
            var response = (NotFoundResult)accountController.RegisterUser(vm);
            Assert.AreEqual(404, response.StatusCode);
        }

        [TestMethod]
        public void AccountLogoutSuccess()
        {
            Initializer(200);
            var response = (RedirectToActionResult)accountController.Logout();
            Assert.AreEqual("Home", response.ControllerName);
        }

        [TestMethod]
        public void AccountLogout404()
        {
            Initializer(404);
            var response = (NotFoundResult)accountController.Logout();
            Assert.AreEqual(404, response.StatusCode);
        }

        [TestMethod]
        public void AccountViewUserRolesSuccess()
        {
            Initializer(200);
            var response = (ViewResult)accountController.ViewUserRoles();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Auth View User Roles Success\"", model.PartialView);
        }

        [TestMethod]
        public void AccountViewUserRoles404()
        {
            Initializer(404);
            var response = (ViewResult)accountController.ViewUserRoles();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>Auth Service Down</h2>", model.PartialView);
        }

        [TestMethod]
        public void AccountEditRoleSuccess()
        {
            Initializer(200);
            var response = (ViewResult)accountController.EditRole("1");
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Auth Edit Role Success\"", model.PartialView);
        }

        [TestMethod]
        public void AccountEditRole404()
        {
            Initializer(404);
            var response = (ViewResult)accountController.EditRole("1");
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>Auth Service Down</h2>", model.PartialView);
        }

        [TestMethod]
        public void AccountSaveRoleSuccess()
        {
            Initializer(200);
            var response = (RedirectToActionResult)accountController.SaveRole("1", "1");
            Assert.AreEqual("Account", response.ControllerName);
        }

        [TestMethod]
        public void AccountSaveRole404()
        {
            Initializer(404);
            var response = (NotFoundResult)accountController.SaveRole("1", "1");
            Assert.AreEqual(404, response.StatusCode);
        }

        [TestMethod]
        public void AccountEditPermissionSuccess()
        {
            Initializer(200);
            var response = (ViewResult)accountController.EditPermission("1");
            var model = (PartialVM)response.Model;
            Assert.AreNotEqual("Auth Edit Permission Success", model.PartialView);
        }

        [TestMethod]
        public void AccountEditPermission404()
        {
            Initializer(404);
            var response = (ViewResult)accountController.EditPermission("1");
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>Auth Service Down</h2>", model.PartialView);
        }

        [TestMethod]
        public void AccountSavePermissionSuccess()
        {
            Initializer(200);
            var response = (RedirectToActionResult)accountController.SavePermission("1", "1");
            Assert.AreEqual("Account", response.ControllerName);
        }

        [TestMethod]
        public void AccountSavePermission404()
        {
            Initializer(404);
            var response = (NotFoundResult)accountController.SavePermission("1", "1");
            Assert.AreEqual(404, response.StatusCode);
        }

        #endregion

        #region Cart Tests

        [TestMethod]
        public void CartIndexSuccess()
        {
            Initializer(200);
            var response = (ViewResult)cartcontroller.Index();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Cart Index Success\"", model.PartialView);
        }


        [TestMethod]
        public void CartIndex404()
        {
            Initializer(404);
            var response = (ViewResult)cartcontroller.Index();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>Cart Service Down</h2>", model.PartialView);
        }

        [TestMethod]
        public void CartMyOrdersSuccess()
        {
            Initializer(200);
            var response = (ViewResult)cartcontroller.MyOrders();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Cart Orders Success\"", model.PartialView);
        }

        [TestMethod]
        public void CartMyOrders404()
        {
            Initializer(404);
            var response = (ViewResult)cartcontroller.MyOrders();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>Order Service Down</h2>", model.PartialView);
        }
        #endregion

        #region Product Tests

        [TestMethod]
        public void ProductIndexSuccess()
        {
            Initializer(200);
            var response = (ViewResult)homeController.Index();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Product Index Success\"", model.PartialView);
        }

        [TestMethod]
        public void ProductIndex404()
        {
            Initializer(404);
            var response = (ViewResult)homeController.Index();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>Products not Found</h2>", model.PartialView);
        }

        [TestMethod]
        public void ProductDetailsSuccess()
        {
            Initializer(200);
            var response = (ViewResult)homeController.ProductDetails("1");
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Product Details Success\"", model.PartialView);
        }

        [TestMethod]
        public void ProductDetails404()
        {
            Initializer(404);
            var response = (ViewResult)homeController.ProductDetails("1");
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>Product Details not Found</h2>", model.PartialView);
        }

        #endregion

        #region Message Tests

        [TestMethod]
        public void MessageMyMessageSuccess()
        {
            Initializer(200);
            var response = (ViewResult)messageController.MyMessages("1");
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Message MyMessages Success\"", model.PartialView);
        }

        [TestMethod]
        public void MessageMyMessage404()
        {
            Initializer(404);
            var response = (ViewResult)messageController.MyMessages("1");
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>Messaging Service Down</h2>", model.PartialView);
        }

        [TestMethod]
        public void MessageSendSuccess()
        {
            Initializer(200);
            var response = (ViewResult)messageController.Send("1");
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Message Send Success\"", model.PartialView);
        }

        [TestMethod]
        public void MessageSend404()
        {
            Initializer(404);
            var response = (ViewResult)messageController.Send("1");
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>Messaging Service Down</h2>", model.PartialView);
        }

        [TestMethod]
        public void MessageSaveMessageSuccess()
        {
            Initializer(200);
            MessageVM vm = new MessageVM()
            {
                MessageID = 1,
                Title = "Title",
                MessageContent = "Content",
                ReceiverUserID = "1",
                SenderUserID = "1",
                DateSent = DateTime.Now
            };
            var response = (RedirectToActionResult)messageController.SaveMessage(vm);
            Assert.AreEqual("Profile", response.ControllerName);
        }

        [TestMethod]
        public void MessageSaveMessage404()
        {
            Initializer(404);
            MessageVM vm = new MessageVM()
            {
                MessageID = 1,
                Title = "Title",
                MessageContent = "Content",
                ReceiverUserID = "1",
                SenderUserID = "1",
                DateSent = DateTime.Now
            };
            var response = (NotFoundResult)messageController.SaveMessage(vm);
            Assert.AreEqual(404, response.StatusCode);
        }

        [TestMethod]
        public void MessageDetailsSuccess()
        {
            Initializer(200);
            var response = (ViewResult)messageController.Details(1);
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Message Details Success\"", model.PartialView);
        }

        [TestMethod]
        public void MessageDetails404()
        {
            Initializer(404);
            var response = (ViewResult)messageController.Details(1);
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>Messaging Service Down</h2>", model.PartialView);
        }
        #endregion

        #region Profile Tests

        [TestMethod]
        public void ProfileIndexSuccess()
        {
            Initializer(200);
            var response = (ViewResult)profileController.Index();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Profile Index Success\"", model.PartialView);
        }

        [TestMethod]
        public void ProfileIndex404()
        {
            Initializer(404);
            var response = (ViewResult)profileController.Index();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>User Service Down</h2>", model.PartialView);
        }

        [TestMethod]
        public void ProfileUserProfileSuccess()
        {
            Initializer(200);
            var response = (ViewResult)profileController.Profile("1");
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Profile User Profile Success\"", model.PartialView);
        }

        [TestMethod]
        public void ProfileUserProfile404()
        {
            Initializer(404);
            var response = (ViewResult)profileController.Profile("1");
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>User Service Down</h2>", model.PartialView);
        }

        [TestMethod]
        public void ProfileEditSuccess()
        {
            Initializer(200);
            var response = (ViewResult)profileController.Edit("1");
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Profile Edit Success\"", model.PartialView);
        }

        [TestMethod]
        public void ProfileEdit404()
        {
            Initializer(404);
            var response = (ViewResult)profileController.Edit("1");
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>User Service Down</h2>", model.PartialView);
        }

        [TestMethod]
        public void ProfileEditPostSuccess()
        {
            Initializer(200);
            User vm = new User()
            {
                ID = 1,
                UserID = "1",
                Name = "Name",
                Email = "Email",
                Authorised = true,
                Active = true
            };
            var response = (RedirectToActionResult)profileController.EditProfilePost(vm);
            Assert.AreEqual("Index", response.ActionName);
        }

        [TestMethod]
        public void ProfileEditPost404()
        {
            Initializer(404);
            User vm = new User()
            {
                ID = 1,
                UserID = "1",
                Name = "Name",
                Email = "Email",
                Authorised = true,
                Active = true
            };
            var response = (NotFoundResult)profileController.EditProfilePost(vm);
            Assert.AreEqual(404, response.StatusCode);
        }
        #endregion

        #region Invoice Tests

        [TestMethod]
        public void InvoiceForApprovalSuccess()
        {
            Initializer(200);
            var response = (ViewResult)invoiceController.InvoicesForApproval();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Invoice for approval Success\"", model.PartialView);
        }

        [TestMethod]
        public void InvoiceForApproval404()
        {
            Initializer(404);
            var response = (ViewResult)invoiceController.InvoicesForApproval();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>Invoice Service Down</h2>", model.PartialView);
        }

        [TestMethod]
        public void InvoiceForUserSuccess()
        {
            Initializer(200);
            var response = (ViewResult)invoiceController.InvoicesForUser("1");
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Invoice for user Success\"", model.PartialView);
        }

        [TestMethod]
        public void InvoiceForUser404()
        {
            Initializer(404);
            var response = (ViewResult)invoiceController.InvoicesForUser("1");
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>Invoice Service Down</h2>", model.PartialView);
        }

        [TestMethod]
        public void InvoiceDetailsSuccess()
        {
            Initializer(200);
            var response = (ViewResult)invoiceController.InvoicesDetails("1");
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Invoice details Success\"", model.PartialView);
        }

        [TestMethod]
        public void InvoiceDetails404()
        {
            Initializer(404);
            var response = (ViewResult)invoiceController.InvoicesDetails("1");
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>Invoice Service Down</h2>", model.PartialView);
        }

        #endregion

        #region Staff Tests

        [TestMethod]
        public void StaffIndexSuccess()
        {
            Initializer(200);
            var response = (ViewResult)staffController.Index();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Staff Index Success\"", model.PartialView);
        }

        [TestMethod]
        public void StaffIndex404()
        {
            Initializer(404);
            var response = (ViewResult)staffController.Index();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>Stock Ordering Service Down</h2>", model.PartialView);
        }

        [TestMethod]
        public void StaffCartSuccess()
        {
            Initializer(200);
            var response = (ViewResult)staffController.Cart();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Staff Cart Success\"", model.PartialView);
        }

        [TestMethod]
        public void StaffCart404()
        {
            Initializer(404);
            var response = (ViewResult)staffController.Cart();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>Stock Ordering Service Down</h2>", model.PartialView);
        }

        [TestMethod]
        public void StaffLowStockSuccess()
        {
            Initializer(200);
            var response = (ViewResult)staffController.LowStock();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Staff LowStock Success\"", model.PartialView);
        }

        [TestMethod]
        public void StaffLowStock404()
        {
            Initializer(404);
            var response = (ViewResult)staffController.LowStock();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>Stock Ordering Service Down</h2>", model.PartialView);
        }

        [TestMethod]
        public void StaffOrdersSuccess()
        {
            Initializer(200);
            var response = (ViewResult)staffController.Orders();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Staff Orders Success\"", model.PartialView);
        }

        [TestMethod]
        public void StaffOrders404()
        {
            Initializer(404);
            var response = (ViewResult)staffController.Orders();
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>Stock Ordering Service Down</h2>", model.PartialView);
        }

        [TestMethod]
        public void StaffSingleOrderSuccess()
        {
            Initializer(200);
            var response = (ViewResult)staffController.Order(1);
            var model = (PartialVM)response.Model;
            Assert.AreEqual("\"Staff Single Order Success\"", model.PartialView);
        }

        [TestMethod]
        public void StaffSingleOrder404()
        {
            Initializer(404);
            var response = (ViewResult)staffController.Order(1);
            var model = (PartialVM)response.Model;
            Assert.AreEqual("<h2>Stock Ordering Service Down</h2>", model.PartialView);
        }

        [TestMethod]
        public void StaffProductDetailsSuccess()
        {
            Initializer(200);
            var response = (ViewResult)staffController.ProductDetails("1");
            var model = (List<String>)response.Model;
            Assert.AreEqual("\"Staff Product Details Success\"", model[0]);
        }

        [TestMethod]
        public void StaffProductDetails404()
        {
            Initializer(404);
            var response = (ViewResult)staffController.ProductDetails("1");
            var model = (List<String>)response.Model;
            Assert.AreEqual("<h2>Stock Ordering Service Down</h2>", model[0]);
        }

        #endregion
    }
}
