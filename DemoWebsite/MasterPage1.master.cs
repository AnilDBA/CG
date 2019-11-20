using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class MasterPage1 : System.Web.UI.MasterPage
{
    public string LoggedUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        LoggedUser = "Capgemini";
        //Page.LoadComplete +=new System.EventHandler(Page_LoadComplete);
    }

    //protected void Page_LoadComplete(object sender, EventArgs e)
    //{
    //    LoggedUser += DateTime.Now.ToString("====== dd/MM/yyyy hh:mm:ss tt");
    //}
}
