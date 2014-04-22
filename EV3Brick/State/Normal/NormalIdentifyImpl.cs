namespace PrgSps2Gr1.State.Normal
{
    class NormalIdentifyImpl : AState
    {
        public const string Name = "NormalIdentify";

        protected override void PerformAction()
        {
            Ev3.WriteLine("analyse object...");
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
