<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        GridView
    <asp:GridView runat="server" ID="GridView1" AutoGenerateColumns="true" AllowPaging="true" AllowSorting="true"
        OnPageIndexChanging="dvGrid_PageIndexChanging" PageSize="2" OnSorting="GridView1_Sorting"></asp:GridView><br />
        FormView
    <asp:FormView runat="server" ID="fvForm" OnPageIndexChanging="fvForm_PageIndexChanging" AllowPaging="true">
        <ItemTemplate>  
            <table>  
                <tr>  
                    <td>Roll Number</td>  
                    <td><%#Eval("orderid") %></td>  
                </tr>  
                <tr>  
                    <td>Student Name</td>  
                    <td><%#Eval("productid") %></td>  
                </tr>  
                <tr>  
                    <td>Hindi</td>  
                    <td><%#Eval("customer") %></td>  
                </tr>
            </table>
        </ItemTemplate>
    </asp:FormView>
    <br />DetailsView
<asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="true" Height="50px"
 AllowPaging="true" onpageindexchanging="DetailsView1_PageIndexChanging">
</asp:DetailsView>
    </form>
</body>
</html>
<%--OnModeChanging="DetailsView1_ModeChanging"
    OnItemUpdated="DetailsView1_ItemUpdated" OnItemUpdating="DetailsView1_ItemUpdating"
    OnItemInserting="DetailsView1_ItemInserting"--%>