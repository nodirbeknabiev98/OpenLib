<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminBookInventory.aspx.cs" Inherits="WebApplication1.adminBookInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

      <script type="text/javascript">

          var checked = false;
          var browsed = false;
          var srcBottomImage = "";

          function readURL(input) {

              if (checked != true) {
                  if (input.files && input.files[0]) {
                      reader = new FileReader();
                      reader.onload = function (e) {
                          $('#bottomImageId').attr('src', e.target.result);
                      };


                        reader.readAsDataURL(input.files[0]);
 
                      browsed = true;
                  }
                  
              }
              else {
                  alert('Default is already being used');
              }
              console.log(53);


          }

          function changeImageSrcBackEnd(input) {
              document.getElementById("bottomImageId").src = String(input);
              browsed = true;
          }

          function RunnerMain() {
              setInterval(
                  function () {
                      //checked = document.getElementById("defaultPictureCheckBox").checked;
                      checked = document.getElementById('<%= CheckBox1.ClientID %>').checked;
                     
                    if ((browsed == false) || (checked == true)) {
                        srcBottomImage = document.getElementById("bottomImageId").src;
                        if (checked == false) {
                            if (srcBottomImage != "https://localhost:44378/images/books_cover/white_dummy_pic.png") {

                                document.getElementById("bottomImageId").src = "images/books_cover/white_dummy_pic.png";
                                console.log(54);
                            }

                        }
                        else {
                            if (srcBottomImage != "https://localhost:44378/images/books_cover/default.jpg") {

                                document.getElementById("bottomImageId").src = "images/books_cover/default.jpg";
                                browsed = false;
                                console.log(55);
                            }

                        }
                    }
                }
                , 300);
          }


          window.onload = function () {
              RunnerMain();
          };
      </script>

    

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section style="padding-top: 50px; padding-bottom: 110px;">

        <div class="container-fluid">
            <div class="row">

                <div class="col-md-5">
                    <div class="card">
                        <div class="card-body">

                            <div class="row">
                                <div class="col" style="text-align: center">
                                    <img src="images/books.png" width="80" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col" style="text-align: center">
                                    <h4>Book Details</h4>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col">
                                    <hr />
                                </div>
                            </div>

                            <div class="row">

                                <div class="col" style="text-align: center;">
                                    <span class="badge badge-pill badge-info">Manage Your Books </span>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col">
                                    <hr />
                                </div>
                            </div>



                            <div class="row">

                                <div class="col-md-3">
                                    <label>Book ID </label>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Type..."> </asp:TextBox>
                                            <asp:Button class="btn btn-primary  " ID="Button1" runat="server" Text="Find" OnClick="Button1_Click" />
                                        </div>
                                    </div>
                                </div>


                                <div class="col-md-9">
                                    <label>Book Name </label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Type Here..."> </asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <div class="row">

                                <div class="col-md-4">

                                    <label>Language </label>
                                    <div class="form-group">

                                        <asp:DropDownList class="form-control" ID="DropDownList1" runat="server">
                                            <asp:ListItem Text="English" Value="English"></asp:ListItem>
                                            <asp:ListItem Text="Russian" Value="Russian"></asp:ListItem>
                                            <asp:ListItem Text="Uzbek" Value="Uzbek"></asp:ListItem>
                                            <asp:ListItem Text="French" Value="French"></asp:ListItem>
                                            <asp:ListItem Text="German" Value="German"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <label>Publisher Name </label>
                                    <div class="form-group">

                                        <asp:DropDownList class="form-control" ID="DropDownList2" runat="server">
                                            <asp:ListItem Text="Publisher1" Value="Publisher1"></asp:ListItem>
                                            <asp:ListItem Text="Publisher1" Value="Publisher2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>


                                <div class="col-md-4">

                                    <label>Author Name </label>
                                    <div class="form-group">

                                        <asp:DropDownList class="form-control" ID="DropDownList3" runat="server">
                                            <asp:ListItem Text="Author1" Value="Author1"></asp:ListItem>
                                            <asp:ListItem Text="Author2" Value="Author2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <label>Publisher Date </label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Loading..." TextMode="Date"> </asp:TextBox>
                                    </div>

                                </div>

                                <div class="col-md-4">
                                    <label>Genre </label>
                                    <div class="form-group">
                                        <asp:ListBox ID="ListBox1" runat="server" CssClass="form-control" SelectionMode="Multiple" Rows="5">
                                            <asp:ListItem Text="Adventure" Value="Adventure"></asp:ListItem>
                                            <asp:ListItem Text="Autobiography" Value="Autobiography"></asp:ListItem>
                                            <asp:ListItem Text="Art" Value="Art"></asp:ListItem>
                                            <asp:ListItem Text="Crime" Value="Crime"></asp:ListItem>
                                            <asp:ListItem Text="Self Help" Value="Self Help"></asp:ListItem>
                                            <asp:ListItem Text="Motivation" Value="Motivation"></asp:ListItem>
                                            <asp:ListItem Text="Drama" Value="Drama"></asp:ListItem>
                                            <asp:ListItem Text="Fantasy" Value="Fantasy"></asp:ListItem>
                                            <asp:ListItem Text="Horror" Value="Horror"></asp:ListItem>
                                            <asp:ListItem Text="Poetry" Value="Poetry"></asp:ListItem>
                                            <asp:ListItem Text="Personal Development" Value="Personal Development"></asp:ListItem>
                                            <asp:ListItem Text="Romance" Value="Romance"></asp:ListItem>
                                            <asp:ListItem Text="Science Fiction" Value="Science Fiction"></asp:ListItem>
                                            <asp:ListItem Text="Thriller" Value="Thriller"></asp:ListItem>
                                            <asp:ListItem Text="Encyclopedia" Value="Encyclopedia"></asp:ListItem>
                                            <asp:ListItem Text="Suspense" Value="Suspense"></asp:ListItem>
                                            <asp:ListItem Text="Healthy Living" Value="Healthy Living"></asp:ListItem>

                                        </asp:ListBox>
                                    </div>
                                </div>


                            </div>

                            <div class="row">

                                <div class="col-md-4">
                                    <label>Edition </label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="Type Here...">

                                        </asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <label>Book Cost (per unit) </label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="Type Here...">

                                        </asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <label>Pages </label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" placeholder="Type Here...">

                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <div class="row">

                                <div class="col-md-4">
                                    <label>Actual Stock </label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox7" runat="server" placeholder="Type Here...">

                                        </asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <label>Current Stock </label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox8" runat="server" placeholder="Loading..." ReadOnly="true">

                                        </asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <label>Issued books </label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox9" runat="server" placeholder="Loading..." ReadOnly="true">

                                        </asp:TextBox>
                                    </div>
                                </div>

                            </div>



                            <div class="row">

                                <div class="col">
                                </div>
                            </div>





                            <div class="row">

                                <div class="col-md-4">
                                    <label>Browse Book cover </label>
                                    <div class="custom-file p-3">
                                        <asp:FileUpload onchange="readURL(this);" class="form-control custom-file-input" ID="FileUpload1" runat="server" />
                                       
                                        <asp:Label class="custom-file-label" ID="Label14" runat="server" Text="Label" for="customFile">Choose file...</asp:Label>
                                    </div>
                                    
                                    <label>Book Cover </label>

                                    <img id="bottomImageId" class="img-fluid img-thumbnail" src="images/books_cover/white_dummy_pic.png" height="270" width="250"/>
                                   
                                    <label><asp:CheckBox ID="CheckBox1" runat="server" />default</label>

                                </div>

                                <div class="col-md-8">
                                    <label>Book Description </label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox10" runat="server" placeholder="Type Here..." TextMode="MultiLine" Rows="15">

                                        </asp:TextBox>
                                    </div>

                                </div>

                            </div>



                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Button class="btn btn-success btn-lg btn-block  " ID="Button2" runat="server" Text="Add" OnClick="Button2_Click" />
                                </div>

                                <div class="col-md-4">
                                    <asp:Button class="btn btn-warning btn-lg btn-block  " ID="Button3" runat="server" Text="Update" OnClick="Button3_Click" />
                                </div>

                                <div class="col-md-4">
                                    <asp:Button class="btn btn-danger btn-lg btn-block  " ID="Button4" runat="server" Text="Delete" OnClick="Button4_Click" />
                                </div>
                            </div>

                            <a href="homepage.aspx"><< Back to Home</a>

                        </div>
                    </div>
                </div>




                <div class="col-md-7">
                    <div class="card">
                        <div class="card-body">

                            <div class="row">
                                <div class="col" style="text-align: center">
                                    <img src="images/img/issued-book-list.jpg" width="85" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col" style="text-align: center">
                                    <h5>Book Invetory List</h5>
                                </div>
                            </div>



                            <div class="row">
                                <div class="col">
                                    <hr />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col" style="text-align: center">
                                    <asp:Label class="badge badge-pill badge-info" ID="Label2" runat="server" Text="Database Table">  </asp:Label>
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
                                    <div class="table-responsive" style="height: 945px; overflow-y: scroll; overflow-x:hidden;">
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
