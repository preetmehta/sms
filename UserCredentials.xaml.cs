using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;


namespace sms
{
    /// <summary>
    /// Interaction logic for UserCredentials.xaml
    /// </summary>
    public partial class UserCredentials : Window
    {
        public UserCredentials()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(UserCredentials_Loaded);
        }

        void UserCredentials_Loaded(object sender, RoutedEventArgs e)
        {
            fillCmboComp();
            fillCmboYear();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Int32 userId = 0;
            String userName = "";
            String pass = "";
            userName = txtUserName.Text.ToString().Trim();
            pass = txtPass.Text.ToString().Trim();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["sms.Properties.Settings.conn"].ConnectionString);

            SqlCommand cmd = new SqlCommand("select id,pass from usermaster where username ='"+userName+"'and active = 1", conn);

            if (userName.Length > 0 && pass.Length > 0)
            {
                try
                {
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (pass == dr["pass"].ToString())
                            {
                                userId = Int32.Parse(dr["id"].ToString());
                            }
                            else
                            {
                                MessageBox.Show("invalid password!");
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("invalid UserName and pass!");
                    }
                    if (userId != 0)
                    {
                        MessageBox.Show(userId.ToString());
                    }


                }
                catch (Exception ex)
                {

                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("UserName and password is empty!");
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void fillCmboComp()
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["sms.Properties.Settings.conn"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand("select id,name from compmaster where active = 1", conn);
                
                conn.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                DataTable dt = new DataTable();
                dt.Clear();
                dataAdapter.Fill(dt);
                cmbComp.ItemsSource = dt.DefaultView;
                cmbComp.DisplayMemberPath = dt.Columns["name"].ToString();
                cmbComp.SelectedValuePath = dt.Columns["id"].ToString();

                cmbComp.SelectedValue = 1 ;

            }catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }finally{
                conn.Close();
            }
        }

        private void fillCmboYear()
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["sms.Properties.Settings.conn"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand("select id,yearname from yearmaster where active = 1", conn);

                conn.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                DataTable dt = new DataTable();
                dt.Clear();
                dataAdapter.Fill(dt);
                cmbYear.ItemsSource = dt.DefaultView;
                cmbYear.DisplayMemberPath = dt.Columns["yearname"].ToString();
                cmbYear.SelectedValuePath = dt.Columns["id"].ToString();

                cmbYear.SelectedValue = 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
