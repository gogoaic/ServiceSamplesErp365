using AuthenticationUtility;
using Microsoft.OData.Client;
using ODataUtility.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ODataConsoleApplication.Common
{
    public static class Queries365
    {
        internal static Employment GetContract(Resources context, string personnelNumber)
        {
            Employment emp = context.Employments.Where(x => x.PersonnelNumber == personnelNumber)
                .OrderByDescending(x => x.EmploymentStartDate).FirstOrDefault();

            return emp;
        }

        internal static Worker GetWorkerById(Resources context, string _personnelNumber)
        {
            Worker worker = context.Workers.Where(x => x.PersonnelNumber == _personnelNumber).ToList().FirstOrDefault();
            Console.WriteLine($"Employee: {worker?.Name}");

            return worker;
        }

        public static void SetFakeSslCertificate()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                                                  SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }

        public static Resources CreateErpContext()
        {
            Queries365.SetFakeSslCertificate();
            // Resource
            string ODataEntityPath = ClientConfiguration.Default.UriString + "data";
            Uri oDataUri = new Uri(ODataEntityPath, UriKind.Absolute);
            Resources context = new Resources(oDataUri);
            context.SendingRequest2 += new EventHandler<SendingRequest2EventArgs>(delegate (object sender, SendingRequest2EventArgs e)
            {
                var authenticationHeader = OAuthHelper.GetAuthenticationHeader(useWebAppAuthentication: true);
                e.RequestMessage.SetHeader(OAuthHelper.OAuthHeader, authenticationHeader);
            });

            return context;
        }
    }
}
