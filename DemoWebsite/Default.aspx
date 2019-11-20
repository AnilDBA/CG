<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Theme="Theme1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%="Capgemini"%></title>
    <script type="text/javascript" src="Scripts/jquery-latest.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <a href="Orders.aspx?orderid=1">Edit order 1</a><br />
        <a href="Orders.aspx?orderid=2&customer=ajay">Edit order 2</a><br />
        <a href="Orders.aspx?orderid=3">Edit order 3</a>
        <%//Response.Write(DateTime.Now.ToLongDateString());%>
        <asp:Label runat="server" ID="lblVersion" />
        <div>11111<%="This is my fisrt web application" %></div>
        <asp:TextBox runat="server" ID="txtName" /><br />
        <input type="date" runat="server" id="txtDate" />
        <br />
        <a href="Products.aspx">Go to Master Page Application</a>
        <div runat="server" id="dvTest">.................</div>
        <asp:Label runat="server" ID="lblError" style="color:red;" />
        <table border="1" cellspacing="0">
            <tr>
                <td>Product</td>
                <td><asp:DropDownList runat="server" ID="ddlProducts" EnableViewState="true">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>Type</td>
                <td><asp:Label runat="server" ID="lblType" /></td>
            </tr>
            <tr>
                <td>Rate</td>
                <td><asp:TextBox runat="server" ID="txtRate" /></td>
            </tr>
            <tr>
                <td>Qty</td>
                <td><asp:TextBox runat="server" ID="txtQty" /></td>
            </tr>
            <tr>
                <td>Amount</td>
                <td><asp:TextBox runat="server" ID="txtAmount" ForeColor="Red" style="font-weight:bold;background-color:aquamarine;" ReadOnly="true" /></td>
            </tr>
            <tr><td>&nbsp;</td><td><asp:Button runat="server" ID="btnProduct" Text="Product" /> </td></tr>
        </table>
        <asp:Label runat="server" ID="lblTest" />
        <%=ViewState["abc"] %>
        <br /><br />
        <asp:Button runat="server" ID="btnSubmit" Text="Show All Orders" OnClick="btnSubmit_Click"/><br />
        List of Orders
        <asp:GridView runat="server" ID="gvOrders" AutoGenerateColumns="true" OnRowCommand="gvOrders_RowCommand">
            <Columns>
                <asp:ButtonField ButtonType="Button" Text="Select" CommandName="Select123"/>
                <asp:TemplateField HeaderText="Edit"><ItemTemplate>
                    <a href="Orders.aspx?orderid=1">Edit</a>
                    <asp:TextBox runat="server" ID="txt123" />
                </ItemTemplate></asp:TemplateField>
                <%--<asp:BoundField DataField="orderid" HeaderText="OrderID" />
                <asp:BoundField DataField="productid" HeaderText="Product ID" />
                <asp:BoundField DataField="customer" HeaderText="Customer" />
                <asp:TemplateField HeaderText="Order ID"><ItemTemplate>
                    <asp:TextBox runat="server" ID="lblOrderID" Text='<%#Eval("orderid")%>' />
                </ItemTemplate></asp:TemplateField>--%>
            </Columns>
        </asp:GridView><br />
        <table border="1" cellspacing="0">
            <tr>
                <td>Order</td>
                <td><asp:Label runat="server" ID="lblOrderid" /></td>
            </tr>
            <tr>
                <td>Product Id</td>
                <td><asp:Label runat="server" ID="lblProductID" /></td>
            </tr>
            <tr>
                <td>Customer</td>
                <td><asp:TextBox runat="server" ID="txtCustomer" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
<script type="text/javascript">
    //alert('546');
    <%//= "alert('this is my alert');"%>
    GetAlerts();
    //-----------ajax------------------
    function GetAlerts() {
        var vl;
        $.ajax({
            type: 'POST',
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            url: '', //'http://localhost:49565/Service.svc/GetData',
            data: "{value:'tyyr'}",
            success: function (data) {//data.preventDefault;
                vl = data.d;
                alert(11111111111);
            },
            error: function (a) {
                vl = '';
                //alert(a.responseText);
                //alert(a.responseText);
            }
        });//alert(vl);
        return (vl);
    }//GetAlerts
    function calcTax(typ, itm, amount, qty) {
        var vl;
        $.ajax({
            type: 'POST',
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            url: 'webservice.asmx/CalcTax',
            data: "{typ:'" + typ + "',item:'" + itm + "',amount:'" + amount + "',qty:'" + qty + "'}",
            success: function (data) {//data.preventDefault;
                vl = data.d; //alert(vl);
            },
            error: function (a) {
                vl = 0;
                alert(a.responseText);
            }
        });
        return (vl);
    }//calcTax            
</script>