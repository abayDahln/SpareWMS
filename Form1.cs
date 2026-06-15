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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            var a = new DataClasses1DataContext();
            var b = a.Users.Where(x => x.Username == tbusername.Text && x.Password == tbpassword.Text).FirstOrDefault();
            if (b != null)
            {
                Class1.userId = b.Id;
                Class1.name = b.FullName;
                Class1.role = b.Role;
                Form2 f = new Form2();
                f.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("invalid credentials");
            }
        }

    }
}
