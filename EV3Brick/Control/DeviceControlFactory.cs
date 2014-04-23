using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrgSps2Gr1.Control.Impl;

namespace PrgSps2Gr1.Control
{
    public class DeviceControlFactory
    {
        private static IDeviceControl _ev3;

        private DeviceControlFactory() { }

        public static IDeviceControl Ev3Control
        {
            get
            {
                if (ProgramEv3Sps2Gr1.IsDebug)
                    return _ev3 ?? (_ev3 = DeviceEv3SimControlImpl.GetInstance());
                else
                    return _ev3 ?? (_ev3 = new DeviceControlImpl());
            }
        }

    }
}
