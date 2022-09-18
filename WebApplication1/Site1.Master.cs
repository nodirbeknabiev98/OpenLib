using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try 
            {
                if (string.IsNullOrEmpty((string)Session["role"]))
                {

                    //View Books
                    LinkButton1.Visible = false;
                    //User Login
                    LinkButton2.Visible = true;
                    //SignUp 
                    LinkButton3.Visible = true;
                    //LogOut
                    LinkButton4.Visible = false;
                    //Hello 
                    LinkButton5.Visible = false;
                    //Admin Login
                    LinkButton6.Visible = true;
                    //Author Management
                    LinkButton7.Visible = false;
                    //Publisher Management
                    LinkButton8.Visible = false;
                    //Book Inventory
                    LinkButton9.Visible = false;
                    //Book Issuing
                    LinkButton10.Visible = false;
                    //User Management
                    LinkButton11.Visible = false;
                    //User Profile
                    LinkButton12.Visible = false;

                }
                else if(Session["role"].Equals("user"))
                {
                    //View Books
                    LinkButton1.Visible = true;
                    //User Login
                    LinkButton2.Visible = false;
                    //SignUp 
                    LinkButton3.Visible = false;
                    //LogOut
                    LinkButton4.Visible = true;
                    //Hello 
                    LinkButton5.Visible = true;
                    LinkButton5.Text = "Hello " + Session["username"].ToString() + " !";
                    LinkButton5.ForeColor = System.Drawing.Color.GreenYellow;
                    LinkButton5.Font.Size = FontUnit.Medium;
                    //Admin Login
                    LinkButton6.Visible = false;
                    //Author Management
                    LinkButton7.Visible = false;
                    //Publisher Management
                    LinkButton8.Visible = false;
                    //Book Inventory
                    LinkButton9.Visible = false;
                    //Book Issuing
                    LinkButton10.Visible = false;
                    //User Management
                    LinkButton11.Visible = false;
                    //User Profile
                    LinkButton12.Visible = true;

                }
                else if (Session["role"].Equals("admin"))
                {
                    //View Books
                    LinkButton1.Visible = true;
                    //User Login
                    LinkButton2.Visible = false;
                    //SignUp 
                    LinkButton3.Visible = false;
                    //LogOut
                    LinkButton4.Visible = true;
                    //Hello 
                    LinkButton5.Visible = true;
                    LinkButton5.Text = "Hello " + Session["adminname"].ToString() + " !";
                    LinkButton5.ForeColor = System.Drawing.Color.DarkOrange;
                    LinkButton5.Font.Size = FontUnit.Medium;
                    
                    //Admin Login
                    LinkButton6.Visible = false;
                    //Author Management
                    LinkButton7.Visible = true;
                    //Publisher Management
                    LinkButton8.Visible = true;
                    //Book Inventory
                    LinkButton9.Visible = true;
                    //Book Issuing
                    LinkButton10.Visible = true;
                    //User Management
                    LinkButton11.Visible = true;
                    //User Profile
                    LinkButton12.Visible = false;

                }

            }
            catch(Exception ex)
            {
                Response.Write("<script> alert(' " + ex.Message + "');</script>");
            }  

        }


        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewBooks.aspx");
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("userLogin.aspx");
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Response.Redirect("userSignUp.aspx");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Session["username"] = "";
            Session["fullname"] = "";
            Session["role"] = "";
            Session["status"] = "";
            //View Books
            LinkButton1.Visible = false;
            //User Login
            LinkButton2.Visible = true;
            //SignUp 
            LinkButton3.Visible = true;
            //LogOut
            LinkButton4.Visible = false;
            //Hello User
            LinkButton5.Visible = false;
            //Admin Login
            LinkButton6.Visible = true;
            //Author Management
            LinkButton7.Visible = false;
            //Publisher Management
            LinkButton8.Visible = false;
            //Book Inventory
            LinkButton9.Visible = false;
            //Book Issuing
            LinkButton10.Visible = false;
            //User Management
            LinkButton11.Visible = false;

            Response.Redirect("homepage.aspx");
        }



        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminLogin.aspx");
        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminAuthorManagement.aspx");
        }
        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminPublisherManagement.aspx");
        }
        protected void LinkButton9_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminBookInventory.aspx");
        }

        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminBookIssuing.aspx");
        }
        protected void LinkButton11_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminUserManagement.aspx");
        }
        protected void LinkButton12_Click(object sender, EventArgs e)
        {
            Response.Redirect("userProfile.aspx");
        }


    }
}