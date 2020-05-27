using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShopCart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Photo photo, int quantity)
        {
            
        }
    }

    public class CartLine //One item in shop card
    {
        public Photo Photo { get; set; }
        public int Quantity { get; set; }
        //public string UserPhoto {get;set}
    }
}
