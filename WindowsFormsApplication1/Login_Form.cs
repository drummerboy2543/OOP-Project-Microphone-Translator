using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    //This class holds a couple of variables to keep track the username and the password. The user name is then sent back 
    
    public partial class Login_Form : Form
    {
        string Username = "";
        string Password = "";
        public Login_Form()
        {
            
            InitializeComponent();
        }
        //This sends the proper information to the next form
        //If the password is blank it will ask for a password before proceeding
        private void button1_Click(object sender, EventArgs e)
        {
            Username = Email_Textbox.Text;
            Password = Password_Textbox.Text;
            if (Password == "")
            {
                MessageBox.Show("You need to provide a password to login.");
            }
            else
            {
                submit_data();
                this.Close();
            }
        }
        public string submit_data() {


            return Username;
        }

    }
}
