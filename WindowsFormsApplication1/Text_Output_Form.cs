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
{//Thia class displays the output of the translated text from the recordings of that session. 

    //This is done by passing in a linked list of translated messages genereated from Sessions_File.cs
    //This function will then tranverse that function and insert the approiate information in the list view. 
    public partial class Text_Output_Form : Form
    {
        
        LinkedList<Text_Session_Files.Text_Translate_Node> Text_List = new LinkedList<Text_Session_Files.Text_Translate_Node>();
        public Text_Output_Form(LinkedList<Text_Session_Files.Text_Translate_Node> x)
        {
            Text_List = x;
            InitializeComponent();


            LinkedListNode<Text_Session_Files.Text_Translate_Node> temp = Text_List.First;
            while (temp.Next != null)
            {
                ListViewItem val = new ListViewItem(temp.Value.Get_Text_Number().ToString());
                val.SubItems.Add(temp.Value.Get_Translated_Message());
                listView1.Items.Add(val);
                temp = temp.Next;
            }

   
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //This will close the form and return to the main recording form (form1)
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
