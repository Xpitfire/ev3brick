using System;
using System.Threading;
using PrgSps2Gr1.State.Normal;

namespace PrgSps2Gr1.State.Init
{
    class InitImpl : AState
    {
        public const string Name = "Init";

        internal InitImpl(ProgramEv3Sps2Gr1 project)
        {
            Controller = project;
            new Thread(() => 
            {
                Thread.Sleep(1000);
                EventQueue.State.Enqueue(NormalDriveImpl.Name);
            }).Start();
        }

        protected override void PerformAction()
        {
            // initialize components
        }

        public override void Log()
        {
            throw new NotImplementedException();
        }

        public override object[] Debug(object[] args)
        {
            throw new NotImplementedException();
        }

        public override String ToString()
        {
            return Name;
        }
    }
}
