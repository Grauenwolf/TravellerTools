namespace Grauenwolf.TravellerTools.Maps
{
    public class Subsector
    {
        public string? Name { get; set; }
        public string? Index { get; set; }
        public int IndexNumber { get; set; }

        public string SafeName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                    return Name;
                if (!string.IsNullOrWhiteSpace(Index))
                    return Index;
                return IndexNumber.ToString();
            }
        }
    }
}
