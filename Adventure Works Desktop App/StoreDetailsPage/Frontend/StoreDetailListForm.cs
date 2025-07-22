using Adventure_Works_Desktop_App.Globals.DataClasses;
using Adventure_Works_Desktop_App.StoreDetailsPage.Backend;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.StoreDetailsPage.Frontend
{
    public partial class StoreDetailListForm : Form
    {
        private string username;
        private StoreDetailsBackend backend;
        private ListViewColumnSorter lvwColumnSorter;

        private enum ColumnName
        {
            StoreName = 1,
            City = 3,
            Country = 6,
            Year = 7
        }

        private enum ComboBoxChoice
        {
            StoreName,
            City,
            Country,
            Year
        }

        public StoreDetailListForm(string username)
        {
            InitializeComponent();
            this.username = username;

            backend = new StoreDetailsBackend();

            lvwColumnSorter = new ListViewColumnSorter();
            this.storeListView.ListViewItemSorter = lvwColumnSorter;
        }

        private void InitialFormLoad(object sender, EventArgs e)
        {
            usernameLabel.Text = "Logged in: " + username;
            backend.GetAllStoreDetailList();
            PopulateList();

            // Select first index for combobox (store name)
            sortByListComboBox.SelectedIndex = 0;
        }

        private void PopulateList()
        {
            storeListView.View = View.Details; // needed to access and edit the list view
            storeListView.Items.Clear(); // ensure that it is cleaned before adding data
            // Need [BusinessID, Store Name, AddressLine1, AddressLine2, City, State, PostalCode, Country], [Year, Specialty, BusinessType]

            var demographicsLookup = new Dictionary<string, StoreDemographicsData>();
            foreach (var demo in backend.demographicsData)
            {
                if (!demographicsLookup.ContainsKey(demo.BusinessEntityID))
                    demographicsLookup.Add(demo.BusinessEntityID, demo);
            }

            int counter = 0;
            countTextBox.Text = counter.ToString();

            foreach (StoreAddressData data in backend.addressData)
            {
                if (data.AddressType.Equals("Shipping"))
                    continue;

                // Try to get matching demographics
                if (!demographicsLookup.TryGetValue(data.BusinessEntityID, out var demo))
                    continue;

                string[] row = {
                    data.BusinessEntityID,
                    data.StoreName,
                    data.AddressLine1,
                    data.City,
                    data.StateProvinceName,
                    data.PostalCode,
                    data.Country,
                    demo.YearOpened,
                    demo.Specialty,
                    demo.BusinessType
                };
                var listViewItem = new ListViewItem(row);
                storeListView.Items.Add(listViewItem); // creates new rows

                counter++;
                countTextBox.Text = counter.ToString();
            }
        }

        private void storeListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // dont want to sort the ID's column
            if (e.Column.ToString().Equals("0"))
            {
                return;
            }

            SortListByComboBox(Convert.ToInt32(e.Column.ToString()));
        }

        private void SortListByComboBox(int choiceIndex)
        {
            // From https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/sort-listview-by-column
            if (choiceIndex == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = choiceIndex;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.storeListView.Sort();
        }

        private int GetChoiceIndexToColumn(int choice)
        {
            switch (choice)
            {
                case (int)ComboBoxChoice.StoreName:
                    return (int)ColumnName.StoreName;
                case (int)ComboBoxChoice.City:
                    return (int)ColumnName.City;
                case (int)ComboBoxChoice.Country:
                    return (int)ColumnName.Country;
                case (int)ComboBoxChoice.Year:
                    return (int)ColumnName.Year;
                default:
                    throw new ArgumentOutOfRangeException("choice error: " + choice);
            }
        }

        private void sortByListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            quickSearchTextBox.Text = "";
            WrapperComboBoxSelect();
        }

        private void WrapperComboBoxSelect()
        {
            int column = GetChoiceIndexToColumn(sortByListComboBox.SelectedIndex);
            SortListByComboBox(column);
        }

        private void quickSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateQuickSearch();
        }

        private void UpdateQuickSearch()
        {
            // clear obj
            backend = new StoreDetailsBackend();

            if (quickSearchTextBox.Text == "")
            {
                backend.GetAllStoreDetailList();
                PopulateList();
                return;
            }

            switch (sortByListComboBox.SelectedIndex)
            {
                case (int)ComboBoxChoice.StoreName:
                    backend.SortedByStoreName(quickSearchTextBox.Text);
                    break;
                case (int)ComboBoxChoice.City:
                    backend.SortedByStoreCity(quickSearchTextBox.Text);
                    break;
                case (int)ComboBoxChoice.Country:
                    backend.SortedByStoreCountry(quickSearchTextBox.Text);
                    break;
                case (int)ComboBoxChoice.Year:
                    backend.SortedByStoreYear(quickSearchTextBox.Text);
                    break;
            }

            PopulateList();
        }

        private void storeListView_Click(object sender, EventArgs e)
        {
            // This is used for when a row is clicked, need to get the selected item from ListView

            ListView.SelectedListViewItemCollection selectedItems = this.storeListView.SelectedItems;

            foreach (ListViewItem item in selectedItems)
            {
                //MessageBox.Show($"ID: {item.SubItems[0].Text}");
                // This passes through the item that is selected to the details window
                // also passes through an Editor bool to make sure it cannot edit data
                bool editor = false;
                var frm = new StoreDetailsForm(username, item.SubItems[0].Text, editor);
                frm.ShowDialog();
            }
        }

        private void ContextWindowMouseDown(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo lstHitTestInfo = storeListView.HitTest(e.X, e.Y);
            if (e.Button == MouseButtons.Right)
            {
                if (lstHitTestInfo.Item != null)
                {
                    storeListView.ContextMenuStrip = listViewContextMenuStrip;
                }
            }
        }

        private void editStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItems =
            this.storeListView.SelectedItems;

            foreach (ListViewItem item in selectedItems)
            {
                // This passes through the item that is selected to the details window
                // also passes through an Editor bool to make sure it can edit data
                bool editor = true;
                var frm = new StoreDetailsForm(username, item.SubItems[0].Text, editor);
                frm.ShowDialog();

                if (frm.saveDataPressed)
                {
                    backend = new StoreDetailsBackend(); // clear old data
                    backend.GetAllStoreDetailList(); // get new data
                    PopulateList(); // display data
                }
            }
        }
    }
}
