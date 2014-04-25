using System;
using PrgSps2Gr1.Control;
using PrgSps2Gr1.State.Normal;
using PrgSps2Gr1.Utility;

namespace PrgSps2Gr1.State.Error
{
    class ErrorEdgeImpl : AState
    {
        public const string Name = "ErrorEdge";

        internal ErrorEdgeImpl()
        {
            StateTimer.TickTimeout = Ev3Timer.TickTime.Medium;
        }

        protected override void PerformRecurrentAction()
        {
            if (StateTimer.IsTimeout())
            {
                EventQueue.EnqueueState(NormalSearchImpl.Name);
            }
            EventQueue.ClearCommandQueue();
        }

        protected override void PerformSingleAction()
        {
            Ev3.VehicleReverse(DeviceConstants.TurnDirection.Left, DeviceConstants.Speed.Slower, 90);
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
