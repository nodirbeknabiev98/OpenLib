<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminBookIssuing.aspx.cs" Inherits="WebApplication1.adminBookIssuing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section style = "padding-top: 50px; padding-bottom: 110px;">

        <div class ="container-fluid">
            <div class = "row">

                <div class = "col-md-5">
                    <div class ="card">
                        <div class ="card-body">

                            <div class ="row">
                                <div class ="col" style="text-align:center">
                                    <img src="images/img/book-issue.png" width = "80"/>
                                </div>
                            </div>  
                            
                            <div class ="row">
                                <div class ="col" style="text-align:center">
                                    <h4>Book Issuing</h4>
                                </div>
                            </div>


                            <div class ="row">
                                <div class ="col">
                                    <hr/>
                                </div>
                            </div>

                            <div class ="row">

                                <div class ="col" style="text-align:center;">
                                    <span class ="badge badge-pill badge-info"> Enter User & Book Info </span>
                                </div>
                             </div>

                            <div class ="row">
                                <div class ="col">
                                    <hr/>
                                </div>
                            </div>


                            <div class ="row">
                    
                                <div class ="col-md-6">
                                    <label> Member ID </label>
                                    <div class = "form-group">
                                        <asp:TextBox CssClass ="form-control" ID="TextBox3" runat="server" placeholder="Type Here..." >

                                        </asp:TextBox>
                                    </div>
                                </div>

                                <div class ="col-md-6">
                                    <label> Book ID </label>
                                    <div class = "form-group">
                                        <div class = "input-group">
                                            <asp:TextBox CssClass ="form-control" ID="TextBox1" runat="server" placeholder="Type Here..."> </asp:TextBox>
                                            <asp:Button class="btn btn-primary  " ID="Button1" runat="server" Text ="Find" OnClick="Button1_Click"/>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class ="row">
                    
                                <div class ="col-md-6">
                                    <label> Member Name </label>
                                    <div class = "form-group">
                                        <asp:TextBox CssClass ="form-control" ID="TextBox2" runat="server" placeholder="Loading..." ReadOnly="true">

                                        </asp:TextBox>
                                    </div>
                                </div>

                                 <div class ="col-md-6">
                                    <label> Book Name </label>
                                    <div class = "form-group">
                                        <asp:TextBox CssClass ="form-control" ID="TextBox4" runat="server" placeholder="Loading..." ReadOnly="true" >

                                        </asp:TextBox>
                                    </div>
                                </div>

                                
                            </div>

                            <div class ="row">
                    
                                <div class ="col-md-6">
                                    <label> Start Date </label>
                                    <div class = "form-group">
                                        <asp:TextBox CssClass ="form-control" ID="TextBox5" runat="server" placeholder="Loading..." TextMode="Date">

                                        </asp:TextBox>
                                    </div>
                                </div>

                                 <div class ="col-md-6">
                                    <label> End Date </label>
                                    <div class = "form-group">
                                        <asp:TextBox CssClass ="form-control" ID="TextBox6" runat="server" placeholder="Loading..." TextMode="Date" >

                                        </asp:TextBox>
                                    </div>
                                </div>

                                
                            </div>


                            <div class ="row">      
                                
                                <div class ="col-md-6">
                                     <asp:Button class="btn btn-primary btn-lg btn-block  " ID="Button2" runat="server" Text ="Issue" OnClick="Button2_Click"/>          
                                </div>
                  
                                <div class ="col-md-6">
                                     <asp:Button class="btn btn-success btn-lg btn-block  " ID="Button3" runat="server" Text ="Return" OnClick="Button3_Click" />                                   
                                </div>
                 
                            </div>
                              
                            <a href ="homepage.aspx"><< Back to Home</a>
                                        
                         </div>
                     </div>
                    </div>
                          

                

                <div class ="col-md-7"> 
                    <div class ="card">
                        <div class ="card-body">

                            <div class ="row">
                                <div class ="col" style="text-align:center">
                                    <img src="images/img/issued-book-list.jpg" width = "85"/>
                                </div>                           
                            </div>

                            <div class ="row">
                                <div class ="col" style="text-align:center">
                                    <h5>Issued Book List</h5>
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
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:openLibDBConnectionString %>" SelectCommand="SELECT * FROM [book_issue_tbl]"></asp:SqlDataSource>

                                <div class="col">
                                    <div class="table-responsive" style="height: 330px; overflow-y: scroll; overflow-x:hidden;">
                                        <asp:GridView CssClass="table table-bordered table-condensed table-hover" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="book_id" DataSourceID="SqlDataSource1">
                                            <HeaderStyle CssClass="thead-dark" />
                                            <Columns>
                                                <asp:BoundField DataField="member_id" HeaderText="member_id" ReadOnly="True" SortExpression="member_id" />
                                                <asp:BoundField DataField="member_name" HeaderText="member_name" SortExpression="member_name" />
                                                <asp:BoundField DataField="book_id" HeaderText="book_id"  SortExpression="book_id" />
                                                <asp:BoundField DataField="book_name" HeaderText="book_name" SortExpression="book_name" />
                                                <asp:BoundField DataField="issue_date" HeaderText="issue_date" SortExpression="issue_date"/>
                                                <asp:BoundField DataField="due_date" HeaderText="due_date" SortExpression="due_date" />
                                            </Columns>
                                        </asp:GridView>
                                     </div>
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

