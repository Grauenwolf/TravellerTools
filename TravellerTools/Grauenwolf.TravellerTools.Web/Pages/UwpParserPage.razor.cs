namespace Grauenwolf.TravellerTools.Web.Pages;

partial class UwpParserPage
{
    protected void GotoPlanet()
    {
        if (Model.UwpNotSelected)
            return;
        Navigation.NavigateTo($"/uwp/{Model.RawUwp}/info");
    }

    protected void GotoPlanet2()
    {
        Navigation.NavigateTo($"/uwp/{Model.CalculatedUwp}/info?tasZone={Model.TasZone}");
    }

    protected void RandomizeUwp()
    {
        var size = Max0(Roll2D6() - 2);

        var atmosphere = Max0(Roll2D6() - 7 + size);

        int hydrosphere;
        if (size <= 1)
            hydrosphere = 0;
        else if (atmosphere <= 1 || (atmosphere >= 10 && atmosphere <= 12))
            hydrosphere = Roll2D6() - 7 + atmosphere - 4;
        else
            hydrosphere = Roll2D6() - 7;
        hydrosphere = Max0(hydrosphere);

        var population = Max0(Roll2D6() - 2);
        var government = Max0(Roll2D6() - 7 + population);
        var lawfulness = Max0(Roll2D6() - 7 + government);
        var starportRoll = Roll2D6() + PopulationStarportDm(population);
        var starportCode = DetermineStarportCode(starportRoll);

        var techBonus = 0;
        techBonus += starportCode switch
        {
            'X' => -4,
            'C' => 2,
            'B' => 4,
            'A' => 6,
            _ => 0,
        };

        if (size <= 1)
            techBonus += 2;
        else if (size <= 4)
            techBonus += 1;

        if (atmosphere <= 3 || (atmosphere >= 10 && atmosphere <= 15))
            techBonus += 1;

        if (hydrosphere == 0)
            techBonus += 1;
        else if (hydrosphere == 9)
            techBonus += 1;
        else if (hydrosphere == 10)
            techBonus += 2;

        if (population >= 1 && population <= 5)
            techBonus += 1;
        else if (population == 9)
            techBonus += 1;
        else if (population == 10)
            techBonus += 4;

        if (government == 0)
            techBonus += 1;
        else if (government == 5)
            techBonus += 1;
        else if (government == 7)
            techBonus += 2;
        else if (government == 13 || government == 14)
            techBonus -= 2;

        var technology = Max0(RollD6() + techBonus);

        if (atmosphere <= 1 && technology < 8)
            technology = 8;
        else if (atmosphere <= 3 && technology < 5)
            technology = 5;
        else if ((atmosphere == 4 || atmosphere == 7 || atmosphere == 9) && technology < 3)
            technology = 3;
        else if (atmosphere == 10 && technology < 8)
            technology = 8;
        else if (atmosphere == 11 && technology < 9)
            technology = 9;
        else if (atmosphere == 12 && technology < 10)
            technology = 10;
        else if ((atmosphere == 13 || atmosphere == 14) && technology < 5)
            technology = 5;
        else if (atmosphere == 15 && technology < 8)
            technology = 8;

        Model.StarportCode = starportCode.ToString();
        Model.SizeCode = ToCode(size);
        Model.AtmosphereCode = ToCode(atmosphere);
        Model.HydrographicsCode = ToCode(hydrosphere);
        Model.PopulationCode = ToCode(population);
        Model.GovernmentCode = ToCode(government);
        Model.LawLevelCode = ToCode(lawfulness);
        Model.TechLevelCode = ToCode(technology);

        Model.RawUwp = Model.CalculatedUwp;
    }

    static int RollD6() => Random.Shared.Next(1, 7);

    static int Roll2D6() => RollD6() + RollD6();

    static int Max0(int value) => value < 0 ? 0 : value;

    static string ToCode(int value) => new EHex(value).ToString();

    static int PopulationStarportDm(int population)
    {
        if (population >= 10)
            return 2;
        if (population >= 8)
            return 1;
        if (population <= 2)
            return -2;
        if (population <= 4)
            return -1;
        return 0;
    }

    static char DetermineStarportCode(int starportRoll)
    {
        if (starportRoll <= 2)
            return 'X';
        if (starportRoll <= 4)
            return 'E';
        if (starportRoll <= 6)
            return 'D';
        if (starportRoll <= 8)
            return 'C';
        if (starportRoll <= 10)
            return 'B';
        return 'A';
    }
}
