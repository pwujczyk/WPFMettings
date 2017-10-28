using DALContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALCompact;
using ConnectionStringHelper;
using Tools;
using System.Collections.ObjectModel;
using AutoMapper;

namespace DALCompactProvider
{
    public class CompactProvider : IDataProvider
    {
        string connectionStringLocal;
        private string ConnectionStringLocal
        {
            get
            {
                if (string.IsNullOrEmpty(connectionStringLocal))
                {
                    connectionStringLocal = ConnectionString.GetSqlCompactEntityFrameworkConnectionString(Consts.CompactFilePath, Consts.MetaDataNameLocal);
                }
                return connectionStringLocal;
            }
        }

        MeetingsLocalEntities localContext;
        private MeetingsLocalEntities LocalContext
        {
            get
            {
                if (localContext == null)
                {
                    localContext = new MeetingsLocalEntities(ConnectionStringLocal);

                }
                return localContext;
            }
        }

        public void AddContact(MeetingsDTO.Contact contact)
        {
            IMapper Mapper = new Mapper(AutoMapperConfig.Configuration);

            DALCompact.Contact compactContact = null;
            if (contact.ContactId == null)
            {
                compactContact = Mapper.Map<DALCompact.Contact>(contact);
                LocalContext.Contact.Add(compactContact);
            }
            else
            {
                compactContact = LocalContext.Contact.Single(x => x.ContactId == contact.ContactId);
                Mapper.Map<MeetingsDTO.Contact, DALCompact.Contact>(contact, compactContact);

            }

            LocalContext.SaveChanges();
        }

        public List<MeetingsDTO.Meeting> GetCompactMeetings()
        {
            List<MeetingsDTO.Meeting> meetings = new List<MeetingsDTO.Meeting>();
            IMapper Mapper = new Mapper(AutoMapperConfig.Configuration);
            //   MeetingsLocalContext meetinglocal = new MeetingsLocalContext();
            try
            {
                var compactmeetings = LocalContext.Meeting.ToList();
                foreach (var item in compactmeetings)
                {
                    MeetingsDTO.Meeting meeting = Mapper.Map<DALCompact.Meeting, MeetingsDTO.Meeting>(item);
                    meeting.Contacts = new ObservableCollection<MeetingsDTO.Contact>(Mapper.Map<ICollection<DALCompact.Contact>, List<MeetingsDTO.Contact>>(item.Contact));
                    meetings.Add(meeting);
                }
            }
            catch (Exception erx)
            {

                throw;
            }


            return meetings;
        }

        public List<MeetingsDTO.Contact> GetContacts()
        {
            List<MeetingsDTO.Contact> contacts = new List<MeetingsDTO.Contact>();
            var dbContacts = LocalContext.Contact.ToList();
            IMapper Mapper = new Mapper(AutoMapperConfig.Configuration);
            contacts = Mapper.Map<List<DALCompact.Contact>, List<MeetingsDTO.Contact>>(dbContacts);
            return contacts;
        }

        public void SaveMeeting(MeetingsDTO.Meeting input)
        {
            IMapper Mapper = new Mapper(AutoMapperConfig.Configuration);
            DALCompact.Meeting compactMeeting;
            if (input.MeetingId == null)
            {
                compactMeeting = Mapper.Map<DALCompact.Meeting>(input);
                //Mapper.Map <MeetingsDTO.Meeting, DALCompact.Meeting> (input, compactMeeting);
                LocalContext.Meeting.Add(compactMeeting);

            }
            else
            {
                compactMeeting = LocalContext.Meeting.Single(x => x.MeetingId == input.MeetingId);
                Mapper.Map<MeetingsDTO.Meeting, DALCompact.Meeting>(input, compactMeeting);

                foreach (var item in input.Contacts)
                {
                    var contactLocal = LocalContext.Contact.Single(x => x.ContactId == item.ContactId);
                    compactMeeting.Contact.Add(contactLocal);

                }
            }
            LocalContext.SaveChanges();
        }

        public void DeleteMeeting(int value)
        {

        }
    }
}
