using MeetingsDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMeetingsWorkplacePlugin
{
    public class MetingsMainWindowVM: VMBase, INotifyPropertyChanged
    {
        MetingsMainWindowBM Model = new MetingsMainWindowBM();

        List<Meeting> meetings;   

        public List<Meeting> Meetings
        {
            get
            {
                if (meetings==null)
                {
                    this.meetings = GetMeetings();
                }
                return this.meetings;
            }
        }

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
        }

        private List<Meeting> GetMeetings ()
        {
            return Model.GetMeetings();
        }

        public void ClearFields()
        {
            this.SelectedMeeting.AfterNotes = this.SelectedMeeting.BeforeNotes = this.SelectedMeeting.DuringNotes  = string.Empty;
            NotifyPropertyChanged("SelectedMeeting");
        }

        public void SaveMeeting()
        {
            this.Model.SaveMeeting(SelectedMeeting);
        }
    }
}
