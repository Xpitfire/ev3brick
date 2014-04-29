using Sps2Gr1.InTeam.Logging;
using Sps2Gr1.InTeam.State;

namespace Sps2Gr1.InTeam.State.Normal
{
    class NormalFoundImpl : AState
    {
        public const string Name = "NormalFound";
        private const EventQueue.StateLevel Level = EventQueue.StateLevel.Level2;

        protected override void PerformRecurrentAction()
        {
            // do nothing
        }

        protected override void PerformSingleAction()
        {
            Logger.Log("Found the enemy!");
        }

        public override EventQueue.StateLevel GetStateLevel()
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
