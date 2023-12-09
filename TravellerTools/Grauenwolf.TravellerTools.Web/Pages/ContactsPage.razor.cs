using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Names;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class ContactsPage
{
    protected ContactsModel? ContactsModel { get; set; }
    [Inject] CharacterBuilderLocator CharacterBuilderLocator { get; set; } = null!;
    [Inject] NameGenerator NameGenerator { get; set; } = null!;

    protected void GenerateContacts()
    {
        try
        {
            int seed = new Random().Next();
            var dice = new Dice(seed);
            var result = new ContactsModel();

            //TODO: Create a stub class with matching interface just for making contacts.
            var character = new Character();
            character.AddAlly(Model.Allies);
            character.AddContact(Model.Contacts);
            character.AddRival(Model.Rivals);
            character.AddEnemy(Model.Enemies);

            CharacterBuilderLocator.BuildContacts(dice, character);
            result.Contacts = character.Contacts;
            result.Seed = seed;

            ContactsModel = result;
        }
        catch (Exception ex)
        {
            LogError(ex, $"Error in creating contacts using {nameof(GenerateContacts)}.");
        }
    }

    protected string Permalink()
    {
        var uri = $"/contacts/view";

        uri = QueryHelpers.AddQueryString(uri, Model.ToQueryString());
        uri = QueryHelpers.AddQueryString(uri, "seed", (ContactsModel?.Seed ?? 0).ToString());
        return uri;
    }
}
