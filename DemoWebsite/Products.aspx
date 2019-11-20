<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Products" Theme="Theme1"%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%=mst.LoggedUser %><br /><br />
    <%= DateTime.Now.ToString() %><br />
    Products <asp:DropDownList runat="server" ID="ddlProducts" /><br />
    Name <asp:TextBox runat="server" ID="txtName" />
    <asp:RequiredFieldValidator runat="server" ID="reqName" 
        ControlToValidate="txtName" Text="Name is required" />
    <br /><br />
    Age <asp:TextBox runat="server" ID="txtAge" TextMode="Number" />
    <asp:RangeValidator runat="server" ID="rngAge" 
        ControlToValidate="txtAge" Type="Integer"
        Text="Invalid Age" MinimumValue="10" MaximumValue="90"  />
    <br /><br />
    Start Date <asp:TextBox id="txtStartDate" Runat="server" TextMode="Date" />
    <br /><br />
    End Date <asp:TextBox id="txtEndDate" Runat="server" TextMode="Date" />
    <asp:CompareValidator id="cmpEndDate" ControlToValidate="txtEndDate" 
        Type="Date" Operator="GreaterThan" Runat="server"
        ControlToCompare="txtStartDate" 
        text="End date must be greater than start date"/>
    <br /><br />
    <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" />
</asp:Content>