<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminUserManagement.aspx.cs" Inherits="WebApplication1.adminUserManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 
    

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <section style = "padding-top: 50px; padding-bottom: 110px;">

        <div class ="container-fluid">
            <div class = "row">

                <div class = "col-md-6">
                    <div class ="card">
                        <div class ="card-body">

                            <div class ="row">
                                <div class ="col" style="text-align:center">
                                    <img src="images/generaluser.png" width = "80"/>
                                </div>
                            </div>  
                            
                            <div class ="row">
                                <div class ="col" style="text-align:center">
                                    <h4>User Info Details</h4>
                                </div>
                            </div>


                            <div class ="row">
                                <div class ="col">
                                    <hr/>
                                </div>
                            </div>

                            <div class ="row">

                                <div class ="col" style="text-align:center;">
                                    <span class ="badge badge-pill badge-info"> Delete User or Change Status </span>
                                </div>
                             </div>

                            <div class ="row">
                                <div class ="col">
                                    <hr/>
                                </div>
                            </div>

                            <div class ="row">

                                <div class ="col-md-3">
                                        <label> User ID </label>
                                        <div class = "form-group">
                                            <div class = "input-group">
                                                <asp:TextBox CssClass ="form-control" ID="TextBox1" runat="server" placeholder="Type..."> </asp:TextBox>
                                                
                                                <asp:LinkButton class="btn btn-primary mr-1 " ID="LinkButton4" runat="server" Text ="S" OnClick ="LinkButton4_Click" >
                                                    <i class ="fas fa-search"></i>
                                                </asp:LinkButton>
                                            </div>

                                        </div>
                                </div>

                         
                    
                                <div class ="col-md-3">
                                    <label> Full Name </label>
                                    <div class = "form-group">
                                        <asp:TextBox CssClass ="form-control" ID="TextBox3" runat="server" placeholder="Loading..." ReadOnly="true" >

                                        </asp:TextBox>
                                    </div>
                                </div>

                                

                                 <div class ="col-md-5">
                                    <label> Account Status </label> 
                                    <div class = "form-group">
                                        <div class = "input-group">
                                            <asp:TextBox CssClass ="form-control mr-1" ID="TextBox7" runat="server" placeholder="Loading..." ReadOnly="true"> </asp:TextBox>

                                            <asp:LinkButton class="btn btn-success mr-1 " ID="LinkButton1" runat="server" Text ="S" OnClick ="LinkButton1_Click">
                                                <i class ="fas fa-check-circle"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton class="btn btn-warning mr-1" ID="LinkButton2" runat="server" Text ="P" OnClick ="LinkButton2_Click">
                                                <i class ="far fa-pause-circle"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton class="btn btn-danger mr-1" ID="LinkButton3" runat="server" Text ="D" OnClick ="LinkButton3_Click">
                                                <i class ="fas fa-times-circle"></i>
                                            </asp:LinkButton>
                                            
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class ="row">
                    
                                <div class ="col-md-3">
                                    <label> DOB </label>
                                    <div class = "form-group">
                                        <asp:TextBox CssClass ="form-control" ID="TextBox2" runat="server" placeholder="Loading..." ReadOnly="true">

                                        </asp:TextBox>
                                    </div>
                                </div>

                                 <div class ="col-md-4">
                                    <label> Contact Number </label>
                                    <div class = "form-group">
                                        <asp:TextBox CssClass ="form-control" ID="TextBox4" runat="server" placeholder="Loading..." ReadOnly="true" >

                                        </asp:TextBox>
                                    </div>
                                </div>

                                <div class ="col-md-4">
                                    <label> E-mail </label>
                                    <div class = "form-group">
                                        <asp:TextBox CssClass ="form-control" ID="TextBox8" runat="server" placeholder="Loading..." ReadOnly="true" >

                                        </asp:TextBox>
                                    </div>
                                </div>

                                
                            </div>

                            <div class ="row">
                    
                                <div class ="col-md-4">
                                    <label> State </label>
                                    <div class = "form-group">
                                        <asp:TextBox CssClass ="form-control" ID="TextBox5" runat="server" placeholder="Loading..." ReadOnly="true">

                                        </asp:TextBox>
                                    </div>
                                </div>

                                 <div class ="col-md-4">
                                    <label> City </label>
                                    <div class = "form-group">
                                        <asp:TextBox CssClass ="form-control" ID="TextBox6" runat="server" placeholder="Loading..." ReadOnly="true" >

                                        </asp:TextBox>
                                    </div>
                                </div>

                                <div class ="col-md-4">
                                    <label> Postal Code </label>
                                    <div class = "form-group">
                                        <asp:TextBox CssClass ="form-control" ID="TextBox9" runat="server" placeholder="Loading..." ReadOnly="true" >

                                        </asp:TextBox>
                                    </div>
                                </div>
  
                            </div>


                            <div class ="row">

                                <div class ="col">
                                    <label> Full Address </label>
                                    <div class = "form-group">
                                        <asp:TextBox CssClass ="form-control" ID="TextBox10" runat="server" placeholder="Loading..." TextMode ="MultiLine"  Rows ="2" ReadOnly ="true">

                                        </asp:TextBox>
                                    </div>
                                </div>
                             </div>




                            <div class ="row">      
                                
                                <div class ="col">
                                     <asp:Button class="btn btn-danger btn-lg btn-block" ID="Button2" runat="server" Text ="Delete User (Permanently)" OnClick ="Button2_Click"/>          
                                </div>
                
                            </div>
                              
                            <a href ="homepage.aspx"><< Back to Home</a>
                                        
                         </div>
                     </div>
                    </div>
                          

                

                <div class ="col-md-6"> 
                    <div class ="card">
                        <div class ="card-body">

                            <div class ="row">
                                <div class ="col" style="text-align:center">
                                    <img src="images/img/author-list.png" width = "85"/>
                                </div>                           
                            </div>

                            <div class ="row">
                                <div class ="col" style="text-align:center">
                                    <h5>User List</h5>
                                </div>
                            </div>



                            <div class ="row">
                                <div class ="col">
                                    <hr/>
                                </div>
                            </div>

                            <div class ="row">
                                <div class ="col" style="text-align:center">    
                                    <asp:Label class = "badge badge-pill badge-info" ID="Label2" runat="server" Text="Database Table">  </asp:Label>
                                </div>
                            </div>

                            

                             <div class ="row">
                                <div class ="col">
                                    <hr/>
                                </div>
                            </div>

                             <div class="row">
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:openLibDBConnectionString %>" SelectCommand="SELECT * FROM [member_master_tbl]"></asp:SqlDataSource>

                                <div class="col">
                                    <div class ="table-responsive" style="height: 405px;overflow: scroll;">
                                        <asp:GridView CssClass="table table-bordered table-condensed table-hover" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="member_id" DataSourceID="SqlDataSource1" >
                                            <HeaderStyle CssClass="thead-dark" />
                                            <Columns>

                                                <asp:BoundField DataField="member_id" HeaderText="member_id" ReadOnly="True" SortExpression="member_id" />
                                                <asp:BoundField DataField="account_status" HeaderText="account_status" SortExpression="account_status" />
                                                <asp:BoundField DataField="full_name" HeaderText="full_name"  SortExpression="full_name" />
                                                <asp:BoundField DataField="dob" HeaderText="dob" SortExpression="dob" />
                                                <asp:BoundField DataField="phone_number" HeaderText="phone_number" SortExpression="phone_number" />
                                                <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
                                                <asp:BoundField DataField="state" HeaderText="state" SortExpression="state" />
                                                <asp:BoundField DataField="city" HeaderText="city" SortExpression="city" />
                                                <asp:BoundField DataField="postal_code" HeaderText="postal_code" SortExpression="postal_code" />
                                                <asp:BoundField DataField="full_address" HeaderText="full_address" SortExpression="full_address" />
                                            </Columns>
                                        </asp:GridView>
                                     </div>
                                </div>
                            </div>

                              <div class ="row">
                                <div class ="col">
                                    <hr/>
                                </div>
                            </div>



                        </div> 
                    </div>
               </div>

               


                    

             </div>
         </div>
    </section> 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
   
    <script type="text/javascript">
        $(document).ready(
            function () {
                $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
            }
        );
    </script>
   
</asp:Content>


