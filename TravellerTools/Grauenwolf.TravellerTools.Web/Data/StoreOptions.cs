using Microsoft.Extensions.Primitives;
using Tortuga.Anchor.Modeling;
using Grauenwolf.TravellerTools.Shared;

namespace Grauenwolf.TravellerTools.Web.Data;

public class StoreOptions : ModelBase
{
    public bool AutoRoll { get => GetDefault(false); set => Set(value); }
    public int BrokerScore { get => GetDefault(0); set => Set(value); }

    public string? BrokerScoreCode
    {
        get => BrokerScore.ToString();
        set
        {
            if (int.TryParse(value, out var score))
                BrokerScore = score;
            else
                BrokerScore = 0;
        }
    }

    public bool DiscountPrices { get => GetDefault(false); set => Set(value); }
    public bool DrugsRestricted { get => GetDefault(false); set => Set(value); }
    public bool InformationRestricted { get => GetDefault(false); set => Set(value); }
    public bool PsionicsRestricted { get => GetDefault(false); set => Set(value); }
    public int StreetwiseScore { get => GetDefault(0); set => Set(value); }

    public string? StreetwiseScoreCode
    {
        get => StreetwiseScore.ToString();
        set
        {
            if (int.TryParse(value, out var score))
                StreetwiseScore = score;
            else
                StreetwiseScore = 0;
        }
    }

    public bool TechnologyRestricted { get => GetDefault(false); set => Set(value); }
    public bool WeaponsRestricted { get => GetDefault(false); set => Set(value); }

    public void FromQueryString(Dictionary<string, StringValues> keyValuePairs)
    {
        AutoRoll = keyValuePairs.ParseBool("autoRoll");
        BrokerScore = keyValuePairs.ParseInt("brokerScore");
        StreetwiseScore = keyValuePairs.ParseInt("streetwiseScore");
        DiscountPrices = keyValuePairs.ParseBool("discountPrices");

        WeaponsRestricted = keyValuePairs.ParseBool("weapons");
        DrugsRestricted = keyValuePairs.ParseBool("drugs");
        TechnologyRestricted = keyValuePairs.ParseBool("technology");
        InformationRestricted = keyValuePairs.ParseBool("information");
        PsionicsRestricted = keyValuePairs.ParseBool("psionics");
    }

    public Dictionary<string, string?> ToQueryString()
    {
        return new Dictionary<string, string?>
        {
            { "autoRoll", AutoRoll.ToString() },
            { "discountPrices", DiscountPrices.ToString() },
            { "brokerScore", BrokerScore.ToString() },
            { "streetwiseScore", StreetwiseScore.ToString() },
            { "weapons", WeaponsRestricted.ToString() },
            { "drugs", DrugsRestricted.ToString() },
            { "technology", TechnologyRestricted.ToString() },
            { "information", InformationRestricted.ToString() },
            { "psionics", PsionicsRestricted.ToString() }
        };
    }
}
