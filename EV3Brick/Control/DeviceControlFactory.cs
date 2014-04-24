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

        /// <summary>
        /// Returns an IDeviceControl instance depending on the debug flag
        /// in the <code>ProgramEv3Sps2Gr1</code> class.
        /// It can variate between an simulation instance and the real-time
        /// Ev3 Firmware control implementation.
        /// </summary>
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
