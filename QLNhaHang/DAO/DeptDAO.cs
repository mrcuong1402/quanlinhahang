using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNhaHang.DAO
{
    public class DeptDAO
    {
        private static DeptDAO instance;

        public static DeptDAO Instance
        {
            get { if (instance == null) instance = new DeptDAO(); return instance; }
            private set { instance = value; }
        }

        private DeptDAO() { }

        public DataTable GetListDept()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT b.id AS [Số hóa đơn], c.name AS [Tên khách hàng], c.phone AS [Điện thoại], c.address AS [Địa chỉ], b.DateCheckIn AS [Ngày đến], b.totalPrice AS [Tổng tiền] FROM dbo.Bill AS b INNER JOIN dbo.Customer AS c ON c.Id = b.idCustomer WHERE typePay = N'Nợ'");
        }

        public bool FinishDept(int idBill)
        {                  
            int result = DataProvider.Instance.ExecuteNonQuery("EXEC spFinishDept @idBill",new object[] {idBill});
            return result > 0;
        }

        public DataTable DeptInfo(int idBill)
        {
            string query = "SELECT f.name AS [Tên món] , bi.count AS [Số lượng] , f.price AS [Đơn giá], (f.price * bi.count) AS [Thành tiền] FROM dbo.Bill b , dbo.BillInfo bi, dbo.Food f WHERE  bi.idBill = b.id AND f.id = bi.idFood AND b.id = " + idBill;
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
