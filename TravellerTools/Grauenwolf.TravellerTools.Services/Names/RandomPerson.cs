
namespace Grauenwolf.TravellerTools.Names
{
    public class RandomPerson
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomName"/> class.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        internal RandomPerson(Result user)
        {
            FirstName = user.name.first.Substring(0, 1).ToUpper() + user.name.first.Substring(1);
            LastName = user.name.last.Substring(0, 1).ToUpper() + user.name.last.Substring(1);
            Gender = user.gender == "female" ? "F" : "M";
        }

        internal RandomPerson(string first, string last, string gender)
        {
            LastName = last.Substring(0, 1).ToUpper() + last.Substring(1); ;
            FirstName = first.Substring(0, 1).ToUpper() + first.Substring(1); ;
            Gender = gender;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }

        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
