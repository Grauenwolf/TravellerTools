using Microsoft.Extensions.Primitives;
using System.Collections.Immutable;
using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Web.Data;

public class TradeOptions : ModelBase
{
    public static readonly ImmutableArray<(string Name, string Code)> EditionList = ImmutableArray.Create(
            ("Mongoose 1", Edition.MGT.ToString()),
            ("Mongoose 2", Edition.MGT2.ToString()),
            ("Mongoose 2022", Edition.MGT2022.ToString())
        );

    public bool AdvancedMode { get => GetDefault(false); set => Set(value); }
    public int AgeWeeks { get => GetDefault(0); set => Set(value); }
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

    public int CounterpartyScore { get => GetDefault(2); set => Set(value); }

    public string? CounterpartyScoreCode
    {
        get => CounterpartyScore.ToString();
        set
        {
            if (int.TryParse(value, out var score))
                CounterpartyScore = score;
            else
                CounterpartyScore = 0;
        }
    }

    public int DestinationIndex { get => GetDefault(-1); set => Set(value); }

    public string? DestinationIndexCode
    {
        get => DestinationIndex.ToString();
        set
        {
            if (int.TryParse(value, out var score))
                DestinationIndex = score;
            else
                DestinationIndex = -1;
        }
    }

    public bool IllegalGoods { get => GetDefault(false); set => Set(value); }
    public bool Raffle { get => GetDefault(false); set => Set(value); }
    public Edition SelectedEdition { get => GetDefault<Edition>(Edition.MGT2022); set => Set(value); }

    public string SelectedEditionCode
    {
        get => SelectedEdition.ToString();
        set
        {
            if (Enum.TryParse<Edition>(value, out var edition))
                SelectedEdition = edition;
            else
                SelectedEdition = Edition.MGT2022;
        }
    }

    public bool SkipPriceRoll { get => GetDefault(false); set => Set(value); }
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

    public void FromQueryString(Dictionary<string, StringValues> keyValuePairs)
    {
        SelectedEditionCode = keyValuePairs.ParseString("edition");
        AdvancedMode = keyValuePairs.ParseBool("advancedMode");
        Raffle = keyValuePairs.ParseBool("raffle");
        IllegalGoods = keyValuePairs.ParseBool("illegalGoods");
        SkipPriceRoll = keyValuePairs.ParseBool("skipPriceRoll");
        BrokerScore = keyValuePairs.ParseInt("brokerScore");
        CounterpartyScore = keyValuePairs.ParseInt("counterpartyScore");
        StreetwiseScore = keyValuePairs.ParseInt("streetwiseScore");
        DestinationIndex = keyValuePairs.ParseInt("destinationIndex", -1);
        AgeWeeks = keyValuePairs.ParseInt("AgeWeeks");
    }

    public Dictionary<string, string?> ToQueryString()
    {
        return new Dictionary<string, string?>
        {
            { "edition", SelectedEditionCode },
            { "advancedMode", AdvancedMode.ToString() },
            { "raffle", Raffle.ToString() },
            { "illegalGoods", IllegalGoods.ToString() },
            { "skipPriceRoll", SkipPriceRoll.ToString() },
            { "brokerScore", BrokerScore.ToString() },
            { "streetwiseScore", StreetwiseScore.ToString() },
            { "destinationIndex", DestinationIndex.ToString() },
            { "counterpartyScore", CounterpartyScore.ToString() },
            { "ageWeeks", AgeWeeks.ToString() }
        };
    }
}
