using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrgSps2Gr1
{
    class ExitProgramImpl : State
    {
        protected override void PerformAction()
        {
            Ev3.WriteLine("Stopping motors");
            Ev3.StopAllMovements();
            Ev3.WriteLine("Exiting application...");
            base.Controller.Exit();
        }
    }
}
