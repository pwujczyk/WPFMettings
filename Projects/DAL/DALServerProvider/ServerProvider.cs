using DALContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingsDTO;
using DALServer;
using AutoMapper;
using Tools;
using ConnectionStringHelper;
using System.Collections.ObjectModel;

namespace DALServerProvider
{
    public class ServerProvider : IDataProvider
    {
        string connectionStringServer;
        private string ConnectionStringServer
        {
            get
            {
                if (string.IsNullOrEmpty(connectionStringServer))
                {
                    connectionStringServer = ConnectionString.GetSqlEntityFrameworkConnectionString(Consts.DataSource, Consts.DatabasesName, Consts.MetaDataNameServer);
                }
                return connectionStringServer;
            }
        }



        WPFMeetingsEntities serverContext;
        private WPFMeetingsEntities ServerContext
        {
            get
            {
                if (serverContext == null)
                {
                    serverContext = new WPFMeetingsEntities(ConnectionStringServer);

                }
                return serverContext;
            }
        }

        public void AddContact(MeetingsDTO.Contact contact)
        {
            IMapper Mapper = new Mapper(AutoMapperConfig.Configuration);

            DALServer.Contact serverContact = null;
            if (contact.ContactId == null)
            {
                serverContact = Mapper.Map<DALServer.Contact>(contact);
                ServerContext.Contact.Add(serverContact);
            }
            else
            {
                serverContact = ServerContext.Contact.Single(x => x.ContactId == contact.ContactId);
                Mapper.Map<MeetingsDTO.Contact, DALServer.Contact>(contact, serverContact);

            }

            ServerContext.SaveChanges();
        }

        public List<MeetingsDTO.Meeting> GetCompactMeetings()
        {
            List<MeetingsDTO.Meeting> meetings = new List<MeetingsDTO.Meeting>();
            IMapper Mapper = new Mapper(AutoMapperConfig.Configuration);
            //   MeetingsLocalContext meetinglocal = new MeetingsLocalContext();
            try
            {
                var compactmeetings = ServerContext.Meeting.ToList();
                foreach (var item in compactmeetings)
                {
                    MeetingsDTO.Meeting meeting = Mapper.Map<DALServer.Meeting, MeetingsDTO.Meeting>(item);
                    meeting.Contacts = new ObservableCollection<MeetingsDTO.Contact>(Mapper.Map<ICollection<DALServer.Contact>, List<MeetingsDTO.Contact>>(item.Contact));
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
            var dbContacts = ServerContext.Contact.ToList();
            IMapper Mapper = new Mapper(AutoMapperConfig.Configuration);
            contacts = Mapper.Map<List<DALServer.Contact>, List<MeetingsDTO.Contact>>(dbContacts);
            return contacts;
        }

        public void SaveMeeting(MeetingsDTO.Meeting input)
        {
            IMapper Mapper = new Mapper(AutoMapperConfig.Configuration);
            DALServer.Meeting serverMeeting;
            if (input.MeetingId == null)
            {

                serverMeeting = Mapper.Map<DALServer.Meeting>(input);
                ServerContext.Meeting.Add(serverMeeting);
            }
            else
            {
                serverMeeting = ServerContext.Meeting.Single(x => x.MeetingId == input.MeetingId);
                Mapper.Map<MeetingsDTO.Meeting, DALServer.Meeting>(input, serverMeeting);

                foreach (var item in input.Contacts)
                {

                    var contactServer = ServerContext.Contact.Single(x => x.ContactId == item.ContactId);
                    serverMeeting.Contact.Add(contactServer);
                }
            }
            ServerContext.SaveChanges();
        }

        public void DeleteMeeting(int value)
        {
            var meeting=ServerContext.Meeting.Single(x => x.MeetingId == value);
            ServerContext.Meeting.Remove(meeting);
            ServerContext.SaveChanges();
        }
    }
}
