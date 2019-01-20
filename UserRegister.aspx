<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserRegister.aspx.cs" Inherits="UserRegister" %>

<!DOCTYPE html>
<head>
	<!-- templatemo 418 form pack -->
    <!-- 
    Form Pack
    http://www.templatemo.com/preview/templatemo_418_form_pack 
    -->
	<title>Create Account</title>
	<meta name="keywords" content="" />
	<meta name="description" content="" />
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<link href="http://fonts.googleapis.com/css?family=Open+Sans:300,400,700" rel="stylesheet" type="text/css">
	<link href="css/bootstrap.min.css" rel="stylesheet" type="text/css">
	<link href="css/bootstrap-theme.min.css" rel="stylesheet" type="text/css">
	<link href="css/templatemo_style.css" rel="stylesheet" type="text/css">	
</head>
<body class="templatemo-bg-gray" runat="server">
	<h1 class="margin-bottom-15">Create Account</h1>
	<div class="container">
		<div class="col-md-12">			
			<form class="form-horizontal templatemo-create-account templatemo-container" role="form" action="#" method="post" runat="server">
				<div class="form-inner">
					<div class="form-group">
			          <div class="col-md-12">		          	
			            <label for="username" class="control-label">Email</label>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
			            <input type="text" class="form-control" id="emailid" placeholder="" runat="server">	
					 <asp:RequiredFieldValidator runat="server" id="reqName1" controltovalidate="emailid" errormessage="Please enter an email id!" />            		            		            
 <asp:RegularExpressionValidator ID="validateEmail"    
  runat="server" ErrorMessage="Invalid email."
  ControlToValidate="emailid" 
  ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" />			          
</div>              
			        </div>			
			        <div class="form-group">
			          <div class="col-md-6">		          	
			            <label for="username" class="control-label">Username</label>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
			            <input type="text" class="form-control" id="username" placeholder=""  runat="server">
				    <asp:RequiredFieldValidator runat="server" id="reqName2" controltovalidate="username" errormessage="Please enter an username!" />		            		            		            
			          </div>
			          <div class="col-md-6 templatemo-radio-group">
			          	<label class="radio-inline">
		          			<input type="radio" name="optionsRadios" id="optionsRadios1" value="Male" runat="server" required> Male
		          		</label>
		          		<label class="radio-inline">
		          			<input type="radio" name="optionsRadios" id="optionsRadios2" value="Female" runat="server"> Female
		          		</label>
				
			          </div>             
			        </div>
			        <div class="form-group">
			          <div class="col-md-6">
			            <label for="password" class="control-label">Password</label>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
			            <input type="password" class="form-control" id="password" placeholder="" runat="server">
					<asp:RequiredFieldValidator runat="server" id="reqName3" controltovalidate="username" errormessage="Please enter a password!" />
			          </div><br/>
			          <div class="col-md-6">
			            <label for="password" class="control-label">Confirm Password</label>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
			            <input type="password" class="form-control" id="password_confirm" placeholder="" runat="server">
					<asp:RequiredFieldValidator runat="server" id="reqName4" controltovalidate="username" errormessage="Please confirm your password!" />
			          </div>
			        </div><br/>
			        <div class="form-group">
			          <div class="col-md-12">
			            <asp:Button ID="Button1" runat="server" BackColor="#edeaef" Font-Bold="True" 
                                            onclick="Button1_Click" Text="Create account" />
			            
			          </div>
			        </div>	
				</div>				    	
		      </form>		      
		</div>
	</div>

	
	<script type="text/javascript" src="js/jquery-1.11.1.min.js"></script>
	<script type="text/javascript" src="js/bootstrap.min.js"></script>
</body>
</html>
