namespace Grauenwolf.TravellerTools.Equipment
{
    public class Item
    {
        public string Name { get; set; }

        public int TechLevel { get; set; }

        public decimal Price { get; set; }

        public int Law { get; set; }

        public int Category { get; set; }

        public string Mod { get; set; }

        public string Book { get; set; }

        public int Availability { get; set; }
        public bool BlackMarket { get; set; }
        public int? SentencingDM { get; set; }

        public string Mass { get; set; }
        public decimal AmmoPrice { get; set; }
        public string Skill { get; set; }
        public string Page { get; set; }


        public bool NotAvailable { get; set; }

        public string BookAndPage => string.IsNullOrEmpty(Page) ? Book : $"{Book} ({Page})";
    }
}
