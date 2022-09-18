<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="resetPasswordChange.aspx.cs" Inherits="WebApplication1.resetPasswordChange" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
									<asp:Label ID ="Label1" runat ="server"  Visible ="False"></asp:Label>
								</div>
							</div>

							<div class ="row">
								<div class ="col">
									<hr/>
								</div>
							</div>

							<div class ="row">
								<div class ="col">
									<label> Please,enter your registered phone number for security purposes: </label>
									<div class="form-group">
										<div class="input-group">
											<div class="input-group-prepend">
												<span class="input-group-text">
												<i class="fa fa-phone"></i>
												</span>                    
											 </div>
											 <asp:TextBox CssClass ="form-control" ID="TextBox1" runat="server" placeholder="Type Here..." ReadOnly ="true" > </asp:TextBox>				
										</div>
									</div>
								</div>
							</div>

						    <div class="form-group">
								<asp:Button class="btn btn-primary btn-block" ID="Button1" runat="server" Text ="Confirm" BackColor ="green" Visible ="False" OnClick ="Button1_Click" > </asp:Button>
								
							</div>

							<div class ="row">
								<div class ="col">
									<label> Your new password: </label>
									<div class="form-group">
										<div class="input-group">
											<div class="input-group-prepend">
												<span class="input-group-text">
												<i class="fa fa-lock"></i>
												</span>                    
											 </div>
											 <asp:TextBox CssClass ="form-control" ID="TextBox2" runat="server" placeholder="Type Here..." ReadOnly ="true"> </asp:TextBox>				
										</div>
									</div>
								</div>
							</div>

							<div class ="row">
								<div class ="col">
									<label> Confirm password: </label>
									<div class="form-group">
										<div class="input-group">
											<div class="input-group-prepend">
												<span class="input-group-text">
												<i class="fa fa-lock"></i>
												</span>                    
											 </div>
											 <asp:TextBox CssClass ="form-control" ID="TextBox3" runat="server" placeholder="Type Here..." ReadOnly ="true"> </asp:TextBox>				
										</div>
									</div>
								</div>
							</div>

							<div class="form-group">
								<asp:Button class="btn btn-primary btn-block" ID="Button2" runat="server" Text ="Change Password" BackColor ="green" Visible ="False" OnClick ="Button2_Click" > </asp:Button>
								<asp:Label ID ="Label2" runat ="server"  Visible ="False"></asp:Label>
								
							</div>

							
						</div>
					</div>
					<a href ="homepage.aspx"><< Back to Home</a>
				</div>
			</div>
		</div>
	</section>





</asp:Content>
