using Grauenwolf.TravellerTools.Characters.Careers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Grauenwolf.TravellerTools.Characters
{
    public class CharacterBuilder
    {
        ImmutableArray<Career> m_Careers;
        ImmutableArray<string> m_Personalities;


        public CharacterBuilder(string dataPath)
        {
            var file = new FileInfo(Path.Combine(dataPath, "CharacterBuilder.xml"));

            var converter = new XmlSerializer(typeof(CharacterTemplates));
            Book book;

            using (var stream = file.OpenRead())
                book = new Book(((CharacterTemplates)converter.Deserialize(stream)));


            var careers = new List<Career>();
            careers.Add(new MilitaryAcademy("Army Academy", "End", 8, book));
            careers.Add(new MilitaryAcademy("Marine Academy", "End", 9, book));
            careers.Add(new MilitaryAcademy("Naval Academy", "Int", 9, book));
            careers.Add(new ArmySupport(book));
            careers.Add(new Administrator(book));
            careers.Add(new Artist(book));
            careers.Add(new Barbarian(book));
            careers.Add(new Broker(book));
            careers.Add(new Cavalry(book));
            careers.Add(new Colonist(book));
            careers.Add(new Corporate(book));
            careers.Add(new CorporateAgent(book));
            careers.Add(new Enforcer(book));
            careers.Add(new Fixer(book));
            careers.Add(new FreeTrader(book));
            careers.Add(new GroundAssault(book));
            careers.Add(new Infantry(book));
            careers.Add(new Inmate(book));
            careers.Add(new Intelligence(book));
            careers.Add(new Journalist(book));
            careers.Add(new LawEnforcement(book));
            careers.Add(new MarineSupport(book));
            careers.Add(new MerchantMarine(book));
            careers.Add(new Performer(book));
            careers.Add(new Pirate(book));
            careers.Add(new Retired(book));
            careers.Add(new Scavenger(book));
            careers.Add(new StarMarine(book));
            careers.Add(new Thief(book));
            careers.Add(new Thug(book));
            careers.Add(new University(book));
            careers.Add(new Wanderer(book));
            careers.Add(new Worker(book));
            careers.Add(new Flight(book));
            careers.Add(new EngineerGunner(book));
            careers.Add(new LineCrew(book));
            careers.Add(new Dilettante(book));
            careers.Add(new Diplomat(book));
            careers.Add(new FieldResearcher(book));
            careers.Add(new Scientist(book));
            careers.Add(new Physician(book));
            careers.Add(new Courier(book));
            careers.Add(new Surveyor(book));
            careers.Add(new Explorer(book));

            m_Careers = careers.ToImmutableArray();

            var personalityFile = new FileInfo(Path.Combine(dataPath, "personality.txt"));
            m_Personalities = File.ReadAllLines(personalityFile.FullName).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToImmutableArray();

        }



        static void AgingRoll(Character character, Dice dice)
        {
            //TODO: Anagathics page 47

            var roll = dice.D(2, 6) - character.CurrentTerm;
            if (roll <= -6)
            {
                character.Strength += -2;
                character.Dexterity += -2;
                character.Endurance += -2;
                switch (dice.D(3))
                {
                    case 1: character.Intellect += -1; break;
                    case 2: character.Education += -1; break;
                    case 3: character.SocialStanding += -1; break;
                }
            }
            else if (roll == -5)
            {
                character.Strength += -2;
                character.Dexterity += -2;
                character.Endurance += -2;
            }
            else if (roll == -4)
            {
                switch (dice.D(3))
                {
                    case 1:
                        character.Strength += -2;
                        character.Dexterity += -2;
                        character.Endurance += -1;
                        break;
                    case 2:
                        character.Strength += -1;
                        character.Dexterity += -2;
                        character.Endurance += -2;
                        break;
                    case 3:
                        character.Strength += -2;
                        character.Dexterity += -1;
                        character.Endurance += -2;
                        break;
                }
            }
            else if (roll == -3)
            {
                switch (dice.D(3))
                {
                    case 1:
                        character.Strength += -2;
                        character.Dexterity += -1;
                        character.Endurance += -1;
                        break;
                    case 2:
                        character.Strength += -1;
                        character.Dexterity += -2;
                        character.Endurance += -1;
                        break;
                    case 3:
                        character.Strength += -1;
                        character.Dexterity += -1;
                        character.Endurance += -2;
                        break;
                }
            }
            else if (roll == -2)
            {
                character.Strength += -1;
                character.Dexterity += -1;
                character.Endurance += -1;
            }
            else if (roll == -1)
            {
                switch (dice.D(3))
                {
                    case 1:
                        character.Strength += -1;
                        character.Dexterity += -1;
                        break;
                    case 2:
                        character.Strength += -1;
                        character.Endurance += -1;
                        break;
                    case 3:
                        character.Dexterity += -1;
                        character.Endurance += -1;
                        break;
                }
            }
            else if (roll == 0)
            {
                switch (dice.D(3))
                {
                    case 1:
                        character.Strength += -1;
                        break;
                    case 2:
                        character.Dexterity += -1;
                        break;
                    case 3:
                        character.Endurance += -1;
                        break;
                }
            }
            else
            {
                return; //no again crisis possible.
            }

            if (character.Strength <= 0 ||
                    character.Dexterity <= 0 ||
                    character.Endurance <= 0 ||
                    character.Intellect <= 0 ||
                    character.Education <= 0 ||
                    character.SocialStanding <= 0)
            {
                var bills = dice.D(6) * 10000;
                character.Debt += bills;
                character.AddHistory($"Aging Crisis. Owe {bills:N0} for medical bills.");

                if (character.Strength < 1) character.Strength = 1;
                if (character.Dexterity < 1) character.Dexterity = 1;
                if (character.Endurance < 1) character.Endurance = 1;
                if (character.Intellect < 1) character.Intellect = 1;
                if (character.Education < 1) character.Education = 1;
                if (character.SocialStanding < 1) character.SocialStanding = 1;
                character.LongTermBenefits.QualificationDM = -100;
                character.LongTermBenefits.Retired = true;
            }
        }

        public Character Build(CharacterBuilderOptions options)
        {
            var seed = options.Seed ?? (new Random()).Next();
            var dice = new Dice(seed);
            var character = new Character();

            character.Seed = seed;
            character.FirstCareer = options.FirstCareer;
            character.Name = options.Name;

            character.Strength = dice.D(2, 6);
            character.Dexterity = dice.D(2, 6);
            character.Endurance = dice.D(2, 6);
            character.Intellect = dice.D(2, 6);
            character.Education = dice.D(2, 6);
            character.SocialStanding = dice.D(2, 6);

            if (character.EducationDM + 3 > 0)
            {
                var backgroundSKills = dice.Choose(m_BackgroundSkills, character.EducationDM + 3, allowDuplicates: false);
                foreach (var skill in backgroundSKills)
                    character.Skills.Add(skill); //all skills added at level 0
            }

            character.CurrentTerm = 1;
            character.NextTermBenefits = new NextTermBenefits();

            if (!string.IsNullOrEmpty(options.FirstCareer))
                character.NextTermBenefits.MustEnroll = options.FirstCareer;

            while (!IsDone(options, character))
            {
                var nextCareer = PickNextCareer(character, dice);
                character.CurrentTermBenefits = character.NextTermBenefits;
                character.NextTermBenefits = new NextTermBenefits();
                nextCareer.Run(character, dice);

                character.CurrentTerm += 1;

                if (character.CurrentTerm >= 4)
                    AgingRoll(character, dice);
            }

            //Add personality
            int personalityTraits = dice.D(3);
            for (var i = 0; i < personalityTraits; i++)
                character.Personality.Add(dice.Choose(m_Personalities));


            //Fixups
            if (options.MaxAge.HasValue && !character.IsDead)
                character.Age = options.MaxAge.Value;

            character.Title = character.CareerHistory.Where(c => c.Title != null).OrderByDescending(c => c.Rank + c.CommissionRank).Select(c => c.Title).FirstOrDefault();

            character.Skills.Collapse();

            return character;
        }

        Career PickNextCareer(Character character, Dice dice)
        {
            var careers = new List<Career>();

            //Forced picks (e.g. Draft)
            if (character.NextTermBenefits.MustEnroll != null)
            {
                foreach (var career in m_Careers)
                {
                    if (string.Equals(character.NextTermBenefits.MustEnroll, career.Name, StringComparison.OrdinalIgnoreCase) || string.Equals(character.NextTermBenefits.MustEnroll, career.Assignment, StringComparison.OrdinalIgnoreCase))
                    {
                        careers.Add(career);
                    }
                }
            }

            if (!character.NextTermBenefits.MusterOut && careers.Count == 0 && character.LastCareer != null)
            {
                if (dice.D(10) > 1) //continue career
                {
                    foreach (var career in m_Careers)
                    {
                        if (character.LastCareer.Name == career.Name && character.LastCareer.Assignment == career.Assignment)
                        {
                            careers.Add(career);
                        }
                    }
                }
                else
                {
                    character.NextTermBenefits.MusterOut = true;
                    character.AddHistory("Voluntarily left " + character.LastCareer.ShortName);
                }
            }

            //Random picks
            if (careers.Count == 0)
            {
                foreach (var career in m_Careers)
                {
                    if (character.NextTermBenefits.MusterOut && character.LastCareer.Name == career.Name && character.LastCareer.Assignment == career.Assignment)
                        continue;

                    if (career.Qualify(character, dice))
                    {
                        careers.Add(career);
                        character.Trace.Add("Qualified for " + career.Name + " at age " + character.Age);
                    }
                }
            }

            var result = dice.Choose(careers);
            character.Trace.Add("Selected " + result.Name + " at age " + character.Age);
            return result;
        }




        static bool IsDone(CharacterBuilderOptions options, Character character)
        {

            if (character.Strength <= 0 ||
                character.Dexterity <= 0 ||
                character.Endurance <= 0 ||
                character.Intellect <= 0 ||
                character.Education <= 0 ||
                character.SocialStanding <= 0)
            {
                character.AddHistory($"Died at age {character.Age}");
                character.IsDead = true;
                return true;

            }

            if ((character.Age + 3) >= options.MaxAge) //+3 because terms are 4 years long
                return true;

            return false;
        }

        static readonly ImmutableList<string> m_BackgroundSkills = ImmutableList.Create(
            "Admin",
            "Animals",
            "Art",
            "Athletics",
            "Carouse",
            "Drive",
            "Science",
            "Seafarer",
            "Streetwise",
            "Survival",
            "Vacc Suit",
            "Electronics",
            "Flyer",
            "Language",
            "Mechanic",
            "Medic",
            "Profession");

        public ImmutableArray<Career> Careers
        {
            get { return m_Careers; }
        }

    }
}
