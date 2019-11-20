using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class _Default : System.Web.UI.Page
{    
    protected void  Page_Load(object sender, EventArgs e)
    {
        //lblError.Text = Request.QueryString["orderid"] ;        
        dvTest.InnerHtml = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionA"].ConnectionString;
        if (IsPostBack)
        {
            return;
        }
        else {
            ViewState["abc"] = "<input type='text' id='txt1' onclick='alert(1234556);' />";
            lblTest.Text = DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");//System.Configuration.ConfigurationManager.AppSettings["Version"];
        }
        LoadProducts();
    }

    void LoadProducts()
    {
        DAL dal = new DAL();
        DataSet dtSet = dal.getdata("select * from product");
        if (dal.Error != "")
        {
            lblError.Text = dal.Error;
            return;
        }
        ///populate dropdownlist using datatable
        ddlProducts.DataSource = dtSet.Tables[0];
        ddlProducts.DataTextField = "product_name";
        ddlProducts.DataValueField = "product_id";
        ddlProducts.DataBind();
        ddlProducts.Items.Insert(0, "--please select----");

        //ListItem itm = new ListItem("Redmi", "1");
        //ddlProducts.Items.Add(itm);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {        
        dvTest.InnerHtml = txtName.Text + "<br/>" + txtDate.Value + "<br/>";
        ///populate gridview using datatable
        DAL dal = new DAL();
        DataSet dtSet = dal.getdata("select * from orders;");
        if (dal.Error != "")
        {
            lblError.Text = dal.Error;
            return;
        }
        gvOrders.DataSource = dtSet.Tables[0];
        gvOrders.DataBind();
    }

    protected void ddlProducts_SelectedIndexChanged(object sender, EventArgs e)
    {
        DAL dal = new DAL();
        DataSet dtSet = dal.getdata("select * from product where product_id='" + ddlProducts.Text + "'" );
        if (dal.Error != "")
        {
            lblError.Text = dal.Error;
            return;
        }
        lblType.Text =(string) dtSet.Tables[0].Rows[0]["product_type"];
    }

    protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
    { 
        if (e.CommandName == "Select123")
        {
            Int16 ro =Convert.ToInt16( e.CommandArgument);
            GridViewRow gr = gvOrders.Rows[ro];
            lblOrderid.Text = gr.Cells[2].Text;
            lblProductID.Text = gr.Cells[3].Text;
            txtCustomer.Text = gr.Cells[4].Text;
        }
    }

    protected void btnProduct_Click(object sender, EventArgs e)
    {
        lblTest.Text = ddlProducts.SelectedItem.Text;
    }
}