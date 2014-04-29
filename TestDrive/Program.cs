using System;
using System.Threading;
using Sps2Gr1.InTeam;
using Sps2Gr1.InTeam.Logging;

namespace SpsGr1.InTeam
{
    class Program
    {

        /// <summary>
        /// The entry point of the test program.
        /// </summary>
        public static void Main()
        {
            try
            {
                StateController.IsDebug = true;
                Logger.Log("Debugging disabled.");
                // start the robot AState update thread with default state
                new StateController("TestDrive").Run();
            }
            catch (Exception ex)
            {
                Logger.Log("ERROR: Exception occured!" + ex.Message);
                Thread.Sleep(5000);
            }
        }
    }
}
