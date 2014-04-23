using System;
using System.Threading;
using PrgSps2Gr1.Control;

namespace PrgSps2Gr1.State.Normal
{
    class NormalDriveImpl : AState
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
            Ev3.VehicleDrive((sbyte) Ev3Constants.Speed.Slow);
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
