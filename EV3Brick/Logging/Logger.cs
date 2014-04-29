using Sps2Gr1.InTeam.Control;

namespace Sps2Gr1.InTeam.Logging
{
    public class Logger
    {
        private static readonly IDeviceControl Control = DeviceControlFactory.Ev3Control;

        /// <summary>
        /// Default project logger, which will log to the default IControl API
        /// instance. This means in real-time execution on the Ev3 device it will
        /// log to the Ev3 Display and in Simulation instance it can log to
        /// a new declared output stream.
        /// </summary>
        /// <param name="o"></param>
        public static void Log(object o)
        {
            Control.WriteLine(o.ToString());
        }
        
    }
}
