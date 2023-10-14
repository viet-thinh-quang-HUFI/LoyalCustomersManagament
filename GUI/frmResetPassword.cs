using BLL;
using System;
using System.Windows.Forms;

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
            String emailNV = tbAccountName.Text;
            String maNV = nhanVienBLL.CheckExistedAccountName(emailNV);
            if (maNV == null)
            {
                MessageBox.Show("Tài khoản không tồn tại!");
            }
            else
            {
                Byte result = nhanVienBLL.ResetPassword(maNV, tbRepassword.Text, cboMaNV.SelectedValue.ToString());
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
                    this.Close();
                    frmLogin login = new frmLogin();
                    login.ShowDialog();
                }
            }
        }
    }
}
