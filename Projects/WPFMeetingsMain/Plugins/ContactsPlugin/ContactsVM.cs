using MeetingsDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMeetingsWorkplacePlugin.Plugins.ContactsPlugin
{
    class ContactsVM: VMBase, INotifyPropertyChanged
    {
        
        private ContactsBM model = new ContactsBM();

        List<Contact> contacts;
        public List<Contact> Contacts
        {
            get
            {
                if (contacts!=null)
                {
                    this.contacts = model.GetContacts();
                }
                return contacts;
            }
        }

        Contact selectedContact;
        public Contact SelectedContact
        {
            get
            {
                return selectedContact;
            }
            set
            {
                this.selectedContact = value;
                NotifyPropertyChanged(nameof(SelectedContact));
            }
        }

        internal void AddNew()
        {
            Contact c = new Contact();
            this.Contacts.Add(c);
            this.SelectedContact = c;
        }
    }
}
