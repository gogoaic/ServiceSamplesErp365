﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationUtility
{
    public partial class ClientConfiguration
    {
        public static ClientConfiguration Default { get { return ClientConfiguration.OneBox; } }

        public static ClientConfiguration OneBox = new ClientConfiguration()
        {
            // You only need to populate this section if you are logging on via a native app. For Service to Service scenarios in which you e.g. use a service principal you don't need that.
            UriString = "https://seavus-test.sandbox.operations.dynamics.com/", // "https://seavus.operations.dynamics.com/"
            UserName = "tusr1@TAEOfficial.ccsctp.net",
            // Insert the correct password here for the actual test.
            Password = "",

            // You need this only if you logon via service principal using a client secret. See: https://docs.microsoft.com/en-us/dynamics365/unified-operations/dev-itpro/data-entities/services-home-page to get more data on how to populate those fields.
            // You can find that under AAD in the azure portal
            ActiveDirectoryResource = "https://seavus-test.sandbox.operations.dynamics.com", // "https://seavus.operations.dynamics.com"  // "https://ontrax.se/ea19b02b-cb7f-4831-b59a-75fc22b848d5", // Don't have a trailing "/". Note: Some of the sample code handles that issue.
            ActiveDirectoryTenant = "https://login.microsoftonline.com/fcd20beb-0cdf-44e2-b774-e90b716d31b9", //"https://ontrax.se/ea19b02b-cb7f-4831-b59a-75fc22b848d5", //"https://testdiapi", // "https://login.windows-ppe.net/TAEOfficial.ccsctp.net", // Some samples: https://login.windows.net/yourtenant.onmicrosoft.com, https://login.windows.net/microsoft.com
            ActiveDirectoryClientAppId = "cf60a37a-36f5-47e4-b23b-858fcb13d446",
            // Insert here the application secret when authenticate with AAD by the application
            ActiveDirectoryClientAppSecret = "zlsazlTBn4fF7qWJS9610lSQm/m4Q8oAELIjnZvAYrY=",

            // Change TLS version of HTTP request from the client here
            // Ex: TLSVersion = "1.2"
            // Leave it empty if want to use the default version
            TLSVersion = "",
        };

        public string TLSVersion { get; set; }
        public string UriString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ActiveDirectoryResource { get; set; }
        public String ActiveDirectoryTenant { get; set; }
        public String ActiveDirectoryClientAppId { get; set; }
        public string ActiveDirectoryClientAppSecret { get; set; }
    }
}
