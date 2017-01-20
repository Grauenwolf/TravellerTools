using Grauenwolf.TravellerTools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grauenwolf.TravellerTools.Characters.Careers
{
    delegate void SkillTable(Character character, Dice dice, int roll, bool level0);
    abstract class Drifter : NormalCareer
    {
        public Drifter(string assignment) : base("Drifter", assignment)
        {

        }

        protected override int AdvancedEductionMin
        {
            get { return int.MaxValue; }
        }

        public override bool Qualify(Character character, Dice dice)
        {
            return true;
        }

        protected override void AdvancedEducation(Character character, Dice dice, int roll, bool level0)
        {
            throw new NotImplementedException();
        }

        protected override void OfficerTraining(Character character, Dice dice, int roll, bool level0)
        {
            throw new NotImplementedException();
        }

        protected override void PersonalDevelopment(Character character, Dice dice, int roll, bool level0)
        {
            switch (roll)
            {
                case 1:
                    return;
                case 2:
                    return;
                case 3:
                    return;
                case 4:
                    return;
                case 5:
                    return;
                case 6:
                    return;
            }
        }

        protected override void ServiceSkill(Character character, Dice dice, int roll, bool level0)
        {
            switch (roll)
            {
                case 1:
                    return;
                case 2:
                    return;
                case 3:
                    return;
                case 4:
                    return;
                case 5:
                    return;
                case 6:
                    return;
            }
        }

        internal override void Event(Character character, Dice dice)
        {

        }

        internal override void Mishap(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    return;
                case 2:
                    return;
                case 3:
                    return;
                case 4:
                    return;
                case 5:
                    return;
                case 6:
                    return;
            }
        }
    }
}

