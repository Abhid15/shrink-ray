using shrink_ray.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Service.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace shrink_ray.Controllers.Tests
{
    [TestClass()]
    public class ShrinkRayUrlControllerTests
    {
        Mock<IShrinkRayUrlService> shrinkRayUrlServiceMock;
        Mock<ILogger<ShrinkRayUrlController>> loggerServiceMock;

        [TestInitialize]
        public void Setup()
        {
            shrinkRayUrlServiceMock = new Mock<IShrinkRayUrlService>();
            loggerServiceMock = new Mock<ILogger<ShrinkRayUrlController>>();
        }
        [TestCleanup]
        public void TearDown()
        {
            // Runs after each test. (Optional)
        }

        [TestMethod()]
        public void Get_StateUnderTest_ExpectedBehavior()
        {
            //Arrange
            var resultMock = new List<ShrinkRayUrlModel>();
            var shrinkRayUrlModel = new ShrinkRayUrlModel()
            {
                CreateDate = DateTime.Now,
                Id = new MongoDB.Bson.ObjectId(),
                LongURL = "https://facebook.com",
                ShortURL = "RT"

            };
            resultMock.Add(shrinkRayUrlModel);
            shrinkRayUrlServiceMock.Setup(x => x.GetCollectionFromDataStore()).Returns(resultMock);
            ShrinkRayUrlController shrinkRayUrlController = new ShrinkRayUrlController(shrinkRayUrlServiceMock.Object, loggerServiceMock.Object);

            //Act
            var output = shrinkRayUrlController.Get();
            var okResult = output as OkObjectResult;

            //Assert
            Assert.IsNotNull(output);
            Assert.IsNotNull(okResult.Value);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod()]
        public void GetSpecific__StateUnderTest_ExpectedBehavior()
        {
            //Arrange 
            var shrinkRayUrlModel = new ShrinkRayUrlModel()
            {
                CreateDate = DateTime.Now,
                Id = new MongoDB.Bson.ObjectId(),
                LongURL = "https://facebook.com",
                ShortURL = "RT"

            };
            shrinkRayUrlServiceMock.Setup(x => x.GetItemFromDataStore("RT")).Returns(shrinkRayUrlModel);
            ShrinkRayUrlController shrinkRayUrlController = new ShrinkRayUrlController(shrinkRayUrlServiceMock.Object, loggerServiceMock.Object);

            //Act 
            var output = shrinkRayUrlController.Get("RT", false);
            var okResult = output as OkObjectResult;

            //Assert
            Assert.IsNotNull(output);
            Assert.IsNotNull(okResult.Value);
            Assert.AreEqual(200, okResult.StatusCode);

        }
        [TestMethod()]
        public void GetSpecific__StateUnderTest_ExpectedBehavior_WhenRedirectTrue()
        {
            //Arrange 
            var shrinkRayUrlModel = new ShrinkRayUrlModel()
            {
                CreateDate = DateTime.Now,
                Id = new MongoDB.Bson.ObjectId(),
                LongURL = "https://facebook.com",
                ShortURL = "RT"

            };
            shrinkRayUrlServiceMock.Setup(x => x.GetItemFromDataStore("RT")).Returns(shrinkRayUrlModel);
            ShrinkRayUrlController shrinkRayUrlController = new ShrinkRayUrlController(shrinkRayUrlServiceMock.Object, loggerServiceMock.Object);

            //Act 
            var output = shrinkRayUrlController.Get("RT", true);
            var redirectResult = output as RedirectResult;

            //Assert
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual(redirectResult.Url, "https://facebook.com");

        }

        [TestMethod()]
        public void Post__StateUnderTest_ExpectedBehavior()
        {
            //Arrange 
            var shrinkRayUrlModel = new ShrinkRayUrlModel()
            {
                CreateDate = DateTime.Now,
                Id = new MongoDB.Bson.ObjectId(),
                LongURL = "https://facebook.com",
                ShortURL = "RT"

            };
            var resultMock = new ShrinkRayUrlResponseModel()
            {
                Message = "URL added successfully.",
                Model = shrinkRayUrlModel,
                Success = true

            };
            var request = new ShrinkRayUrlRequestModel()
            {
                LongURL = "https://facebook.com"
            };
            //Setting up controller
            shrinkRayUrlServiceMock.Setup(x => x.SaveItemToDataStore(request)).Returns(resultMock);
            var mockHttpRequest = new Mock<HttpRequest>();
            mockHttpRequest.Setup(x => x.Scheme).Returns("http");
            mockHttpRequest.Setup(x => x.Host).Returns(HostString.FromUriComponent("localhost:5000"));
            mockHttpRequest.Setup(x => x.PathBase).Returns(PathString.FromUriComponent(""));

            var httpContext = Mock.Of<HttpContext>(_ =>
                _.Request == mockHttpRequest.Object
            );

            //Controller needs a controller context 
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };
            //assign context to controller
            var shrinkRayUrlController = new ShrinkRayUrlController(shrinkRayUrlServiceMock.Object, loggerServiceMock.Object)
            {
                ControllerContext = controllerContext,
            };


            //Act 
            var output = shrinkRayUrlController.Post(request);
            var okResult = output as OkObjectResult;

            //Assert
            Assert.IsNotNull(output);
            Assert.IsNotNull(okResult.Value);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod()]
        public void Post__StateUnderTest_ExpectedBehavior_BadRquest()
        {
            //Arrange 
            var shrinkRayUrlModel = new ShrinkRayUrlModel()
            {
                CreateDate = DateTime.Now,
                Id = new MongoDB.Bson.ObjectId(),
                LongURL = "https://facebook.com",
                ShortURL = "RT"

            };
            var resultMock = new ShrinkRayUrlResponseModel()
            {
                Message = "URL added successfully.",
                Model = shrinkRayUrlModel,
                Success = true

            };
            var request = new ShrinkRayUrlRequestModel()
            {
                LongURL = "https://facebook.com"
            };
            shrinkRayUrlServiceMock.Setup(x => x.SaveItemToDataStore(request));
            ShrinkRayUrlController shrinkRayUrlController = new ShrinkRayUrlController(shrinkRayUrlServiceMock.Object, loggerServiceMock.Object);

            //Act 
            var output = shrinkRayUrlController.Post(request);
            var okResult = output as ObjectResult;

            //Assert
            Assert.IsNotNull(output);
            Assert.IsNotNull(okResult.Value);
            Assert.AreEqual(400, okResult.StatusCode);
        }
    }
}