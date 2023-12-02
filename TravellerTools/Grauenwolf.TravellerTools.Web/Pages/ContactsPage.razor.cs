using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Names;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.WebUtilities;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class ContactsPage
{
    public int? Seed { get => Get<int?>(); set => Set(value); }
    protected ContactsModel? ContactsModel { get; set; }
    [Inject] CharacterBuilder CharacterBuilder { get; set; } = null!;
    [Inject] NameGenerator NameGenerator { get; set; } = null!;

    protected void GenerateContacts()
    {
        OnGenerateContacts(new Random().Next());
    }

    protected override void Initialized()
    {
        if (Navigation.TryGetQueryString("seed", out int seed))
            Seed = seed;

        Model.FromQueryString(Navigation.ParsedQueryString());

        if (Seed != null)
            OnGenerateContacts(Seed.Value);
    }

    protected void Permalink(MouseEventArgs _)
    {
        string uri = Permalink();
        Navigation.NavigateTo(uri, true);
    }

    protected string Permalink()
    {
        var uri = $"/contacts";

        uri = QueryHelpers.AddQueryString(uri, Model.ToQueryString());
        uri = QueryHelpers.AddQueryString(uri, "seed", (ContactsModel?.Seed ?? 0).ToString());
        return uri;
    }

    private void OnGenerateContacts(int seed)
    {
        try
        {
            //TODO: Read seed from URL
            var dice = new Dice(seed);
            var result = new ContactsModel();

            //TODO: Create a stub class with matching interface just for making contacts.
            var character = new Character();
            character.AddAlly(Model.Allies);
            character.AddContact(Model.Contacts);
            character.AddRival(Model.Rivals);
            character.AddEnemy(Model.Enemies);

            CharacterBuilder.BuildContacts(dice, character);
            result.Contacts = character.Contacts;
            result.Seed = seed;

            ContactsModel = result;
        }
        catch (Exception ex)
        {
            LogError(ex, $"Error in creating contacts using {nameof(GenerateContacts)}.");
        }
    }
}
