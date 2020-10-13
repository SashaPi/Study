using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoginForm2;

// 1 step
using MySql.Data.MySqlClient;

namespace MarathonSkills2015
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string connection = @"server=localhost;user id=Admin;password=studentKP11;persistsecurityinfo=True;database=isip13";
            //string Query = "select * from users where usersname = '" + tb_login.Text + "' and userspassword = '" + tb_password.Text + "'";
            string Query = "select idusers, usersName, usersPassword, roleName from users, role where usersname = '" + tb_login.Text + "' and userspassword = '" + tb_password.Text + "' and usersRole = idrole";
            MySqlConnection myCon = new MySqlConnection(connection);
            MySqlCommand myCommand = new MySqlCommand(Query,myCon);
            myCon.Open();
            myCommand.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(myCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if ((tb_login.Text != "") && (tb_password.Text != ""))
            {
                if (dt.Rows.Count == 1)
                {
                    //MessageBox.Show("Добро пожаловать!");
                    if (dt.Rows[0][3].ToString() == "Admin")
                    {
                        new adminMain().Show();
                        Hide();
                    }
                    else if (dt.Rows[0][3].ToString() == "User")
                    {
                        new userMain().Show();
                        Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Учетная запись не найдена!");
                }
            }
            else
            {
                MessageBox.Show("Поля не заполнены!");
            }
            //tb_login.Clear();
            //tb_password.Clear();
            myCon.Close();
        }
    }
}
