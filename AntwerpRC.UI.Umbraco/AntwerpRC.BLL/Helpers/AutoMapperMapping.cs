using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace AntwerpRC.BLL.Helpers
{
    public class AutoMapperMapping
    {
        public static void CreateMappings()
        {
            Mapper.CreateMap<DAL.ScoreTableLine, BDO.ScoreTableLine>().ForMember(dest=>dest.TeamName, map=>map.MapFrom(src=>src.TeamClub.Club.ClubName));
            Mapper.CreateMap<DAL.Division, BDO.Division>();
            Mapper.CreateMap<DAL.Season, BDO.Season>();
            Mapper.CreateMap<DAL.Category, BDO.Category>();
            Mapper.CreateMap<DAL.Team, BDO.Team>();
            Mapper.CreateMap<DAL.TeamClub, BDO.Team>().
                ForMember(dest=>dest.Category, map=>map.MapFrom(src=>src.Team.Category)).
                ForMember(dest=>dest.Season, map=>map.MapFrom(src=>src.Team.Season)).
                ForMember(dest=>dest.Division, map=>map.MapFrom(src=>src.Team.Division));
            Mapper.CreateMap<DAL.Address, BDO.Address>();
            Mapper.CreateMap<DAL.Club, BDO.Club>().
                ForMember(dest=>dest.Addresses, map=>map.MapFrom(src=>src.Address));
            Mapper.CreateMap<DAL.ScoreTable, BDO.ScoreTable>().
                ForMember(dest=>dest.ScoreTableLines, map=>map.MapFrom(src=>src.ScoreTableLine)).
                ForMember(dest=>dest.Team, map=>map.MapFrom(src=>src.Team));
            Mapper.CreateMap<BDO.TestMapper, BDO.TestMapperBis>();
            Mapper.AssertConfigurationIsValid();

        }
    }
}
