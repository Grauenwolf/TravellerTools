using System;

namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class MilitaryAcademy : Career
    {
        public MilitaryAcademy(string type, string qualifyAttribute, int qualifyTarget) : base("MilitaryAcademy", type)
        {
            Type = type;
            QualifyTarget = qualifyTarget;
            QualifyAttribute = qualifyAttribute;
        }

        public string QualifyAttribute { get; }

        public int QualifyTarget { get; }

        public string Type { get; }

        public override bool Qualify(Character character, Dice dice)
        {
            if (!character.LongTermBenefits.MayEnrollInSchool)
                return false;
            if (character.CurrentTerm > 3)
                return false;

            var dm = character.GetDM(QualifyAttribute);
            if (character.CurrentTerm == 2)
                dm += -2;
            if (character.CurrentTerm == 3)
                dm += -4;

            return dice.RollHigh(QualifyTarget);
        }

        public override void Run(Character character, Dice dice)
        {
            throw new NotImplementedException();
        }
    }
}
