using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using AntwerpRC.DAL;
using AntwerpRC.Score.Fetcher.Helper;
using System.Linq;

namespace AntwerpRC.Score.Fetcher
{
    internal class TeamFetcher
    {
        private readonly BrowserSession browserSession;

        public TeamFetcher()
        {
            browserSession = new BrowserSession();
        }

        internal void FetchTeamsForSeason(long seasonId, string user)
        {
            using (var context = new DAL.AntwerpRCEntities())
            {
                var season = context.Season.FirstOrDefault(s => s.SeasonId == seasonId && !s.AuditDeleted);
                if (season != null)
                {
                    if (!string.IsNullOrEmpty(season.CompetitionBaseUrl))
                        Initialize(season.CompetitionBaseUrl);

                    LoadTeamsPerCategory(season.Team.ToList(), context, user);

                }
                else
                {
                    throw new Exception("Season doesn't exist, no teams can be fetched");
                }

            }
        }

        private void Initialize(string baseUrl = null)
        {
            if (!string.IsNullOrEmpty(baseUrl))
            {
                browserSession.GetDoc(baseUrl);
            }
        }

        private void LoadTeamsPerCategory(List<DAL.Team> teams, DAL.AntwerpRCEntities context, string user)
        {
            foreach (var team in teams)
            {
                var doc = browserSession.GetDoc(team.CompetitionUrl);
                var table = doc.DocumentNode.Descendants("table")
                       .FirstOrDefault(
                           t => t.Descendants("tr").Count() > 1 &&
                               t.Descendants("tr").Skip(1).FirstOrDefault() != null &&
                               t.Descendants("tr").Skip(1).First().Attributes.Contains("class") &&
                               t.Descendants("tr").Skip(1).First().Attributes["class"].Value.Contains("comp_title"));
                if (table != null)
                {
                    int skip = 2;
                    var rows = table.Descendants("tr").Skip(skip).ToList();
                    if (rows.Any())
                    {
                        foreach (var row in rows)
                        {
                            var clubElement = row.Descendants("td").Skip(1).Take(1).FirstOrDefault();
                            if (clubElement != null)
                            {
                                var clubName = clubElement.InnerText.CleanHtml();
                                Console.WriteLine("Club {0} found in table", clubName);
                                var existingClub =
                                    context.Club.FirstOrDefault(
                                        c =>
                                            c.ClubName == clubName ||
                                            c.ClubAlias.Any(a => a.Alias == clubName));
                                if (existingClub != null)
                                {
                                    if (existingClub.TeamClub.All(tc => tc.TeamId != team.TeamId))
                                        team.TeamClub.Add(new TeamClub() { Club = existingClub, AuditCreatedBy = user, AuditCreatedOn = DateTime.Now });

                                }
                                else
                                {
                                    team.TeamClub.Add(new TeamClub() { Club = CreateClub(clubName, user), AuditCreatedBy = user, AuditCreatedOn = DateTime.Now });
                                }
                            }
                        }
                        context.SaveChanges();
                    }
                }
            }
        }

        private DAL.Club CreateClub(string clubName, string user)
        {
            return new DAL.Club()
            {
                ClubName = clubName,
                HomeClub = false,
                AuditCreatedBy = user,
                AuditCreatedOn = DateTime.Now
            };
        }
    }
}
