namespace Grauenwolf.TravellerTools.Characters.Careers
{
	abstract class Marine : MilitaryCareer
	{
		public Marine(string assignment, Book book) : base("Marine", assignment, book)
		{
		}

		protected override int AdvancedEductionMin => 8;

		internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
		{
			var roll = dice.D(6);

			if (all || roll == 1)
				character.Skills.Add(dice.Choose(SpecialtiesFor("Athletics")));
			if (all || roll == 2)
				character.Skills.Add("Vacc Suit");
			if (all || roll == 3)
				character.Skills.Add(dice.Choose(SpecialtiesFor("Tactics")));
			if (all || roll == 4)
				character.Skills.Add(dice.Choose(SpecialtiesFor("Heavy Weapons")));
			if (all || roll == 5)
				character.Skills.Add(dice.Choose(SpecialtiesFor("Gun Combat")));
			if (all || roll == 6)
				character.Skills.Add("Stealth");
		}

		internal override void Event(Character character, Dice dice)
		{
			switch (dice.D(2, 6))
			{
				case 2:
					Mishap(character, dice);
					character.NextTermBenefits.MusterOut = false;
					return;

				case 3:
					character.AddHistory("Trapped behind enemy lines.");
					{
						var skillList = new SkillTemplateCollection();
						skillList.Add("Survival");
						skillList.Add("Stealth");
						skillList.Add("Deception");
						skillList.Add("Streetwise");
						skillList.RemoveOverlap(character.Skills, 1);
						if (skillList.Count > 0)
							character.Skills.Add(dice.Choose(skillList), 1);
					}
					return;

				case 4:
					character.AddHistory("Assigned to the security staff of a space station.");
					{
						var skillList = new SkillTemplateCollection();
						skillList.Add("Vacc Suit");
						skillList.Add("Athletics", "Dexterity");
						character.Skills.Increase(dice.Choose(skillList));
					}

					return;

				case 5:
					character.AddHistory($"Advanced training in a specialist field");
					if (dice.RollHigh(character.EducationDM, 8))
					{
						var skillList = new SkillTemplateCollection();
						skillList.AddRange(RandomSkills);
						skillList.RemoveOverlap(character.Skills, 1);
						if (skillList.Count > 0)
							character.Skills.Add(dice.Choose(skillList), 1);
					}
					return;

				case 6:
					character.AddHistory("Assigned to an assault on an enemy fortress.");
					if (dice.RollHigh(character.Skills.BestSkillLevel("Gun Combat", "Melee"), 8))
					{
						var skillList = new SkillTemplateCollection();
						skillList.Add("Tactics", "Military");
						skillList.Add("Leadership");
						character.Skills.Increase(dice.Choose(skillList));
					}
					else
					{
						character.AddHistory("Injured");
						switch (dice.D(3))
						{
							case 1:
								character.Strength -= 1;
								return;

							case 2:
								character.Dexterity -= 1;
								return;

							case 3:
								character.Endurance -= 1;
								return;
						}
					}
					return;

				case 7:
					LifeEvent(character, dice);
					return;

				case 8:
					character.AddHistory("On the front lines of a planetary assault and occupation.");
					{
						var skillList = new SkillTemplateCollection();
						skillList.Add("Recon");
						skillList.AddRange(SpecialtiesFor("Gun Combat"));
						skillList.Add("Leadership");
						skillList.Add("Electronics", "Comms");
						skillList.RemoveOverlap(character.Skills, 1);
						if (skillList.Count > 0)
							character.Skills.Add(dice.Choose(skillList), 1);
					}
					return;

				case 9:
					character.AddHistory("A mission goes disastrously wrong due to your commander’s error or incompetence, but you survive.");
					if (dice.D(2) == 1)
					{
						character.AddHistory("Report commander and gain an Enemy.");
						character.CurrentTermBenefits.AdvancementDM += 2;
					}
					else
					{
						character.AddHistory("Cover for the commander and gain an Ally.");
					}
					return;

				case 10:
					character.AddHistory("Assigned to a black ops mission.");
					character.CurrentTermBenefits.AdvancementDM += 2;

					return;

				case 11:
					character.AddHistory("Commanding officer takes an interest in your career.");
					switch (dice.D(2))
					{
						case 1:
							character.Skills.Add("Tactics", "Military", 1);
							return;

						case 2:
							character.CurrentTermBenefits.AdvancementDM += 4;
							return;
					}
					return;

				case 12:
					character.AddHistory("Display heroism in battle.");

					character.CurrentTermBenefits.AdvancementDM += 100; //also applies to commission rolls

					if (character.LastCareer.CommissionRank == 0)
						character.CurrentTermBenefits.FreeCommissionRoll = true;

					return;
			}
		}

		internal override void Mishap(Character character, Dice dice)
		{
			switch (dice.D(6))
			{
				case 1:
					Injury(character, dice, true);
					return;

				case 2:
					character.AddHistory("A mission goes wrong; you and several others are captured and mistreated by the enemy. Gain your jailer as an Enemy.");
					character.Strength += -1;
					character.Dexterity += -1;
					return;

				case 3:
					character.AddHistory("A mission goes wrong and you are stranded behind enemy lines. Ejected from the service.");
					{
						var skillList = new SkillTemplateCollection();
						skillList.Add("Stealth");
						skillList.Add("Survival");
						character.Skills.Increase(dice.Choose(skillList));
					}
					return;

				case 4:
					if (dice.D(2) == 1)
					{
						character.AddHistory("Refused to take part in a black ops mission that goes against the conscience and ejected from the service.");
					}
					else
					{
						character.AddHistory("You are ordered to take part in a black ops mission that goes against your conscience. Gain the lone survivor as an Enemy.");
						character.CurrentTermBenefits.MusterOut = false;
					}
					return;

				case 5:
					character.AddHistory("You are tormented by or quarrel with an officer or fellow soldier. Gain that officer as a Rival.");
					return;

				case 6:
					Injury(character, dice, false);
					return;
			}
		}

