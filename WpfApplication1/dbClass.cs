using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace WpfApplication1
{
    class dbClass
    {
        private SqlConnection sqlcon=null;
        public string Error { get; set; }
        public void st()
        {
            try
            {
                sqlcon = new SqlConnection(Properties.Settings.Default.dbconnection);
                //sqlcon.Open();
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
        }
        private bool checkConnectionState()
        {
            try
            {
                if (sqlcon == null)
                {
                    sqlcon = new SqlConnection(Properties.Settings.Default.dbCon);
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
        public bool ExecuteQuery(string query){
            Error = "";
            SqlCommand sqlCmd = new SqlCommand(query);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = sqlcon;
            if (! checkConnectionState())
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
            SqlDataAdapter sqladp = new SqlDataAdapter(query,sqlcon);
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
            try {
                return sqlCmd.ExecuteReader();
            }
            catch(Exception e) {
                Error = e.Message;
                return null;
            }            
        }
    }
}
