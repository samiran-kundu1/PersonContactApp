using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonContactApp.Model 
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class PersonContact : INotifyPropertyChanged
    {
        #region Private Members
        /// <summary>
        /// The first name
        /// </summary>
        private string firstName;

        /// <summary>
        /// The last name
        /// </summary>
        private string lastName;

        /// <summary>
        /// The contact identifier
        /// </summary>
        private int contactId;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required(ErrorMessage ="Cannot Be Empty!!")]
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
                {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }

        }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged(nameof(LastName));
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

        #endregion

        #region Clone Method

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public PersonContact Clone()
        {
            return new PersonContact
            {
                FirstName = FirstName,
                ContactId = ContactId,
                LastName = LastName
            };
        }

        #endregion

        #region INotifyPropertyChanged Members  

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
