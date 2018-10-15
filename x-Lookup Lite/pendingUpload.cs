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
        private void pendingUpload()
        {

            if (radioButton68.Checked)
            {

                using (SqlConnection dataConnection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.DVAConnectionStringFP"].ToString()))

                using (SqlConnection dataConnection2 = new SqlConnection(
                        ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.TurboscanNG_ATL_VA_0005ConnectionString"].ToString()))


                    try
                    {
                        dataConnection.Open();
                        dataConnection2.Open();


                        SqlCommand selectCMD = new SqlCommand(@"select distinct kbatch Batchname, RMN, sum(images) TotalImages, min(releasedate) TSExport1stJobdate from Stats 
                        where exists (select * from document where ftpstime is null and comments is null 
                        and pbatch like '02%' and stats.kbatch = document.PBatch) 
                        group by kbatch, RMN 
                        order by TSExport1stJobdate", dataConnection);


                        SqlCommand selectCMD01 = new SqlCommand(@"select Batchname, 
                        case
                        WHEN BatchLocation = 1 then 'Scan'
                        WHEN BatchLocation = 16 then 'OCR'
                        WHEN BatchLocation = 256 then 'Export'
                        WHEN BatchLocation = 0 then 'Clean'
                        END
                        AS BatchLocation, 
                        case
                        WHEN BatchStatus = 1 then 'Ready'
                        WHEN BatchStatus = 2 then 'In Progress'
                        WHEN BatchStatus = 8 then 'Auto-Fail'
                        END
                        AS BatchStatus, (DATEADD(hh, -4, DATEADD(ss, TimeStamp, '01/01/1970'))) TSImportDate 
                        from Batches where batchname = @batchname", dataConnection2);

                        selectCMD01.Parameters.AddWithValue("@batchname", SqlDbType.VarChar);


                        SqlCommand selectCMD02 = new SqlCommand(@"select count(*) from Batches where batchname = @batchname", dataConnection2);
                        selectCMD02.Parameters.AddWithValue("@batchname", SqlDbType.VarChar);



                        DataTable dt = new DataTable();
                        SqlDataAdapter workAdapter = new SqlDataAdapter(selectCMD);
                        workAdapter.Fill(dt);

                        dt.Columns.Add("BatchLocation", typeof(String));
                        dt.Columns.Add("BatchStatus", typeof(String));
                        dt.Columns.Add("TSImportDate", typeof(String));
                        dt.Columns.Add("OCR POD", typeof(String));


                        if (dt.Rows.Count > 0)
                        {
                            label274.Visible = true;
                            label274.Text = "Total Batches: ";
                            label275.Visible = true;
                            label275.Text = dt.Rows.Count.ToString();


                        }


                        for (int i = 1; i < dt.Rows.Count; i++)
                        {


                            DataRow myRow = dt.Rows[i];

                            selectCMD02.Parameters["@batchname"].Value = dt.Rows[i][0];
                            Int32 countPOD1batches = (Int32)selectCMD02.ExecuteScalar();


                            if (countPOD1batches >= 1)
                            {


                                selectCMD01.Parameters["@batchname"].Value = dt.Rows[i][0];
                                SqlDataReader dataReader = selectCMD01.ExecuteReader();

                                if (dataReader.Read())
                                {

                                    dt.Rows[i]["BatchLocation"] = dataReader[1];
                                    dt.Rows[i]["BatchStatus"] = dataReader[2];
                                    dt.Rows[i]["TSImportDate"] = dataReader[3];
                                    dt.Rows[i]["OCR POD"] = "POD 1 - ATL-VA-TSCN110";

                                    dataReader.Close();
                                }



                            }


                            

                        }






                        dataGridView5.DataSource = dt;



                        //dataGridView1.DataSource = dt;




                    }

                    catch
                    {



                    }


            }


            if (radioButton67.Checked)
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult dialogResult = MessageBox.Show("Results make take about 5 min; continue?", "Confirmation", buttons, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {

                    using (SqlConnection dataConnection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.DVAConnectionStringMTV"].ToString()))

                    using (SqlConnection dataConnection2 = new SqlConnection(
                            ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.TurboscanNG_OCR1ConnectionString"].ToString()))

                    using (SqlConnection dataConnection3 = new SqlConnection(
                            ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.TurboscanNG_OCR2ConnectionString"].ToString()))

                    using (SqlConnection dataConnection4 = new SqlConnection(
                            ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.TurboscanNG_OCR3ConnectionString"].ToString()))

                    using (SqlConnection dataConnection5 = new SqlConnection(
                            ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.TurboscanNG_OCR4ConnectionString"].ToString()))

                        try
                        {
                            dataConnection.Open();
                            dataConnection2.Open();
                            dataConnection3.Open();
                            dataConnection4.Open();
                            dataConnection5.Open();


                            SqlCommand selectCMD = new SqlCommand(@"select distinct kbatch Batchname, RMN, sum(images) TotalImages, min(releasedate) TSExport1stJobdate from Stats 
                        where exists (select * from document where ftpstime is null and comments is null 
                        and pbatch like '02%' and stats.kbatch = document.PBatch and imagedatetime > '2017-01-01') 
						and releasedate > '2017-01-01'
                        group by kbatch, RMN 
                        order by TSExport1stJobdate", dataConnection);


                            SqlCommand selectCMD01 = new SqlCommand(@"select Batchname, 
                        case
                        WHEN BatchLocation = 1 then 'Scan'
                        WHEN BatchLocation = 16 then 'OCR'
                        WHEN BatchLocation = 256 then 'Export'
                        WHEN BatchLocation = 0 then 'Clean'
                        END
                        AS BatchLocation, 
                        case
                        WHEN BatchStatus = 1 then 'Ready'
                        WHEN BatchStatus = 2 then 'In Progress'
                        WHEN BatchStatus = 8 then 'Auto-Fail'
                        END
                        AS BatchStatus, (DATEADD(hh, -4, DATEADD(ss, TimeStamp, '01/01/1970'))) TSImportDate 
                        from Batches where batchname = @batchname", dataConnection2);

                            selectCMD01.Parameters.AddWithValue("@batchname", SqlDbType.VarChar);


                            SqlCommand selectCMD02 = new SqlCommand(@"select count(*) from Batches where batchname = @batchname", dataConnection2);
                            selectCMD02.Parameters.AddWithValue("@batchname", SqlDbType.VarChar);


                            SqlCommand selectCMD05 = new SqlCommand(@"select Batchname, 
                        case
                        WHEN BatchLocation = 1 then 'Scan'
                        WHEN BatchLocation = 16 then 'OCR'
                        WHEN BatchLocation = 256 then 'Export'
                        WHEN BatchLocation = 0 then 'Clean'
                        END
                        AS BatchLocation, 
                        case
                        WHEN BatchStatus = 1 then 'Ready'
                        WHEN BatchStatus = 2 then 'In Progress'
                        WHEN BatchStatus = 8 then 'Auto-Fail'
                        END
                        AS BatchStatus, (DATEADD(hh, -4, DATEADD(ss, TimeStamp, '01/01/1970'))) TSImportDate 
                        from Batches where batchname = @batchname", dataConnection3);

                            selectCMD05.Parameters.AddWithValue("@batchname", SqlDbType.VarChar);

                            SqlCommand selectCMD03 = new SqlCommand(@"select count(*) from Batches where batchname = @batchname", dataConnection3);
                            selectCMD03.Parameters.AddWithValue("@batchname", SqlDbType.VarChar);


                            SqlCommand selectCMD06 = new SqlCommand(@"select Batchname, 
                        case
                        WHEN BatchLocation = 1 then 'Scan'
                        WHEN BatchLocation = 16 then 'OCR'
                        WHEN BatchLocation = 256 then 'Export'
                        WHEN BatchLocation = 0 then 'Clean'
                        END
                        AS BatchLocation, 
                        case
                        WHEN BatchStatus = 1 then 'Ready'
                        WHEN BatchStatus = 2 then 'In Progress'
                        WHEN BatchStatus = 8 then 'Auto-Fail'
                        END
                        AS BatchStatus, (DATEADD(hh, -4, DATEADD(ss, TimeStamp, '01/01/1970'))) TSImportDate 
                        from Batches where batchname = @batchname", dataConnection4);

                            selectCMD06.Parameters.AddWithValue("@batchname", SqlDbType.VarChar);


                            SqlCommand selectCMD04 = new SqlCommand(@"select count(*) from Batches where batchname = @batchname", dataConnection4);
                            selectCMD04.Parameters.AddWithValue("@batchname", SqlDbType.VarChar);

                            SqlCommand selectCMD06A = new SqlCommand(@"select Batchname, 
                        case
                        WHEN BatchLocation = 1 then 'Scan'
                        WHEN BatchLocation = 16 then 'OCR'
                        WHEN BatchLocation = 256 then 'Export'
                        WHEN BatchLocation = 0 then 'Clean'
                        END
                        AS BatchLocation, 
                        case
                        WHEN BatchStatus = 1 then 'Ready'
                        WHEN BatchStatus = 2 then 'In Progress'
                        WHEN BatchStatus = 8 then 'Auto-Fail'
                        END
                        AS BatchStatus, (DATEADD(hh, -4, DATEADD(ss, TimeStamp, '01/01/1970'))) TSImportDate 
                        from Batches where batchname = @batchname", dataConnection5);

                            selectCMD06A.Parameters.AddWithValue("@batchname", SqlDbType.VarChar);


                            SqlCommand selectCMD05A = new SqlCommand(@"select count(*) from Batches where batchname = @batchname", dataConnection5);
                            selectCMD05A.Parameters.AddWithValue("@batchname", SqlDbType.VarChar);

                            DataTable dt = new DataTable();
                            SqlDataAdapter workAdapter = new SqlDataAdapter(selectCMD);
                            workAdapter.Fill(dt);

                            dt.Columns.Add("BatchLocation", typeof(String));
                            dt.Columns.Add("BatchStatus", typeof(String));
                            dt.Columns.Add("TSImportDate", typeof(String));
                            dt.Columns.Add("OCR POD", typeof(String));

                            if (dt.Rows.Count > 0)
                            {
                                label274.Visible = true;
                                label274.Text = "Total Batches: ";
                                label275.Visible = true;
                                label275.Text = dt.Rows.Count.ToString();


                            }


                            for (int i = 1; i < dt.Rows.Count; i++)
                            {


                                DataRow myRow = dt.Rows[i];

                                selectCMD02.Parameters["@batchname"].Value = dt.Rows[i][0];
                                Int32 countPOD1batches = (Int32)selectCMD02.ExecuteScalar();

                                selectCMD03.Parameters["@batchname"].Value = dt.Rows[i][0];
                                Int32 countPOD2batches = (Int32)selectCMD03.ExecuteScalar();

                                selectCMD04.Parameters["@batchname"].Value = dt.Rows[i][0];
                                Int32 countPOD3batches = (Int32)selectCMD04.ExecuteScalar();

                                selectCMD05A.Parameters["@batchname"].Value = dt.Rows[i][0];
                                Int32 countPOD4batches = (Int32)selectCMD05A.ExecuteScalar();

                                if (countPOD1batches >= 1)
                                {


                                    selectCMD01.Parameters["@batchname"].Value = dt.Rows[i][0];
                                    SqlDataReader dataReader = selectCMD01.ExecuteReader();

                                    if (dataReader.Read())
                                    {

                                        dt.Rows[i]["BatchLocation"] = dataReader[1];
                                        dt.Rows[i]["BatchStatus"] = dataReader[2];
                                        dt.Rows[i]["TSImportDate"] = dataReader[3];
                                        dt.Rows[i]["OCR POD"] = "POD 1 - MTV-VA-TSCN101";

                                        dataReader.Close();
                                    }



                                }




                                if (countPOD2batches >= 1)
                                {


                                    selectCMD05.Parameters["@batchname"].Value = dt.Rows[i][0];
                                    SqlDataReader dataReader = selectCMD05.ExecuteReader();

                                    if (dataReader.Read())
                                    {

                                        dt.Rows[i]["BatchLocation"] = dataReader[1];
                                        dt.Rows[i]["BatchStatus"] = dataReader[2];
                                        dt.Rows[i]["TSImportDate"] = dataReader[3];
                                        dt.Rows[i]["OCR POD"] = "POD 2 - MTV-VA-TSCN111";

                                        dataReader.Close();
                                    }



                                }




                                if (countPOD3batches >= 1)
                                {


                                    selectCMD06.Parameters["@batchname"].Value = dt.Rows[i][0];
                                    SqlDataReader dataReader = selectCMD06.ExecuteReader();

                                    if (dataReader.Read())
                                    {

                                        dt.Rows[i]["BatchLocation"] = dataReader[1];
                                        dt.Rows[i]["BatchStatus"] = dataReader[2];
                                        dt.Rows[i]["TSImportDate"] = dataReader[3];
                                        dt.Rows[i]["OCR POD"] = "POD 3 - MTV-VA-TSCN121";

                                        dataReader.Close();
                                    }



                                }


                                if (countPOD4batches >= 1)
                                {


                                    selectCMD06A.Parameters["@batchname"].Value = dt.Rows[i][0];
                                    SqlDataReader dataReader = selectCMD06A.ExecuteReader();

                                    if (dataReader.Read())
                                    {

                                        dt.Rows[i]["BatchLocation"] = dataReader[1];
                                        dt.Rows[i]["BatchStatus"] = dataReader[2];
                                        dt.Rows[i]["TSImportDate"] = dataReader[3];
                                        dt.Rows[i]["OCR POD"] = "POD 4 - MTV-VA-TSCN131";

                                        dataReader.Close();
                                    }



                                }






                            }






                            dataGridView5.DataSource = dt;



                            //dataGridView1.DataSource = dt;




                        }



                        catch
                        {



                        }

                }
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            button10.Enabled = false;
            dataGridView5.DataSource = null;
            dataGridView5.Rows.Clear();
            dataGridView5.Refresh();
            label274.Visible = false;
            label275.Visible = false;

            pendingUpload();
            button10.Text = "Re-Load";
            button10.Enabled = true;
        }
    }
}
