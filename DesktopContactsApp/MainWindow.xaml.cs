using DesktopContactsApp.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Contact> contacts;
        public MainWindow()
        {
            InitializeComponent();
            contacts = new List<Contact>();
            ReadDatabase();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow();
            //newContactWindow.Show(); Multiple windows possible here.

            newContactWindow.ShowDialog(); // Only single windo is possible.
            ReadDatabase(); // After closing dialog, read the db again.
        }

        void ReadDatabase()
        {
            using (SQLite.SQLiteConnection conn = new SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<Contact>();
                contacts = conn.Table<Contact>().OrderBy(x=>x.Name).ToList();
            }

            if (contacts != null)
            {
                //foreach (var contact in contacts)
                //{
                //    contactListView.Items.Add(new ListViewItem()
                //    {
                //        Content = contact
                //    });
                //}

                contactListView.ItemsSource = contacts;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchTextBox = sender as TextBox;
            var filteredList = contacts.Where(x=>x.Name.ToLower().Contains(searchTextBox.Text.ToLower())).OrderBy(x=>x.Name).ToList();
            contactListView.ItemsSource = filteredList;
        }

        private void contactListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var contact =  contactListView.SelectedItem as Contact;

            /*We need to check null here as when we update any list member, then 
            SelectionChanged event occurs but SelectedItem is null in this case.
            */
            if (contact != null)
            {
                ContactDetailsWindow contactDetailsWindow = new ContactDetailsWindow(contact);
                contactDetailsWindow.ShowDialog();
                ReadDatabase();
            }
            
        }
    }
}
