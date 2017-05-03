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
{    //This function works to confirm or deny a overwrite operation.
    //If the person does nothing nothing will happen. 
    public partial class Pop_Up_Warning : Form
    {
        //Is the boolean to determine if the files neede to be deleted or not. 
        bool confirm_delete=false;
        public Pop_Up_Warning(string path_name)
        {
            InitializeComponent();
            Path_Name.Text = path_name;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Yes_Button_Click(object sender, EventArgs e)
        {
            confirm_delete = true;
            this.Close();
        }

        private void No_Button_Click(object sender, EventArgs e)
        {
            confirm_delete = false;
            this.Close();
        }
        public bool Get_Choice() {
            return confirm_delete;

        }
    }
}
