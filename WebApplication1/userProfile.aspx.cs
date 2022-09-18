using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

using System.IO;

using customUsersInputValidation = usersInputValidation;
using customPasswordHash = passwordHash;

namespace WebApplication1
{
    public partial class UserProfile : System.Web.UI.Page
    {
        String strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["username"] = "nabiev_n";

            try
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
                }

                if (Session["username"].ToString() == "" || Session["username"] == null)
                {
                    TextBox1.ReadOnly = true;
                    TextBox2.ReadOnly = true;
                    TextBox3.ReadOnly = true;
                    TextBox4.ReadOnly = true;
                    TextBox5.ReadOnly = true;
                    TextBox6.ReadOnly = true;
                    TextBox7.ReadOnly = true;
                    TextBox8.ReadOnly = true;
                    TextBox9.ReadOnly = true;
                    Button1.Enabled = false;
                    DropDownList1.Enabled = false;
                    fAlert("Session Expired! Login Again. You will be redirected to Login Page automatically. Press OK.", "error", "userLogin.aspx");

                }
                else
                {

                    if (!Page.IsPostBack)
                    {

                        findAllUserData();
                        findIssuedBookData();
                    }

                }
            }
            catch (Exception ex)
            {
                
                //Response.Write("<script> alert(' " + ex.Message + "');</script>");
                fAlert("Session Expired or smth went wrong! Login Again. You will be redirected to Login Page automatically. Press OK.", "error", "userLogin.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
            if (checkAccStatus() == true)
            {
                if (validateInput() == true)
                {
                    updateUserData();
                    findAllUserData();
                    findIssuedBookData();

                }
            }
            else
            {
                fAlert("You cant update! Your account status is " + Label1.Text.ToString() + "", "error", "stay");
            }

        }


        private bool checkAccStatus()
        {
            bool active = false;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    String query1 = "SELECT [account_status]" +
                                   "FROM [member_master_tbl]" +
                                   "WHERE [member_id]=@memberId";

                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {
                        cmd.Parameters.AddWithValue("@memberId", Session["username"]);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);

                                if(dt.Rows.Count >= 1)
                                {
                                    if (dt.Rows[0][0].ToString() == "Active")
                                    {
                                        active = true;
                                    }

                                }

                            }

                        }
                    }
                }
                return active;
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");
                return active;
            }
        }




        private void findIssuedBookData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    String query1 = "SELECT *" +
                                   "FROM [book_issue_tbl]" +
                                   "WHERE [member_id]=@memberId";

                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {
                        cmd.Parameters.AddWithValue("@memberId", Session["username"]);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);

                                if (dt.Rows.Count >= 1)
                                {
                                    GridView1.DataSource = dt;
                                    GridView1.DataBind();
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

        }



        private void findAllUserData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    String query1 = "SELECT *" +
                                   "FROM [member_master_tbl]" +
                                   "WHERE member_id=@memberId";

                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {
                        cmd.Parameters.AddWithValue("@memberId", Session["username"]);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    TextBox1.Text = dr.GetValue(0).ToString().Trim(); //Full name
                                    TextBox2.Text = dr.GetValue(2).ToString().Trim(); //Contact
                                    TextBox3.Text = dr.GetValue(1).ToString().Trim(); //DOB
                                    TextBox4.Text = dr.GetValue(3).ToString().Trim(); //Email
                                    TextBox5.Text = dr.GetValue(7).ToString().Trim(); //Full Address
                                    TextBox6.Text = dr.GetValue(5).ToString().Trim(); //City
                                    TextBox7.Text = dr.GetValue(6).ToString().Trim(); //Postal Code
                                    TextBox8.Text = dr.GetValue(8).ToString().Trim(); //User Name (UserID)

                                    Label1.Text = dr.GetValue(10).ToString().Trim(); 

                                    if(Label1.Text == "Active")
                                    {
                                        Label1.Attributes.Add("class","badge badge-pill badge-success" );
                                    }
                                    else if (Label1.Text == "Pending")
                                    {
                                        Label1.Attributes.Add("class", "badge badge-pill badge-warning");
                                    }
                                    else if (Label1.Text == "Blocked")
                                    {
                                        Label1.Attributes.Add("class", "badge badge-pill badge-danger");
                                    }
                                    else
                                    {
                                        Label1.Attributes.Add("class", "badge badge-pill badge-info");

                                    }

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
        }



        private void updateUserData()
        {
            try
            {
                String listToUpdate = "";
                bool wantToUpdatePass = false;
                bool wantToUpdateFullName = false; // To update book_issue table Full name coloumn


                String showToUserList = "";

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlTransaction transaction = con.BeginTransaction();

                    try
                    {
                        String query1 = "SELECT *" +
                                    "FROM [member_master_tbl]" +
                                    "WHERE member_id=@memberId";

                        using (SqlCommand cmd = new SqlCommand(query1, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@memberId", Session["username"]);
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    while (dr.Read())
                                    {

                                        if (TextBox1.Text != dr.GetValue(0).ToString().Trim())
                                        {
                                            listToUpdate += "[full_name]=@fullName,";
                                        }

                                        if (TextBox2.Text != dr.GetValue(2).ToString().Trim())
                                        {
                                            listToUpdate += "[phone_number]=@phoneNumber,";
                                        }

                                        if (TextBox3.Text != dr.GetValue(1).ToString().Trim())
                                        {
                                            listToUpdate += "[dob]=@dob,";
                                        }

                                        if (TextBox4.Text != dr.GetValue(3).ToString().Trim())
                                        {
                                            listToUpdate += "[email]=@email,";
                                        }

                                        if (TextBox5.Text != dr.GetValue(7).ToString().Trim())
                                        {
                                            listToUpdate += "[full_address]=@fullAddress,";
                                        }

                                        if (TextBox6.Text != dr.GetValue(5).ToString().Trim())
                                        {
                                            listToUpdate += "[city]=@city,";
                                        }

                                        if (TextBox7.Text != dr.GetValue(6).ToString().Trim())
                                        {
                                            listToUpdate += "[postal_code]=@postalCode,";
                                        }

                                        if (DropDownList1.SelectedValue != dr.GetValue(4).ToString().Trim())
                                        {
                                            listToUpdate += "[state]=@state,";

                                        }
                                        if (TextBox9.Text.Trim() != "")
                                        {
                                            wantToUpdatePass = true;
                                        }
                                    }
                                }
                            }
                        }

                        if (listToUpdate != "")
                        {
                            listToUpdate += "[account_status]=@accountStatus,";

                            listToUpdate = listToUpdate.Remove(listToUpdate.Length - 1);

                            String query2 = "UPDATE [member_master_tbl]" +
                                     "SET " + listToUpdate + " " +
                                     "WHERE [member_id]=@memberId;";

                            //Response.Write("<script> alert('" + query2 + "');</script>");

                            using (SqlCommand cmd = new SqlCommand(query2, con, transaction))
                            {
                                if (listToUpdate.Contains("@fullName"))
                                {
                                    cmd.Parameters.AddWithValue("@fullName", TextBox1.Text.Trim());
                                    showToUserList += "Full name,";
                                    wantToUpdateFullName = true;
                                }
                                if (listToUpdate.Contains("@phoneNumber"))
                                {
                                    cmd.Parameters.AddWithValue("@phoneNumber", TextBox2.Text.Trim());
                                    showToUserList += "Phone Number,";

                                }
                                if (listToUpdate.Contains("@dob"))
                                {
                                    cmd.Parameters.AddWithValue("@dob", TextBox3.Text.Trim());
                                    showToUserList += "Date of Birth,";
                                }
                                if (listToUpdate.Contains("@email"))
                                {
                                    cmd.Parameters.AddWithValue("@email", TextBox4.Text.Trim());
                                    showToUserList += "Email,";
                                }
                                if (listToUpdate.Contains("@fullAddress"))
                                {
                                    cmd.Parameters.AddWithValue("@fullAddress", TextBox5.Text.Trim());
                                    showToUserList += "Full address,";
                                }
                                if (listToUpdate.Contains("@city"))
                                {
                                    cmd.Parameters.AddWithValue("@city", TextBox6.Text.Trim());
                                    showToUserList += "City,";
                                }
                                if (listToUpdate.Contains("@postalCode"))
                                {
                                    cmd.Parameters.AddWithValue("@postalCode", TextBox7.Text.Trim());
                                    showToUserList += "Postal Code,";
                                }
                                if (listToUpdate.Contains("@state"))
                                {
                                    cmd.Parameters.AddWithValue("@state", DropDownList1.SelectedItem.Value);
                                    showToUserList += "State,";
                                }

                                cmd.Parameters.AddWithValue("@accountStatus", "Pending");

                                cmd.Parameters.AddWithValue("@memberId", Session["username"]);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        if (wantToUpdateFullName == true)
                        {

                            String query3 = "UPDATE [book_issue_tbl]" +
                                      "SET [member_name] = @memberName " +
                                      "WHERE [member_id]=@memberId;";

                            using (SqlCommand cmd = new SqlCommand(query3, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@memberId", Session["username"]);
                                cmd.Parameters.AddWithValue("@memberName", TextBox1.Text.Trim());
                                cmd.ExecuteNonQuery();
                            }

                        }
                        transaction.Commit();
                    }

                    catch (Exception ex)
                    {
                        Response.Write("<script> alert(' " + ex.Message + "');</script>");
                        showToUserList = "";
                        wantToUpdatePass = false;
                        wantToUpdateFullName = false;
                        transaction.Rollback();

                    }
                }




                if (wantToUpdatePass == true)
                {
                    if (customUsersInputValidation.inputValidation.validatePassword(TextBox9.Text) == true)
                    {
                        if (updateExistingUserPasswordWithSlowHash() == true)
                        {
                            showToUserList += "Password,";
                        }
                    }
                    else
                    {
                        TextBox9.BorderColor = System.Drawing.Color.Red;
                        fAlert("Wrong input type! Please, try again.", "error", "stay");
                    }
                }

                if (showToUserList != "")
                {
                    showToUserList = showToUserList.Remove(showToUserList.Length - 1);

                    fAlert("You have updated: " + showToUserList + "", "success", "stay");


                }
                else
                {
                    if (wantToUpdatePass == false)
                    {
                        fAlert("You did not enter any changes or smth went wrong!", "error", "stay");
                    }
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");
                fAlert("User status was NOT UPD!", "error", "stay");
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
            if (customUsersInputValidation.inputValidation.validateDate(TextBox3.Text) == false)
            {
                TextBox3.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validatePhoneNumber(TextBox2.Text) == false)
            {
                TextBox2.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateEmail(TextBox4.Text) == false)
            {
                TextBox4.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateCityName(TextBox6.Text) == false)
            {
                TextBox6.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validatePostalCode(TextBox7.Text) == false)
            {
                TextBox7.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateFullAddress(TextBox5.Text) == false)
            {
                TextBox5.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateUserName(TextBox8.Text) == false)
            {
                TextBox8.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (allInputsCorrect == false)
            {
                fAlert("Wrong input type! Please, try again.", "error", "stay");
            }

            return allInputsCorrect;

        }

        private void ClearAllTextBoxes()
        {
            TextBox1.Text = ""; 
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DateTime dt = Convert.ToDateTime(e.Row.Cells[5].Text);
                    DateTime today = DateTime.Today;
                    if (today >= dt)
                    {
                        e.Row.BackColor = System.Drawing.Color.PaleVioletRed;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        private bool updateExistingUserPasswordWithSlowHash()
        {
            try
            {
                // format=> saltHashReturned => algorithm:iterations:hashSize:salt:hash
                string saltHashReturned = customPasswordHash.PasswordStorage.CreateHash(TextBox9.Text.Trim());


                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    String query1 = "UPDATE [member_master_tbl]" +
                                     "SET [password]=@password " +
                                     "WHERE [member_id]=@memberId;";
                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {
                        cmd.Parameters.AddWithValue("@password", saltHashReturned);
                        cmd.Parameters.AddWithValue("@memberId", TextBox8.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");
                return false;
            }
        }


        private void fAlert(string message,string type,string url)
        {
            //Example: fAlert("User status was NOT UPD!", "error", "stay");
            //message can be anything 
            //type can be "success","error", "warning","info","question"
            //if url == "stay",then there will no redirection to other pages.
            String funcBuild = "fAlertFront(" + "'" + message + "'" + "," + "'" + type + "'" + ","  + "'" + url + "'" + ")";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),"randomText",funcBuild, true); //AJAX call to JS function errMsg() in front
        }

    }
}