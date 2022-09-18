using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using hbehr.recaptcha;

using customPasswordHash = passwordHash;
using customUsersInputValidation = usersInputValidation;

namespace WebApplication1
{
    public partial class userLogin : System.Web.UI.Page
    {
        String strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        
        protected void Page_Load(object sender, EventArgs e)
        {


            if (IsPostBack)
            {
                TextBox1.BorderColor = System.Drawing.Color.LightGray;
                TextBox2.BorderColor = System.Drawing.Color.LightGray;
            }


            if (!IsPostBack)
            {
                if(Request.Cookies["usernamecookies"] != null)
                {
                    TextBox1.Text = Request.Cookies["usernamecookies"].Value;
                }
            }

        }

        //User Login Onlick Event Part
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (validateInput() == true)
            {
                try
                {
                    string userResponse = Request.Params["g-recaptcha-response"];
                    bool validCaptcha = ReCaptcha.ValidateCaptcha(userResponse);
                    if (validCaptcha)
                    {
                        // Real User, validated !
                        LoginWithPasswordHashFunction();
                    }
                    else
                    {
                        // Bot Attack, non validated !
                        fAlert("You've failed CAPTCHA TEST", "error", "stay");
                    }
                }
                catch (Exception ex)
                {
                    fAlert("You should pass RECAPTCHA TEST!", "error", "stay");

                }
            }
        }

        // My functions
       
        private void LoginWithPasswordHashFunction()
        {
            try
            {
                
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT *" +
                                                "FROM [member_master_tbl]" +
                                                "WHERE member_id=@uname;", con);
                cmd.Parameters.AddWithValue("@uname", TextBox1.Text);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        String slowHashSaltDb = dr.GetValue(9).ToString();

                        if (!String.IsNullOrEmpty(slowHashSaltDb))
                        {
                            bool validUser = customPasswordHash.PasswordStorage.VerifyPassword(TextBox2.Text, slowHashSaltDb);

                            if (validUser == true)
                            {
                                //Session Var
                                Session["username"] = dr.GetValue(8).ToString();
                                //Session["fullname"] = dr.GetValue(0).ToString();
                                Session["role"] = "user";
                                Session["status"] = dr.GetValue(10).ToString();
                                //Cookies Var
                                if (CheckBox1.Checked)
                                {
                                    Response.Cookies["usernamecookies"].Value = dr.GetValue(8).ToString();
                                    Response.Cookies["usernamecookies"].Expires = DateTime.Now.AddMinutes(20);
                                }
                                else
                                {
                                    Response.Cookies["usernamecookies"].Expires = DateTime.Now.AddMinutes(-1);
                                }

                                fAlert("Login Successful!", "success", "homepage.aspx");
                            }
                            else
                            {
                                fAlert("Invalid User Name or Password! Please, try again.", "error", "stay");
                            }

                            
                        }   
                    }
                    

                }
                else
                {
                    fAlert("Invalid User Name or Password! Please, try again.", "error", "stay");
                }
                dr.Close();

            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");
            }

        }

        private bool validateInput()
        {
            //You can add your custom 'White List' Validation Code to make user input validation more strict.
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

