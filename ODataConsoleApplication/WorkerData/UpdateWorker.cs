using Microsoft.OData.Client;
using Newtonsoft.Json;
using ODataConsoleApplication.Common;
using ODataUtility.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODataConsoleApplication.WorkerData
{
    public class UpdateWorker
    {
        public static bool Process(string choice)
        {
            bool succ = false;

            string _persNum = string.Empty;
            string _email = string.Empty;
            string _phoneNum = string.Empty;

            if (choice == "3")
            {
                _persNum = "4097";
                _email = "email777";
                _phoneNum = "77777";
            }
            else if (choice == "4")
            {
                Console.Write("Personnel Number: ");
                _persNum = Console.ReadLine();
                Console.Write("Email: ");
                _email = Console.ReadLine();
                Console.Write("Phone Number: ");
                _phoneNum = Console.ReadLine();
            }

            succ = UpdateWorkerConsole(_persNum, _email, _phoneNum);

            return succ;
        }

        private static bool UpdateWorkerConsole(string _persNum, string _email, string _phoneNum)
        {
            ////
            bool success = true;
            Queries365.SetFakeSslCertificate();
            try
            {
                Resources _contextUpd = Queries365.CreateErpContext();

                var queryUpd = from Worker
                            in _contextUpd.Workers
                               where Worker.PersonnelNumber == _persNum
                               select Worker;

                DataServiceCollection<Worker> workerCollection = new DataServiceCollection<Worker>(queryUpd);

                Worker worBefore = Queries365.GetWorkerById(_contextUpd, _persNum);
                Console.WriteLine($"ORIGINAL WORKER: {JsonConvert.SerializeObject(worBefore)}");
                LogHelper.WriteDevLog($"ORIGINAL WORKER: {JsonConvert.SerializeObject(worBefore)}");

                if (workerCollection != null)
                {
                    foreach (Worker wrk in workerCollection)
                    {
                        try
                        {
                            bool shouldUpdate = false;
                            #region Update record
                            wrk.PrimaryContactEmail = _email;
                            wrk.PrimaryContactPhone = _phoneNum;
                            shouldUpdate = true;

                            if (shouldUpdate)
                            {
                                Console.WriteLine($"Updating.... worker with Personnel Number {_persNum}");
                                _contextUpd.UpdateObject(wrk);
                                Console.WriteLine($"IN MEMORY CHANGED EMPLOYMENT: {JsonConvert.SerializeObject(wrk)}");
                                LogHelper.WriteDevLog($"IN MEMORY CHANGED EMPLOYMENT: {JsonConvert.SerializeObject(wrk)}");
                                DataServiceResponse response = _contextUpd.SaveChanges(SaveChangesOptions.PostOnlySetProperties);
                                // check context record
                                Worker wrkAfter = Queries365.GetWorkerById(_contextUpd, _persNum);
                                Console.WriteLine($"AFTER UPDATE: {JsonConvert.SerializeObject(wrkAfter)}");
                                LogHelper.WriteDevLog($"AFTER UPDATE: {JsonConvert.SerializeObject(wrkAfter)}");
                                // check with new context
                                Resources _contextNew = Queries365.CreateErpContext();
                                Worker empAfterNew = Queries365.GetWorkerById(_contextNew, _persNum);
                                Console.WriteLine($"AFTER UPDATE NEW CONTEXT: {JsonConvert.SerializeObject(empAfterNew)}");
                                LogHelper.WriteDevLog($"AFTER UPDATE NEW CONTEXT: {JsonConvert.SerializeObject(empAfterNew)}");

                                // 2
                                //DataServiceResponse response = _contextUpd.SaveChanges(
                                //SaveChangesOptions.PostOnlySetProperties /*| SaveChangesOptions.BatchWithSingleChangeset*/);
                            }
                            else
                            {
                                Console.WriteLine($"No changes for Contract with Personnel Number {_persNum}");
                            }
                            success = true;
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteErrorLog($"Error updating Contract for Personnel Number {_persNum} with Exception: {ex}");
                            success = false;
                        }
                        #endregion
                        break;
                    }
                }

                return success;
                //
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog($"On Create Contract for number:{_persNum}, Exception: {ex}");
                return false;
            }
            ////
        }
       
    }
}
/*
 
     
     
     */
