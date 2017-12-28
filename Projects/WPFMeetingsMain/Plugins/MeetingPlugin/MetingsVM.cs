using MeetingsDTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFMeetingsWorkplacePlugin.Arch;
using WPFMeetingsWorkplacePlugin.Plugins.ContactsPlugin;

namespace WPFMeetingsWorkplacePlugin.Plugins.MeetingPlugin
{
    public class MetingsVM : VMBase, INotifyPropertyChanged
    {

        public MetingsVM()
        {
            this.AddContact = new RelayCommand(AddContactRelay);
            this.EnterNotes = new RelayCommand(EnterNotesRelay);
            this.Notes = new ObservableCollection<NotesVM>();
            this.Notes.Add(new NotesVM() { Notes = new MeetingsDTO.Notes() { Text = "pawel" }});
        }

        private void EnterNotesRelay(object obj)
        {
            LoadMenu(null);
        }

        MeetingsManager MetingsManager = new MeetingsManager();

        public ICommand AddContact { get; set; }
        public ICommand EnterNotes { get; set; }
        
        List<Contact> contacts;
        public List<Contact> Contacts
        {
            get
            {
                if (contacts == null)
                {
                    ContactsBM contactcBM = new ContactsBM();
                    contacts = contactcBM.GetContacts();
                }
                return contacts;
            }
        }

        private Contact selectedContact;
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

        ObservableCollection<Meeting> meetings;
        public ObservableCollection<Meeting> Meetings
        {
            get
            {
                if (meetings == null)
                {
                    ReloadMeetings();
                }
                return this.meetings;
            }
        }

            

        
        public bool SelectedText
        {
            get;set;
        }

        private void ReloadMeetings()
        {
            this.meetings = new ObservableCollection<Meeting>(GetMeetings());
            NotifyPropertyChanged("Meetings");
        }

        public ObservableCollection<NotesVM> Notes { get; set; }

        private Meeting selectedMeeting;
        public Meeting SelectedMeeting
        {
            get
            {
                return this.selectedMeeting;
            }
            set
            {
                this.selectedMeeting = value;
                NotifyPropertyChanged("SelectedMeeting");
                
            }
        }

        internal void AddNew()
        {
            Meeting m = new Meeting(true);
            this.Meetings.Add(m);
            this.SelectedMeeting = m;
            NotifyPropertyChanged("Meetings");
        }

        private List<Meeting> GetMeetings()
        {
            //return new List<Meeting>();
            return MetingsManager.GetMeetings();
        }

        public void ClearFields()
        {
            //this.SelectedMeeting.AfterNotes = this.SelectedMeeting.BeforeNotes = this.SelectedMeeting.DuringNotes = string.Empty;
            NotifyPropertyChanged("SelectedMeeting");
        }

        public void SaveMeeting()
        {
            NotifyPropertyChanged("SelectedMeeting");
            NotifyPropertyChanged("TextRawProperty");
            //var x = SelectedMeeting.DuringNotes;
            this.MetingsManager.SaveMeeting(SelectedMeeting);
            if (!SelectedMeeting.MeetingId.HasValue)
            {
                ReloadMeetings();
            }
        }

        private void AddContactRelay(object obj)
        {
            if (!this.SelectedMeeting.Contacts.Contains(this.SelectedContact))
            {
                this.SelectedMeeting.Contacts.Add(this.SelectedContact);
            }
        }

        public void DeleteMeeting()
        {
            this.MetingsManager.DeleteMeeting(SelectedMeeting);
            ReloadMeetings();
        }
        
        public void Bullet()
        {
            this.SelectedText = true;
            NotifyPropertyChanged("SelectedText");
        }
    }
}
