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
    public partial class Form5 : Form
    {
        int r = -1;
        bool isUpdate = false;

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            tb(false);
            btn(false);

            loadcb();
            loadData();

        }

        private void loadData()
        {
            var a = new DataClasses1DataContext();
            var b = a.SpareParts.Where(x => (x.Name.ToString().ToLower().Contains(tbsearch.Text.ToLower()) || x.PartNumber.ToString().ToLower().Contains(tbsearch.Text.ToLower()) || x.Category.Name.ToString().ToLower().Contains(tbsearch.Text.ToLower())) && x.DeletedAt == null).ToList();
            dgv.Rows.Clear();
            foreach (var i in b)
            {
                dgv.Rows.Add(i.Id, i.PartNumber, i.Category.Name, i.Name, i.MaxCapacityInRack);
            }
        }

        private void loadcb()
        {
            var a = new DataClasses1DataContext();
            var b = a.Categories.ToList();
            cbcate.Items.Clear();
            cbcate.DataSource = b;
            cbcate.ValueMember = "Id";
            cbcate.DisplayMember = "Name";
        }

        private void btn(bool v)
        {
            btnsave.Enabled = v;
            btncancel.Enabled = v;
            btninsert.Enabled = !v;
            btnupdate.Enabled = !v;
            btndelete.Enabled = !v;
        }

        private void tb(bool v)
        {
            tbname.Enabled = v;
            tbnumber.Enabled = v;
            cbcate.Enabled = v;
            nummax.Enabled = v;

        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            r = e.RowIndex;

            if (r != -1)
            {
                tbid.Text = dgv.Rows[r].Cells[0].Value.ToString();
                tbnumber.Text = dgv.Rows[r].Cells[1].Value.ToString();
                cbcate.Text = dgv.Rows[r].Cells[2].Value.ToString();
                tbname.Text = dgv.Rows[r].Cells[3].Value.ToString();
                nummax.Value = int.Parse(dgv.Rows[r].Cells[4].Value.ToString());
            }
        }

        private void tbsearch_TextChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btninsert_Click(object sender, EventArgs e)
        {
            isUpdate = false;
            r = -1;
            clear();
            tb(true);
            btn(true);
        }

        private void clear()
        {
            tbid.Clear();
            tbname.Clear();
            cbcate.Text = "";
            tbnumber.Clear();
            nummax.Value = 0;
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (r != -1)
            {
                isUpdate = true;
                tb(true);
                btn(true);
            }
            else
            {
                MessageBox.Show("please select a row");
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (r != -1)
            {
                if (MessageBox.Show("are you sure to delete this data?", "delete sparepart", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    var a = new DataClasses1DataContext();
                    var b = a.SpareParts.Where(x => x.Id == int.Parse(dgv.Rows[r].Cells[0].Value.ToString())).FirstOrDefault();
                    b.DeletedAt = DateTime.Now;
                    b.DeletedByUserId = Class1.userId;
                    a.SubmitChanges();
                    MessageBox.Show("delete success");
                    clear();
                    r = -1;
                    loadData();
                    
                }
            }
            else
            {
                MessageBox.Show("please select a row");
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            clear();
            r = -1;
            btn(false);
            tb(false);

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                if (isUpdate)
                {
                    var a = new DataClasses1DataContext();

                    if (a.SpareParts.Any(x => x.PartNumber == tbnumber.Text) && tbnumber.Text != dgv.Rows[r].Cells[1].Value.ToString())
                    {
                        MessageBox.Show("part number is already use");
                        return;
                    }

                    var b = a.SpareParts.Where(x => x.Id == int.Parse(dgv.Rows[r].Cells[0].Value.ToString())).FirstOrDefault();
                    b.PartNumber = tbnumber.Text;
                    b.Name = tbname.Text;
                    b.CategoryId = (int)cbcate.SelectedValue;
                    b.MaxCapacityInRack = (int)nummax.Value;
                    b.ModifiedAt = DateTime.Now;
                    b.ModifiedByUserId = Class1.userId;
                    a.SubmitChanges();
                    MessageBox.Show("update success");

                }
                else
                {
                    var a = new DataClasses1DataContext();
                    if (a.SpareParts.Any(x => x.PartNumber == tbnumber.Text))
                    {
                        MessageBox.Show("part number is already use");
                        return;
                    }
                    var b = new SparePart();
                    b.PartNumber = tbnumber.Text;
                    b.Name = tbname.Text;
                    b.CategoryId = (int)cbcate.SelectedValue;
                    b.MaxCapacityInRack = (int)nummax.Value;
                    b.CreatedAt = DateTime.Now;
                    b.CreatedByUserId = Class1.userId;
                    a.SpareParts.InsertOnSubmit(b);
                    a.SubmitChanges();
                    MessageBox.Show("insert success");

                }
                loadData();
                clear();
                r = -1;
                btn(false);
                tb(false);
            }
        }

        private bool validate()
        {
            if (string.IsNullOrEmpty(tbname.Text) || string.IsNullOrEmpty(tbnumber.Text))
            {
                MessageBox.Show("all must be filled");
                return false;
            }
            else if (cbcate.SelectedIndex == -1)
            {
                MessageBox.Show("invalid category");
                return false;
            }
            else if (nummax.Value <= 0)
            {
                MessageBox.Show("Max Capacity in Rack value must be a positive integer greater than zero");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
