using System.IO;
using MonoBrickFirmware.UserInput;

namespace PrgSps2Gr1
{
    abstract class State
    {

        private readonly ButtonEvents _buttonEvents;
        private static Ev3Utilities _ev3;
        private State _resumeState;

        protected int DetectedObjectDegree
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }


        protected Ev3Utilities Ev3
        {
            get { return _ev3 ?? (_ev3 = new Ev3Utilities()); }
        }

        private static ProgramEv3Sps2Gr1 _controller;
        protected ProgramEv3Sps2Gr1 Controller
        {
            set
            {
                if (value == null) throw new InvalidDataException("Controller == null!");
                Ev3.WriteLine("Set new controller!");
                _controller = value;
            }
            get { return _controller; }
        }

        protected State()
        {
            _buttonEvents = new ButtonEvents();
            _buttonEvents.EscapeReleased += Exit;
            _buttonEvents.EnterReleased += PauseOrResume;
        }

        protected abstract void PerformAction();

        protected void SetState(State newState)
        {
            if (Equals(newState)) return;
            Ev3.WriteLine("StateChanged: " + newState);
            _controller.ControllerState = newState;
        }

        public void Update()
        {
            // spins sensor to detect environment
            Ev3.SpinScanner(true);
            // handle general events befor starting implementation performed actions --> PerformAction()
            if (Ev3.ReachedEdge() && !(_controller.ControllerState is ErrorEdgeImpl))
            {
                SetState(new ErrorEdgeImpl());
            }
            else if (Ev3.ObjectDetected(100) && !(_controller.ControllerState is NormalObjectDetectedImpl))
            {
                SetState(new NormalObjectDetectedImpl());
            }
            else
            {
                PerformAction();
            }
        }

        public void PauseOrResume()
        {
            if (_controller.ControllerState is NormalPauseImpl)
            {
                SetState(_resumeState);
            }
            else
            {
                _resumeState = _controller.ControllerState;
                SetState(new NormalPauseImpl());
            }
        }

        public void Exit()
        {
            SetState(new ExitProgramImpl());
        }

        public override bool Equals(object other)
        {
            return other != null && Equals(ToString(), other.ToString());
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode() + _buttonEvents.GetHashCode();
        }
    }
}
