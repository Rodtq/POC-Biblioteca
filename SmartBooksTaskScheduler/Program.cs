using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SmartBooksTaskScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            using (CookieWebClient urlTrigger = new CookieWebClient())
            {
                //string authUrl = "http://localhost:85/Login/Index";
                string mailServiceUrl = "http://localhost:85/SmartBooks/Mail";
                //string credentials = "UserName=rodtq&Password=asdfg";
                urlTrigger.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                //urlTrigger.UploadString(authUrl, credentials);

                try
                {
                    //urlTrigger.OpenRead(args[0]);
                    urlTrigger.DownloadString(mailServiceUrl);

                }
                catch (WebException ex)
                {
                    Console.WriteLine(string.Format("***Please enter a valid URL*** \n Error : {0}", ex.InnerException.Message));
                }
                catch (NotSupportedException ex)
                {
                    Console.WriteLine(string.Format("***Please enter a valid URL*** \n Error : {0}", ex.InnerException.Message));
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine(string.Format("***Please enter a valid URL*** \n Error : {0}", ex.InnerException.Message));
                }
            }
        }
    }
}
