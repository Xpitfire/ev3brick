using System.Threading;

namespace PrgSps2Gr1.State.Normal
{
    class NormalSearchImpl : AState
    {
        public const string Name = "NormalSearch";

        internal NormalSearchImpl()
        {
            new Thread(() =>
            {
                Thread.Sleep(3000);
                EventQueue.State.Enqueue(NormalDriveImpl.Name);
            }).Start();
        }

        protected override void PerformAction()
        {
            // TODO implements search procedure...
            Ev3.StopAllMovements();
        }

        public override void Log()
        {
            throw new System.NotImplementedException();
        }

        public override object[] Debug(object[] args)
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
