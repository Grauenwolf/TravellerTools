namespace Grauenwolf.TravellerTools.Names;

public class RandomPerson
{
    internal RandomPerson(string first, string last, string gender)
    {
        LastName = last.Substring(0, 1).ToUpper() + last.Substring(1); ;
        FirstName = first.Substring(0, 1).ToUpper() + first.Substring(1); ;
        Gender = gender;
    }

    public string FirstName { get; set; }
    public string FullName => FirstName + " " + LastName;
    public string Gender { get; set; }
    public string LastName { get; set; }
}