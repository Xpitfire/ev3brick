using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using PrgSps2Gr1;
using PrgSps2Gr1.Control.Impl;

namespace MonobrickSimulationTest
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateMonobrickInstance();
            new Thread(UpdateView).Start();
        }
        
        private void CreateMonobrickInstance()
        {
            new Thread(() =>
                {
                    ProgramEv3Sps2Gr1.IsDebug = true;
                    var prg = new ProgramEv3Sps2Gr1();
                    prg.Run();
                }).Start();
        }

        public void UpdateView()
        {
            while (ProgramEv3Sps2Gr1.IsAlive)
            {
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(
                            () => TextBlockEv3Console.Text = DeviceEv3SimControlImpl.GetInstance().Ev3ConsoleText, DispatcherPriority.Normal);
                    
                }
                else
                {
                    this.TextBlockEv3Console.Text = "<Failed to bind update dispatcher! Data Not-Available, please restart the program.>";
                }

                Thread.Sleep(100);
            }
        }

        private void ProgramClosing(object sender, CancelEventArgs e)
        {
            ProgramEv3Sps2Gr1.IsAlive = false;
            Environment.Exit(0);
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            DeviceEv3SimControlImpl.GetInstance().OnEscapeReleasedButtonEvent(null, null);
        }

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            DeviceEv3SimControlImpl.GetInstance().OnEnterReleasedButtonEvent(null, null);
        }

        private void ButtonReachedEdgeEvent_Click(object sender, RoutedEventArgs e)
        {
            DeviceEv3SimControlImpl.GetInstance().OnReachedEdgeEvent(null, null);
        }
          
    }

}
