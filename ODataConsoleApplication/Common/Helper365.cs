using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ODataConsoleApplication.Common
{
    public static class Helper365
    {
        public static void SetFakeSslCertificate()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                                                  SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }

        public static string SetConsoleData(string parameterName)
        {
            Console.WriteLine($"{parameterName}: ");
            return Console.ReadLine();
        }
    }
}
