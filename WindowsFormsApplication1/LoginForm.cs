using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicrophoneRecord
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            string email,password;
            //new form1();
             email= LoginBox.Text;
            password = PasswordBox.Text;
            if (email == "att11@zips.uakron.edu"&& password=="zippy") {
            new Form1().Show();
            }
            else {
                label3.Text = "Error: Try Again";
            }
        
         
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
