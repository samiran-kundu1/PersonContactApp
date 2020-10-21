using PersonContactApp.Helper_Class;
using PersonContactApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using System.Net.Http;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Navigation;
using PersonContactApp.View;
using PersonContactApp.Shared;

namespace PersonContactApp.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="PersonContactApp.Helper_Class.Updater" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class PersonContactViewModel : Updater
    {
        #region Private Members

        /// <summary>
        /// The person contacts
        /// </summary>
        private ObservableCollection<PersonContact> personContacts;

        /// <summary>
        /// The emails
        /// </summary>
        private ObservableCollection<Emails> emails;
        /// <summary>
        /// The phone list
        /// </summary>
        private ObservableCollection<PhoneNumbers> phoneList;

        /// <summary>
        /// The person contact details
        /// </summary>
        private PersonContact personContactDetails;

        /// <summary>
        /// The selected person contact
        /// </summary>
        private PersonContact selectedPersonContact;

        /// <summary>
        /// The selected email
        /// </summary>
        private Emails selectedEmail;

        /// <summary>
        /// The selected phone
        /// </summary>
        private PhoneNumbers selectedPhone;

        /// <summary>
        /// The is grid enabled
        /// </summary>
        private bool isGridEnabled = true;

        /// <summary>
        /// The is email or phone button enabled
        /// </summary>
        private bool isEmailOrPhoneButtonEnabled;

        /// <summary>
        /// The HTTP client
        /// </summary>
        static HttpClient httpClient = new HttpClient();

        /// <summary>
        /// The email view model
        /// </summary>
        private EmailDetailsViewModel emailViewModel;

        /// <summary>
        /// The phone number view model
        /// </summary>
        private PhoneNumberViewModel phoneNumberViewModel;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonContactViewModel"/> class.
        /// </summary>
        public PersonContactViewModel()
        {
            GetContactsList();
            emailViewModel = new EmailDetailsViewModel();
            phoneNumberViewModel = new PhoneNumberViewModel();
            AddCommand = new DelegateCommand(OnAdd);
            UpdateCommand = new DelegateCommand(OnUpdate);
            AddEmailCommand = new DelegateCommand(AddEmail);
            UpdateEmailCommand = new DelegateCommand(UpdateEmail);
            AddPhoneNumberCommand = new DelegateCommand(AddPhoneNumber);
            UpdatePhoneNumberCommand = new DelegateCommand(UpdatePhoneNumber);
            OnSaveCommand = new DelegateCommand(OnSave);
            OnCancelCommand = new DelegateCommand(OnCancel);


        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the person contacts.
        /// </summary>
        /// <value>
        /// The person contacts.
        /// </value>
        public ObservableCollection<PersonContact> PersonContacts
        {
            get { return personContacts; }
            set
            {

                personContacts = value;
                OnPropertyChanged(nameof(PersonContacts));
            }
        }

        /// <summary>
        /// Gets or sets the email list.
        /// </summary>
        /// <value>
        /// The email list.
        /// </value>
        public ObservableCollection<Emails> EmailList
        {
            get { return emails; }
            set
            {

                emails = value;
                OnPropertyChanged(nameof(EmailList));
            }
        }

        /// <summary>
        /// Gets or sets the phone list.
        /// </summary>
        /// <value>
        /// The phone list.
        /// </value>
        public ObservableCollection<PhoneNumbers> PhoneList
        {
            get { return phoneList; }
            set
            {

                phoneList = value;
                OnPropertyChanged(nameof(PhoneList));
            }
        }

        /// <summary>
        /// Gets or sets the selected email.
        /// </summary>
        /// <value>
        /// The selected email.
        /// </value>
        public Emails SelectedEmail
        {
            get { return selectedEmail; }
            set
            {

                selectedEmail = value;
                OnPropertyChanged(nameof(SelectedEmail));
            }
        }

        /// <summary>
        /// Gets or sets the selected person contact.
        /// </summary>
        /// <value>
        /// The selected person contact.
        /// </value>
        public PersonContact SelectedPersonContact
        {
            get { return selectedPersonContact; }
            set
            {

                selectedPersonContact = value;
                OnPropertyChanged(nameof(SelectedPersonContact));
            }
        }

        /// <summary>
        /// Gets or sets the selected phone.
        /// </summary>
        /// <value>
        /// The selected phone.
        /// </value>
        public PhoneNumbers SelectedPhone
        {
            get { return selectedPhone; }
            set
            {

                selectedPhone = value;
                OnPropertyChanged(nameof(SelectedPhone));
            }
        }

        /// <summary>
        /// Gets or sets the person contact details.
        /// </summary>
        /// <value>
        /// The person contact details.
        /// </value>
        public PersonContact PersonContactDetails
        {
            get { return personContactDetails; }
            set
            {

                if (personContactDetails != value)
                {
                    personContactDetails = value;
                    OnPropertyChanged(nameof(PersonContactDetails));
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is list grid enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is list grid enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsListGridEnabled
        {
            get { return isGridEnabled; }
            set
            {

                if (isGridEnabled != value)
                {
                    isGridEnabled = value;
                    OnPropertyChanged(nameof(IsListGridEnabled));
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is email or phone button enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is email or phone button enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmailOrPhoneButtonEnabled
        {
            get { return isEmailOrPhoneButtonEnabled; }
            set
            {

                if (isEmailOrPhoneButtonEnabled != value)
                {
                    isEmailOrPhoneButtonEnabled = value;
                    OnPropertyChanged(nameof(IsEmailOrPhoneButtonEnabled));
                }
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Gets or sets the add command.
        /// </summary>
        /// <value>
        /// The add command.
        /// </value>
        public ICommand AddCommand { get; set; }
        /// <summary>
        /// Gets or sets the update command.
        /// </summary>
        /// <value>
        /// The update command.
        /// </value>
        public ICommand UpdateCommand { get; set; }
        /// <summary>
        /// Gets or sets the on save command.
        /// </summary>
        /// <value>
        /// The on save command.
        /// </value>
        public ICommand OnSaveCommand { get; set; }
        /// <summary>
        /// Gets or sets the on cancel command.
        /// </summary>
        /// <value>
        /// The on cancel command.
        /// </value>
        public ICommand OnCancelCommand { get; set; }
        /// <summary>
        /// Gets or sets the update email command.
        /// </summary>
        /// <value>
        /// The update email command.
        /// </value>
        public ICommand UpdateEmailCommand { get; set; }
        /// <summary>
        /// Gets or sets the add email command.
        /// </summary>
        /// <value>
        /// The add email command.
        /// </value>
        public ICommand AddEmailCommand { get; set; }
        /// <summary>
        /// Gets or sets the add phone number command.
        /// </summary>
        /// <value>
        /// The add phone number command.
        /// </value>
        public ICommand AddPhoneNumberCommand { get; set; }
        /// <summary>
        /// Gets or sets the update phone number command.
        /// </summary>
        /// <value>
        /// The update phone number command.
        /// </value>
        public ICommand UpdatePhoneNumberCommand { get; set; }
        #endregion


        #region Methods

        /// <summary>
        /// Gets the contacts list.
        /// </summary>
        private async Task GetContactsList()
        {
            await GetContactsFromApi("http://localhost:57432/api/Contacts");
        }

        /// <summary>
        /// Gets the email list.
        /// </summary>
        private async Task GetEmailList()
        {
            await GetEmailsFromApi("http://localhost:57432/");
        }

        /// <summary>
        /// Gets the phone list.
        /// </summary>
        private async Task GetPhoneList()
        {
            await GetPhoneNumbersFromApi("http://localhost:57432/");
        }

        /// <summary>
        /// Called when [update].
        /// </summary>
        private async void OnUpdate()
        {
            PersonContactDetails = SelectedPersonContact.Clone();
            await GetEmailList();
            await GetPhoneList();
            IsEmailOrPhoneButtonEnabled = true;
            IsListGridEnabled = false;
            

        }

        /// <summary>
        /// Called when [add].
        /// </summary>
        private void OnAdd()
        {
            IsListGridEnabled = false;
            IsEmailOrPhoneButtonEnabled = false;
            PersonContactDetails = new PersonContact();
        }

        /// <summary>
        /// Updates the contacts from API.
        /// </summary>
        /// <param name="v">The v.</param>
        private async Task UpdateContactsFromApi(string v)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(v);
                var response = client.PutAsJsonAsync("api/Contact/" + PersonContactDetails?.ContactId.ToString(), PersonContactDetails).Result;
                if (response.IsSuccessStatusCode)
                {
                    PersonContactDetails = null;
                    IsListGridEnabled = true;
                    await GetContactsList();
                }
                else
                    Console.Write("Error");
            }

            
        }

        /// <summary>
        /// Gets the emails from API.
        /// </summary>
        /// <param name="v">The v.</param>
        private async Task GetEmailsFromApi(string v)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(v);
                var response = client.GetAsync("api/Emails/" + PersonContactDetails?.ContactId.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    EmailList =await response.Content.ReadAsAsync<ObservableCollection<Emails>>();
                    SelectedEmail = EmailList?.FirstOrDefault();
                }
                else
                    MessageBox.Show("Some Error occured on getting list of emails");
            }

            
        }

        /// <summary>
        /// Gets the phone numbers from API.
        /// </summary>
        /// <param name="v">The v.</param>
        private async Task GetPhoneNumbersFromApi(string v)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(v);
                var response = client.GetAsync("api/Phones/" + PersonContactDetails?.ContactId.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    PhoneList =await response.Content.ReadAsAsync<ObservableCollection<PhoneNumbers>>();
                    SelectedPhone = PhoneList?.FirstOrDefault();
                }
                else
                    MessageBox.Show("Some Error occured on getting list of emails");
            }

            
        }

        /// <summary>
        /// Adds the contacts from API.
        /// </summary>
        /// <param name="v">The v.</param>
        private async Task AddContactsFromApi(string v)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(v);
                var response = client.PostAsJsonAsync("api/Contact/", PersonContactDetails).Result;
                if (response.IsSuccessStatusCode)
                {
                    PersonContactDetails = null;
                    IsListGridEnabled = true;
                    await GetContactsList();
                }
                else
                    Console.Write("Error");
            }

            
        }

        /// <summary>
        /// Gets the contacts from API.
        /// </summary>
        /// <param name="v">The v.</param>
        private async Task GetContactsFromApi(string v)
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(v);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    PersonContacts = await httpResponseMessage.Content.ReadAsAsync<ObservableCollection<PersonContact>>();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            

        }

        /// <summary>
        /// Called when [cancel].
        /// </summary>
        private void OnCancel()
        {
            PersonContactDetails = null;
            EmailList = null;
            SelectedEmail = null;
            PhoneList = null;
            IsListGridEnabled = true;
        }

        /// <summary>
        /// Called when [save].
        /// </summary>
        private async void OnSave()
        {
            //ValidateProperty(PersonContactDetails.FirstName, nameof(PersonContactDetails.FirstName));
            if (string.IsNullOrEmpty(PersonContactDetails.FirstName))
            {
                MessageBox.Show("First Name is Required!!!");
            }

            if (string.IsNullOrEmpty(PersonContactDetails.LastName))
            {
                MessageBox.Show("Last Name is Required!!!");
            }
            else
            { 
                if (PersonContactDetails.ContactId > 0)
                {
                    await UpdateContactsFromApi("http://localhost:57432/");
                }
                else
                {
                    await AddContactsFromApi("http://localhost:57432/");
                }
            }
        }

        /// <summary>
        /// Updates the email.
        /// </summary>
        private async void UpdateEmail()
        {
            EmailDetailsView emailDetailsView = new EmailDetailsView();
            emailDetailsView.DataContext = emailViewModel;
            emailViewModel.EmailDetail = SelectedEmail;
            emailViewModel.EmailDetailsView = emailDetailsView;
            emailViewModel.EmailDetail.ContactId = SelectedPersonContact.ContactId;
            emailDetailsView.ShowDialog();
            if(SharedContext.IsRefreshed)
            {
               await GetEmailList();
            }
        }

        /// <summary>
        /// Adds the email.
        /// </summary>
        private async void AddEmail()
        {
            EmailDetailsView emailDetailsView = new EmailDetailsView();
            emailDetailsView.DataContext = emailViewModel;
            emailViewModel.EmailDetail = new Emails();
            emailViewModel.EmailDetailsView = emailDetailsView;
            emailViewModel.EmailDetail.ContactId = SelectedPersonContact.ContactId;
            emailDetailsView.ShowDialog();
            if(SharedContext.IsRefreshed)
            {
               await GetEmailList();
            }
        }

        /// <summary>
        /// Updates the phone number.
        /// </summary>
        private async void UpdatePhoneNumber()
        {
            PhoneDetailsView phoneNumberView = new PhoneDetailsView();
            phoneNumberView.DataContext = phoneNumberViewModel;
            phoneNumberViewModel.PhoneNumber = SelectedPhone;
            phoneNumberViewModel.PhoneDetailsView = phoneNumberView;
            phoneNumberViewModel.PhoneNumber.ContactId = SelectedPersonContact.ContactId;
            phoneNumberView.ShowDialog();
            if(SharedContext.IsRefreshed)
            {
               await GetPhoneList();
            }
        }

        /// <summary>
        /// Adds the phone number.
        /// </summary>
        private async void AddPhoneNumber()
        {
            PhoneDetailsView phoneNumberView = new PhoneDetailsView();
            phoneNumberView.DataContext = phoneNumberViewModel;
            phoneNumberViewModel.PhoneNumber = new PhoneNumbers();
            phoneNumberViewModel.PhoneDetailsView = phoneNumberView;
            phoneNumberViewModel.PhoneNumber.ContactId = SelectedPersonContact.ContactId;
            phoneNumberView.ShowDialog();
            if (SharedContext.IsRefreshed)
            {
                await GetPhoneList();
            }
        }
        #endregion





    }


}
