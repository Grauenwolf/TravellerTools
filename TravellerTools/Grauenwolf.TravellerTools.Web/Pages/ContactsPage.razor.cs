using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Names;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using System;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class ContactsPage
{
    protected ContactsModel? ContactsModel { get; set; }
    [Inject] CharacterBuilder CharacterBuilder { get; set; } = null!;
    [Inject] NameGenerator NameGenerator { get; set; } = null!;
    //public int? Seed { get => Get<int?>(); set => Set(value); }

    protected void GenerateContacts()
    {
        if (Model == null) //this shouldn't happen.
            return;
        try
        {
            //TODO: Read seed from URL
            var seed = new Random().Next();
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

            ContactsModel = result;
        }
        catch (Exception ex)
        {
            LogError(ex, $"Error in creating character using {nameof(GenerateContacts)}.");
        }
    }

    protected override void Initialized()
    {
        //if (Navigation.TryGetQueryString("seed", out int seed))
        //    Seed = seed;
        //else
        //    Seed = (new Random()).Next();

        //Options.FromQueryString(Navigation.ParsedQueryString());

        //if (Navigation.TryGetQueryString("tasZone", out string tasZone))
        //    TasZone = tasZone;

        Model = new ContactOptions();
    }
}
