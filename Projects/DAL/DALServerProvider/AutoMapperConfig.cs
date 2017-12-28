using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALServerProvider
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
                cfg.CreateMap<MeetingsDTO.Meeting, DALServer.Meeting>()
                .ForMember(dst => dst.BeforeNotesXaml, opt => opt.MapFrom(src => src.BeforeNotesXaml.Text))
                .ForMember(dst => dst.AfterNotesXaml, opt => opt.MapFrom(src => src.AfterNotesXaml.Text))
                .ForMember(dst => dst.DuringNotesXaml, opt => opt.MapFrom(src => src.DuringNotesXaml.Text));


                cfg.CreateMap<DALServer.Meeting, MeetingsDTO.Meeting>()
                .ForMember(dst => dst.BeforeNotesXaml, opt => opt.MapFrom(src => new MeetingsDTO.Notes() { Text = src.BeforeNotesXaml }))
                .ForMember(dst => dst.AfterNotesXaml, opt => opt.MapFrom(src => new MeetingsDTO.Notes() { Text = src.AfterNotesXaml }))
                .ForMember(dst => dst.DuringNotesXaml, opt => opt.MapFrom(src => new MeetingsDTO.Notes() { Text = src.DuringNotesXaml }));


                cfg.CreateMap<DALServer.Contact, MeetingsDTO.Contact>();

                cfg.CreateMap<MeetingsDTO.Contact, DALServer.Contact>();

            });
        }
    }
}
