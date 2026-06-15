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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            isScan(false);
        }

        private void isScan(bool v)
        {
            submit.Enabled = v;
            cancel.Enabled = v;

            cbrack.Enabled = v;
            numqty.Enabled = v;
            date.Enabled = v;
            scan.Enabled = !v;
            tbnumber.Enabled = !v;
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

        private void loadData(SparePart s)
        {
            clear();

            tbname.Text = s.Name;
            tbinfo.Text = Class1.name;
            loadCb(s.CategoryId, s.Id);
            
        }

        private void loadCb(int categoryId, int sparepartId)
        {

            var a = new DataClasses1DataContext();
            var b = a.Racks.Where(x => x.CategoryId == categoryId).Select(x => x.Id).ToList();
            var c = a.RackRows.Where(x => b.Contains(x.RackId)).Select(x => x.Id).ToList();
            var d = a.RackBins.Where(x => c.Contains(x.RackRowId)).OrderBy(x => x.BinCode).ToList();

            var e = new List<RackBin>(); 
            for (int i = 0; i < d.Count; i++)
            {
               
                if (d[i].SparePartId == sparepartId)
                {
                    d[i].BinCode = d[i].BinCode + " (Last Used - Current Qty: " + d[i].Capacity + ")";
                    e.Add(d[i]);
                }
            }
            RackBin s = d.OrderBy(x => x.Id).Where(x => x.EntryDate == null).FirstOrDefault();
            if (s != null)
            {
                s.BinCode = s.BinCode + " (Empty Bin - Recommended)";
                e.Add(s);
            }
            if (e.Count == 0)
            {
                e.Add(new RackBin()
                {
                    Id = 999,
                    BinCode = "no more space for this sparepart"
                });
                cbrack.Enabled = false;
            }
            else
            {
                cbrack.Enabled = true;
            }


                cbrack.DataSource = e.ToList();
            cbrack.ValueMember = "Id";
            cbrack.DisplayMember = "BinCode";

        }

        private void clear()
        {
            tbinfo.Text = "";
            tbname.Text = "";
            numqty.Value = 0;
            date.Value = DateTime.Now;
            cbrack.DataSource = null;
        }

        private void submit_Click(object sender, EventArgs e)
        {
            if (validasi())
            {
                var a = new DataClasses1DataContext();

                var b = a.RackBins.Where(x => x.Id == (int)cbrack.SelectedValue).FirstOrDefault();
                if (b.EntryDate != null)
                {
                    b.ModifiedAt = date.Value;
                    b.Capacity = b.Capacity + (int)numqty.Value;
                    a.SubmitChanges();
                }
                else
                {
                    b.SparePartId = a.SpareParts.Where(x => x.PartNumber == tbnumber.Text).FirstOrDefault().Id;
                    b.Capacity = (int)numqty.Value;
                    b.EntryDate = date.Value;
                    a.SubmitChanges();
                }
                var c = new InventoryLog();
                c.BinId = b.Id;
                c.SparePartId = a.SpareParts.Where(x => x.PartNumber == tbnumber.Text).FirstOrDefault().Id;
                c.TransactionType = "IN";
                c.TransactionDate = date.Value;
                c.Quantity = (int)numqty.Value;
                c.UserId = Class1.userId;
                a.InventoryLogs.InsertOnSubmit(c);
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

            if (cbrack.SelectedIndex == -1)
            {
                MessageBox.Show("invalid rack");
                return false;
            }

            if (numqty.Value <= 0)
            {
                MessageBox.Show("quantity must be greater than zero");
                return false;
            }

            var d = a.RackBins.Where(x => x.Id == (int)cbrack.SelectedValue).FirstOrDefault();
            if (d.EntryDate != null && numqty.Value > c.MaxCapacityInRack - d.Capacity)
            {
                MessageBox.Show("capacity is greater than max capacity in rack");
                return false;
            }

            if (d.EntryDate == null && numqty.Value > c.MaxCapacityInRack)
            { 
                MessageBox.Show("capacity is greater than max capacity in rack");
                return false;
            }


            if (DateTime.Now.AddHours(-4) > date.Value || date.Value > DateTime.Now)
            {
                MessageBox.Show("maximum transaction time is 4 hours from the current time");
                return false;
            }

            return true;


        }

        private void cancel_Click(object sender, EventArgs e)
        {
            clear();
            isScan(false);
        }
    }
}
