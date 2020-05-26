using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebUI.Controllers;
using WebUI.HtmlHelpers;
using WebUI.Models;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate() //разбиение на страницы
        {

            //Организация
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
            //действие
            PhotosListViewModel result = (PhotosListViewModel)controller.List(null,2).Model;
            
            //Утверждение
            List<Photo> photos = result.Photos.ToList();
            Assert.IsTrue(photos.Count == 2);
            Assert.AreEqual(photos[0].Name, "PService4");
            Assert.AreEqual(photos[1].Name, "PService5");

        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //организация 
            HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage =2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            //Действие
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            //Утверждение
            Assert.AreEqual(
                @"<a class=""btn btn-default"" href=""Page1"">1</a>"
              + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
              + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        //Нужно удостовериться что контроллер отправляет представлению.
        //правильную информацию о разбиении страницы

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //Организация
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
            //действие
            PhotosListViewModel result = (PhotosListViewModel)controller.List(null,2).Model;

            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);


        }

        [TestMethod]
        public void Can_Filter_PhotoServices()
        {
            //Организация
            Mock<IPhotoRepository> mock = new Mock<IPhotoRepository>();
            mock.Setup(m => m.Photos).Returns(new List<Photo>
            {
                new Photo {PhotoId = 1, Name = "PService1", ColorType = "Colorfull"},
                new Photo {PhotoId = 2, Name = "PService2", ColorType = "Colorfull"},
                new Photo {PhotoId = 3, Name = "PService3", ColorType = "Monochrome"},
                new Photo {PhotoId = 4, Name = "PService4", ColorType = "AllColors"},
                new Photo {PhotoId = 5, Name = "PService5", ColorType = "Monochrome"}

            });

            PhotosController controller = new PhotosController(mock.Object);
            controller.pageSize = 3;
            //действие запрашивается только монохромная категория услуг
            List<Photo> result = ((PhotosListViewModel)controller.List("Monochrome", 1).Model).Photos.ToList();


            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "PService3" && result[0].ColorType == "Monochrome");
            Assert.IsTrue(result[1].Name == "PService5" && result[1].ColorType == "Monochrome");
        }


        [TestMethod]
        public void Can_CriateCategories()
        {
            //Организация
            Mock<IPhotoRepository> mock = new Mock<IPhotoRepository>();
            mock.Setup(m => m.Photos).Returns(new List<Photo>
            {
                //имитированая реалезация хранилища
                new Photo {PhotoId = 1, Name = "PService1", ColorType = "Monochrome"},
                new Photo {PhotoId = 2, Name = "PService2", ColorType = "Colorful"},
                new Photo {PhotoId = 3, Name = "PService3", ColorType = "Monochrome"},
                new Photo {PhotoId = 4, Name = "PService4", ColorType = "All"},
                new Photo {PhotoId = 5, Name = "PService5", ColorType = "Colorful"}

            });

            NavController target = new NavController(mock.Object);

            //действие запрашивается только монохромная категория услуг
            List<string> result = ((IEnumerable<string>)target.Menu().Model).ToList();


            Assert.AreEqual(result.Count(), 3);
            Assert.AreEqual(result[0], "All");
            Assert.AreEqual(result[1], "Colorful");
            Assert.AreEqual(result[2], "Monochrome");
        }


        [TestMethod]
        public void Indicates_Selected_ColorType()
        {
            //Организация
            Mock<IPhotoRepository> mock = new Mock<IPhotoRepository>();
            mock.Setup(m => m.Photos).Returns(new List<Photo>
            {
                //имитированая реалезация хранилища
                new Photo {PhotoId = 1, Name = "PService1", ColorType = "Monochrome"},
                new Photo {PhotoId = 2, Name = "PService2", ColorType = "Colorful"},
                new Photo {PhotoId = 3, Name = "PService3", ColorType = "Monochrome"},
                new Photo {PhotoId = 4, Name = "PService4", ColorType = "All"},
                new Photo {PhotoId = 5, Name = "PService5", ColorType = "Colorful"}

            });

            NavController target = new NavController(mock.Object);

            string colortypes = "Colorful";

            //действие запрашивается только монохромная категория услуг
            string result = target.Menu(colortypes).ViewBag.SelectedColorType;

            Assert.AreEqual(colortypes, result);

        }


        [TestMethod]
        public void Generate_ColorType_Specific_Book_Count()
        {
            //Организация
            Mock<IPhotoRepository> mock = new Mock<IPhotoRepository>();
            mock.Setup(m => m.Photos).Returns(new List<Photo>
            {
                //имитированая реалезация хранилища
                new Photo {PhotoId = 1, Name = "PService1", ColorType = "Monochrome"},
                new Photo {PhotoId = 2, Name = "PService2", ColorType = "Colorful"},
                new Photo {PhotoId = 3, Name = "PService3", ColorType = "Monochrome"},
                new Photo {PhotoId = 4, Name = "PService4", ColorType = "All"},
                new Photo {PhotoId = 5, Name = "PService5", ColorType = "Colorful"}

            });

            PhotosController controller = new PhotosController(mock.Object);
            controller.pageSize = 3;

            int result1 = ((PhotosListViewModel)controller.List("All").Model).PagingInfo.TotalItems;
            int result2 = ((PhotosListViewModel)controller.List("Colorful").Model).PagingInfo.TotalItems;
            int result3 = ((PhotosListViewModel)controller.List("Monochrome").Model).PagingInfo.TotalItems;
            int resultAll = ((PhotosListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            Assert.AreEqual(result1, 1);
            Assert.AreEqual(result2, 2);
            Assert.AreEqual(result3, 2);
            Assert.AreEqual(resultAll, 5);

        }

    }
}
