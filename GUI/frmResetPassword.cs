using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using MongoDB.Bson;

namespace GUI
{
    public partial class frmResetPassword : Form
    {
        NhanVienBLL nhanVienBLL = new NhanVienBLL();

        public frmResetPassword()
        {
            InitializeComponent();
            this.Load += FrmResetPassword_Load;
        }

        private void FrmResetPassword_Load(object sender, EventArgs e)
        {
            cboMaNV.DataSource = nhanVienBLL.GetNhanVien();
            cboMaNV.DisplayMember = "MaNV";
            cboMaNV.ValueMember = "MaNV";

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();
            login.ShowDialog();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            List<String> listKH = new List<String>();
            listKH.Add("KH003");
            listKH.Add("KH004");

            NhanVien nhanVien = new NhanVien();
            nhanVien.EmailNV = tbAccountName.Text;
            NhanVien document = nhanVienBLL.CheckExistedAccountName(nhanVien);
            if (document == null)
            {
                MessageBox.Show("Tài khoản không tồn tại!");
            }
            else
            {
                Byte result = nhanVienBLL.ResetPassword(document, tbRepassword.Text, cboMaNV.SelectedValue.ToString());
                if (result == 1)
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin!");
                }
                else if (result == 2)
                {
                    MessageBox.Show("Xác thực mã nhân viên không hợp lệ!");
                }
                else
                {
                    MessageBox.Show("Đổi mật khẩu thành công!");
                }
            }
        }
    }
}
