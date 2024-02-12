using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Encounters;
using Microsoft.AspNetCore.Components;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class EncountersPage
{
    [Inject] CharacterBuilder CharacterBuilder { get; set; } = null!;
    [Inject] EncounterGenerator EncounterGenerator { get; set; } = null!;

    protected void BackwaterStarportGeneralEncounter()
    {
        Model.Encounters.Insert(0, EncounterGenerator.BackwaterStarportGeneralEncounter(new Dice(), Model));
    }

    protected void BackwaterStarportSignificantEncounter()
    {
        Model.Encounters.Insert(0, EncounterGenerator.BackwaterStarportSignificantEncounter(new Dice(), Model));
    }

    protected void BustlingStarportGeneralEncounter()
    {
        Model.Encounters.Insert(0, EncounterGenerator.BustlingStarportGeneralEncounter(new Dice(), Model));
    }

    protected void BustlingStarportSignificantEncounter()
    {
        Model.Encounters.Insert(0, EncounterGenerator.BustlingStarportSignificantEncounter(new Dice(), Model));
    }

    protected void GenerateAlliesAndEnemies()
    {
        Model.Encounters.Insert(0, EncounterGenerator.PickAlliesAndEnemies(new Dice(), Model));
    }

    protected void GenerateMission()
    {
        Model.Encounters.Insert(0, EncounterGenerator.PickMission(new Dice(), Model));
    }

    protected void GeneratePatron()
    {
        Model.Encounters.Insert(0, EncounterGenerator.PickPatron(new Dice(), Model));
    }

    protected override void Initialized()
    {
        Model.SpeciesAndFactionsList = CharacterBuilder.FactionsAndSpecies;
    }

    protected void MetropolisStarportGeneralEncounter()
    {
        Model.Encounters.Insert(0, EncounterGenerator.MetropolisStarportGeneralEncounter(new Dice(), Model));
    }

    protected void MetropolisStarportSignificantEncounter()
    {
        Model.Encounters.Insert(0, EncounterGenerator.MetropolisStarportSignificantEncounter(new Dice(), Model));
    }
}
