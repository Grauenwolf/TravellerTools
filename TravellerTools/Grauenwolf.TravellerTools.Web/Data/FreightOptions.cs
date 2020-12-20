using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Web.Data
{
    public class FreightOptions : ModelBase
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

        //public bool AdvancedMode { get => GetDefault(false); set => Set(value); }


        public Dictionary<string, string?> ToQueryString()
        {
            var result = new Dictionary<string, string?>();
            result.Add("edition", SelectedEditionCode);
            //result.Add("advancedMode", AdvancedMode.ToString());

            return result;
        }

        public void FromQueryString(Dictionary<string, StringValues> keyValuePairs)
        {
            if (keyValuePairs.TryGetValue("edition", out var editionCode))
                SelectedEditionCode = editionCode;
            //if (keyValuePairs.TryGetValue("advancedMode", out var advancedMode))
            //    AdvancedMode = bool.Parse(advancedMode);
        }

    }
}
