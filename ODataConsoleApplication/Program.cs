using AuthenticationUtility;
// using AuthenticationUtility.Microsoft.Dynamics.DataEntities;
using ODataUtility.Microsoft.Dynamics.DataEntities;
using Microsoft.OData.Client;
using System;
using System.Linq;
using ODataConsoleApplication.ContractData;
using ODataConsoleApplication.WorkerData;

namespace ODataConsoleApplication
{
    class Program
    {
        public static string ODataEntityPath = ClientConfiguration.Default.UriString + "data";

        static void Main(string[] args)
        {
            Console.WriteLine("1. CONTRACT with input data: 4097, new DateTime(2019, 12, 11)");
            Console.WriteLine("2. CONTRACT with new input data");
            Console.WriteLine();
            Console.WriteLine("3. WORKER with input data: 4097, ...");
            Console.WriteLine("4. WORKER with new input data");
            string choice = Console.ReadLine();
            if(choice == "1" || choice == "2")
                UpdateContract.Process(choice);
            else if(choice == "3" || choice == "4")
                UpdateWorker.Process(choice);
            else
            {
                Console.WriteLine("Wrong choice");
            }
            


        Console.WriteLine("Press any key to close the console");
            Console.ReadLine();
        }

       
    }
}
