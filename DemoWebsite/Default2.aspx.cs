using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (! IsPostBack)
        {
            ViewState["sortdr"] = "Asc";
            BindFormView();
        }
    }

    void BindFormView()
    {
        DAL dal = new DAL();
        DataSet dtSet = dal.getdata("select * from orders");
        if (dal.Error != "")
        {
            //lblError.Text = dal.Error;
            return;
        }
        DataView dv = dtSet.Tables[0].DefaultView;
        ViewState["sortdr"] = Convert.ToString(ViewState["sortdr"]) == "ASC" ? "DESC" : "ASC";
        dv.Sort = Convert.ToString(ViewState["sortexpression"]) + " " +  ViewState["sortdr"];
        GridView1.DataSource = dtSet.Tables[0];
        GridView1.DataBind();
        ViewState["sortdr"] = "Asc";
        fvForm.DataSource = dtSet.Tables[0];
        fvForm.DataBind();
        //DetailsView1.DataSource = dtSet.Tables[0];
        //DetailsView1.DataBind();
    }

    protected void fvForm_PageIndexChanging(object sender, FormViewPageEventArgs e)
    {
        fvForm.PageIndex = e.NewPageIndex;
        this.BindFormView();
    }

    protected void DetailsView1_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
    {
        //DetailsView1.PageIndex = e.NewPageIndex;
        this.BindFormView();
    }

    protected void DetailsView1_ModeChanging(object sender, DetailsViewModeEventArgs e)
    {
        //DetailsView1.ChangeMode(e.NewMode);
    }

    protected void DetailsView1_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
    {
        if(e.Exception !=null)
        {
            //DetailsView1.DataBind();
        }                    
    }

    protected void DetailsView1_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
        int i = 0;
        for (i = 0; i< e.NewValues.Count; i++)
        {
            if (e.NewValues[i] != null)
            {
                e.NewValues[i] = Server.HtmlEncode(e.NewValues[i].ToString());
            }
        }
    }

    protected void DetailsView1_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
        //
    }

    protected void dvGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindFormView();
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        //ViewState["sortdr"] = e.SortExpression;
        ViewState["sortexpression"] = e.SortExpression;
        this.BindFormView();
        //DataTable dtrslt = (DataTable)ViewState["dirState"];
        //if (dtrslt.Rows.Count > 0)
        //{
        //    if (Convert.ToString(ViewState["sortdr"]) == "Asc")
        //    {
        //        dtrslt.DefaultView.Sort = e.SortExpression + " Desc";
        //        ViewState["sortdr"] = "Desc";
        //    }
        //    else
        //    {
        //        dtrslt.DefaultView.Sort = e.SortExpression + " Asc";
        //        ViewState["sortdr"] = "Asc";
        //    }
        //    GridView1.DataSource = dtrslt;
        //    GridView1.DataBind();
        //}
    }
}