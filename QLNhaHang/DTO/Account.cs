using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNhaHang.DTO
{
    public class Account
    {
        public Account(string name, string displayName, int type, string pass = null)
        {
            this.UserName = name;
            this.DisplayName = displayName;
            this.Type = type;
            this.PassWord = pass;
        }
        public Account(DataRow row)
        {
            this.UserName = row["userName"].ToString();
            this.DisplayName = row["displayName"].ToString();
            this.Type = (int)row["typee"];
            this.PassWord = row["password"].ToString();
        }
        private int type;

        public int Type
        {
            get { return type; }
            set { type = value; }
        }
        private string displayName;

        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }
        private string passWord;

        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
    }
}
