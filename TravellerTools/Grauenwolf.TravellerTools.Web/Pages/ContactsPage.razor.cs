using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Names;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Tortuga.Anchor;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class ContactsPage
{
    protected ContactsModel? ContactsModel { get; set; }
    [Inject] CharacterBuilder CharacterBuilder { get; set; } = null!;
    [Inject] NameGenerator NameGenerator { get; set; } = null!;

    protected void GenerateContacts()
    {
        if (Model == null)
            return; //this shouldn't happen.

        try
        {
            int seed = new Random().Next();
            var dice = new Dice(seed);
            var result = new ContactsModel();

            var character = new ContactGroup();
            character.AddAlly(Model.Allies);
            character.AddContact(Model.Contacts);
            character.AddRival(Model.Rivals);
            character.AddEnemy(Model.Enemies);

            var speciesOrFactionOdds = new OddsTable<string>();
            if (!Model.SpeciesOrFaction.IsNullOrEmpty())
                speciesOrFactionOdds.Add(Model.SpeciesOrFaction, 100); //TODO - Add the option for 50% of the contacts to be of the selected race

            CharacterBuilder.BuildContacts(dice, character, speciesOrFactionOdds);
            result.Contacts = character.Contacts;
            result.Seed = seed;

            ContactsModel = result;
        }
        catch (Exception ex)
        {
            LogError(ex, $"Error in creating contacts using {nameof(GenerateContacts)}.");
        }
    }

    protected override void Initialized()
    {
        Model = new ContactOptions(CharacterBuilder);
    }

    protected string Permalink()
    {
        if (Model == null)
            return ""; //this shouldn't happen.

        var uri = $"/contacts/view";

        uri = QueryHelpers.AddQueryString(uri, Model.ToQueryString());
        uri = QueryHelpers.AddQueryString(uri, "seed", (ContactsModel?.Seed ?? 0).ToString());
        return uri;
    }
}
