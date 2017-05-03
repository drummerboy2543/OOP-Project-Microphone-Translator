using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ClientServices.Providers;

namespace WindowsFormsApplication1
{
    class Microsoft_Login_Library
    {

        //This will validate a microsoft login.  This will have been done by using microsoft 365 api. to verifty the user has a microsoft account.

        //I could not get it to work unfortantly thus this is the only function for the class. 
        public ClientFormsAuthenticationCredentials GetCredentials(string username, string password)
        {
            if (username!=null&&password!=null)
            {
                return new ClientFormsAuthenticationCredentials(
                    username, password,
                    false);
            }
            else
            {
                return null;
            }
        }


 
    }
}
