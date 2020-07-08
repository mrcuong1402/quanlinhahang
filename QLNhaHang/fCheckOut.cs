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
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;

namespace QLNhaHang
{
    public partial class fCheckOut : Form
    {
        private Account lgAccount;

        public Account LgAccount
        {
            get { return lgAccount; }
            set { lgAccount = value; CheckAccount(lgAccount.Type); }
        }
        string CurentAccount = "";
        void CheckAccount(int type)
        {
            CurentAccount = lgAccount.DisplayName;
        }

        public fCheckOut()
        {
            InitializeComponent();
            ViewBillInfoExport();
        }

        private void fCheckOut_Load(object sender, EventArgs e)
        {
          
        }

        void ViewBillInfoExport()
        {
            dgvBillInfoExport.DataSource = BillInforDAO.Instance.ViewBillInfoExport();
        }

        private void btnExportBill_Click(object sender, EventArgs e)
        {
            AddCustomer();
            if (chbDept.Checked)
            {
                BillDAO.Instance.UpdateTypePay();
                ExportExcel();
            }
            else
            {
                ExportExcel();
            }
        }

        void AddCustomer()
        {               
            string name = txtNameCustomer.Text;
            string phone = txtPhoneNumber.Text;
            string adress = txtAddress.Text;
            int id = CustomerDAO.Instance.GetMaxIdCustomer();
            if (txtNameCustomer.Text == "" || txtPhoneNumber.Text == "" || txtAddress.Text == "")
            {
                MessageBox.Show("Chưa nhập đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    CustomerDAO.Instance.InsertCustomer(name, phone, adress);
                    BillDAO.Instance.AddIdCustomerToBill();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            } 
        }
        String NumberToString(decimal total)
        {

            string rs = "";
            total = Math.Round(total, 0);
            string[] ch = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] rch = { "lẻ", "mốt", "", "", "", "lăm" };
            string[] u = { "", "mươi", "trăm", "nghìn", "", "", "triệu", "", "", "tỷ", "", "", "nghìn", "", "", "triệu" };
            string nstr = total.ToString();
            int[] n = new int[nstr.Length];
            int len = n.Length;
            for (int i = 0; i < len; i++)
            {
                n[len - 1 - i] = Convert.ToInt32(nstr.Substring(i, 1));
            }
            for (int i = len - 1; i >= 0; i--)
            {
                if (i % 3 == 2)
                {
                    if (n[i] == 0 && n[i - 1] == 0 && n[i - 2] == 0) continue;
                }
                else if (i % 3 == 1)
                {
                    if (n[i] == 0)
                    {
                        if (n[i - 1] == 0) { continue; }
                        else
                        {
                            rs += " " + rch[n[i]]; continue;
                        }
                    }
                    if (n[i] == 1)
                    {
                        rs += " mười"; continue;
                    }
                }
                else if (i != len - 1)
                {
                    if (n[i] == 0)
                    {
                        if (i + 2 <= len - 1 && n[i + 2] == 0 && n[i + 1] == 0) continue;
                        rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);
                        continue;
                    }
                    if (n[i] == 1)
                    {
                        rs += " " + ((n[i + 1] == 1 || n[i + 1] == 0) ? ch[n[i]] : rch[n[i]]);
                        rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);
                        continue;
                    }
                    if (n[i] == 5) 
                    {
                        if (n[i + 1] != 0) 
                        {
                            rs += " " + rch[n[i]];
                            rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);
                            continue;
                        }
                    }
                }
                rs += (rs == "" ? " " : ", ") + ch[n[i]];
                rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);
            }
            if (rs[rs.Length - 1] != ' ')
                rs += " đồng.";
            else
                rs += "đồng.";
            if (rs.Length > 2)
            {
                string rs1 = rs.Substring(0, 2);
                rs1 = rs1.ToUpper();
                rs = rs.Substring(2);
                rs = rs1 + rs;
            }
            return rs.Trim().Replace("lẻ,", "lẻ").Replace("mươi,", "mươi").Replace("trăm,", "trăm").Replace("mười,", "mười");
        }
        
        void ExportExcel()
        {      
           
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = app.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;

            Microsoft.Office.Interop.Excel.Range range = worksheet.get_Range("A1", "F2");
            Microsoft.Office.Interop.Excel.Borders border = range.Borders;
            worksheet.Range["B1", "B25"].Columns.AutoFit();

            worksheet.Name = " Nhà hàng Sao Hỏa ";   
            worksheet.Cells[1, 2] = "NHÀ HÀNG SAO HỎA";
            worksheet.Cells[2, 2] = "Địa chỉ: Số 69, Ngã 4 Thiên Đình";
            worksheet.Cells[4, 2] = "HÓA ĐƠN BÁN HÀNG";
            worksheet.Cells[6, 2] = "Tên khách hàng: " + txtNameCustomer.Text;
            worksheet.Cells[7, 2] = "Địa chỉ: " + txtAddress.Text;
            worksheet.Cells[7, 5] = "Điện thoại: " + txtPhoneNumber.Text;
            worksheet.Cells[10, 2] = "STT";

            int totalPrice = 0;
            for (int i = 0; i < dgvBillInfoExport.Rows.Count; i++)
            {
                totalPrice += Int32.Parse(dgvBillInfoExport.Rows[i].Cells["Thành tiền"].Value.ToString());
            }           

            worksheet.Cells[26, 2] = "Tổng cộng:";
            worksheet.Cells[26, 6] = totalPrice;

            for (int i = 1; i < dgvBillInfoExport.Columns.Count + 1; i++)
            {
                worksheet.Cells[10, i+2] = dgvBillInfoExport.Columns[i - 1].HeaderText;
            }

            for (int i = 1; i <= dgvBillInfoExport.Rows.Count; i++)
            {
                worksheet.Cells[10 + i, 2] = i;
            }

            for (int i = 0; i < dgvBillInfoExport.Rows.Count; i++)
            {
                for (int j = 0; j < dgvBillInfoExport.Columns.Count; j++)
                {
                    worksheet.Cells[i + 11, j + 3] = dgvBillInfoExport.Rows[i].Cells[j].Value.ToString();
                }
            }

            string ReadNumber = NumberToString(totalPrice);
            
            worksheet.Cells[27, 2] = "Bằng chữ:";  
            worksheet.Cells[27, 3] = ReadNumber;
            worksheet.Cells[29, 3] = "Khách hàng";
            worksheet.Cells[29, 5] = "Người bán hàng";         

            worksheet.Range["C1", "C25"].Columns.AutoFit();
            worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[4, 7]].Merge();
            worksheet.Range[worksheet.Cells[27, 3], worksheet.Cells[27, 6]].Merge();
            worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[4, 7]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            worksheet.Range["A1", "G35"].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDouble;
            worksheet.Range["A1", "G35"].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDouble;
            worksheet.Range["A1", "G35"].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDouble;
            worksheet.Range["A1", "G35"].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDouble;
           
            worksheet.Range["B26", "F27"].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            worksheet.Range["B26", "F27"].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            worksheet.Range["B26", "F27"].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            worksheet.Range["B26", "F27"].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            worksheet.Range["B10", "F25"].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            worksheet.Range[worksheet.Cells[10, 2], worksheet.Cells[25, 2]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            worksheet.Range[worksheet.Cells[10, 2], worksheet.Cells[10, 6]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            worksheet.Range[worksheet.Cells[11, 4], worksheet.Cells[25, 4]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "File Name";
            saveFileDialog.DefaultExt = ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    workbook.SaveAs(saveFileDialog.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                catch { }
            }
            app.Quit();
        }                  
    }
}
