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
        var size = Roll2D6() - 2;

        var atmosphere = Roll2D6() - 7 + size;
        if (atmosphere < 0 || size == 0)
            atmosphere = 0;

        var hydrosphere = Roll2D6() - 7 + size;
        if (size < 2)
            hydrosphere = 0;
        else if (atmosphere < 2 || atmosphere > 8)
            hydrosphere -= 4;
        hydrosphere = Math.Clamp(hydrosphere, 0, 10);

        var population = Roll2D6() - 2;
        var government = Math.Clamp(Roll2D6() - 7 + population, 0, 13);
        var lawfulness = Math.Clamp(Roll2D6() - 7 + government, 0, 9);
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

        if (size < 2)
            techBonus += 1;
        else if (size <= 4)
            techBonus += 2;

        if (atmosphere <= 3 || (atmosphere >= 10 && atmosphere <= 15))
            techBonus += 1;

        if (hydrosphere == 9)
            techBonus += 1;
        else if (hydrosphere == 10)
            techBonus += 2;

        if (population >= 1 && population <= 5)
            techBonus += 1;
        else if (population == 9)
            techBonus += 2;
        else if (population == 10)
            techBonus += 4;

        if (government == 13)
            techBonus -= 2;
        else if (government is 0 or 5)
            techBonus += 1;

        var technology = Math.Clamp(RollD6() + techBonus, 0, 18);

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
