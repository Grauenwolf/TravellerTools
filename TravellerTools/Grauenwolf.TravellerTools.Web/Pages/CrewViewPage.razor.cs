using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Names;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class CrewViewPage
{
    public int? Seed { get => Get<int?>(); set => Set(value); }
    protected CrewModel? CrewModel { get; set; }
    [Inject] CharacterBuilder CharacterBuilder { get; set; } = null!;
    [Inject] NameGenerator NameGenerator { get; set; } = null!;

    protected void GenerateCrew(int seed)
    {
        if (Model == null)
            return; //this shouldn't happen.

        try
        {
            var dice = new Dice(seed);
            var result = new CrewModel();
            result.Seed = seed;

            result.Crew.AddRange(CreateRole(dice, Model, CrewRole.Pilot, Model.Pilots));
            result.Crew.AddRange(CreateRole(dice, Model, CrewRole.Astrogator, Model.Astrogators));
            result.Crew.AddRange(CreateRole(dice, Model, CrewRole.Engineer, Model.Engineers));
            result.Crew.AddRange(CreateRole(dice, Model, CrewRole.Medic, Model.Medics));
            result.Crew.AddRange(CreateRole(dice, Model, CrewRole.Gunner, Model.Gunners));
            result.Crew.AddRange(CreateRole(dice, Model, CrewRole.Steward, Model.Stewards));
            result.Crew.AddRange(CreateRole(dice, Model, CrewRole.Administrator, Model.Administrators));
            result.Crew.AddRange(CreateRole(dice, Model, CrewRole.SensorOperator, Model.SensorOperators));
            result.Crew.AddRange(CreateRole(dice, Model, CrewRole.Officer, Model.Officers));
            result.Crew.AddRange(CreateRole(dice, Model, CrewRole.Passenger, Model.Passengers));

            CrewModel = result;
        }
        catch (Exception ex)
        {
            LogError(ex, $"Error in creating contacts using {nameof(GenerateCrew)}.");
        }
    }

    protected override void Initialized()
    {
        if (Navigation.TryGetQueryString("seed", out int seed))
            Seed = seed;

        Model = new CrewOptions(CharacterBuilder);
        Model.FromQueryString(Navigation.ParsedQueryString());

        if (Seed != null)
            GenerateCrew(Seed.Value);
        else
            Navigation.NavigateTo("/contacts", true, true);
    }

    protected string Permalink()
    {
        if (Model == null)
            return ""; //this shouldn't happen.

        var uri = $"/crew/view";

        uri = QueryHelpers.AddQueryString(uri, Model.ToQueryString());
        uri = QueryHelpers.AddQueryString(uri, "seed", (CrewModel?.Seed ?? 0).ToString());
        return uri;
    }

    CrewMember CreateCrewMember(Dice dice, ISpeciesSettings speciesSettings, CrewRole crewRole, string? targetSkillName, int targetSkillLevel)
    {
        if (targetSkillName == null)
        {
            while (true)
            {
                var character = CharacterBuilder.CreateCharacter(dice, speciesSettings);
                if (!character.IsDead)
                {
                    var skill = character.Skills.BestSkill();
                    return new CrewMember(crewRole, character, skill?.FullName, skill?.Level, character.Title);
                }
            }
        }
        else if (targetSkillName.Contains('|'))
        {
            var parts = targetSkillName.Split('|');
            var character = CharacterBuilder.CreateCharacterWithSkill(dice, speciesSettings, parts[0], parts[1], targetSkillLevel);

            var lastBestSkill = character.Skills.BestSkill(parts[1]);
            return new CrewMember(crewRole, character, lastBestSkill?.FullName, lastBestSkill?.Level, character.Title);
        }
        else
        {
            var character = CharacterBuilder.CreateCharacterWithSkill(dice, speciesSettings, targetSkillName, null, targetSkillLevel);

            var lastBestSkill = character.Skills.BestSkill(targetSkillName);
            return new CrewMember(crewRole, character, lastBestSkill?.FullName, lastBestSkill?.Level, character.Title);
        }
    }

    List<CrewMember> CreateRole(Dice dice, ISpeciesSettings speciesSettings, CrewRole crewRole, int count)
    {
        var result = new List<CrewMember>();
        if (count == 0) return result;

        var targetSkillLevel = (int)Math.Floor(dice.D(2, 6) / 3.0);
        var targetSkillName = crewRole switch
        {
            CrewRole.Pilot => "Pilot",
            CrewRole.Astrogator => "Astrogation",
            CrewRole.Engineer => "Engineer",
            CrewRole.Medic => "Medic",
            CrewRole.Gunner => "Gunner",
            CrewRole.Steward => "Steward",
            CrewRole.SensorOperator => "Electronics|Sensors",
            CrewRole.Officer => "Leadership",
            CrewRole.Administrator => "Admin",
            _ => null
        };

        result.Add(CreateCrewMember(dice, speciesSettings, crewRole, targetSkillName, targetSkillLevel));

        for (int i = 2; i <= count; i++) //additional crew members have equal or lower skill
            result.Add(CreateCrewMember(dice, speciesSettings, crewRole, targetSkillName, dice.Next(0, targetSkillLevel + 1)));

        return result;
    }
}