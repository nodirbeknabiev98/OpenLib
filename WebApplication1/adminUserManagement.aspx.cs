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
    public partial class adminUserManagement : System.Web.UI.Page
    {

        String strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                TextBox1.BorderColor = System.Drawing.Color.LightGray;

            }

        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            if (validateInput() == true)
            {
                if (checkUserExists() == true)
                {
                    
                    findAllUserData();

                    GridView1.DataBind();
                    

                }
                else
                {
                    fAlert("User does not exist !", "error", "stay");
                    ClearAllTextBoxes();

                }

            }



        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            if (validateInput() == true)
            {
                if (checkUserExists() == true)
                {

                    updateUserStatus("Active");
                    GridView1.DataBind();
                   

                }
                else
                {
                    fAlert("User does not exist !", "error", "stay");
                }

            }
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (validateInput() == true)
            {
                if (checkUserExists() == true)
                {

                    updateUserStatus("Pending");
                    GridView1.DataBind();
                   

                }
                else
                {
                    fAlert("User does not exist !", "error", "stay");

                }

            }

        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (validateInput() == true)
            {
                if (checkUserExists() == true)
                {

                    updateUserStatus("Blocked");
                    GridView1.DataBind();



                }
                else
                {
                    fAlert("User does not exist !", "error", "stay");

                }

            }

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (validateInput() == true)
            {
                Response.Write("<script>alert('DEBUG !'); </script>");
                if (checkUserExists() == true)
                {
                    Response.Write("<script>alert('DEBUG !'); </script>");
                    deleteExistingUser();
                    GridView1.DataBind();
                    ClearAllTextBoxes();
                }
                else
                {
                    fAlert("User does not exist !", "error", "stay");

                }

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

            if (allInputsCorrect == false)
            {
                fAlert("Wrong input type! Please, try again.", "error", "stay");
            }

            return allInputsCorrect;

        }

        private bool checkUserExists()
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
                                   "FROM [member_master_tbl]" +
                                   "WHERE member_id=@memberId";

                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {

                        cmd.Parameters.AddWithValue("@memberId", TextBox1.Text);
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

                        cmd.Parameters.AddWithValue("@memberId", TextBox1.Text);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                     TextBox3.Text = dr.GetValue(0).ToString();
                                     TextBox2.Text = dr.GetValue(1).ToString();
                                     TextBox4.Text = dr.GetValue(2).ToString();
                                     TextBox8.Text = dr.GetValue(3).ToString();
                                     TextBox5.Text = dr.GetValue(4).ToString();
                                     TextBox6.Text = dr.GetValue(5).ToString();
                                     TextBox9.Text = dr.GetValue(6).ToString();
                                     TextBox10.Text = dr.GetValue(7).ToString();
                                     TextBox7.Text = dr.GetValue(10).ToString();

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

        private void updateUserStatus(String p_UserStat)
        {
            try
            {
                if(p_UserStat != TextBox7.Text.ToString())
                {
                    using (SqlConnection con = new SqlConnection(strcon))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        String query1 = "UPDATE [member_master_tbl]" +
                                         "SET [account_status]=@accountStatus " +
                                         "WHERE [member_id]=@memberId;";
                        using (SqlCommand cmd = new SqlCommand(query1, con))
                        {
                            cmd.Parameters.AddWithValue("@accountStatus", p_UserStat);
                            cmd.Parameters.AddWithValue("@memberId", TextBox1.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }
                        TextBox7.Text = p_UserStat;

                        fAlert("User status updated successfully !", "success", "stay");
                    }
                }
                else
                {

                    fAlert("User status is already: " + p_UserStat + " !", "warning", "stay");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");
                Response.Write("<script>alert('User status was NOT UPDATED!'); </script>");

            }



        }

        private void deleteExistingUser()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    //Delete Member using "member_id"
                    String query1 = "DELETE FROM [member_master_tbl]" +
                                    "WHERE [member_id]=@memberId;";
                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {
                        cmd.Parameters.AddWithValue("@memberId", TextBox1.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                    fAlert("Existing member deleted successfully !", "success", "stay");
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
        }


        private void ClearAllTextBoxes()
        {
            TextBox1.Text = "";

            TextBox3.Text = "";
            TextBox2.Text = "";
            TextBox4.Text = "";
            TextBox8.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox7.Text = "";

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




        




