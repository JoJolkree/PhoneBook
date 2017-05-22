using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PhoneBook.Model;
using PhoneBook.Presenter;

namespace PhoneBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private IPresenter Presenter { get; }

        private int? _editingId;

        public MainWindow()
        {
            Presenter = new Presenter.Presenter(new Model.Model(new PersonContext()));

            InitializeComponent();
            Loaded += OnLoaded;
            Closed += OnClosed;
        }

        private void OnClosed(object sender, EventArgs eventArgs)
        {
            Presenter?.Dispose();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            UpdateInfoInDataGrid();
            AddPersonButton.IsDefault = true;
        }

        private void DeletePersonButton_OnClick(object sender, RoutedEventArgs e)
        {
            Presenter.DeletePerson(int.Parse(((Button) sender).Tag.ToString()));
            UpdateInfoInDataGrid();
        }

        private void AddPersonButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_editingId != null)
            {
                Presenter.UpdatePerson(_editingId.Value, new Person(LastNameTextBox.Text, FirstNameTextBox.Text, PhoneNumberTextBox.Text, EmailTextBox.Text));
                LastNameTextBox.Text = "";
                FirstNameTextBox.Text = "";
                PhoneNumberTextBox.Text = "";
                EmailTextBox.Text = "";

                AddPersonButton.Content = "Add";
                _editingId = null;
                UpdateInfoInDataGrid();
                return;
            }

            if (EmailTextBox.Text != "" && !EmailTextBox.Text.Contains("@"))
            {
                ShowErrorMessage("Email has invalid format");
                return;
            }
            var person = new Person(LastNameTextBox.Text, FirstNameTextBox.Text, PhoneNumberTextBox.Text,
                EmailTextBox.Text);
            Presenter.AddPerson(person);

            UpdateInfoInDataGrid();
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void UpdateInfoInDataGrid(string query = "")
        {
            PersonsDataGrid.ItemsSource = query == "" ? Presenter.GetListOfPersons() : Presenter.GetListOfPersons(query);
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            var query = SearchTextBox.Text;
            UpdateInfoInDataGrid(query);
        }

        private void EditPersonButton_OnClick(object sender, RoutedEventArgs e)
        {
            var id = int.Parse(((Button)sender).Tag.ToString());
            var person = Presenter.GetPersonFromId(id);
            if (person == null) return;
            LastNameTextBox.Text = person.LastName;
            FirstNameTextBox.Text = person.FirstName;
            PhoneNumberTextBox.Text = person.PhoneNumber;
            EmailTextBox.Text = person.Email;

            AddPersonButton.Content = "Save";
            CancelButton.IsEnabled = true;
            _editingId = id;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            LastNameTextBox.Text = "";
            FirstNameTextBox.Text = "";
            PhoneNumberTextBox.Text = "";
            EmailTextBox.Text = "";
            CancelButton.IsEnabled = false;
            AddPersonButton.Content = "Add";
            _editingId = null;
        }

        private void PhoneNumberTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9()\\-+\\s]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
