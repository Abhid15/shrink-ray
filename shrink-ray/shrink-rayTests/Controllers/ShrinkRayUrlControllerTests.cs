using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Service.Interfaces;
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

        [TestMethod()]
        public void GetTest()
        {
            //Arrange
            var result = new List<ShrinkRayUrlModel>();
            shrinkRayUrlServiceMock.Setup(x => x.GetCollectionFromDataStore()).Returns(result);
            ShrinkRayUrlController shrinkRayUrlController = new ShrinkRayUrlController(shrinkRayUrlServiceMock.Object, loggerServiceMock.Object);

            //Act
            shrinkRayUrlController.Get();

            //Assert


        }
        [TestCleanup]
        public void TearDown()
        {
            // Runs after each test. (Optional)
        }
    }
}