using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Names;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class CrewPage
{
    protected CrewModel? CrewModel { get; set; }
    [Inject] CharacterBuilderLocator CharacterBuilderLocator { get; set; } = null!;
    [Inject] NameGenerator NameGenerator { get; set; } = null!;

    protected void GenerateCrew()
    {
        if (Model == null)
            return; //this shouldn't happen.

        try
        {
            int seed = new Random().Next();
            var dice = new Dice(seed);
            var result = new CrewModel();
            result.Seed = seed;

            result.Crew.AddRange(CreateRole(dice, Model.Species, CrewRole.Pilot, Model.Pilots));
            result.Crew.AddRange(CreateRole(dice, Model.Species, CrewRole.Astrogator, Model.Astrogators));
            result.Crew.AddRange(CreateRole(dice, Model.Species, CrewRole.Engineer, Model.Engineers));
            result.Crew.AddRange(CreateRole(dice, Model.Species, CrewRole.Medic, Model.Medics));
            result.Crew.AddRange(CreateRole(dice, Model.Species, CrewRole.Gunner, Model.Gunners));
            result.Crew.AddRange(CreateRole(dice, Model.Species, CrewRole.Steward, Model.Stewards));
            result.Crew.AddRange(CreateRole(dice, Model.Species, CrewRole.Passenger, Model.Passengers));

            CrewModel = result;
        }
        catch (Exception ex)
        {
            LogError(ex, $"Error in creating contacts using {nameof(GenerateCrew)}.");
        }
    }

    protected override void Initialized()
    {
        Model = new CrewOptions(CharacterBuilderLocator);
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

    CrewMember CreateCrewMember(Dice dice, string? species, CrewRole crewRole, string? targetSkillName, int targetSkillLevel)
    {
        var options = new CharacterBuilderOptions();
        var temp = NameGenerator.CreateRandomPerson(dice);
        options.Name = temp.FullName;
        options.Gender = temp.Gender;
        options.Species = species ?? CharacterBuilderLocator.GetRandomSpecies(dice);
        options.MaxAge = 22 + dice.D(1, 60);

        if (targetSkillName == null)
        {
            while (true)
            {
                options.Seed = dice.Next();
                var character = CharacterBuilderLocator.Build(options);
                if (!character.IsDead)
                {
                    var skill = character.Skills.BestSkill();
                    return new CrewMember(crewRole, character.GetCharacterBuilderOptions(), skill?.FullName, skill?.Level, character.Title);
                }
            }
        }
        else
        {
            Character? lastBest = null;
            int lastBestSkillLevel = -3;

            for (var i = 0; i < 500; i++)
            {
                options.Seed = dice.Next();
                var character = CharacterBuilderLocator.Build(options);
                if (!character.IsDead)
                {
                    int currentSkill = character.Skills.EffectiveSkillLevel(targetSkillName);
                    if (currentSkill == targetSkillLevel)
                    {
                        var skill = character.Skills.BestSkill(targetSkillName);
                        return new CrewMember(crewRole, character.GetCharacterBuilderOptions(), skill?.FullName, skill?.Level, character.Title);
                    }
                    else if (currentSkill > lastBestSkillLevel)
                    {
                        lastBest = character;
                        lastBestSkillLevel = currentSkill;
                    }
                }
            }

            var lastBestSkill = lastBest?.Skills.BestSkill(targetSkillName);
            return new CrewMember(crewRole, lastBest?.GetCharacterBuilderOptions() ?? options, lastBestSkill?.FullName, lastBestSkill?.Level, lastBest?.Title);
        }
    }

    List<CrewMember> CreateRole(Dice dice, string? species, CrewRole crewRole, int count)
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
            _ => null
        };

        result.Add(CreateCrewMember(dice, species, crewRole, targetSkillName, targetSkillLevel));

        for (int i = 2; i <= count; i++) //additional crew members have equal or lower skill
            result.Add(CreateCrewMember(dice, species, crewRole, targetSkillName, dice.Next(0, targetSkillLevel + 1)));

        return result;
    }
}
