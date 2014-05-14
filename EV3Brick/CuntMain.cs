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
                Logger.Log("ERROR: Exception occured!" + ex.Message);
                Thread.Sleep(5000);
            }
        }
    }
}
