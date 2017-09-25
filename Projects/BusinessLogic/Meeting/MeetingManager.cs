using DALDataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingsDTO;

namespace BusinessLogic.Meeting
{
    public class MeetingManager
    {
        //DALDataManager dataManager = new DALDataManager();

        public void UpdateDatabase()
        {
            //dataManager.UpgradeDB();
        }

        public List<MeetingsDTO.Meeting> GetMeetings()
        {
            DALDataManager dataManager = new DALDataManager();
            //return dataManager.GetMeetings();
             return dataManager.GetCompactMeetings();
            return null;
        }

        public void SaveMeeting(MeetingsDTO.Meeting meeting)
        {
            DALDataManager dataManager = new DALDataManager();
            dataManager.AddMeeting(meeting);
            
            //dataManager.AddMeeting(meeting);
        }
    }
}
