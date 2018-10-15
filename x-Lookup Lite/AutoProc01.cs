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

namespace x_Lookup_Lite
{
    public partial class Form1 : Form
    {
        private void autoProc01()
        {
            //http://www.color-hex.com/color-palette/49081

            using (SqlConnection dataConnection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.TURBOSCANNG1ConnectionString"].ToString()))

                try
                {

                    dataConnection.Open();

                    SqlCommand selectCMD01 = new SqlCommand(@"select 
                        CASE
	                        When X.tsModule = 'Enhance' Then 'Enhance'
	                        When X.tsModule = 'FOCR' Then 'FOCR'
	                        When X.tsModule = 'Enhance2' Then 'Enhance2'
	                        When X.tsModule = '4' Then 'Separation'
	                        When X.tsModule = 'AutoIndex' Then 'AutoIndex'
                        END as TSModule, x.xDate, x.xHour, x.Images
                        FROM
                        (select distinct tsmodule, convert(date, t.timestamp, 101) as xDate, datepart(hh,t.timestamp) as xHour , 
			                        ISNULL(sum(t.userimagesprocessed), 0) as Images
			                        from [mtv-va-sql-4\p1].turboscanng1.dbo.ts_audit t
			                        left join [mtv-va-sql-4\p1].turboscanng1.dbo.Workstation w
			                        on t.WSID = w.WSID
			                        where t.timestamp > DATEADD(d,0,DATEDIFF(d,0,GETDATE()))
			                        and tsmodule in ('Enhance', 'FOCR', 'Enhance2', '4', 'AutoIndex')
			                        group by convert(date, t.timestamp, 101), datepart(hh,t.timestamp), t.TSModule
			                        having sum(userimagesprocessed) > 0) X
			                        order by x.xDate, x.xHour", dataConnection);


                    chart1.Legends[0].TitleBackColor = Color.FromKnownColor(KnownColor.Control);
                    chart1.Titles.Add("Mt. Vernon - TS AutoProcess Performance (1st Job)");
                    chart1.Titles[0].ForeColor = ColorTranslator.FromHtml("#e8e9e9");
                    chart1.Titles[0].Font = new Font("Microsoft YaHei UI", 8, FontStyle.Regular);
                    chart1.ChartAreas[0].ShadowOffset = 1;
                    chart1.ChartAreas[0].ShadowColor = ColorTranslator.FromHtml("#757575");
                    chart1.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
                    chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
                    chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
                    chart1.ChartAreas[0].AxisX.LineColor = ColorTranslator.FromHtml("#2f2f2f");
                    chart1.ChartAreas[0].AxisY.LineColor = ColorTranslator.FromHtml("#464646");
                    chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = ColorTranslator.FromHtml("#2f2f2f");
                    chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#464646");
                    //lighter than #a3a3a3 below
                    chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = ColorTranslator.FromHtml("#2f2f2f");
                    chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = ColorTranslator.FromHtml("#e7e7e7");
                    chart1.BackColor = ColorTranslator.FromHtml("#191919");


                    //setting for X and Y Axis titles
                    chart1.ChartAreas["ChartArea1"].AxisY.Title = "Number of Images";
                    chart1.ChartAreas["ChartArea1"].AxisY.TitleFont = new Font("Microsoft YaHei UI", 8, FontStyle.Regular);
                    chart1.ChartAreas["ChartArea1"].AxisX.Title = "Hour";
                    chart1.ChartAreas["ChartArea1"].AxisX.TitleFont = new Font("Microsoft YaHei UI", 8, FontStyle.Regular);
                    chart1.ChartAreas["ChartArea1"].AxisY.TitleForeColor = ColorTranslator.FromHtml("#e7e7e7");
                    chart1.ChartAreas["ChartArea1"].AxisX.TitleForeColor = ColorTranslator.FromHtml("#e7e7e7");

                    //setting for X Axis
                    chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                    chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new Font("Microsoft YaHei UI", 8, FontStyle.Regular); ;
                    chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.ForeColor = ColorTranslator.FromHtml("#e8e9e9");

                    chart1.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep90;

                    //changes chart background color
                    chart1.ChartAreas["ChartArea1"].BackColor = ColorTranslator.FromHtml("#191919");


                    //setting series       

                    chart1.Series["Enhance1"].LabelForeColor = ColorTranslator.FromHtml("#eceded");
                    chart1.Series["Enhance1"].IsVisibleInLegend = false;
                    chart1.Series["Enhance1"].IsValueShownAsLabel = true;
                    chart1.Series["Enhance1"].Font = new Font("Microsoft YaHei UI", 8, FontStyle.Regular);
                    chart1.Series["Enhance1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    chart1.Series["Enhance1"].Color = ColorTranslator.FromHtml("#325e83");
                    chart1.Series["Enhance1"].SmartLabelStyle.Enabled = false;
                    chart1.Series["Enhance1"].LabelAngle = -90;


                    chart1.Series["FullPageOCR"].LabelForeColor = ColorTranslator.FromHtml("#eceded");
                    chart1.Series["FullPageOCR"].IsVisibleInLegend = false;
                    chart1.Series["FullPageOCR"].IsValueShownAsLabel = true;
                    chart1.Series["FullPageOCR"].Font = new Font("Microsoft YaHei UI", 8, FontStyle.Regular);
                    chart1.Series["FullPageOCR"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    chart1.Series["FullPageOCR"].Color = ColorTranslator.FromHtml("#537ea8");
                    chart1.Series["FullPageOCR"].SmartLabelStyle.Enabled = false;
                    chart1.Series["FullPageOCR"].LabelAngle = -90;


                    chart1.Series["Enhance2"].LabelForeColor = ColorTranslator.FromHtml("#eceded");
                    chart1.Series["Enhance2"].IsVisibleInLegend = false;
                    chart1.Series["Enhance2"].IsValueShownAsLabel = true;
                    chart1.Series["Enhance2"].Font = new Font("Microsoft YaHei UI", 8, FontStyle.Regular);
                    chart1.Series["Enhance2"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    chart1.Series["Enhance2"].Color = ColorTranslator.FromHtml("#f9e625");
                    chart1.Series["Enhance2"].SmartLabelStyle.Enabled = false;
                    chart1.Series["Enhance2"].LabelAngle = -90;


                    chart1.Series["Separation"].LabelForeColor = ColorTranslator.FromHtml("#eceded");
                    chart1.Series["Separation"].IsVisibleInLegend = false;
                    chart1.Series["Separation"].IsValueShownAsLabel = true;
                    chart1.Series["Separation"].Font = new Font("Microsoft YaHei UI", 8, FontStyle.Regular);
                    chart1.Series["Separation"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    chart1.Series["Separation"].Color = ColorTranslator.FromHtml("#faf9fd");
                    chart1.Series["Separation"].SmartLabelStyle.Enabled = false;
                    chart1.Series["Separation"].LabelAngle = -90;


                    chart1.Series["AutoIndex"].LabelForeColor = ColorTranslator.FromHtml("#eceded");
                    chart1.Series["AutoIndex"].IsVisibleInLegend = false;
                    chart1.Series["AutoIndex"].IsValueShownAsLabel = true;
                    chart1.Series["AutoIndex"].Font = new Font("Microsoft YaHei UI", 8, FontStyle.Regular);
                    chart1.Series["AutoIndex"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    chart1.Series["AutoIndex"].Color = ColorTranslator.FromHtml("#da3a22");
                    chart1.Series["AutoIndex"].SmartLabelStyle.Enabled = false;
                    chart1.Series["AutoIndex"].LabelAngle = -90;


                    SqlDataReader dataReader = selectCMD01.ExecuteReader();
                    if (dataReader.HasRows)
                    {

                        while (dataReader.Read())
                        {
                            string TSModule = dataReader.GetString(0);

                            if (TSModule == "Enhance")
                            {
                                int hour = dataReader.GetInt32(2);
                                int images = dataReader.GetInt32(3);
                                chart1.Series["Enhance1"].Points.AddXY(hour, images);

                            }

                            if (TSModule == "FOCR")
                            {
                                int hour = dataReader.GetInt32(2);
                                int images = dataReader.GetInt32(3);
                                chart1.Series["FullPageOCR"].Points.AddXY(hour, images);

                            }

                            if (TSModule == "Enhance2")
                            {
                                int hour = dataReader.GetInt32(2);
                                int images = dataReader.GetInt32(3);
                                chart1.Series["Enhance2"].Points.AddXY(hour, images);

                            }

                            if (TSModule == "Separation")
                            {
                                int hour = dataReader.GetInt32(2);
                                int images = dataReader.GetInt32(3);
                                chart1.Series["Separation"].Points.AddXY(hour, images);

                            }

                            if (TSModule == "AutoIndex")
                            {
                                int hour = dataReader.GetInt32(2);
                                int images = dataReader.GetInt32(3);
                                chart1.Series["AutoIndex"].Points.AddXY(hour, images);

                            }


                        }
                    }


                    chart1.Visible = true;
                }



                catch (Exception x)
                {
                    MessageBox.Show(x.Message);

                }





        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            chart1.Visible = false;
            enhanceProd();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            chart1.Visible = false;
            fullPageOCRProd();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            chart1.Visible = false;
            enhance2Prod();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            chart1.Visible = false;
            separationProd();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            chart1.Visible = false;
            autoIndexProd();
        }

        private void enhanceProd()
        {

            using (SqlConnection dataConnection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.TURBOSCANNG1ConnectionString"].ToString()))

                try
                {

                    dataConnection.Open();

                    SqlCommand selectCMD01 = new SqlCommand(@"select distinct w.WSName Workstation, ISNULL(sum(t.userimagesprocessed), 0) as Images, tsmodule, len(w.WSName)
			            from [mtv-va-sql-4\p1].turboscanng1.dbo.ts_audit t
			            left join [mtv-va-sql-4\p1].turboscanng1.dbo.Workstation w
			            on t.WSID = w.WSID
			            where t.timestamp > DATEADD(d,0,DATEDIFF(d,0,GETDATE()))
			            and tsmodule in ('Enhance')
			            group by t.TSModule, w.WSName  
			            having sum(userimagesprocessed) > 0
			            order by len(w.WSName), w.WSName", dataConnection);

                    DataTable dt = new DataTable();
                    SqlDataAdapter workAdapter = new SqlDataAdapter(selectCMD01);
                    workAdapter.Fill(dt);

                    dataGridView7.DataSource = dt;
                    
                    dataGridView7.Columns[2].Visible = false;
                    dataGridView7.Columns[3].Visible = false;

                    

                    label13.Visible = true;
                    label19.Visible = true;
                    label19.Text = "Enhance 1";
                    dataGridView7.Visible = true;

                    dataGridView7.ClearSelection();

                }

                catch
                {

                }


        }

        private void fullPageOCRProd()
        {

            using (SqlConnection dataConnection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.TURBOSCANNG1ConnectionString"].ToString()))

                try
                {

                    dataConnection.Open();

                    SqlCommand selectCMD01 = new SqlCommand(@"select distinct w.WSName Workstation, ISNULL(sum(t.userimagesprocessed), 0) as Images, tsmodule, len(w.WSName)
			            from [mtv-va-sql-4\p1].turboscanng1.dbo.ts_audit t
			            left join [mtv-va-sql-4\p1].turboscanng1.dbo.Workstation w
			            on t.WSID = w.WSID
			            where t.timestamp > DATEADD(d,0,DATEDIFF(d,0,GETDATE()))
			            and tsmodule in ('FOCR')
			            group by t.TSModule, w.WSName  
			            having sum(userimagesprocessed) > 0
			            order by len(w.WSName), w.WSName", dataConnection);

                    DataTable dt = new DataTable();
                    SqlDataAdapter workAdapter = new SqlDataAdapter(selectCMD01);
                    workAdapter.Fill(dt);

                    dataGridView7.DataSource = dt;

                    dataGridView7.Columns[2].Visible = false;
                    dataGridView7.Columns[3].Visible = false;



                    label13.Visible = true;
                    label19.Visible = true;
                    label19.Text = "Full Page OCR";
                    dataGridView7.Visible = true;

                    dataGridView7.ClearSelection();

                }

                catch
                {

                }


        }


        private void enhance2Prod()
        {

            using (SqlConnection dataConnection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.TURBOSCANNG1ConnectionString"].ToString()))

                try
                {

                    dataConnection.Open();

                    SqlCommand selectCMD01 = new SqlCommand(@"select distinct w.WSName Workstation, ISNULL(sum(t.userimagesprocessed), 0) as Images, tsmodule, len(w.WSName)
			            from [mtv-va-sql-4\p1].turboscanng1.dbo.ts_audit t
			            left join [mtv-va-sql-4\p1].turboscanng1.dbo.Workstation w
			            on t.WSID = w.WSID
			            where t.timestamp > DATEADD(d,0,DATEDIFF(d,0,GETDATE()))
			            and tsmodule in ('Enhance2')
			            group by t.TSModule, w.WSName  
			            having sum(userimagesprocessed) > 0
			            order by len(w.WSName), w.WSName", dataConnection);

                    DataTable dt = new DataTable();
                    SqlDataAdapter workAdapter = new SqlDataAdapter(selectCMD01);
                    workAdapter.Fill(dt);

                    dataGridView7.DataSource = dt;

                    dataGridView7.Columns[2].Visible = false;
                    dataGridView7.Columns[3].Visible = false;



                    label13.Visible = true;
                    label19.Visible = true;
                    label19.Text = "Enhance 2";
                    dataGridView7.Visible = true;

                    dataGridView7.ClearSelection();

                }

                catch
                {

                }


        }

        private void separationProd()
        {

            using (SqlConnection dataConnection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.TURBOSCANNG1ConnectionString"].ToString()))

                try
                {

                    dataConnection.Open();

                    SqlCommand selectCMD01 = new SqlCommand(@"select distinct w.WSName Workstation, ISNULL(sum(t.userimagesprocessed), 0) as Images, tsmodule, len(w.WSName)
			            from [mtv-va-sql-4\p1].turboscanng1.dbo.ts_audit t
			            left join [mtv-va-sql-4\p1].turboscanng1.dbo.Workstation w
			            on t.WSID = w.WSID
			            where t.timestamp > DATEADD(d,0,DATEDIFF(d,0,GETDATE()))
			            and tsmodule in ('4')
			            group by t.TSModule, w.WSName  
			            having sum(userimagesprocessed) > 0
			            order by len(w.WSName), w.WSName", dataConnection);

                    DataTable dt = new DataTable();
                    SqlDataAdapter workAdapter = new SqlDataAdapter(selectCMD01);
                    workAdapter.Fill(dt);

                    dataGridView7.DataSource = dt;

                    dataGridView7.Columns[2].Visible = false;
                    dataGridView7.Columns[3].Visible = false;



                    label13.Visible = true;
                    label19.Visible = true;
                    label19.Text = "Separation";
                    dataGridView7.Visible = true;

                    dataGridView7.ClearSelection();

                }

                catch
                {

                }


        }

        private void autoIndexProd()
        {

            using (SqlConnection dataConnection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.TURBOSCANNG1ConnectionString"].ToString()))

                try
                {

                    dataConnection.Open();

                    SqlCommand selectCMD01 = new SqlCommand(@"select distinct w.WSName Workstation, ISNULL(sum(t.userimagesprocessed), 0) as Images, tsmodule, len(w.WSName)
			            from [mtv-va-sql-4\p1].turboscanng1.dbo.ts_audit t
			            left join [mtv-va-sql-4\p1].turboscanng1.dbo.Workstation w
			            on t.WSID = w.WSID
			            where t.timestamp > DATEADD(d,0,DATEDIFF(d,0,GETDATE()))
			            and tsmodule in ('AutoIndex')
			            group by t.TSModule, w.WSName  
			            having sum(userimagesprocessed) > 0
			            order by len(w.WSName), w.WSName", dataConnection);

                    DataTable dt = new DataTable();
                    SqlDataAdapter workAdapter = new SqlDataAdapter(selectCMD01);
                    workAdapter.Fill(dt);

                    dataGridView7.DataSource = dt;

                    dataGridView7.Columns[2].Visible = false;
                    dataGridView7.Columns[3].Visible = false;



                    label13.Visible = true;
                    label19.Visible = true;
                    label19.Text = "AutoIndex";
                    dataGridView7.Visible = true;

                    dataGridView7.ClearSelection();

                }

                catch
                {

                }


        }


    }
}
