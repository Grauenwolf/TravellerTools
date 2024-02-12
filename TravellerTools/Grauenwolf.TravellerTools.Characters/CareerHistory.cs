namespace Grauenwolf.TravellerTools.Characters;

public class CareerHistory(int firstTermAge, string career, string? assignment, CareerType careerTypes, int rank, int commissionRank = 0)
{
    public string? Assignment { get; } = assignment;
    public string Career { get; } = career;
    public CareerType CareerTypes { get; } = careerTypes;
    public int CommissionRank { get; set; } = commissionRank;
    public int FirstTermAge { get; } = firstTermAge;
    public int LastTermAge { get; set; } = firstTermAge;

    public string LongName
    {
        get
        {
            if (Assignment == null)
                return Career;
            else
                return $"{Career} ({Assignment})";
        }
    }

    public int Rank { get; set; } = rank;

    public string ShortName => Assignment ?? Career;
    public int Terms { get; set; }
    public string? Title { get; set; }
}
