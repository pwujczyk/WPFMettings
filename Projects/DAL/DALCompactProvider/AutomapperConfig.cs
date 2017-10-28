using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALCompactProvider
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

                cfg.CreateMap<DALCompact.Meeting, MeetingsDTO.Meeting>();
                
                cfg.CreateMap<DALCompact.Contact, MeetingsDTO.Contact>();

                cfg.CreateMap<MeetingsDTO.Contact, DALCompact.Contact>();

            });
        }
    }
}