		internal override bool Qualify(Character character, Dice dice)
		{
			var dm = character.EnduranceDM;
			dm += -1 * character.CareerHistory.Count;
			if (character.Age >= 30)
				dm += -2;

			dm += character.GetEnlistmentBonus(Name, Assignment);

			return dice.RollHigh(dm, 6);
		}

		internal override void ServiceSkill(Character character, Dice dice)
		{
			switch (dice.D(6))
			{
				case 1:
					character.Skills.Increase(dice.Choose(SpecialtiesFor("Athletics")));
					return;

				case 2:
					character.Skills.Increase("Vacc Suit");
					return;

				case 3:
					character.Skills.Increase(dice.Choose(SpecialtiesFor("Tactics")));
					return;

				case 4:
					character.Skills.Increase(dice.Choose(SpecialtiesFor("Heavy Weapons")));
					return;

				case 5:
					character.Skills.Increase(dice.Choose(SpecialtiesFor("Gun Combat")));
					return;

				case 6:
					character.Skills.Increase("Stealth");
					return;
			}
		}

		internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
		{
			if (careerHistory.CommissionRank == 0)
			{
				switch (careerHistory.Rank)
				{
					case 0:
						careerHistory.Title = "Marine";
						{
							var skillList = new SkillTemplateCollection();
							skillList.AddRange(SpecialtiesFor("Gun Combat"));
							skillList.Add("Melee", "Blade");
							skillList.RemoveOverlap(character.Skills, 1);
							if (skillList.Count > 0)
								character.Skills.Add(dice.Choose(skillList), 1);
						}
						return;

					case 1:
						careerHistory.Title = "Lance Corporal";
						{
							var skillList = new SkillTemplateCollection();
							skillList.AddRange(SpecialtiesFor("Gun Combat"));
							skillList.RemoveOverlap(character.Skills, 1);
							if (skillList.Count > 0)
								character.Skills.Add(dice.Choose(skillList), 1);
						}
						return;

					case 2:
						careerHistory.Title = "Corporal";
						return;

					case 3:
						careerHistory.Title = "Lance Sergeant";
						character.Skills.Add("Leadership", 1);
						return;

					case 4:
						careerHistory.Title = "Sergeant";
						return;

					case 5:
						careerHistory.Title = "Gunnery Sergeant";
						character.Endurance += 1;
						return;

					case 6:
						careerHistory.Title = "Sergeant Major";
						return;
				}
			}
			else
			{
				switch (careerHistory.CommissionRank)
				{
					case 1:
						careerHistory.Title = "Lieutenant";
						character.Skills.Add("Leadership", 1);
						return;

					case 2:
						careerHistory.Title = "Captain";
						return;

					case 3:
						careerHistory.Title = "Force Commander";
						{
							var skillList = new SkillTemplateCollection();
							skillList.AddRange(SpecialtiesFor("Tatics"));
							skillList.RemoveOverlap(character.Skills, 1);
							if (skillList.Count > 0)
								character.Skills.Add(dice.Choose(skillList), 1);
						}
						return;

					case 4:
						careerHistory.Title = "Lieutenant Colonel";
						return;

					case 5:
						careerHistory.Title = "Colonel";
						if (character.SocialStanding < 10)
							character.SocialStanding = 10;
						else
							character.SocialStanding += 1;
						return;

					case 6:
						careerHistory.Title = "Brigadier";
						return;
				}
			}
		}

		protected override void AdvancedEducation(Character character, Dice dice)
		{
			switch (dice.D(6))
			{
				case 1:
					character.Skills.Increase("Medic");
					return;

				case 2:
					character.Skills.Increase("Survival");
					return;

				case 3:
					character.Skills.Increase("Explosives");
					return;

				case 4:
					character.Skills.Increase(dice.Choose(SpecialtiesFor("Engineer")));
					return;

				case 5:
					character.Skills.Increase(dice.Choose(SpecialtiesFor("Pilot")));
					return;

				case 6:
					character.Skills.Increase("Navigation");
					return;
			}
		}

		protected override void OfficerTraining(Character character, Dice dice)
		{
			switch (dice.D(6))
			{
				case 1:
					character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
					return;

				case 2:
					character.Skills.Increase(dice.Choose(SpecialtiesFor("Tactics")));
					return;

				case 3:
					character.Skills.Increase("Admin");
					return;

				case 4:
					character.Skills.Increase("Advocate");
					return;

				case 5:
					character.Skills.Increase("Vacc Suit");
					return;

				case 6:
					character.Skills.Increase("Leadership");
					return;
			}
		}

		protected override void PersonalDevelopment(Character character, Dice dice)
		{
			switch (dice.D(6))
			{
				case 1:
					character.Strength += 1;
					return;

				case 2:
					character.Dexterity += 1;
					return;

				case 3:
					character.Endurance += 1;
					return;

				case 4:
					character.Skills.Increase("Gambler");
					return;

				case 5:
					character.Skills.Increase("Melee", "Unarmed");
					return;

				case 6:
					character.Skills.Increase("Melee", "Unarmed");
					return;
			}
		}
	}
}