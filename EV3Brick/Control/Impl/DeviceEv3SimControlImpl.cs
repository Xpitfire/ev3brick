﻿using System;
using PrgSps2Gr1.Logging;

namespace PrgSps2Gr1.Control.Impl
{
    public class DeviceEv3SimControlImpl : IDeviceControl
    {
        private static readonly object Sync = new object();

        private static DeviceEv3SimControlImpl _instance;
        private string _consoleText;

        public string Ev3ConsoleText
        {
            get { lock (Sync) return _consoleText; }
        }

        #region Ev3 Simulation Events

        public event Action EscapeReleasedButtonEvent;
        public event Action EnterReleasedButtonEvent;
        public event Action ReachedEdgeEvent;

        public void OnEscapeReleasedButtonEvent(object sender, EventArgs e)
        {
            var handler = EscapeReleasedButtonEvent;
            if (handler != null) handler();
        }

        public void OnEnterReleasedButtonEvent(object sender, EventArgs e)
        {
            var handler = EnterReleasedButtonEvent;
            if (handler != null) handler();
        }

        public void OnReachedEdgeEvent(object sender, EventArgs e)
        {
            var handler = ReachedEdgeEvent;
            if (handler != null) handler();
        }

        #endregion

        /// <summary>
        /// Private constructor to create a sigleton for external
        /// simulation tests.
        /// </summary>
        private DeviceEv3SimControlImpl()
        {
            _instance = this;
        }

        public static DeviceEv3SimControlImpl GetInstance()
        {
            return _instance ?? (_instance = new DeviceEv3SimControlImpl());
        }

        #region Ev3 Device simulated implementaiton

        public void SpinScanner(bool active)
        {
            Logger.Log("SpinScanner action: active = " + active);
        }

        public void StopAllMovements()
        {
            Logger.Log("StopAllMovements action");
        }

        public void VehicleDrive(sbyte speed)
        {
            Logger.Log("VehicleDrive action: speed = " + speed);
        }

        public void VehicleStop()
        {
            Logger.Log("VehicleStop action");
        }

        public void VehicleReverse(DeviceConstants.TurnDirection turn, sbyte speed, sbyte turnPercent)
        {
            Logger.Log("VehicleReverse action: turn = " + turn + ", speed = " + speed + ", turnPercent = " + turnPercent);
        }

        public void WriteLine(string s)
        {
            lock (Sync)
            {
                _consoleText += "\n" + s;
            }
        }

        #endregion

        public object[] Debug(object[] args)
        {
            throw new NotImplementedException();
        }

    }
}