using SPSGrp1Grp2.Cunt.Control.Impl;

namespace SPSGrp1Grp2.Cunt.Control
{
    public class DeviceControlFactory
    {
        private static IDeviceControl _ev3;

        private DeviceControlFactory() { }

        /// <summary>
        /// Returns an IDeviceControl instance depending on the debug flag
        /// in the <code>StateController</code> class.
        /// It can variate between an simulation instance and the real-time
        /// Ev3 Firmware control implementation.
        /// </summary>
        public static IDeviceControl Ev3Control
        {
            get
            {
                if (StateController.IsDebug)
                    return _ev3 ?? (_ev3 = DeviceEv3SimControlImpl.GetInstance());
                else
                    return _ev3 ?? (_ev3 = new DeviceControlImpl());
            }
        }

    }
}
