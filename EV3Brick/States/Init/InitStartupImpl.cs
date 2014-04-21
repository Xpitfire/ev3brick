using System;
using System.Threading;

namespace PrgSps2Gr1
{
    class InitStartupImpl : State
    {   

        public InitStartupImpl(ProgramEv3Sps2Gr1 project)
        {
            Controller = project;
			// start the general sensor monitoring thread
			var thread = new Thread(new ThreadStart(WorkThreadFunction));
			thread.Start();
        }

        protected override void PerformAction()
        {
            //Ev3.SavedColor = Ev3.ScanColor();
            //Ev3.WriteLine(Ev3.SavedColor.ToString());
            SetState(new NormalDriveImpl());
        }

        public override String ToString()
        {
            return "InitStartup";
        }
    }
}
