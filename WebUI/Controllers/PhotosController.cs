using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class PhotosController : Controller
    {
        //добавление конструктора который объявляет зависимость 
        //от интерфейса IPhotoRepository

        private IPhotoRepository repository;
        public PhotosController(IPhotoRepository repo)
        {
            repository = repo;
        }

        //Добавление метода действия визуализации представления
        public ViewResult List()
        {
            //полный список услуг
            //предоставляем в функцию данные, которыми необходимо заполнить
            //Model в строго типизированом представлении
            return View(repository.Photos);
        }
        
    }
}