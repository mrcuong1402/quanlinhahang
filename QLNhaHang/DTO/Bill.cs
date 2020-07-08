using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNhaHang.DTO
{
    public class Bill
    {
        public Bill(int id, DateTime? dateCheckOut, DateTime? dateCheckIn, int status,int idCustomer,int discount = 0)
        {
            this.ID = id;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.Status = status;
            this.DisCount = discount;
           // this.IdCustomer = idCustomer;
        }

        public Bill(DataRow row)
        {
            this.ID = (int)row["id"];
            this.DateCheckIn = (DateTime?)row["dateCheckIn"];
            var dateCheckOutTemp = row["dateCheckOut"];
            if( dateCheckOutTemp.ToString() !="")
                this.DateCheckOut = (DateTime?)dateCheckOutTemp;
            this.Status = (int)row["status"]; ;
            if (row["discount"].ToString() != "")
                this.DisCount = (int)row["discount"];
            //this.IdCustomer = (int)row["idCustomer"];
        }

        private int idCustomer;

        public int IdCustomer
        {
            get { return idCustomer; }
            set { idCustomer = value; }
        }

        private int disCount;

        public int DisCount
        {
            get { return disCount; }
            set { disCount = value; }
        }
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        private DateTime? dateCheckIn;

        public DateTime? DateCheckIn
        {
            get { return dateCheckIn; }
            set { dateCheckIn = value; }
        }
        private DateTime? dateCheckOut;

        public DateTime? DateCheckOut
        {
            get { return dateCheckOut; }
            set { dateCheckOut = value; }
        }
        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
