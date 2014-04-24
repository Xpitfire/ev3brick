using System.Threading;
using Timer = PrgSps2Gr1.Utility.Timer;

namespace PrgSps2Gr1.State.Normal
{
    class NormalSearchImpl : AState
    {
        public const string Name = "NormalSearch";

        internal NormalSearchImpl()
        {
            Timer.TickTimeout = Timer.TickTime.Short;
        }

        protected override void PerformRecurrentAction()
        {
            // TODO implements search procedure...
            if (Timer.IsTimeout())
            {
                EventQueue.EnqueueState(NormalDriveImpl.Name);
            }
        }

        protected override void PerformSingleAction()
        {
            Ev3.StopAllMovements();
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
