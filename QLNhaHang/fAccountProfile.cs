using QLNhaHang.DAO;
using QLNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhaHang
{
    public partial class fAccountProfile : Form
    {
        int flag = 0;
        private Account lgAccount;

        public Account LgAccount
        {
            get { return lgAccount; }
            set { lgAccount = value; CheckAccount(lgAccount); }
        }
        public fAccountProfile(Account acc)
        {
            InitializeComponent();
            LgAccount = acc;
            LoadPanelPass(false);
            btnUpdate.Enabled = false;
        }
        void LoadPanelPass(bool e)
        {
            panel3.Visible = panel4.Visible = panel5.Visible = e;
            btnUpdate.Enabled = e;
        }
        void CheckAccount(Account acc)
        {
            txtDisplayName.Text = lgAccount.DisplayName;
            txtUserName.Text = lgAccount.UserName;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (flag == 0)
            {
                try
                {
                    ChangePassword();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    ChangeDisplayName();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //LoadPanelPass(false);
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {   
            flag = 0;
            btnUpdate.Enabled = true;
            LoadPanelPass(true);
        }

        void ChangePassword()
        {
            string userName = txtUserName.Text;
            string pass = txtPassWord.Text;
            string newPass = txtNewPassWord.Text;
            string reEnter = txtReEnterPass.Text;

                if (!newPass.Equals(reEnter))
                {
                    MessageBox.Show("Mật khẩu mới không trùng nhau", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewPassWord.Focus();
                    txtReEnterPass.Clear();
                }
                else
                {
                    if (AccountDAO.Instance.ChangePassword(userName, pass, newPass))
                    {
                        MessageBox.Show("Đổi mật khẩu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Mật khảu cũ không chính xác !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }                                                 
        }

        void ChangeDisplayName()
        {
            string userName = txtUserName.Text;
            string pass = txtPassWord.Text;
            string displayName = txtDisplayName.Text;

            if (AccountDAO.Instance.ChangDisplayName(userName, displayName, pass))
            {
                MessageBox.Show("Đổi tên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Sai mật khẩu. Vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnChangeDisplayName_Click(object sender, EventArgs e)
        {
            flag = 1;
            btnUpdate.Enabled = true;           
            LoadPanelPass(true);
            panel4.Visible = false;
            panel5.Visible = false;
        }
    }
}
