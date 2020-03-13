using Microsoft.OData.Client;
using Newtonsoft.Json;
using ODataConsoleApplication.Common;
using ODataUtility.Microsoft.Dynamics.DataEntities;
using System;
using System.Linq;

namespace ODataConsoleApplication.ContractData
{
    public static class UpdateContract
    {
        public static bool Process(string choice)
        {
            bool succ = false;

            string _persNum = string.Empty;
            DateTime _endDate = DateTime.Now;

            if (choice == "1")
            {
                _persNum = "1889";
                _endDate = new DateTime(2019, 12, 16);
            }
            else if (choice == "2")
            {
                Console.Write("Personnel Number: ");
                _persNum = Console.ReadLine();
                Console.Write("EndDate (yyyy-mm-dd): ");
                string _endDateStr = Console.ReadLine();
                try
                {
                    _endDate = DateTime.Parse(_endDateStr);
                }
                catch
                { }
            }

            succ = UpdateContractConsoleSimple(_persNum, _endDate);

            return succ;
        }

        

        public static bool UpdateContractConsoleSimple(string pPersonnelNumber, DateTime pEndEmploymentDate)
        {
            try
            {
                Resources _contextUpd = Queries365.CreateErpContext();

                var queryUpd = from Employment
                            in _contextUpd.Employments
                               where Employment.PersonnelNumber == pPersonnelNumber
                               orderby Employment.EmploymentStartDate descending
                               select Employment;

                DataServiceCollection<Employment> employmentCollection = new DataServiceCollection<Employment>(queryUpd);

                if (employmentCollection != null)
                {
                    foreach (Employment emp in employmentCollection)
                    {
                        Console.WriteLine($"OLD EMPLOYMENT: {JsonConvert.SerializeObject(emp)}");
                        LogHelper.WriteDevLog($"OLD EMPLOYMENT: {JsonConvert.SerializeObject(emp)}");
                        emp.EmploymentEndDate = pEndEmploymentDate.ToUniversalTime();

                        _contextUpd.UpdateObject(emp);
                        Console.WriteLine($"IN MEMORY CHANGED EMPLOYMENT: {JsonConvert.SerializeObject(emp)}");
                        LogHelper.WriteDevLog($"IN MEMORY CHANGED EMPLOYMENT: {JsonConvert.SerializeObject(emp)}");
                        break;
                    }
                }

                DataServiceResponse response = _contextUpd.SaveChanges(SaveChangesOptions.PostOnlySetProperties);

                // for testing (same context Check)
                Employment emp2 = Queries365.GetContract(_contextUpd, pPersonnelNumber);
                Console.WriteLine($"FROM SAME CONTEXT EMPLOYMENT: {JsonConvert.SerializeObject(emp2)}");
                LogHelper.WriteDevLog($"FROM SAME CONTEXT EMPLOYMENT: {JsonConvert.SerializeObject(emp2)}");

                // new context refreshed data
                Resources _newContext = Queries365.CreateErpContext();
                Employment empAfterNew = Queries365.GetContract(_newContext, pPersonnelNumber);
                Console.WriteLine($"AFTER UPDATE NEW CONTEXT: {JsonConvert.SerializeObject(empAfterNew)}");
                LogHelper.WriteDevLog($"AFTER UPDATE NEW CONTEXT: {JsonConvert.SerializeObject(empAfterNew)}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating contract with Id {pPersonnelNumber} with Exception: {ex} ");
                return false;
            }
        }
    }
}
