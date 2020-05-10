using Domain.Abstract;
using Domain.Entities;
using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        private void AddBindings()
        {
            Mock<IPhotoRepository> mock = new Mock<IPhotoRepository>();
            mock.Setup(m => m.Photos).Returns(new List<Photo>
            {
                new Photo {Name = "Удалить красные глаза", PhotoFormat = "jpg, png", Price = 111},
                new Photo {Name = "Обычная печать", PhotoFormat = "jpg, png, bmp", Price = 50},
                new Photo {Name = "Ламинирование", PhotoFormat = "jpg, png, bmp", Price = 155},
            });
            kernel.Bind<IPhotoRepository>().ToConstant(mock.Object);
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}