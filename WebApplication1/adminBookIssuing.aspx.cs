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

namespace WebApplication1
{
    public partial class adminBookIssuing : System.Web.UI.Page
    {

        String strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
            
            if (IsPostBack)
            {
                TextBox1.BorderColor = System.Drawing.Color.LightGray;
                TextBox2.BorderColor = System.Drawing.Color.LightGray;
                TextBox3.BorderColor = System.Drawing.Color.LightGray;
                TextBox4.BorderColor = System.Drawing.Color.LightGray;
                TextBox5.BorderColor = System.Drawing.Color.LightGray;
                TextBox6.BorderColor = System.Drawing.Color.LightGray;

            }

        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            if (validateInput2() == true)
            {
                if (checkUserAndBookExists() == true)
                {
                    findUserAndBookNames();
                }
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (validateInput() == true)
            {
                if (checkUserAndBookExists() == true)
                {
                    if(hasAlreadyIssued() == false)
                    {
                        if(limitOfTakenBooksExceeded() == false)
                        {
                            issueBook();
                            GridView1.DataBind();
                            ClearAllTextBoxes();

                        }
                        else
                        {
                            fAlert("User has already taken max number of different books! To proceed further, user must return some books.", "warning", "stay");
                        }
                       
                    }
                    else
                    {
                        fAlert("This book was already issued to this UserId!", "error", "stay");
                    }
                  
                }
            }

        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            if (validateInput2() == true)
            {
                if (checkUserAndBookExists() == true)
                {
                    returnBook();
                    GridView1.DataBind();
                    ClearAllTextBoxes();
                }
            }

        }

        private void issueBook()
        {
            try
            {
                int  currentStockDb = 0;
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    String query1 = "SELECT *" +
                                  "FROM [book_master_tbl]" +
                                  "WHERE book_id=@bookId";

                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {

                        cmd.Parameters.AddWithValue("@bookId", TextBox1.Text);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    currentStockDb = Convert.ToInt32(dr.GetValue(12));
                                }

                            }
                        }
                    }


                    if (currentStockDb > 0)
                    {

                        SqlTransaction transaction = con.BeginTransaction();

                        try
                        {
                            String query2 = "INSERT INTO [book_issue_tbl]([member_id],[member_name],[book_id],[book_name],[issue_date],[due_date])" +
                                        "VALUES(@memberId,@memberName,@bookId,@bookName,@issueDate,@dueDate)";

                            using (SqlCommand cmd = new SqlCommand(query2, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@memberId", TextBox3.Text.Trim());
                                cmd.Parameters.AddWithValue("@memberName", TextBox2.Text.Trim());
                                cmd.Parameters.AddWithValue("@bookId", TextBox1.Text.Trim());
                                cmd.Parameters.AddWithValue("@bookName", TextBox4.Text.Trim());
                                cmd.Parameters.AddWithValue("@issueDate", TextBox5.Text.Trim());
                                cmd.Parameters.AddWithValue("@dueDate", TextBox6.Text.Trim());

                                cmd.ExecuteNonQuery();
                            }


                            String query3 = "UPDATE [book_master_tbl]" +
                                         "SET [current_stock]=[current_stock]-1 " +
                                         "WHERE [book_id]=@bookId;";
                            using (SqlCommand cmd = new SqlCommand(query3, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@bookId", TextBox1.Text.Trim());
                                cmd.ExecuteNonQuery();
                            }


                            String query4 = "UPDATE [member_master_tbl]" +
                                        "SET [num_taken_books]=[num_taken_books]+1 " +
                                        "WHERE [member_id]=@memberId;";
                            using (SqlCommand cmd = new SqlCommand(query4, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@memberId", TextBox3.Text.Trim());
                                cmd.ExecuteNonQuery();
                            }


                            transaction.Commit();
                        }

                        catch (Exception ex)
                        {
                            Response.Write("<script> alert(' " + ex.Message + "');</script>");
                            transaction.Rollback();

                        }

                        fAlert("New book issued successfully !", "success", "stay");
                    }
                    else
                    {

                        fAlert("There are no available books with this BookId now! Sorry !", "error", "stay");
                    }

                }
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");

            }


        }


