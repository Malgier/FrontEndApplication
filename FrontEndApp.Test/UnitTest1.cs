using FrontEndApp.Controllers;
using FrontEndApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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

        static FakeResponseHandler fakeResponseHandler;

        [AssemblyInitialize]
        public static void AssemblyIntializer(TestContext context)
        {
            fakeResponseHandler = new FakeResponseHandler();

            #region Messaging Handler
            //My Messages
            fakeResponseHandler.AddFakeResponse(
                new Uri("http://localhost:50143/MessagesMVC/MyMessages/1"),
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<String>("Message MyMessages Success", new JsonMediaTypeFormatter())
                });

            //Send Message
            fakeResponseHandler.AddFakeResponse(
                 new Uri("http://localhost:50143/MessagesMVC/send/1"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Message Send Success", new JsonMediaTypeFormatter())
                 });

            //Save Message
            fakeResponseHandler.AddFakeResponse(
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
            fakeResponseHandler.AddFakeResponse(
                 new Uri("http://localhost:50143/MessagesMVC/details/1"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Message Details Success", new JsonMediaTypeFormatter())
                 });
            #endregion

            #region Product Handler
            //Product Index
            fakeResponseHandler.AddFakeResponse(
                 new Uri("http://localhost:54330/api/Product/Views/Index"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Product Index Success", new JsonMediaTypeFormatter())
                 });

            //Product Details
            fakeResponseHandler.AddFakeResponse(
                 new Uri("http://localhost:54330/api/Product/Views/ProductDetails?EAN=1"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Product Details Success", new JsonMediaTypeFormatter())
                 });
            #endregion

            #region Auth Handler
            //Register Page
            fakeResponseHandler.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/register"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Auth Register Success", new JsonMediaTypeFormatter())
                 });

            //Register Post
            fakeResponseHandler.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/RegisterUser"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {

                 });

            //Login Page
            fakeResponseHandler.AddFakeResponse(
                 new Uri("https://localhost:44347/Account/Login"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Auth Login Success", new JsonMediaTypeFormatter())
                 });

            //Login Post
            fakeResponseHandler.AddFakeResponse(
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

            fakeResponseHandler.AddFakeResponse(
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

            #endregion

            #region Cart Handler
            //Cart Index
            fakeResponseHandler.AddFakeResponse(
                 new Uri("http://localhost:54997/api/CustomerOrdering/View/Cart"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Cart Index Success", new JsonMediaTypeFormatter())
                 });
            #endregion

            #region Profile Handler
            //Profile Index
            fakeResponseHandler.AddFakeResponse(
                 new Uri("http://localhost:51520/User/Index"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Profile Index Success", new JsonMediaTypeFormatter())
                 });

            //Profile User Profile
            fakeResponseHandler.AddFakeResponse(
                 new Uri("http://localhost:51520/User/Profile/1"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Profile User Profile Success", new JsonMediaTypeFormatter())
                 });

            //Profile Edit
            fakeResponseHandler.AddFakeResponse(
                 new Uri("http://localhost:51520/User/Edit/1"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ObjectContent<String>("Profile Edit Success", new JsonMediaTypeFormatter())
                 });

            //Profile Edit Post
            fakeResponseHandler.AddFakeResponse(
                 new Uri("http://localhost:51520/User/EditProfilePost"),
                 new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     
                 });
            #endregion
        }

        [TestInitialize]
        public void Initializer()
        {
            var testClaims = new ClaimsPrincipal(new ClaimsIdentity(TokenGenerator.UserToken("Customer").Claims));

            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.tests.json")
                .Build();

            var token = TokenGenerator.UserToken("Staff");
            var testClaim = new ClaimsPrincipal(new ClaimsIdentity(TokenGenerator.UserToken("Staff").Claims));

            accountController = new AccountController(config, fakeResponseHandler);
            accountController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext
                {
                    User = testClaims
                }
            };
            accountController.ControllerContext.HttpContext.Request.Headers.Add("Authorization", "Bearer " + new JwtSecurityTokenHandler().WriteToken(token));

            cartcontroller = new CartController(config, fakeResponseHandler);
            cartcontroller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext
                {
                    User = testClaims
                }
            };
            cartcontroller.ControllerContext.HttpContext.Request.Headers.Add("Authorization", "Bearer " + new JwtSecurityTokenHandler().WriteToken(token));

            homeController = new HomeController(config, fakeResponseHandler);
            homeController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext
                {
                    User = testClaims
                }
            };
            homeController.ControllerContext.HttpContext.Request.Headers.Add("Authorization", "Bearer " + new JwtSecurityTokenHandler().WriteToken(token));

            messageController = new MessageController(config, fakeResponseHandler);
            messageController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext
                {
                    User = testClaims
                }
            };
            messageController.ControllerContext.HttpContext.Request.Headers.Add("Authorization", "Bearer " + new JwtSecurityTokenHandler().WriteToken(token));

            profileController = new ProfileController(config, fakeResponseHandler);
            profileController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext
                {
                    User = testClaims
                }
            };
            profileController.ControllerContext.HttpContext.Request.Headers.Add("Authorization", "Bearer " + new JwtSecurityTokenHandler().WriteToken(token));
        }

        #region Auth Tests
        [TestMethod]
        public void AccountLoginSuccess()
        {
            var response = (ViewResult)accountController.Login();
            var model = (PartialVM)response.Model;
            Assert.AreNotEqual("Auth Login Success", model.PartialView);
        }

        [TestMethod]
        public void AccountLoginReturnSuccess()
        {
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
        public void AccountRegisterSuccess()
        {
            var response = (ViewResult)accountController.Register();
            var model = (PartialVM)response.Model;
            Assert.AreNotEqual("Auth Register Success", model.PartialView);
        }

        [TestMethod]
        public void AccountRegisterUserSuccess()
        {
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
        public void AccountLogoutSuccess()
        {
            var response = (RedirectToActionResult)accountController.Logout();
            Assert.AreEqual("Home", response.ControllerName);
        }
        #endregion

        #region Cart Tests

        [TestMethod]
        public void CartIndexSuccess()
        {
            var response = (ViewResult)cartcontroller.Index();
            var model = (PartialVM)response.Model;
            Assert.AreNotEqual("Cart Index Success", model.PartialView);
        }

        #endregion

        #region Product Tests

        [TestMethod]
        public void ProductIndexSuccess()
        {
            var response = (ViewResult)homeController.Index();
            var model = (PartialVM)response.Model;
            Assert.AreNotEqual("Product Index Success", model.PartialView);
        }

        [TestMethod]
        public void ProductDetailsSuccess()
        {
            var response = (ViewResult)homeController.ProductDetails("1");
            var model = (PartialVM)response.Model;
            Assert.AreNotEqual("Product Details Success", model.PartialView);
        }

        #endregion

        #region Message Tests

        [TestMethod]
        public void MessageMyMessageSuccess()
        {
            var response = (ViewResult)messageController.MyMessages("1");
            var model = (PartialVM)response.Model;
            Assert.AreNotEqual("Message MyMessages Success", model.PartialView);
        }

        [TestMethod]
        public void MessageSendSuccess()
        {
            var response = (ViewResult)messageController.Send("1");
            var model = (PartialVM)response.Model;
            Assert.AreNotEqual("Message Send Success", model.PartialView);
        }

        [TestMethod]
        public void MessageSaveMessageSuccess()
        {
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
        public void MessageDetailsSuccess()
        {
            var response = (ViewResult)messageController.Details(1);
            var model = (PartialVM)response.Model;
            Assert.AreNotEqual("Message Details Success", model.PartialView);
        }
        #endregion

        #region Profile Tests

        [TestMethod]
        public void ProfileIndexSuccess()
        {
            var response = (ViewResult)profileController.Index();
            var model = (PartialVM)response.Model;
            Assert.AreNotEqual("Profile Index Success", model.PartialView);
        }

        [TestMethod]
        public void ProfileUserProfileSuccess()
        {
            var response = (ViewResult)profileController.Profile("1");
            var model = (PartialVM)response.Model;
            Assert.AreNotEqual("Profile User Profile Success", model.PartialView);
        }

        [TestMethod]
        public void ProfileEditSuccess()
        {
            var response = (ViewResult)profileController.Edit("1");
            var model = (PartialVM)response.Model;
            Assert.AreNotEqual("Profile Edit Success", model.PartialView);
        }

        [TestMethod]
        public void ProfileEditPostSuccess()
        {
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
            Assert.AreEqual("Profile", response.ControllerName);
        }
        #endregion
    }
}
