using Grauenwolf.TravellerTools.Characters;
using Microsoft.Extensions.Primitives;
using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Web.Data;

public class ContactOptions : ModelBase
{
    private CharacterBuilderLocator m_CharacterBuilderLocator;

    public ContactOptions(CharacterBuilderLocator characterBuilderLocator)
    {
        m_CharacterBuilderLocator = characterBuilderLocator;
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

    public string? Species { get => Get<string?>(); set => Set(value); }

    public IReadOnlyList<string> SpeciesList => m_CharacterBuilderLocator.SpeciesList;

    public void FromQueryString(Dictionary<string, StringValues> keyValuePairs)
    {
        Allies = keyValuePairs.ParseInt("allies");
        Contacts = keyValuePairs.ParseInt("contacts");
        Enemies = keyValuePairs.ParseInt("enemies");
        Rivals = keyValuePairs.ParseInt("rivals");
        Species = keyValuePairs.ParseStringOrNull("species");
    }

    public IDictionary<string, string?> ToQueryString()
    {
        return new Dictionary<string, string?>
        {
            { "allies", Allies.ToString() },
            { "contacts", Contacts.ToString() },
            { "enemies", Enemies.ToString() },
            { "rivals", Rivals.ToString() },
            { "species", Species }
        };
    }
}
