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
    public partial class fAdmin : Form
    {
        BindingSource foodlist = new BindingSource();
        BindingSource tableList = new BindingSource();
        BindingSource accountType = new BindingSource();
        BindingSource import = new BindingSource();
        BindingSource customer = new BindingSource();
        BindingSource dept = new BindingSource();
        public fAdmin()
        {
            InitializeComponent();
            LoadAll();
        }

        #region Methods
    
        void LoadAll()
        {            
            dgvDept.DataSource = dept;
            dgvFood.DataSource = foodlist;
            dgvTablename.DataSource = tableList;
            dgvAccount.DataSource = accountType;
            dgvProduct.DataSource = import;
            dgvCustomer.DataSource = customer;
            loadAccount();
            loadTableFood();
            LoadListBillByDay(tpFromDate.Value, tpToDate.Value);
            loadDateTimePicker();
            loadListFood();
            loadCategory();
            BindingFood();
            BindingCategory();
            loadComboBoxCategoryFood(cbCategoryFood);
            BindingTable();
            BingdingAccount();
            loadImport();
            BindingImport();
            BindingCustomer();
            loadListCustomer();
            LoadListDept();
            BinDingDept();
        }
        #endregion


        #region Bill
        void LoadListProcductByDay(DateTime dayImport)
        {
            dgvProduct.DataSource = ImportDAO.Instance.GetListImportByDate(dayImport);
        }

        void loadDateTimePicker()
        {
            DateTime today = DateTime.Now;
            tpFromDate.Value = new DateTime(today.Year, today.Month, 1);
            tpToDate.Value = tpFromDate.Value.AddMonths(1).AddDays(-1);

        }

        void LoadListBillByDay(DateTime checkIn, DateTime checkOut)
        {
            dgvBill.DataSource = BillDAO.Instance.getListBillByDay(checkIn, checkOut);
            int sc = dgvBill.Rows.Count;
            float totalPrice = 0;
            for (int i = 0; i < sc; i++)
            {
                totalPrice += float.Parse(dgvBill.Rows[i].Cells["Tổng tiền"].Value.ToString());
            }
            txtBill.Text = totalPrice.ToString("c");
        }

        private void fAdmin_Load(object sender, EventArgs e)
        {
          
        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDay(tpFromDate.Value, tpToDate.Value);
            tpToday.Visible = false;
        }        

        #endregion

        #region Food

        void SortFood()
        {
            dgvFood.DataSource = FoodDAO.Instance.SortFood();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SortFood();
        }

        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = FoodDAO.Instance.SearchFoodByName(name);            
            return listFood;
        }


        private void btnSearchFood_Click(object sender, EventArgs e)
        {             
                string search = txtSearchFood.Text;
                foodlist.DataSource = SearchFoodByName(search);        
        }

        void BindingFood()
        {
            txtNameFood.DataBindings.Add(new Binding("text", dgvFood.DataSource, "name", true, DataSourceUpdateMode.Never));
            txtIdFood.DataBindings.Add(new Binding("text", dgvFood.DataSource, "id", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("value", dgvFood.DataSource, "price", true, DataSourceUpdateMode.Never));
        }

        void loadListFood()
        {
            foodlist.DataSource = FoodDAO.Instance.GetListFood();

        }
        private void btnShowFood_Click(object sender, EventArgs e)
        {
            loadListFood();
        }

        private void txtSearchFood_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSearchFood_Click(sender, e);
            }
        }

        private void txtIdFood_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvFood.SelectedCells.Count > 0)
                {
                    int id = (int)dgvFood.SelectedCells[0].OwningRow.Cells["IdCategory"].Value;

                    Category cateogory = CategoryDAO.Instance.getCategoryById(id);

                    cbCategoryFood.SelectedItem = cateogory;

                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbCategoryFood.Items)
                    {
                        if (item.Id == cateogory.Id)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }

                    cbCategoryFood.SelectedIndex = index;
                }
            }
            catch { }            

        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txtNameFood.Text;
            int idCategory = (cbCategoryFood.SelectedItem as Category).Id;
            float price = (float)nmFoodPrice.Value;
            if (FoodDAO.Instance.InsertFood(name, idCategory, price))
            {
                MessageBox.Show("Thêm món thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadListFood();
                if (insertFood != null)
                {
                    insertFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi rồi !");
            }
        }

        private void btnUpdateFood_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(txtIdFood.Text);
            string name = txtNameFood.Text;
            int idCategory = (cbCategoryFood.SelectedItem as Category).Id;
            float price = (float)nmFoodPrice.Value;
            if (FoodDAO.Instance.UpdateFood(id, name, idCategory, price))
            {
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadListFood();
                if (updateFood != null)
                {
                    updateFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi rồi !");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(txtIdFood.Text);
            if (FoodDAO.Instance.DeleteFoodByIdFood(id))
            {
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadListFood();
                if (deleteFood != null)
                {
                    deleteFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi rồi !");
            }
        }

        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }
        #endregion

        #region Category

        void loadComboBoxCategoryFood(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.getListCategory();
            cb.DisplayMember = "name";
        }

        void BindingCategory()
        {
            txtIdCategory.DataBindings.Add(new Binding("text", dgvCategory.DataSource, "id", true, DataSourceUpdateMode.Never));
            txtNameCategory.DataBindings.Add(new Binding("text", dgvCategory.DataSource, "Tên Thực đơn", true, DataSourceUpdateMode.Never));
        }   

        void loadCategory()
        {
            dgvCategory.DataSource = CategoryDAO.Instance.GetListCategory();
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            loadCategory();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            //int id = Convert.ToInt32(txtIdCategory.Text);
            string nameCategory = txtNameCategory.Text;
            if (CategoryDAO.Instance.InsertCategory(nameCategory))
            {
                MessageBox.Show("Thêm danh mục thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadCategory();
            }
            else
            {
                MessageBox.Show("Có lỗi rồi !");
            }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIdCategory.Text);
            string nameCategory = txtNameCategory.Text;
            if (CategoryDAO.Instance.UpdateCategory(id, nameCategory))
            {
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadCategory();
            }
            else
            {
                MessageBox.Show("Có lỗi rồi !");
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(txtIdCategory.Text);
            try
            {
                if (CategoryDAO.Instance.DeleteCategory(id))
                {
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadCategory();
                }
                else
                {
                    MessageBox.Show("Có lỗi rồi !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion

        #region Tables

        void loadTableFood()
        {
            tableList.DataSource = TableDAO.Instance.GetListTable();
        }

        void BindingTable()
        {
            txtIdTable.DataBindings.Add(new Binding("text", dgvTablename.DataSource, "id", true, DataSourceUpdateMode.Never));
            txtTableName.DataBindings.Add(new Binding("text", dgvTablename.DataSource, "Tên bàn", true, DataSourceUpdateMode.Never));
            cbStatusTable.DataBindings.Add(new Binding("text", dgvTablename.DataSource, "Trạng thái", true, DataSourceUpdateMode.Never));
        }

        void loadComboBoxTableFood(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember = "status";
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            loadTableFood();
        }
         

        private void btnAddTable_Click(object sender, EventArgs e)
        {          
            string tableName = txtTableName.Text;
            string status = cbStatusTable.Text;
            if (TableDAO.Instance.InsertTableFood(tableName,status))
            {
                MessageBox.Show("Thêm bàn thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadTableFood();
            }
            else
            {
                MessageBox.Show("Có lỗi rồi !");
            }
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIdTable.Text);
            string tableName = txtTableName.Text;
            if (TableDAO.Instance.UpdateTableFood(id, tableName))
            {
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadTableFood();
            }
            else
            {
                MessageBox.Show("Có lỗi rồi !");
            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIdTable.Text);
            if (TableDAO.Instance.DeleteTableFood(id))
            {
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadTableFood();
            }
            else
            {
                MessageBox.Show("Có lỗi rồi !");
            }
        }

        #endregion 

        #region Account

        void BingdingAccount()
        {
            txtIdAccount.DataBindings.Add(new Binding("text", dgvAccount.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtUserName.DataBindings.Add(new Binding("text", dgvAccount.DataSource, "Tên đăng nhập", true, DataSourceUpdateMode.Never));
            txtDisplayName.DataBindings.Add(new Binding("text", dgvAccount.DataSource, "Họ tên", true, DataSourceUpdateMode.Never));
            cbAccountType.DataBindings.Clear();
            cbAccountType.DataBindings.Add("text", dgvAccount.DataSource, "Chức vụ", true, DataSourceUpdateMode.Never);
        }

        void loadAccount()
        {
            accountType.DataSource = AccountDAO.Instance.GetListAccount();
        }
        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            loadAccount();
        }

       
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string displayName = txtDisplayName.Text;
            string passWord = txtPassword.Text;
            int type = Int32.Parse(cbAccountType.Text);
            if (txtPassword.Text == null || txtPassword.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu trước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (AccountDAO.Instance.InsertAccount(userName, displayName, passWord, type))
                {
                    MessageBox.Show("Thêm Tài khoản thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadAccount();
                }
                else
                {
                    MessageBox.Show("Có lỗi rồi !");
                }
            }
            
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string displayName = txtDisplayName.Text;
            int type = Int32.Parse(cbAccountType.Text);
            int id = Int32.Parse(txtIdAccount.Text);
            if (AccountDAO.Instance.UpdateAccount(id, displayName, type))
            {
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadAccount();
            }
            else
            {
                MessageBox.Show("Có lỗi rồi !");
            }
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(txtIdAccount.Text);
            if (AccountDAO.Instance.DeleteAccount(id))
            {
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadAccount();
            }
            else
            {
                MessageBox.Show("Có lỗi rồi !");
            }
        }

        private void btnResetPass_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(txtIdAccount.Text);
            if (MessageBox.Show("Bạn có muốn reset mật khẩu về mặc định là 1111", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (AccountDAO.Instance.ResetPassword(id))
                {
                    MessageBox.Show("Reset thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion             

        #region Import

        void BindingImport()
        {
            txtIdProduct.DataBindings.Add(new Binding("text", dgvProduct.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtNameProduct.DataBindings.Add(new Binding("text", dgvProduct.DataSource, "Tên hàng", true, DataSourceUpdateMode.Never));
            txtPriceProduct.DataBindings.Add(new Binding("text", dgvProduct.DataSource, "Giá tiền", true, DataSourceUpdateMode.Never));
            txtUnit.DataBindings.Add(new Binding("text", dgvProduct.DataSource, "Đơn vị tính", true, DataSourceUpdateMode.Never));
            txtCountProduct.DataBindings.Add(new Binding("text", dgvProduct.DataSource, "Số lượng", true, DataSourceUpdateMode.Never));
            txtTotalPriceProduuct.DataBindings.Add(new Binding("text", dgvProduct.DataSource, "Thành tiền", true, DataSourceUpdateMode.Never));

        }

        void loadImport()
        {
            import.DataSource = ImportDAO.Instance.GetListImport();
        }

        private void btnShowProduct_Click(object sender, EventArgs e)
        {
            try
            {
                LoadListProcductByDay(tpDayImport.Value);
            }
            catch     (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            string name = txtNameProduct.Text;
            float price = (float)Convert.ToDouble(txtPriceProduct.Text);
            string unit = txtUnit.Text;
            int count = Int32.Parse(txtCountProduct.Text);
            try
            {
                if (ImportDAO.Instance.InsertProduct(name, price, unit, count))
                {
                    MessageBox.Show("Thêm hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadImport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(txtIdProduct.Text);
            string name = txtNameProduct.Text;
            float price = (float)Convert.ToDouble(txtPriceProduct.Text);
            string unit = txtUnit.Text;
            int count = Int32.Parse(txtCountProduct.Text);
            try
            {
                if (ImportDAO.Instance.UpdateProduct(id, name, price, unit, count))
                {
                    MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadImport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(txtIdProduct.Text);
            try
            {
                if (ImportDAO.Instance.DeleteProduct(id))
                {
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadImport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion            

        #region Customer

        void BindingCustomer()
        {
            txtIdCustomer.DataBindings.Add(new Binding("text", dgvCustomer.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtNameCustomer.DataBindings.Add(new Binding("text", dgvCustomer.DataSource, "Tên khách hàng", true, DataSourceUpdateMode.Never));
            txtPhoneNumber.DataBindings.Add(new Binding("text", dgvCustomer.DataSource, "Điện thoại", true, DataSourceUpdateMode.Never));
            txtAddress.DataBindings.Add(new Binding("text", dgvCustomer.DataSource, "Địa chỉ", true, DataSourceUpdateMode.Never));
        }

        void loadListCustomer()
        {
            customer.DataSource = CustomerDAO.Instance.GetListCustomer();
        }
        private void btnShowCustomer_Click(object sender, EventArgs e)
        {
            loadListCustomer();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            string name = txtNameCustomer.Text;
            string phone = txtPhoneNumber.Text;
            string adress = txtAddress.Text;

            try
            {
                if (CustomerDAO.Instance.InsertCustomer(name, phone, adress))
                {
                    MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadListCustomer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(txtIdCustomer.Text);
            string name = txtNameCustomer.Text;
            string phone = txtPhoneNumber.Text;
            string adress = txtAddress.Text;

            try
            {
                if (CustomerDAO.Instance.UpdateCustomer(id, name, phone, adress))
                {
                    MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadListCustomer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(txtIdCustomer.Text);

            try
            {
                if (CustomerDAO.Instance.DeleteCustomer(id))
                {
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadListCustomer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion         

        #region Dept

        void BinDingDept()
        {
            txtIdBillDept.DataBindings.Add(new Binding("text", dgvDept.DataSource, "Số hóa đơn", true, DataSourceUpdateMode.Never));
            txtDeptCustomer.DataBindings.Add(new Binding("text", dgvDept.DataSource, "Tên khách hàng", true, DataSourceUpdateMode.Never));
            txtPhoneDeptCustomer.DataBindings.Add(new Binding("text", dgvDept.DataSource, "Điện thoại", true, DataSourceUpdateMode.Never));
            txtAddressCustomer.DataBindings.Add(new Binding("text", dgvDept.DataSource, "Địa chỉ", true, DataSourceUpdateMode.Never));
            txtTotalDept.DataBindings.Add(new Binding("text", dgvDept.DataSource, "Tổng tiền", true, DataSourceUpdateMode.Never));
        }

        void LoadListDept()
        {
            dgvDept.DataSource = DeptDAO.Instance.GetListDept();
        }

        private void dgvDept_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Int32.Parse(txtIdBillDept.Text);
            dgvDeptInfo.DataSource = DeptDAO.Instance.DeptInfo(id);
        }

        private void btnFinishDept_Click(object sender, EventArgs e)
        {
            int idBill = Int32.Parse(txtIdBillDept.Text);
            if (DeptDAO.Instance.FinishDept(idBill))
            {
                MessageBox.Show("Đã thu nợ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListDept();
                dgvDeptInfo.Refresh();
            }                              
        }
        #endregion         

        private void tpFromDate_ValueChanged(object sender, EventArgs e)
        {

        }

        
       
    }
}
