using System.Threading;

namespace PrgSps2Gr1.States.Normal
{
    class NormalSearchImpl : State
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

        public override string ToString()
        {
            return Name;
        }
    }
}
