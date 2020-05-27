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

        public ViewResult Index(ShopCart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                //снабжение контроллера объектами cart
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        
        public RedirectToRouteResult AddToCart(ShopCart cart,int photoId, string returnUrl)
        {
            Photo photo = repository.Photos
                .FirstOrDefault(p => p.PhotoId == photoId);

            if (photo != null)
            {
                //GetCart() - средство состояния сеанса
                cart.AddItem(photo,1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        //RedirectToRouteResult - отправляет инструкцию перенаправления
        public RedirectToRouteResult RemoveFromCart(ShopCart cart, int photoId, string returnUrl)
        {
            Photo photo = repository.Photos
                .FirstOrDefault(p => p.PhotoId == photoId);

            if (photo != null)
            {
                
                cart.RemoveLine(photo);
            }

            return RedirectToAction("Index", new { returnUrl });
        }


        public PartialViewResult Summary(ShopCart cart)
        {
            return PartialView(cart);
        }
    }
}