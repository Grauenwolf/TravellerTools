using System;
using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Characters
{
    class MilitaryAcademy : Career
    {
        public MilitaryAcademy(string type, string qualifyAttribute, int qualifyTarget) : base("MilitaryAcademy: " + type)
        {
            Type = type;
            QualifyTarget = qualifyTarget;
            QualifyAttribute = qualifyAttribute;
        }

        public override bool Qualify(Character character, Dice dice)
        {
            var dm = character.GetDM(QualifyAttribute);
            if (character.CurrentTerm == 2)
                dm -= 2;
            if (character.CurrentTerm == 3)
                dm -= 4;
            if (character.CurrentTerm > 3)
                dm = -100;

            return dice.RollHigh(QualifyTarget);
        }

        public override void Run(Character character, Dice dice)
        {
            throw new NotImplementedException();
        }

        public string QualifyAttribute { get; }
        public int QualifyTarget { get; }
        public string Type { get; }
    }
}
