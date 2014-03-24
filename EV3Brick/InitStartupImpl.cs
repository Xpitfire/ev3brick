using System;

namespace PrgSps2Gr1
{
    class InitStartupImpl : State
    {
        public InitStartupImpl(ProgramEv3Sps2Gr1 project)
        {
            Controller = project;
        }

        protected override void PerformAction()
        {
            SetState(new NormalDriveImpl());
        }

        public override String ToString()
        {
            return "InitStartup";
        }
    }
}
