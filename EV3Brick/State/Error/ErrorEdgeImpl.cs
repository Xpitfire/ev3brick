using System;
using System.Threading;
using PrgSps2Gr1.Control;
using PrgSps2Gr1.State.Normal;

namespace PrgSps2Gr1.State.Error
{
    class ErrorEdgeImpl : AState
    {
        public const string Name = "ErrorEdge";

        internal ErrorEdgeImpl()
        {
            new Thread(() =>
            {
                Thread.Sleep(1000);
                EventQueue.State.Enqueue(NormalDriveImpl.Name);
            }).Start();
        }

        protected override void PerformAction()
        {
            Ev3.VehicleReverse(Ev3Constants.TurnDirection.Left, 25, 90);
        }

        public override void Log()
        {
            throw new NotImplementedException();
        }

        public override object[] Debug(object[] args)
        {
            throw new NotImplementedException();
        }

        public override String ToString()
        {
            return Name;
        }
    }
}
