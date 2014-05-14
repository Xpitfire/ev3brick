using System.Threading;
using SPSGrp1Grp2.Cunt.State.Normal;

namespace SPSGrp1Grp2.Cunt.State.Test
{
    class TestDriveImpl : AState
    {
        public const string Name = "TestDrive";
        private const int Level = EventQueue.StateLevel.Level2;

        public TestDriveImpl(StateController controller)
        {
            // set the abstract state controller instance
            Controller = controller;
            StateTimer.TickTimeout = Utility.Ev3Timer.TickTime.Long;
        }

        protected override void PerformRecurrentAction()
        {
        }

        protected override void PerformSingleAction()
        {
        }

        public override int GetStateLevel()
        {
            return Level;
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
