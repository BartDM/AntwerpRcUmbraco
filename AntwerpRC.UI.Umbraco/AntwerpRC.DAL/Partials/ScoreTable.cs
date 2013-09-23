using System;
using System.Linq;

namespace AntwerpRC.DAL
{
    public partial class ScoreTable
    {
        protected bool Equals(ScoreTable other)
        {
            try
            {
                var linesToCompare = other.ScoreTableLine.ToList();

                if (linesToCompare.Count() != ScoreTableLine.Count())
                    return false;
                bool equal = true;
                foreach (var scoreTableLine in ScoreTableLine)
                {
                    var compareLine = linesToCompare.First(t => t.TeamclubId == scoreTableLine.TeamclubId);
                    equal &=
                        scoreTableLine.Equals(compareLine);
                }

                return TeamId == other.TeamId && equal;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            //if (obj.GetType() != this.GetType()) return false;
            return Equals((ScoreTable)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (TeamId.GetHashCode() * 397) ^ (ScoreTableLine != null ? ScoreTableLine.GetHashCode() : 0);
            }
        }

        public static bool operator ==(ScoreTable left, ScoreTable right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ScoreTable left, ScoreTable right)
        {
            return !Equals(left, right);
        }
    }
}
