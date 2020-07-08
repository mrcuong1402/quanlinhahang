using QLNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNhaHang.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get { if (instance == null)instance = new FoodDAO(); return FoodDAO.instance; }
            private set { FoodDAO.instance = value; }
        }

        private FoodDAO() { }

        public List<Food> GetFoodByCategoryID(int id)
        {
            List<Food> list = new List<Food>();
            string query = "select * from Food where idCategory = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }

        public DataTable GetListFood()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT *FROM dbo.Food");
        }

        public bool InsertFood(string name, int idCategory, float price)
        {
            string query = "INSERT INTO dbo.Food( name, idCategory, price ) VALUES  ( N'"+name+"' , "+idCategory+","+price+")";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateFood(int id, string name, int idCategory, float price)
        {
            string query = "UPDATE dbo.Food SET name = N'"+name+"', idCategory='"+idCategory+"', price='"+price+"' where id = " + id;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteFoodByIdFood(int id)
        {
            BillInforDAO.Instance.DeleteBillinfoByFoodId(id);
            string query = string.Format("Delete Food where id = {0}",id); ;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        /// <summary>
        /// Xóa Food có idCategory = id(Category)
        /// </summary>
        /// <param name="id"></param>
        public void DeleteFoodByCategoryId(int id)
        {
            DataProvider.Instance.ExecuteQuery("DELETE dbo.Food WHERE idCategory = " + id);
        }

        public List<Food> SearchFoodByName(string name)
        {
            List<Food> list = new List<Food>();
            string query = string.Format("SELECT * FROM dbo.Food WHERE dbo.fuConvertToUnsign1(name) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }


        // xóa danh mục

        /// <summary>
        /// Xóa tát cả food có idCategory = "idCategory" trong bảng BillInfo       
        /// </summary>
        /// <param name="idCategory"></param>
        public void DeleteFoodInBillInfoByIdCategory(int idCategory)
        {
 
        }

        public DataTable SortFood()
        {
            return DataProvider.Instance.ExecuteQuery("EXEC spSortFood");
        }


    }
}
