using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;

using System.Security.Cryptography;
using System.ComponentModel.Design;

using customPasswordHash = passwordHash;
using customUsersInputValidation = usersInputValidation;


namespace WebApplication1
{
    public partial class resetPasswordChange : System.Web.UI.Page
    {
        String strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

                if (isPasswordLinkValid() == true)
                {
                    TextBox1.ReadOnly = false;
                    Button1.Visible = true;
                }
                else 
                {
                    Label1.Text = "Your Password Reset Link Expired or Wrong Link was Used!";
                    Label1.ForeColor = System.Drawing.Color.Red;
                    Label1.Visible = true;
                }

            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (validateInput() == true)
            {
                if (phoneNumExists() == true)
                {
                    TextBox1.BorderColor = System.Drawing.Color.LightGray;
                    TextBox1.ReadOnly = true;
                    TextBox2.ReadOnly = false;
                    TextBox3.ReadOnly = false;
                    Button1.Visible = false;
                    Button2.Visible = true;

                }
                else
                {
                    fAlert("Wrong Phone Number! Please try again!", "error", "stay");
                    TextBox1.BorderColor = System.Drawing.Color.Red;

                }
            }
            
        }

        protected void Button2_Click(object sender, EventArgs e) 
        {
            if (validateInput2() == true)
            {
                resetPassword();
                Response.AddHeader("REFRESH", "5;URL=homepage.aspx");

            }
            
        }




        // Checking two conditions: 1) Valid User Signature? ; 2) Not Expired Link ?;
        private bool isPasswordLinkValid()
        {
            bool validLink = false;

            try
            {
                if (Request.QueryString["usig"] != null)
                {
                    bool found = false;
                    String usigDb = "";

                    using (SqlConnection con = new SqlConnection(strcon))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        String query1 ="SELECT *" +
                                       "FROM [password_reset_tbl]" +
                                       "WHERE reset_id=@resetId";

                        using (SqlCommand cmd = new SqlCommand(query1, con))
                        {

                            cmd.Parameters.AddWithValue("@resetId", Request.QueryString["rid"]);
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    while (dr.Read())
                                    {
                                        usigDb = dr.GetValue(0).ToString();
                                        found = customPasswordHash.PasswordStorage.VerifyPassword(Request.QueryString["usig"], usigDb);

                                        if (found == true)
                                        {
                                            DateTime dateTimeNow = DateTime.Now;
                                            DateTime linkExp =  (DateTime)dr.GetValue(5);

                                            if (linkExp> dateTimeNow)
                                            {
                                                validLink = true;
                                            }
                                            else
                                            {
                                                fAlert("Link Expired", "error", "stay");
                                            }
                                           
                                        }
                                        else
                                        {
                                            fAlert("Wrong usig!", "error", "stay");
                                        }
                                    }
                                }
                                else
                                {
                                    fAlert("Wrong uid or already expired link!", "error", "stay");
                                }

                            }
                        }


                    }
                }
                else
                {
                    fAlert("Invalid User Signature!", "error", "stay");
                }

                return validLink;

            }
            catch(Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");
                return validLink;
            }

        }

        private bool validateInput()
        {
            bool allInputsCorrect = true;
            
            if (customUsersInputValidation.inputValidation.validatePhoneNumber(TextBox1.Text) == true)
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

        private bool validateInput2()
        {
            bool allInputsCorrect = true;
           

            if (customUsersInputValidation.inputValidation.validatePassword(TextBox2.Text) == false)
            {
                TextBox2.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validatePassword(TextBox3.Text) == false)
            {
                TextBox3.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (allInputsCorrect == false)
            {
                fAlert("Wrong input type! Please, try again.", "error", "stay");
            }

            if (TextBox2.Text != TextBox3.Text)
            {
                fAlert("Passwords should match! Please try again.", "info", "stay");
                TextBox2.BorderColor = System.Drawing.Color.Red;
                TextBox3.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            
            return allInputsCorrect;

        }

        private bool phoneNumExists()
        {
            bool exists = false;

            try
            {

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    String query1 = "SELECT *" +
                                   "FROM [password_reset_tbl]" +
                                    "WHERE reset_id=@resetId;";

                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {
                        cmd.Parameters.AddWithValue("@resetId", Request.QueryString["rid"]);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    if(dr.GetValue(2).ToString() == TextBox1.Text)
                                    {
                                        exists = true;
                                    }
                                    
                                }
                            }
                        }
                    }

                }


                return exists;

            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");
                return exists;
            }

           
        }

        protected void resetPassword()
        {
            try
            {
                string memberIdDb = "";

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlTransaction transaction = con.BeginTransaction();

                    try 
                    {

                        //Get the "member_id" using "rid"
                        String query1 = "SELECT *" +
                                       "FROM [password_reset_tbl]" +
                                        "WHERE reset_id=@resetId;";
                        using (SqlCommand cmd = new SqlCommand(query1, con,transaction))
                        {
                            cmd.Parameters.AddWithValue("@resetId", Request.QueryString["rid"]);
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    while (dr.Read())
                                    {
                                        memberIdDb = dr.GetValue(1).ToString();
                                    }
                                }
                            }
                        }


                        //Delete user's data from "password_reset_tbl" because we don't need it anymore
                        String query2 = "DELETE FROM [password_reset_tbl]" +
                                        "WHERE reset_id=@resetId;";
                        using (SqlCommand cmd = new SqlCommand(query2, con,transaction))
                        {
                            cmd.Parameters.AddWithValue("@resetId", Request.QueryString["rid"]);
                            cmd.ExecuteNonQuery();
                        }


                        //Update "member_master_tbl" using "member_id"
                        String query3 = "UPDATE [member_master_tbl]" +
                                        "SET password=@passW " +
                                        "WHERE member_id=@memberId;";
                        using (SqlCommand cmd = new SqlCommand(query3, con,transaction))
                        {
                            // format=> saltHashReturned => algorithm:iterations:hashSize:salt:hash
                            string saltHashReturned = customPasswordHash.PasswordStorage.CreateHash(TextBox2.Text.Trim());

                            cmd.Parameters.AddWithValue("@passW", saltHashReturned);
                            cmd.Parameters.AddWithValue("@memberId", memberIdDb);
                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();


                    }

                    catch (Exception ex)
                    {
                        Response.Write("<script> alert(' " + ex.Message + "');</script>");
                        transaction.Rollback();

                    }  
                }
                Button2.Visible = false;
                TextBox2.BorderColor = System.Drawing.Color.LightGray;
                TextBox3.BorderColor = System.Drawing.Color.LightGray;
                TextBox2.ReadOnly = true;
                TextBox3.ReadOnly = true;
                Label2.Text = "Your password has been successfully changed! Redirecting to home page ....";
                Label2.ForeColor = System.Drawing.Color.Green;
                Label2.Visible = true;



            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");
               
            }

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