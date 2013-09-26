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
            Mapper.CreateMap<DAL.ScoreTableLine, BDO.ScoreTableLine>().
                ForMember(dest=>dest.TeamName, map=>map.MapFrom(src=>src.TeamClub.Club.ClubName)).
                ForMember(dest=>dest.HomeTeam, map=>map.MapFrom(src=>src.TeamClub.Club.HomeClub));
            Mapper.CreateMap<DAL.Division, BDO.Division>();
            Mapper.CreateMap<DAL.Season, BDO.Season>();
            Mapper.CreateMap<DAL.Category, BDO.Category>();
            Mapper.CreateMap<DAL.Team, BDO.Team>().
                ForMember(dest=>dest.Category, map=>map.MapFrom(src=>src.Category)).
                ForMember(dest=>dest.Season, map=>map.MapFrom(src=>src.Season)).
                ForMember(dest=>dest.Division, map=>map.MapFrom(src=>src.Division));
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

            Mapper.CreateMap<DAL.Game, BDO.GameResult>().
                ForMember(dest => dest.Team1Name, map => map.MapFrom(src => src.TeamClub.Club.ClubName)).
                ForMember(dest => dest.Team2Name, map => map.MapFrom(src => src.TeamClub1.Club.ClubName)).
                ForMember(dest => dest.HomeTeam,
                    map =>
                        map.MapFrom(
                            src => src.TeamClub.Club.HomeClub ? BDO.GameResult.Teams.Team1 : BDO.GameResult.Teams.Team2))
                .
                ForMember(dest => dest.Team1Score, map => map.MapFrom(src => src.TeamClub1Score)).
                ForMember(dest => dest.Team2Score, map => map.MapFrom(src => src.TeamClub2Score)).
                ForMember(dest => dest.Team1Tries, map => map.MapFrom(src => src.TeamClub1Tries)).
                ForMember(dest => dest.Team2Tries, map => map.MapFrom(src => src.TeamClub2Tries)).
                ForMember(dest => dest.Team, opt => opt.Ignore()).
                ForMember(dest => dest.Bonus, opt => opt.Ignore()).
                ForMember(dest => dest.TeamUrl, opt => opt.Ignore()).
                ForMember(dest=>dest.Winner, opt=>opt.Ignore());
            


            Mapper.AssertConfigurationIsValid();

        }
    }
}
