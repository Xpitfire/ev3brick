namespace PrgSps2Gr1.State.Normal
{
    class NormalFoundImpl : AState
    {
        public const string Name = "NormalFound";

        protected override void PerformAction()
        {
            // TODO implement found handling
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
