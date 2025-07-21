using Adventure_Works_Desktop_App.ProductPage.BackEnd;
using System;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.ProductPage.FrontEnd
{
    public partial class LanguageProductForm : Form
    {
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
    }
}
