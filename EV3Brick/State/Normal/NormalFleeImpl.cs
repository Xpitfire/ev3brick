using System;
using SPSGrp1Grp2.Cunt.Control;
using SPSGrp1Grp2.Cunt.Utility;

namespace SPSGrp1Grp2.Cunt.State.Normal
{
    class NormalFleeImpl : AState
    {
        public const string Name = "NormalFlee";
        private const int Level = EventQueue.StateLevel.Level3;

        internal NormalFleeImpl()
        {
            StateTimer.TickTimeout = Ev3Timer.TickTime.Medium;
        }

        protected override void PerformRecurrentAction()
        {
            if (StateTimer.IsTimeout())
            {
                // TODO drive in different ways
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
