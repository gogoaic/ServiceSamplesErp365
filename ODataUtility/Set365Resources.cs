//using Microsoft.OData.Client;
//using ODataUtility.Microsoft.Dynamics.DataEntities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Aut


//namespace ODataUtility
//{
//    public class Set365Resources
//    {
//        public static string ODataEntityPath = ClientConfiguration.Default.UriString + "data";
//        Uri oDataUri = new Uri(ODataEntityPath, UriKind.Absolute);

//        public Resources Context { get; set; }

//        public Set365Resources()
//        {
//            this.Context = new Resources(oDataUri);

//            this.Context.SendingRequest2 += new EventHandler<SendingRequest2EventArgs>(delegate (object sender, SendingRequest2EventArgs e)
//            {
//                var authenticationHeader = OAuthHelper.GetAuthenticationHeader(useWebAppAuthentication: true);
//                e.RequestMessage.SetHeader(OAuthHelper.OAuthHeader, authenticationHeader);
//            });
//        }
//    }
//}
