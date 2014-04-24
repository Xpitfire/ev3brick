using System;
using System.Threading;
using PrgSps2Gr1.Control;
using PrgSps2Gr1.State.Normal;
using Timer = PrgSps2Gr1.Utility.Timer;

namespace PrgSps2Gr1.State.Error
{
    class ErrorEdgeImpl : AState
    {
        public const string Name = "ErrorEdge";

        internal ErrorEdgeImpl()
        {
            Timer.TickTimeout = Timer.TickTime.Medium;
        }

        protected override void PerformRecurrentAction()
        {
            if (Timer.IsTimeout())
            {
                EventQueue.EnqueueState(NormalSearchImpl.Name);
            }
        }

        protected override void PerformSingleAction()
        {
            Ev3.VehicleReverse(DeviceConstants.TurnDirection.Left, 25, 90);
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
