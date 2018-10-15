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
        private void deleteDCSBatch()
        {

            if (radioButton34.Checked)
            {

                using (SqlConnection dataConnection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.DVAConnectionStringFP"].ToString()))


                    try
                    {
                        //hammertime
                        DateTime now = DateTime.Now;

                        dataConnection.Open();


                        var Batches = textBox4.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                        List<string> deletedBatches = new List<string>();
                        List<string> NOTdeletedBatches = new List<string>();
                        int countDelete = deletedBatches.Count();

                        foreach (string batch in Batches)
                        {


                            //check if batch exists in the muthafuckan physicalbatch table
                            SqlCommand checkCMD = new SqlCommand(@"select count(*) from PhysicalBatch where pbatch = @pbatch", dataConnection);
                            checkCMD.Parameters.AddWithValue("@pbatch", batch);
                            var result = (Int32)checkCMD.ExecuteScalar();
                            //MessageBox.Show("1");

                            //check if the dayum batch exists in the muthafuckan document table
                            SqlCommand checkCMD01 = new SqlCommand(@"select count(*) from document where pbatch = @pbatch", dataConnection);
                            checkCMD01.Parameters.AddWithValue("@pbatch", batch);
                            var result01 = (Int32)checkCMD01.ExecuteScalar();
                            //MessageBox.Show("2");

                            //check if the dayum batch exists in the muthafuckan Turboscan table
                            SqlCommand checkCMD02 = new SqlCommand(@"select count(*) from TurboscanNG1.dbo.batches where JobID in ('8', '11') and batchname = @batchname", dataConnection);
                            checkCMD02.Parameters.AddWithValue("@batchname", batch);
                            var result02 = (Int32)checkCMD02.ExecuteScalar();
                            //MessageBox.Show("3");

                            //if (result == 1 && result01 < 1)
                            if (batch.StartsWith("02") && result == 1 && result01 < 1)
                            {

                                SqlCommand selectCMD = new SqlCommand(@"insert into PhysicalBatch_bck (PBatch, RecvDate, BackDate,pobox,InvTime, BatchClassName, RMN, RNDAudit)
                                select PBatch, RecvDate, BackDate,pobox,InvTime, BatchClassName, RMN, RNDAudit from PhysicalBatch where pbatch = @pbatch", dataConnection);
                                //MessageBox.Show("4");

                                SqlCommand deleteCMD = new SqlCommand(@"delete from PhysicalBatch where pbatch = @pbatch", dataConnection);
                                //MessageBox.Show("5");

                                SqlCommand selectCMD01 = new SqlCommand(@"update PhysicalBatch_bck
                                set DeletedBy = @deletedby, DeletedRequestedBy = @deleterequestedby, DeleteReason = @deletereason
                                where pbatch = @pbatch", dataConnection);
                                //MessageBox.Show("6");

                                try
                                {
                                    //move the biotch to backup table
                                    selectCMD.Parameters.AddWithValue("@pbatch", batch);
                                    selectCMD.ExecuteNonQuery();



                                    //delete that puppy
                                    deleteCMD.Parameters.AddWithValue("@pbatch", batch);
                                    deleteCMD.ExecuteNonQuery();



                                    //get more goods
                                    selectCMD01.Parameters.AddWithValue("@deletedby", varGlob.operID);
                                    selectCMD01.Parameters.AddWithValue("@deleterequestedby", textBox5.Text);
                                    selectCMD01.Parameters.AddWithValue("@deletereason", comboBox1.Text);
                                    selectCMD01.Parameters.AddWithValue("@pbatch", batch);
                                    selectCMD01.ExecuteNonQuery();





                                    deletedBatches.Add(batch);

                                    label276.Visible = true;
                                    dataGridView52.Visible = true;
                                    RecentlyDeletedBatches();



                                    if (result02 >= 1)
                                    {
                                        MessageBox.Show("Notify operations that they need to delete batch " + batch + " from Turboscan.");


                                    }

                                }


                                catch
                                {



                                }


                            }




                            if (!batch.StartsWith("02") && result == 1 && result01 < 1)
                            {

                                SqlCommand selectCMD = new SqlCommand(@"insert into PhysicalBatch_bck (PBatch, RecvDate, BackDate,pobox,InvTime, BatchClassName, RMN, RNDAudit)
                                select PBatch, RecvDate, BackDate,pobox,InvTime, BatchClassName, RMN, RNDAudit from PhysicalBatch where pbatch = @pbatch", dataConnection);

                                SqlCommand selectCMD02 = new SqlCommand(@"insert into cmpcheckin_bck (pbatch, operid, env, insertdate, unscannable)
                                select pbatch, operid, env, insertdate, unscannable from cmpcheckin where pbatch = @pbatch", dataConnection);

                                SqlCommand deleteCMD = new SqlCommand(@"delete from PhysicalBatch where pbatch = @pbatch", dataConnection);

                                SqlCommand deleteCMD01 = new SqlCommand(@"delete from cmpcheckin where pbatch = @pbatch", dataConnection);

                                SqlCommand selectCMD01 = new SqlCommand(@"update PhysicalBatch_bck
                                set DeletedBy = @deletedby, DeletedRequestedBy = @deleterequestedby, DeleteReason = @deletereason
                                where pbatch = @pbatch", dataConnection);


                                try
                                {
                                    //move the biotch to backup table
                                    selectCMD.Parameters.AddWithValue("@pbatch", batch);
                                    selectCMD.ExecuteNonQuery();
                                    selectCMD02.Parameters.AddWithValue("@pbatch", batch);
                                    selectCMD02.ExecuteNonQuery();




                                    //delete that puppy
                                    deleteCMD.Parameters.AddWithValue("@pbatch", batch);
                                    deleteCMD.ExecuteNonQuery();
                                    deleteCMD01.Parameters.AddWithValue("@pbatch", batch);
                                    deleteCMD01.ExecuteNonQuery();



                                    //get more goods
                                    selectCMD01.Parameters.AddWithValue("@deletedby", varGlob.operID);
                                    selectCMD01.Parameters.AddWithValue("@deleterequestedby", textBox5.Text);
                                    selectCMD01.Parameters.AddWithValue("@deletereason", comboBox1.Text);
                                    selectCMD01.Parameters.AddWithValue("@pbatch", batch);
                                    selectCMD01.ExecuteNonQuery();





                                    deletedBatches.Add(batch);

                                    label276.Visible = true;
                                    dataGridView52.Visible = true;
                                    RecentlyDeletedBatches();

                                    if (result02 >= 1)
                                    {
                                        MessageBox.Show("Notify operations that they need to delete batch " + batch + " from Turboscan.");


                                    }

                                }


                                catch
                                {



                                }


                            }


                            if (result < 1 && result01 < 1)
                            {
                                MessageBox.Show("Batch " + batch + " not found.");



                            }


                            if (result01 >= 1)
                            {
                                MessageBox.Show("Batch " + batch + " has records that have been exported.", "WTF - batch will not be deleted.", MessageBoxButtons.OK, MessageBoxIcon.Error);





                            }
                        }

                        var message = string.Join(Environment.NewLine, deletedBatches);
                        MessageBox.Show(message, "Total batches deleted = " + deletedBatches.Count.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);



                    }

                    catch
                    {



                    }

            }


            if (radioButton35.Checked)
            {

                using (SqlConnection dataConnection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.DVAConnectionStringMTV"].ToString()))


                    try
                    {
                        //hammertime
                        DateTime now = DateTime.Now;

                        dataConnection.Open();


                        var Batches = textBox4.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                        List<string> deletedBatches = new List<string>();
                        List<string> NOTdeletedBatches = new List<string>();
                        int countDelete = deletedBatches.Count();

                        foreach (string batch in Batches)
                        {


                            //check if batch exists in the muthafuckan physicalbatch table
                            SqlCommand checkCMD = new SqlCommand(@"select count(*) from PhysicalBatch where pbatch = @pbatch", dataConnection);
                            checkCMD.Parameters.AddWithValue("@pbatch", batch);
                            var result = (Int32)checkCMD.ExecuteScalar();
                            //MessageBox.Show("1");

                            //check if the dayum batch exists in the muthafuckan document table
                            SqlCommand checkCMD01 = new SqlCommand(@"select count(*) from document where pbatch = @pbatch", dataConnection);
                            checkCMD01.Parameters.AddWithValue("@pbatch", batch);
                            var result01 = (Int32)checkCMD01.ExecuteScalar();
                            //MessageBox.Show("2");

                            //check if the dayum batch exists in the muthafuckan Turboscan table
                            SqlCommand checkCMD02 = new SqlCommand(@"select count(*) from [mtv-va-sql-4\p1].TurboscanNG1.dbo.batches where JobID in ('8', '11') and batchname = @batchname", dataConnection);
                            checkCMD02.Parameters.AddWithValue("@batchname", batch);
                            var result02 = (Int32)checkCMD02.ExecuteScalar();
                            //MessageBox.Show("3");

                            //if (result == 1 && result01 < 1)
                            if ((batch.StartsWith("02") || batch.StartsWith("01")) && result == 1 && result01 < 1)
                            {

                                SqlCommand selectCMD = new SqlCommand(@"insert into PhysicalBatch_bck (PBatch, RecvDate, BackDate,pobox,InvTime, BatchClassName, RMN, RNDAudit)
                                select PBatch, RecvDate, BackDate,pobox,InvTime, BatchClassName, RMN, RNDAudit from PhysicalBatch where pbatch = @pbatch", dataConnection);
                                //MessageBox.Show("4");

                                SqlCommand deleteCMD = new SqlCommand(@"delete from PhysicalBatch where pbatch = @pbatch", dataConnection);
                                //MessageBox.Show("5");

                                SqlCommand selectCMD01 = new SqlCommand(@"update PhysicalBatch_bck
                                set DeletedBy = @deletedby, DeletedRequestedBy = @deleterequestedby, DeleteReason = @deletereason
                                where pbatch = @pbatch", dataConnection);
                                //MessageBox.Show("6");

                                try
                                {
                                    //move the biotch to backup table
                                    selectCMD.Parameters.AddWithValue("@pbatch", batch);
                                    selectCMD.ExecuteNonQuery();



                                    //delete that puppy
                                    deleteCMD.Parameters.AddWithValue("@pbatch", batch);
                                    deleteCMD.ExecuteNonQuery();



                                    //get more goods
                                    selectCMD01.Parameters.AddWithValue("@deletedby", varGlob.operID);
                                    selectCMD01.Parameters.AddWithValue("@deleterequestedby", textBox5.Text);
                                    selectCMD01.Parameters.AddWithValue("@deletereason", comboBox1.Text);
                                    selectCMD01.Parameters.AddWithValue("@pbatch", batch);
                                    selectCMD01.ExecuteNonQuery();





                                    deletedBatches.Add(batch);

                                    label276.Visible = true;
                                    dataGridView52.Visible = true;
                                    RecentlyDeletedBatches();

                                    if (result02 >= 1)
                                    {
                                        MessageBox.Show("Notify operations that they need to delete batch " + batch + " from Turboscan.");


                                    }

                                }


                                catch
                                {



                                }


                            }




                            if ((!batch.StartsWith("02") || !batch.StartsWith("01")) && result == 1 && result01 < 1)
                            {

                                SqlCommand selectCMD = new SqlCommand(@"insert into PhysicalBatch_bck (PBatch, RecvDate, BackDate,pobox,InvTime, BatchClassName, RMN, RNDAudit)
                                select PBatch, RecvDate, BackDate,pobox,InvTime, BatchClassName, RMN, RNDAudit from PhysicalBatch where pbatch = @pbatch", dataConnection);

                                SqlCommand selectCMD02 = new SqlCommand(@"insert into cmpcheckin_bck (pbatch, operid, env, insertdate, unscannable)
                                select pbatch, operid, env, insertdate, unscannable from cmpcheckin where pbatch = @pbatch", dataConnection);

                                SqlCommand deleteCMD = new SqlCommand(@"delete from PhysicalBatch where pbatch = @pbatch", dataConnection);

                                SqlCommand deleteCMD01 = new SqlCommand(@"delete from cmpcheckin where pbatch = @pbatch", dataConnection);

                                SqlCommand selectCMD01 = new SqlCommand(@"update PhysicalBatch_bck
                                set DeletedBy = @deletedby, DeletedRequestedBy = @deleterequestedby, DeleteReason = @deletereason
                                where pbatch = @pbatch", dataConnection);


                                try
                                {
                                    //move the biotch to backup table
                                    selectCMD.Parameters.AddWithValue("@pbatch", batch);
                                    selectCMD.ExecuteNonQuery();
                                    selectCMD02.Parameters.AddWithValue("@pbatch", batch);
                                    selectCMD02.ExecuteNonQuery();




                                    //delete that puppy
                                    deleteCMD.Parameters.AddWithValue("@pbatch", batch);
                                    deleteCMD.ExecuteNonQuery();
                                    deleteCMD01.Parameters.AddWithValue("@pbatch", batch);
                                    deleteCMD01.ExecuteNonQuery();



                                    //get more goods
                                    selectCMD01.Parameters.AddWithValue("@deletedby", varGlob.operID);
                                    selectCMD01.Parameters.AddWithValue("@deleterequestedby", textBox5.Text);
                                    selectCMD01.Parameters.AddWithValue("@deletereason", comboBox1.Text);
                                    selectCMD01.Parameters.AddWithValue("@pbatch", batch);
                                    selectCMD01.ExecuteNonQuery();






                                    deletedBatches.Add(batch);

                                    label276.Visible = true;
                                    dataGridView52.Visible = true;
                                    RecentlyDeletedBatches();

                                    if (result02 >= 1)
                                    {
                                        MessageBox.Show("Notify operations that they need to delete batch " + batch + " from Turboscan.");


                                    }

                                }


                                catch
                                {



                                }


                            }


                            if (result < 1 && result01 < 1)
                            {
                                MessageBox.Show("Batch " + batch + " not found.");



                            }


                            if (result01 >= 1)
                            {
                                MessageBox.Show("Batch " + batch + " has records that have been exported.", "WTF - batch will not be deleted.", MessageBoxButtons.OK, MessageBoxIcon.Error);





                            }
                        }

                        var message = string.Join(Environment.NewLine, deletedBatches);
                        MessageBox.Show(message, "Total batches deleted = " + deletedBatches.Count.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);


                    }

                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.ToString());

                    }

            }

        }

        public void RecentlyDeletedBatches()
        {

            if (radioButton34.Checked)
            {

                using (SqlConnection dataConnection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.DVAConnectionStringFP"].ToString()))

                    try
                    {
                        dataConnection.Open();


                        SqlCommand selectCMD = new SqlCommand(@"select top(100) pbatch BatchName, DeletedBy, DeletedDate, DeletedRequestedBy from PhysicalBatch_bck order by deleteddate desc", dataConnection);



                        DataTable dt = new DataTable();
                        SqlDataAdapter workAdapter = new SqlDataAdapter(selectCMD);
                        workAdapter.Fill(dt);

                        dataGridView52.DataSource = dt;

                        //label276.Visible = true;
                        label276.Text = "Batches recently deleted:";


                    }


                    catch
                    {


                    }

            }


            if (radioButton35.Checked)
            {

                using (SqlConnection dataConnection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["x_Lookup_Lite.Properties.Settings.DVAConnectionStringMTV"].ToString()))

                    try
                    {
                        dataConnection.Open();


                        SqlCommand selectCMD = new SqlCommand(@"select top(100) pbatch BatchName, DeletedBy, DeletedDate, DeletedRequestedBy from PhysicalBatch_bck order by deleteddate desc", dataConnection);



                        DataTable dt = new DataTable();
                        SqlDataAdapter workAdapter = new SqlDataAdapter(selectCMD);
                        workAdapter.Fill(dt);

                        dataGridView52.DataSource = dt;


                        label276.Text = "Batches recently deleted:";


                    }


                    catch
                    {


                    }

            }

        }


        private void button7_Click(object sender, EventArgs e)
        {
            button7.Enabled = false;
            var batchname = textBox4.Text;

            if (textBox4.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter a batchname value");
                textBox4.Text = string.Empty;
                button7.Enabled = true;
            }

            //else if (textBox4.Text.Trim().Length != 14)
            //{
            //    MessageBox.Show("Please enter a valid batchname");
            //    textBox4.Text = string.Empty;
            //    button7.Enabled = true;
            //}

            else
            {
                deleteDCSBatch();
                button7.Enabled = true;
                textBox4.Text = string.Empty;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button7.Enabled = true;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            comboBox1.SelectedIndex = -1;
        }

        private void radioButton35_CheckedChanged(object sender, EventArgs e)
        {
            label248.Text = "Mt. Vernon";
        }

        private void radioButton34_CheckedChanged(object sender, EventArgs e)
        {
            label248.Text = "Forest Park";
        }

    }
}
