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
    public partial class fTableManager : Form
    {
        private Account lgAccount;

        public Account LgAccount
        {
            get { return lgAccount; }
            set { lgAccount = value; CheckAccount(lgAccount.Type); }
        }

        public fTableManager(Account acc)
        {
            InitializeComponent();
            this.LgAccount = acc;
            LoadTable();
            LoadCategory();
            LoadComboboxTable(cbSwitchTable);
        }
        #region Event

        void btn_Click(object sender, EventArgs e)
        {          
            int id = ((sender as Button).Tag as Tables).ID;
            ShowBill(id);
            listView_Bill.Tag = (sender as Button).Tag;
            //(sender as Button).BackColor = Color.Red;
            label6.Text = (sender as Button).Text;
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile(lgAccount);
            f.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.InsertFood += f_InsertFood;
            f.DeleteFood += f_DeleteFood;
            f.UpdateFood += f_UpdateFood;
            f.ShowDialog();
        }

        private void f_UpdateFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryId((cbCategory.SelectedItem as Category).Id);
            if(listView_Bill.Tag !=null)
                ShowBill((listView_Bill.Tag as Tables).ID);
            flowLayoutPanel_Table.Controls.Clear();
        }

        private void f_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryId((cbCategory.SelectedItem as Category).Id);
            if (listView_Bill.Tag != null)
                ShowBill((listView_Bill.Tag as Tables).ID);
            flowLayoutPanel_Table.Controls.Clear();
            LoadTable();
        }

        private void f_InsertFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryId((cbCategory.SelectedItem as Category).Id);
            if (listView_Bill.Tag != null)
                ShowBill((listView_Bill.Tag as Tables).ID);
        }

        #endregion
        void LoadComboboxTable(ComboBox cb)
        {
            cbSwitchTable.DataSource = TableDAO.Instance.LoadTableList();
            cbSwitchTable.DisplayMember = "Name";
        }

        void ShowBill(int id)
        {
           flowLayoutPanel_Table.Controls.Clear();
           listView_Bill.Items.Clear();
           float totalPrice = 0;
           List<QLNhaHang.DTO.Menu> listBillInfo = MenuDAO.Instance.getListMenuByTable(id);
           foreach (QLNhaHang.DTO.Menu item in listBillInfo)
           {
               ListViewItem lvItem = new ListViewItem(item.FoodName.ToString());              
               lvItem.SubItems.Add(item.Count.ToString());
               lvItem.SubItems.Add(item.Price.ToString());
               lvItem.SubItems.Add(item.TotalPrice.ToString());
               totalPrice += item.TotalPrice;
               listView_Bill.Items.Add(lvItem);
           }  
               lbTotalPrice.Text = totalPrice.ToString("c");
               LoadTable();
        }
        #region Methods

        void CheckAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += " (" + lgAccount.DisplayName + ")";
        }

        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.getListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "name";
        }

        void LoadFoodListByCategoryId(int id)
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "Name";
        }

        void LoadTable()
        {
            List<Tables> tbList = TableDAO.Instance.LoadTableList();
            foreach (Tables item in tbList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
               //btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Font = new Font("Constantia", 10F);
                btn.Text = item.Name;
                btn.Click += btn_Click;
                btn.BackColor = Color.Chocolate;
                btn.Tag = item;
                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.LightSkyBlue;
                        break;
                    default:
                        btn.BackColor = Color.Green;
                        break;
                }
                flowLayoutPanel_Table.Controls.Add(btn);                 
            }             
        }

        
        #endregion

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
            {
                return;
            }

            Category selected = cb.SelectedItem as Category;
            id = selected.Id;
            LoadFoodListByCategoryId(id);
        }
        bool flag = false;
        private void btnAddFood_Click(object sender, EventArgs e)
        {           
                Tables table = listView_Bill.Tag as Tables;
                if (table == null)
                {
                    MessageBox.Show("Hãy chọn bàn","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                int idBill = BillDAO.Instance.GetUnCheckBillIdByTableId(table.ID);
                int idFood = (cbFood.SelectedItem as Food).ID;
                int count = (int)nmAddFood.Value;               
                //thêm món ăn trong trường hợp bàn rỗng sẽ tạo hóa đơn mới 
                if (idBill == -1)
                {
                    BillDAO.Instance.InsertBill(table.ID);
                    BillInforDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIdBill(), idFood, count);
                }
                else
                {
                    //thêm món khi hóa đơn đã tồn tại
                    BillInforDAO.Instance.InsertBillInfo(idBill, idFood, count);
                }

                ShowBill(table.ID);
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            try
            {
                flag = true;
                Tables table = listView_Bill.Tag as Tables;
                int idBill = BillDAO.Instance.GetUnCheckBillIdByTableId(table.ID);
                int discount = (int)nmDisCount.Value;

                double totalPrice = Convert.ToDouble(lbTotalPrice.Text.Split(',')[0]);
                double finalTotalPrice = totalPrice - (totalPrice / 100) * discount;


                if (idBill != -1)
                {
                    if (MessageBox.Show(string.Format("Bạn có chắc thanh toán hóa đơn cho bàn {0}\nTổng tiền - (Tổng tiền / 100) x Giảm giá\n=> {1} - ({1} / 100) x {2} = {3}", table.Name, totalPrice, discount, finalTotalPrice), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        BillDAO.Instance.Checkout(idBill, discount, (float)finalTotalPrice);
                        ShowBill(table.ID);
                        flowLayoutPanel_Table.Controls.Clear();
                        LoadTable();
                    }
                }
            }
            catch { }
        }

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            try
            {
                int id1 = (listView_Bill.Tag as Tables).ID;
                int id2 = (cbSwitchTable.SelectedItem as Tables).ID;
                if (MessageBox.Show(string.Format("Bạn có thật sự muốn chuyển bàn {0} qua bàn {1}", (listView_Bill.Tag as Tables).Name, (cbSwitchTable.SelectedItem as Tables).Name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    TableDAO.Instance.SwitchTable(id1, id2);
                    flowLayoutPanel_Table.Controls.Clear();
                    LoadTable();
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn bàn trước !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnExportBill_Click(object sender, EventArgs e)
        {
            if (flag == true)
            {
                fCheckOut f = new fCheckOut();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Thanh toán trước để in hóa đơn !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void giớiThiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fRestaurantInfo f = new fRestaurantInfo();
            f.ShowDialog();
        }         
    }
}
