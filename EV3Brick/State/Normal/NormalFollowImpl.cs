using System;
using Sps2Gr1.InTeam.Control;
using Sps2Gr1.InTeam.Utility;

namespace Sps2Gr1.InTeam.State.Normal
{
    class NormalFollowImpl : AState
    {
        public const string Name = "NormalFollow";
        private const int Level = EventQueue.StateLevel.Level3;

        internal NormalFollowImpl()
        {
            StateTimer.TickTimeout = Ev3Timer.TickTime.Medium;
        }

        protected override void PerformRecurrentAction()
        {
            if (StateTimer.IsTimeout() && Ev3.HasLostObject())
            {
                StateEventQueue.EnqueueState(NormalSearchImpl.Name);
            }
        }

        protected override void PerformSingleAction()
        {
            Ev3.VehicleDrive(DeviceConstants.Speed.Medium);
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
