using System;
using System.Threading;
using PrgSps2Gr1.States.Normal;

namespace PrgSps2Gr1.States.Error
{
    class ErrorEdgeImpl : State
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
            Ev3.VehicleReverse(Ev3Utilities.TurnDirection.Left, 25, 90);
        }

        public override String ToString()
        {
            return Name;
        }
    }
}
