using System;
using System.Threading;

namespace PrgSps2Gr1.States.Normal
{
    class NormalDriveImpl : State
    {
        public const string Name = "NormalDrive";

        internal NormalDriveImpl()
        {
            new Thread(() =>
            {
                Thread.Sleep(5000);
                EventQueue.State.Enqueue(NormalSearchImpl.Name);
            }).Start();
        }

        protected override void PerformAction()
        {
            Ev3.VehicleDrive((sbyte) Ev3Utilities.Speed.Slow);
        }

        public override String ToString()
        {
            return Name;
        }

    }
}
