using QLNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNhaHang.DAO
{
    public class TableDAO
    {
        public static int TableWidth = 90;
        public static int TableHeight = 90;
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return TableDAO.instance; }
            private set { TableDAO.instance = value; }
        }

        private TableDAO() { }

        public List<Tables> LoadTableList()
        {
            List<Tables> tbList = new List<Tables>();
            DataTable data = DataProvider.Instance.ExecuteQuery("EXEC dbo.spGetTableList");

            foreach (DataRow items in data.Rows)
            {
                Tables tb = new Tables(items);
                tbList.Add(tb);
            }
            return tbList;
        }

        public void SwitchTable(int id1, int id2)
        {
            DataProvider.Instance.ExecuteQuery("USP_SwitchTabel @idTable1 , @idTabel2", new object[] { id1, id2 });
        }
        public DataTable GetListTable()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT id AS [ID], name AS [Tên bàn], status AS [Trạng thái] FROM dbo.TableFood");
        }

        public Tables getTableById(int id)
        {
            Tables table = null;
            string query = "select *from TableFood where id = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                table = new Tables(item);
                return table;
            }
            return table;
        }

        public bool InsertTableFood(string name, string status)
        {
            string query = "INSERT dbo.TableFood VALUES  ( N'" + name + "' , N'" + status + "')";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateTableFood(int id, string name)
        {
            string query = "UPDATE dbo.TableFood SET name = N'"+name+"' WHERE id = " + id;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteTableFood(int id)
        {
            string query = "DELETE dbo.TableFood WHERE id = " + id;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public void DeleteFoodByCategoryId(int id)
        {
            DataProvider.Instance.ExecuteQuery("DELETE dbo.FoodCategory WHERE id = " + id);
        }
    }
}
