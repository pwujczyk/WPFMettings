using BusinessLogic.Meeting;
using MeetingsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMeetingsWorkplacePlugin
{
    class MetingsMainWindowBM
    {
        MeetingManager manager = new MeetingManager();
        public MetingsMainWindowBM()
        {
            UpdateDatabase();
        }

        private void UpdateDatabase()
        {
            manager.UpdateDatabase();
        }

        public List<Meeting> GetMeetings()
        {
            var x = manager.GetMeetings();
            return x;
        }

        public void SaveMeeting(Meeting meeting)
        {
            manager.SaveMeeting(meeting);
        }
    }
}
