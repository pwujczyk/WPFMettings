using AutoMapper;
using ConnectionStringHelper;
using DALCompact;
using DALServer;
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
        //private string ConnectionString
        //{
        //    get
        //    {
        //        string path = @"D:\VisualStudio\WPFMeetings\DALLocal\ClearSQLiteDB.sqlite";
        //        string connectionString = new ConnectionStringHelper.ConnectionString().GetSqliteConnectionString(path);
        //        return connectionString;
        //    }
        //}

        //LocalDBContext localContext;
        //private LocalDBContext LocalContext
        //{
        //    get
        //    {
        //        if (localContext == null)
        //        {
        //            localContext = new LocalDBContext(ConnectionString);
        //        }
        //        return localContext;
        //    }
        //}

        //public void UpgradeDB()
        //{
        //    try
        //    {
        //        new LocalDBContext(ConnectionString).Initialize(ConnectionString);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}

        //public List<MeetingsDTO.Meeting> GetMeetings()
        //{

        //    LocalDBContext localContext = new LocalDBContext(ConnectionString);
        //    List<MeetingsDTO.Meeting> meetings = new List<MeetingsDTO.Meeting>();
        //    var dalMeetings = localContext.GetMeetings();
        //    foreach (var item in dalMeetings)
        //    {
        //        meetings.Add(new MeetingsDTO.Meeting()
        //        {
        //            AfterNotes = item.AfterNotes,
        //            BeforeNotes = item.BeforeNotes,
        //            Date = item.Date,
        //            DuringNotes = item.DuringNotes,
        //            MeetingId = item.MeetingId
        //        });
        //    }
        //    return meetings;
        //}

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

        //MeetingsLocalEntities serverContext;
        //private MeetingsLocalEntities ServerContext
        //{
        //    get
        //    {
        //        if (serverContext == null)
        //        {
        //            serverContext = new MeetingsLocalEntities(ConnectionStringServer);

        //        }
        //        return serverContext;
        //    }
        //}

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


        public void SaveMeeting(MeetingsDTO.Meeting input)
        {
            IMapper Mapper = new Mapper(AutoMapperConfig.Configuration);
            DALCompact.Meeting compactMeeting;
            DALServer.Meeting serverMeeting;
            if (input.MeetingId == null)
            {
                compactMeeting = Mapper.Map<DALCompact.Meeting>(input);
                //Mapper.Map <MeetingsDTO.Meeting, DALCompact.Meeting> (input, compactMeeting);
                LocalContext.Meeting.Add(compactMeeting);

                serverMeeting = Mapper.Map<DALServer.Meeting>(input);
                ServerContext.Meeting.Add(serverMeeting);
            }
            else
            {
                compactMeeting = LocalContext.Meeting.Single(x => x.MeetingId == input.MeetingId);
                serverMeeting = ServerContext.Meeting.Single(x => x.MeetingId == input.MeetingId);
                Mapper.Map<MeetingsDTO.Meeting, DALCompact.Meeting>(input, compactMeeting);
                Mapper.Map<MeetingsDTO.Meeting, DALServer.Meeting>(input, serverMeeting);

                foreach (var item in input.Contacts)
                {
                    var contactLocal = LocalContext.Contact.Single(x => x.ContactId == item.ContactId);
                    compactMeeting.Contact.Add(contactLocal);

                    var contactServer = ServerContext.Contact.Single(x => x.ContactId == item.ContactId);
                    serverMeeting.Contact.Add(contactServer);
                }
            }           
            LocalContext.SaveChanges();
            ServerContext.SaveChanges();
        }

        public void AddContact(MeetingsDTO.Contact contact)
        {
            IMapper Mapper = new Mapper(AutoMapperConfig.Configuration);

            DALCompact.Contact compactContact = null;
            DALServer.Contact serverContact = null;
            if (contact.ContactId==null)
            {
                compactContact = Mapper.Map<DALCompact.Contact>(contact);
                serverContact = Mapper.Map<DALServer.Contact>(contact);
                LocalContext.Contact.Add(compactContact);
                ServerContext.Contact.Add(serverContact);
            }
            else
            {
                compactContact = LocalContext.Contact.Single(x => x.ContactId == contact.ContactId);
                serverContact = ServerContext.Contact.Single(x => x.ContactId == contact.ContactId);
                Mapper.Map<MeetingsDTO.Contact, DALCompact.Contact>(contact, compactContact);
                Mapper.Map<MeetingsDTO.Contact, DALServer.Contact>(contact, serverContact);
                
            }
            
            LocalContext.SaveChanges();
            ServerContext.SaveChanges();
        }

        public List<MeetingsDTO.Contact> GetContacts()
        {
            List<MeetingsDTO.Contact> contacts = new List<MeetingsDTO.Contact>();
            var dbContacts = LocalContext.Contact.ToList();
            IMapper Mapper = new Mapper(AutoMapperConfig.Configuration);
            contacts = Mapper.Map<List<DALCompact.Contact>,List<MeetingsDTO.Contact>>(dbContacts);
            return contacts;
        }
    }
}
