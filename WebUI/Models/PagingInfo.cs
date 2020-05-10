using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; } //общее количество фото сервисов
        public int ItemsPerPage { get; set; } //кол-во сервисов на странице
        public int CurrentPage { get; set; } //номер текущей страницы
        public int TotalPages               //общее количество страниц
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }


        }
    }
}