using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class EncountersPage
{
    [Inject] CharacterBuilder CharacterBuilder { get; set; } = null!;

    protected void GenerateAlliesAndEnemies()
    {
        //TODO: Add name generator
        var dice = new Dice();
        Model.Encounters.Add(new NameDescriptionPair("Allies and Enemies", PatronBuilder.PickAlliesAndEnemies(dice)));
    }

    protected void GenerateMission()
    {
        var dice = new Dice();
        Model.Encounters.Add(new NameDescriptionPair("Mission", PatronBuilder.PickMission(dice)));
    }

    protected void GeneratePatron()
    {
        //TODO: Add name generator
        var dice = new Dice();
        Model.Encounters.Add(new NameDescriptionPair("Patron", PatronBuilder.PickPatron(dice)));
    }
}
