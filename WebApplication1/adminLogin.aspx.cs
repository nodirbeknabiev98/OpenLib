using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Configuration;
using System.Data.SqlClient;
using System.Data;

using customPasswordHash = passwordHash;
using customUsersInputValidation = usersInputValidation;

namespace WebApplication1
{
    public partial class adminLogin : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (IsPostBack)
            {
                TextBox1.BorderColor = System.Drawing.Color.LightGray;
                TextBox2.BorderColor = System.Drawing.Color.LightGray;
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (validateInput() == true)
            {
                LoginWithPasswordHashFunction();
            }
        }


        // My functions

        private void LoginWithPasswordHashFunction()
        {
            try
            {


                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("SELECT *" +
                                           "FROM [admin_login_tbl]" +
                                           "WHERE admin_id =@aname;", con);
                    cmd.Parameters.AddWithValue("@aname", TextBox1.Text);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                String slowHashSaltDb = dr.GetValue(2).ToString();

                                if (!String.IsNullOrEmpty(slowHashSaltDb))
                                {
                                    bool validAdmin = customPasswordHash.PasswordStorage.VerifyPassword(TextBox2.Text, slowHashSaltDb);

                                    if (validAdmin == true)
                                    {
                                        //Session Var
                                        Session["adminname"] = dr.GetValue(0).ToString();
                                        Session["fullname"] = dr.GetValue(2).ToString();
                                        Session["role"] = "admin";
                                        fAlert("Login Successful!", "success", "homepage.aspx");

                                    }
                                    else
                                    {
                                        fAlert("Invalid Admin Name or Password! Please, try again.", "error", "stay");
                                    }


                                }
                            }

                        }
                        else
                        {
                            fAlert("Invalid Admin Name or Password! Please, try again. ", "error", "stay");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");
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
            if (customUsersInputValidation.inputValidation.validatePassword(TextBox2.Text) == false)
            {
                TextBox2.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (allInputsCorrect == false)
            {
                fAlert("Input type is incorrect !", "error", "stay");
            }

            return allInputsCorrect;

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




