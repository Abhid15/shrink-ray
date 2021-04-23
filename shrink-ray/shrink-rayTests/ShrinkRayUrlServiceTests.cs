using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Repository.DAL.Interfaces;
using Service;
using System;
using System.Collections.Generic;

namespace shrink_rayTests
{
    [TestClass]
    public class ShrinkRayUrlServiceTests
    {
        private MockRepository mockRepository;

        private Mock<IRepository> mockIRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockIRepository = this.mockRepository.Create<IRepository>();
        }

        private ShrinkRayUrlService CreateService()
        {
            return new ShrinkRayUrlService(
                this.mockIRepository.Object);
        }

        [TestMethod]
        public void GetCollectionFromDataStore_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var resultMock = new List<ShrinkRayUrlModel>();
            var shrinkRayUrlModel = new ShrinkRayUrlModel()
            {
                CreateDate = DateTime.Now,
                Id = new MongoDB.Bson.ObjectId(),
                LongURL = "https://facebook.com",
                ShortURL = "RT"

            };
            resultMock.Add(shrinkRayUrlModel);
            var service = this.CreateService();
            mockIRepository.Setup(x => x.GetCollectionFromDataStore()).Returns(resultMock);

            // Act
            var result = service.GetCollectionFromDataStore() as List<ShrinkRayUrlModel>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 1);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void GetItemFromDataStore_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            string shortUrl = "RT";
            var shrinkRayUrlModel = new ShrinkRayUrlModel()
            {
                CreateDate = DateTime.Now,
                Id = new MongoDB.Bson.ObjectId(),
                LongURL = "https://facebook.com",
                ShortURL = "RT"

            };
            mockIRepository.Setup(x => x.GetItemFromDataStoreByShortUrl(shortUrl)).Returns(shrinkRayUrlModel);

            // Act
            var result = service.GetItemFromDataStore(
                shortUrl);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ShortURL, "RT");
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void SaveItemToDataStore_StateUnderTest_ExpectedBehavior_UrlAlreadyPresent()
        {
            // Arrange
            var service = this.CreateService();
            ShrinkRayUrlRequestModel model = new ShrinkRayUrlRequestModel() 
            {
                LongURL = "http://google.com"
            };
            var shrinkRayUrlModel = new ShrinkRayUrlModel()
            {
                CreateDate = DateTime.Now,
                Id = new MongoDB.Bson.ObjectId(),
                LongURL = "http://google.com",
                ShortURL = "RT"

            };
            mockIRepository.Setup(x => x.GetItemFromDataStoreByLongUrl(model.LongURL)).Returns(shrinkRayUrlModel);
            // Act
            var result = service.SaveItemToDataStore(
                model);

            // Assert
            this.mockRepository.VerifyAll();
        }

        public void SaveItemToDataStore_StateUnderTest_ExpectedBehavior_NewUrl()
        {
            // Arrange
            var service = this.CreateService();
            ShrinkRayUrlRequestModel model = new ShrinkRayUrlRequestModel()
            {
                LongURL = "http://google.com"
            };
            var shrinkRayUrlModel = new ShrinkRayUrlModel()
            {
                CreateDate = DateTime.Now,
                Id = new MongoDB.Bson.ObjectId(),
                LongURL = "http://google.com",
                ShortURL = "RT"

            };
            mockIRepository.Setup(x => x.GetItemFromDataStoreByLongUrl(model.LongURL));
            mockIRepository.Setup(x => x.SaveItemToDataStore(shrinkRayUrlModel)).Returns(shrinkRayUrlModel);
            // Act
            var result = service.SaveItemToDataStore(
                model);

            // Assert
            this.mockRepository.VerifyAll();
        }
    }
}
