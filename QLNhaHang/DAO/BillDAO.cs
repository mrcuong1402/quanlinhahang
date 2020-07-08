using QLNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNhaHang.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set { BillDAO.instance = value; }
        }
        private BillDAO() { }
           
        public int GetUnCheckBillIdByTableId(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT *FROM dbo.Bill WHERE  idTable ="+id+" AND status = 0");
            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }
            return -1;
        }

        /// <summary>
        /// hàm thêm hóa đơn mới
        /// </summary>
        /// <param name="id"></param>
        public void InsertBill(int id)
        {
            DataProvider.Instance.ExecuteQuery("EXEC dbo.spInsertBill @idTable ", new object[] { id });
        }

        public int GetMaxIdBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("SELECT MAX(id) FROM dbo.Bill");
            }
            catch
            {
                return 1;
            }
        }

        //public void Checkout(int id,int discount,float totalPrice)
        //{
        //    string query = "UPDATE dbo.Bill SET dateCheckOut = GETDATE(), status = 1, " + "discount = " + discount + ", totalPrice = " + totalPrice + " WHERE id = " + id;
        //    DataProvider.Instance.ExecuteNonQuery(query);
        //}

        public void Checkout(int id, int discount, float totalPrice)
        {
            string query = "UPDATE dbo.Bill SET dateCheckOut = GETDATE(), status = 1, " + "discount = " + discount + ", totalPrice = " + totalPrice + " WHERE id = " + id ;
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public DataTable getListBillByDay(DateTime dayin, DateTime dayout)
        {
            return DataProvider.Instance.ExecuteQuery("exec spGetListBillByDate @checkIn , @checkOut", new object[] { dayin, dayout });
        }

        public void DeleteBillByTableId(int id)
        {
            DataProvider.Instance.ExecuteQuery("DELETE dbo.Bill WHERE idTable = " + id);
        }


        public void AddIdCustomerToBill()
        {
            int idCustomer = CustomerDAO.Instance.GetMaxIdCustomer();
            int idBillMax = GetMaxIdBill();
            DataProvider.Instance.ExecuteQuery(string.Format("UPDATE dbo.Bill SET idCustomer = {0} WHERE id = {1} ",idCustomer,idBillMax));
        }

        public bool UpdateTypePay()
        {   int id = GetMaxIdBill();
            string query = string.Format("UPDATE dbo.Bill SET typePay = N'Nợ' WHERE id = {0}",id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }      
        
    }
}
