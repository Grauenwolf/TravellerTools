namespace Grauenwolf.TravellerTools.Equipment
{
    public class Item
    {
        public string Name { get; set; }

        public int TechLevel { get; set; }

        public int Price { get; set; }

        public int Law { get; set; }

        public int Category { get; set; }

        public string Mod { get; set; }

        public string Book { get; set; }

        public int Availability { get; set; }
        public bool BlackMarket { get; set; }
        public int? SentencingDM { get; set; }

    }
}
