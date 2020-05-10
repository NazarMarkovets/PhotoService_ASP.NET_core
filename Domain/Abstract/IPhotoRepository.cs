using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IPhotoRepository
    {
        //получить последовательности объектов Photo не сообщая
        //о том где или как хранятся данные
        IEnumerable<Photo> Photos { get; }

        
    }
}
