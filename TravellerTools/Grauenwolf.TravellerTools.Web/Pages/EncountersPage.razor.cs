using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Encounters;
using Microsoft.AspNetCore.Components;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class EncountersPage
{
    [Inject] CharacterBuilderLocator CharacterBuilderLocator { get; set; } = null!;
    [Inject] EncounterGenerator EncounterGenerator { get; set; } = null!;

    protected void BackwaterStarportGeneralEncounter()
    {
        Model.Encounters.Insert(0, EncounterGenerator.BackwaterStarportGeneralEncounter(new Dice()));
    }

    protected void BackwaterStarportSignificantEncounter()
    {
        Model.Encounters.Insert(0, EncounterGenerator.BackwaterStarportSignificantEncounter(new Dice()));
    }

    protected void GenerateAlliesAndEnemies()
    {
        Model.Encounters.Insert(0, EncounterGenerator.PickAlliesAndEnemies(new Dice()));
    }

    protected void GenerateMission()
    {
        Model.Encounters.Insert(0, EncounterGenerator.PickMission(new Dice()));
    }

    protected void GeneratePatron()
    {
        Model.Encounters.Insert(0, EncounterGenerator.PickPatron(new Dice()));
    }
}
