using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using PrgSps2Gr1;
using PrgSps2Gr1.Control;

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
                var prg = new ProgramEv3Sps2Gr1(true);
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
                            () => TextBlockEv3Console.Text = Ev3SimControlImpl.GetInstance().Ev3ConsoleText, DispatcherPriority.Normal);
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
            Ev3SimControlImpl.GetInstance().OnEscapeReleasedButtonEvent(null, null);
        }

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            Ev3SimControlImpl.GetInstance().OnEnterReleasedButtonEvent(null, null);
        }

        private void ButtonReachedEdgeEvent_Click(object sender, RoutedEventArgs e)
        {
            Ev3SimControlImpl.GetInstance().OnReachedEdgeEvent(null, null);
        }
                        
    }
}
