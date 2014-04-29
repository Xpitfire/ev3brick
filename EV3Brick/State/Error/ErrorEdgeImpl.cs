using System;
using Sps2Gr1.InTeam.Control;
using Sps2Gr1.InTeam.State.Normal;
using Sps2Gr1.InTeam.Utility;

namespace Sps2Gr1.InTeam.State.Error
{
    class ErrorEdgeImpl : AState
    {
        public const string Name = "ErrorEdge";
        private const EventQueue.StateLevel Level = EventQueue.StateLevel.Level2;

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
            Ev3.VehicleReverse(DeviceConstants.TurnDirection.Left, DeviceConstants.Speed.Slower, 90);
        }

        public override EventQueue.StateLevel GetStateLevel()
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
