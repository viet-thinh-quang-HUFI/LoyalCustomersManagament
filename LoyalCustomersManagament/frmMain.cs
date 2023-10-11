using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoyalCustomersManagament
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            CustomMenu();
        }
        private void CustomMenu()
        {
            panel1.Visible = false;
            panel2.Visible = false;
        }
        private void hideMenu()
        {
            if (panel1.Visible == true)
                panel1.Visible = false;
            if (panel2.Visible == true)
                panel2.Visible = false;
        }
        private void showMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showMenu(panel1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenForm(new Form1());
            hideMenu();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            OpenForm(new Form2());
            hideMenu();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            OpenForm(new Form3());
            hideMenu();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            showMenu(panel2);
        }
        private void button9_Click(object sender, EventArgs e)
        {
            OpenForm(new Form2());
            hideMenu();
        }

        private Form activeForm = null;
        private void OpenForm(Form form)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelForm.Controls.Add(form);
            panelForm.Tag = form;
            form.BringToFront();
            form.Show();
        }
    }
}
