using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {

        private IPhotoRepository repository;

        public NavController(IPhotoRepository repo)
        {
            repository = repo;
        }
        public PartialViewResult Menu(string colortype = null)
        {
            ViewBag.SelectedColorType = colortype;
            IEnumerable<string> colortypes = repository.Photos
                .Select(photo => photo.ColorType)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(colortypes);
        }
    }
}