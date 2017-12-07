using System;

namespace Grauenwolf.TravellerTools.Characters
{
    public class Person
    {
        public string Title { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }
        public int ApparentAge { get; set; }
        public string Trait { get; set; }

        public EHex Strength { get; set; }
        public EHex Dexterity { get; set; }
        public EHex Endurance { get; set; }
        public EHex Intellect { get; set; }
        public EHex Education { get; set; }
        public EHex Social { get; set; }

        public int StrengthDM => DMCalc(Strength);
        public int DexterityDM => DMCalc(Dexterity);
        public int EnduranceDM => DMCalc(Endurance);
        public int IntellectDM => DMCalc(Intellect);
        public int EducationDM => DMCalc(Education);
        public int SocialDM => DMCalc(Social);

        static int DMCalc(EHex characteristic)
        {
            var value = characteristic.Value;
            if (value == 0)
                return -3;
            if (value <= 2)
                return -2;
            if (value <= 5)
                return -1;
            if (value <= 8)
                return 0;
            if (value <= 11)
                return 1;
            if (value <= 14)
                return 2;
            if (value <= 15)
                return 3;
            throw new ArgumentOutOfRangeException("characteristic", characteristic, "characteristic must be between 0 and 15");
        }

        public bool IsPatron { get; set; }

        ///// <summary>
        ///// Gets or sets a value indicating whether the character has experienced an aging crisis. If true, the character can no long qualify for new careers.
        ///// </summary>
        ///// <value><c>true</c> if [aging crisis]; otherwise, <c>false</c>.</value>
        //public bool AgingCrisis { get; set; }

        public string Upp
        {
            get { return Strength.ToString() + Dexterity.ToString() + Endurance.ToString() + Intellect.ToString() + Education.ToString() + Social.ToString(); }
        }
    }
}
