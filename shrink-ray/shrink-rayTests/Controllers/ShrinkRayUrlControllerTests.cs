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
        public void GetTest()
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
        public void GetSpecificTest()
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
            var output = shrinkRayUrlController.GetSpecific("RT", false);
            var okResult = output as OkObjectResult;

            //Assert
            Assert.IsNotNull(output);
            Assert.IsNotNull(okResult.Value);
            Assert.AreEqual(200, okResult.StatusCode);

        }
        [TestMethod()]
        public void GetSpecificTestWhenRedirectTrue()
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
            var output = shrinkRayUrlController.GetSpecific("RT", true);
            var redirectResult = output as RedirectResult;

            //Assert
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual(redirectResult.Url, "https://facebook.com");

        }

        [TestMethod()]
        public void PostTest()
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
            shrinkRayUrlServiceMock.Setup(x => x.SaveItemToDataStore(request)).Returns(resultMock);
            ShrinkRayUrlController shrinkRayUrlController = new ShrinkRayUrlController(shrinkRayUrlServiceMock.Object, loggerServiceMock.Object);

            //Act 
            var output = shrinkRayUrlController.Post(request);
            var okResult = output as OkObjectResult;

            //Assert
            Assert.IsNotNull(output);
            Assert.IsNotNull(okResult.Value);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod()]
        public void PostTestBadRquest()
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