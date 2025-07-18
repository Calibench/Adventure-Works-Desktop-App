using Adventure_Works_Desktop_App.ProductPage.BackEnd;
using System;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.ProductPage.FrontEnd
{
    public partial class LanguageProductForm : Form
    {
        public bool backButton = false;
        public LanguageProductForm(string cultureID)
        {
            InitializeComponent();
            languageComboBox.Text = cultureID;
            ProductLanguageSelect langSelect = new ProductLanguageSelect(languageComboBox);
        }

        public string GetSelectedLanguage()
        {
            return languageComboBox.Text;
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            backButton = true;
            this.Close();
        }
    }
}
