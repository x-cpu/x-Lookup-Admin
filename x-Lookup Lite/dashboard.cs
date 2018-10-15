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
using System.IO;

namespace x_Lookup_Lite
{
    public partial class Form1 : Form
    {
        public System.Windows.Forms.Timer aTimer = new System.Windows.Forms.Timer();

        private void queueMonitorMTV01()
        {

            try
            {
                if (!Directory.Exists(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd")))
                {
                    Directory.CreateDirectory(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            using (SqlConnection dataConnection2 = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.TurboscanNG_OCR1ConnectionString"].ToString()))


                try
                {

                    dataConnection2.Open();

                    SqlCommand selectCMD = new SqlCommand(@"use TurboscanNG_OCR1
                        select BatchLocation 'Location', ISNULL(SUM(T.Ready),0) Ready, ISNULL(SUM(T.InProcess),0) InProcess, ISNULL(SUM(T.Suspended),0) Suspended, ISNULL(SUM(T.AutoFail),0) AutoFail,
                        count(BatchID) 'Batches',
                        SUM(TotalImages) 'Images'
                        FROM(
                        select
                        distinct WFStep,
                        case 
                        WHEN BatchLocation = 1 then 'Scan'
						WHEN BatchLocation = 4 then 'Separation'
                        WHEN BatchLocation = 16 then 'OCR'
                        WHEN BatchLocation = 256 then 'Export'
                        WHEN BatchLocation = 0 then 'Clean'
                               ELSE NULL
                        END
                        AS BatchLocation,
						case WHEN BatchStatus = 1 then 1 Else 0 END as 'Ready',
												case WHEN BatchStatus = 2 then 1 Else 0 END as 'InProcess',
												case WHEN BatchStatus = 4 then 1 Else 0 END as 'Suspended',
												case WHEN BatchStatus = 8 then 1 Else 0 END as 'AutoFail',
                        BatchID, TotalImages
                        FROM Batches
                        --WHERE
                        --WFStep >= 0
                        --AND WFStep <= 12
                        --AND BatchStatus < 16
                        --and jobid = 11
                        Group by WFStep, BatchLocation, BatchStatus, BatchID, TotalImages) T
                        where BatchLocation <> 'NULL'
                        Group by WFStep, BatchLocation
                        Order by WFStep, 
						Case BatchLocation
							WHEN 'Scan' Then 1
							WHEN 'Separation' Then 2
							WHEN 'OCR' Then 3
							WHEN 'Export' Then 4
							WHEN 'Clean' Then 5
							END", dataConnection2);



                    DataTable dt = new DataTable();
                    SqlDataAdapter workAdapter = new SqlDataAdapter(selectCMD);
                    workAdapter.Fill(dt);

                    dataGridView15.DataSource = dt;
                    dataGridView15.ClearSelection();

                    label74.Visible = true;

                    for (int i = 0; i < dataGridView15.Rows.Count; i++)
                    {

                        DataGridViewRow row = dataGridView15.Rows[i];

                        varGlob.aValue = dataGridView15.Rows[i].Cells[4].Value == null ?
                                string.Empty : dataGridView15.Rows[i].Cells[4].Value.ToString();

                        int x = Int32.Parse(varGlob.aValue);

                        if (x >= 1)
                        {
                            dataGridView15.Rows[i].Cells[4].Style.BackColor = ColorTranslator.FromHtml("#113446");


                        }


                    }

                    //dataGridView14.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);


                }

                catch (Exception x)
                {
                    MessageBox.Show(x.Message);

                }
        





        }


        private void queueMonitorMTV02()
        {

            using (SqlConnection dataConnection3 = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.TurboscanNG_OCR2ConnectionString"].ToString()))


                try
                {

                    dataConnection3.Open();

                    SqlCommand selectCMD = new SqlCommand(@"use TurboscanNG_OCR2
                        select BatchLocation 'Location', ISNULL(SUM(T.Ready),0) Ready, ISNULL(SUM(T.InProcess),0) InProcess, ISNULL(SUM(T.Suspended),0) Suspended, ISNULL(SUM(T.AutoFail),0) AutoFail,
                        count(BatchID) 'Batches',
                        SUM(TotalImages) 'Images'
                        FROM(
                        select
                        distinct WFStep,
                        case 
                        WHEN BatchLocation = 1 then 'Scan'
						WHEN BatchLocation = 4 then 'Separation'
                        WHEN BatchLocation = 16 then 'OCR'
                        WHEN BatchLocation = 256 then 'Export'
                        WHEN BatchLocation = 0 then 'Clean'
                               ELSE NULL
                        END
                        AS BatchLocation,
						case WHEN BatchStatus = 1 then 1 Else 0 END as 'Ready',
												case WHEN BatchStatus = 2 then 1 Else 0 END as 'InProcess',
												case WHEN BatchStatus = 4 then 1 Else 0 END as 'Suspended',
												case WHEN BatchStatus = 8 then 1 Else 0 END as 'AutoFail',
                        BatchID, TotalImages
                        FROM Batches
                        --WHERE
                        --WFStep >= 0
                        --AND WFStep <= 12
                        --AND BatchStatus < 16
                        --and jobid = 11
                        Group by WFStep, BatchLocation, BatchStatus, BatchID, TotalImages) T
                        where BatchLocation <> 'NULL'
                        Group by WFStep, BatchLocation
                        Order by WFStep, 
						Case BatchLocation
							WHEN 'Scan' Then 1
							WHEN 'Separation' Then 2
							WHEN 'OCR' Then 3
							WHEN 'Export' Then 4
							WHEN 'Clean' Then 5
							END", dataConnection3);



                    DataTable dt = new DataTable();
                    SqlDataAdapter workAdapter = new SqlDataAdapter(selectCMD);
                    workAdapter.Fill(dt);

                    dataGridView1.DataSource = dt;
                    dataGridView1.ClearSelection();

                    label1.Visible = true;

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {

                        DataGridViewRow row = dataGridView1.Rows[i];

                        varGlob.aValue = dataGridView1.Rows[i].Cells[4].Value == null ?
                                string.Empty : dataGridView1.Rows[i].Cells[4].Value.ToString();

                        int x = Int32.Parse(varGlob.aValue);

                        if (x >= 1)
                        {
                            dataGridView1.Rows[i].Cells[4].Style.BackColor = ColorTranslator.FromHtml("#113446");


                        }


                    }

                    //dataGridView14.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);


                }

                catch (Exception x)
                {
                    MessageBox.Show(x.Message);

                }






        }


        private void queueMonitorMTV03()
        {

            using (SqlConnection dataConnection4 = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.TurboscanNG_OCR3ConnectionString"].ToString()))


                try
                {

                    dataConnection4.Open();

                    SqlCommand selectCMD = new SqlCommand(@"use TurboscanNG_OCR3
                        select BatchLocation 'Location', ISNULL(SUM(T.Ready),0) Ready, ISNULL(SUM(T.InProcess),0) InProcess, ISNULL(SUM(T.Suspended),0) Suspended, ISNULL(SUM(T.AutoFail),0) AutoFail,
                        count(BatchID) 'Batches',
                        SUM(TotalImages) 'Images'
                        FROM(
                        select
                        distinct WFStep,
                        case 
                        WHEN BatchLocation = 1 then 'Scan'
						WHEN BatchLocation = 4 then 'Separation'
                        WHEN BatchLocation = 16 then 'OCR'
                        WHEN BatchLocation = 256 then 'Export'
                        WHEN BatchLocation = 0 then 'Clean'
                               ELSE NULL
                        END
                        AS BatchLocation,
						case WHEN BatchStatus = 1 then 1 Else 0 END as 'Ready',
												case WHEN BatchStatus = 2 then 1 Else 0 END as 'InProcess',
												case WHEN BatchStatus = 4 then 1 Else 0 END as 'Suspended',
												case WHEN BatchStatus = 8 then 1 Else 0 END as 'AutoFail',
                        BatchID, TotalImages
                        FROM Batches
                        --WHERE
                        --WFStep >= 0
                        --AND WFStep <= 12
                        --AND BatchStatus < 16
                        --and jobid = 11
                        Group by WFStep, BatchLocation, BatchStatus, BatchID, TotalImages) T
                        where BatchLocation <> 'NULL'
                        Group by WFStep, BatchLocation
                        Order by WFStep, 
						Case BatchLocation
							WHEN 'Scan' Then 1
							WHEN 'Separation' Then 2
							WHEN 'OCR' Then 3
							WHEN 'Export' Then 4
							WHEN 'Clean' Then 5
							END", dataConnection4);



                    DataTable dt = new DataTable();
                    SqlDataAdapter workAdapter = new SqlDataAdapter(selectCMD);
                    workAdapter.Fill(dt);

                    dataGridView2.DataSource = dt;
                    dataGridView2.ClearSelection();

                    label2.Visible = true;

                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {

                        DataGridViewRow row = dataGridView2.Rows[i];

                        varGlob.aValue = dataGridView2.Rows[i].Cells[4].Value == null ?
                                string.Empty : dataGridView2.Rows[i].Cells[4].Value.ToString();

                        int x = Int32.Parse(varGlob.aValue);

                        if (x >= 1)
                        {
                            dataGridView2.Rows[i].Cells[4].Style.BackColor = ColorTranslator.FromHtml("#113446");


                        }


                    }

                    //dataGridView2.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);


                }

                catch (Exception x)
                {
                    MessageBox.Show(x.Message);

                }






        }

        private void queueMonitorMTV04()
        {

            using (SqlConnection dataConnection5 = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.TurboscanNG_OCR4ConnectionString"].ToString()))


                try
                {

                    dataConnection5.Open();

                    SqlCommand selectCMD = new SqlCommand(@"use TurboscanNG_OCR4
                        select BatchLocation 'Location', ISNULL(SUM(T.Ready),0) Ready, ISNULL(SUM(T.InProcess),0) InProcess, ISNULL(SUM(T.Suspended),0) Suspended, ISNULL(SUM(T.AutoFail),0) AutoFail,
                        count(BatchID) 'Batches',
                        SUM(TotalImages) 'Images'
                        FROM(
                        select
                        distinct WFStep,
                        case 
                        WHEN BatchLocation = 1 then 'Scan'
						WHEN BatchLocation = 4 then 'Separation'
                        WHEN BatchLocation = 16 then 'OCR'
                        WHEN BatchLocation = 256 then 'Export'
                        WHEN BatchLocation = 0 then 'Clean'
                               ELSE NULL
                        END
                        AS BatchLocation,
						case WHEN BatchStatus = 1 then 1 Else 0 END as 'Ready',
												case WHEN BatchStatus = 2 then 1 Else 0 END as 'InProcess',
												case WHEN BatchStatus = 4 then 1 Else 0 END as 'Suspended',
												case WHEN BatchStatus = 8 then 1 Else 0 END as 'AutoFail',
                        BatchID, TotalImages
                        FROM Batches
                        --WHERE
                        --WFStep >= 0
                        --AND WFStep <= 12
                        --AND BatchStatus < 16
                        --and jobid = 11
                        Group by WFStep, BatchLocation, BatchStatus, BatchID, TotalImages) T
                        where BatchLocation <> 'NULL'
                        Group by WFStep, BatchLocation
                        Order by WFStep, 
						Case BatchLocation
							WHEN 'Scan' Then 1
							WHEN 'Separation' Then 2
							WHEN 'OCR' Then 3
							WHEN 'Export' Then 4
							WHEN 'Clean' Then 5
							END", dataConnection5);



                    DataTable dt = new DataTable();
                    SqlDataAdapter workAdapter = new SqlDataAdapter(selectCMD);
                    workAdapter.Fill(dt);

                    dataGridView3.DataSource = dt;
                    dataGridView3.ClearSelection();

                    label3.Visible = true;
                    label4.Visible = true;
                    label14.Visible = true;
                    label5.Visible = true;


                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {

                        DataGridViewRow row = dataGridView3.Rows[i];

                        varGlob.aValue = dataGridView3.Rows[i].Cells[4].Value == null ?
                                string.Empty : dataGridView3.Rows[i].Cells[4].Value.ToString();

                        int x = Int32.Parse(varGlob.aValue);

                        if (x >= 1)
                        {
                            dataGridView3.Rows[i].Cells[4].Style.BackColor = ColorTranslator.FromHtml("#113446");


                        }


                    }

                    //dataGridView3.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);


                }

                catch (Exception x)
                {
                    MessageBox.Show(x.Message);

                }






        }

        private void queueMonitorFP01()
        {

            if (varGlob.IPaddress.StartsWith("10.101.10.") || varGlob.IPaddress.StartsWith("10.101.18."))
            {


                using (SqlConnection dataConnection6 = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.TurboscanNG_ATL_VA_0005ConnectionString"].ToString()))


                    try
                    {

                        dataConnection6.Open();

                        SqlCommand selectCMD = new SqlCommand(@"use [TurboscanNG_ATL-VA-0005]
                        select BatchLocation 'Location', ISNULL(SUM(T.Ready),0) Ready, ISNULL(SUM(T.InProcess),0) InProcess, ISNULL(SUM(T.Suspended),0) Suspended, ISNULL(SUM(T.AutoFail),0) AutoFail,
                        count(BatchID) 'Batches',
                        SUM(TotalImages) 'Images'
                        FROM(
                        select
                        distinct WFStep,
                        case 
                        WHEN BatchLocation = 1 then 'Scan'
						WHEN BatchLocation = 4 then 'Separation'
                        WHEN BatchLocation = 16 then 'OCR'
                        WHEN BatchLocation = 256 then 'Export'
                        WHEN BatchLocation = 0 then 'Clean'
                               ELSE NULL
                        END
                        AS BatchLocation,
						case WHEN BatchStatus = 1 then 1 Else 0 END as 'Ready',
												case WHEN BatchStatus = 2 then 1 Else 0 END as 'InProcess',
												case WHEN BatchStatus = 4 then 1 Else 0 END as 'Suspended',
												case WHEN BatchStatus = 8 then 1 Else 0 END as 'AutoFail',
                        BatchID, TotalImages
                        FROM Batches
                        WHERE
                        WFStep >= 0
                        AND WFStep <= 12
                        AND BatchStatus < 16
                        --and jobid = 11
                        Group by WFStep, BatchLocation, BatchStatus, BatchID, TotalImages) T
                        where BatchLocation <> 'NULL'
                        Group by WFStep, BatchLocation
                        Order by WFStep, 
						Case BatchLocation
							WHEN 'Scan' Then 1
							WHEN 'Separation' Then 2
							WHEN 'OCR' Then 3
							WHEN 'Export' Then 4
							WHEN 'Clean' Then 5
							END", dataConnection6);



                        DataTable dt = new DataTable();
                        SqlDataAdapter workAdapter = new SqlDataAdapter(selectCMD);
                        workAdapter.Fill(dt);

                        dataGridView4.DataSource = dt;
                        dataGridView4.ClearSelection();

                        label6.Visible = true;
                        label4.Visible = true;
                        label14.Visible = true;
                        label5.Visible = true;


                        for (int i = 0; i < dataGridView4.Rows.Count; i++)
                        {

                            DataGridViewRow row = dataGridView4.Rows[i];

                            varGlob.aValue = dataGridView4.Rows[i].Cells[4].Value == null ?
                                    string.Empty : dataGridView4.Rows[i].Cells[4].Value.ToString();

                            int x = Int32.Parse(varGlob.aValue);

                            if (x >= 1)
                            {
                                dataGridView4.Rows[i].Cells[4].Style.BackColor = ColorTranslator.FromHtml("#113446");


                            }


                        }

                        //dataGridView3.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);


                    }

                    catch (Exception x)
                    {
                        MessageBox.Show(x.Message);

                    }



            }


        }

        private void miscGrid()
        {

            try
            {
                if (!Directory.Exists(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd")))
                {
                    Directory.CreateDirectory(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            try
            {

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Property", typeof(string)));
                dt.Columns.Add(new DataColumn("Value", typeof(string)));
                dt.Columns.Add(new DataColumn("index", typeof(string)));

                var files = Directory.GetFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"));


                foreach (string file in files)
                {

                    if (file.EndsWith(".mtvMonthly"))
                    {

                        varGlob.mtvMonthly = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("MTV ICMHS Uploads", "", 10);
                        dt.Rows.Add("Monthly Upload Count", varGlob.mtvMonthly, 15);

                    }
               

                    if (file.EndsWith(".mtv"))
                    {

                        varGlob.mtv = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("Daily Upload Count", varGlob.mtv, 20);
                        dt.Rows.Add("", "", 21);

                    }

                    if (file.EndsWith(".mtvMonthlyRMC"))
                    {

                        varGlob.mtvMonthlyRMC = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("MTV RMC Uploads", "", 24);
                        dt.Rows.Add("Monthly Upload Count", varGlob.mtvMonthlyRMC, 26);

                    }

                    if (file.EndsWith(".mtvRMC"))
                    {

                        varGlob.mtvRMC = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("Daily Upload Count", varGlob.mtvRMC, 27);
                        dt.Rows.Add("", "", 28);

                    }

                    if (file.EndsWith(".docidTotal"))
                    {

                        varGlob.docidTotal = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("ICMHS DocID Stats", "", 76);
                        dt.Rows.Add("Total DocID Count", varGlob.docidTotal, 77);

                    }

                    if (file.EndsWith(".docidMTV"))
                    {

                        varGlob.docidMTV = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("MTV", varGlob.docidMTV, 78);

                    }


                    if (file.EndsWith(".docidFP"))
                    {

                        varGlob.docidFP = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("FP", varGlob.docidFP, 79);
                        //dt.Rows.Add("", "", 80);

                    }

                    if (file.EndsWith(".docidLON"))
                    {

                        varGlob.docidLON = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("LON", varGlob.docidLON, 81);
                        dt.Rows.Add("", "", 82);

                    }


                    if (file.EndsWith(".docidTotalRMC"))
                    {

                        varGlob.docidTotalRMC = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("RMC DocID Stats", "", 83);
                        dt.Rows.Add("Total DocID Count", varGlob.docidTotalRMC, 84);

                    }

                    if (file.EndsWith(".docidMTVRMC"))
                    {

                        varGlob.docidMTVRMC = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("MTV", varGlob.docidMTVRMC, 85);

                    }

                    if (file.EndsWith(".docidFPRMC"))
                    {

                        varGlob.docidFPRMC = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("FP", varGlob.docidFPRMC, 86);
                        //dt.Rows.Add("", "", 61);

                    }

                    if (file.EndsWith(".docidLONRMC"))
                    {

                        varGlob.docidLONRMC = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("LON", varGlob.docidLONRMC, 87);
                        dt.Rows.Add("", "", 88);

                    }


                    if (file.EndsWith(".couplerMTV"))
                    {

                        varGlob.couplerMTV = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("System Processes", "", 39);
                        dt.Rows.Add("ICMHS Coupler Count", varGlob.couplerMTV, 40);
                        dt.Rows.Add("", "", 48);

                    }

                    if (file.EndsWith(".couplerMTVRMC"))
                    {

                        varGlob.couplerMTVRMC = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("RMC Coupler Count", varGlob.couplerMTVRMC, 41);

                    }

                    if (file.EndsWith(".sms"))
                    {

                        varGlob.sms = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("FP Uploads", "", 70);
                        dt.Rows.Add("SMS Upload Count", varGlob.sms, 72);

                    }

                    if (file.EndsWith(".doma"))
                    {

                        varGlob.doma = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("DOMA Upload Count", varGlob.doma, 73);

                    }

                    if (file.EndsWith(".ncc"))
                    {

                        varGlob.ncc = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("NCC Upload Count", varGlob.ncc, 74);
                        dt.Rows.Add("", "", 75);

                    }

                    if (file.EndsWith(".CMPtoBEZIP"))
                    {
                        varGlob.CMPtoBEZIP = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("Pending CMP Zip Move", varGlob.CMPtoBEZIP, 43);

                    }

                    if (file.EndsWith(".CMPwaitingForPP"))
                    {
                        varGlob.CMPwaitingForPP = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("Pending CMP Post Ready", varGlob.CMPwaitingForPP, 44);

                    }

                    if (file.EndsWith(".CMPpendingSMS_ZIP"))
                    {
                        varGlob.CMPpendingSMS_ZIP = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("Pending SMS ZIP Upload", varGlob.CMPpendingSMS_ZIP, 45);

                    }

                    if (file.EndsWith(".CMPpendingSMS_ACK"))
                    {
                        varGlob.CMPpendingSMS_ACK = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("Pending SMS ACK Upload", varGlob.CMPpendingSMS_ACK, 46);

                    }

                    if (file.EndsWith(".CMPpendingDOMA_ZIP"))
                    {
                        varGlob.CMPpendingDOMA_ZIP = Int32.Parse(Path.GetFileNameWithoutExtension(file));
                        dt.Rows.Add("Pending DOMA ZIP Upload", varGlob.CMPpendingDOMA_ZIP, 47);

                    }

                }

                bool xMTVmonthly = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.mtvMonthly").Any();
                bool xMTVdaily = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.mtv").Any();
                bool xMTVmonthlyRMC = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.mtvMonthlyRMC").Any();
                bool xMTVdailyRMC = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.mtvRMC").Any();
                bool xdocIDtotal = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.docidTotal").Any();
                bool xdocidMTV = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.docidMTV").Any();
                bool xdocidFP = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.docidFP").Any();
                bool xdocidLON = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.docidLON").Any();
                bool xdocIDtotalRMC = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.docidTotalRMC").Any();
                bool xdocidMTVRMC = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.docidMTVRMC").Any();
                bool xdocidFPRMC = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.docidFPRMC").Any();
                bool xdocidLONRMC = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.docidLONRMC").Any();
                bool xmtvCoupler = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.couplerMTV").Any();
                bool xmtvCouplerRMC = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.couplerMTVRMC").Any();
                bool xsmsCount = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.sms").Any();
                bool xdomaCount = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.doma").Any();
                bool xnccCount = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.ncc").Any();

                bool xfileCountCMPtoBEZIP = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.CMPtoBEZIP").Any();
                bool xfileCountCMPwaitingForPP = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.CMPwaitingForPP").Any();
                bool xfileCountCMPpendingSMS_ACK = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.CMPpendingSMS_ACK").Any();
                bool xfileCountCMPpendingSMS_ZIP = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.CMPpendingSMS_ZIP").Any();
                bool xfileCountCMPpendingDOMA_ZIP = Directory.EnumerateFiles(@"\\mtv-va-fs05\data\temp\HRLY\" + DateTime.Now.ToString("yyyyMMdd"), "*.CMPpendingDOMA_ZIP").Any();

                if (!xMTVmonthly)
                {
                    dt.Rows.Add("MTV ICMHS Uploads", "0", 10);
                    dt.Rows.Add("Monthly Upload Count", 0, 15);
                }

                if (!xMTVdaily)
                {
                    dt.Rows.Add("Daily Upload Count", 0, 20);
                    dt.Rows.Add("", "", 25);
                }

                if (!xMTVmonthlyRMC)
                {
                    dt.Rows.Add("MTV RMC Uploads", "0", 24);
                    dt.Rows.Add("Monthly Upload Count", 0, 26);
                }

                if (!xMTVdailyRMC)
                {
                    dt.Rows.Add("Daily Upload Count", 0, 27);
                    dt.Rows.Add("", "", 28);
                }

                if (!xdocIDtotal)
                {
                    dt.Rows.Add("ICMHS DocID Stats", "", 76);
                    dt.Rows.Add("Total DocID Count", 0, 77);
                }

                if (!xdocidMTV)
                {
                    dt.Rows.Add("MTV", 0, 78);
                }

                if (!xdocidFP)
                {
                    dt.Rows.Add("FP", 0, 79);
                    dt.Rows.Add("", "", 80);
                }

                if (!xdocidLON)
                {
                    dt.Rows.Add("LON", 0, 81);
                    dt.Rows.Add("", "", 82);
                }

                if (!xdocIDtotal)
                {
                    dt.Rows.Add("RMC DocID Stats", "", 83);
                    dt.Rows.Add("Total DocID Count", 0, 84);
                }

                if (!xdocidMTV)
                {
                    dt.Rows.Add("MTV", 0, 85);
                }

                if (!xdocidFP)
                {
                    dt.Rows.Add("FP", 0, 86);
                    //dt.Rows.Add("", "", 61);
                }

                if (!xdocidLON)
                {
                    dt.Rows.Add("LON", 0, 87);
                    dt.Rows.Add("", "", 88);
                }


                if (!xmtvCoupler)
                {
                    dt.Rows.Add("System Processes", "", 39);
                    dt.Rows.Add("ICMHS Coupler Count", 0, 40);
                    dt.Rows.Add("", "", 48);
                }

                if (!xmtvCouplerRMC)
                {
                    dt.Rows.Add("RMC Coupler Count", 0, 41);
                }

                if (!xsmsCount)
                {
                    dt.Rows.Add("FP Uploads", "", 70);
                    dt.Rows.Add("SMS Upload Count", 0, 72);
                }

                if (!xdomaCount)
                {
                    dt.Rows.Add("DOMA Upload Count", 0, 73);
                }

                if (!xnccCount)
                {
                    dt.Rows.Add("NCC Upload Count", 0, 74);
                    dt.Rows.Add("", "", 75);
                }


                var fileCountMTVExportOCRready = (from file in Directory.EnumerateFiles(@"\\mtv-va-fs08\data\ExportOCR", "*.sem*", SearchOption.TopDirectoryOnly)
                                                select file).Count();

                var fileCountMTVExportOCRerr = (from file in Directory.EnumerateFiles(@"\\mtv-va-fs08\data\ExportOCR", "*.err", SearchOption.TopDirectoryOnly)
                                                select file).Count();

                //remove because MTV doesn't have network access to FP
                //var fileCountCMPtoBEZIP = (from file in Directory.EnumerateFiles(@"\\atl-va-fs08\data\ExportOCR\CMP_Trigger", "*.SEM_CMP", SearchOption.TopDirectoryOnly)
                //                                select file).Count();

                //var fileCountCMPwaitingForPP = (from file in Directory.EnumerateFiles(@"\\atl-va-fs08\data\ExportOCR\CMP_Trigger\ready4zip", "*.SEM_CMP", SearchOption.TopDirectoryOnly)
                //                           select file).Count();

                //var fileCountCMPpendingSMS_ACK = (from file in Directory.EnumerateFiles(@"\\atl-va-fs08\data\CMP\to_DMHS\ready", "*.ack", SearchOption.TopDirectoryOnly)
                //                                select file).Count();

                //var fileCountCMPpendingSMS_ZIP = (from file in Directory.EnumerateFiles(@"\\atl-va-fs08\data\CMP\to_DMHS\ready", "*.zip", SearchOption.TopDirectoryOnly)
                //                                  select file).Count();

                //var fileCountCMPpendingDOMA_ZIP = (from file in Directory.EnumerateFiles(@"\\atl-va-fs08\data\CMP\to_DOMA\ready", "*.zip", SearchOption.TopDirectoryOnly)
                //                                  select file).Count();

                var fileCountErrorMTV = 0;
                var fileCountErrorFOP = 0;
                var fileCountErrorLON = 0;


                if (Directory.Exists(@"\\mtv-va-fs05\data\ErrorReporter\" + DateTime.Now.ToString("yyyyMMdd")))
                {
                     fileCountErrorMTV = (from file in Directory.EnumerateFiles(@"\\mtv-va-fs05\data\ErrorReporter\" + DateTime.Now.ToString("yyyyMMdd"), "MTV*.*", SearchOption.TopDirectoryOnly)
                                             select file).Count();

                     fileCountErrorFOP = (from file in Directory.EnumerateFiles(@"\\mtv-va-fs05\data\ErrorReporter\" + DateTime.Now.ToString("yyyyMMdd"), "ATL*.*", SearchOption.TopDirectoryOnly)
                                             select file).Count();

                    fileCountErrorLON = (from file in Directory.EnumerateFiles(@"\\mtv-va-fs05\data\ErrorReporter\" + DateTime.Now.ToString("yyyyMMdd"), "LON*.*", SearchOption.TopDirectoryOnly)
                                         select file).Count();
                }



                dt.Rows.Add("Pending VBMS Ready Trig (MTV)", fileCountMTVExportOCRready, 41);
                dt.Rows.Add("Pending VBMS Error Trig (MTV)", fileCountMTVExportOCRerr, 42);

                if (!xfileCountCMPtoBEZIP)
                {
                    dt.Rows.Add("Pending CMP Zip Move", 0, 43);
                }

                if (!xfileCountCMPwaitingForPP)
                {
                    dt.Rows.Add("Pending CMP Post Ready", 0, 44);
                }

                if (!xfileCountCMPpendingSMS_ACK)
                {
                    dt.Rows.Add("Pending SMS ACK Upload", 0, 46);
                }
                
                if (!xfileCountCMPpendingSMS_ZIP)
                {
                    dt.Rows.Add("Pending SMS ZIP Upload", 0, 45);
                }
                
                if (!xfileCountCMPpendingDOMA_ZIP)
                {
                    dt.Rows.Add("Pending DOMA ZIP Upload", 0, 47);
                }
                

                dt.Rows.Add("Prod Floor Errors Reported", "", 89);
                dt.Rows.Add("MTV", fileCountErrorMTV, 90);
                dt.Rows.Add("FP", fileCountErrorFOP, 91);
                dt.Rows.Add("LON", fileCountErrorLON, 92);


                dataGridView6.AllowUserToAddRows = false;
                dataGridView6.DataSource = dt;
                dataGridView6.Sort(dataGridView6.Columns[2], System.ComponentModel.ListSortDirection.Ascending);
                dataGridView6.Columns[2].Visible = false;
                dataGridView6.ColumnHeadersVisible = false;
                dataGridView6.ClearSelection();




                if (varGlob.couplerMTV >= 20)
                {
                    dataGridView6.Rows[9].Cells[1].Style.BackColor = ColorTranslator.FromHtml("#113446");


                }

                if (varGlob.couplerMTVRMC >= 20)
                {
                    dataGridView6.Rows[10].Cells[1].Style.BackColor = ColorTranslator.FromHtml("#113446");


                }

                if (fileCountMTVExportOCRready >= 20)
                {
                    dataGridView6.Rows[11].Cells[1].Style.BackColor = ColorTranslator.FromHtml("#113446");


                }

                if (fileCountMTVExportOCRerr >= 20)
                {
                    dataGridView6.Rows[12].Cells[1].Style.BackColor = ColorTranslator.FromHtml("#113446");


                }

                if (varGlob.CMPtoBEZIP >= 20)
                {
                    dataGridView6.Rows[13].Cells[1].Style.BackColor = ColorTranslator.FromHtml("#113446");


                }

                if (varGlob.CMPwaitingForPP >= 20)
                {
                    dataGridView6.Rows[14].Cells[1].Style.BackColor = ColorTranslator.FromHtml("#113446");


                }

                if (varGlob.CMPpendingSMS_ZIP >= 20)
                {
                    dataGridView6.Rows[15].Cells[1].Style.BackColor = ColorTranslator.FromHtml("#113446");


                }

                if (varGlob.CMPpendingSMS_ACK >= 20)
                {
                    dataGridView6.Rows[16].Cells[1].Style.BackColor = ColorTranslator.FromHtml("#113446");


                }

                if (varGlob.CMPpendingDOMA_ZIP >= 20)
                {
                    dataGridView6.Rows[17].Cells[1].Style.BackColor = ColorTranslator.FromHtml("#113446");


                }

                if (fileCountErrorMTV >= 9)
                {
                    dataGridView6.Rows[37].Cells[1].Style.BackColor = ColorTranslator.FromHtml("#113446");


                }

                if (fileCountErrorFOP >= 9)
                {
                    dataGridView6.Rows[38].Cells[1].Style.BackColor = ColorTranslator.FromHtml("#113446");


                }

                if (fileCountErrorLON >= 9)
                {
                    dataGridView6.Rows[39].Cells[1].Style.BackColor = ColorTranslator.FromHtml("#113446");


                }

                for (int i = 0; i < dataGridView6.Rows.Count; i++)
                {

                    if (i == 0 || i == 4 || i == 8 || i == 19 || i==24 || i == 30 || i == 36)
                    {

                        ////DataGridViewCellStyle style = new DataGridViewCellStyle();
                        dataGridView6.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView6.Rows[i].DefaultCellStyle.Font = new Font("Microsoft YaHei UI", 8, FontStyle.Regular);
                        dataGridView6.Rows[i].Cells[0].Style.BackColor = ColorTranslator.FromHtml("#1c1f24");
                        dataGridView6.Rows[i].Cells[0].Style.ForeColor = ColorTranslator.FromHtml("#F2F2F2");
                        dataGridView6.Rows[i].Cells[1].Style.BackColor = ColorTranslator.FromHtml("#1c1f24");
                        ////style.Font = new Font(dataGridView13.Font, FontStyle.Bold);
                        ////dataGridView13.Rows[i].DefaultCellStyle = style;
                        



                    }

                    if (i != 0 || i != 4 || i != 8 || i != 19 || i != 24 || i != 30 || i != 36)
                    {

                        dataGridView6.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    }


                }

                dataGridView6.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);

            }

            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }


        }

        private void serverSpace()
        {

            if (varGlob.IPaddress.StartsWith("10.101.10.") || varGlob.IPaddress.StartsWith("10.101.18."))
            {

                using (SqlConnection dataConnection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.DVAConnectionStringFP"].ToString()))



                    try
                    {
                        dataConnection.Open();



                        //SqlCommand selectCMD = new SqlCommand(@"use DOCID
                        //    select 
                        //    CASE
                        //     When T.Box = 'ATL-VA-FSCN-1A' and T.Drive = 'J:\' then 'ATL-VA-FS05\DATA'
                        //     When T.Box = 'ATL-VA-FSCN-1B' and T.Drive = 'K:\' then 'ATL-VA-FS06\DATA'
                        //     When T.Box = 'ATL-VA-FSCN-1A' and T.Drive = 'L:\' then 'ATL-VA-FS07\DATA'
                        //     When T.Box = 'ATL-VA-FSCN-1B' and T.Drive = 'M:\' then 'ATL-VA-FS08\DATA'
                        //     END AS Location, 
                        //     T.TotalSize, T.FreeSpace, T.FreePercentage, T.LastUpdateDate	
                        //    FROM
                        //    (select top(4) *
                        //    from getDriveInfo
                        //    where (Box = 'ATL-VA-FSCN-1B' and Drive = 'K:\' or
                        //    Box = 'ATL-VA-FSCN-1B' and Drive = 'M:\' or
                        //    Box = 'ATL-VA-FSCN-1A' and Drive = 'L:\' or
                        //    Box = 'ATL-VA-FSCN-1A' and Drive = 'J:\')) T
                        //    order by Location", dataConnection);

                        SqlCommand selectCMD = new SqlCommand(@"use DOCID
                        select 
                        CASE
	                        When T.Box = 'ATL-VA-FSCN-1A' and T.Drive = 'J:\' then 'ATL-VA-FS05\DATA'
	                        When T.Box = 'ATL-VA-FSCN-1B' and T.Drive = 'K:\' then 'ATL-VA-FS06\DATA'
	                        When T.Box = 'ATL-VA-FSCN-1A' and T.Drive = 'L:\' then 'ATL-VA-FS07\DATA'
	                        When T.Box = 'ATL-VA-FSCN-1B' and T.Drive = 'M:\' then 'ATL-VA-FS08\DATA'
							When T.Box = 'MTV-VA-FSCN-1A' and T.Drive = 'S:\' then 'MTV-VA-FS01\DATA1'
							When T.Box = 'MTV-VA-FSCN-1A' and T.Drive = 'F:\' then 'MTV-VA-FS01\DATA'
							When T.Box = 'MTV-VA-FSCN-1B' and T.Drive = 'G:\' then 'MTV-VA-FS02\DATA'
							When T.Box = 'MTV-VA-FSCN-1B' and T.Drive = 'T:\' then 'MTV-VA-FS02\DATA1'
							When T.Box = 'MTV-VA-FSCN-1C' and T.Drive = 'U:\' then 'MTV-VA-FS03\DATA'
							When T.Box = 'MTV-VA-FSCN-1D' and T.Drive = 'E:\' then 'MTV-VA-FS04\DATA'
							When T.Box = 'MTV-VA-FSCN-1D' and T.Drive = 'V:\' then 'MTV-VA-FS04\DATA1'
							When T.Box = 'MTV-VA-FSCN-1A' and T.Drive = 'J:\' then 'MTV-VA-FS05\DATA'
							When T.Box = 'MTV-VA-FSCN-1A' and T.Drive = 'P:\' then 'MTV-VA-FS05\DATA2'
							When T.Box = 'MTV-VA-FSCN-1B' and T.Drive = 'K:\' then 'MTV-VA-FS06\DATA'
							When T.Box = 'MTV-VA-FSCN-1C' and T.Drive = 'L:\' then 'MTV-VA-FS07\DATA'
							When T.Box = 'MTV-VA-FSCN-1D' and T.Drive = 'M:\' then 'MTV-VA-FS08\DATA'
							When T.Box = 'MTV-VA-FSCN-1B' and T.Drive = 'N:\' then 'MTV-VA-FS09\DATA'
							When T.Box = 'MTV-VA-FSCN-1D' and T.Drive = 'O:\' then 'MTV-VA-FS10\DATA'
							When T.Box = 'MTV-VA-FSCN-1D' and T.Drive = 'R:\' then 'MTV-VA-FS10\DATA2'
							When T.Box = 'MTV-VA-FSCN-1F' and T.Drive = 'W:\' then 'MTV-VA-FS11\DATA'
	                        END AS Location, 
	                        T.TotalSize, T.FreeSpace, T.FreePercentage, T.LastUpdateDate	
                        FROM
                        (select top(20) *
                        from getDriveInfo
                        where (Box = 'ATL-VA-FSCN-1B' and Drive = 'K:\' or
                        Box = 'ATL-VA-FSCN-1B' and Drive = 'M:\' or
                        Box = 'ATL-VA-FSCN-1A' and Drive = 'L:\' or
						Box = 'MTV-VA-FSCN-1A' and Drive = 'S:\' or
						Box = 'MTV-VA-FSCN-1A' and Drive = 'F:\' or
						Box = 'MTV-VA-FSCN-1B' and Drive = 'G:\' or
						Box = 'MTV-VA-FSCN-1B' and Drive = 'T:\' or
						Box = 'MTV-VA-FSCN-1C' and Drive = 'U:\' or
						Box = 'MTV-VA-FSCN-1D' and Drive = 'E:\' or
						Box = 'MTV-VA-FSCN-1D' and Drive = 'V:\' or
						Box = 'MTV-VA-FSCN-1A' and Drive = 'J:\' or
						Box = 'MTV-VA-FSCN-1A' and Drive = 'P:\' or
						Box = 'MTV-VA-FSCN-1B' and Drive = 'K:\' or
						Box = 'MTV-VA-FSCN-1C' and Drive = 'L:\' or
						Box = 'MTV-VA-FSCN-1D' and Drive = 'M:\' or
						Box = 'MTV-VA-FSCN-1B' and Drive = 'N:\' or
						Box = 'MTV-VA-FSCN-1D' and Drive = 'O:\' or
						Box = 'MTV-VA-FSCN-1D' and Drive = 'R:\' or
						Box = 'MTV-VA-FSCN-1F' and Drive = 'W:\' or
                        Box = 'ATL-VA-FSCN-1A' and Drive = 'J:\')) T
                        order by Location", dataConnection);

                        selectCMD.CommandTimeout = 0;


                        using (SqlDataReader reader = selectCMD.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                string location = reader.GetValue(0).ToString();
                                string totalsize = reader.GetValue(1).ToString();
                                string freespace = reader.GetValue(2).ToString();
                                string freepercent = reader.GetValue(3).ToString();
                                string lastupdate = reader.GetValue(4).ToString();

                                totalsize = totalsize.Replace(" GB", "");
                                freespace = freespace.Replace(" GB", "");
                                freepercent = freepercent.Replace(" %", "");

                                varGlob.totalspace = Convert.ToDouble(totalsize);
                                varGlob.freespace = Convert.ToDouble(freespace);
                                varGlob.freepercent = Convert.ToDouble(freepercent);
                                //MessageBox.Show(varGlob.freepercent.ToString());

                                var num = (100 - varGlob.freepercent);
                                //MessageBox.Show(num.ToString());

                                if (location == @"ATL-VA-FS05\DATA")
                                {
                                    bunifuGauge1.Visible = true;
                                    bunifuGauge1.Value = Convert.ToInt32(num);
                                    label15.Visible = true;
                                    label16.Text = freespace + "GB free";
                                    label16.Visible = true;
                                }

                                if (location == @"ATL-VA-FS06\DATA")
                                {
                                    bunifuGauge2.Visible = true;
                                    bunifuGauge2.Value = Convert.ToInt32(num);
                                    label18.Visible = true;
                                    label17.Text = freespace + "GB free";
                                    label17.Visible = true;
                                }

                                if (location == @"ATL-VA-FS07\DATA")
                                {
                                    bunifuGauge3.Visible = true;
                                    bunifuGauge3.Value = Convert.ToInt32(num);
                                    label21.Visible = true;
                                    label20.Text = freespace + "GB free";
                                    label20.Visible = true;
                                }

                                if (location == @"ATL-VA-FS08\DATA")
                                {
                                    bunifuGauge4.Visible = true;
                                    bunifuGauge4.Value = Convert.ToInt32(num);
                                    label23.Visible = true;
                                    label22.Text = freespace + "GB free";
                                    label22.Visible = true;
                                }



                                if (location == @"MTV-VA-FS01\DATA")
                                {
                                    bunifuGauge8.Visible = true;
                                    bunifuGauge8.Value = Convert.ToInt32(num);
                                    label34.Visible = true;
                                    label33.Text = freespace + "GB free";
                                    label33.Visible = true;
                                }

                                if (location == @"MTV-VA-FS02\DATA")
                                {
                                    bunifuGauge7.Visible = true;
                                    bunifuGauge7.Value = Convert.ToInt32(num);
                                    label32.Visible = true;
                                    label31.Text = freespace + "GB free";
                                    label31.Visible = true;
                                }

                                if (location == @"MTV-VA-FS03\DATA")
                                {
                                    bunifuGauge6.Visible = true;
                                    bunifuGauge6.Value = Convert.ToInt32(num);
                                    label30.Visible = true;
                                    label29.Text = freespace + "GB free";
                                    label29.Visible = true;
                                }

                                if (location == @"MTV-VA-FS04\DATA")
                                {
                                    bunifuGauge5.Visible = true;
                                    bunifuGauge5.Value = Convert.ToInt32(num);
                                    label28.Visible = true;
                                    label27.Text = freespace + "GB free";
                                    label27.Visible = true;
                                }

                                if (location == @"MTV-VA-FS05\DATA")
                                {
                                    bunifuGauge10.Visible = true;
                                    bunifuGauge10.Value = Convert.ToInt32(num);
                                    label38.Visible = true;
                                    label37.Text = freespace + "GB free";
                                    label37.Visible = true;
                                }

                                if (location == @"MTV-VA-FS06\DATA")
                                {
                                    bunifuGauge9.Visible = true;
                                    bunifuGauge9.Value = Convert.ToInt32(num);
                                    label36.Visible = true;
                                    label35.Text = freespace + "GB free";
                                    label35.Visible = true;
                                }

                                if (location == @"MTV-VA-FS07\DATA")
                                {
                                    bunifuGauge16.Visible = true;
                                    bunifuGauge16.Value = Convert.ToInt32(num);
                                    label50.Visible = true;
                                    label49.Text = freespace + "GB free";
                                    label49.Visible = true;
                                }

                                if (location == @"MTV-VA-FS08\DATA")
                                {
                                    bunifuGauge15.Visible = true;
                                    bunifuGauge15.Value = Convert.ToInt32(num);
                                    label48.Visible = true;
                                    label47.Text = freespace + "GB free";
                                    label47.Visible = true;
                                }

                                if (location == @"MTV-VA-FS09\DATA")
                                {
                                    bunifuGauge14.Visible = true;
                                    bunifuGauge14.Value = Convert.ToInt32(num);
                                    label46.Visible = true;
                                    label45.Text = freespace + "GB free";
                                    label45.Visible = true;
                                }

                                if (location == @"MTV-VA-FS10\DATA")
                                {
                                    bunifuGauge13.Visible = true;
                                    bunifuGauge13.Value = Convert.ToInt32(num);
                                    label44.Visible = true;
                                    label43.Text = freespace + "GB free";
                                    label43.Visible = true;
                                }

                                if (location == @"MTV-VA-FS11\DATA")
                                {
                                    bunifuGauge12.Visible = true;
                                    bunifuGauge12.Value = Convert.ToInt32(num);
                                    label42.Visible = true;
                                    label41.Text = freespace + "GB free";
                                    label41.Visible = true;
                                }

                                if (location == @"MTV-VA-FS05\DATA2")
                                {
                                    bunifuGauge11.Visible = true;
                                    bunifuGauge11.Value = Convert.ToInt32(num);
                                    label40.Visible = true;
                                    label39.Text = freespace + "GB free";
                                    label39.Visible = true;
                                }


                            }

                            reader.Close();
                            label24.Visible = true;
                            label51.Visible = true;
                            label25.Visible = true;
                        }

                    }


                    catch
                    {




                    }

            }


        }

        public void xTimer()
        {
            try
            {
                varGlob.counter = 1800;
                aTimer.Start();

            }

            catch (Exception x)
            {
                MessageBox.Show(x.Message);


            }

        }

        public void aTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                var timespan = TimeSpan.FromSeconds(varGlob.counter);
                button19.Text = "{Refresh} " + timespan.ToString(@"mm\:ss");
                varGlob.counter--;
                if (varGlob.counter == 0)
                {
                    aTimer.Stop();
                    aTimer.Dispose();
                    flatButton1.Enabled = false;
                    bunifuGauge1.Visible = false;
                    bunifuGauge2.Visible = false;
                    bunifuGauge3.Visible = false;
                    bunifuGauge4.Visible = false;
                    bunifuGauge5.Visible = false;
                    bunifuGauge6.Visible = false;
                    bunifuGauge7.Visible = false;
                    bunifuGauge8.Visible = false;
                    bunifuGauge9.Visible = false;
                    bunifuGauge10.Visible = false;
                    bunifuGauge11.Visible = false;
                    bunifuGauge12.Visible = false;
                    bunifuGauge13.Visible = false;
                    bunifuGauge14.Visible = false;
                    bunifuGauge15.Visible = false;
                    bunifuGauge16.Visible = false;
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
                    label27.Visible = false;
                    label28.Visible = false;
                    label29.Visible = false;
                    label30.Visible = false;
                    label31.Visible = false;
                    label32.Visible = false;
                    label33.Visible = false;
                    label34.Visible = false;
                    label35.Visible = false;
                    label36.Visible = false;
                    label37.Visible = false;
                    label38.Visible = false;
                    label39.Visible = false;
                    label40.Visible = false;
                    label41.Visible = false;
                    label42.Visible = false;
                    label43.Visible = false;
                    label44.Visible = false;
                    label45.Visible = false;
                    label46.Visible = false;
                    label47.Visible = false;
                    label48.Visible = false;
                    label49.Visible = false;
                    label50.Visible = false;
                    label51.Visible = false;
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
            }

            catch (Exception x)
            {
                MessageBox.Show(x.Message);


            }
        }



        private void button19_Click(object sender, EventArgs e)
        {
            aTimer.Stop();
            aTimer.Dispose();
            flatButton1.Enabled = false;
            bunifuGauge1.Visible = false;
            bunifuGauge2.Visible = false;
            bunifuGauge3.Visible = false;
            bunifuGauge4.Visible = false;
            bunifuGauge5.Visible = false;
            bunifuGauge6.Visible = false;
            bunifuGauge7.Visible = false;
            bunifuGauge8.Visible = false;
            bunifuGauge9.Visible = false;
            bunifuGauge10.Visible = false;
            bunifuGauge11.Visible = false;
            bunifuGauge12.Visible = false;
            bunifuGauge13.Visible = false;
            bunifuGauge14.Visible = false;
            bunifuGauge15.Visible = false;
            bunifuGauge16.Visible = false;
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
            label27.Visible = false;
            label28.Visible = false;
            label29.Visible = false;
            label30.Visible = false;
            label31.Visible = false;
            label32.Visible = false;
            label33.Visible = false;
            label34.Visible = false;
            label35.Visible = false;
            label36.Visible = false;
            label37.Visible = false;
            label38.Visible = false;
            label39.Visible = false;
            label40.Visible = false;
            label41.Visible = false;
            label42.Visible = false;
            label43.Visible = false;
            label44.Visible = false;
            label45.Visible = false;
            label46.Visible = false;
            label47.Visible = false;
            label48.Visible = false;
            label49.Visible = false;
            label50.Visible = false;
            label51.Visible = false;
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

    }
}
