using PrgSps2Gr1.Logging;

namespace PrgSps2Gr1.State.Normal
{
    class NormalIdentifyImpl : AState
    {
        public const string Name = "NormalIdentify";

        protected override void PerformRecurrentAction()
        {
            // TODO implement identify
        }

        protected override void PerformSingleAction()
        {
            // TODO implement action
            Logger.Log("Analyse object not implemented!");
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
