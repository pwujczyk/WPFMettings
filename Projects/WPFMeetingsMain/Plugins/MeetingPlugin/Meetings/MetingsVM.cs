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
using WPFMeetingsWorkplacePlugin.Plugins.MeetingPlugin.Meetingss;
using WPFMeetingsWorkplacePlugin.Plugins.MeetingPlugin.Notes;

namespace WPFMeetingsWorkplacePlugin.Plugins.MeetingPlugin.Meetings
{
    public class MetingsVM : VMBase, INotifyPropertyChanged
    {

        public MetingsVM()
        {
            this.AddContact = new RelayCommand(AddContactRelay);
            this.EnterNotes = new RelayCommand(EnterNotesRelay);


            this.NotesVM = new ObservableCollection<NotesVM>();
            this.NotesVM.Add(new NotesVM());
            this.NotesVM.Add(new NotesVM());
            this.NotesVM.Add(new NotesVM());
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
            set
            {
                this.SelectedMeeting.DuringNotesXaml.SelectedText = value;
            }
            get
            {
                return this.SelectedMeeting.DuringNotesXaml.SelectedText;
            }
        }

        private void ReloadMeetings()
        {
            this.meetings = new ObservableCollection<Meeting>(GetMeetings());
            NotifyPropertyChanged("Meetings");
        }

        public ObservableCollection<NotesVM> NotesVM { get; set; }

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
                UpdateNotes();
                NotifyPropertyChanged("SelectedMeeting");
            }
        }

        private void UpdateNotes()
        {
            this.NotesVM[0].Note = this.SelectedMeeting.BeforeNotesXaml;
            this.NotesVM[1].Note = this.SelectedMeeting.DuringNotesXaml;
            this.NotesVM[2].Note = this.SelectedMeeting.AfterNotesXaml;
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
            // this.SelectedText = !this.SelectedText;
            foreach (var item in this.NotesVM)
            {
                item.BulletChanged();
            }
            //this.SelectedMeeting = this.Meetings[2];
            //this.NotesVM[0].Note = this.SelectedMeeting.BeforeNotesXaml;
            //this.NotesVM[1].Note = this.SelectedMeeting.DuringNotesXaml;
            //NotifyPropertyChanged("SelectedMeeting");
            //NotifyPropertyChanged("SelectedText");
            //NotifyPropertyChanged("SelectedMeeting");
            //NotifyPropertyChanged("Note");
        }
    }
}
