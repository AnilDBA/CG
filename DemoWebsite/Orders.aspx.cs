using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Orders : System.Web.UI.Page
{
    protected void onInit(object sender, EventArgs e)
    {
        base.OnInit(e);
        //Page.Theme = "Theme1";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        txtOrderID.Text = Request.QueryString["orderid"];
        lblCustomer.Text = Request.QueryString["customer"];
        if (Cache["product_items"] != null)
        {
            ddlProducts.DataSource = Cache["product_items"];
            ddlProducts.DataBind();
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string pth = Server.MapPath("Docs");
        if (flFileupload.HasFile)
        {
            flFileupload.PostedFile.SaveAs(pth + "\\" + flFileupload.PostedFile.FileName);
        }
    }
}