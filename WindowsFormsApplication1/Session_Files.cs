using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//This is the main class that holds all the audio and text infrormation for  a given session . 
// The class maintains a record of each audio file being produced and text file being produced.
namespace MicrophoneRecord
{
    //This class is the parent class for the 2 derived classes below.
    //This class keeps general information about the class Like the users name the date it was recorded on and the root url for all of the other sessions.
    public class Session_Files
    {
        private string Root_Session_Url = "C:\\Users\\Andrew\\Documents\\Visual Studio 2015\\Projects\\micprogram\\Files";
        private string Session_Name="";
        protected string Session_Date="";
        private string User_Name = "";
      
        private bool session_created = false;

        //This constructor is used to get the date and assign the user name for the main session the subject for this session is default as 'generic'
        public Session_Files(string User_N,bool dir_this_session)
        {
        DateTime thisDay = DateTime.Today;

            Session_Date = String.Format("{0:MM.dd.yyyy}", thisDay);
            Session_Name = "Generic";
            User_Name = User_N;
            Update_URL();
            this.Make_Session_Directory();
            session_created = dir_this_session;

        }
        //This constructor is used to get the date and assign the user name for the main session and sets the session subject as well 
        public Session_Files( string User_N, string Session_N, bool dir_this_session)
        {
            DateTime thisDay = DateTime.Today;

            Session_Date = String.Format("{0:MM.dd.yyyy}", thisDay);
            Session_Name = Session_N;
            User_Name = User_N;
            Update_URL();
            session_created = dir_this_session;
            this.Make_Session_Directory();
            

        }
        //This updates the Url with all the approiate information that was used when setuping the recording
        private void Update_URL() {
            Root_Session_Url = Root_Session_Url + "\\" +User_Name +"_Sessions\\"+ Session_Name + "\\" + Session_Date;

        }
        //Returns the root session url. This is meant for child classes. 
        protected string get_Session_URL() {

            return Root_Session_Url;
        }
        //This is a function to make sure there is no files in the path you are about to write. 
        public bool Check_If_Folder_Clear(string total_session)
        {
            return System.IO.Directory.Exists(total_session);
 
        }
        //This function creates the directory for the session. It just creates the base session however.
        //This function also checks if there is any files in the path specified. If there is another form appears and confirms if it they want the files deleted.
        //If they confirm all the files are deleted and then a new directory is created. The odds of this happening are slim becuase most people only need one recording per class per day.
        public  bool Make_Session_Directory()
        { bool choice = false;
            if (session_created == false)
            {
                string session_URL = get_Session_URL();
                if (!Check_If_Folder_Clear(Root_Session_Url))
                {
                    System.IO.Directory.CreateDirectory(Root_Session_Url);


                }
                else
                {
                    var Pop_up = new Pop_Up_Warning(Root_Session_Url);
                    Pop_up.ShowDialog();
                    //Application.Run(Pop_up);
                    choice = Pop_up.Get_Choice();
                    if (choice == true)
                    {

                        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(session_URL);

                        foreach (System.IO.FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (System.IO.DirectoryInfo dir in di.GetDirectories())
                        {
                            dir.Delete(true);
                        }
                        System.IO.Directory.CreateDirectory(Root_Session_Url);
                    }

                }
            }
            return true;
        }

    }

    //This is a child class that handles the audio side of the session.
    //There is a link list that holds a type called audio nodes. Audio nodes is the information pertaining to the audio file.
    //We also have a Root_Audio _URL to specify the url for the audio portion.
    //Finally we have a generic audio file name used to have a common naming shceme for each file. 

    class Audio_Session_Files: Session_Files
    {

        LinkedList<Audio_Node> Audio_List = new LinkedList<Audio_Node>();
        private string Root_Audio_Url = "";
        private int count = 0;
        private string Audio_File_Name = "Audio_Sample";

