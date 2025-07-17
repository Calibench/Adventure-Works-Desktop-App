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

        public static void ShowFormAndHideCurren(Form currentForm, Form nextForm)
        {
            HideAndShow(currentForm, nextForm);
            currentForm.Show();
        }
    }
}
