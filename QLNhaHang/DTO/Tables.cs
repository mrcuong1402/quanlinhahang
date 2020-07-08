using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace QLNhaHang.DTO
{
    public class Tables
    {
        public Tables(int id, string name, string status)
        {
            this.ID = id;
            this.Name = name;
            this.Status = status;
        }
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Tables(DataRow row)
        {
            this.ID=(int)row["iD"];
            this.Name = row["name"].ToString();
            this.Status = row["status"].ToString();
        }
    }
}