        //This Constructor sends the approratie info to the base session class while creating the name for the audio files from this session. 
        //There is no subject so it is replaced with generic.
        //The constructor also creates the root audio url and creates the directory for the audio files to be stored properly.  
        public Audio_Session_Files(string Name, bool dir_this_session) : base(Name, dir_this_session)
        {
            string session_URL = get_Session_URL();
            Create_Audio_Url(session_URL);
            this.Make_Audio_Directory();
            Audio_File_Name = Name + "_Generic_" + Session_Date + "_" + Audio_File_Name;

        }
        //This Constructor sends the approratie info to the base session class while creating the name for the audio files from this session. 
        //The format folows the name of the user the subject of the session the date it is recorded on and then the number of audio sample.
        //The constructor also creates the root audio url and creates the directory for the audio files to be stored properly.  
        public Audio_Session_Files(string Name,string session, bool dir_this_session) : base(Name,session, dir_this_session) {
            string session_URL = get_Session_URL();
            Create_Audio_Url(session_URL);
            this.Make_Audio_Directory();
            Audio_File_Name = Name + "_" + session + "_" + Session_Date + "_" + Audio_File_Name;

        }
        //This is the same as the Make directory in the base class but is used for just the audio folder only. While the session is both audio and text.
        public  bool Make_Audio_Directory()
        {
            string session_URL = get_Session_URL();
            if (!System.IO.Directory.Exists(Root_Audio_Url))
            {
                System.IO.Directory.CreateDirectory(Root_Audio_Url);
            }
            else {
                bool choice = false;
                var Pop_up = new Pop_Up_Warning(Root_Audio_Url);
                Pop_up.ShowDialog();
                choice = Pop_up.Get_Choice();
                if (choice == true)
                {
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(session_URL);

                    foreach (System.IO.FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (System.IO.DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                    System.IO.Directory.CreateDirectory(Root_Audio_Url);
                }

            }
            return true;
        }

        //This creates the Root url for the audio 
        void Create_Audio_Url(string root)
        {
            Root_Audio_Url = root + "\\Audio\\";
        }
        //This creates a new instance of a audio file. 
        //This provides the audio file with the apporaptie string to be called so the audio is made in the right place.
        //The audio files is then put in the internal session audio list through the information of the audio_node.
        public string create_audiofile_path() {
            string Audio_File_Name_Final = "";
            string Audio_File_Name_Path = "";
            count = count + 1;
            Audio_File_Name_Final = Audio_File_Name+count.ToString();
            Audio_File_Name_Path = Root_Audio_Url + Audio_File_Name_Final + ".wav";
            Audio_Node temp =new Audio_Node();
            temp.Set_Audio_Number(count);
                temp.Set_Audio_Location(Audio_File_Name_Path);
            Audio_List.AddLast(temp);
            return Audio_List.Last.Value.Get_Audio_Location(); }

        //This returns the current audio path it is on
        public string get_audiofile_path() {
            string Audio_File_Name_Path = "";
            Audio_File_Name_Path = Root_Audio_Url + Audio_File_Name + count.ToString()+".wav";
            return Audio_File_Name_Path;
        }

        //This class stores all the aspects of the audio file.
        //There is setters and getters for each var for eassy asscess and to make it error resistant.
        //The ifo it gathers is the path to the audio file specificly (so the path with the name of the file) 
        //and the number of the audio sample in the session.
        class Audio_Node {
            private string Audio_Url = "";
            private int Position;
           public Audio_Node() { }
            public Audio_Node(string file,int pos) {
                Audio_Url = file;
                Position = pos;

            }
           public void Set_Audio_Location(string url)
            { Audio_Url = url; }
            public string Get_Audio_Location()
            { return Audio_Url; }
            public void Set_Audio_Number(int pos)
            { Position = pos; }
            public int Get_Audio_Number()
            { return Position; }
        }
        //This prints all the audio info to visualise for a user. 
        public string Print_Audio_Info()
        {
            string final_message = "";
            LinkedListNode<Audio_Node> temp = Audio_List.First;
            while (temp !=null)
            {
                final_message = final_message +"Audio Number" + temp.Value.Get_Audio_Number().ToString()+ "  Url: "+temp.Value.Get_Audio_Location() + System.Environment.NewLine;
                temp = temp.Next;
            }
            return final_message;
        }


        //This setups the audio files to create one master audio file.
        //The setup is creating its own directory and setuping the audio files to be fed in the Combine_Audio_Files. 
        public void Create_Master_Audio_File(){
            string Combined_Session_Wav_Path = "\\Final_Session_Output";
            Combined_Session_Wav_Path = Root_Audio_Url+ Combined_Session_Wav_Path;
            if (System.IO.Directory.Exists(Combined_Session_Wav_Path)) {

                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Combined_Session_Wav_Path);
                foreach (System.IO.FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (System.IO.DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
                
            }

            System.IO.Directory.CreateDirectory(Combined_Session_Wav_Path);

            string Combined_Session_Wav_File = Combined_Session_Wav_Path + "\\Final_Audio.wav";
            Combine_Audio_Files(Combined_Session_Wav_File, Root_Audio_Url);

        }

        //This function combines each of the individual audio files into one master audio file for refrenece. 
        public static void Combine_Audio_Files(string outputFile, string sourceFiles)
        {
            byte[] buffer = new byte[1024];
            NAudio.Wave.WaveFileWriter waveFileWriter = null;
            string sourceFile="";
            try
            {
           
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(sourceFiles);
                    foreach (System.IO.FileInfo file in di.GetFiles())
                    {
                        sourceFile = sourceFiles+file.Name;
                    
                    using (WaveFileReader reader = new WaveFileReader(sourceFile))
                    {
                        if (waveFileWriter == null)
                        {
                            // first time in create new Writer
                            waveFileWriter = new WaveFileWriter(outputFile, reader.WaveFormat);
                        }
                        else
                        {
                            if (!reader.WaveFormat.Equals(waveFileWriter.WaveFormat))
                            {
                                throw new InvalidOperationException("Can't concatenate WAV Files that don't share the same format");
                            }
                        }

                        int read;
                        while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            waveFileWriter.WriteData(buffer, 0, read);
                        }
                    }
                }
            }
            finally
            {
                if (waveFileWriter != null)
                {
                    waveFileWriter.Dispose();
                }
            }

        }

    }



    //This is a child class that handles the translated text side of the session.
    //There is a link list that holds a type called Text_Translate_Nodes. Text_Translate_Node is the information pertaining to the translated text file.
    //We also have a Root_Text_Url to specify the url for the translated text portion.
    //Finally we have a generic text file name  called Text_File_Name. This is used to have a common naming shceme for each file created. 

    public class   Text_Session_Files : Session_Files
    {

        LinkedList<Text_Translate_Node> Text_Translation_List = new LinkedList<Text_Translate_Node>();
       
        private string Root_Text_Url = "";
        private int count = 0;
        private string Text_File_Name = "Audio_Translation_Sample";

        //This Constructor sends the approratie info to the base session class while creating the name for the translated text files from this session. 
        //There is the user did not input a subject for the recording so it is replaced with generic.
        //The constructor also creates the Root Text Url while creating  the directory for the audio files to be stored properly.  
        public Text_Session_Files(string Name, bool dir_this_session) : base(Name, dir_this_session)
        {
            string session_URL = get_Session_URL();
            Create_Text_Url(session_URL);
            this.Make_Translation_Directory();
            Text_File_Name = Name + "_Generic_" + Session_Date + "_" + Text_File_Name;

        }

        //This Constructor sends the approratie info to the base session class while creating the name for the translated text files from this session. 
        //The format folows the name of the user the subject of the session the date it is recorded on and then the number of translated text sample.
        //The constructor also creates the root text url while creating the directory for the audio files to be stored properly.  
        public Text_Session_Files(string Name, string session, bool dir_this_session) : base(Name, session, dir_this_session)
        {
            string session_URL = get_Session_URL();
            Create_Text_Url(session_URL);
            this.Make_Translation_Directory();
            Text_File_Name = Name + "_"+ session +"_"+ Session_Date + "_" + Text_File_Name;
        }

        //This is the same as the Make directory in the base class but is used for just the translated_text folder only. While the base class is for audio and text.
        protected bool Make_Translation_Directory()
        {
            string session_URL = get_Session_URL();
            if (!System.IO.Directory.Exists(Root_Text_Url))
            {
                System.IO.Directory.CreateDirectory(Root_Text_Url);
            }
            else
            {
                bool choice = false;
                var Pop_up = new Pop_Up_Warning(Root_Text_Url);
                Pop_up.ShowDialog();
                choice = Pop_up.Get_Choice();
                if (choice == true)
                {
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(session_URL);

                    foreach (System.IO.FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (System.IO.DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                    System.IO.Directory.CreateDirectory(Root_Text_Url);
                }

            }
            return true;
        }

        //This creates the Root url for the translated_text 

        void Create_Text_Url(string root)
        {
            Root_Text_Url = root + "\\Text\\";
        }

        //This creates a new instance of a text file. 
        //This creates the text file with the from the input of the  string from the google translate. 
        //The text files is then put in the internal session audio list through the information of the audio_node.
        public void Create_Translate_Text_File(string message)
        {
            string Text_File_Name_Path = "";
            count = count + 1;
            string final_Text_File_Name;
            final_Text_File_Name = Text_File_Name+count.ToString();
            Text_File_Name_Path = Root_Text_Url + final_Text_File_Name + ".txt";
            Text_Translate_Node temp = new Text_Translate_Node();
            temp.Set_Text_Number(count);
            temp.Set_Text_Location(Text_File_Name_Path);
            temp.Set_Translated_Message(message);
            Text_Translation_List.AddLast(temp);
            using (System.IO.StreamWriter sw = System.IO.File.CreateText(Text_File_Name_Path))
            {
                

                sw.WriteLine(message);
                temp.Set_Writer_Stream(sw);
            }
        }
        //This returns the current path  the text file it is on
        public string Get_Translate_Text_File_Path()
        {
            string Text_File_Name_Path = "";
            Text_File_Name_Path = Root_Text_Url + Text_File_Name + ".txt";
            return Text_File_Name_Path;
        }


        //This class stores all the aspects of the translated text file.
        //There is setters and getters for each var for eassy asscess and to make it error resistant.
        //The info it gathers is the path to the translated text file specificly (so the path with the name of the file) 
        //the number of the tranaslated text of the audio sample it is associated with  in the session.
        //It also stores the message of each message the speech api translates. Finally it keeps the write stream if more info  needs to be added to the text file. 
        public class Text_Translate_Node
        {
            private string Translate_Text_Url = "";
            private int Position;
            private string Translate_Message="";
            System.IO.StreamWriter Writer_For_Text;
            public Text_Translate_Node() { }
            public Text_Translate_Node(string file, int pos)
            {
                Translate_Text_Url = file;
                Position = pos;

            }
            public void Set_Text_Location(string url)
            { Translate_Text_Url = url; }
            public string Get_Text_Location()
            { return Translate_Text_Url; }
            public void Set_Text_Number(int pos)
            { Position = pos; }
            public int Get_Text_Number()
            { return Position; }

            public void Set_Translated_Message(string message)
            { Translate_Message = message; }
            public string Get_Translated_Message()
            { return Translate_Message; }

            public void Set_Writer_Stream(System.IO.StreamWriter sw)
            { Writer_For_Text = sw; }
            public System.IO.StreamWriter Get_Writer_Stream()
            { return Writer_For_Text; }
        }

        //This function returns the link list of all the Text_Translate_Nodes so it can be displayed in the text output form
        public LinkedList<Text_Translate_Node> Get_Link_List() {
            return Text_Translation_List;

        }

        //This combines all the information from every text file into one text file.
        //There is some formating that is needed for legability. For example a new line for each session.
        public string Print_Text_Info()
        {
            string final_message = "";
            LinkedListNode<Text_Translate_Node> temp = Text_Translation_List.First;
            while (temp != null)
            {
                final_message = final_message + "Translation Number: " + temp.Value.Get_Text_Number().ToString() + "  Message: " + temp.Value.Get_Translated_Message() + System.Environment.NewLine;
                temp = temp.Next;
            }
            return final_message;

        }


        //This creates a text file that has all the message at once for the user to look at.
        //There is a special directory that is created as well as a unique text file with all the speech translations. 
        public void Create_Master_Translation_Text_File()
        {
            string Final_Message;
            string Combined_Session_text_Path = "\\Final_Session_Output";
            Combined_Session_text_Path = Root_Text_Url + Combined_Session_text_Path;
            if (System.IO.Directory.Exists(Combined_Session_text_Path))
            {

                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Combined_Session_text_Path);
                foreach (System.IO.FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (System.IO.DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

            }

            System.IO.Directory.CreateDirectory(Combined_Session_text_Path);

            string Combined_Session_Text_File = Combined_Session_text_Path + "\\Final_Translated_Text.txt";
            Final_Message = Print_Text_Info();
            count = count + 1;
            Text_Translate_Node temp = new Text_Translate_Node();
            temp.Set_Text_Number(count);
            temp.Set_Text_Location(Combined_Session_Text_File);
            temp.Set_Translated_Message(Final_Message);
            Text_Translation_List.AddLast(temp);

            using (System.IO.StreamWriter sw = System.IO.File.CreateText(Combined_Session_Text_File))
            {


                sw.WriteLine(Final_Message);

                temp.Set_Writer_Stream(sw);
            }


        }


   

    }


    
}
