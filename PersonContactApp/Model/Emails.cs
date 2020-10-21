using PersonContactApp.Helper_Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonContactApp.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="PersonContactApp.Helper_Class.Updater" />
    public class Emails: Updater
    {


        #region Private Members
        /// <summary>
        /// The email identifier
        /// </summary>
        private int emailId;

        /// <summary>
        /// The contact identifier
        /// </summary>
        private int contactId;

        /// <summary>
        /// The email
        /// </summary>
        private string email;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the email identifier.
        /// </summary>
        /// <value>
        /// The email identifier.
        /// </value>
        public int EmailId
        {
            get
            {
                return emailId;
            }
            set
            {
                if (emailId != value)
                {
                    emailId = value;
                    OnPropertyChanged(nameof(EmailId));
                }
            }

        }
        /// <summary>
        /// Gets or sets the contact identifier.
        /// </summary>
        /// <value>
        /// The contact identifier.
        /// </value>
        public int ContactId
        {
            get
            {
                return contactId;
            }
            set
            {
                if (contactId != value)
                {
                    contactId = value;
                    OnPropertyChanged(nameof(ContactId));
                }
            }

        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }

        }

        #endregion
    }
}
