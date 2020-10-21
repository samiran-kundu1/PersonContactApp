using Microsoft.Practices.Prism.Commands;
using PersonContactApp.Helper_Class;
using PersonContactApp.Model;
using PersonContactApp.Shared;
using PersonContactApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PersonContactApp.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="PersonContactApp.Helper_Class.Updater" />
    public class PhoneNumberViewModel:Updater
    {
        #region Private Members
        /// <summary>
        /// The phone number
        /// </summary>
        private PhoneNumbers  phoneNumber;
        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumberViewModel"/> class.
        /// </summary>
        public PhoneNumberViewModel()
        {
            OnSaveCommand = new DelegateCommand(OnSave);
            OnCancelCommand = new DelegateCommand(OnCancel);
        }



        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public PhoneNumbers PhoneNumber
        {
            get { return  phoneNumber; }
            set
            {
                phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        /// <summary>
        /// The phone details view
        /// </summary>
        private PhoneDetailsView phoneDetailsView  ;

        /// <summary>
        /// Gets or sets the phone details view.
        /// </summary>
        /// <value>
        /// The phone details view.
        /// </value>
        public PhoneDetailsView PhoneDetailsView  
        {
            get { return phoneDetailsView; }
            set {
                phoneDetailsView = value;
                OnPropertyChanged(nameof(PhoneDetailsView));
            }
        }


        #region Commands
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
        #endregion

        #region Methods

        /// <summary>
        /// Called when [save].
        /// </summary>
        private async void OnSave()
        {
            if (PhoneNumber.PhoneId > 0)
            {
                await UpdatePhoneFromApi("http://localhost:57432/");
            }
            else
            {
                await AddPhoneFromApi("http://localhost:57432/");
            }
        }
        /// <summary>
        /// Adds the phone from API.
        /// </summary>
        /// <param name="v">The v.</param>
        private async Task AddPhoneFromApi(string v)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(v);
                var response = client.PostAsJsonAsync("api/Phone/", PhoneNumber).Result;
                if (response.IsSuccessStatusCode)
                {
                    SharedContext.IsRefreshed = true;
                    PhoneDetailsView.Close();
                }
                else
                    SharedContext.IsRefreshed = false;

            }


        }

        /// <summary>
        /// Updates the phone from API.
        /// </summary>
        /// <param name="v">The v.</param>
        private async Task UpdatePhoneFromApi(string v)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(v);
                var response = client.PutAsJsonAsync("api/Phone/" + PhoneNumber?.PhoneId.ToString(), PhoneNumber).Result;
                if (response.IsSuccessStatusCode)
                {
                    SharedContext.IsRefreshed = true;
                    PhoneDetailsView.Close();

                }
                else
                    SharedContext.IsRefreshed = false;
            }
        }
        /// <summary>
        /// Called when [cancel].
        /// </summary>
        private void OnCancel()
        {
            SharedContext.IsRefreshed = false;
            PhoneDetailsView.Close();
        }
        #endregion

    }
}
