using BLL;
using DTO;
using MongoDB.Bson;
using MongoDB.Driver;
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
    public partial class frmHoaDon : Form
    {
        //DataTable data = new DataTable();
        SanPhamBLL sanPhamBLL = new SanPhamBLL();
        public frmHoaDon()
        {
            InitializeComponent();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = sanPhamBLL.GetALLSP();
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            //data.Columns.Add("MaSP");
            //data.Columns.Add("SoLuongMua");
            //data.Rows.Add("", "");
            //dataGridView2.DataSource = data;
        }

        private void btnCHoaDon_Click(object sender, EventArgs e)
        {
            String MaSP = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            String SoLuongMua = txtSoLuongMua.Text;
            //DataGridViewRow row = (DataGridViewRow)dataGridView2.Rows[0].Clone();
            //row.Cells["MaSP"].Value = MaSP;
            //row.Cells["SoLuongMua"].Value = SoLuongMua;
            //var index = dataGridView2.Rows.Add();
            //dataGridView2.Rows[index].Cells["MaSP"].Value = MaSP;
            //dataGridView2.Rows[index].Cells["SoLuongMua"].Value = SoLuongMua;
            //DataRow newrow = data.NewRow();
            //string[] strings = {MaSP, SoLuongMua};
            //newrow.ItemArray[3] = strings;
            //data.Rows.Add (newrow);
            dataGridView2.Rows.Add(MaSP, SoLuongMua);



            //for (int i = 1; i <= dataGridView1.Rows.Count - 1; i++)
            //{

            //    bool rowAlreadyExist = false;
            //    bool checkedCell = (bool)dataGridView1.Rows[i].Cells[7].Value;
            //    if (checkedCell == true)
            //    {
            //        DataGridViewRow row = dataGridView1.Rows[i];

            //        if (dataGridView2.Rows.Count != 0)
            //        {
            //            for (int j = 1; j <= dataGridView2.Rows.Count - 1; j++)
            //            {
            //                if (row.Cells[0].Value.ToString() == dataGridView2.Rows[j].Cells[1].Value.ToString())
            //                {
            //                    rowAlreadyExist = true;
            //                    break;
            //                }
            //            }


            //            if (rowAlreadyExist == false)
            //            {
            //                dataGridView2.Rows.Add(row.Cells[1].Value.ToString(),
            //                                       row.Cells[2].Value.ToString(),
            //                                       row.Cells[3].Value.ToString(),
            //                                       row.Cells[4].Value.ToString(),
            //                                       row.Cells[5].Value.ToString(),
            //                                       row.Cells[6].Value.ToString()
            //                                       );
            //            }
            //        }

            //        else
            //        {
            //            dataGridView2.Rows.Add(row.Cells[0].Value.ToString(),
            //                                       row.Cells[1].Value.ToString(),
            //                                       row.Cells[2].Value.ToString(),
            //                                       row.Cells[3].Value.ToString(),
            //                                       row.Cells[4].Value.ToString(),
            //                                       row.Cells[5].Value.ToString()
            //                                       );
            //        }
            //    }
            //}
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