        //User should not take the same book twice 
        private bool hasAlreadyIssued()
        {
            bool issued = false;
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
                                   "WHERE book_id=@bookId AND member_id = @memberId";

                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {
                        cmd.Parameters.AddWithValue("@bookId", TextBox1.Text);
                        cmd.Parameters.AddWithValue("@memberId", TextBox3.Text);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    issued = true;
                                }

                            }
                        }
                    }
                }
                return issued;
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");
                return issued;
            }
        }

        //User should not take different books more than 2 times
        private bool limitOfTakenBooksExceeded()
        {
            int numTakenBooksDb = 0; // number of taken books by user
            const int MAX_LIMIT = 2; // maximum number of books that can be taken by user
            bool exceeded = false;
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
                        cmd.Parameters.AddWithValue("@memberId", TextBox3.Text);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    numTakenBooksDb = Convert.ToInt32(dr.GetValue(11));
                                }

                            }
                        }
                    }

                    if(numTakenBooksDb >= MAX_LIMIT)
                    {
                        exceeded = true;
                    }
                    else
                    {
                        exceeded = false;
                    }

                }
                return exceeded;
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");
                return exceeded;
            }
        }


        private void returnBook()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlTransaction transaction = con.BeginTransaction();

                    try
                    {

                        String query1 = "DELETE FROM [book_issue_tbl]" +
                                        "WHERE [book_id]=@bookId;";
                        using (SqlCommand cmd = new SqlCommand(query1, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@bookId", TextBox1.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }

                        String query2 = "UPDATE [book_master_tbl]" +
                                       "SET [current_stock]=[current_stock]+1 " +
                                       "WHERE [book_id]=@bookId;";
                        using (SqlCommand cmd = new SqlCommand(query2, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@bookId", TextBox1.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }

                        String query3 = "UPDATE [member_master_tbl]" +
                                        "SET [num_taken_books]=[num_taken_books]-1 " +
                                        "WHERE [member_id]=@memberId;";
                        using (SqlCommand cmd = new SqlCommand(query3, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@memberId", TextBox3.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        fAlert("Taken Book returned successfully !", "success", "stay");
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Taken Book was NOT returned!'); </script>");
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





        private bool validateInput()
        {
            bool allInputsCorrect = true;


            if (customUsersInputValidation.inputValidation.validateUserName(TextBox3.Text) == false)
            {
                TextBox3.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateBookId(TextBox1.Text) == false)
            {
                TextBox1.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateFullName(TextBox2.Text) == false)
            {
                TextBox2.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateBookName(TextBox4.Text) == false)
            {
                TextBox4.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateDate(TextBox5.Text) == false)
            {
                TextBox5.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateDate(TextBox6.Text) == false)
            {
                TextBox6.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }

            if (allInputsCorrect == false)
            {
                fAlert("Wrong input type! Please, try again. ", "error", "stay");
            }

            return allInputsCorrect;

        }


        private bool validateInput2()
        {
            bool allInputsCorrect = true;


            if (customUsersInputValidation.inputValidation.validateUserName(TextBox3.Text) == false)
            {
                TextBox3.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateBookId(TextBox1.Text) == false)
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


        private bool checkUserAndBookExists()
        {
            bool found = false;
            bool UserFound = false;
            bool BookFound = false;
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
                        cmd.Parameters.AddWithValue("@memberId", TextBox3.Text);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    UserFound = true;
                                }

                            }
                        }
                    }
                }

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    String query1 = "SELECT *" +
                                   "FROM [book_master_tbl]" +
                                   "WHERE book_id=@bookId";

                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {
                        cmd.Parameters.AddWithValue("@bookId", TextBox1.Text);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    BookFound = true;


                                }

                            }
                        }
                    }
                }


                if(UserFound == true && BookFound == true)
                {
                    found = true;
                }
                else if(UserFound == true && BookFound == false)
                {
                    fAlert("User ID was found, Book ID was NOT", "error", "stay");
                    found = false;
                }
                else if(UserFound == false && BookFound == true)
                {
                    fAlert("Book ID was found, User ID was NOT", "error", "stay");
                    found = false;
                }
                else
                {
                    fAlert("User ID and Book ID was NOT found", "error", "stay");
                    found = false;
                }




                return found;
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");
                return found;
            }
        }



        private void findUserAndBookNames()
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
                        cmd.Parameters.AddWithValue("@memberId", TextBox3.Text);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    TextBox2.Text = dr.GetValue(0).ToString().Trim();

                                }

                            }
                        }
                    }

                    String query2 = "SELECT *" +
                                  "FROM [book_master_tbl]" +
                                  "WHERE book_id=@bookId";

                    using (SqlCommand cmd = new SqlCommand(query2, con))
                    {

                        cmd.Parameters.AddWithValue("@bookId", TextBox1.Text);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    TextBox4.Text = dr.GetValue(1).ToString().Trim();

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

        private void ClearAllTextBoxes()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
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




