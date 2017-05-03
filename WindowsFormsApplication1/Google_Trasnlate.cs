using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Speech.V1;
using System.IO;
using System.Threading;


namespace WindowsFormsApplication1
{
   public class Google_Trasnlate
    {
        //This function calls the google speech api and translate the audio from the path provided.
        //The text is then returned as a string for more processing  
        //It willl print NO Response if google could not detect anything 

        public string Send_Value(string path)
        {
            string file_path = path;
            RecognitionAudio audio1 = RecognitionAudio.FromFile(file_path);
            SpeechClient client = SpeechClient.Create();
            RecognitionConfig config = new RecognitionConfig
            {

                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = 44100,
                LanguageCode = LanguageCodes.English.UnitedStates
            };
            RecognizeResponse response = client.Recognize(config, audio1);

            foreach (var result in response.Results)
            {
                foreach (var alternative in result.Alternatives)
                {
                    Console.WriteLine(alternative.Transcript);
                }
            }
            var output = response.Results;
            if (output.Count != 0) { 
            var finaloutput = output[0].Alternatives;
            return finaloutput[0].Transcript;
        }
          
        else {
        return "NO RESPONSE";
        }
        }

    }
}
