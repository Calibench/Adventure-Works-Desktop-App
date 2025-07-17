using Adventure_Works_Desktop_App.StoreDetailsPage.Backend;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Adventure_Works_Desktop_App.StoreDetailsPage.Frontend
{
    public partial class StoreDetailListForm : Form
    {
        private string username;
        private StoreDetailsBackend backend;
        private ListViewColumnSorter lvwColumnSorter;
        public bool backButtonPressed = false;

        public StoreDetailListForm(string username)
        {
            InitializeComponent();
            this.username = username;

            backend = new StoreDetailsBackend();

            lvwColumnSorter = new ListViewColumnSorter();
            this.storeListView.ListViewItemSorter = lvwColumnSorter;
        }

        private void PopulateList() 
        {
            //storeListView.View = View.Details; // needed to edit the listView

            //storeListView.Items.Clear(); // Ensures the list is cleared before adding to it
            //string[] row1 = { "Text1", "Text2", "Text3", "Text4", "Text5", "Text6", "Text7", "Text8", "Text9", "Text10", "Text11" };
            //string[] row2 = { "Scary", "Text2", "Text3", "Text4", "Text5", "Text6", "Text7", "Text8", "Text9", "Text10", "Text11" };
            //var listViewItem1 = new ListViewItem(row1); // this is how you add via columns
            //var listViewItem2 = new ListViewItem(row2); 
            //storeListView.Items.Add(listViewItem1); // adding makes a new row everytime 
            //storeListView.Items.Add(listViewItem2);

            storeListView.View = View.Details;
            storeListView.Items.Clear();
            // Need [BusinessID, Store Name, AddressLine1, AddressLine2, City, State, PostalCode, Country], [Year, Specialty, BusinessType]

            var demographicsLookup = new Dictionary<string, StoreDemographicsData>();
            foreach (var demo in backend.demographicsData)
            {
                if (!demographicsLookup.ContainsKey(demo.BusinessEntityID))
                    demographicsLookup.Add(demo.BusinessEntityID, demo);
            }

            int counter = 0;
            countTextBox.Text = $"{counter}";

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
                storeListView.Items.Add(listViewItem);

                counter++;
                countTextBox.Text = $"{counter}";
            }
        }

        private void InitialFormLoad(object sender, System.EventArgs e)
        {
            usernameLabel.Text = "Logged in: " + username;
            backend.GetAllStoreDetailList();
            PopulateList();

            // Select first index for combobox (store name)
            sortByListComboBox.SelectedIndex = 0;
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
                case 0: // ComboBox select 1 (store name)
                    return 1; // Store Name Column
                case 1:// ComboBox select 2 (city)
                    return 3; // City Column
                case 2:// ComboBox select 3 (Country)
                    return 6; // Country Column
                case 3:// ComboBox select 4 (Year)
                    return 7; // Year Column
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
                // Store Name
                case 0:
                    backend.SortedByStoreName(quickSearchTextBox.Text);
                    break;
                // City
                case 1:
                    backend.SortedByStoreCity(quickSearchTextBox.Text);
                    break;
                // Country
                case 2:
                    backend.SortedByStoreCountry(quickSearchTextBox.Text);
                    break;
                // Year
                case 3:
                    backend.SortedByStoreYear(quickSearchTextBox.Text);
                    break;
            }

            PopulateList();
        }

        private void storeListView_Click(object sender, EventArgs e)
        {
            // This is used for when a row is clicked, need to get the selected item from ListView

            ListView.SelectedListViewItemCollection selectedItems =
            this.storeListView.SelectedItems;

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

        private void backButton_Click(object sender, EventArgs e)
        {
            backButtonPressed = true;
            this.Close();
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
