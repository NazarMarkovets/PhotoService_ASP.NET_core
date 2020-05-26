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
    public class PhotosController : Controller
    {
        //добавление конструктора который объявляет зависимость 
        //от интерфейса IPhotoRepository

        private IPhotoRepository repository;
        public int pageSize = 4;

        public PhotosController(IPhotoRepository repo)
        {
            repository = repo;
        }

        //Добавление метода действия визуализации представления
        public ViewResult List( string genre, int page = 1)
        {
            PhotosListViewModel model = new PhotosListViewModel
            {
                Photos = repository.Photos
                .Where(b =>genre == null || b.ColorType == genre) //for filtering by ColorType
                .OrderBy(photo => photo.PhotoId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.Photos.Count()
                },
                CurrColorType = genre
            };
            //полный список услуг
            //предоставляем в функцию данные, которыми необходимо заполнить
            //Model в строго типизированом представлении
            return View(model);
        }
        
    }
}