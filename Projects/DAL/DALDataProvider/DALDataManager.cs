﻿using AutoMapper;
using ConnectionStringHelper;
using DALCompact;
using DALServer;
using MeetingsDTO;
using System;
using System.Collections.Generic;
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
                    connectionStringLocal=ConnectionString.GetSqlCompactEntityFrameworkConnectionString(Consts.CompactFilePath, Consts.MetaDataNameLocal);
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
                    connectionStringServer = ConnectionString.GetSqlEntityFrameworkConnectionString(Consts.DataSource,Consts.DatabasesName, Consts.MetaDataNameServer);
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

            //   MeetingsLocalContext meetinglocal = new MeetingsLocalContext();
            try
            {
                var compactmeetings = LocalContext.Meeting.ToList();
                foreach (var item in compactmeetings)
                {
                    meetings.Add(new MeetingsDTO.Meeting()
                    {
                        AfterNotes = item.AfterNotes,
                        BeforeNotes = item.BeforeNotes,
                        Date = item.Date,
                        DuringNotes = item.DuringNotes,
                        MeetingId = item.MeetingId
                    });
                }
            }
            catch (Exception erx)
            {

                throw;
            }
            
           
            return meetings;
        }

        //public void AddMeeting(MeetingsDTO.Meeting input)
        //{
        //    DALLocal.Tables.Meeting meeting = new DALLocal.Tables.Meeting();
        //    if (input.MeetingId.HasValue)
        //    {
        //        meeting.MeetingId = input.MeetingId.Value;
        //    }
        //    else
        //    {
        //        meeting.MeetingId = -1;
        //    }
        //    meeting.AfterNotes = input.AfterNotes;
        //    meeting.BeforeNotes = input.BeforeNotes;
        //    meeting.DuringNotes = input.DuringNotes;
        //    meeting.Date = input.Date;

        //    LocalContext.AddMeeting(meeting);
        //}

        public void AddCompactMeeting(MeetingsDTO.Meeting input)
        {
            IMapper Mapper = new Mapper(AutoMapperConfig.Configuration);
            var compactMeeting = Mapper.Map<DALCompact.Meeting>(input);

            //if (input.MeetingId.HasValue)
            //{
            //    meeting.MeetingId = input.MeetingId.Value;
            //}
            //else
            //{
            //    meeting.MeetingId = -1;
            //}
            //meeting.AfterNotes = input.AfterNotes;
            //meeting.BeforeNotes = input.BeforeNotes;
            //meeting.DuringNotes = input.DuringNotes;
            //meeting.Date = input.Date;

           // MeetingsLocalEntities meetingContect = new DALCompact.MeetingsLocalEntities();
            LocalContext.Meeting.Add(compactMeeting);
            LocalContext.SaveChanges();

            var serverMeeting = Mapper.Map<DALServer.Meeting>(input);
            ServerContext.Meeting.Add(serverMeeting);
            ServerContext.SaveChanges();

        }
    }
}
