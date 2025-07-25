using Adventure_Works_Desktop_App.ProductPage.Backend;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.ProductPage.Frontend
{
    public partial class LanguageProductForm : Form
    {
        public LanguageProductForm(string cultureID)
        {
            InitializeComponent();
            languageComboBox.Text = cultureID;
        }

        public string GetSelectedLanguage()
        {
            return languageComboBox.Text;
        }

        private void InitialFormLoad(object sender, EventArgs e)
        {
            ProductLanguageBackend langSelect = new ProductLanguageBackend();
            List<string> languages = langSelect.RetrieveLanguages();
            PopulateLanguageComboBox(languages);
        }

        private void PopulateLanguageComboBox(List<string> languages)
        {
            foreach (string lang in languages)
            {
                languageComboBox.Items.Add(lang);   
            }
        }
    }
}
