using PrgSps2Gr1.Control;

namespace PrgSps2Gr1.Logging
{
    public class Logger
    {
        private static readonly IDeviceControl Control = DeviceControlFactory.Ev3Control;

        public static void Log(object o)
        {
            Control.WriteLine(o.ToString());
        }
        
    }
}
