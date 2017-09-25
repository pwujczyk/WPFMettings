using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALDataProvider
{
    public static class AutoMapperConfig
    {
        static AutoMapper.MapperConfiguration configuration;
        public static AutoMapper.MapperConfiguration Configuration
        {
            get
            {
                if (configuration == null)
                {
                    Configure();
                }
                return configuration;
            }
        }

        public static void Configure()
        {
            configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MeetingsDTO.Meeting, DALCompact.Meeting>();
                cfg.CreateMap<MeetingsDTO.Meeting, DALServer.Meeting>();

                cfg.CreateMap<DALServer.Contact, MeetingsDTO.Contact>();

            });
        }
    }
}
