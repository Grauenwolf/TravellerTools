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

		Book m_Book;
		ImmutableArray<string> m_Personalities;

		public CharacterBuilder(string dataPath)
		{
			var file = new FileInfo(Path.Combine(dataPath, "CharacterBuilder.xml"));

			var converter = new XmlSerializer(typeof(CharacterTemplates));

			using (var stream = file.OpenRead())
				m_Book = new Book(((CharacterTemplates)converter.Deserialize(stream)));

			var careers = new List<Career>();
			careers.Add(new MilitaryAcademy("Army Academy", "End", 8, m_Book));
			careers.Add(new MilitaryAcademy("Marine Academy", "End", 9, m_Book));
			careers.Add(new MilitaryAcademy("Naval Academy", "Int", 9, m_Book));
			careers.Add(new ArmySupport(m_Book));
			careers.Add(new Administrator(m_Book));
			careers.Add(new Artist(m_Book));
			careers.Add(new Barbarian(m_Book));
			careers.Add(new Broker(m_Book));
			careers.Add(new Cavalry(m_Book));
			careers.Add(new Colonist(m_Book));
			careers.Add(new Corporate(m_Book));
			careers.Add(new CorporateAgent(m_Book));
			careers.Add(new Enforcer(m_Book));
			careers.Add(new Fixer(m_Book));
			careers.Add(new FreeTrader(m_Book));
			careers.Add(new GroundAssault(m_Book));
			careers.Add(new Infantry(m_Book));
			careers.Add(new Inmate(m_Book));
			careers.Add(new Intelligence(m_Book));
			careers.Add(new Journalist(m_Book));
			careers.Add(new LawEnforcement(m_Book));
			careers.Add(new MarineSupport(m_Book));
			careers.Add(new MerchantMarine(m_Book));
			careers.Add(new Performer(m_Book));
			careers.Add(new Pirate(m_Book));
			careers.Add(new Retired(m_Book));
			careers.Add(new Scavenger(m_Book));
			careers.Add(new StarMarine(m_Book));
			careers.Add(new Thief(m_Book));
			careers.Add(new Thug(m_Book));
			careers.Add(new University(m_Book));
			careers.Add(new Wanderer(m_Book));
			careers.Add(new Worker(m_Book));
			careers.Add(new Flight(m_Book));
			careers.Add(new EngineerGunner(m_Book));
			careers.Add(new LineCrew(m_Book));
			careers.Add(new Dilettante(m_Book));
			careers.Add(new Diplomat(m_Book));
			careers.Add(new FieldResearcher(m_Book));
			careers.Add(new Scientist(m_Book));
			careers.Add(new Physician(m_Book));
			careers.Add(new Courier(m_Book));
			careers.Add(new Surveyor(m_Book));
			careers.Add(new Explorer(m_Book));
			careers.Add(new WildTalent(m_Book));
			careers.Add(new Adept(m_Book));
			careers.Add(new PsiWarrrior(m_Book));

			Careers = careers.ToImmutableArray();

			var personalityFile = new FileInfo(Path.Combine(dataPath, "personality.txt"));
			m_Personalities = File.ReadAllLines(personalityFile.FullName).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToImmutableArray();
		}

		public ImmutableArray<Career> Careers { get; }

		public Character Build(CharacterBuilderOptions options)
		{
			var seed = options.Seed ?? (new Random()).Next();
			var dice = new Dice(seed);
			var character = new Character();

			character.Seed = seed;
			character.FirstCareer = options.FirstCareer;
			character.Name = options.Name;
			character.Gender = options.Gender;

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

				if (character.LongTermBenefits.MayTestPsi && dice.RollHigh(10))
					m_Book.TestPsionic(character, dice);

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

		Career PickNextCareer(Character character, Dice dice)
		{
			var careers = new List<Career>();

			//Forced picks (e.g. Draft)
			if (character.NextTermBenefits.MustEnroll != null)
			{
				foreach (var career in Careers)
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
					foreach (var career in Careers)
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
				foreach (var career in Careers)
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
	}
}