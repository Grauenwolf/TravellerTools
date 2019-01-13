namespace Grauenwolf.TravellerTools.Characters
{
    public class CharacterBuilderOptions
    {
        //public CareerChoice CareerChoice(string career)
        //{
        //    if (m_CareerChoices.ContainsKey(career))
        //        return m_CareerChoices[career];
        //    else
        //        return Characters.CareerChoice.Default;
        //}
        public string FirstAssignment { get; set; }

        public string Gender { get; set; }
        public int? MaxAge { get; set; }
        public string Name { get; set; }

        //readonly Dictionary<string, CareerChoice> m_CareerChoices = new Dictionary<string, CareerChoice>(StringComparer.OrdinalIgnoreCase);
        public int? Seed { get; set; }
    }

    //public enum CareerChoice
    //{
    //    Disabled = -1,
    //    Default = 0,
    //    Preferred = 1,
    //    Forced = 2
    //}
}