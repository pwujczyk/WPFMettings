using MeetingsDTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMeetingsWorkplacePlugin.Plugins.ContactsPlugin
{
    class ContactsVM: VMBase, INotifyPropertyChanged
    {
        
        private ContactsBM model = new ContactsBM();

        ObservableCollection<Contact> contacts;
        public ObservableCollection<Contact> Contacts
        {
            get
            {
                if (contacts==null)
                {
                    this.contacts = new ObservableCollection<Contact>(model.GetContacts());
                }
                return contacts;
            }
        }

        Contact selectedContact;
        public Contact SelectedContact
        {
            get
            {
                if (selectedContact==null)
                {
                    selectedContact = this.Contacts.First();
                }
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
        
        internal void Save()
        {
            model.SaveContact(this.SelectedContact);
            ReloadContacts();
        }

        private void ReloadContacts()
        {
            this.contacts= new ObservableCollection<Contact>(model.GetContacts());
            NotifyPropertyChanged(nameof(Contacts));
        }
    }
}
