using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.ComponentModel.Design;
using hbehr.recaptcha;

using customUsersInputValidation = usersInputValidation;
using customPasswordHash = passwordHash; 

namespace WebApplication1
{

    public partial class signUpUser : System.Web.UI.Page
    {

        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                TextBox1.BorderColor = System.Drawing.Color.LightGray;
                TextBox2.BorderColor = System.Drawing.Color.LightGray;
                TextBox3.BorderColor = System.Drawing.Color.LightGray;
                TextBox4.BorderColor = System.Drawing.Color.LightGray;
                TextBox5.BorderColor = System.Drawing.Color.LightGray;
                TextBox6.BorderColor = System.Drawing.Color.LightGray;
                TextBox7.BorderColor = System.Drawing.Color.LightGray;
                TextBox8.BorderColor = System.Drawing.Color.LightGray;
                TextBox9.BorderColor = System.Drawing.Color.LightGray;
                DropDownList1.BorderColor = System.Drawing.Color.LightGray;
                
            }
            

        }


        //Sign Up OnClick Event Part
        protected void Button1_Click(object sender, EventArgs e)
        {
            if(validateInput() == true)
            {
                if (checkUserExists() == true)
                {
                    fAlert("Something went wrong! Try other user name !", "warning", "stay");
                }
                else
                {
                    try
                    {
                        string userResponse = Request.Params["g-recaptcha-response"];
                        bool validCaptcha = ReCaptcha.ValidateCaptcha(userResponse);
                        if (validCaptcha)
                        {
                            // Real User, validated !
                            signUpNewUserWithSlowHash();
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
            
        }


        //Supporting Methods 

        private bool checkUserExists()
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
                                                "WHERE member_id=@userid ;",con);

                cmd.Parameters.AddWithValue("@userid", TextBox8.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                
                DataTable dt = new DataTable();
                da.Fill(dt);

                if(dt.Rows.Count >= 1)
                {
                    con.Close();
                    return true;
                }
                else
                {
                    con.Close();
                    return false;
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('"+ex.Message+"'); </script>");
                
                return false;

            }

        }

        private bool validateInput()
        {
            bool allInputsCorrect = true;

            if (customUsersInputValidation.inputValidation.validateFullName(TextBox1.Text) == false)
            {
                TextBox1.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateDate(TextBox2.Text) == false)
            {
                TextBox2.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validatePhoneNumber(TextBox3.Text) == false)
            {
                TextBox3.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateEmail(TextBox4.Text) == false)
            {
                TextBox4.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateCityName(TextBox5.Text) == false)
            {
                TextBox5.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validatePostalCode(TextBox6.Text) == false)
            {
                TextBox6.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateFullAddress(TextBox7.Text) == false)
            {
                TextBox7.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateUserName(TextBox8.Text) == false)
            {
                TextBox8.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validatePassword(TextBox9.Text) == false)
            {
                TextBox9.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateCityName(DropDownList1.SelectedValue) == false)
            {
                DropDownList1.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }



            if (allInputsCorrect == false)
            {
                fAlert("Wrong input type! Please, try again.", "error", "stay");
            }

            return allInputsCorrect;

        }


        private void signUpNewUserWithSlowHash()
        {
            try
            {
                // format=> saltHashReturned => algorithm:iterations:hashSize:salt:hash
                string saltHashReturned = customPasswordHash.PasswordStorage.CreateHash(TextBox9.Text.Trim());

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO member_master_tbl(full_name,dob,phone_number,email,state,city,postal_code,full_address,member_id,password,account_status)" +
                                                "VALUES (@full_name,@dob,@phone_number,@email,@state,@city,@postal_code,@full_address,@member_id,@password, @account_status)", con);

                cmd.Parameters.AddWithValue("@full_name", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@dob", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@phone_number", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@email", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@state", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@city", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@postal_code", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@full_address", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@member_id", TextBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@password", saltHashReturned);
                cmd.Parameters.AddWithValue("@account_status", "Pending");


                cmd.ExecuteNonQuery();
                con.Close();
                fAlert("Registration Completed! Thank you!", "success", "homepage.aspx");

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

