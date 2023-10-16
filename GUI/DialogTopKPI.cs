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

namespace GUI
{
    public partial class DialogTopKPI : Form
    {
        public DialogTopKPI()
        {
            InitializeComponent();
            this.Load += DialogTopKPI_Load;
        }

        private void DialogTopKPI_Load(object sender, EventArgs e)
        {
            List<NhanVien> nhanViens = frmNhanVien.listTopKPI;
            lblTop1.Text = nhanViens[0].HotenNV.ToUpper().ToString();
            lblTop2.Text = nhanViens[1].HotenNV.ToUpper().ToString();
            lblTop3.Text = nhanViens[2].HotenNV.ToUpper().ToString();

            lblNum1.Text = nhanViens[0].KPI.ToString();
            lblNum2.Text = nhanViens[1].KPI.ToString();
            lblNum3.Text = nhanViens[2].KPI.ToString();

            if (nhanViens[0].Avatar != null)
                pbTop1.LoadAsync(nhanViens[0].Avatar);
            if (nhanViens[1].Avatar != null)
                pbTop2.LoadAsync(nhanViens[1].Avatar);
            if (nhanViens[2].Avatar != null)
                pbTop3.LoadAsync(nhanViens[2].Avatar);
        }
    }
}
