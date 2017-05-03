using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Globalization;
using System.Timers;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrophoneRecord;

namespace WindowsFormsApplication1
{




    public partial class Form1 : Form
    {
        private bool button_Pressed=false;
        private int count;
        private string Old_File_Format="";
        private string date;
        private string username;
        bool timer_switch = false;
        string current_data = "";
        Audio_Session_Files audio_files;
        Text_Session_Files Translation_files;



      //Constructor for form one. This is to initalize the form with a generic user
        public Form1()
        {
            
            InitializeComponent();
            label3.Text = "Welcome: User" + System.Environment.NewLine+"Name the class you would like to record.";
        }
        //Constructor for form one. This is to initalize the form with the the proper username wh
        public Form1(string email)
        {
            
            InitializeComponent();
            username = email;
            label3.Text = "Welcome: "+ System.Environment.NewLine + username + System.Environment.NewLine + "Name the class you would like to record.";
        }


  //Get List of sources for a mic
        private void button1_Click_1(object sender, EventArgs e)
        {
            List<NAudio.Wave.WaveInCapabilities> sources = new List<NAudio.Wave.WaveInCapabilities>();
            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {

                sources.Add(NAudio.Wave.WaveIn.GetCapabilities(i));
            }
            SourceList.Items.Clear();
            foreach (var source in sources) {

                ListViewItem Item = new ListViewItem(source.ProductName);
                Item.SubItems.Add(new ListViewItem.ListViewSubItem(Item, source.Channels.ToString()));
                SourceList.Items.Add(Item);
            };
        }

        //Created some wave form
        private NAudio.Wave.WaveIn sourcestream = null;
        private NAudio.Wave.DirectSoundOut wavout = null;
        private NAudio.Wave.WaveFileWriter wavewriter = null;
        //Used to play out what is recording 
        private void button2_Click(object sender, EventArgs e)
        {
            sourcestream.DeviceNumber = 0;
            int deviceNumber = SourceList.SelectedItems[0].Index;
            sourcestream = new NAudio.Wave.WaveIn();
            sourcestream.WaveFormat = new NAudio.Wave.WaveFormat(44100, NAudio.Wave.WaveIn.GetCapabilities(0).Channels);
            NAudio.Wave.WaveInProvider waveIn = new NAudio.Wave.WaveInProvider(sourcestream);
            wavout = new NAudio.Wave.DirectSoundOut();
            wavout.Init(waveIn);
            sourcestream.StartRecording();
            wavout.Play();
        }
        //Starts the recording process
        private void button5_Click(object sender, EventArgs e)
        {
            int count = 1;
            label1.Text = "button5";
            Start_Recording(count.ToString());



        }
        //Checks if source stream is avlaible this for the above functions
        private void sourcestream_DataAvalible(object sender, NAudio.Wave.WaveInEventArgs e)
        {


            if (wavewriter == null) return;
            wavewriter.WriteData(e.Buffer, 0, e.BytesRecorded);
            wavewriter.Flush();
            ;
        }
        //Stops the recording process used to start translating
        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = "Stop Recording";
            if (wavout != null)
            {

                wavout.Stop();
                wavout.Dispose();
                wavout = null;
            }
            if (sourcestream != null) {

                sourcestream.StopRecording();
                sourcestream.Dispose();
                sourcestream = null;
            }

            if (wavewriter != null)
            {
                wavewriter.Dispose();
                wavewriter = null;
            }
        }
        //Exits the stops the recording and closes the form
        private void button4_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
            this.Close();
        }
        //Translate the last audio and sends in to a data struct in sessions_files.cs
        private void Translate_Click(object sender, EventArgs e)
        {
            Google_Trasnlate x = new Google_Trasnlate();
            current_data = x.Send_Value(Old_File_Format);
            Translation_files.Create_Translate_Text_File(current_data);
        }


        //Starts the recording process from computer mic
        public void Start_Recording(string File_Form) {
            string audio_location = File_Form;
            int deviceNumber = 0;
            sourcestream = new NAudio.Wave.WaveIn();
            sourcestream.DeviceNumber = deviceNumber;
            sourcestream.WaveFormat = new NAudio.Wave.WaveFormat(44100, 1);
            sourcestream.DataAvailable += new EventHandler<NAudio.Wave.WaveInEventArgs>(sourcestream_DataAvalible);
            try
            {
                wavewriter = new NAudio.Wave.WaveFileWriter(audio_location, sourcestream.WaveFormat);
            }
            catch {; }
            sourcestream.StartRecording();
            label1.Text = "Recording";

        }
  
        //This runs the show every 10 seconds a new recording session starts while the other audio file is processed onto another thread. This using parallel programing
        private void timer1_Tick(object sender, EventArgs e)
        {
            string new_File = "";
            button3_Click(sender, e);
            Old_File_Format = audio_files.get_audiofile_path();
            count = count + 1;
            new_File=audio_files.create_audiofile_path();
            Start_Recording(new_File);
            if (timer_switch == false)
            {
                timer_switch = true;
            }
            else {
                var t = Task.Run(() => Translate_Click(sender, e));

            }
            timer1.Interval = 10000;
        }
        //This stops the timer used to finssh recording
        private void Release_Text_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            label1.Text = "Stopped";
        }


        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        //This functions prints the paths for all the audio files.
        private void button1_Click(object sender, EventArgs e)
        { string mesages = "";
            mesages = audio_files.Print_Audio_Info();
        }

        //This is to start or stop the recordin process
        //To do this is starting and stoping the timer while enabling and disabling the button.
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (button_Pressed == false)
            {
                string Session;
                label1.Text = "Recording Session Started";
               
                button_Pressed = true;
                Session = Course_Name_Box.Text;
                if (Session != "")
                {
                    audio_files = new Audio_Session_Files(username, Session,false);
                    Translation_files= new Text_Session_Files(username, Session,true);
                }
                else {

                    audio_files = new Audio_Session_Files(username,false);
                    Translation_files=  new Text_Session_Files(username,true);
                }
                timer1.Enabled = true;
                button1.Enabled = false;
            }
            else {
                label1.Text = "Stopping";
                timer1.Enabled = false;
                button_Pressed = false;
                timer_switch = false;
                button3_Click(sender, e);
                Old_File_Format = audio_files.get_audiofile_path();
                var t = Task.Run(() => Translate_Click(sender, e));
                label1.Text = "finishing translation";
                Task.WhenAll(t);
                label1.Text = "Recording Session Over";
                audio_files.Create_Master_Audio_File();
                Translation_files.Create_Master_Translation_Text_File();
                button1.Enabled = true;
            }

        }

        //This function deliviers the translated text files to the test_output_form
        private void button1_Click_2(object sender, EventArgs e)
        {
            var Pop_up = new Text_Output_Form(Translation_files.Get_Link_List());
            Pop_up.ShowDialog();
         
        }
    }

 
}
    
