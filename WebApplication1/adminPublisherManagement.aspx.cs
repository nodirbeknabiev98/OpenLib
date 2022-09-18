using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

using customUsersInputValidation = usersInputValidation;

namespace WebApplication1
{
    public partial class adminPublisherManagement : System.Web.UI.Page
    {
      
        String strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                TextBox1.BorderColor = System.Drawing.Color.LightGray;
                TextBox3.BorderColor = System.Drawing.Color.LightGray;
                
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (validateInput2() == true)
            {
                if (checkPublisherExists() == true)
                {
                    TextBox3.Text = findPublisherName();
                }
                else
                {
                    fAlert("Publisher does not exist !", "error", "stay");
                }
            }


        }

        
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (validateInput() == true)
            {
                if (checkPublisherExists() == true)
                {
                    fAlert("Publisher already exists !", "warning", "stay");
                }
                else
                {
                    addNewPublisher();
                    GridView1.DataBind();
                    ClearTextBoxes();
                }
            }
        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            if (validateInput() == true)
            {
                if (checkPublisherExists() == true)
                {
                    updateExistingPublisherName();
                    GridView1.DataBind();
                    ClearTextBoxes();

                }
                else
                {
                    fAlert("Publisher does not exist !", "error", "stay");
                }

            }

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (validateInput() == true)
            {
                if (checkPublisherExists() == true)
                {
                    deleteExistingPublisher();
                    GridView1.DataBind();
                    ClearTextBoxes();
                }
                else
                {
                    fAlert("Publisher does not exist !", "error", "stay");
                }

            }

        }



        private bool checkPublisherExists()
        {
            bool found = false;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    String query1 = "SELECT *" +
                                   "FROM [publisher_master_tbl]" +
                                   "WHERE publisher_id=@publisherId";

                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {

                        cmd.Parameters.AddWithValue("@publisherId", TextBox1.Text);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    found = true;


                                }

                            }
                        }
                    }
                }
                return found;
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");
                return found;
            }

        }

        private bool validateInput()
        {
            bool allInputsCorrect = true;

            if (customUsersInputValidation.inputValidation.validateUserName(TextBox1.Text) == false)
            {
                TextBox1.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }

            if (customUsersInputValidation.inputValidation.validateFullName(TextBox3.Text) == false)
            {
                TextBox3.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;

            }

            if (allInputsCorrect == false)
            {
                fAlert("Wrong input type! Please, try again.", "error", "stay");
            }

            return allInputsCorrect;

        }


        private bool validateInput2()
        {
            bool allInputsCorrect = true;

            if (customUsersInputValidation.inputValidation.validateUserName(TextBox1.Text) == false)
            {
                TextBox1.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }

            if (allInputsCorrect == false)
            {
                fAlert("Wrong input type! Please, try again.", "error", "stay");
            }

            return allInputsCorrect;

        }


        private String findPublisherName()
        {
            String publisherNameDb = "Not found";
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    String query1 = "SELECT *" +
                                   "FROM [publisher_master_tbl]" +
                                   "WHERE publisher_id=@publisherId";

                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {

                        cmd.Parameters.AddWithValue("@publisherId", TextBox1.Text);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    publisherNameDb = dr.GetValue(1).ToString();


                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");
            }
            return publisherNameDb;
        }


        private void addNewPublisher()
        {

            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    String query1 = "INSERT INTO [publisher_master_tbl]([publisher_id],[publisher_name])" +
                                    "VALUES(@publisherId,@publisherName)";

                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {

                        cmd.Parameters.AddWithValue("@publisherId", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@publisherName", TextBox3.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }
                    fAlert("New publisher added successfully !", "success", "stay");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");

            }

        }


        private void updateExistingPublisherName()
        {

            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    String query1 = "UPDATE [publisher_master_tbl]" +
                                     "SET [publisher_name]=@publisherName " +
                                     "WHERE [publisher_id]=@publisherId;";
                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {
                        cmd.Parameters.AddWithValue("@publisherId", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@publisherName", TextBox3.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }
                    fAlert("Existing publisher updated successfully !", "success", "stay");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");

            }

        }

        private void deleteExistingPublisher()
        {
            try
            {
                string publisherNameDb = "";

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlTransaction transaction = con.BeginTransaction();

                    try
                    {

                        //Get the "publisher_name" using "publisher_id"
                        String query1 = "SELECT *" +
                                       "FROM [publisher_master_tbl]" +
                                       "WHERE [publisher_id]=@publisherId;";
                        using (SqlCommand cmd = new SqlCommand(query1, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@publisherId", TextBox1.Text.Trim());
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    while (dr.Read())
                                    {
                                         publisherNameDb = dr.GetValue(1).ToString();
                                    }
                                }
                            }
                        }

                        if (publisherNameDb == TextBox3.Text.Trim())
                        {
                            //Delete Author using "publisher_id"
                            String query2 = "DELETE FROM [publisher_master_tbl]" +
                                            "WHERE [publisher_id]=@publisherId;";
                            using (SqlCommand cmd = new SqlCommand(query2, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@publisherId", TextBox1.Text.Trim());
                                cmd.ExecuteNonQuery();
                            }
                            fAlert("Existing publisher deleted successfully !", "success", "stay");
                        }
                        else
                        {
                            fAlert("Publisher name is incorrect ! Existing publisher not deleted ! ", "error", "stay");
                        }


                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script> alert(' " + ex.Message + "');</script>");
                        transaction.Rollback();
                    }

                }


            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");

            }
        }


        private void ClearTextBoxes()
        {
            TextBox1.Text = "";
            TextBox3.Text = "";

        }



        private void fAlert(string message, string type, string url)
        {
            //Example: fAlert("User status was NOT UPD!", "error", "stay");
            //message can be anything 
            //type can be "success","error", "warning","info","question"
            //if url == "stay",then there will no redirection to other pages.
            String funcBuild = "fAlertFront(" + "'" + message + "'" + "," + "'" + type + "'" + "," + "'" + url + "'" + ")";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "randomText", funcBuild, true); //AJAX call to JS function errMsg() in front
        }





    }
}