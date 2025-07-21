using Adventure_Works_Desktop_App.ProductPage.Backend;
using System;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.ProductPage.Frontend
{
    public partial class LanguageProductForm : Form
    {
        public LanguageProductForm(string cultureID)
        {
            InitializeComponent();
            languageComboBox.Text = cultureID;
            ProductLanguageBackend langSelect = new ProductLanguageBackend(languageComboBox);
        }

        public string GetSelectedLanguage()
        {
            return languageComboBox.Text;
        }
    }
}
