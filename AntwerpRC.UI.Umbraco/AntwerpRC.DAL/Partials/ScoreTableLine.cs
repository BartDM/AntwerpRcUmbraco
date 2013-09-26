using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntwerpRC.DAL
{
    public partial class ScoreTableLine
    {
        protected bool Equals(ScoreTableLine other)
        {
            var a = Points == other.Points &&
                    Bonus == other.Bonus &&
                    TotalPoints == other.TotalPoints &&
                    Games == other.Games &&
                    Won == other.Won &&
                    Draw == other.Draw &&
                    Lost == other.Lost &&
                    Forfait == other.Forfait &&
                    PositivePoints == other.PositivePoints &&
                    NegativePoints == other.NegativePoints &&
                    RedCards == other.RedCards &&
                    TeamclubId == other.TeamclubId &&
                    //Equals(TeamClub, other.TeamClub) &&
                    AuditDeleted.Equals(other.AuditDeleted) &&
                    Order == other.Order &&
                    InternalOrder == other.InternalOrder;
            return a;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            //if (obj.GetType() != this.GetType()) return false;
            return Equals((ScoreTableLine) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = TableId.GetHashCode();
                hashCode = (hashCode*397) ^ Points;
                hashCode = (hashCode*397) ^ Bonus;
                hashCode = (hashCode*397) ^ TotalPoints;
                hashCode = (hashCode*397) ^ Games;
                hashCode = (hashCode*397) ^ Won;
                hashCode = (hashCode*397) ^ Draw;
                hashCode = (hashCode*397) ^ Lost;
                hashCode = (hashCode*397) ^ Forfait;
                hashCode = (hashCode*397) ^ PositivePoints;
                hashCode = (hashCode*397) ^ NegativePoints;
                hashCode = (hashCode*397) ^ RedCards;
                hashCode = (hashCode*397) ^ TeamclubId.GetHashCode();
                hashCode = (hashCode*397) ^ (TeamClub != null ? TeamClub.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ AuditDeleted.GetHashCode();
                hashCode = (hashCode*397) ^ Order.GetHashCode();
                hashCode = (hashCode*397) ^ InternalOrder.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(ScoreTableLine left, ScoreTableLine right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ScoreTableLine left, ScoreTableLine right)
        {
            return !Equals(left, right);
        }
    }
}
