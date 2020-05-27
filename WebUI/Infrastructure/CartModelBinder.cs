using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Infrastructure
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "ShopCart";
        //private int sessionkey;

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ShopCart cart = null;
         
            //получение данных сеанса
            if (controllerContext.HttpContext.Session != null)
            {
                cart = (ShopCart)controllerContext.HttpContext.Session[sessionKey];
            }
            if(cart == null)
            {
                cart = new ShopCart();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }
            return cart; 
        }
    }
}