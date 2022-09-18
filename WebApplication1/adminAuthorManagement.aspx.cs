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
    public partial class adminAuthorManagement : System.Web.UI.Page
    {

        String strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
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
                if (checkAuthorExists() == true)
                {
                    TextBox3.Text = findAuthorName();
                }
                else
                {
                    fAlert("Author does not exist !", "error", "stay");
                }
            }


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (validateInput() == true)
            {
                if (checkAuthorExists() == true)
                {
                    fAlert("Author already exists !", "warning", "stay");
                }
                else
                {
                    addNewAuthor();
                    GridView1.DataBind();
                    ClearTextBoxes();
                }
            }  
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (validateInput() == true)
            {
                if (checkAuthorExists() == true)
                {
                    updateExistingAuthorName();
                    GridView1.DataBind();
                    ClearTextBoxes();
                    
                }
                else
                {
                    fAlert("Author does not exist !", "error", "stay");

                }
                
            }

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (validateInput() == true)
            {
                if (checkAuthorExists() == true)
                {
                    deleteExistingAuthor();
                    GridView1.DataBind();
                    ClearTextBoxes();
                }
                else
                {
                    fAlert("Author does not exist !", "error", "stay");

                }
                
            }

        }



        private bool checkAuthorExists()
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
                                   "FROM [author_master_tbl]" +
                                   "WHERE author_id=@authorId";

                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {

                        cmd.Parameters.AddWithValue("@authorId", TextBox1.Text);
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

            if(allInputsCorrect == false)
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


        private String findAuthorName()
        {
            String authorNameDb = "Not found";
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    String query1 = "SELECT *" +
                                   "FROM [author_master_tbl]" +
                                   "WHERE author_id=@authorId";

                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {

                        cmd.Parameters.AddWithValue("@authorId", TextBox1.Text);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    authorNameDb = dr.GetValue(1).ToString();


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
            return authorNameDb;
        }

        private void addNewAuthor()
        {

            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    String query1 = "INSERT INTO [author_master_tbl]([author_id],[author_name])" +
                                    "VALUES(@authorId,@authorName)";

                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {

                        cmd.Parameters.AddWithValue("@authorId", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@authorName", TextBox3.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }
                    fAlert("New author added successfully !", "success", "stay");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");

            }

        }


        private void updateExistingAuthorName()
        {

            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    String query1 = "UPDATE [author_master_tbl]" +
                                     "SET [author_name]=@authorName " +
                                     "WHERE [author_id]=@authorId;";
                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {
                        cmd.Parameters.AddWithValue("@authorId",TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@authorName", TextBox3.Text.Trim());
                        
                        cmd.ExecuteNonQuery();
                    }
                    fAlert("Existing author updated successfully !", "success", "stay");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");

            }

        }
        private void deleteExistingAuthor()
        {
            try
            {
                string authorNameDb = "";
                
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlTransaction transaction = con.BeginTransaction();

                    try
                    {

                        //Get the "author_name" using "author_id"
                        String query1 = "SELECT *" +
                                       "FROM [author_master_tbl]" +
                                       "WHERE [author_id]=@authorId;";
                        using (SqlCommand cmd = new SqlCommand(query1, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@authorId", TextBox1.Text.Trim());
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    while (dr.Read())
                                    {
                                        authorNameDb = dr.GetValue(1).ToString();
                                    }
                                }
                            }
                        }

                        if (authorNameDb == TextBox3.Text.Trim())
                        {
                            //Delete Author using "author_id"
                            String query2 = "DELETE FROM [author_master_tbl]" +
                                            "WHERE [author_id]=@authorId;";
                            using (SqlCommand cmd = new SqlCommand(query2, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@authorId", TextBox1.Text.Trim());
                                cmd.ExecuteNonQuery();
                            }
                            fAlert("Existing author deleted successfully !", "success", "stay");
                        }
                        else
                        {
                            fAlert("Author name is incorrect ! Existing author not deleted !", "error", "stay");
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