using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using SPSGrp1Grp2.Cunt;
using SPSGrp1Grp2.Cunt.Control.Impl;

namespace SpsGr1.InTeam
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
                    StateController.IsDebug = true;
                    var prg = new StateController("Init");
                    prg.Run();
                }).Start();
        }

        public void UpdateView()
        {
            while (StateController.IsAlive)
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
            StateController.IsAlive = false;
            Environment.Exit(0);
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            DeviceEv3SimControlImpl.GetInstance().OnEscapeReleasedButtonEvent(sender, e);
        }

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            DeviceEv3SimControlImpl.GetInstance().OnEnterReleasedButtonEvent(sender, e);
        }

        private void ButtonReachedEdgeEvent_Click(object sender, RoutedEventArgs e)
        {
            DeviceEv3SimControlImpl.GetInstance().OnReachedEdgeEvent(sender, e);
        }

        private void ButtonObjectDetected_Click(object sender, RoutedEventArgs e)
        {
            DeviceEv3SimControlImpl.GetInstance().OnDetectedObjectEvent(sender, e);
        }

        private void ButtonIdentifiedEnemy_Click(object sender, RoutedEventArgs e)
        {
            DeviceEv3SimControlImpl.GetInstance().OnIdentifiredEnemyEvent(sender, e);
        }

        private void ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            DeviceEv3SimControlImpl.GetInstance().OnUpReleasedButtonEvent(sender, e);
        }
          
    }

}
