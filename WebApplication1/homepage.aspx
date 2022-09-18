<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="WebApplication1.homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	
	<!-- Here you can place the references to the css,js files which will be used specifically to this page-->
	 <script src="custom_js/customjsfile.js" type ="text/javascript"></script>

</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<!-- Carousel -->
	<section>
		<div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
			<ol class="carousel-indicators">
				<li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
				<li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
				<li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
			</ol>
			<div class="carousel-inner">
				<div class="carousel-item active">
					<img class="d-block w-100" src="images/img/Students_Outdoors_09262018_9949_1920x1080.jpg" alt="First slide" style ="width:1000px; height:600px">
				</div>
				<div class="carousel-item">
					<img class="d-block w-100" src="images/img/Library.jpg" alt="Second slide" style ="width:1000px; height:600px">
				</div>
				<div class="carousel-item">
				<img class="d-block w-100" src="images/img/reading%20rooms%20reopened.jpg" alt="Third slide" style ="width:1000px; height:600px">
				</div>
			</div>
			<a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
				<span class="carousel-control-prev-icon" aria-hidden="true"></span>
					<span class="sr-only">Previous</span>
			</a>
			<a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
				<span class="carousel-control-next-icon" aria-hidden="true"></span>
					<span class="sr-only">Next</span>
			</a>
		</div>
	</section>
		

	<!-- Carousel -->
	
	
	 <!-- Second Image -->
	<section>
		<div class ="container">
			<div class ="row">
				<div class ="col-12" style="text-align:center">  
					<h2>Our Features</h2>
					<p><b> Our 3 Primary Feautures</b></p>       
				</div>
				
				<div class ="col-md-4" style= "text-align:center">
					<img src="images/digital-inventory.png" width ="150"/>
					<h4> Digital Book Inventory</h4>
					<p class ="text-justify">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard.</p>
				</div>
				<div class ="col-md-4" style= "text-align:center">
					 <img src="gifs/DQsG.gif" width ="300" />
					<h4>Search Books</h4>
					<p class ="text-justify">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard.</p>
				</div>
				<div class ="col-md-4" style= "text-align:center">
					<img src="images/defaulters-list.png" width ="150"/>
					<h4>Defaulter List</h4>
					<p class ="text-justify">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard.</p>
				</div>
			  
			</div>
		</div>
	</section>
	<!-- Second Image -->


	<!-- Third Image -->
	<section>
		<div style="text-align:center">
			<img class ="img-fluid img-thumbnail" style="max-width:100%; max-height:100%;" src="images/in-homepage-banner.jpg" />
		</div>
	</section>
	
	<!-- Third Image -->

	

	<!-- Fourth Image -->
	<section>
		<div class ="container">
			<div class ="row">
				<div class ="col-12" style="text-align:center">  
					<h2>Simple, Convenient and Reliable</h2>
					<p><b> Our 3 Simple Step Process</b></p>       
				</div>
			   
				<div class ="col-md-4" style= "text-align:center">
					<img src="images/sign-up.png" width ="150"/>
					<h4> Sign Up</h4>
					<p class ="text-justify">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard.</p>
				</div>
				<div class ="col-md-4" style= "text-align:center">
					 <img src="images/book-online.png" width ="250" />
					<h4>Work</h4>
					<p class ="text-justify">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard.</p>
				</div>
				<div class ="col-md-4" style= "text-align:center">
					<img src="images/library.png" width ="150"/>
					<h4>Get Info and Books</h4>
					<p class ="text-justify">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard.</p>
				</div>

			</div>
		</div>
	</section>
	<!-- Fourth Image -->


	


</asp:Content>
