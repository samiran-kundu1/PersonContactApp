using Microsoft.Practices.Prism.Commands;
using PersonContactApp.Helper_Class;
using PersonContactApp.Model;
using PersonContactApp.Shared;
using PersonContactApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class EmailDetailsViewModel : Updater
    {
        #region Private Members
        /// <summary>
        /// The email detail
        /// </summary>
        private Emails emailDetail;
        /// <summary>
        /// The email details view
        /// </summary>
        private EmailDetailsView emailDetailsView;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailDetailsViewModel" /> class.
        /// </summary>
        public EmailDetailsViewModel()
        {
            OnSaveCommand = new DelegateCommand(OnSave);
            OnCancelCommand = new DelegateCommand(OnCancel);
        }
        #endregion



        #region Properties
        /// <summary>
        /// Gets or sets the email detail.
        /// </summary>
        /// <value>
        /// The email detail.
        /// </value>
        public Emails EmailDetail
        {
            get
            {
                return
                    emailDetail;
            }
            set
            {
                emailDetail = value;
                OnPropertyChanged(nameof(EmailDetail));
            }
        }

        /// <summary>
        /// Gets or sets the email details view.
        /// </summary>
        /// <value>
        /// The email details view.
        /// </value>
        public EmailDetailsView EmailDetailsView
        {
            get
            {
                return
                    emailDetailsView;
            }
            set
            {
                emailDetailsView = value;
                OnPropertyChanged(nameof(EmailDetailsView));
            }
        }
        #endregion

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
        /// <exception cref="NotImplementedException"></exception>
        private async void OnSave()
        {
            if (EmailDetail.EmailId > 0)
            {
                await UpdateEmailFromApi("http://localhost:57432/");
            }
            else
            {
                await AddEmailFromApi("http://localhost:57432/");
            }
        }

        /// <summary>
        /// Adds the email from API.
        /// </summary>
        /// <param name="v">The v.</param>
        private async Task AddEmailFromApi(string v)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(v);
                var response = client.PostAsJsonAsync("api/Email/", EmailDetail).Result;
                if (response.IsSuccessStatusCode)
                {
                    SharedContext.IsRefreshed = true;
                    EmailDetailsView.Close();
                }
                else
                    SharedContext.IsRefreshed = false;

            }


        }

        /// <summary>
        /// Updates the email from API.
        /// </summary>
        /// <param name="v">The v.</param>
        private async Task UpdateEmailFromApi(string v)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(v);
                var response = client.PutAsJsonAsync("api/Email/" + EmailDetail?.EmailId.ToString(), EmailDetail).Result;
                if (response.IsSuccessStatusCode)
                {
                    SharedContext.IsRefreshed = true;
                    EmailDetailsView.Close();

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
            EmailDetailsView.Close();
        }
        #endregion


    }
}
