using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace x_Lookup_Lite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            bunifuFlatButton1.Click += new EventHandler(bunifuFlatButton1_Click);
            aTimer = new System.Windows.Forms.Timer();
            aTimer.Tick += new EventHandler(aTimer_Tick);
            aTimer.Interval = 1000;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            varGlob.operID = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            label10.Text = "Logged in as: " + varGlob.operID;
            varGlob.machineName = Environment.MachineName;
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = host.AddressList.First(a => a.AddressFamily == AddressFamily.InterNetwork); // ipv4
            varGlob.IPaddress = ipAddress.ToString();
            label11.Text = "Computer Name: " + varGlob.machineName;
            label12.Text = "IP Address: " + varGlob.IPaddress;

            if (!varGlob.IPaddress.StartsWith("10.101.10.") && !varGlob.IPaddress.StartsWith("10.101.18."))
            {
                radioButton68.Enabled = false;
                radioButton67.Checked = true;

                radioButton34.Enabled = false;
                radioButton35.Checked = true;

                radioButton39.Enabled = false;
                radioButton38.Checked = true;
            }


            try
            {
                //duplicated since MTV bs connect cant touch our shiot
                string xPath = @"\\mtv-va-fs05\data\temp\STIG\XLookup.dll";
                string xPath2 = @"\\mtv-va-fs05\data\Temp\STIG\XLookup.dll";
                if (File.Exists(xPath) || File.Exists(xPath2))
                {
                    string checkdate = File.ReadLines(xPath).First();
                    DateTime dt = Convert.ToDateTime(checkdate);
                    string checkdate2 = File.ReadLines(xPath2).First();
                    DateTime dt2 = Convert.ToDateTime(checkdate2);
                    //MessageBox.Show(dt.ToString());

                    if (dt < DateTime.Now || dt2 < DateTime.Now)
                    {

                        Application.Exit();

                    }

                }


                if (!File.Exists(xPath) || !File.Exists(xPath2))
                {
                    Application.Exit();

                }
            }

            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }

            //WindowState = System.Windows.Forms.FormWindowState.Maximized;
            panel1.Hide();
            panel4.Hide();
            panel6.Hide();
            panel22.Hide();
            dashboard_panel.Show();
            queueMonitorMTV01();
            queueMonitorMTV02();
            queueMonitorMTV03();
            queueMonitorMTV04();
            queueMonitorFP01();
            miscGrid();
            serverSpace();
            xTimer();
        }

        private void flatButton1_Click(object sender, EventArgs e)
        {
            flatButton1.Enabled = false;
            bunifuGauge1.Visible = false;
            bunifuGauge2.Visible = false;
            bunifuGauge3.Visible = false;
            bunifuGauge4.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            label17.Visible = false;
            label18.Visible = false;
            label19.Visible = false;
            label20.Visible = false;
            label21.Visible = false;
            label22.Visible = false;
            label23.Visible = false;
            label24.Visible = false;
            label25.Visible = false;
            dataGridView6.DataSource = null;
            dataGridView6.Columns.Clear();
            dataGridView6.Rows.Clear();
            dataGridView6.Refresh();
            dataGridView4.DataSource = null;
            dataGridView4.Columns.Clear();
            dataGridView4.Rows.Clear();
            dataGridView4.Refresh();
            dataGridView3.DataSource = null;
            dataGridView3.Columns.Clear();
            dataGridView3.Rows.Clear();
            dataGridView3.Refresh();
            dataGridView2.DataSource = null;
            dataGridView2.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            dataGridView15.DataSource = null;
            dataGridView15.Columns.Clear();
            dataGridView15.Rows.Clear();
            dataGridView15.Refresh();
            label74.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label14.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            panel1.Hide();
            panel4.Hide();
            panel6.Hide();
            panel22.Hide();
            dashboard_panel.Show();
            queueMonitorMTV01();
            queueMonitorMTV02();
            queueMonitorMTV03();
            queueMonitorMTV04();
            queueMonitorFP01();
            miscGrid();
            serverSpace();
            xTimer();
            flatButton1.Enabled = true;

        }

        //private void Dropdown1_onItemSelected(object sender, EventArgs e)
        //{

        //}

        private void flatButton2_Click(object sender, EventArgs e)
        {
            dashboard_panel.Hide();
            panel1.Hide();
            panel4.Hide();
            panel6.Show();
            panel22.Hide();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void flatButton3_Click(object sender, EventArgs e)
        {
            dashboard_panel.Hide();
            panel1.Hide();
            panel4.Show();
            panel6.Hide();
            panel22.Hide();
        }

        private void flatButton6_Click(object sender, EventArgs e)
        {
            dashboard_panel.Hide();
            panel1.Hide();
            panel4.Hide();
            panel6.Hide();
            panel22.Show();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            label13.Visible = false;
            label19.Visible = false;
            dataGridView7.Visible = false;
            chart1.Series["Enhance1"].Points.Clear();
            chart1.Series["FullPageOCR"].Points.Clear();
            chart1.Series["Enhance2"].Points.Clear();
            chart1.Series["Separation"].Points.Clear();
            chart1.Series["AutoIndex"].Points.Clear();
            chart1.Titles.Clear();
            dashboard_panel.Hide();
            panel4.Hide();
            panel6.Hide();
            panel22.Hide();
            panel1.Show();
            autoProc01();
        }


    }
}
