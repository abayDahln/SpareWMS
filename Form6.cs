using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SpareWMS
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            loadData(); 
        }

        public class listChart
        {
            public int inbound { get; set; }
            public int outbound { get; set; }
            public DateTime time { get; set; }
        }
        

        private void loadData()
        {
            var a = new DataClasses1DataContext();



            chart.Series.Clear();

            var list = new List<listChart>();

            for (int i = -7; i < 0; i++)
            {
                var v = a.InventoryLogs.Where(x => x.TransactionDate.Date == DateTime.Now.AddDays(i).Date).ToList();

                listChart lc = new listChart();
                lc.time = DateTime.Now.AddDays(i);
                lc.inbound = v.Where(x => x.TransactionType == "IN").Sum(x => x.Quantity);
                lc.outbound = v.Where(x => x.TransactionType == "OUT").Sum(x => x.Quantity);
                list.Add(lc);

            }

            Series s1 = new Series();
            Series s2 = new Series();
            s1.ChartType = SeriesChartType.Column;
            s1.Name = "Inbound";
            s2.Name = "Outbound";
            s1.Color = Color.Green;
            s2.Color = Color.Red;
            s1.IsValueShownAsLabel = true;
            s2.IsValueShownAsLabel = true;
            s1.ChartType = SeriesChartType.Column;
            s2.ChartType = SeriesChartType.Column;
            for (int i = 0; i < list.Count; i++)
            {
                s1.Points.AddXY(list[i].time.ToString("yyyy-MM-dd"), list[i].inbound);
                s2.Points.AddXY(list[i].time.ToString("yyyy-MM-dd"), list[i].outbound);
                
            }
            chart.Series.Add(s1);
            chart.Series.Add(s2);


            var b = a.InventoryLogs.ToList();
            foreach (var i in b)
            {
                dgvlog.Rows.Add(i.TransactionDate.ToString("yyyy-MM-dd HH:mm"), i.SparePart.Name, i.TransactionType, i.Quantity, i.User.FullName);
            }


            for (int i = 0; i < dgvlog.Rows.Count; i++)
            {
                if (dgvlog.Rows[i].Cells[2].Value.ToString() == "OUT")
                {
                    dgvlog.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                }
                else if (dgvlog.Rows[i].Cells[2].Value.ToString() == "IN")
                {
                    dgvlog.Rows[i].Cells[2].Style.ForeColor = Color.Green;
                }
            }

            var c = a.Racks.Select(x => new
            {
                x.RackCode,
                x.Category.Name,
                totalbin = a.RackRows.Where(y => y.RackId == x.Id).Select(y => y.RackBins.Count()).Sum(),
                used = a.RackRows.Where(y => y.RackId == x.Id).Select(y => y.RackBins.Where(z => z.EntryDate != null).Count()).Sum(),
                empty = a.RackRows.Where(y => y.RackId == x.Id).Select(y => y.RackBins.Where(z => z.EntryDate == null).Count()).Sum(),
        
                utilization = (double)(((double)a.RackRows.Where(y => y.RackId == x.Id).Select(y => y.RackBins.Where(z => z.EntryDate != null).Count()).Sum()) * 100 / ((double)a.RackRows.Where(y => y.RackId == x.Id).Select(y => y.RackBins.Count()).Sum()))
            }).ToList();

            foreach (var i in c)
            {
                dgvrack.Rows.Add(i.RackCode, i.Name, i.totalbin, i.used, i.empty, i.utilization.ToString("0.0") + "%");
            }

            for (int i = 0; i < dgvrack.Rows.Count; i++)
            {
                if (double.Parse(dgvrack.Rows[i].Cells[5].Value.ToString().Replace("%", "").Trim()) > 90)
                {
                    dgvrack.Rows[i].Cells[5].Style.ForeColor = Color.White;
                    dgvrack.Rows[i].Cells[5].Style.BackColor = Color.Red;
                }
            }

            

        }
     }
}
