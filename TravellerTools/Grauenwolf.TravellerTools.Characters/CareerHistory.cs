namespace Grauenwolf.TravellerTools.Characters
{
    public class CareerHistory
    {
        public CareerHistory(string name, string assignment, int rank, int commissionRank = 0)
        {
            CommissionRank = commissionRank;
            Rank = rank;
            Name = name;
            Assignment = assignment;
        }

        public string Assignment { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public int CommissionRank { get; set; }
        public int Terms { get; set; }
    }
}
