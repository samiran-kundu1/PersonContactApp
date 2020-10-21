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
    public class PhoneNumbers : Updater
    {

        #region Private Members
        /// <summary>
        /// The phone identifier
        /// </summary>
        private int phoneId;
        /// <summary>
        /// The contact identifier
        /// </summary>
        private int contactId;

        /// <summary>
        /// The phone number
        /// </summary>
        private string phoneNumber;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the phone identifier.
        /// </summary>
        /// <value>
        /// The phone identifier.
        /// </value>
        public int PhoneId
        {
            get
            {
                return phoneId;
            }
            set
            {
                if (phoneId != value)
                {
                    phoneId = value;
                    OnPropertyChanged(nameof(PhoneId));
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
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                if (phoneNumber != value)
                {
                    phoneNumber = value;
                    OnPropertyChanged(nameof(PhoneNumber));
                }
            }

        }
        #endregion
    }
}
