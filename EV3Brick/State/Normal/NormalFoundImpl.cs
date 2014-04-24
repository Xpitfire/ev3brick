namespace PrgSps2Gr1.State.Normal
{
    class NormalFoundImpl : AState
    {
        public const string Name = "NormalFound";

        protected override void PerformRecurrentAction()
        {
            // TODO implement found handling
        }

        protected override void PerformSingleAction()
        {
            // TODO implement action
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
