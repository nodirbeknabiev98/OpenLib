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
using System.Net;
using System.Net.Mail;
using System.Text;


using System.Security.Cryptography;
using System.ComponentModel.Design;

using customUniqueKey = UniqueKey;
using customPasswordHash = passwordHash;
using customUsersInputValidation = usersInputValidation;

using System.Runtime.Remoting.Messaging;
using hbehr.recaptcha;

namespace WebApplication1
{
    public partial class resetPassword : System.Web.UI.Page
    {
        String strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                TextBox1.BorderColor = System.Drawing.Color.LightGray;
            }
        }

        protected void Button1_Click(object sender, EventArgs e) // Main logic function
        {
            if (validateInput() == true)
            {
                string userResponse = Request.Params["g-recaptcha-response"];
                bool validCaptcha = ReCaptcha.ValidateCaptcha(userResponse);
                if (validCaptcha)
                {
                    // Real User, validated !
                    if (hasAlreadySent() == true)
                    {
                        Button1.Visible = false;
                        Label1.Text = "Password reset link has been sent already and still valid. Please go to your e-mail account to proceed further.";
                        Label1.ForeColor = System.Drawing.Color.Red;
                        Label1.Visible = true;
                    }
                    else
                    {
                        doPasswordResetEmail();
                    }
                }
                else
                {
                    // Bot Attack, non validated !
                    fAlert("You've failed CAPTCHA test....", "error", "stay");
                }

            } 

        }



        private void doPasswordResetEmail()
        {
            try
            {
                String emailDb = "";
                String memberIdDb = "";
                String phoneNumDb = "";
                bool found = false;

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    String query1 = "SELECT *" +
                                  "FROM [member_master_tbl]" +
                                  "WHERE member_id=@uname;";
                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {
                        cmd.Parameters.AddWithValue("@uname", TextBox1.Text);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if(dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    phoneNumDb = dr.GetValue(2).ToString();
                                    memberIdDb = dr.GetValue(8).ToString();
                                    emailDb = dr.GetValue(3).ToString();
                                    found = true;
                                }
                            }
                            else
                            {
                                fAlert("This username does not exist! Please, try again. ", "error", "stay");
                                TextBox1.BorderColor = System.Drawing.Color.Red;
                            }

                        }
                    }

                    if (found == true)
                    {
                        String uniqueSig = createUniqueString();
                        String resetID = createUniqueString();
                        String uniqueSigHashed = customPasswordHash.PasswordStorage.CreateHash(uniqueSig);

                        DateTime linkReq = DateTime.Now;
                        DateTime linkExp = linkReq.AddDays(1);

                        if (SendPasswordResetEmail(emailDb, memberIdDb, uniqueSig, resetID) == true)
                        {
                            String query2 = "INSERT INTO [password_reset_tbl](userSignature,member_id,phone_number,reset_id,linkReq,linkExp)" +
                                   "VALUES (@userSignature,@member_id,@phone_number,@reset_id,@linkReq,@linkExp)";
                            using (SqlCommand cmd = new SqlCommand(query2, con))
                            {
                                cmd.Parameters.AddWithValue("@userSignature", uniqueSigHashed);
                                cmd.Parameters.AddWithValue("@member_id", memberIdDb);
                                cmd.Parameters.AddWithValue("@phone_number", phoneNumDb);
                                cmd.Parameters.AddWithValue("@reset_id", resetID);
                                cmd.Parameters.AddWithValue("@linkReq", linkReq);
                                cmd.Parameters.AddWithValue("@linkExp", linkExp);
                                cmd.ExecuteNonQuery();
                            }

                        }  
                    }

                }

            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");
            }
            
        }


        private bool SendPasswordResetEmail(String toEmail, String userID, String uniqueSig, String resetID) //Helper Function for "doPasswordResetEmail()"
        {
            try
            {
                MailMessage mailMessage = new MailMessage(ConfigurationManager.AppSettings["emailSmtpClient"].ToString(), toEmail);
                

                //StringBuilder class is present in System.Text namespace
                StringBuilder sbEmailBody = new StringBuilder();
                sbEmailBody.Append("Dear " + userID + ",<br/><br/>");
                sbEmailBody.Append("Please click on the following link to reset your password");
                sbEmailBody.Append("<br/>");
                sbEmailBody.Append("https://localhost:44378/resetPasswordChange.aspx" + "?rid="+ resetID +"&usig=" + uniqueSig);
                sbEmailBody.Append("<br/><br/>");
                sbEmailBody.Append("<b>Best Regards,</b>");
                sbEmailBody.Append("<br/>");
                sbEmailBody.Append("<b>Open-Lib Administration</b>");

                mailMessage.IsBodyHtml = true;

                mailMessage.Body = sbEmailBody.ToString();
                mailMessage.Subject = "Reset Your Password";
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.UseDefaultCredentials = false;
              

                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = ConfigurationManager.AppSettings["emailSmtpClient"].ToString(),
                    Password = ConfigurationManager.AppSettings["passwordSmtpClient"].ToString()
                };


                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;

                smtpClient.Send(mailMessage);
               
                Button1.Visible = false;
                Label1.Text = "Password reset intructions has been sent to the e-mail address of the specified user!";
                Label1.ForeColor = System.Drawing.Color.Green;
                Label1.Visible = true;

                return true;

            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");

                Label1.Text = "Could not send password reset instruction! We will send the reset link as soon as it will be available. Go and check your email, if you did not receive the reset link then try again later. Otherwise, proceed further.";
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Visible = true;

                return false;
            }
            
        }

        private String createUniqueString() //Helper Function for "doPasswordResetEmail()"
        {
            String randString = customUniqueKey.KeyGenerator.GetUniqueKey(25);
            //String uniqueSig = PasswordStorage.CreateHash(randString);

            return randString;
        }



        private bool validateInput()
        {
            bool allInputsCorrect = true;
            if (customUsersInputValidation.inputValidation.validateUserName(TextBox1.Text) == false)
            {
                TextBox1.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (allInputsCorrect == false)
            {
                fAlert("Wrong input type! Please, try again. ", "error", "stay");
            }

            return allInputsCorrect;

        }

        



        private bool hasAlreadySent()
        {
            bool sent = false;

            using (SqlConnection con = new SqlConnection(strcon))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                String query1 = "SELECT *" +
                               "FROM [password_reset_tbl]" +
                               "WHERE member_id=@userId AND linkExp > @dateTimeNow";

                using (SqlCommand cmd = new SqlCommand(query1, con))
                {

                    cmd.Parameters.AddWithValue("@userId", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@dateTimeNow", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            { 
                                sent = true;
                            }
                        }
                    }
                }
            }
            return sent;
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


