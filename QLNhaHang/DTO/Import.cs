using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNhaHang.DTO
{
     public class Import
    {
         public Import(int id, string name, float price, string unit, int count, float totalPrice, DateTime? dayImport)
         {
             this.Id = id;
             this.Name = name;
             this.Price = price;
             this.Unit = unit;
             this.Count = count;
             this.TotalPrice = totalPrice;
             this.DayImport1 = dayImport;
         }

         public Import(DataRow row)
         {
             this.Id = (int)row["id"];
             this.Name = row["name"].ToString();
             this.Price = (float)Convert.ToDouble(row["price"]);
             this.Unit = row["unit"].ToString();
             this.Count = (int)row["count"];
             this.TotalPrice = (float)Convert.ToDouble(row["totalPrice"]);
             this.DayImport1 = (DateTime?)row["DayImport"];
         }

         private DateTime? DayImport;

         public DateTime? DayImport1
         {
             get { return DayImport; }
             set { DayImport = value; }
         }
        private float totalPrice;

        public float TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }
        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        private string unit;

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        private float price;

        public float Price
        {
            get { return price; }
            set { price = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
