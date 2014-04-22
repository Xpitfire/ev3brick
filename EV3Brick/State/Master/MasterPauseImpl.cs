namespace PrgSps2Gr1.State.Master
{
    class MasterPauseImpl : AState
    {
        public const string Name = "MasterPause";

        protected override void PerformAction()
        {
            // do nothing
            Ev3.StopAllMovements();
        }

        public override void Log()
        {
            throw new System.NotImplementedException();
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
