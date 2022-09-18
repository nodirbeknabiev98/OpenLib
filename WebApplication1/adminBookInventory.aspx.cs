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


//SweetAlert is not embeded into this code yet. 
//Because I've already used scriptmanager in this module for putting the script which changes the "book cover" to default pic or white dummy pic.
//Must read callbacks,promises and async JS in overall in order to coordinate the scripts to be executed in order.

namespace WebApplication1
{
    public partial class adminBookInventory : System.Web.UI.Page
    {

        String strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            fillAuthorName();
            fillPublisherName();
            

            if (IsPostBack)
            {
                TextBox1.BorderColor = System.Drawing.Color.LightGray;
                TextBox2.BorderColor = System.Drawing.Color.LightGray;
                TextBox3.BorderColor = System.Drawing.Color.LightGray;
                TextBox4.BorderColor = System.Drawing.Color.LightGray;
                TextBox5.BorderColor = System.Drawing.Color.LightGray;
                TextBox6.BorderColor = System.Drawing.Color.LightGray;
                TextBox7.BorderColor = System.Drawing.Color.LightGray;
                TextBox10.BorderColor = System.Drawing.Color.LightGray;
                DropDownList1.BorderColor = System.Drawing.Color.LightGray;
                DropDownList2.BorderColor = System.Drawing.Color.LightGray;
                DropDownList3.BorderColor = System.Drawing.Color.LightGray;
                ListBox1.BorderColor = System.Drawing.Color.LightGray;
                Label14.ForeColor = System.Drawing.Color.LightGray;

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (validateInput2() == true)
            {
                if (checkBookExists() == true)
                {
                    findAllBookData();
                }
                else
                {
                    Response.Write("<script> alert('Book Id does not exist !');</script>");

                }
            }

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            if(validateInput() == true)
            {
                if (checkBookExists() == false)
                {
                    addNewBook();
                    GridView1.DataBind();
                    ClearAllTextBoxes();

                }
                else
                {
                    Response.Write("<script> alert('Book Id already exists !');</script>");

                }
            }

        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (validateInput() == true)
            {
                if (checkBookExists() == true)
                {
                    updateExistingBook();
                    GridView1.DataBind();
                    ClearAllTextBoxes();
                }
                else
                {
                    Response.Write("<script> alert('Book id does not exist !');</script>");

                }
            }
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (validateInput() == true)
            {
                if (checkBookExists() == true)
                {
                    deleteExistingBook();
                    GridView1.DataBind();
                    ClearAllTextBoxes();
                }
                else
                {
                    Response.Write("<script> alert('Book id does not exist !');</script>");

                }
            }


        }


