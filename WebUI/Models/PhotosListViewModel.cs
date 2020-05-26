using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class PhotosListViewModel
    {
        public IEnumerable<Photo> Photos { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public string CurrColorType { get; set; }
    }
}