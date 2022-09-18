<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminPublisherManagement.aspx.cs" Inherits="WebApplication1.adminPublisherManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <section style = "padding-top: 50px; padding-bottom: 110px;">

        <div class ="container">
            <div class = "row">

                <div class = "col-md-6">
                    <div class ="card">
                        <div class ="card-body">
                            <div class ="row">
                                <div class ="col" style="text-align:center">
                                    <img src="images/publisher.png" width = "80"/>
                                </div>
                            </div>

                            <div class ="row">
                                <div class ="col" style="text-align:center">
                                    <h4>Publisher Details</h4>
                                </div>
                            </div>



                            <div class ="row">
                                <div class ="col">
                                    <hr/>
                                </div>
                            </div>

                            <div class ="row">

                                <div class ="col" style="text-align:center;">
                                    <span class ="badge badge-pill badge-info"> Modify Publisher Info </span>
                                </div>
                             </div>

                            <div class ="row">
                                <div class ="col">
                                    <hr/>
                                </div>
                            </div>


                            <div class ="row">

                                <div class ="col-md-4">
                                    <label> ID </label>
                                    <div class = "form-group">
                                        <div class = "input-group">
                                            <asp:TextBox CssClass ="form-control" ID="TextBox1" runat="server" placeholder="Type..."> </asp:TextBox>
                                            <asp:Button class="btn btn-primary  " ID="Button1" runat="server" Text ="Find"  OnClick="Button1_Click"/>
                                        </div>

                                    </div>
                                </div>

                                <div class ="col-md-8">
                                    <label>  Name </label>
                                    <div class = "form-group">
                                        <asp:TextBox CssClass ="form-control" ID="TextBox3" runat="server" placeholder="Type Here..." >

                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <div class ="row">                                
                                <div class ="col-md-4">
                                     <asp:Button class="btn btn-success btn-lg btn-block  " ID="Button2" runat="server" Text ="Add" OnClick="Button2_Click" />          
                                </div>
                            
                                 <div class ="col-md-4">
                                      <asp:Button class="btn btn-warning btn-lg btn-block  " ID="Button3" runat="server" Text ="Update" OnClick="Button3_Click" />                                   
                                 </div>
                                 
                                 <div class ="col-md-4">
                                      <asp:Button class="btn btn-danger btn-lg btn-block  " ID="Button4" runat="server" Text ="Delete" OnClick="Button4_Click" />                             
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
                                    <h5>Publisher List</h5>
                     
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
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:openLibDBConnectionString %>" SelectCommand="SELECT * FROM [publisher_master_tbl]"></asp:SqlDataSource>

                                <div class="col">

                                    <asp:GridView class="table table-sm table-bordered table-hover" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="publisher_id" DataSourceID="SqlDataSource1">
                                        <HeaderStyle CssClass="thead-dark" />
                                        <Columns>
                                            <asp:BoundField DataField="publisher_id" HeaderText="publisher_id" ReadOnly="True" SortExpression="publisher_id" />
                                            <asp:BoundField DataField="publisher_name" HeaderText="publisher_name" SortExpression="publisher_name" />
                                        </Columns>
                                    </asp:GridView>
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
