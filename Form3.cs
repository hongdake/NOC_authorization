using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ONE
{
    public partial class Form3 : Form
    {
        public static string strName;
        public Form3()
        {
            InitializeComponent();
        }

        public string GetStrValue
        {
            get
            {
                return strName;
            }
            set
            {
                strName = value;
            }
        }

        public static string strDisplay(string name_display)
        {

            string connOrder1 = "data source=192.168.222.161;" + "database=grant;" + "user id=root;" + "password = sztop05@MGT";
            MySqlConnection conn1 = new MySqlConnection(connOrder1);
            conn1.Open();
            string n_d = "select display from member where name = '" + name_display + "';";
            MySqlCommand nameComm1 = new MySqlCommand(n_d, conn1);
            MySqlDataReader nameReader1 = nameComm1.ExecuteReader(CommandBehavior.CloseConnection);
            nameReader1.Read();
            return nameReader1[0].ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            string connOrder = "data source=192.168.222.161;" + "database=grant;" + "user id=root;" + "password = sztop05@MGT";
            MySqlConnection conn = new MySqlConnection(connOrder);
            conn.Open();

            if ((textBox1.Text.Length != 0) && (textBox2.Text.Length != 0) && (textBox3.Text.Length != 0) && (textBox4.Text.Length != 0))
            {
                string uid_pwd = "select name from member;";
                MySqlCommand nameComm = new MySqlCommand(uid_pwd, conn);
                MySqlDataReader nameReader = nameComm.ExecuteReader(CommandBehavior.CloseConnection);

                int i = 0;
                while (nameReader.Read())
                {
                    string name;

                    name = nameReader[0].ToString();
                    i++;

                    Regex nameReg = new Regex(textBox1.Text);
                    Match nameMatch = nameReg.Match(name);
                    string nameFit = nameMatch.Value.ToString();

                    if (nameFit.Length == 0)
                    {
                        if (i == 31)
                        {
                            MessageBox.Show("该用户名不存在！");
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        nameReader.Close();
                        uid_pwd = "select password from member where name = '" + textBox1.Text + "';";
                        MySqlDataAdapter pwdAdapter = new MySqlDataAdapter(uid_pwd, conn);
                        DataSet ds = new DataSet();
                        pwdAdapter.Fill(ds, "pwd");

                        DataTable pwdTable = ds.Tables["pwd"];
                        string pwd = pwdTable.Rows[0][0].ToString();

                        Regex pwdReg = new Regex(textBox2.Text);
                        Match pwdMatch = pwdReg.Match(pwd);
                        string pwdFit = pwdMatch.Value.ToString();

                        if (pwdFit.Length != 0)
                        {
                            if (textBox3.Text.ToString() == textBox4.Text.ToString())
                            {
                                string connOrder2 = "data source=192.168.222.161;" + "database=grant;" + "user id=root;" + "password = sztop05@MGT";
                                MySqlConnection conn2 = new MySqlConnection(connOrder2);
                                conn2.Open();


                                string change = "update member set Password = '" + textBox3.Text + "' where Name = '" + textBox1.Text + "';";
                                MySqlCommand comInss = new MySqlCommand(change, conn2);
                                comInss.ExecuteNonQuery();
                                conn2.Close();

                                this.GetStrValue = strDisplay(name);
                                textBox1.Text = null;
                                textBox2.Text = null;
                                textBox3.Text = null;
                                textBox4.Text = null;

                                MessageBox.Show("密码修改成功！");
                                this.Close();
                                
                            }
                            else 
                            {
                                MessageBox.Show("新密码输入不一致！");
                            }



                            break;
                        }
                        else
                        {
                            MessageBox.Show("原密码错误！");
                            break;
                        }
                    }

                }


            }
            else 
            {
                MessageBox.Show("请输入完整信息！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form2 newform = new Form2();
            newform.ShowDialog();
            newform.Dispose();
            this.Show();
        }
    }
}