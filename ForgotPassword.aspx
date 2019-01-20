<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="Default2" %>


<!DOCTYPE html>
<head>
	<title>Forgot Password</title>
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
	<div class="container">
		<div class="col-md-12">
			<h1 class="margin-bottom-15">Forgot Password</h1>
			<form class="form-horizontal templatemo-forgot-password-form templatemo-container" role="form" action="#" method="post" runat="server">	
				<div class="form-group">
		          <div class="col-md-12">
		          	Enter the username you used when creating the account and click Send Password.
		          </div>
		        </div>		
		        <div class="form-group">
		          <div class="col-md-12">
		            <input type="text" class="form-control" id="username" placeholder="Your Username" runat="server">	            
		          </div>              
		        </div>
		        <div class="form-group">
		          <div class="col-md-12">
		            <asp:Button ID="Button1" runat="server" BackColor="#669999" Font-Bold="True" 
                                            onclick="forgotpassword" Text="Send Password" Width="150px"    />
                    <br><br>
                </div>
		        </div>
		      </form>
		</div>
	</div>
</body>
