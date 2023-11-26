using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Tortuga.Anchor.Modeling;

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

    public void FromQueryString(Dictionary<string, StringValues> keyValuePairs)
    {
        if (keyValuePairs.TryGetValue("edition", out var editionCode))
            SelectedEditionCode = editionCode;
        //if (keyValuePairs.TryGetValue("advancedCharacters", out var advancedMode))
        //    AdvancedCharacters = bool.Parse(advancedMode);
    }

    public Dictionary<string, string?> ToQueryString()
    {
        var result = new Dictionary<string, string?>();
        result.Add("edition", SelectedEditionCode);
        //result.Add("advancedCharacters", AdvancedCharacters.ToString());

        return result;
    }
}