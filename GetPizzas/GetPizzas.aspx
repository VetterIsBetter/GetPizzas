<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetPizzas.aspx.cs" Inherits="GetPizzas.GetPizzas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" 
      type="image/png" 
      href="images/favicon.ico" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>  
            <table>  
                <tr>                     
                    <td>  
                        <asp:Button ID="btnSearch" Text="Update" runat="server" OnClick="btnSearch_Click" Enabled="true" type="button" class="btn btn-primary" /> 
                    </td>  
                </tr>                  
            </table>  
        </div>  
        <asp:Label ID="lblMessage" Text="" runat="server" class="badge badge-primary">Top 20 most ordered Pizza combinations</asp:Label>  
        <div>
            <ul id="PizzasOrdered" runat="server"></ul>
           
        </div>  
    </form>  
</body>  
</html>  

<style>
    #lblMessage {
        text-align: center;
        font-size:25px;
    }
    #PizzasOrdered {
        display:inline-block;
        background-color: lightblue;
        max-width:50%;
    }
</style>