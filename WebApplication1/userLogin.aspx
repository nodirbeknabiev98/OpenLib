<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="userLogin.aspx.cs" Inherits="WebApplication1.userLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script src='https://www.google.com/recaptcha/api.js'></script>

	<style>
 
</style>
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
									<h3>User Login</h3>
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

							<div class ="row">
								<div class ="col">
									<label> Password </label>
									<div class="form-group">
										<div class="input-group">
											<div class="input-group-prepend">
												<span class="input-group-text">
													<i class="fa fa-lock"></i>
												</span>                    
											 </div>
											 <asp:TextBox CssClass ="form-control" ID="TextBox2" runat="server" placeholder="Type Here..." TextMode="Password"> </asp:TextBox>				
										</div>
									</div>
								</div>
							</div>

							<div class="form-group">
								<div class="g-recaptcha" data-sitekey="6LdsDskZAAAAAEqiDIvGM8XdFMwyDearUjtfrnH9"></div>
							</div>
						
							<div class="form-group">
								<asp:Button class="btn btn-primary btn-block" ID="Button1" runat="server" Text ="Login" BackColor ="green" OnClick ="Button1_Click"/>
							</div>

							
							

							<div class="form-group">
								<a href ="userSignUp.aspx">
									<input class="btn btn-primary btn-block" id="Button2" type="button" value="Sign Up" />
								</a>
							</div>

							
							<div class ="row">
								<div class ="col">
									<div class="form-group">
										<div class="clearfix">
											<label class="float-left form-check-label">
												<asp:CheckBox ID ="CheckBox1" runat ="server" />
												Remember me
											</label>
												<a href="resetPassword.aspx" class="float-right">Forgot Password?</a>
										</div>
										<div class="or-seperator">
											<i>or</i>
										</div>
										<p class="text-center">Login with your social media account</p>
										<div class="text-center social-btn">
											<a href="#" class="btn btn-secondary">
												<i class="fa-brands fa-facebook"></i>
												&nbsp; Facebook
											</a>
											<a href="#" class="btn btn-info">
												<i class="fa-brands fa-twitter"></i>
												&nbsp; Twitter
											</a>
											<a href="#" class="btn btn-danger">
												<i class="fa-brands fa-google"></i>
												&nbsp; Google
											</a>
										</div>
									</div>
								</div>
							</div>

							
						</div>
					</div>
					<a href ="homepage.aspx"><< Back to Home</a>
				</div>
			</div>
		</div>
	</section>
  


</asp:Content>
