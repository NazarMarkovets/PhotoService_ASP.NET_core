using System;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

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


    }


}
