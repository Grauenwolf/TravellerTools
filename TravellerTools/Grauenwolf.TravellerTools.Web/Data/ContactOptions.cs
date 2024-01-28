using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Shared;
using Microsoft.Extensions.Primitives;
using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Web.Data;

public class ContactOptions : ModelBase
{
    private CharacterBuilder m_CharacterBuilder;

    public ContactOptions(CharacterBuilder characterBuilder)
    {
        m_CharacterBuilder = characterBuilder;
    }

    public int Allies
    {
        get => Get<int>();
        set
        {
            Set(value);
            if (value < 0)
                Set(0); //We need to change the value, then change it back, to update the UI.
        }
    }

    public int Contacts
    {
        get => Get<int>();
        set
        {
            Set(value);
            if (value < 0)
                Set(0);
        }
    }

    public int Enemies
    {
        get => Get<int>();
        set
        {
            Set(value);
            if (value < 0)
                Set(0);
        }
    }

    public int Rivals
    {
        get => Get<int>();
        set
        {
            Set(value);
            if (value < 0)
                Set(0);
        }
    }

    public IReadOnlyList<FactionOrSpecies> SpeciesAndFactionsList => m_CharacterBuilder.FactionsAndSpecies;

    public string? SpeciesOrFaction { get => Get<string?>(); set => Set(value); }

    public void FromQueryString(Dictionary<string, StringValues> keyValuePairs)
    {
        Allies = keyValuePairs.ParseInt("allies");
        Contacts = keyValuePairs.ParseInt("contacts");
        Enemies = keyValuePairs.ParseInt("enemies");
        Rivals = keyValuePairs.ParseInt("rivals");
        SpeciesOrFaction = keyValuePairs.ParseStringOrNull("species");
    }

    public IDictionary<string, string?> ToQueryString()
    {
        return new Dictionary<string, string?>
        {
            { "allies", Allies.ToString() },
            { "contacts", Contacts.ToString() },
            { "enemies", Enemies.ToString() },
            { "rivals", Rivals.ToString() },
            { "species", SpeciesOrFaction }
        };
    }
}
