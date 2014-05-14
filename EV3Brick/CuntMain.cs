using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SPSGrp1Grp2.Cunt.Logging;

namespace SPSGrp1Grp2.Cunt
{
    class CuntMain
    {

        /// <summary>
        /// The entry point of the main program.
        /// </summary>
        public static void Main()
        {
            try
            {
                StateController.IsDebug = false;
                Logger.Log("Debugging disabled.");
                // start the robot AState update thread with default state
                new StateController("Init").Run();
            }
            catch (Exception ex)
            {
                Logger.Log("Sys-ERROR");
                /*
                string s = ex.Message;
                int subLen = 30;
                int i = 0;
                var eMes = new List<string>();
                while (i < s.Length)
                {
                    if (subLen >= s.Length - i)
                    {
                        eMes.Add(s.Substring(i, subLen - 1));
                    }
                    else
                    {
                        eMes.Add(s.Substring(s.Length - i, s.Length - 1));
                    }
                    i = i + subLen;
                }
                foreach(string sm in eMes)
                {
                    Logger.Log(sm);
                }
                */
                Logger.Log(ex.Message);
                Logger.Log(ex.Message.Substring(ex.Message.Length-20, ex.Message.Length));
                Thread.Sleep(5000);
                StateController.Exit();
            }
        }
    }
}
