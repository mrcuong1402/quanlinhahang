using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNhaHang.DTO
{
    public class BillInfor
    {
        private int idBill;

        public int IdBill
        {
            get { return idBill; }
            set { idBill = value; }
        }
        private int idFood;

        public int IdFood
        {
            get { return idFood; }
            set { idFood = value; }
        }
        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public BillInfor(int id, int idbill, int idfood, int count)
        {
            this.Id = id;
            this.IdBill = idbill;
            this.IdFood = idfood;
            this.Count = count;
        }
        public BillInfor(DataRow row)
        {
            this.Id = (int)row["id"];
            this.IdBill = (int)row["idbill"];
            this.IdFood = (int)row["idfood"];
            this.Count = (int)row["count"];
        }

       

    }
}
