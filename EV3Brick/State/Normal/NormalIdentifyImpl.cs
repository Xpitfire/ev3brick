using Sps2Gr1.InTeam.Utility;

namespace Sps2Gr1.InTeam.State.Normal
{
    class NormalIdentifyImpl : AState
    {
        public const string Name = "NormalIdentify";
        private const int Level = EventQueue.StateLevel.Level2;

        internal NormalIdentifyImpl() {
            StateTimer.TickTimeout = Ev3Timer.TickTime.Long;
        }

        protected override void PerformRecurrentAction()
        {
            if (StateTimer.IsTimeout())
            {
                StateEventQueue.EnqueueState(NormalSearchImpl.Name);
            }
        }

        protected override void PerformSingleAction()
        {
            Ev3.StopAllMovements();
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
