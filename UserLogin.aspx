<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserLogin.aspx.cs" Inherits="UserLogin" %>

<!DOCTYPE html>
<html>
<head>

	<title>Home</title>
	<meta name="keywords" content="" />
	<meta name="description" content="" />
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<link href="http://fonts.googleapis.com/css?family=Open+Sans:300,400,700" rel="stylesheet" type="text/css">
	<link href="css/font-awesome.min.css" rel="stylesheet" type="text/css">
	<link href="css/bootstrap.min.css" rel="stylesheet" type="text/css">
	<link href="css/bootstrap-theme.min.css" rel="stylesheet" type="text/css">
	<link href="css/templatemo_style.css" rel="stylesheet" type="text/css">	
</head>
<body class="templatemo-bg-gray">
    <h1>Dedupe Drive</h1>
	<div class="container">
		<div class="col-md-12">
			<h1 class="margin-bottom-15"></h1>
			<form class="form-horizontal templatemo-container templatemo-login-form-1 margin-bottom-30" role="form" action="#" method="post" runat="server">				
		        <div class="form-group">
		          <div class="col-xs-12">		            
		            <div class="control-wrapper">
		            	<label for="username" class="control-label fa-label"><i class="fa fa-user fa-medium"></i></label>
		            	<input type="text" class="form-control" id="username" placeholder="Username" runat="server">
				<asp:RequiredFieldValidator runat="server" id="reqName" controltovalidate="username" errormessage="Please enter your username!" />
		            </div>		            	            
		          </div>              
		        </div>
		        <div class="form-group">
		          <div class="col-md-12">
		          	<div class="control-wrapper">
		            	<label for="password" class="control-label fa-label"><i class="fa fa-lock fa-medium"></i></label>
		            	<input type="password" class="form-control" id="password" placeholder="Password" runat="server">
				
				<asp:RequiredFieldValidator runat="server" id="reqName2" controltovalidate="password" errormessage="Please enter your password!" />
		            </div>
		          </div>
		        </div>
		        <div class="form-group">
		          <div class="col-md-12">
		          	<div class="control-wrapper">
		          		 <asp:Button ID="Button1" runat="server" BackColor="#edeaef" Font-Bold="True" 
                                            onclick="Button1_Click" Text="Sign In" />
		          		<a href="ForgotPassword.aspx" class="text-right pull-right">Forgot password?</a>
		          	</div>
		          </div>
		        </div>
		        <hr>
		        
		      </form>
		      <div class="text-center">
		      	<center><a href="UserRegister.aspx" class="templatemo-create-new">Create new account <i class="fa fa-arrow-circle-o-right"></i></a>	
</center>		      
</div>
		</div>
	</div>
</body>
</html>