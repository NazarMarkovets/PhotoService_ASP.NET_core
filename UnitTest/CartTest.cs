using System;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstract;
using Moq;
using WebUI.Controllers;
using System.Web.Mvc;
using WebUI.Models;

namespace UnitTest
{
    [TestClass]
    public class CartTest
    {
        [TestMethod]
        public void Can_Add_New_Items()
        {
            //Организация
            Photo photo1 = new Photo { PhotoId = 1, Name = "PhotoService1" };
            Photo photo2 = new Photo { PhotoId = 2, Name = "PhotoService2" };

            ShopCart cart = new ShopCart();

            //Действие
            cart.AddItem(photo1, 1);
            cart.AddItem(photo2, 1);
            List<CartLine> result = cart.Lines.ToList();

            //Подтверждение
            Assert.AreEqual(result.Count(), 2);
            Assert.AreEqual(result[0].Photo, photo1);
            Assert.AreEqual(result[1].Photo, photo2);
        }


        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Items() //Если уже присутствует товар
        {
            //Организация
            Photo photo1 = new Photo { PhotoId = 1, Name = "PhotoService1" };
            Photo photo2 = new Photo { PhotoId = 2, Name = "PhotoService2" };

            ShopCart cart = new ShopCart();

            //Действие
            cart.AddItem(photo1, 1);
            cart.AddItem(photo2, 1);
            cart.AddItem(photo1, 5);
            List<CartLine> result = cart.Lines.OrderBy( c =>c.Photo.PhotoId).ToList();

            //Подтверждение
            Assert.AreEqual(result.Count(), 2);
            Assert.AreEqual(result[0].Quantity, 6);
            Assert.AreEqual(result[1].Quantity, 1);
        }

        [TestMethod]
        //Возможность удалить товар пользователем
        public void Can_Item_From_Basket() 
        {
            //Организация
            Photo photo1 = new Photo { PhotoId = 1, Name = "PhotoService1" };
            Photo photo2 = new Photo { PhotoId = 2, Name = "PhotoService2" };
            Photo photo3 = new Photo { PhotoId = 3, Name = "PhotoService3" };

            ShopCart cart = new ShopCart();

            //Действие
            cart.AddItem(photo1, 1);
            cart.AddItem(photo2, 1);
            cart.AddItem(photo1, 5);
            cart.AddItem(photo3, 2);
            cart.RemoveLine(photo2);

            //Подтверждение
            Assert.AreEqual(cart.Lines
                .Where(c => c.Photo == photo2)
                .Count(), 0);

            Assert.AreEqual(cart.Lines.Count(), 2);
        }

        [TestMethod]
        //Вычисление общей стоимости елементов в корзине
        public void Can_Calculate_Cart_Total()
        {
            //Организация
            Photo photo1 = new Photo { PhotoId = 1, Name = "PhotoService1", Price = 100 };
            Photo photo2 = new Photo { PhotoId = 2, Name = "PhotoService2", Price = 55 };
            

            ShopCart cart = new ShopCart();

            //Действие
            cart.AddItem(photo1, 1);
            cart.AddItem(photo2, 1); //summary Price 655
            cart.AddItem(photo1, 5);
            
            decimal result = cart.ComputeTotalValue();

            //Подтверждение
            Assert.AreEqual(result, 655);
           
        }


        [TestMethod]
        //Корректное удаление елементов
        public void Can_Clear_Contents()
        {
            //Организация
            Photo photo1 = new Photo { PhotoId = 1, Name = "PhotoService1", Price = 100 };
            Photo photo2 = new Photo { PhotoId = 2, Name = "PhotoService2", Price = 55 };


            ShopCart cart = new ShopCart();

            //Действие
            cart.AddItem(photo1, 1);
            cart.AddItem(photo2, 1); //summary Price 655
            cart.AddItem(photo1, 5);

            cart.ClearShopCard();

            //Подтверждение
            Assert.AreEqual(cart.Lines.Count(), 0);

        }


        [TestMethod]
        //Добавление объектов с помощью Binder class
        public void Can_Add_to_Cart()
        {
            //Организация
            //Добавление выбраного товара в корзину
            Mock<IPhotoRepository> mock = new Mock<IPhotoRepository>();
            mock.Setup(m => m.Photos).Returns(new List<Photo>{
                new Photo {PhotoId = 1,Name = "PhotoService1", ColorType = "Monochrome"}
            }.AsQueryable());


            //Действие
            ShopCart cart = new ShopCart();
            CartController cartController = new CartController(mock.Object);
            cartController.AddToCart(cart, 1, null);

            //Подтверждение
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToList()[0].Photo.PhotoId, 1);

        }

        [TestMethod]
        //перенаправление на Index.cshtml после добавления книги
        public void Can_Add_Photoserv_Tocard_AndShow()
        {
            //Организация
            //Добавление выбраного товара в корзину
            Mock<IPhotoRepository> mock = new Mock<IPhotoRepository>();
            mock.Setup(m => m.Photos).Returns(new List<Photo>{
                new Photo {PhotoId = 1,Name = "PhotoService1", ColorType = "Monochrome"}
            }.AsQueryable());


            //Действие
            ShopCart cart = new ShopCart();
            CartController cartController = new CartController(mock.Object);
            RedirectToRouteResult result = cartController.AddToCart(cart, 2, "TestUrl");

            //Подтверждение
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "TestUrl");

        }


        [TestMethod]
        //перенаправление на Index.cshtml Корректная передача URL
        public void Can_Add_URLcorrect_To_View_Index()
        {
            //Организация
            ShopCart cart = new ShopCart();
            CartController target = new CartController(null);


            //Действие
            CartIndexViewModel result = (CartIndexViewModel)target.Index(cart, "testURL").ViewData.Model;

            //Подтверждение
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "testURL");

        }


    }


}
