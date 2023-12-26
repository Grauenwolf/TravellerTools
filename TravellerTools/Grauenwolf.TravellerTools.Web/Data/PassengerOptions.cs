using Microsoft.Extensions.Primitives;
using System.Collections.Immutable;
using Tortuga.Anchor.Modeling;
using Grauenwolf.TravellerTools.Shared;

namespace Grauenwolf.TravellerTools.Web.Data;

public class PassengerOptions : ModelBase
{
    public static readonly ImmutableArray<(string Name, string Code)> EditionList = ImmutableArray.Create(
            ("Mongoose 1", Edition.MGT.ToString()),
            ("Mongoose 2", Edition.MGT2.ToString()),
            ("Mongoose 2022", Edition.MGT2022.ToString())
        );

    //public bool AdvancedCharacters { get => GetDefault(false); set => Set(value); }
    public Edition SelectedEdition { get => GetDefault<Edition>(Edition.MGT2022); set => Set(value); }

    public string SelectedEditionCode
    {
        get => SelectedEdition.ToString();
        set
        {
            if (Enum.TryParse<Edition>(value, out var edition))
                SelectedEdition = edition;
            else
                SelectedEdition = Edition.MGT2;
        }
    }

    public bool ShowDetails { get => GetDefault(false); set => Set(value); }
    public bool VariablePrices { get => GetDefault(false); set => Set(value); }

    public void FromQueryString(Dictionary<string, StringValues> keyValuePairs)
    {
        SelectedEditionCode = keyValuePairs.ParseString("edition");
        VariablePrices = keyValuePairs.ParseBool("variablePrices");
    }

    public Dictionary<string, string?> ToQueryString()
    {
        return new Dictionary<string, string?>
        {
            { "edition", SelectedEditionCode },
            { "variablePrices", VariablePrices.ToString() }
        };
    }
}
