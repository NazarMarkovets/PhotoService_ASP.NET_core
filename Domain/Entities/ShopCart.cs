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
        //Свойство позвол. обращаться к содержимому
        public IEnumerable<CartLine> Lines { get { return lineCollection; } }
        //add to basket
        public void AddItem(Photo photo, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Photo.PhotoId == photo.PhotoId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine { Photo = photo, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        //Delete from basket
        public void RemoveLine(Photo photo)
        {
            lineCollection.RemoveAll(l => l.Photo.PhotoId == photo.PhotoId);
        }

        //Count summ for payment
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Photo.Price * e.Quantity);
        }

        //Delete all items from basket
        public void ClearShopCard()
        {
            lineCollection.Clear();
        }

    }

    public class CartLine //One item in shop card
    {
        public Photo Photo { get; set; }
        public int Quantity { get; set; }
        //public string UserPhoto {get;set}
    }
}
