using System;
using Adventure_Works_Desktop_App.SalesPersonPage.Backend;
using System.Windows.Forms;
using System.Diagnostics;

namespace Adventure_Works_Desktop_App.SalesPersonPage.Frontend
{
    public partial class ChangeSalesPersonByIDForm : Form
    {
        private string selectedID;
        public string SelectedID
        {
            get { return selectedID; }
        }

        private SalesPersonBackend backend = new SalesPersonBackend();

        public ChangeSalesPersonByIDForm(string formName, string title)
        {
            InitializeComponent();
            this.Text = formName;
            enterIDLabel.Text = title;
        }

        private void InitialFormLoad(object sender, EventArgs e)
        {
            enterIDLabel.Left = (panel1.Width / 2) - (enterIDLabel.Width / 2);
            enterIDLabel.Top = (panel1.Height / 2) - (enterIDLabel.Height / 2);
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            SubmitCheck(idTextBox.Text);
        }

        private void SubmitCheck(string id)
        {
            if (this.Text == "Name")
            {
                if (backend.ValidateName(id))
                {
                    this.selectedID = backend.Id;
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Could not find Sales Person\nPlease try again.");
                }
            }
            else
            {
                if (backend.ValidateID(id))
                {
                    this.selectedID = id;
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Could not find find ID\nPlease try again.");
                }
            }
        }
    }
}
