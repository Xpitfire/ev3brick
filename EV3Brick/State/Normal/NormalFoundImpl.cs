using Sps2Gr1.InTeam.Logging;
using Sps2Gr1.InTeam.State;

namespace Sps2Gr1.InTeam.State.Normal
{
    class NormalFoundImpl : AState
    {
        public const string Name = "NormalFound";

        protected override void PerformRecurrentAction()
        {
            // do nothing
        }

        protected override void PerformSingleAction()
        {
            Logger.Log("Found the enemy!");
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
