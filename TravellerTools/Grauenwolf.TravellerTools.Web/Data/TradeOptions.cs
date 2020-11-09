using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Web.Data
{
    public class TradeOptions : ModelBase
    {
        public static readonly ImmutableArray<(string Name, string Code)> EditionList = ImmutableArray.Create(
                ("Mongoose 1", Edition.MGT.ToString()),
                ("Mongoose 2", Edition.MGT2.ToString())
            );

        public Edition SelectedEdition { get => GetDefault<Edition>(Edition.MGT2); set => Set(value); }

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

        public bool AdvancedMode { get => GetDefault(false); set => Set(value); }
        public bool Raffle { get => GetDefault(false); set => Set(value); }
        public bool IllegalGoods { get => GetDefault(false); set => Set(value); }

        public int BrokerScore { get => GetDefault(0); set => Set(value); }
        public int StreetwiseScore { get => GetDefault(0); set => Set(value); }

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

        public Dictionary<string, string> ToQueryString()
        {
            var result = new Dictionary<string, string>();
            result.Add("edition", SelectedEditionCode);
            result.Add("advancedMode", AdvancedMode.ToString());
            result.Add("raffle", Raffle.ToString());
            result.Add("illegalGoods", IllegalGoods.ToString());
            result.Add("brokerScore", BrokerScore.ToString());
            result.Add("streetwiseScore", StreetwiseScore.ToString());

            return result;
        }

        public void FromQueryString(Dictionary<string, StringValues> keyValuePairs)
        {
            if (keyValuePairs.TryGetValue("edition", out var editionCode))
                SelectedEditionCode = editionCode;
            if (keyValuePairs.TryGetValue("advancedMode", out var advancedMode))
                AdvancedMode = bool.Parse(advancedMode);
            if (keyValuePairs.TryGetValue("raffle", out var raffle))
                Raffle = bool.Parse(raffle);
            if (keyValuePairs.TryGetValue("illegalGoods", out var illegalGoods))
                IllegalGoods = bool.Parse(illegalGoods);
            if (keyValuePairs.TryGetValue("brokerScore", out var brokerScore))
                BrokerScore = int.Parse(brokerScore);
            if (keyValuePairs.TryGetValue("streetwiseScore", out var streetwiseScore))
                StreetwiseScore = int.Parse(streetwiseScore);
        }
    }
}
