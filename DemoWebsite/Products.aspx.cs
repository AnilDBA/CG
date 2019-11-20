using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Products : System.Web.UI.Page
{
    protected MasterPage1 mst;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "Theme1";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        mst = (MasterPage1)Master;
        mst.LoggedUser = "Ajay";
        if(IsPostBack)
        {
            return;
        }
        if (Cache["product_items"] == null)
        {
            ListItemCollection lstProducts = new ListItemCollection();
            lstProducts.Add(new ListItem("Redmi", "1"));
            lstProducts.Add(new ListItem("iphone", "2"));
            lstProducts.Add(new ListItem("samsung", "3"));
            Cache.Add("product_items", lstProducts, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 20), System.Web.Caching.CacheItemPriority.Default, null);
            ddlProducts.DataSource = lstProducts;

            ddlProducts.DataBind();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Session["name"] = txtName.Text;
        HttpCookie ntime1 = new HttpCookie("ntime");
        ntime1.Value = DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        Response.SetCookie(ntime1);
        Response.Redirect("orders.aspx");
    }
}