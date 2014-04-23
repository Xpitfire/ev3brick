using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrgSps2Gr1.Control
{
    public class Ev3SimControlImpl : IEv3Control
    {
        private static readonly object Sync = new object();

        private static Ev3SimControlImpl _instance;

        public event Action EscapeReleasedButtonEvent;
        public event Action EnterReleasedButtonEvent;
        public event Action ReachedEdgeEvent;

        public int SpinScannerCnt { get; set; }

        private string _consoleText;
        public string Ev3ConsoleText { 
            get 
            { 
                lock (Sync) return _consoleText; 
            } 
        }

        private Ev3SimControlImpl()
        {
            _instance = this;
        }

        public static Ev3SimControlImpl GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Ev3SimControlImpl();
            }
            return _instance;
        }

        public void OnEscapeReleasedButtonEvent(object sender, EventArgs e)
        {
            EscapeReleasedButtonEvent.Invoke();
        }

        public void OnEnterReleasedButtonEvent(object sender, EventArgs e)
        {
            EnterReleasedButtonEvent.Invoke();
        }

        public void OnReachedEdgeEvent(object sender, EventArgs e)
        {
            ReachedEdgeEvent.Invoke();
        }


        public void SpinScanner(bool active)
        {
            SpinScannerCnt++;
        }

        public void StopAllMovements()
        {
            // TODO implement
        }

        public void VehicleDrive(sbyte speed)
        {
            // TODO implement
        }

        public void VehicleStop()
        {
            // TODO implement
        }

        public void VehicleReverse(Ev3Constants.TurnDirection turn, sbyte speed, sbyte turnPercent)
        {
            // TODO implement
        }

        public void WriteLine(string s)
        {
            lock (Sync)
            {
                _consoleText += "\n" + s;
            }
        }

        public void Log()
        {
            // TODO implement
        }

        public object[] Debug(object[] args)
        {
            throw new NotImplementedException();
        }

    }
}
