namespace Grauenwolf.TravellerTools.Characters
{
    public class CareerHistory
    {
        public CareerHistory(string career, string assignment, int rank, int commissionRank = 0)
        {
            CommissionRank = commissionRank;
            Rank = rank;
            Career = career;
            Assignment = assignment;
        }

        public string Assignment { get; set; }
        public string Career { get; set; }
        public int CommissionRank { get; set; }
        public int Rank { get; set; }

        public string ShortName
        {
            get { return Assignment ?? Career; }
        }

        public int Terms { get; set; }
        public string Title { get; set; }
    }
}