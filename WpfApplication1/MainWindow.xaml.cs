using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Interop;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        dbClass mydb = new dbClass();
        public string abc;
        bool cmbLoaded = false;
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            dtEntryDate.SelectedDate = DateTime.Now;
            dgvData.CanUserAddRows = false;
            dgvData.CanUserDeleteRows = false;
            loaddata();
            //DataSet dtReadXML = new DataSet();
            //dtReadXML.Tables.Add(new DataTable());
            //dtReadXML.Tables[0].Columns.Add("name");
            //DataRow ro = dtReadXML.Tables[0].NewRow();
            //ro[0] = "adsfd";
            //dtReadXML.Tables[0].Rows.Add(ro);
            //dtReadXML.AcceptChanges();
            //dtReadXML.WriteXml("d:/y2.xml");
            //dtReadXML.ReadXml("d:/abc.xml");
            //commonClass.UserName = "abc";
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            dgvData.Width = this.ActualWidth - 20;
         //   menu.Width = this.ActualWidth - 50;
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            //string dt = DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt ") + txtName.Text;
            //MessageBox.Show(dt);
        }

        private void loaddata()
        {
            DataSet dtset = mydb.getdata("exec pr_loadProductData");
            if (mydb.Error.Length > 0)
            {
                lblStatus.Content = mydb.Error;
            }
            else
            {
                dgvData.ItemsSource = dtset.Tables[0].DefaultView;
                lblStatus.Content = "fetched rows " + dtset.Tables[0].Rows.Count.ToString();
                if (! cmbLoaded)
                {
                    cmbType.Items.Clear();
                    cmbType.ItemsSource = dtset.Tables[1].DefaultView;
                    cmbType.DisplayMemberPath = "t";
                    cmbType.SelectedValuePath = "mb";
                    cmbLoaded = true;
                    cmbType.SelectedIndex = 0;
                }
            }
        }

        private void loaddataUsingDataReader()
        {
            SqlDataReader dtReader = mydb.getdatareader("select * from product");
            if (mydb.Error.Length > 0)
            {
                lblStatus.Content = mydb.Error;
            }
            else
            {
                DataTable dtData = new DataTable();
                dtData.Load(dtReader);

                if (dtReader.HasRows)
                {
                    int i = 0;
                    //ItemCollection item=dtReader.;
                    while (dtReader.Read())
                    {
                        //item = dgvData.Items.
                        //for (i = 0; i < dtReader.FieldCount; i++)
                        //{
                        //    item.Add("");
                        //}
                        //dgvData.Items.Add()
                    }
                    dgvData.ItemsSource = dtData.DefaultView;
                    lblStatus.Content = "fetched rows " + Convert.ToString(i);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loaddata();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //string dt = txtName.Text + DateTime.Now.ToString(" dd.MMM.yyyy hh:mm:ss tt");
            //MessageBox.Show(dt);

            if (txtName.Text == "")
            {
                lblStatus.Content = "Invalid Name";
                return;
            }
            SqlCommand sqlCmd = new SqlCommand("pr_saveproduct");
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@id", txtID.Text).Direction = ParameterDirection.InputOutput;
            sqlCmd.Parameters["@id"].Size = 50;
            sqlCmd.Parameters.AddWithValue("@name", txtName.Text);
            sqlCmd.Parameters.AddWithValue("@type", cmbType.Text);
            sqlCmd.Parameters.AddWithValue("@entrydate", dtEntryDate.SelectedDate);
            mydb.ExecuteQuery(sqlCmd);
            //DataSet dtset = mydb.getdata(sqlCmd);
            if (mydb.Error != "")
            {
                lblStatus.Content = mydb.Error;
            }
            else
            {
                lblStatus.Content = "Data saved: " + sqlCmd.Parameters["@id"].Value;
                loaddata();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                lblStatus.Content = "Invalid Product id";
            }
            SqlCommand sqlCmd = new SqlCommand("pr_deleteproduct");
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@return", 0).Direction = ParameterDirection.ReturnValue;
            sqlCmd.Parameters["@return"].Size = 50;
            sqlCmd.Parameters.AddWithValue("@id", txtID.Text);
            mydb.ExecuteQuery(sqlCmd);
            if (mydb.Error != "")
            {
                lblStatus.Content = mydb.Error;
            }
            else
            {
                if (Convert.ToInt32(sqlCmd.Parameters["@return"].Value) < 1)
                {
                    lblStatus.Content = "Unable to delete the product.";
                }
                else
                {
                    lblStatus.Content = "Product is not deleted.";
                    loaddata();
                }
            }

        }

        private void dgvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int rowindex= dgvData.SelectedIndex;
            if (rowindex < 0)
            {
                return;
            }
            txtID.Text = getCellData(dgvData, rowindex, 0);
            txtName.Text = getCellData(dgvData, rowindex, 1);
            cmbType.Text = getCellData(dgvData, rowindex, 2);
            dtEntryDate.SelectedDate =Convert.ToDateTime(getCellData(dgvData, rowindex, 3));
            chkActive.IsChecked = (getCellData(dgvData, rowindex, 4) == "Y");
        }

        private string getCellData(DataGrid dgv, int rowindex,int cellindex){
            DataGridRow drow = dgv.ItemContainerGenerator.ContainerFromIndex(rowindex) as DataGridRow;
            var cellContent = dgv.Columns[cellindex].GetCellContent(drow) as TextBlock;
            return cellContent.Text;
        }

        private void dgvData_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //
        }

        DataSet ds;
        SqlDataAdapter dataAdapter;
        void ReadData()
        {
            this.ds = new DataSet();
            string connString = "CONNECTION STRING GOES HERE";
            this.dataAdapter = new SqlDataAdapter("QUERY GOES HERE", connString);
            this.dataAdapter.Fill(this.ds, "TABLE1");
            this.ds.AcceptChanges();
            //set the table as the datasource for the grid in order to show that data in the grid
            dgvData.ItemsSource = ds.DefaultViewManager;
        }

        void SaveData()
        {
            DataSet changes = this.ds.GetChanges();
            if (changes != null)
            {
                //Data has changes. 
                //use update method in the adapter. it should update your datasource
                int updatedRows = this.dataAdapter.Update(changes);
                this.ds.AcceptChanges();
            }
        }

        public class UpdateStudentMob
        {
            public string stud_id { get; set; }
            public string stud_mobile { get; set; }
        }

        public async Task<string> UpdateStudMobile(string studid, string mobile)
        {
            string Response = "";
            try
            {
                HttpResponseMessage HttpResponseMessage = null;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //application/xml

                    UpdateStudentMob obj = new UpdateStudentMob() { stud_id = studid, stud_mobile = mobile };
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    // serialize into json string
                    var myContent = jss.Serialize(obj);

                    var httpContent = new StringContent(myContent, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage = await httpClient.PostAsync("http://localhost:50875/api/School", httpContent);

                    if (HttpResponseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        Response = HttpResponseMessage.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        Response = "Some error occured." + HttpResponseMessage.StatusCode;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return Response;
        }

        private async void btnTest_Click(object sender, RoutedEventArgs e)
        {
            string resp = await UpdateStudMobile("S1100001", "99999999999");
            MessageBox.Show(resp);
            //MessageBox.Show(txtName.Text);
            //Window nt = new Window1();
            //this.Visibility = Visibility.Collapsed;
            //nt.ShowDialog();
            //this.Visibility = Visibility.Visible;
            if (true) { return; }
            SqlConnection sqlConn = new SqlConnection(Properties.Settings.Default.dbCon);
            try
            {
                sqlConn.Open();
                MessageBox.Show("Connected to database server");
            }            
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            //datareader starts
            SqlCommand sqlCmd1 = new SqlCommand("select * from product");
            sqlCmd1.CommandType = CommandType.Text;
            sqlCmd1.Connection = sqlConn;
            SqlDataReader sqlDataRdr;
            try
            {
                sqlDataRdr = sqlCmd1.ExecuteReader();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return;
            }
            while (sqlDataRdr.Read())
            {
                txtID.Text = Convert.ToString(sqlDataRdr[0]);
                txtName.Text = Convert.ToString(sqlDataRdr["product_name"]);
                cmbType.Text = Convert.ToString(sqlDataRdr.GetValue(2));
                //break;
            }

            sqlDataRdr.NextResult();

            //datareder ends
            string query= "insert into product values(9,@name,@type,getdate(),'Y');insert into orders values(6,2,'CustomerD');";
            SqlCommand sqlCmd = new SqlCommand(query);
            SqlTransaction sqlTran = sqlConn.BeginTransaction();
            sqlCmd.CommandType = CommandType.Text;
            SqlParameter sqlparam = new SqlParameter("@return",0);
            sqlparam.Direction = ParameterDirection.ReturnValue;
            sqlparam.Size = 50;
            sqlCmd.Parameters.AddWithValue("@name", txtName.Text);
            sqlCmd.Parameters.AddWithValue("@type", cmbType.Text);
            sqlCmd.Parameters["@name"].DbType = DbType.Guid;
            sqlCmd.Parameters["@name"].Direction = ParameterDirection.InputOutput;

            sqlCmd.Connection = sqlConn;
            sqlCmd.Transaction = sqlTran;
            //try
            //{
            //    sqlCmd.ExecuteNonQuery();
            //    sqlTran.Commit();
            //    MessageBox.Show("Data saved");
            //}
            //catch (Exception exp)
            //{
            //    sqlTran.Rollback();
            //    MessageBox.Show(exp.Message);
            //}
            MessageBox.Show( sqlCmd.Parameters["@return"].Value.ToString());
            txtName.Text=Convert.ToString( sqlCmd.Parameters["@name"].Value);
            query = "select * from product;select * from orders";
            DataSet dtSet = new DataSet();
            SqlDataAdapter sqlAdp = new SqlDataAdapter(query, sqlConn);
            SqlCommandBuilder sqlBld = new SqlCommandBuilder(sqlAdp);
            sqlAdp.Fill(dtSet);
            dtSet.Tables[0].TableName = "product";
            dtSet.Tables[1].TableName = "orders";
            dtSet.Tables[1].Rows[4].Delete();
            dtSet.Tables[1].AcceptChanges();
            dtSet.Relations.Add("orders", dtSet.Tables["product"].Columns["product_id"], dtSet.Tables["orders"].Columns["productid"]);
            DataRow dr = dtSet.Tables["orders"].NewRow();
            dr["orderid"] = 5;
            dr["customer"] = "abc";
            dr["productid"] = 233;
            try {
                dtSet.Tables["orders"].Rows.Add(dr);
                dtSet.Tables["orders"].AcceptChanges();
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            dtSet.WriteXml("d:/ab1.xml");
            //try
            //{
            //    sqlAdp.Fill(dtSet);
            //    //sqlConn.Close();
            //    dtSet.Tables[0].Rows[0]["Product_name"] = txtName.Text;
            //    sqlAdp.Update(dtSet);
            //    dtSet.Tables[0].AcceptChanges();
            //    MessageBox.Show("Data saved");
            //}
            //catch (Exception exp)
            //{
            //    MessageBox.Show(exp.Message);
            //}
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void mnuAddOrders_Click(object sender, RoutedEventArgs e)
        {

        }


        //void CustomerDataProvider()
        //{
        //    NorthwindDataSet dataset = new NorthwindDataSet();

        //    adapter = new CustomersTableAdapter();
        //    adapter.Fill(dataset.Customers);

        //    dataset.Customers.CustomersRowChanged +=
        //        new NorthwindDataSet.CustomersRowChangeEventHandler(CustomersRowModified);
        //    dataset.Customers.CustomersRowDeleted +=
        //        new NorthwindDataSet.CustomersRowChangeEventHandler(CustomersRowModified);
        //}

        //void CustomersRowModified(object sender, NorthwindDataSet.CustomersRowChangeEvent e)
        //{
        //    adapter.Update(dataset.Customers);
        //}
    }
}
