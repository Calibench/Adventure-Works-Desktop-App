using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.Globals
{
    public static class FormNavigationHelper
    {
        private static void HideAndShow(Form currentForm, Form nextForm)
        {
            currentForm.Hide();
            nextForm.Location = currentForm.Location;
            nextForm.ShowDialog();
        }

        public static void ShowFormAndCloseCurrent(Form currentForm, Form nextForm)
        {
            HideAndShow(currentForm, nextForm);
            currentForm.Close();
        }

        public static void ShowFormAndHideCurrent(Form currentForm, Form nextForm)
        {
            HideAndShow(currentForm, nextForm);
            currentForm.Show();
        }

        public static void ShowFormAndBackButton(Form currentForm, Form nextForm)
        {
            HideAndShow(currentForm, nextForm);

            if ((nextForm.DialogResult == DialogResult.Cancel)) // When closing a form it triggers a Cancel - so don't make the backbutton same
            {
                Application.Exit();
            }
            else 
            {
                currentForm.Show();
            }
        }
    }
}
