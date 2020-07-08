using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNhaHang.DAO
{
    public class ImportDAO
    {
        private static ImportDAO instance;

        public static  ImportDAO Instance
        {
            get { if (instance == null) instance = new ImportDAO(); return instance; }
            private set { instance = value; }
        }

        public ImportDAO() { }

        public DataTable GetListImportByDate(DateTime dayImport)
        {
            return DataProvider.Instance.ExecuteQuery("EXEC dbo.spGetListProductByDate @dayImport ", new object [] { dayImport } );
        }

        public DataTable GetListImport()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT id AS[ID], name AS [Tên hàng], price AS [Giá tiền], unit AS [Đơn vị tính], count AS [Số lượng], totalPrice AS [Thành tiền], DayImport AS [Ngày nhập] FROM dbo.Import");
        }

        public bool InsertProduct(string name, float price, string unit, int count)
        {
            string query = string.Format("INSERT dbo.Import(name , price , unit , count , totalPrice, DayImport ) VALUES ( N'{0}' , {1} ,  N'{2}' , {3} , {4}, GETDATE() ) ", name, price, unit, count, count * price);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateProduct(int id, string name, float price, string unit, int count)
        {
            string query = "UPDATE dbo.Import SET name = N'"+name+"' ,price = '"+price+"' , unit = '"+unit+"' , count = '"+count+"', totalPrice = '"+ price * count +"' WHERE id = " + id;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteProduct(int id)
        {
            string query = "DELETE dbo.Import WHERE id = " + id;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
