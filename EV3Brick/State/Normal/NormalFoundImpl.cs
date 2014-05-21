using SPSGrp1Grp2.Cunt.Logging;
using SPSGrp1Grp2.Cunt.State;
using System.Threading;
using SPSGrp1Grp2.Cunt.Utility;

namespace SPSGrp1Grp2.Cunt.State.Normal
{
    class NormalFoundImpl : AState
    {
        public const string Name = "NormalFound";
        private const int Level = EventQueue.StateLevel.Level2;

        internal NormalFoundImpl() 
        {
            StateTimer.TickTimeout = Ev3Timer.TickTime.Medium;
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
            Logger.Log("Found the enemy!");
            Ev3.PlaySound(3000, 300, 2000);
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
