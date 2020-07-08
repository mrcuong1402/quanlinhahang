using QLNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNhaHang.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance
        {
            get { if (instance == null)instance = new CategoryDAO(); return CategoryDAO.instance; }
            private set { CategoryDAO.instance = value; }
        }

        private CategoryDAO() { }

        public List<Category> getListCategory()
        {
            List<Category> listCategory = new List<Category>();
            string query = "SELECT *FROM dbo.FoodCategory";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);
                listCategory.Add(category);
            }
            return listCategory;
        }

        public DataTable GetListCategory()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT id AS [ID], name AS [Tên Thực đơn] FROM dbo.FoodCategory ");
        }
        public Category getCategoryById(int id)
        {
            Category cat = null;
            string query = "select *from foodcategory where id = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                cat = new Category(item);
                return cat;
            }
            return cat;
        }

        public bool InsertCategory(string name)
        {
            string query = "INSERT dbo.FoodCategory VALUES  ( N'" + name + "')";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateCategory(int id, string name)
        {
            string query = "UPDATE dbo.FoodCategory SET name = N'" + name + "' WHERE id = " + id;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteCategory(int id)
        {
            string query = string.Format("Delete FoodCategory where id = {0}", id); ;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }           
      
    }
}
