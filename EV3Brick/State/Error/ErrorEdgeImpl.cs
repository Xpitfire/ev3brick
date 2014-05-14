using System;
using SPSGrp1Grp2.Cunt.Control;
using SPSGrp1Grp2.Cunt.State.Normal;
using SPSGrp1Grp2.Cunt.Utility;

namespace SPSGrp1Grp2.Cunt.State.Error
{
    class ErrorEdgeImpl : AState
    {
        public const string Name = "ErrorEdge";
        private const int Level = EventQueue.StateLevel.Level2;

        internal ErrorEdgeImpl()
        {
            StateTimer.TickTimeout = Ev3Timer.TickTime.Medium;
        }

        protected override void PerformRecurrentAction()
        {
            if (StateTimer.IsTimeout())
            {
                StateEventQueue.EnqueueState(NormalSearchImpl.Name);
            }
            StateEventQueue.ClearCommandQueue();
        }

        protected override void PerformSingleAction()
        {
            Ev3.VehicleReverse(DeviceConstants.TurnDirection.Left, DeviceConstants.Speed.Slower, 60);
        }

        public override int GetStateLevel()
        {
            return Level;
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
