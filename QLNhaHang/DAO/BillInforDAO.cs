using QLNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNhaHang.DAO
{
    public class BillInforDAO
    {
        private static BillInforDAO instance;

        public static BillInforDAO Instance
        {
            get { if (instance == null) instance = new BillInforDAO(); return BillInforDAO.instance; }
            private set { BillInforDAO.instance = value; }
        }

        private BillInforDAO() { }

        public void InsertBillInfo(int idBill, int idFood,int count)
        {
            DataProvider.Instance.ExecuteNonQuery("spInsertBillInfo @idBill , @idFood , @count", new object[] { idBill, idFood, count });
        }
        /// <summary>
        /// Xóa Food trong BillInfo
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBillinfoByFoodId(int id)
        {
            DataProvider.Instance.ExecuteQuery("DELETE dbo.BillInfo WHERE idFood = " + id);
        }

        public DataTable ViewBillInfoExport()
        {
            return DataProvider.Instance.ExecuteQuery("EXEC dbo.ExportReport");
        }
    }
}
