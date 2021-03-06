using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Web.Data
{
    public class StoreOptions : ModelBase
    {
        public bool AutoRoll { get => GetDefault(false); set => Set(value); }
        public bool DiscountPrices { get => GetDefault(false); set => Set(value); }
        public int BrokerScore { get => GetDefault(0); set => Set(value); }
        public int StreetwiseScore { get => GetDefault(0); set => Set(value); }

        public bool WeaponsRestricted { get => GetDefault(false); set => Set(value); }
        public bool DrugsRestricted { get => GetDefault(false); set => Set(value); }
        public bool PsionicsRestricted { get => GetDefault(false); set => Set(value); }
        public bool TechnologyRestricted { get => GetDefault(false); set => Set(value); }
        public bool InformationRestricted { get => GetDefault(false); set => Set(value); }

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

        public Dictionary<string, string?> ToQueryString()
        {
            var result = new Dictionary<string, string?>();
            result.Add("autoRoll", AutoRoll.ToString());
            result.Add("discountPrices", DiscountPrices.ToString());
            result.Add("brokerScore", BrokerScore.ToString());
            result.Add("streetwiseScore", StreetwiseScore.ToString());
            result.Add("weapons", WeaponsRestricted.ToString());
            result.Add("drugs", DrugsRestricted.ToString());
            result.Add("technology", TechnologyRestricted.ToString());
            result.Add("information", InformationRestricted.ToString());
            result.Add("psionics", PsionicsRestricted.ToString());

            return result;
        }

        public void FromQueryString(Dictionary<string, StringValues> keyValuePairs)
        {
            if (keyValuePairs.TryGetValue("autoRoll", out var autoRoll))
                AutoRoll = bool.Parse(autoRoll);
            if (keyValuePairs.TryGetValue("brokerScore", out var brokerScore))
                BrokerScore = int.Parse(brokerScore);
            if (keyValuePairs.TryGetValue("streetwiseScore", out var streetwiseScore))
                StreetwiseScore = int.Parse(streetwiseScore);
            if (keyValuePairs.TryGetValue("discountPrices", out var discountPrices))
                DiscountPrices = bool.Parse(discountPrices);

            if (keyValuePairs.TryGetValue("weapons", out var weapons))
                WeaponsRestricted = bool.Parse(weapons);
            if (keyValuePairs.TryGetValue("drugs", out var drugs))
                DrugsRestricted = bool.Parse(drugs);
            if (keyValuePairs.TryGetValue("technology", out var technology))
                TechnologyRestricted = bool.Parse(technology);
            if (keyValuePairs.TryGetValue("information", out var information))
                InformationRestricted = bool.Parse(information);
            if (keyValuePairs.TryGetValue("psionics", out var psionics))
                PsionicsRestricted = bool.Parse(psionics);
        }
    }
}
