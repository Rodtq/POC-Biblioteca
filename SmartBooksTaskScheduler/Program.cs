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
            if (!args.Any())
            {
                return;
            }
            using (CookieWebClient urlTrigger = new CookieWebClient())
            {
                string uri = "AuthenticationURI";
                string credentials = "UserName=value&Password=value";
                urlTrigger.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                urlTrigger.UploadString(uri, credentials);

                try
                {
                    urlTrigger.OpenRead(args[0]);
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
