using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpareWMS
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void inventoryInboundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.MdiParent = this;
            f.Show();
        }

        private void inventoryOutboundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.MdiParent = this;
            f.Show();
        }

        private void masterDataSparepartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5();
            f.MdiParent = this;
            f.Show();
        }

        private void dashboardAnaliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 f = new Form6();
            f.MdiParent = this;
            f.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (Class1.role == "Supervisor")
            {
                inventoryOutboundToolStripMenuItem.Visible = false;
                inventoryInboundToolStripMenuItem.Visible = false;
                masterDataSparepartToolStripMenuItem.Visible = true;
                dashboardAnaliToolStripMenuItem.Visible = true;
            }
            else
            {
                inventoryOutboundToolStripMenuItem.Visible = true;
                inventoryInboundToolStripMenuItem.Visible = true;
                masterDataSparepartToolStripMenuItem.Visible = false;
                dashboardAnaliToolStripMenuItem.Visible = false;
            }
        }
    }
}
