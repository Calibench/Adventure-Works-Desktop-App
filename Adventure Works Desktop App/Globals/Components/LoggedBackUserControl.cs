using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.Globals.Components
{
    public partial class LoggedBackUserControl : UserControl
    {
        public string LoggedInText
        {
            get { return loggedLabel.Text; }
            set { loggedLabel.Text = value; }
        }

        public LoggedBackUserControl()
        {
            InitializeComponent();
        }

        public void ChangeDisplayName(string displayName)
        {
            loggedLabel.Text = string.Format(Properties.Resources.LoggedInAs, displayName);

        }
    }
}
