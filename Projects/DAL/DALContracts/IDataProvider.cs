using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALContracts
{
    public interface IDataProvider
    {
        List<MeetingsDTO.Meeting> GetCompactMeetings();
        void SaveMeeting(MeetingsDTO.Meeting input);
        void AddContact(MeetingsDTO.Contact contact);
        List<MeetingsDTO.Contact> GetContacts();
        void DeleteMeeting(int value);
    }
}
