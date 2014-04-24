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
                EventQueue.EnqueueState(NormalSearchImpl.Name);
            }).Start();
        }

        protected override void PerformAction()
        {
            Ev3.VehicleDrive((sbyte) DeviceConstants.Speed.Slow);
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
