using AutoMapper;
using ConnectionStringHelper;
using DALCompactProvider;
using DALContracts;
using DALServerProvider;
using MeetingsDTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace DALDataProvider
{
    public class DALDataManager
    {

        IDataProvider dataProvider;

        public DALDataManager()
        {
            dataProvider = new ServerProvider();
        }

        public List<MeetingsDTO.Meeting> GetCompactMeetings()
        {
            return dataProvider.GetCompactMeetings();
        }

        public void SaveMeeting(MeetingsDTO.Meeting input)
        {
            dataProvider.SaveMeeting(input);
        }

        public void AddContact(MeetingsDTO.Contact contact)
        {
            dataProvider.AddContact(contact);
        }

        public List<MeetingsDTO.Contact> GetContacts()
        {
            return dataProvider.GetContacts();
        }

        public void DeleteMeeting(int value)
        {
            dataProvider.DeleteMeeting(value);
        }
    }
}
