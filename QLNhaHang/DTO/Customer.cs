using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNhaHang.DTO
{
    public class Customer
    {
        public Customer(int id, string name,string phone,string adress)
        {
            this.Id = id;
            this.Name = name;
            this.Phone = phone;
            this.Adress = adress;
           // this.IdBill = IdBill;
        }

        public Customer(DataRow row)
        {
            this.Id = (int)row["id"];
            this.Name = row["name"].ToString();
            this.Phone = row["phone"].ToString();
            this.Adress = row["adress"].ToString();
           // this.IdBill = (int)row["IdBill"];
        }

        private string adress;

        public string Adress
        {
            get { return adress; }
            set { adress = value; }
        }
        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
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
