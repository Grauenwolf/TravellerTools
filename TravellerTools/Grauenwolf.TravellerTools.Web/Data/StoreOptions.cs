using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Web.Data
{
    public class StoreOptions : ModelBase
    {
        public bool AutoRoll { get => GetDefault(false); set => Set(value); }
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

        public Dictionary<string, string?> ToQueryString()
        {
            var result = new Dictionary<string, string?>();
            result.Add("autoRoll", AutoRoll.ToString());
            result.Add("brokerScore", BrokerScore.ToString());
            result.Add("streetwiseScore", StreetwiseScore.ToString());

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
        }
    }
}
