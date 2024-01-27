using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Shared;
using Microsoft.Extensions.Primitives;
using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Web.Data;

public class CrewOptions : ModelBase
{
    private CharacterBuilder m_CharacterBuilder;

    public CrewOptions(CharacterBuilder characterBuilder)
    {
        m_CharacterBuilder = characterBuilder;
    }

    public int Astrogators
    {
        get => Get<int>();
        set
        {
            Set(value);
            if (value < 0)
                Set(0);
        }
    }

    public int Engineers
    {
        get => Get<int>();
        set
        {
            Set(value);
            if (value < 0)
                Set(0);
        }
    }

    public int Gunners
    {
        get => Get<int>();
        set
        {
            Set(value);
            if (value < 0)
                Set(0);
        }
    }

    public int Medics
    {
        get => Get<int>();
        set
        {
            Set(value);
            if (value < 0)
                Set(0);
        }
    }

    public int Passengers
    {
        get => Get<int>();
        set
        {
            Set(value);
            if (value < 0)
                Set(0);
        }
    }

    public int Pilots
    {
        get => Get<int>();
        set
        {
            Set(value);
            if (value < 0)
                Set(0); //We need to change the value, then change it back, to update the UI.
        }
    }

    public bool ShowDetails { get => Get<bool>(); set => Set(value); }
    public IReadOnlyList<FactionOrSpecies> SpeciesAndFactionsList => m_CharacterBuilder.FactionsAndSpecies;

    public string? SpeciesOrFaction { get => Get<string?>(); set => Set(value); }

    public int Stewards
    {
        get => Get<int>();
        set
        {
            Set(value);
            if (value < 0)
                Set(0);
        }
    }

    public void FromQueryString(Dictionary<string, StringValues> keyValuePairs)
    {
        Pilots = keyValuePairs.ParseInt("pilots");
        Astrogators = keyValuePairs.ParseInt("astrogators");
        Engineers = keyValuePairs.ParseInt("engineers");
        Medics = keyValuePairs.ParseInt("medics");
        Gunners = keyValuePairs.ParseInt("gunners");
        Stewards = keyValuePairs.ParseInt("stewards");
        Passengers = keyValuePairs.ParseInt("passengers");
        SpeciesOrFaction = keyValuePairs.ParseStringOrNull("species");
    }

    public IDictionary<string, string?> ToQueryString()
    {
        return new Dictionary<string, string?>
        {
            { "pilots", Pilots.ToString() },
            { "astrogators", Astrogators.ToString() },
            { "engineers", Engineers.ToString() },
            { "medics", Medics.ToString() },
            { "gunners", Gunners.ToString() },
            { "stewards", Stewards.ToString() },
            { "passengers", Passengers.ToString() },
            { "species", SpeciesOrFaction },
        };
    }
}
