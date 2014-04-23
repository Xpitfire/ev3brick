using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrgSps2Gr1.Control
{
    public partial class SimControlGui : Form
    {
        public SimControlGui()
        {
            InitializeComponent();
            Visible = true;
        }

        
        private void buttonEscapeReleased_Click(object sender, EventArgs e)
        {
            //EscapeReleasedButtonEvent.Invoke();
        }

        private void buttonEnterReleased_Click(object sender, EventArgs e)
        {
            //EnterReleasedButtonEvent.Invoke();
        }

        private void buttonReachedEdge_Click(object sender, EventArgs e)
        {
            //ReachedEdgeEvent.Invoke();
        }
        
    }
}
