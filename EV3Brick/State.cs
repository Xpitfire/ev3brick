using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
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

        /// <summary>
        /// State constructor to inistialize the base class instances.
        /// </summary>
        protected State()
        {
            _buttonEvents = new ButtonEvents();
            // add ananymouse method action to event queue
            _buttonEvents.EscapeReleased += () => EventQueue.Primary.Enqueue(Exit);
            _buttonEvents.EnterReleased += () => EventQueue.Primary.Enqueue(PauseOrResume);
            // start the general sensor monitoring thread
            var thread = new Thread(new ThreadStart(WorkThreadFunction));
            thread.Start();
        }

        /// <summary>
        /// Mothod to override from the implementing sub-classes.
        /// </summary>
        protected abstract void PerformAction();

        /// <summary>
        /// Sets a new state instance in the controller.
        /// </summary>
        /// <param name="newState"></param>
        protected void SetState(State newState)
        {
            if (Equals(newState)) return;
			Ev3.WriteLine("StateChanged: " + newState);
            _controller.ControllerState = newState;
        }

        /// <summary>
        /// Update sequence called by the controller.
        /// </summary>
        public void Update()
        {
			// dequeue events from a primary event queue until it's empty and then check the secondary
			if (EventQueue.Primary.Count > 0)
			{
				EventQueue.Primary.Dequeue()();
			}
            else if (EventQueue.Secondary.Count > 0)
            {
                EventQueue.Secondary.Dequeue()();
            }

            // spins sensor to detect environment
            Ev3.SpinScanner(true);
            // handle general events befor starting implementation performed actions --> PerformAction()
            if (Ev3.ObjectDetected(15) && !(_controller.ControllerState is NormalObjectDetectedImpl))
            {
                SetState(new NormalObjectDetectedImpl());
            }
            else
            {
                PerformAction();
            }
           
        }
        
        /// <summary>
        /// Set the controller state to a pause sequence.
        /// TODO Has a resume bug!
        /// </summary>
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

        private void GotoErrorEdge()
        {
            SetState(new ErrorEdgeImpl());
        }

        /// <summary>
        /// Sets the controller state to exit the program.
        /// </summary>
        public void Exit()
        {
            SetState(new ExitProgramImpl());
        }

        // ----- special event update thread for state behavior -----

        /// <summary>
        /// Update thread to wrap the polling of sensor behaviors to a event base 
        /// system.
        /// </summary>
        public void WorkThreadFunction()
        {
            while (_controller.IsAlive)
            {
                try
                {
                    if (Ev3.ReachedEdge() && !(_controller.ControllerState is ErrorEdgeImpl))
                    {
                        EventQueue.Secondary.Enqueue(GotoErrorEdge);
                    }
                }
                catch (Exception)
                {
                    Ev3.WriteLine("Sensor-Update-Thread died!");
                    // continue process
                }
            }
        }

        // ----- default class implementation methods -----

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
