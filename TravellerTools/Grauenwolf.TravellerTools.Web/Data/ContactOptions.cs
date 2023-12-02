using Microsoft.Extensions.Primitives;
using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Web.Data;

public class ContactOptions : ModelBase
{
    public int Allies { get => Get<int>(); set => Set(value); }
    public int Contacts { get => Get<int>(); set => Set(value); }
    public int Enemies { get => Get<int>(); set => Set(value); }
    public int Rivals { get => Get<int>(); set => Set(value); }

    public void FromQueryString(Dictionary<string, StringValues> keyValuePairs)
    {
        Allies = keyValuePairs.ParseInt("allies");
        Contacts = keyValuePairs.ParseInt("contacts");
        Enemies = keyValuePairs.ParseInt("enemies");
        Rivals = keyValuePairs.ParseInt("rivals");
    }

    public IDictionary<string, string?> ToQueryString()
    {
        return new Dictionary<string, string?>
        {
            { "allies", Allies.ToString() },
            { "contacts", Contacts.ToString() },
            { "enemies", Enemies.ToString() },
            { "rivals", Rivals.ToString() }
        };
    }
}
