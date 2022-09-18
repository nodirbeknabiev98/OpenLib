<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="viewBooks.aspx.cs" Inherits="WebApplication3.viewbooks" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container">
            <div class="row">
                <div class="card">
                        <div class="card-body">
                             <div class="row">
                                <div class="col" style="text-align: center">
                                    <img src="images/img/searchViewBooks.png" width="85" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <br/>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col" style="text-align: center">
                                    <h5> List of Available Books</h5>
                                </div>
                            </div>



                            <div class="row">
                                <div class="col">
                                    <hr />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col" style="text-align: center">
                                    <asp:Label class="badge badge-pill badge-info" ID="Label2" runat="server" Text="Available Books">  </asp:Label>
                                </div>
                            </div>



                            <div class="row">
                                <div class="col">
                                    <hr />
                                </div>
                            </div>


                            <div class="row">
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:openLibDBConnectionString %>" SelectCommand="SELECT * FROM [book_master_tbl]"></asp:SqlDataSource>

                                <div class="col">
                                    <div class="table-responsive" style="height: 600px; overflow-y: scroll; overflow-x:hidden;">
                                        <asp:GridView CssClass="table table-bordered table-condensed table-hover" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="book_id" DataSourceID="SqlDataSource1">
                                            <HeaderStyle CssClass="thead-dark" />
                                            <Columns>

                                                <asp:BoundField DataField="book_id" HeaderText="ID" ReadOnly="True" SortExpression="book_id" >
                                                     <ControlStyle Font-Bold="True" />
                                                     <ItemStyle Font-Bold="True" />
                                                </asp:Boundfield>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div class="container-fluid">
                                                            <div class="row">
                                                                <div class="col-lg-10">

                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("book_name") %>' Font-Bold="True" Font-Size="X-Large"></asp:Label>

                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            Author -
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("author_name") %>' Font-Bold="True"></asp:Label>
                                                                            &nbsp;|&nbsp; Genre -
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("genre") %>' Font-Bold="True"></asp:Label>
                                                                            &nbsp;| Language -
                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("language") %>' Font-Bold="True"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            Publisher -
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("publisher_name") %>' Font-Bold="True"></asp:Label>
                                                                            &nbsp;|&nbsp; Publish Date -
                                                                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("publish_date") %>' Font-Bold="True"></asp:Label>
                                                                            &nbsp;| Pages -
                                                                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("no_of_pages") %>' Font-Bold="True"></asp:Label>
                                                                            &nbsp;| Edition -
                                                                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("edition") %>' Font-Bold="True"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            Cost -
                                                                        <asp:Label ID="Label10" runat="server" Text='<%# Eval("book_cost") %>' Font-Bold="True"></asp:Label>
                                                                            &nbsp;|&nbsp; Actual Stock -
                                                                        <asp:Label ID="Label11" runat="server" Text='<%# Eval("actual_stock") %>' Font-Bold="True"></asp:Label>
                                                                            &nbsp;| Current Stock -
                                                                        <asp:Label ID="Label12" runat="server" Text='<%# Eval("current_stock") %>' Font-Bold="True"></asp:Label>

                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            Description -
                                                                        <asp:Label ID="Label13" runat="server" Text='<%# Eval("book_description") %>' Font-Bold="True"></asp:Label>

                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="col-lg-2">
                                                                    <asp:Image class="img-fluid" ID="Image1" runat="server" ImageUrl='<%# Eval("book_img_link") %>' />

                                                                </div>
                                                            </div>
                                                        </div>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>




                                    </div>
                                </div>
                            </div>

                            

                        </div>
                        
                </div>
                <a href ="homepage.aspx"><< Back to Home</a>

                
              
            </div>
        </div>
       
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
