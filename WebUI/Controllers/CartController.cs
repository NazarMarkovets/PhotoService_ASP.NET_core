using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private IPhotoRepository repository;

        public CartController(IPhotoRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetShopCart(),
                ReturnUrl = returnUrl
            });
        }
        
        public ShopCart GetShopCart()
        {
            ShopCart cart = (ShopCart)Session["ShopCart"];
            if(cart == null)
            {
                cart = new ShopCart();
                Session["ShopCart"] = cart;
            }
            return cart;

        }

        public RedirectToRouteResult AddToCart(int photoId, string returnUrl)
        {
            Photo photo = repository.Photos
                .FirstOrDefault(p => p.PhotoId == photoId);

            if (photo != null)
            {
                //GetCart() - средство состояния сеанса
                GetShopCart().AddItem(photo,1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        //RedirectToRouteResult - отправляет инструкцию перенаправления
        public RedirectToRouteResult RemoveFromCart(int photoId, string returnUrl)
        {
            Photo photo = repository.Photos
                .FirstOrDefault(p => p.PhotoId == photoId);

            if (photo != null)
            {
                
                GetShopCart().RemoveLine(photo);
            }

            return RedirectToAction("Index", new { returnUrl });
        }
    }
}