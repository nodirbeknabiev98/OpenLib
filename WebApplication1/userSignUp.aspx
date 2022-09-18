<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="userSignUp.aspx.cs" Inherits="WebApplication1.signUpUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src='https://www.google.com/recaptcha/api.js' type="text/javascript"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section style="padding-top: 10px; padding-bottom: 10px;">

        <div class="container">
            <div class="row">
                <div class="col-md-8 mx-auto">
                    <div class="card" style="width: 45rem;">
                        <div class="card-body">

                            <div class="row">
                                <div class="col" style="text-align: center">
                                    <img src="images/generaluser.png" width="80" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col" style="text-align: center">
                                    <h4>User Sign Up</h4>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col">
                                    <hr />
                                </div>
                            </div>

                            <div class="row">

                                <div class="col" style="text-align: center;">
                                    <span class="badge badge-pill badge-info">General Info</span>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col">
                                    <hr />
                                </div>
                            </div>


                            <div class="row">

                                <div class="col-md-6">
                                    <label>Full Name </label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="E.g: George Thompson">

                                        </asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <label>Date of birth </label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Choose Here..." TextMode="Date">

                                        </asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="row">

                                <div class="col-md-6">
                                    <label>Phone Number </label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="E.g: +998935348708" TextMode="Number">

                                        </asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <label>Email Adress </label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="E.g: georgethompson98@gmail.com" TextMode="Email">

                                        </asp:TextBox>
                                    </div>
                                </div>

                            </div>


                            <div class="row">

                                <div class="col-md-4">
                                    <label>State </label>
                                    <div class="form-group">
                                        <asp:DropDownList CssClass="form-control" ID="DropDownList1" runat="server">

                                            <asp:ListItem Text="Tashkent City" Value="Tashkent City"> </asp:ListItem>
                                            <asp:ListItem Text="Tashkent Region" Value="Tashkent Region"> </asp:ListItem>
                                            <asp:ListItem Text="Andijan" Value="Andijan"> </asp:ListItem>
                                            <asp:ListItem Text="Bukhara" Value="Bukhara"> </asp:ListItem>
                                            <asp:ListItem Text="Fergana" Value="Fergana"> </asp:ListItem>
                                            <asp:ListItem Text="Jizzakh" Value="Jizzakh"></asp:ListItem>
                                            <asp:ListItem Text="Karakalpakstan" Value="Karakalpakstan"> </asp:ListItem>
                                            <asp:ListItem Text="Khorezm" Value="Khorezm"> </asp:ListItem>
                                            <asp:ListItem Text="Namangan" Value="Namangan"></asp:ListItem>
                                            <asp:ListItem Text="Qashqadaryo" Value="Qashqadaryo"> </asp:ListItem>
                                            <asp:ListItem Text="Samarkand" Value="Samarkand"> </asp:ListItem>
                                            <asp:ListItem Text="Sirdaryo" Value="Sirdaryo"> </asp:ListItem>
                                            <asp:ListItem Text="Surxondaryo" Value="Surxondaryo"> </asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <label>City </label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="E.g: New York">

                                        </asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <label>Postal Code </label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" placeholder="E.g: 10001">

                                        </asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="row">

                                <div class="col">
                                    <label>Full Address </label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox7" runat="server" placeholder="E.g: 19 Washington Square N, New York, NY 10011, USA" TextMode="MultiLine" Rows="2">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col">
                                    <hr />
                                </div>
                            </div>



                            <div class="row">

                                <div class="col" style="text-align: center;">
                                    <span class="badge badge-pill badge-info">Login Info</span>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col">
                                    <hr />
                                </div>
                            </div>


                            <div class="row">

                                <div class="col-md-4">
                                    <label>User Name </label>
                                    <div class="form-group">
                                        <div class="input-group">
                                              <div class="input-group-prepend">
												<span class="input-group-text">
													<i class="fa fa-user"></i>
												</span>                    
											  </div>
                                             <asp:TextBox CssClass="form-control" ID="TextBox8" runat="server" placeholder="E.g: george_thompson"> </asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-8">
                                    <label>Password </label>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <div class="input-group-prepend">
												<span class="input-group-text">
												    <i class="fa fa-lock"></i>
												</span>                    
											</div>
                                            <asp:TextBox CssClass="form-control" ID="TextBox9" runat="server" placeholder="E.g: 912220yusio" TextMode="Password" Text=""></asp:TextBox>

                                            <input id="clickMe" type="button" value="Generate" />
                                            <input id="copy-button" type="button" value="Copy" onclick="doCopy();" disabled="disabled">
                                        </div>
                                        <div class="progress">
                                            <div id="dynamic" class="progress-bar progress-bar-success progress-bar-striped active" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%">
                                                <span id="current-progress"></span>
                                            </div>
                                        </div>


                                    </div>
                                </div>

                            </div>

                           <div class="form-group">
								<div class="g-recaptcha" data-sitekey="6LdsDskZAAAAAEqiDIvGM8XdFMwyDearUjtfrnH9"></div>
							</div>

                            


                            <div class="form-group">
                                <asp:Button class="btn btn-primary btn-block" ID="Button1" runat="server" Text="Sign Up" OnClick="Button1_Click" />
                            </div>

                            <a href="homepage.aspx"><< Back to Home</a>





                        </div>
                    </div>
                </div>
            </div>
        </div>

        
    </section>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <script src='<%= ResolveUrl("~/custom_js/passwordGenerator_ClientSide.js") %>' type ="text/javascript"></script>
    <script src='<%= ResolveUrl("~/custom_js/zxcvbn.js") %>' type ="text/javascript"></script>
    

    <script type ="text/javascript">

        // p = past; c = current;
        function RunnerMain() {
            var textPass_p = "";
            var textFname_p = "";
            var textDob_p = "";
            var textPnum_p = "";
            var textEmail_p = "";
            var textCname_p = "";
            var textPcode_p = "";
            var textFaddr_p = "";
            var textUname_p = "";

            var currentProgress = 0;
            var passStrengthName = ["Too guessable", "Very guessable", "Weak", "Medium", "Strong"];
            setInterval(
                function () {
                    var textFname_c = document.getElementById('<%=TextBox1.ClientID%>').value;
            var textDob_c = document.getElementById('<%=TextBox2.ClientID%>').value;
            var textPnum_c = document.getElementById('<%=TextBox3.ClientID%>').value;
            var textEmail_c = document.getElementById('<%=TextBox4.ClientID%>').value;
            var textCname_c = document.getElementById('<%=TextBox5.ClientID%>').value;
            var textPcode_c = document.getElementById('<%=TextBox6.ClientID%>').value;
            var textFaddr_c = document.getElementById('<%=TextBox7.ClientID%>').value;
            var textUname_c = document.getElementById('<%=TextBox8.ClientID%>').value;
            var textPass_c = document.getElementById('<%=TextBox9.ClientID%>').value;

            if (textFname_c != textFname_p || textDob_c != textDob_p || textPnum_c != textPnum_p || textEmail_c != textEmail_p || textCname_c != textCname_p || textPcode_c != textPcode_p || textFaddr_c != textFaddr_p || textUname_c != textUname_p || textPass_c != textPass_p) {
                if (textPass_c.length != 0) {
                    if (textPass_c.length <= 256) {

                        var string = zxcvbn(textPass_c, user_inputs = [textFname_c, textDob_c, textPnum_c, textEmail_c, textCname_c, textPcode_c, textFaddr_c, textUname_c, "library", "openlib", "Open-Lib", "Library Management System"]);

                        if (string.score == 0) {
                            currentProgress = 20;
                            $("#dynamic")
                                .css("width", currentProgress + "%")
                                .css("background-color", "#ff0000")
                                .attr("aria-valuenow", currentProgress)
                                .text(passStrengthName[string.score]);
                        }
                        if (string.score == 1) {
                            currentProgress = 40;
                            $("#dynamic")
                                .css("width", currentProgress + "%")
                                .css("background-color", "#f04747")
                                .attr("aria-valuenow", currentProgress)
                                .text(passStrengthName[string.score]);
                        }
                        if (string.score == 2) {
                            currentProgress = 60;
                            $("#dynamic")
                                .css("width", currentProgress + "%")
                                .css("background-color", "#ff6100")
                                .attr("aria-valuenow", currentProgress)
                                .text(passStrengthName[string.score]);
                        }
                        if (string.score == 3) {
                            currentProgress = 80;
                            $("#dynamic")
                                .css("width", currentProgress + "%")
                                .css("background-color", "#43b581")
                                .attr("aria-valuenow", currentProgress)
                                .text(passStrengthName[string.score]);
                        }
                        if (string.score == 4) {
                            currentProgress = 100;
                            $("#dynamic")
                                .css("width", currentProgress + "%")
                                .css("background-color", "green")
                                .attr("aria-valuenow", currentProgress)
                                .text(passStrengthName[string.score]);
                        }



                    }
                    else {
                        currentProgress = 50;
                        $("#dynamic")
                            .css("width", currentProgress + "%")
                            .attr("aria-valuenow", currentProgress)
                            .text("Pretty weak");

                    }

                }
                else {
                    currentProgress = 0;
                    $("#dynamic")
                        .css("width", currentProgress + "%")
                        .attr("aria-valuenow", currentProgress)
                }

            }

            textFname_p = textFname_c;
            textDob_p = textDob_c;
            textPnum_p = textPnum_c;
            textEmail_p = textEmail_c;
            textCname_p = textCname_c;
            textPcode_p = textPcode_c;
            textFaddr_p = textFaddr_c;
            textUname_p = textUname_c;
            textPass_p = textPass_c;
        }, 1000);

        }


        window.onload = function () {
            RunnerMain();
        };
    </script>

    
   
   
    
 
</asp:Content>

