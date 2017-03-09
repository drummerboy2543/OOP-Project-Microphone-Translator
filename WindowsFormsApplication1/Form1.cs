using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

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


        private NAudio.Wave.WaveIn sourcestream = null;
        private NAudio.Wave.DirectSoundOut wavout = null;
        private NAudio.Wave.WaveFileWriter wavewriter = null;
        private void button2_Click(object sender, EventArgs e)
        { 
            if (SourceList.SelectedItems.Count==0) return;

            int deviceNumber = SourceList.SelectedItems[0].Index;
            sourcestream = new NAudio.Wave.WaveIn();
            sourcestream.DeviceNumber = deviceNumber;
            sourcestream.WaveFormat = new NAudio.Wave.WaveFormat(44100, NAudio.Wave.WaveIn.GetCapabilities(deviceNumber).Channels);

            NAudio.Wave.WaveInProvider waveIn = new NAudio.Wave.WaveInProvider(sourcestream);
            wavout = new NAudio.Wave.DirectSoundOut();
            wavout.Init(waveIn);
            sourcestream.StartRecording();
            wavout.Play();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (SourceList.SelectedItems.Count == 0) return;

            string audio_location = "C:\\Users\\Andrew\\Documents\\Visual Studio 2015\\Projects\\micprogram\\AudioFiles";
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Wave File (*.wav)|*.wav;";
            if (save.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            int deviceNumber = SourceList.SelectedItems[0].Index;
            sourcestream = new NAudio.Wave.WaveIn();
            sourcestream.DeviceNumber = deviceNumber;
            sourcestream.WaveFormat = new NAudio.Wave.WaveFormat(44100, NAudio.Wave.WaveIn.GetCapabilities(deviceNumber).Channels);
            sourcestream.DataAvailable += new EventHandler<NAudio.Wave.WaveInEventArgs>(sourcestream_DataAvalible);
            try
            {
                wavewriter = new NAudio.Wave.WaveFileWriter(save.FileName, sourcestream.WaveFormat);
            }
            catch {; }
            sourcestream.StartRecording();
        }
        private void sourcestream_DataAvalible(object sender, NAudio.Wave.WaveInEventArgs e)
        {


            if (wavewriter == null) return;
            wavewriter.WriteData(e.Buffer, 0, e.BytesRecorded);
            wavewriter.Flush();
            ;
        }

        private void button3_Click(object sender, EventArgs e)
        {
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

        private void button4_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
            this.Close();
        }

        
    }
}
    
