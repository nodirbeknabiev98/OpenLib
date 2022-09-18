<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminLogin.aspx.cs" Inherits="WebApplication1.adminLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <section style = "padding-top: 70px; padding-bottom: 100px;">

        <div class ="container" >
            <div class = "row">
                <div class = "col-md-6 mx-auto" >
                    <div class ="card" style="width: 35rem;">
                        <div class ="card-body">

                            <div class ="row">
                                <div class ="col" style="text-align:center">
                                    <img src="images/adminuser.png" width = "100"/>
                                </div>
                            </div>

                            <div class ="row">
                                <div class ="col" style="text-align:center">
                                    <h3>Admin Login</h3>
                                </div>
                            </div>

                            <div class ="row">
                                <div class ="col">
                                    <hr/>
                                </div>
                            </div>

                            <div class ="row">
                                <div class ="col">
                                    <label> Admin ID </label>
                                    <div class = "form-group">
                                        <div class="input-group">
											<div class="input-group-prepend">
												<span class="input-group-text">
													<i class="fa fa-user"></i>
												</span>                    
											 </div>
											 <asp:TextBox CssClass ="form-control" ID="TextBox1" runat="server" placeholder="Type Here..."> </asp:TextBox>			
										</div>
                                    </div>
                                </div>
                            </div>

                            <div class ="row">
                                <div class ="col">
                                    <label> Password </label>
                                    <div class ="form-group">
                                        <div class="input-group">
											<div class="input-group-prepend">
												<span class="input-group-text">
													<i class="fa fa-lock"></i>
												</span>                    
											 </div>
											 <asp:TextBox CssClass ="form-control" ID="TextBox2" runat="server" TextMode ="Password" placeholder="Type Here... "></asp:TextBox>		
										</div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Button class="btn btn-primary btn-block" ID="Button1" runat="server" Text ="Login" BackColor ="green" OnClick ="Button1_Click"/>
                            </div>

                          

                            <a href ="homepage.aspx"><< Back to Home</a>





                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
  






</asp:Content>
