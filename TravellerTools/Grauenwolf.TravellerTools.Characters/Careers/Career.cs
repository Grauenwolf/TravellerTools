using System.Collections.Generic;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers
{
    public abstract class CareerBase
    {
        internal CareerBase(string career, string assignment, Book book)
        {
            Book = book;
            Career = career;
            Assignment = assignment;
        }

        public string Assignment { get; }

        public string Career { get; }

        public string Key
        {
            get { return Assignment ?? Career; }
        }

        internal Book Book { get; }

        protected ImmutableArray<SkillTemplate> RandomSkills
        {
            get { return Book.RandomSkills; }
        }

        public override string ToString()
        {
            if (Assignment == null)
                return Career;
            else
                return $"{Assignment} ({Career})";
        }

        internal virtual decimal MedicalPaymentPercentage(Character character, Dice dice)
        {
            return 0;
        }

        internal abstract bool Qualify(Character character, Dice dice);

        internal abstract void Run(Character character, Dice dice);

        protected void Injury(Character character, Dice dice, bool severe = false) => Book.Injury(character, dice, this, severe);

        protected void LifeEvent(Character character, Dice dice) => Book.LifeEvent(character, dice, this);

        protected List<SkillTemplate> SpecialtiesFor(string skillName) => Book.SpecialtiesFor(skillName);

        protected void UnusualLifeEvent(Character character, Dice dice) => Book.UnusualLifeEvent(character, dice);
    }
}