        void fillAuthorName()
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
                                   "FROM [author_master_tbl]";

                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {

                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                DropDownList3.DataSource = dt;
                                DropDownList3.DataValueField = "author_name";
                                DropDownList3.DataBind();
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


        void fillPublisherName()
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
                                   "FROM [publisher_master_tbl]";

                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {

                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                DropDownList2.DataSource = dt;
                                DropDownList2.DataValueField = "publisher_name";
                                DropDownList2.DataBind();
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


        private bool checkBookExists()
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



        private void findAllBookData()
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

                                    TextBox2.Text = dr.GetValue(5).ToString();
                                    TextBox3.Text = dr.GetValue(1).ToString();
                                    TextBox4.Text = dr.GetValue(7).ToString();
                                    TextBox5.Text = dr.GetValue(8).ToString();
                                    TextBox6.Text = dr.GetValue(9).ToString();
                                    TextBox7.Text = dr.GetValue(11).ToString();
                                    TextBox8.Text = dr.GetValue(12).ToString();

                                    int local_curr_stock = 0;
                                    local_curr_stock = Convert.ToInt32(dr.GetValue(12).ToString()) - Convert.ToInt32(dr.GetValue(11).ToString());
                                    TextBox9.Text = local_curr_stock.ToString();

                                    TextBox10.Text = dr.GetValue(10).ToString();



                                    string[] genre = dr.GetValue(2).ToString().Trim().Split(',');
                                    ListBox1.ClearSelection();
                                    for(int i = 0; i < genre.Length; i++)
                                    {
                                        for(int j = 0; j < ListBox1.Items.Count;j++)
                                        {
                                            if(ListBox1.Items[j].ToString() == genre [i])
                                            {
                                                ListBox1.Items[j].Selected = true;
                                            }
                                           
                                        }

                                    }

                                    DropDownList1.SelectedValue = dr.GetValue(6).ToString().Trim();
                                    DropDownList2.SelectedValue = dr.GetValue(4).ToString().Trim();
                                    DropDownList3.SelectedValue = dr.GetValue(3).ToString().Trim();


                                    string filePath = dr.GetValue(13).ToString().Trim();
                                    filePath = filePath.Remove(0, 2);
                                    string jsFunc = "changeImageSrcBackEnd('" + filePath + "')";
                                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "changingImageSrcFromBackEnd", jsFunc, true);
                                 
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



        private void addNewBook()
        {
            try
            {
                string filePath = "";
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);

                fileName = fileName.Trim();
                bool checkedValue = CheckBox1.Checked;

                if ((fileName == "" || fileName == null) && (checkedValue == false))
                {
                    Response.Write("<script>alert('You must choose your custom book cover or use the default book cover by checking the check box!'); </script>");
                    Label14.ForeColor = System.Drawing.Color.Red;

                }
                else
                {
                    using (SqlConnection con = new SqlConnection(strcon))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        String query1 = "INSERT INTO [book_master_tbl]([book_id],[book_name],[genre],[author_name],[publisher_name],[publish_date],[language],[edition],[book_cost],[no_of_pages],[book_description],[actual_stock],[current_stock],[book_img_link])" +
                                        "VALUES(@bookId,@bookName,@genre,@authorName,@publisherName,@publishDate,@language,@edition,@bookCost,@noOfPages,@bookDescription,@actualStock,@currentStock,@bookImgLink)";



                        using (SqlCommand cmd = new SqlCommand(query1, con))
                        {

                            cmd.Parameters.AddWithValue("@bookId", TextBox1.Text.Trim());
                            cmd.Parameters.AddWithValue("@bookName", TextBox3.Text.Trim());


                            string genres = "";
                            foreach (int i in ListBox1.GetSelectedIndices())
                            {
                                genres = genres + ListBox1.Items[i] + ",";
                            }
                            genres = genres.Remove(genres.Length - 1);
                            cmd.Parameters.AddWithValue("@genre", genres);

                            cmd.Parameters.AddWithValue("@authorName", DropDownList3.SelectedItem.Value);
                            cmd.Parameters.AddWithValue("@publisherName", DropDownList2.SelectedItem.Value);
                            cmd.Parameters.AddWithValue("@publishDate", TextBox2.Text.Trim());
                            cmd.Parameters.AddWithValue("@language", DropDownList1.SelectedItem.Value);
                            cmd.Parameters.AddWithValue("@edition", TextBox4.Text.Trim());
                            cmd.Parameters.AddWithValue("@bookCost", TextBox5.Text.Trim());
                            cmd.Parameters.AddWithValue("@noOfPages", TextBox6.Text.Trim());
                            cmd.Parameters.AddWithValue("@bookDescription", TextBox10.Text.Trim());

                            cmd.Parameters.AddWithValue("@actualStock", TextBox7.Text.Trim());
                            cmd.Parameters.AddWithValue("@currentStock", TextBox7.Text.Trim());


                            if (checkedValue == true)
                            {
                                filePath = "~/images/books_cover/default.jpg";
                               
                            }
                            else
                            {
                                filePath = "~/images/books_cover/" + TextBox1.Text.Trim() + "_" + fileName;
                                FileUpload1.SaveAs(Server.MapPath("images/books_cover/" + TextBox1.Text.Trim() + "_" + fileName));
                            }

                            cmd.Parameters.AddWithValue("@bookImgLink", filePath);
                           


                            cmd.ExecuteNonQuery();
                        }
                        Response.Write("<script>alert('New book added successfully !'); </script>");
                    }
                }
            }
               
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");

            }


        }

        private void updateExistingBook()
        {
            try
            {
                string filePath = "";
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);


                bool checkedValue = CheckBox1.Checked;


                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }


                    String query1 = "UPDATE [book_master_tbl]" +
                                     "SET [book_name]=@bookName,[genre]=@genre,[author_name]=@authorName,[publisher_name]=@publisherName," +
                                     "[publish_date]=@publishDate,[language]=@language,[edition]=@edition,[book_cost]=@bookCost," +
                                     "[no_of_pages]=@noOfPages,[book_description]=@bookDescription,[actual_stock]=@actualStock,[current_stock] = @currentStock " +
                                     "WHERE [book_id]=@bookId;";
                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {
                        cmd.Parameters.AddWithValue("@bookId", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@bookName", TextBox3.Text.Trim());


                        string genres = "";
                        foreach (int i in ListBox1.GetSelectedIndices())
                        {
                            genres = genres + ListBox1.Items[i] + ",";
                        }
                        genres = genres.Remove(genres.Length - 1);
                        cmd.Parameters.AddWithValue("@genre", genres);

                        cmd.Parameters.AddWithValue("@authorName", DropDownList3.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@publisherName", DropDownList2.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@publishDate", TextBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@language", DropDownList1.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@edition", TextBox4.Text.Trim());
                        cmd.Parameters.AddWithValue("@bookCost", TextBox5.Text.Trim());
                        cmd.Parameters.AddWithValue("@noOfPages", TextBox6.Text.Trim());
                        cmd.Parameters.AddWithValue("@bookDescription", TextBox10.Text.Trim());

                        cmd.Parameters.AddWithValue("@actualStock", TextBox7.Text.Trim());
                        cmd.Parameters.AddWithValue("@currentStock", TextBox7.Text.Trim());

                        cmd.ExecuteNonQuery();


                        Response.Write("<script>alert('Existing book details updated successfully !'); </script>");
                    }



                    if (fileName != "" || fileName != null || checkedValue == true)
                    {
                        if(deleteExestingBookImage() == true)
                        {
                            String query2 = "UPDATE [book_master_tbl]" +
                                   "SET [book_img_link]=@bookImgLink " +
                                   "WHERE [book_id]=@bookId;";

                            if (checkedValue == true)
                            {

                                filePath = "~/images/books_cover/default.jpg";
                            }
                            else
                            {
                                FileUpload1.SaveAs(Server.MapPath("images/books_cover/" + TextBox1.Text.Trim() + "_" + fileName));
                                filePath = "~/images/books_cover/" + TextBox1.Text.Trim() + "_" + fileName;
                            }




                            using (SqlCommand cmd = new SqlCommand(query2, con))
                            {
                                cmd.Parameters.AddWithValue("@bookImgLink", filePath);

                                cmd.Parameters.AddWithValue("@bookId", TextBox1.Text.Trim());


                                cmd.ExecuteNonQuery();

                            }
                            Response.Write("<script>alert('Book cover was also updated successfully !'); </script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Book cover was NOT UPDATED !'); </script>");

                        }
                        
                       

                    }
                    else
                    {
                        Response.Write("<script>alert('Image was not updated.Old/Original image was saved.'); </script>");

                    }

                   
                }

               
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");

            }

        }




        private void deleteExistingBook()
        {
            try
            {
                bool issued = false;
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    //We should check whether the book was issued and not returne yet before deleting the book
                    String query1 = "SELECT *" +
                                   "FROM [book_issue_tbl]" +
                                   "WHERE book_id=@bookId";

                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {

                        cmd.Parameters.AddWithValue("@bookId", TextBox1.Text.Trim());
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {

                                issued = true;
                            }
                        }
                    }
                    if (issued == false)
                    {
                        if(deleteExestingBookImage() == true)
                        {
                            String query3 = "DELETE FROM [book_master_tbl]" +
                                                "WHERE [book_id]=@bookId;";
                            using (SqlCommand cmd = new SqlCommand(query3, con))
                            {
                                cmd.Parameters.AddWithValue("@bookId", TextBox1.Text.Trim());
                                cmd.ExecuteNonQuery();
                            }
                            Response.Write("<script>alert('Existing book deleted successfully !'); </script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Image was not deleted!'); </script>");

                        }

                    }
                    else
                    {
                        Response.Write("<script>alert('This book was issued to some user! You cant delete this book before user returns it'); </script>");

                    }
                    
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");

            }

        }
        private bool deleteExestingBookImage()
        {
            bool imageDeleted = false;
            try
            {
                string filePathDb = "";

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    String query2 = "SELECT [book_img_link]" +
                              "FROM [book_master_tbl]" +
                              "WHERE [book_id]=@bookId";

                    using (SqlCommand cmd = new SqlCommand(query2, con))
                    {

                        cmd.Parameters.AddWithValue("@bookId", TextBox1.Text.Trim());
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    filePathDb = dr.GetValue(0).ToString();
                                }
                            }
                        }
                    }

                }


                if (File.Exists(Server.MapPath(filePathDb)))
                {
                    File.Delete(Server.MapPath(filePathDb));
                    imageDeleted = true;


                }
                else
                {
                    Response.Write("<script>alert('Cant find the path to delete the image!'); </script>");
                    imageDeleted = false;

                }
                return imageDeleted;
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");
                return imageDeleted;
            }

        }

        private bool validateInput()
        {
            bool allInputsCorrect = true;

            if (customUsersInputValidation.inputValidation.validateBookId(TextBox1.Text) == false) 
            {
                TextBox1.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateDate(TextBox2.Text) == false)
            {
                TextBox2.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateBookName(TextBox3.Text) == false) 
            {
                TextBox3.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateBookEdition(TextBox4.Text) == false)
            {
                TextBox4.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateBookCost(TextBox5.Text) == false)
            {
                TextBox5.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateNoOfPages(TextBox6.Text) == false)
            {
                TextBox6.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateActualStock(TextBox7.Text) == false)
            {
                TextBox7.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (customUsersInputValidation.inputValidation.validateBookDescription(TextBox10.Text) == false)
            {
                TextBox10.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (DropDownList1.SelectedValue == "")
            {
                DropDownList1.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (DropDownList2.SelectedValue == "")
            {
                DropDownList2.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (DropDownList3.SelectedValue == "")
            {
                DropDownList3.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (ListBox1.SelectedValue == "")
            {
                ListBox1.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }
            if (allInputsCorrect == false)
            {
                Response.Write("<script> alert('Wrong input type! Please, try again. ')</script>");
            }

            return allInputsCorrect;

        }

        private bool validateInput2()
        {
            bool allInputsCorrect = true;

            if (customUsersInputValidation.inputValidation.validateBookId(TextBox1.Text) == false)
            {
                TextBox1.BorderColor = System.Drawing.Color.Red;
                allInputsCorrect = false;
            }

            if (allInputsCorrect == false)
            {
                Response.Write("<script> alert('Wrong input type! Please, try again. ')</script>");
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
            TextBox10.Text = "";

            ListBox1.ClearSelection();

            DropDownList1.ClearSelection();
            DropDownList2.ClearSelection();
            DropDownList3.ClearSelection();



            string filePath = "images/books_cover/white_dummy_pic.png";

            string jsFunc = "changeImageSrcBackEnd('" + filePath + "')";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "changingImageSrcFromBackEnd", jsFunc, true);

        }

     
    }
}



