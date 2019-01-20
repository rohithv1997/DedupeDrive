<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewFiles.aspx.cs" Inherits="UserHome" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <title>User Home</title>
    <link href="../vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <link href="../vendor/metisMenu/metisMenu.min.css" rel="stylesheet">
    <link href="../dist/css/sb-admin-2.css" rel="stylesheet">
    <link href="../vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
</head>

<body>
    <div id="wrapper" runat="server">

        <!-- Navigation -->
        <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0">
            <div class="navbar-header" runat="server">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" id="username" runat="server" href="UserHome.aspx">uname</a>
            </div>
            <!-- /.navbar-header -->

            <ul class="nav navbar-top-links navbar-right">
                
                <!-- /.dropdown -->
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                        <i class="fa fa-user fa-fw"></i> <i class="fa fa-caret-down"></i>
                    </a>
                    <ul class="dropdown-menu dropdown-user">
                       
                        <li><a href="UserLogin.aspx"><i class="fa fa-sign-out fa-fw"></i> Log out</a>
                        </li>
                        <li><a href="ChangePassword.aspx"><i class="fa fa-key"></i> Change Password</a>
                        </li>
                    </ul>
                    <!-- /.dropdown-user -->
                </li>
                <!-- /.dropdown -->
            </ul>
            <!-- /.navbar-top-links -->

            <div class="navbar-default sidebar" role="navigation">
                <div class="sidebar-nav navbar-collapse">
                    <ul class="nav" id="side-menu">
                        
                        <li>
                            <a href="UploadFiles.aspx"><i class="fa fa-upload fa-fw"></i> Upload Files</a>
                        </li>
                        <li>
                            <a class="active"><i class="fa fa-tasks fa-fw"></i> View Files</a>
                        </li>
                        
                        
                        
                    </ul>
                </div>
                <!-- /.sidebar-collapse -->
            </div>
            <!-- /.navbar-static-side -->
        </nav>

        <!-- Page Content -->
        <div id="page-wrapper">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12">
                        <h1 class="page-header">View Files</h1>
                        <form runat="server">
                               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                DataSourceID="SqlDataSource1" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="Fid" HeaderText="File ID" 
                                        SortExpression="File ID" />
                                    <asp:BoundField DataField="filename" HeaderText="File Name" 
                                        SortExpression="File Name" />
                                    <asp:BoundField DataField="ftype" HeaderText="File Type" 
                                        SortExpression="File Type" />
                                    <asp:BoundField DataField="fsize" HeaderText="File Size" 
                                        SortExpression="File Size" />
                                    <asp:BoundField DataField="date" HeaderText="Date" 
                                        SortExpression="Date" />  
                                    <asp:TemplateField>
            <ItemTemplate>
                 
               
           <asp:LinkButton ID="btnRandom"  CommandName="Download" CommandArgument="<%# Container.DataItemIndex %>"
            runat="server" 
            CssClass="btn btn-success btn-circle"    
       >
    <span aria-hidden="true" class="fa fa-download"></span>
</asp:LinkButton>
                <asp:LinkButton ID="LinkButton1"  CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>"
            runat="server" 
            CssClass="btn btn-danger btn-circle"    
       >
    <span aria-hidden="true" class="fa fa-eraser"></span>
</asp:LinkButton>
                </ItemTemplate>
        </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:DeConnection %>" 
                                SelectCommand="SELECT [Fid], [filename], [ftype], [fsize],[date] FROM [DeDup].[dbo].[FileUp] where oname=@uname">
                               
                            </asp:SqlDataSource>

                            <br/>
                            <asp:DropDownList ID="ComboBox1" runat="server" Width="162px" Visible="false"></asp:DropDownList>
               
                            <asp:Button ID="dall" runat="server" BackColor="#edeaef" Font-Bold="true" onclick="dall_Click" Text="Download all" />
                         </form>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->
            </div>
            <!-- /.container-fluid -->
        </div>
        <!-- /#page-wrapper -->

    </div>
    <!-- /#wrapper -->

    <!-- jQuery -->
    <script src="../vendor/jquery/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="../vendor/bootstrap/js/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="../vendor/metisMenu/metisMenu.min.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="../dist/js/sb-admin-2.js"></script>

</body>

</html>
