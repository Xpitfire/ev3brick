namespace Sps2Gr1.InTeam.State.Master
{
    class MasterPauseImpl : AState
    {
        public const string Name = "MasterPause";
        private const int Level = EventQueue.StateLevel.Level1;
        private static bool _inPause = false;

        protected override void PerformRecurrentAction()
        {
            // nothing must be called
        }

        protected override void PerformSingleAction()
        {
            Ev3.StopAllMovements();

            // Handles how to interpret pause state interactions and goes to a dedicated
            // state (Pause or Resume previous state).
            if (_inPause == false)
            {
                _inPause = true;
            }
            else
            {
                _inPause = false;
                StateEventQueue.EnqueueState(StateEventQueue.LastState);
            }
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
