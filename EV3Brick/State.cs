﻿using System.IO;
using MonoBrickFirmware.UserInput;

namespace PrgSps2Gr1
{
    abstract class State
    {

        private readonly ButtonEvents _buttonEvents;

        private static Ev3Control _ev3;
        protected Ev3Control Ev3
        {
            get { return _ev3 ?? (_ev3 = new Ev3Control()); }
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
            if (!this.Equals(newState))
            {
                Ev3.WriteLine("StateChanged: " + newState);
                _controller.ControllerState = newState;
            }
        }

        public void Update()
        {
            // handle general events befor starting implementation performed actions --> PerformAction()
            if (Ev3.ReachedEdge() && !(_controller.ControllerState is ErrorEdgeImpl))
            {
                SetState(new ErrorEdgeImpl());
            }
            else
            {
                PerformAction();
            }
        }

        private State _resumeState;

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
            _ev3.WriteLine("Stopping motors");
            _ev3.VehicleStop();
            _ev3.WriteLine("Exiting application...");
            _controller.Exit();
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
