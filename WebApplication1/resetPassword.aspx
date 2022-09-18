<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="resetPassword.aspx.cs" Inherits="WebApplication1.resetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script src='https://www.google.com/recaptcha/api.js'></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<section style = "padding-top: 20px; padding-bottom: 75px;">

		<div class ="container" >
			<div class = "row">
				<div class = "col-md-6 mx-auto" >
					<div class ="card" style="width: 35rem;">
						<div class ="card-body">

							<div class ="row">
								<div class ="col" style="text-align:center">
									<img src="images/generaluser.png" width = "100"/>
								</div>
							</div>

							<div class ="row">
								<div class ="col" style="text-align:center">
									<h3>Reset Password</h3>
								</div>
							</div>

							<div class ="row">
								<div class ="col">
									<hr/>
								</div>
							</div>

							<div class ="row">
								<div class ="col">
									<label> Member ID </label>
									<div class="form-group">
										<div class="input-group">
											<div class="input-group-prepend">
												<span class="input-group-text">
												<i class="fa fa-user"></i>
												</span>                    
											 </div>
											 <asp:TextBox CssClass ="form-control" ID="TextBox1" runat="server" placeholder="Type Here..." > </asp:TextBox>				
										</div>
									</div>
								</div>
							</div>

							<div class="form-group" >
								<div class="g-recaptcha" data-sitekey="6LdsDskZAAAAAEqiDIvGM8XdFMwyDearUjtfrnH9"></div>
							</div>

							<div class="form-group">
								<asp:Button class="btn btn-primary btn-block" ID="Button1" runat="server" Text ="Send Reset Password Link" BackColor ="green" Visible ="True" OnClick ="Button1_Click" > </asp:Button>
								<asp:Label ID ="Label1" runat ="server"  Visible ="False"></asp:Label>
							</div>

							
						</div>
					</div>
					<a href ="homepage.aspx"><< Back to Home</a>
				</div>
			</div>
		</div>
	</section>
  


</asp:Content>
