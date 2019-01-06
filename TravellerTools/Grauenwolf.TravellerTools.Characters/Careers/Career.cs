using System.Collections.Generic;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers
{
    public abstract class Career
    {
        internal Career(string name, string assignment, Book book)
        {
            Book = book;
            Name = name;
            Assignment = assignment;
        }

        public string Assignment { get; }

        public string Key
        {
            get { return Assignment ?? Name; }
        }

        public string Name { get; }
        internal Book Book { get; }

        protected ImmutableArray<SkillTemplate> RandomSkills
        {
            get { return Book.RandomSkills; }
        }

        public override string ToString()
        {
            if (Assignment == null)
                return Name;
            else
                return $"{Assignment} ({Name})";
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