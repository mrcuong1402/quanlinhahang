using QLNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNhaHang.DAO
{
    public class CustomerDAO
    {
        private static CustomerDAO instance;

        public static  CustomerDAO Instance
        {
            get { if (instance == null) instance = new CustomerDAO(); return instance; }
            private set { instance = value; }
        }

        public CustomerDAO() { }

        public DataTable GetListCustomer()
        {
            return DataProvider.Instance.ExecuteQuery(" SELECT id AS [ID] , name AS [Tên khách hàng] , phone AS [Điện thoại] , address AS [Địa chỉ] FROM dbo.Customer ");
        }

        public bool InsertCustomer(string name, string phone, string adress)
        {
            string query = string.Format("INSERT INTO dbo.Customer( name, phone, address ) VALUES  ( N'{0}',N'{1}',N'{2}')",name,phone,adress);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateCustomer(int id, string name, string phone, string adress)
        {
            string query = "UPDATE dbo.Customer SET name = N'" + name + "' ,phone = '" + phone + "' , address = '" + adress + "' WHERE id = " + id;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteCustomer(int id)
        {
            string query = "DELETE dbo.Customer WHERE id = " + id;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public int GetMaxIdCustomer()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("SELECT MAX(id) FROM dbo.Customer");
            }
            catch
            {
                return 1;
            }
        }
    }
}
