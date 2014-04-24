namespace PrgSps2Gr1.State.Normal
{
    class NormalAdjustImpl : AState
    {
        public const string Name = "NormalAdjust";

        protected override void PerformAction()
        {
            // TODO implement search adjustment
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
