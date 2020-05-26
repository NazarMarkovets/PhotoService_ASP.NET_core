using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public string Name { get; set; }
        public string PhotoFormat { get; set; }
        public string Description { get; set; }
        public string  ColorType{ get; set; } //Genre
        public decimal Price { get; set; }
    }
    
}
