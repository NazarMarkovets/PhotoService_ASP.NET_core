using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstract;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebUI.Controllers;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate() //разбиение на страницы
        {
            Mock<IPhotoRepository> mock = new Mock<IPhotoRepository>();
            mock.Setup(m => m.Photos).Returns(new List<Photo>
            {
                new Photo {PhotoId = 1, Name = "PService1"},
                new Photo {PhotoId = 2, Name = "PService2"},
                new Photo {PhotoId = 3, Name = "PService3"},
                new Photo {PhotoId = 4, Name = "PService4"},
                new Photo {PhotoId = 5, Name = "PService5"}

            });

            PhotosController controller = new PhotosController(mock.Object);
            controller.pageSize = 3;

            IEnumerable<Photo> result = (IEnumerable<Photo>)controller.List(2).Model;

            List<Photo> photos = result.ToList();
            Assert.IsTrue(photos.Count == 2);
            Assert.AreEqual(photos[0].Name, "PService4");
            Assert.AreEqual(photos[1].Name, "PService5");

        }
    }
}
