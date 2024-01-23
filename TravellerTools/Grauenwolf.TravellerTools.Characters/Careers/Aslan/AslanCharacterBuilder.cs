using Grauenwolf.TravellerTools.Names;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

public class AslanCharacterBuilder : CharacterBuilder
{
    CareerLists FemaleCareers;

    CareerLists MaleCareers;

    public AslanCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilderLocator characterBuilderLocator) : base(dataPath, nameGenerator, characterBuilderLocator)
    {
        var femaleCareers = new List<CareerBase>()
        {
            new Ceremonial_ClanAgent(this),
            new Ceremonial_Priest(this),
            //new Envoy_Negotiator(this),
            //new Envoy_Spy(this),
            //new Envoy_Duellist(this),
            //new Management_Corporate(this),
            //new Management_ClanAide(this),
            //new Management_Governess(this),
            //new Military_Support(this),
            //new MilitaryOfficer_ExecutiveOfficer(this),
            //new Scientist_Healer(this),
            //new Scientist_Researcher(this),
            //new Scientist_Explorer(this),
            //new Spacer_Engineer(this),
            //new Spacer_Crew(this),
            //new SpaceOfficer_Shipmaster(this),
            //new SpaceOfficer_Navigator(this),
            //new Outcast_Labourer(this),
            //new Outcast_Trader(this),
            //new Outcast_Scavenger(this),
            //new Outlaw_Pirate(this),
            //new Outlaw_Raider(this),
            //new Outlaw_Thief(this)
        }.ToImmutableArray();

        var maleCareers = new List<CareerBase>()
        {
            new Ceremonial_Poet(this),
            new Ceremonial_ClanAgent(this),
            new Ceremonial_Priest(this),
            //new Envoy_Negotiator(this),
            //new Envoy_Spy(this),
            //new Envoy_Duellist(this),
            //new Military_Warrior(this),
            //new Military_Cavalry(this),
            //new Military_Flyer(this),
            //new MilitaryOfficer_Leader(this),
            //new MilitaryOfficer_Assassin(this),
            //new Scientist_Healer(this),
            //new Spacer_Pilot(this),
            //new Spacer_Gunner(this),
            //new Spacer_Crew(this),
            //new SpaceOfficer_Commander(this),
            //new Outcast_Labourer(this),
            //new Outcast_Trader(this),
            //new Outcast_Scavenger(this),
            //new Outlaw_Pirate(this),
            //new Outlaw_Raider(this),
            //new Outlaw_Thief(this),
            //new Wanderer_Belter(this),
            //new Wanderer_Nomad(this),
            //new Wanderer_Scout(this),
        }.ToImmutableArray();

        var draftCareers = new List<CareerBase>()
        {
            new Humaniti.Drifter_Barbarian(this),
            new Humaniti.Drifter_Scavenger(this),
            new Humaniti.Drifter_Wanderer(this),
            new Humaniti.Rogue_Enforcer(this),
            new Humaniti.Rogue_Pirate(this),
            new Humaniti.Rogue_Thief(this)
            //new Outcast_Labourer(this),
            //new Outcast_Trader(this),
            //new Outcast_Scavenger(this),
            //new Outlaw_Pirate(this),
            //new Outlaw_Raider(this),
            //new Outlaw_Thief(this),
        }.ToImmutableArray();

        var defaultCareers = new List<CareerBase>()
        {
            new Humaniti.Drifter_Barbarian(this),
            new Humaniti.Drifter_Scavenger(this),
            new Humaniti.Drifter_Wanderer(this),
            //new Outcast_Labourer(this),
            //new Outcast_Trader(this),
            //new Outcast_Scavenger(this),
        }.ToImmutableArray();

        MaleCareers = new(defaultCareers, draftCareers, maleCareers);
        FemaleCareers = new(defaultCareers, draftCareers, femaleCareers);
    }

    public override ImmutableArray<Gender> Genders { get; } = ImmutableArray.Create<Gender>(new("F", "Female", 26), new("M", "Male", 10));
    public override string Species => "Aslan";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Aslan";
    protected override int AgingRollMinAge => 40;
    protected override bool AllowPsionics => false;

    public override ImmutableArray<CareerBase> Careers(Character? character)
    {
        if (character == null)
            return MaleCareers.AllCareers
                .Union(FemaleCareers.AllCareers)
                .Distinct(DistinctTypeComparer<CareerBase>.Default).ToImmutableArray();

        if (character.Gender == "M")
            return MaleCareers.AllCareers;
        else
            return FemaleCareers.AllCareers;
    }

    public override ImmutableArray<CareerBase> DefaultCareers(Character? character)
    {
        if (character == null)
            return MaleCareers.DefaultCareers
                .Union(FemaleCareers.DefaultCareers)
                .Distinct(DistinctTypeComparer<CareerBase>.Default).ToImmutableArray();

        if (character.Gender == "M")
            return MaleCareers.DefaultCareers;
        else
            return FemaleCareers.DefaultCareers;
    }

    public override ImmutableArray<CareerBase> DraftCareers(Character? character)
    {
        if (character == null)
            return MaleCareers.DraftCareers
                .Union(FemaleCareers.DraftCareers)
                .Distinct(DistinctTypeComparer<CareerBase>.Default).ToImmutableArray();

        if (character.Gender == "M")
            return MaleCareers.DraftCareers;
        else
            return FemaleCareers.DraftCareers;
    }

    protected override int AgingRollDM(Character character) => -2 * character.CurrentTerm;

    protected override CareerLists CreateCareerList() => CareerLists.Empty;

    protected override void InitialCharacterStats(Dice dice, Character character)
    {
        var majorClan = dice.NextBoolean();
        if (majorClan)
            character.AddHistory($"Born into a major clan.", 0);
        else
            character.AddHistory($"Born into a minor clan.", 0);

        var ancestralTerritory = 0;

        switch (dice.D(6) + (majorClan ? 1 : 0))
        {
            case 1:
                character.AddHistory($"{character.Name}'s ancestor shamed the clan and {character.Name} come from a branch long dishonoured.", 0);
                break;

            case 2:
                character.AddHistory($"{character.Name}'s family�s glory days are long gone, all that is left is the tales of great landholdings now lost to upstarts.", 0);
                break;

            case 3:
                character.AddHistory($"{character.Name}'s family made its fortune in the great expansion after the discovery of jump drive; most family holdings are on distant worlds.", 0);
                ancestralTerritory += 1;
                break;

            case 4:
                character.AddHistory($"{character.Name}'s family are the descendants of an ancient hero forgotten by most Aslan.", 0);
                ancestralTerritory += 1;
                break;

            case 5:
                character.AddHistory($"{character.Name}'s family�s ancestor was a trickster who deceived his enemies.", 0);
                ancestralTerritory += 2;
                break;

            case 6:
                character.AddHistory($"{character.Name}'s ancestors were conquerors and great warriors.", 0);
                ancestralTerritory += 2;
                break;

            case 7:
                character.AddHistory($"{character.Name}'s family is one of the most influential and wealthy in the Hierate.", 0);
                ancestralTerritory += 3;
                break;
        }

        var parents = new[] { "Grandfather", "Father" };
        foreach (var parent in parents)
        {
            switch (dice.D(2, 6))
            {
                case 2:
                    character.AddHistory($"Dishonoured! {character.Name}'s {parent} committed some dishonourable act that caused the clan to strip {character.Name}'s family of all territory.", 0);
                    if (character.Gender == "M")
                        character.Skills.Add("Independence");
                    else
                        character.Skills.Add("Profession");
                    ancestralTerritory = 0;
                    break;

                case 3:
                    character.AddHistory($"{character.Name}'s {parent} was beset by many foes, one of whom conquered much of {character.Name}'s land.", 0);
                    character.AddEnemy();
                    character.Skills.Add("Gun Combat");
                    ancestralTerritory += -4;
                    break;

                case 4:
                    character.AddHistory($"{character.Name}'s {parent} was a fool who gambled away much of {character.Name}'s land. ", 0);
                    ancestralTerritory += -3;
                    if (dice.NextBoolean())
                        character.Skills.Add("Gamble");
                    else
                        character.Skills.Add("Carouse");
                    break;

                case 5:
                    character.AddHistory($"{character.Name}'s {parent} suffered from a degenerative genetic disease that {character.Name} may have inherited. ", 0);
                    ancestralTerritory += -2;
                    character.Skills.Add("Medic");
                    break;

                case 6:
                    character.AddHistory($"{character.Name}'s {parent} barely managed to hold onto {character.Name}'s landhold. ", 0);
                    ancestralTerritory += -1;
                    break;

                case 7: ancestralTerritory += 1; break;
                case 8: ancestralTerritory += 2; break;
                case 9: ancestralTerritory += 3; break;
                case 10: ancestralTerritory += 4; break;
                case 11: ancestralTerritory += 5; break;
                case 12: ancestralTerritory += 6; break;
            }
        }

        character.AddHistory($"{character.Name}'s ancestral territory is {ancestralTerritory}. ", 0);

        if (character.Gender == "M")
            switch (dice.D(2, 6))
            {
                case <= 3: character.AddHistory($"{character.Name} is the first son.", 0); break;
                case <= 10: character.AddHistory($"{character.Name} is the second son.", 0); break;
                default: character.AddHistory($"{character.Name} is the third son.", 0); break;
            }
        else
            switch (dice.D(2, 6))
            {
                case <= 3: character.AddHistory($"{character.Name} is the eldest daughter.", 0); break;
                case <= 10: character.AddHistory($"{character.Name} is the middle daughter.", 0); break;
                default: character.AddHistory($"{character.Name} is the youngest daughter.", 0); break;
            }

        character.Strength = dice.D(2, 6);
        character.Dexterity = dice.D(2, 6);
        character.Endurance = dice.D(2, 6);
        character.Intellect = dice.D(2, 6);
        character.Education = dice.D(2, 6);
        character.SocialStanding = Math.Max(ancestralTerritory, 0);
        character.Territory = 0;

        if (ancestralTerritory >= 10 && character.Gender == "M")
            character.Skills.Add("Leadership", 1);

        character.Age = 14;

        //rite of passage
        var rollA = dice.D(6);
        var rollB = dice.D(6);
        var riteOfPassageRoll = rollA + rollB;

        if (character.Gender == "M")
        {
            if (character.Strength > riteOfPassageRoll)
                character.RiteOfPassageDM += 1;
            if (character.Dexterity > riteOfPassageRoll)
                character.RiteOfPassageDM += 1;
            if (character.Endurance > riteOfPassageRoll)
                character.RiteOfPassageDM += 1;
            if (character.Intellect > riteOfPassageRoll)
                character.RiteOfPassageDM += 1;
            if (character.Education > riteOfPassageRoll)
                character.RiteOfPassageDM += 1;
            if (character.SocialStanding > riteOfPassageRoll)
                character.RiteOfPassageDM += 1;
        }
        else
        {
            if (character.Intellect > riteOfPassageRoll)
                character.RiteOfPassageDM += 2;
            if (character.Education > riteOfPassageRoll)
                character.RiteOfPassageDM += 2;
            if (character.SocialStanding > riteOfPassageRoll)
                character.RiteOfPassageDM += 2;
        }

        switch ((rollA, rollB))
        {
            case (1, 1):
                character.AddHistory($"{character.Name} has a great destiny. Gain {dice.D(3)} clan shares.", character.Age);
                break;

            case (2, 2):
                character.AddHistory($"Impressive Performance. Earn Cr5000 as a reward", character.Age);
                break;

            case (3, 3):
                character.AddHistory($"Befriended one of the other young Aslan undergoing the rite that day. Gain a Contact.", character.Age);
                character.AddContact();
                break;

            case (4, 4):
                character.AddHistory($"One of the other Aslan undergoing the rite tries to outdo {character.Name}. Gain a Rival.", character.Age);
                character.AddRival();
                break;

            case (5, 5):
                character.AddHistory($"{character.Name} is wounded in one of the tests, leaving a distinctive scar across {character.Name}'s fur.", character.Age);
                break;

            case (6, 6):
                character.AddHistory($"{character.Name}'s rite tests {character.Name} to the limit, but {character.Name} is determined not to give in..", character.Age);
                break;
        }

        character.AddHistory($"Rite of passage completed with a {character.RiteOfPassageDM}.", character.Age);
    }

    protected override CareerBase PickNextCareer(Character character, Dice dice)
    {
        bool noRoll = false;
        var careersOptions = new List<CareerBase>();

        //Forced picks (e.g. Draft)
        if (character.NextTermBenefits.MustEnroll != null)
        {
            foreach (var career in Careers(character))
            {
                noRoll = true; //Don't need to roll if forced to enroll
                if (string.Equals(character.NextTermBenefits.MustEnroll, career.Career, StringComparison.OrdinalIgnoreCase) || string.Equals(character.NextTermBenefits.MustEnroll, career.Assignment, StringComparison.OrdinalIgnoreCase))
                {
                    careersOptions.Add(career);
                }
            }
        }

        //If an outcast, change the career list.
        var careerList = character.IsOutcast ? DraftCareers(character) : Careers(character);

        //Normal career progression
        if (!character.NextTermBenefits.MusterOut && careersOptions.Count == 0 && character.LastCareer != null)
        {
            if (dice.D(10) > 1 || character.LastCareer.Terms < 3) //continue career
            {
                foreach (var career in careerList)
                {
                    noRoll = true; //Don't need to roll if continuing a career
                    if (character.LastCareer.ShortName == career.ShortName)
                    {
                        careersOptions.Add(career);
                    }
                }
            }
            else
            {
                character.NextTermBenefits.MusterOut = true;
                character.AddHistory($"Voluntarily left " + character.LastCareer.ShortName, character.Age);
            }
        }

        //Random picks
        if (careersOptions.Count == 0)
        {
            foreach (var career in careerList)
            {
                if (character.NextTermBenefits.MusterOut && character.LastCareer!.Career == career.Career)
                    continue; //No assignments from previous career allowed

                if (career.Qualify(character, dice, true))
                    careersOptions.Add(career);
            }
        }

        //Random picks when not qualified for anything
        if (careersOptions.Count == 0)
        {
            foreach (var career in careerList)
            {
                if (character.NextTermBenefits.MusterOut && character.LastCareer!.Career == career.Career)
                    continue; //No assignments from previous career allowed

                careersOptions.Add(career);
            }
        }

        var result = dice.Choose(careersOptions);

        if (result.Qualify(character, dice, false) || noRoll) //Force a Qualify roll so we can get special behavior for Psionic Community
            return result;
        else
        {
            if (character.IsOutcast)
            {
                character.AddHistory($"Failed to qualify for {result}.", character.Age);
            }
            else
            {
                character.AddHistory($"Failed to qualify for {result} and became an outcast.", character.Age);
                character.IsOutcast = true;
                character.SocialStanding = 2;
            }
            return dice.Choose(DefaultCareers(character));
        }
    }

    protected override int RollForPsi(Character character, Dice dice) => 0;
}