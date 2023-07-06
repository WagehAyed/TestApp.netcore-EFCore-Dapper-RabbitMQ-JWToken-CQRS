using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Item
    {
        public string Code;
        public double Price;
        public double Quantity;
        public Item(string code,double price,double quantity)
        {
            Code = code;
            Price = price;
            Quantity = quantity; 
        }
    }
    public static class Stock
    {
        public static List<Item> items = new List<Item>{
            new Item(code:"123",price:10,quantity:3),
            new Item(code:"444",price:50,quantity:9),
            new Item(code:"555",price:20,quantity:8) 
            };
    }

    public class salesFactory{ 
    }
}
