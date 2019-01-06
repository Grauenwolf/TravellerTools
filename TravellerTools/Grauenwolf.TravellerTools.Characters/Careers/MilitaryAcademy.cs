using System;
using System.Linq;

namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class MilitaryAcademy : Career
    {
        MilitaryCareer m_Stub;

        public MilitaryAcademy(string type, string qualifyAttribute, int qualifyTarget, Book book) : base("Military Academy", type, book)
        {
            Type = type;
            QualifyTarget = qualifyTarget;
            QualifyAttribute = qualifyAttribute;

            switch (type)
            {
                case "Naval Academy": m_Stub = new NavyStub(book); break;
                case "Army Academy": m_Stub = new ArmyStub(book); break;
                case "Marine Academy": m_Stub = new MarineStub(book); break;
                default: throw new ArgumentException("Unknown military academy type");
            }
        }

        public string QualifyAttribute { get; }

        public int QualifyTarget { get; }

        public string Type { get; }

        internal override bool Qualify(Character character, Dice dice)
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

        internal override void Run(Character character, Dice dice)
        {
            character.LongTermBenefits.MayEnrollInSchool = false;

            character.AddHistory($"Entered {Assignment} at age {character.Age}");
            m_Stub.BasicTrainingSkills(character, dice, true);

            var gradDM = character.IntellectDM;
            if (character.Endurance >= 8)
                gradDM += 1;
            if (character.SocialStanding >= 8)
                gradDM += 1;

            character.EducationHistory = new EducationHistory();
            character.EducationHistory.Name = Assignment;

            Book.PreCareerEvents(character, dice, this, character.Skills.Select(s => s.Name).ToArray());

            var graduation = dice.D(2, 6);
            if (graduation == 2)
            {
                character.AddHistory("Kicked out of military academy.");
                character.NextTermBenefits.EnlistmentDM[m_Stub.Name] = -100;
            }
            else
            {
                graduation += gradDM;
                if (graduation < 8)
                {
                    character.AddHistory("Dropped out of military academy.");
                    character.EducationHistory.Status = "Failed";
                    character.NextTermBenefits.EnlistmentDM[m_Stub.Name] = 100;
                    character.NextTermBenefits.CommissionDM = -100;
                }
                else
                {
                    if (graduation >= 11)
                    {
                        character.EducationHistory.Status = "Honors";
                        character.AddHistory($"Graduated with honors at age {character.Age}.");
                        character.SocialStanding += 1;
                        character.NextTermBenefits.FreeCommissionRoll = true;
                        character.NextTermBenefits.CommissionDM += 100;
                    }
                    else
                    {
                        character.EducationHistory.Status = "Graduated";
                        character.AddHistory($"Graduated at age {character.Age}.");
                        character.NextTermBenefits.CommissionDM += 2;
                    }
                    m_Stub.ServiceSkill(character, dice);
                    m_Stub.ServiceSkill(character, dice);
                    m_Stub.ServiceSkill(character, dice);
                    character.Education += 1;

                    character.NextTermBenefits.MustEnroll = m_Stub.Name;
                }
            }
        }

        class ArmyStub : Army
        {
            public ArmyStub(Book book) : base(null, book)
            {
            }

            protected override string AdvancementAttribute => throw new NotImplementedException();

            protected override int AdvancementTarget => throw new NotImplementedException();

            protected override string SurvivalAttribute => throw new NotImplementedException();

            protected override int SurvivalTarget => throw new NotImplementedException();

            internal override void AssignmentSkills(Character character, Dice dice)
            {
                throw new NotImplementedException();
            }
        }

        class MarineStub : Marine
        {
            public MarineStub(Book book) : base(null, book)
            {
            }

            protected override string AdvancementAttribute => throw new NotImplementedException();

            protected override int AdvancementTarget => throw new NotImplementedException();

            protected override string SurvivalAttribute => throw new NotImplementedException();

            protected override int SurvivalTarget => throw new NotImplementedException();

            internal override void AssignmentSkills(Character character, Dice dice)
            {
                throw new NotImplementedException();
            }
        }

        class NavyStub : Navy
        {
            public NavyStub(Book book) : base(null, book)
            {
            }

            protected override string AdvancementAttribute => throw new NotImplementedException();

            protected override int AdvancementTarget => throw new NotImplementedException();

            protected override string SurvivalAttribute => throw new NotImplementedException();

            protected override int SurvivalTarget => throw new NotImplementedException();

            internal override void AssignmentSkills(Character character, Dice dice)
            {
                throw new NotImplementedException();
            }
        }
    }
}