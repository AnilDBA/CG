<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="Orders.aspx.cs" Inherits="Orders" Theme="Theme1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    This is orders screen <br />
   &nbsp; &nbsp; <%=System.Configuration.ConfigurationManager.AppSettings["Version"] %>
    <br />i
    &nbsp; &nbsp; <%=System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionA"].ConnectionString %>
    <br />
    Products <asp:DropDownList runat="server" ID="ddlProducts" /><br />
    Order id:<asp:Label runat="server" ID="txtOrderID" /><br />
    Customer:<asp:Label runat="server" ID="lblCustomer" /><br />
    Session(name): <%Response.Write(Session["name"]);%><br />
    Session id: <%=Session.SessionID %><br />
    Cookie(ntime): <%=Request.Cookies["ntime"].Value %> <br /><br />
    Upload file <asp:FileUpload runat="server" ID="flFileupload" />
    <asp:Button runat="server" ID="btnUpload" Text="Upload File" OnClick="btnUpload_Click" />
    <br /><br />
    <asp:DetailsView runat="server" ID="dvFormView">
        
    </asp:DetailsView>
</asp:Content>