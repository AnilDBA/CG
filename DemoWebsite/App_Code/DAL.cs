using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DAL
/// </summary>
public class DAL
{
    public DAL()
    {
        //
        // TODO: Add constructor logic here
        //
        Error = "";
    }
    private SqlConnection sqlcon = null;
    public string Error { get; set; }
    private bool checkConnectionState()
    {
        try
        {
            if (sqlcon == null)
            {
                sqlcon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionA"].ConnectionString);
            }
            if (sqlcon.State != ConnectionState.Open)
            {
                sqlcon.Open();
            }
        }
        catch (Exception e)
        {
            Error = e.Message;
            return false;
        }
        return true;
    }
    public bool ExecuteQuery(string query)
    {
        Error = "";
        SqlCommand sqlCmd = new SqlCommand(query);
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = sqlcon;
        if (!checkConnectionState())
        {
            return false;
        }
        try
        {
            sqlCmd.ExecuteNonQuery();
            sqlcon.Close();
        }
        catch (Exception e)
        {
            Error = e.Message;
            return false;
        }
        return true;
    }

    public bool ExecuteQuery(SqlCommand sqlCmd)
    {
        Error = "";
        sqlCmd.Connection = sqlcon;
        if (!checkConnectionState())
        {
            return false;
        }
        try
        {
            sqlCmd.ExecuteNonQuery();
            sqlcon.Close();
        }
        catch (Exception e)
        {
            Error = e.Message;
            return false;
        }
        return true;
    }

    public DataSet getdata(string query)
    {
        Error = "";
        DataSet dtset = new DataSet();
        if (!checkConnectionState())
        {
            return null;
        }
        SqlDataAdapter sqladp = new SqlDataAdapter(query, sqlcon);
        try
        {
            sqladp.Fill(dtset);
            sqlcon.Close();
        }
        catch (Exception e)
        {
            Error = e.Message;
            return null;
        }
        return dtset;
    }
    public DataSet getdata(SqlCommand sqlCmd)
    {
        Error = "";
        sqlCmd.Connection = sqlcon;
        if (!checkConnectionState())
        {
            return null;
        }
        DataSet dtset = new DataSet();
        SqlDataAdapter sqladp = new SqlDataAdapter(sqlCmd);
        try
        {
            sqladp.Fill(dtset);
            sqlcon.Close();
        }
        catch (Exception e)
        {
            Error = e.Message;
            return null;
        }
        return dtset;
    }
    public SqlDataReader getdatareader(string query)
    {
        Error = "";
        SqlCommand sqlCmd = new SqlCommand(query);
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = sqlcon;
        if (!checkConnectionState())
        {
            return null;
        }
        try
        {
            return sqlCmd.ExecuteReader();
        }
        catch (Exception e)
        {
            Error = e.Message;
            return null;
        }
    }

    public SqlDataReader getdatareader(SqlCommand sqlCmd)
    {
        Error = "";
        sqlCmd.Connection = sqlcon;
        if (!checkConnectionState())
        {
            return null;
        }
        try
        {
            return sqlCmd.ExecuteReader();
        }
        catch (Exception e)
        {
            Error = e.Message;
            return null;
        }
    }

}