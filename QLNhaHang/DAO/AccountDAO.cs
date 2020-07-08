using QLNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNhaHang.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        private AccountDAO() { }

        public bool Login(string userName, string passWord)
        {
            string sql = "SELECT *FROM dbo.Account WHERE UserName=N'"+userName+"'AND PassWord =N'"+passWord+"'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql);
            return dt.Rows.Count > 0;
        }

        public Account getAccountByUserName(string userName)
        {
            DataTable data =  DataProvider.Instance.ExecuteQuery("SELECT *FROM dbo.Account WHERE UserName = '" + userName + "'");
            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }

        public bool ChangePassword(string username,string oldPass, string newPass)
        {          
            int result = DataProvider.Instance.ExecuteNonQuery("EXEC dbo.spChangePasswordAccount @userName , @password , @newPassword", new object[]{username,oldPass,newPass});
            return result > 0;
        }
        public bool ChangDisplayName(string username, string displayName, string Pass)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("exec spChangeDisplayNameAccount @userName , @displayName , @password", new object[]{username,displayName,Pass});
            return result > 0;
        }

        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT id AS [ID], UserName AS [Tên đăng nhập], DisplayName AS [Họ tên], Typee AS [Chức vụ] FROM dbo.Account");
        }

        public Account GetAccountById(int id)
        {
            Account cat = null;
            string query = "select *from Account where id = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                cat = new Account(item);
                return cat;
            }
            return cat;
        }



        public bool InsertAccount(string userName, string DisplayName, string passWord, int type)
        {
            string query = string.Format("INSERT dbo.Account( UserName ,DisplayName ,PassWord ,Typee) VALUES  ( N'{0}' , N'{1}' , N'{2}' , {3} )",userName,DisplayName,passWord,type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
         /// <summary>
         /// Admin chỉ có quyền đồi Tên hiển thị và CHức vụ
         /// </summary>
         /// <param name="DisplayName"></param>
         /// <param name="type"></param>
         /// <returns></returns>
        public bool UpdateAccount(int id, string DisplayName, int type)
        {
            string query = string.Format("UPDATE dbo.Account SET DisplayName =N'{0}' , Typee = {1} WHERE id = {2}",DisplayName,type,id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteAccount(int id)
        {
            string query = string.Format("DELETE dbo.Account WHERE id = {0}", id );
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool ResetPassword(int id)
        {
            string query = string.Format("UPDATE dbo.Account SET password = '1111' WHERE id = {0}",id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }      
    }
}
