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
        public void mismatchDCS()
        {

            if (radioButton39.Checked)
            {
                using (SqlConnection dataConnection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.DVAConnectionStringFP"].ToString()))

                    try
                    {
                        dataConnection.Open();


                        SqlCommand selectCMD01 = new SqlCommand(@"select pbatch BatchName, RMN, dcsid DCSID, invtime InvTime, 
                        boxno BoxNo, lastname LastName, firstname FirstName, middleinitial MI, filenumber FileNumber, 
                        participantid ParticipantID from PbatchDCSMapping where pbatch = @pbatch", dataConnection);
                        selectCMD01.Parameters.AddWithValue("@pbatch", textBox18.Text);



                        SqlCommand selectCMD02 = new SqlCommand(@"select distinct RMN from PbatchDCSMapping where pbatch = @pbatch", dataConnection);
                        selectCMD02.Parameters.AddWithValue("@pbatch", textBox18.Text);



                        SqlCommand selectCMD04 = new SqlCommand(@"select top 1 batchdesc from turboscanng1.dbo.Batches where batchname = @pbatch", dataConnection);
                        selectCMD04.Parameters.AddWithValue("@pbatch", textBox18.Text);
                        string batchDesc = Convert.ToString(selectCMD04.ExecuteScalar());


                        if (batchDesc.StartsWith("MismatchDCSMapping:"))
                        {
                            string DCSID = batchDesc.Substring(20);
                            DCSID = DCSID.TrimEnd(';');
                            label160.Text = DCSID;


                            SqlCommand selectCMD05 = new SqlCommand(@"select count(*) from PbatchDCSMapping where pbatch = @pbatch and dcsid = @dcsid", dataConnection);
                            selectCMD05.Parameters.AddWithValue("@pbatch", textBox18.Text);
                            selectCMD05.Parameters.AddWithValue("@dcsid", DCSID);
                            Int32 dcsidCount = (Int32)selectCMD05.ExecuteScalar();



                            if (dcsidCount >= 1)
                            {

                                label156.Visible = true;
                                label156.Text = "Batch Issue:";
                                label157.Visible = true;
                                label157.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(96)))), ((int)(((byte)(115)))));
                                label157.Text = "Batch is missing DCSID " + DCSID;


                            }


                            if (dcsidCount < 1)
                            {

                                label156.Visible = true;
                                label156.Text = "Batch Issue:";
                                label157.Visible = true;
                                label157.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(96)))), ((int)(((byte)(115)))));
                                label157.Text = "Extra DCSID " + DCSID + " found in batch";



                            }

                        }

                        else
                        {
                            label160.Text = "";

                        }





                        DataTable dt = new DataTable();
                        SqlDataAdapter workAdapter = new SqlDataAdapter(selectCMD01);
                        workAdapter.Fill(dt);
                        Column1.Visible = true;
                        dataGridView26.Columns.Add(Column1);


                        dataGridView26.AllowUserToAddRows = false;
                        dataGridView26.DataSource = dt;
                        dataGridView26.ClearSelection();
                        dataGridView26.Visible = true;


                        int totItems = dt.Rows.Count;

                        label150.Visible = true;
                        label150.Text = "Total Records:";
                        label151.Visible = true;
                        label151.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(96)))), ((int)(((byte)(115)))));
                        label151.Text = totItems.ToString();


                        label152.Visible = true;
                        label152.Text = "Batch Desc:";
                        label153.Visible = true;
                        label153.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(96)))), ((int)(((byte)(115)))));
                        label153.Text = batchDesc;


                        label154.Visible = true;
                        label154.Text = "Batch Name:";
                        label155.Visible = true;
                        label155.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(96)))), ((int)(((byte)(115)))));
                        label155.Text = textBox18.Text;







                        if (totItems >= 1)
                        {

                            button32.Visible = true;
                            button32.Text = "Show all records matching RMN";
                            groupBox4.Visible = true;
                            button35.Visible = true;

                        }


                        else
                        {

                            MessageBox.Show("No records not found.");
                            button32.Visible = false;
                            return;



                        }


                    }



                    catch
                    {



                    }

            }


            if (radioButton38.Checked)
            {
                using (SqlConnection dataConnection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.DVAConnectionStringMTV"].ToString()))

                    try
                    {
                        dataConnection.Open();


                        SqlCommand selectCMD01 = new SqlCommand(@"select pbatch BatchName, RMN, dcsid DCSID, invtime InvTime, 
                        boxno BoxNo, lastname LastName, firstname FirstName, middleinitial MI, filenumber FileNumber, 
                        participantid ParticipantID from PbatchDCSMapping where pbatch = @pbatch", dataConnection);
                        selectCMD01.Parameters.AddWithValue("@pbatch", textBox18.Text);



                        SqlCommand selectCMD02 = new SqlCommand(@"select distinct RMN from PbatchDCSMapping where pbatch = @pbatch", dataConnection);
                        selectCMD02.Parameters.AddWithValue("@pbatch", textBox18.Text);



                        SqlCommand selectCMD04 = new SqlCommand(@"select top 1 batchdesc from [mtv-va-sql-4\p1].turboscanng1.dbo.Batches where batchname = @pbatch", dataConnection);
                        selectCMD04.Parameters.AddWithValue("@pbatch", textBox18.Text);
                        string batchDesc = Convert.ToString(selectCMD04.ExecuteScalar());


                        if (batchDesc.StartsWith("MismatchDCSMapping:"))
                        {
                            string DCSID = batchDesc.Substring(20);
                            DCSID = DCSID.TrimEnd(';');
                            label160.Text = DCSID;


                            SqlCommand selectCMD05 = new SqlCommand(@"select count(*) from PbatchDCSMapping where pbatch = @pbatch and dcsid = @dcsid", dataConnection);
                            selectCMD05.Parameters.AddWithValue("@pbatch", textBox18.Text);
                            selectCMD05.Parameters.AddWithValue("@dcsid", DCSID);
                            Int32 dcsidCount = (Int32)selectCMD05.ExecuteScalar();



                            if (dcsidCount >= 1)
                            {

                                label156.Visible = true;
                                label156.Text = "Batch Issue:";
                                label157.Visible = true;
                                label157.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(96)))), ((int)(((byte)(115)))));
                                label157.Text = "Batch is missing DCSID " + DCSID;


                            }


                            if (dcsidCount < 1)
                            {

                                label156.Visible = true;
                                label156.Text = "Batch Issue:";
                                label157.Visible = true;
                                label157.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(96)))), ((int)(((byte)(115)))));
                                label157.Text = "Extra DCSID " + DCSID + " found in batch";



                            }

                        }

                        else
                        {
                            label160.Text = "";

                        }





                        DataTable dt = new DataTable();
                        SqlDataAdapter workAdapter = new SqlDataAdapter(selectCMD01);
                        workAdapter.Fill(dt);
                        Column1.Visible = true;
                        dataGridView26.Columns.Add(Column1);


                        dataGridView26.AllowUserToAddRows = false;
                        dataGridView26.DataSource = dt;
                        dataGridView26.ClearSelection();
                        dataGridView26.Visible = true;


                        int totItems = dt.Rows.Count;

                        label150.Visible = true;
                        label150.Text = "Total Records:";
                        label151.Visible = true;
                        label151.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(96)))), ((int)(((byte)(115)))));
                        label151.Text = totItems.ToString();


                        label152.Visible = true;
                        label152.Text = "Batch Desc:";
                        label153.Visible = true;
                        label153.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(96)))), ((int)(((byte)(115)))));
                        label153.Text = batchDesc;


                        label154.Visible = true;
                        label154.Text = "Batch Name:";
                        label155.Visible = true;
                        label155.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(96)))), ((int)(((byte)(115)))));
                        label155.Text = textBox18.Text;







                        if (totItems >= 1)
                        {

                            button32.Visible = true;
                            button32.Text = "Show all records matching RMN";
                            groupBox4.Visible = true;
                            button35.Visible = true;

                        }


                        else
                        {

                            MessageBox.Show("No records not found.");
                            button32.Visible = false;
                            return;



                        }


                    }



                    catch
                    {



                    }

            }

        }







        public void mismatchDCS_RMN()
        {

            if (radioButton39.Checked)
            {
                using (SqlConnection dataConnection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.DVAConnectionStringFP"].ToString()))

                    try
                    {
                        dataConnection.Open();

                        SqlCommand selectCMD02 = new SqlCommand(@"select distinct RMN from PbatchDCSMapping where pbatch = @pbatch", dataConnection);
                        selectCMD02.Parameters.AddWithValue("@pbatch", textBox18.Text);
                        string RMN = Convert.ToString(selectCMD02.ExecuteScalar());


                        SqlCommand selectCMD03 = new SqlCommand(@"select pbatch BatchName, RMN, dcsid DCSID, invtime InvTime, 
                        boxno BoxNo, lastname LastName, firstname FirstName, middleinitial MI, filenumber FileNumber, 
                        participantid ParticipantID from PbatchDCSMapping where RMN = @RMN", dataConnection);
                        selectCMD03.Parameters.AddWithValue("@RMN", RMN);



                        DataTable dt = new DataTable();
                        SqlDataAdapter workAdapter = new SqlDataAdapter(selectCMD03);
                        workAdapter.Fill(dt);
                        Column1.Visible = true;

                        dataGridView26.AllowUserToAddRows = false;
                        dataGridView26.DataSource = dt;
                        dataGridView26.ClearSelection();
                        dataGridView26.Visible = true;

                        int totItems = dt.Rows.Count;

                        label150.Visible = true;
                        label150.Text = "Total Records:";
                        label151.Visible = true;
                        label151.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(96)))), ((int)(((byte)(115)))));
                        label151.Text = totItems.ToString();



                    }


                    catch
                    {




                    }
            }


            if (radioButton38.Checked)
            {
                using (SqlConnection dataConnection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.DVAConnectionStringMTV"].ToString()))

                    try
                    {
                        dataConnection.Open();

                        SqlCommand selectCMD02 = new SqlCommand(@"select distinct RMN from PbatchDCSMapping where pbatch = @pbatch", dataConnection);
                        selectCMD02.Parameters.AddWithValue("@pbatch", textBox18.Text);
                        string RMN = Convert.ToString(selectCMD02.ExecuteScalar());


                        SqlCommand selectCMD03 = new SqlCommand(@"select pbatch BatchName, RMN, dcsid DCSID, invtime InvTime, 
                        boxno BoxNo, lastname LastName, firstname FirstName, middleinitial MI, filenumber FileNumber, 
                        participantid ParticipantID from PbatchDCSMapping where RMN = @RMN", dataConnection);
                        selectCMD03.Parameters.AddWithValue("@RMN", RMN);



                        DataTable dt = new DataTable();
                        SqlDataAdapter workAdapter = new SqlDataAdapter(selectCMD03);
                        workAdapter.Fill(dt);
                        Column1.Visible = true;

                        dataGridView26.AllowUserToAddRows = false;
                        dataGridView26.DataSource = dt;
                        dataGridView26.ClearSelection();
                        dataGridView26.Visible = true;

                        int totItems = dt.Rows.Count;

                        label150.Visible = true;
                        label150.Text = "Total Records:";
                        label151.Visible = true;
                        label151.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(96)))), ((int)(((byte)(115)))));
                        label151.Text = totItems.ToString();



                    }


                    catch
                    {




                    }
            }

        }


        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked)
            {
                label158.Visible = true;
                label158.Text = "Select new batch for DCSID " + label160.Text + " :";
                button33.Visible = true;
                button33.Text = "Move";
                comboBox4.Visible = true;
                label159.Visible = false;
                textBox19.Visible = false;
                button34.Visible = false;
                button36.Visible = false;
                fillCombox1();



            }

        }


        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                label158.Visible = false;
                button33.Visible = false;
                comboBox4.Visible = false;
                label159.Visible = true;
                label159.Text = "Enter DCSID value to add to batch " + textBox18.Text.Trim() + " :";
                textBox19.Visible = true;
                button34.Visible = true;
                button34.Text = "Add";
                button36.Visible = false;



            }
        }


        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
            {
                label158.Visible = false;
                button33.Visible = false;
                comboBox4.Visible = false;
                label159.Visible = false;
                textBox19.Visible = false;
                button34.Visible = false;
                button36.Visible = true;
                button36.Text = "Delete";



            }
        }

        private void fillCombox1()
        {


            if (radioButton39.Checked)
            {
                using (SqlConnection dataConnection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.DVAConnectionStringFP"].ToString()))

                    try
                    {
                        dataConnection.Open();

                        comboBox4.Items.Clear();

                        SqlCommand selectCMD02 = new SqlCommand(@"select distinct RMN from PbatchDCSMapping where pbatch = @pbatch", dataConnection);
                        selectCMD02.Parameters.AddWithValue("@pbatch", textBox18.Text);
                        string RMN = Convert.ToString(selectCMD02.ExecuteScalar());


                        SqlCommand selectCMD06 = new SqlCommand(@"select distinct pbatch from PbatchDCSMapping where RMN = @RMN", dataConnection);
                        selectCMD06.Parameters.AddWithValue("@RMN", RMN);

                        //comboBox4.Visible = true;

                        using (SqlDataReader reader = selectCMD06.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBox4.Items.Add(reader.GetValue(0).ToString());

                            }


                            reader.Close();

                        }



                    }


                    catch
                    {


                    }
            }


            if (radioButton38.Checked)
            {
                using (SqlConnection dataConnection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.DVAConnectionStringMTV"].ToString()))

                    try
                    {
                        dataConnection.Open();

                        comboBox4.Items.Clear();

                        SqlCommand selectCMD02 = new SqlCommand(@"select distinct RMN from PbatchDCSMapping where pbatch = @pbatch", dataConnection);
                        selectCMD02.Parameters.AddWithValue("@pbatch", textBox18.Text);
                        string RMN = Convert.ToString(selectCMD02.ExecuteScalar());


                        SqlCommand selectCMD06 = new SqlCommand(@"select distinct pbatch from PbatchDCSMapping where RMN = @RMN", dataConnection);
                        selectCMD06.Parameters.AddWithValue("@RMN", RMN);

                        //comboBox4.Visible = true;

                        using (SqlDataReader reader = selectCMD06.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBox4.Items.Add(reader.GetValue(0).ToString());

                            }


                            reader.Close();

                        }



                    }


                    catch
                    {


                    }
            }
        }

        private void moveBatch()
        {
            if (radioButton39.Checked)
            {
                for (int i = dataGridView26.Rows.Count - 1; i >= 0; i--)
                {

                    if ((bool)dataGridView26.Rows[i].Cells[0].FormattedValue)
                    {

                        string batchName = dataGridView26.Rows[i].Cells[1].Value.ToString();
                        string RMN = dataGridView26.Rows[i].Cells[2].Value.ToString();
                        string DCSID = dataGridView26.Rows[i].Cells[3].Value.ToString();



                        using (SqlConnection dataConnection = new SqlConnection(
                            ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.DVAConnectionStringFP"].ToString()))

                            try
                            {
                                dataConnection.Open();


                                SqlCommand checkCMD = new SqlCommand(@"select count(*) from PbatchDCSMapping where dcsid = @dcsid and pbatch = @pbatch", dataConnection);
                                checkCMD.Parameters.AddWithValue("@dcsid", DCSID);
                                checkCMD.Parameters.AddWithValue("@pbatch", comboBox4.Text);
                                var resultchck = (Int32)checkCMD.ExecuteScalar();

                                if (resultchck >= 1)
                                {

                                    MessageBox.Show("The selected DCSID " + DCSID + " record is already been mapped to batch " + batchName + ".  DCSID can't be updated.");
                                    return;

                                }



                                if (resultchck < 1)
                                {
                                    SqlCommand updateCMD = new SqlCommand(@"update PbatchDCSMapping 
                                    set pbatch = @newpbatch 
                                    where dcsid = @dcsid and pbatch = @pbatch", dataConnection);
                                    updateCMD.Parameters.AddWithValue("@newpbatch", comboBox4.Text);
                                    updateCMD.Parameters.AddWithValue("@pbatch", batchName);
                                    updateCMD.Parameters.AddWithValue("@dcsid", DCSID);
                                    updateCMD.ExecuteNonQuery();
                                    dataGridView26.ClearSelection();


                                    SqlCommand updateCMD01 = new SqlCommand(@"INSERT INTO docid.dbo.loglookupactions
                                    (action, value01, value02, value03, operID) VALUES ('MoveDCSID', @value01, @value02, @value03, @operID)", dataConnection);
                                    updateCMD01.Parameters.AddWithValue("@value01", DCSID);
                                    updateCMD01.Parameters.AddWithValue("@value02", batchName);
                                    updateCMD01.Parameters.AddWithValue("@value03", comboBox4.Text);
                                    updateCMD01.Parameters.AddWithValue("@operID", label10.Text);
                                    updateCMD01.ExecuteNonQuery();

                                    MessageBox.Show("DCSID " + DCSID + " has been moved to batch " + comboBox4.Text);


                                    comboBox4.Text = textBox18.Text;

                                    label158.Visible = false;
                                    button33.Visible = false;
                                    comboBox4.Visible = false;
                                    label159.Visible = false;
                                    textBox19.Visible = false;
                                    button34.Visible = false;
                                    button36.Visible = false;
                                    //dataGridView26.Visible = false;
                                    mismatchDCS_RMN();
                                    comboBox4.Text = "";
                                    radioButton7.Checked = false;
                                    radioButton8.Checked = false;
                                    radioButton9.Checked = false;







                                }



                            }


                            catch
                            {



                            }

                    }


                }

            }

            if (radioButton38.Checked)
            {
                for (int i = dataGridView26.Rows.Count - 1; i >= 0; i--)
                {

                    if ((bool)dataGridView26.Rows[i].Cells[0].FormattedValue)
                    {

                        string batchName = dataGridView26.Rows[i].Cells[1].Value.ToString();
                        string RMN = dataGridView26.Rows[i].Cells[2].Value.ToString();
                        string DCSID = dataGridView26.Rows[i].Cells[3].Value.ToString();



                        using (SqlConnection dataConnection = new SqlConnection(
                            ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.DVAConnectionStringMTV"].ToString()))

                            try
                            {
                                dataConnection.Open();


                                SqlCommand checkCMD = new SqlCommand(@"select count(*) from PbatchDCSMapping where dcsid = @dcsid and pbatch = @pbatch", dataConnection);
                                checkCMD.Parameters.AddWithValue("@dcsid", DCSID);
                                checkCMD.Parameters.AddWithValue("@pbatch", comboBox4.Text);
                                var resultchck = (Int32)checkCMD.ExecuteScalar();

                                if (resultchck >= 1)
                                {

                                    MessageBox.Show("The selected DCSID " + DCSID + " record is already been mapped to batch " + batchName + ".  DCSID can't be updated.");
                                    return;

                                }



                                if (resultchck < 1)
                                {
                                    SqlCommand updateCMD = new SqlCommand(@"update PbatchDCSMapping 
                                    set pbatch = @newpbatch 
                                    where dcsid = @dcsid and pbatch = @pbatch", dataConnection);
                                    updateCMD.Parameters.AddWithValue("@newpbatch", comboBox4.Text);
                                    updateCMD.Parameters.AddWithValue("@pbatch", batchName);
                                    updateCMD.Parameters.AddWithValue("@dcsid", DCSID);
                                    updateCMD.ExecuteNonQuery();
                                    dataGridView26.ClearSelection();


                                    SqlCommand updateCMD01 = new SqlCommand(@"INSERT INTO docid.dbo.loglookupactions
                                    (action, value01, value02, value03, operID) VALUES ('MoveDCSID', @value01, @value02, @value03, @operID)", dataConnection);
                                    updateCMD01.Parameters.AddWithValue("@value01", DCSID);
                                    updateCMD01.Parameters.AddWithValue("@value02", batchName);
                                    updateCMD01.Parameters.AddWithValue("@value03", comboBox4.Text);
                                    updateCMD01.Parameters.AddWithValue("@operID", label10.Text);
                                    updateCMD01.ExecuteNonQuery();

                                    MessageBox.Show("DCSID " + DCSID + " has been moved to batch " + comboBox4.Text);


                                    comboBox4.Text = textBox18.Text;

                                    label158.Visible = false;
                                    button33.Visible = false;
                                    comboBox4.Visible = false;
                                    label159.Visible = false;
                                    textBox19.Visible = false;
                                    button34.Visible = false;
                                    button36.Visible = false;
                                    //dataGridView26.Visible = false;
                                    mismatchDCS_RMN();
                                    comboBox4.Text = "";
                                    radioButton7.Checked = false;
                                    radioButton8.Checked = false;
                                    radioButton9.Checked = false;







                                }



                            }


                            catch
                            {



                            }

                    }


                }

            }
        }


        private void addDCSID()
        {
            if (radioButton39.Checked)
            {
                using (SqlConnection dataConnection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.DVAConnectionStringFP"].ToString()))

                    try
                    {
                        dataConnection.Open();

                        SqlCommand selectCMD04 = new SqlCommand(@"select top 1 RMN from PbatchDCSMapping where pbatch = @pbatch", dataConnection);
                        selectCMD04.Parameters.AddWithValue("@pbatch", textBox18.Text);
                        string RMN = Convert.ToString(selectCMD04.ExecuteScalar());


                        SqlCommand insertCMD01 = new SqlCommand(@"insert into PbatchDCSMapping (Pbatch, RMN, dcsid) VALUES (@Pbatch, @RMN, @dcsid)", dataConnection);
                        insertCMD01.Parameters.AddWithValue("@Pbatch", textBox18.Text);
                        insertCMD01.Parameters.AddWithValue("@RMN", RMN);
                        insertCMD01.Parameters.AddWithValue("@dcsid", textBox19.Text);
                        insertCMD01.ExecuteNonQuery();


                        SqlCommand updateCMD01 = new SqlCommand(@"INSERT INTO docid.dbo.loglookupactions
                                    (action, value01, value02, value03, operID) VALUES ('AddDCSID', @value01, @value02, @value03, @operID)", dataConnection);
                        updateCMD01.Parameters.AddWithValue("@value01", textBox19.Text);
                        updateCMD01.Parameters.AddWithValue("@value02", RMN);
                        updateCMD01.Parameters.AddWithValue("@value03", textBox18.Text);
                        updateCMD01.Parameters.AddWithValue("@operID", label10.Text);
                        updateCMD01.ExecuteNonQuery();


                        MessageBox.Show("DCSID " + textBox19.Text + " has been added to batch " + textBox18.Text);



                        label158.Visible = false;
                        button33.Visible = false;
                        comboBox4.Visible = false;
                        label159.Visible = false;
                        textBox19.Visible = false;
                        button34.Visible = false;
                        button36.Visible = false;
                        //dataGridView26.Visible = false;
                        //mismatchDCS_RMN();
                        button31.PerformClick();
                        textBox19.Text = "";
                        radioButton7.Checked = false;
                        radioButton8.Checked = false;
                        radioButton9.Checked = false;




                    }


                    catch
                    {





                    }


            }

            if (radioButton38.Checked)
            {
                using (SqlConnection dataConnection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.DVAConnectionStringMTV"].ToString()))

                    try
                    {
                        dataConnection.Open();

                        SqlCommand selectCMD04 = new SqlCommand(@"select top 1 RMN from PbatchDCSMapping where pbatch = @pbatch", dataConnection);
                        selectCMD04.Parameters.AddWithValue("@pbatch", textBox18.Text);
                        string RMN = Convert.ToString(selectCMD04.ExecuteScalar());


                        SqlCommand insertCMD01 = new SqlCommand(@"insert into PbatchDCSMapping (Pbatch, RMN, dcsid) VALUES (@Pbatch, @RMN, @dcsid)", dataConnection);
                        insertCMD01.Parameters.AddWithValue("@Pbatch", textBox18.Text);
                        insertCMD01.Parameters.AddWithValue("@RMN", RMN);
                        insertCMD01.Parameters.AddWithValue("@dcsid", textBox19.Text);
                        insertCMD01.ExecuteNonQuery();


                        SqlCommand updateCMD01 = new SqlCommand(@"INSERT INTO docid.dbo.loglookupactions
                                    (action, value01, value02, value03, operID) VALUES ('AddDCSID', @value01, @value02, @value03, @operID)", dataConnection);
                        updateCMD01.Parameters.AddWithValue("@value01", textBox19.Text);
                        updateCMD01.Parameters.AddWithValue("@value02", RMN);
                        updateCMD01.Parameters.AddWithValue("@value03", textBox18.Text);
                        updateCMD01.Parameters.AddWithValue("@operID", label10.Text);
                        updateCMD01.ExecuteNonQuery();


                        MessageBox.Show("DCSID " + textBox19.Text + " has been added to batch " + textBox18.Text);



                        label158.Visible = false;
                        button33.Visible = false;
                        comboBox4.Visible = false;
                        label159.Visible = false;
                        textBox19.Visible = false;
                        button34.Visible = false;
                        button36.Visible = false;
                        //dataGridView26.Visible = false;
                        //mismatchDCS_RMN();
                        button31.PerformClick();
                        textBox19.Text = "";
                        radioButton7.Checked = false;
                        radioButton8.Checked = false;
                        radioButton9.Checked = false;




                    }


                    catch
                    {





                    }


            }
        }


        private void deleteDCSID()
        {
            if (radioButton39.Checked)
            {
                //MessageBox.Show("Are you sure you want to delete selected records?");
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete selected records?", "Confirmation", buttons, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {

                    for (int i = dataGridView26.Rows.Count - 1; i >= 0; i--)
                    {

                        if ((bool)dataGridView26.Rows[i].Cells[0].FormattedValue)
                        {

                            string batchName = dataGridView26.Rows[i].Cells[1].Value.ToString();
                            string RMN = dataGridView26.Rows[i].Cells[2].Value.ToString();
                            string DCSID = dataGridView26.Rows[i].Cells[3].Value.ToString();



                            using (SqlConnection dataConnection = new SqlConnection(
                                ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.DVAConnectionStringFP"].ToString()))



                                try
                                {
                                    dataConnection.Open();


                                    SqlCommand deleteCMD = new SqlCommand(@"delete from PbatchDCSMapping where dcsid = @dcsid and pbatch = @pbatch and rmn = @rmn", dataConnection);
                                    deleteCMD.Parameters.AddWithValue("@dcsid", DCSID);
                                    deleteCMD.Parameters.AddWithValue("@rmn", RMN);
                                    deleteCMD.Parameters.AddWithValue("@pbatch", textBox18.Text);
                                    deleteCMD.ExecuteNonQuery();
                                    dataGridView26.ClearSelection();



                                    SqlCommand updateCMD01 = new SqlCommand(@"INSERT INTO docid.dbo.loglookupactions
                                    (action, value01, value02, value03, operID) VALUES ('DeleteDCSID', @value01, @value02, @value03, @operID)", dataConnection);
                                    updateCMD01.Parameters.AddWithValue("@value01", DCSID);
                                    updateCMD01.Parameters.AddWithValue("@value02", RMN);
                                    updateCMD01.Parameters.AddWithValue("@value03", textBox18.Text);
                                    updateCMD01.Parameters.AddWithValue("@operID", label10.Text);
                                    updateCMD01.ExecuteNonQuery();




                                    MessageBox.Show("DCSID " + DCSID + " has been deleted from batch " + textBox18.Text);


                                    label158.Visible = false;
                                    button33.Visible = false;
                                    comboBox4.Visible = false;
                                    label159.Visible = false;
                                    textBox19.Visible = false;
                                    button34.Visible = false;
                                    button36.Visible = false;
                                    //dataGridView26.Visible = false;
                                    //mismatchDCS_RMN();
                                    button31.PerformClick();
                                    radioButton7.Checked = false;
                                    radioButton8.Checked = false;
                                    radioButton9.Checked = false;






                                }


                                catch
                                {


                                }

                        }

                    }




                }

            }

            if (radioButton38.Checked)
            {
                //MessageBox.Show("Are you sure you want to delete selected records?");
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete selected records?", "Confirmation", buttons, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {

                    for (int i = dataGridView26.Rows.Count - 1; i >= 0; i--)
                    {

                        if ((bool)dataGridView26.Rows[i].Cells[0].FormattedValue)
                        {

                            string batchName = dataGridView26.Rows[i].Cells[1].Value.ToString();
                            string RMN = dataGridView26.Rows[i].Cells[2].Value.ToString();
                            string DCSID = dataGridView26.Rows[i].Cells[3].Value.ToString();



                            using (SqlConnection dataConnection = new SqlConnection(
                                ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.DVAConnectionStringMTV"].ToString()))



                                try
                                {
                                    dataConnection.Open();


                                    SqlCommand deleteCMD = new SqlCommand(@"delete from PbatchDCSMapping where dcsid = @dcsid and pbatch = @pbatch and rmn = @rmn", dataConnection);
                                    deleteCMD.Parameters.AddWithValue("@dcsid", DCSID);
                                    deleteCMD.Parameters.AddWithValue("@rmn", RMN);
                                    deleteCMD.Parameters.AddWithValue("@pbatch", textBox18.Text);
                                    deleteCMD.ExecuteNonQuery();
                                    dataGridView26.ClearSelection();



                                    SqlCommand updateCMD01 = new SqlCommand(@"INSERT INTO docid.dbo.loglookupactions
                                    (action, value01, value02, value03, operID) VALUES ('DeleteDCSID', @value01, @value02, @value03, @operID)", dataConnection);
                                    updateCMD01.Parameters.AddWithValue("@value01", DCSID);
                                    updateCMD01.Parameters.AddWithValue("@value02", RMN);
                                    updateCMD01.Parameters.AddWithValue("@value03", textBox18.Text);
                                    updateCMD01.Parameters.AddWithValue("@operID", label10.Text);
                                    updateCMD01.ExecuteNonQuery();




                                    MessageBox.Show("DCSID " + DCSID + " has been deleted from batch " + textBox18.Text);


                                    label158.Visible = false;
                                    button33.Visible = false;
                                    comboBox4.Visible = false;
                                    label159.Visible = false;
                                    textBox19.Visible = false;
                                    button34.Visible = false;
                                    button36.Visible = false;
                                    //dataGridView26.Visible = false;
                                    //mismatchDCS_RMN();
                                    button31.PerformClick();
                                    radioButton7.Checked = false;
                                    radioButton8.Checked = false;
                                    radioButton9.Checked = false;






                                }


                                catch
                                {


                                }

                        }

                    }




                }

            }

        }

        //lookup up details by batch
        private void button31_Click(object sender, EventArgs e)
        {

            button31.Enabled = false;
            dataGridView26.DataSource = null;
            dataGridView26.Rows.Clear();
            dataGridView26.Columns.Clear();
            dataGridView26.Refresh();
            dataGridView26.Visible = false;
            label150.Visible = false;
            label151.Visible = false;
            label152.Visible = false;
            label153.Visible = false;
            label154.Visible = false;
            label155.Visible = false;
            label156.Visible = false;
            label157.Visible = false;
            groupBox4.Visible = false;
            label158.Visible = false;
            button33.Visible = false;
            comboBox4.Visible = false;
            label159.Visible = false;
            textBox19.Visible = false;
            button34.Visible = false;
            button36.Visible = false;
            radioButton7.Checked = false;
            radioButton8.Checked = false;
            radioButton9.Checked = false;
            mismatchDCS();
            button31.Enabled = true;




        }

        //view all records for RMN
        private void button32_Click(object sender, EventArgs e)
        {
            button32.Enabled = false;
            dataGridView26.DataSource = null;
            dataGridView26.Rows.Clear();
            //dataGridView26.Columns.Clear();
            dataGridView26.Refresh();
            dataGridView26.Visible = false;
            label150.Visible = false;
            label151.Visible = false;
            label158.Visible = false;
            button33.Visible = false;
            comboBox4.Visible = false;
            label159.Visible = false;
            textBox19.Visible = false;
            button34.Visible = false;
            button36.Visible = false;
            radioButton7.Checked = false;
            radioButton8.Checked = false;
            radioButton9.Checked = false;
            mismatchDCS_RMN();
            button32.Enabled = true;

        }

        private void dataGridView26_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ((sender as DataGridView).CurrentCell is DataGridViewCheckBoxCell)
            {
                if (Convert.ToBoolean(((sender as DataGridView).CurrentCell as DataGridViewCheckBoxCell).Value))
                {
                    foreach (DataGridViewRow row in (sender as DataGridView).Rows)
                    {
                        if (row.Index != (sender as DataGridView).CurrentCell.RowIndex && Convert.ToBoolean(row.Cells[e.ColumnIndex].Value) == true)
                        {
                            row.Cells[e.ColumnIndex].Value = false;
                        }
                    }
                }
            }
        }

        private void dataGridView26_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView26.IsCurrentCellDirty)
            {
                dataGridView26.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }


        //move DCSID
        private void button33_Click(object sender, EventArgs e)
        {
            button33.Enabled = false;
            moveBatch();
            button33.Enabled = true;



        }

        //clear selected
        private void button35_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dataGridView26.Rows)
            {

                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)item.Cells[0];
                cell.Value = false;

            }

        }

        private void button34_Click(object sender, EventArgs e)
        {
            button34.Enabled = false;
            addDCSID();
            button34.Enabled = true;

        }

        private void button36_Click(object sender, EventArgs e)
        {
            button36.Enabled = false;
            deleteDCSID();
            button36.Enabled = true;

        }


    }
}
