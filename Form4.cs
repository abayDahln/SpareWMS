using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace SpareWMS
{
    public partial class Form4 : Form
    {
        int binId;
        public Form4()
        {
            InitializeComponent();
        }

     

        private void loadData(SparePart s)
        {
            clear();


            var a = new DataClasses1DataContext();

            var b = a.RackBins.Where(x => x.SparePartId == s.Id).OrderBy(x => x.EntryDate).FirstOrDefault();
            if (b == null)
            {
                MessageBox.Show("Sparepart not found");
                return;
            }
            binId = b.Id;
            tbname.Text = s.Name;
            tbrack.Text = b.BinCode;
            tbstock.Text = b.Capacity.ToString();
            tbinfo.Text = Class1.name;

        }

        private void clear()
        {
            tbinfo.Text = "";
            tbname.Text = "";
            numqty.Value = 0;
            date.Value = DateTime.Now;
            tbrack.Text = "";
            tbstock.Text = "";

        }


        private void isScan(bool v)
        {
            submit.Enabled = v;
            cancel.Enabled = v;

            tbrack.Enabled = v;
            tbstock.Enabled = v;
            numqty.Enabled = v;
            date.Enabled = v;
            scan.Enabled = !v;
            tbnumber.Enabled = !v;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            clear();
            isScan(false);
        }

        private void submit_Click(object sender, EventArgs e)
        {
            if (validasi())
            {

                var a = new DataClasses1DataContext();
                var b = a.SpareParts.Where(x => x.PartNumber == tbnumber.Text).FirstOrDefault();
                var c = a.RackBins.Where(x => x.Id == binId).FirstOrDefault();

                c.Capacity = c.Capacity - (int)numqty.Value;
                c.ModifiedAt = date.Value;
                a.SubmitChanges();

                var d = new InventoryLog();
                d.BinId = binId;
                d.SparePartId = b.Id;
                d.TransactionType = "OUT";
                d.Quantity = (int)numqty.Value;
                d.TransactionDate = date.Value;
                d.UserId = Class1.userId;

                a.InventoryLogs.InsertOnSubmit(d);
                a.SubmitChanges();
                MessageBox.Show("Transaction success");
                clear();
                isScan(false);



            }
        }

        private bool validasi()
        {
            var a = new DataClasses1DataContext();

            var c = a.SpareParts.Where(x => x.PartNumber == tbnumber.Text).FirstOrDefault();

            if (c == null)
            {
                MessageBox.Show("invalid spaarepart number");
                return false;
            }

            if (numqty.Value <= 0)
            {
                MessageBox.Show("quantity must be greater than zero");
                return false;
            }

            var d = a.RackBins.Where(x => x.Id == binId).FirstOrDefault();
            if (numqty.Value > d.Capacity)
            {
                MessageBox.Show("quantity can't more than stock");
                return false;
            }

            if (DateTime.Now.AddHours(-4) > date.Value || date.Value > DateTime.Now)
            {
                MessageBox.Show("maximum transaction time is 4 hours from the current time");
                return false;
            }

            return true;
        }

        private void scan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbnumber.Text))
            {
                MessageBox.Show("please input the part number");
                return;
            }

            var a = new DataClasses1DataContext();
            var b = a.SpareParts.Where(x => x.PartNumber == tbnumber.Text).FirstOrDefault();
            if (b == null)
            {
                MessageBox.Show("part number is not found");
                return;
            }
            else
            {
                isScan(true);
                loadData(b);
            }
        }
    }
}
