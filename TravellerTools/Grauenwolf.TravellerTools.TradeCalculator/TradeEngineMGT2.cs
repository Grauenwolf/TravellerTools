using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Names;

namespace Grauenwolf.TravellerTools.TradeCalculator;

public class TradeEngineMgt2(TravellerMapService mapService, string dataPath, NameGenerator nameGenerator, Characters.CharacterBuilderLocator characterBuilderLocator) : TradeEngine(mapService, dataPath, nameGenerator, characterBuilderLocator)
{
    protected override string DataFileName => "TradeGoods-MGT2.xml";

    public override FreightList Freight(World origin, World destination, Dice dice, bool variableFees)
    {
        if (origin == null)
            throw new ArgumentNullException(nameof(origin), $"{nameof(origin)} is null.");

        if (destination == null)
            throw new ArgumentNullException(nameof(destination), $"{nameof(destination)} is null.");

        if (dice == null)
            throw new ArgumentNullException(nameof(dice), $"{nameof(dice)} is null.");

        var result = new FreightList();

        var traffic = 0;
        var incidentalDM = 2;
        var minorDM = 0;
        var majorDM = -4;

        if (origin.PopulationCode.Value <= 1)
            traffic += -4;
        else if (origin.PopulationCode.Value == 6 || origin.PopulationCode.Value == 7)
            traffic += 2;
        else if (origin.PopulationCode.Value >= 8)
            traffic += 4;

        if (origin.TechCode.Value <= 6) traffic += -1;
        if (origin.TechCode.Value >= 9) traffic += 2;

        if (origin.ContainsRemark("A")) traffic += -2;
        if (origin.ContainsRemark("R")) traffic += -6;

        result.Incidental = dice.D(FreightTraffic(traffic + incidentalDM, dice));
        result.Minor = dice.D(FreightTraffic(traffic + minorDM, dice));
        result.Major = dice.D(FreightTraffic(traffic + majorDM, dice));

        var lots = new List<FreightLot>();
        for (var i = 0; i < result.Incidental; i++)
        {
            int size = dice.D("1D6");
            int value = FreightCost(destination.JumpDistance) * size;
            lots.Add(new FreightLot(size, value));
        }

        for (var i = 0; i < result.Minor; i++)
        {
            int size = dice.D("1D6") * 5;
            int value = FreightCost(destination.JumpDistance) * size;
            lots.Add(new FreightLot(size, value));
        }

        for (var i = 0; i < result.Major; i++)
        {
            int size = dice.D("1D6") * 10;
            int value = FreightCost(destination.JumpDistance) * size;
            lots.Add(new FreightLot(size, value)); ;
        }

        AddLotDetails(origin, destination, dice, lots, variableFees);

        result.Lots.Add(GenerateMail(origin, dice, traffic));
        result.Lots.AddRange(lots.OrderByDescending(f => f.Size));

        return result;
    }

    /*
    public override World GenerateRandomWorld()
    {
        var dice = new Dice();

        EHex starportCode; // UWP[0]
        EHex sizeCode; // UWP[1]
        EHex atmosphereCode; // UWP[2]
        EHex hydrographicsCode; // UWP[3]
        EHex populationCode; // UWP[4]
        EHex governmentCode; // UWP[5]
        EHex lawCode; // UWP[6]
        EHex techCode; // UWP[8]

        sizeCode = Max(dice.D(2, 6) - 2, 0);

        atmosphereCode = Max(dice.D(2, 6) - 7 + sizeCode.Value, 0);

        if (sizeCode <= 1)
            hydrographicsCode = 0;
        else if (atmosphereCode <= 1 || atmosphereCode.Between('A', 'C'))
            hydrographicsCode = dice.D(2, 6) - 7 + atmosphereCode.Value - 4;
        else
            hydrographicsCode = dice.D(2, 6) - 7;

        hydrographicsCode = Max(hydrographicsCode.Value, 0);

        populationCode = Max(dice.D(2, 6) - 2, 0);
        governmentCode = Max(dice.D(2, 6) - 7 + populationCode.Value, 0);
        lawCode = Max(dice.D(2, 6) - 7 + governmentCode.Value, 0);

        var starportDM = 0;
        if (populationCode >= 10)
            starportDM += 2;
        else if (populationCode >= 8)
            starportDM += 1;
        else if (populationCode <= 2)
            starportDM -= 2;
        else if (populationCode <= 4)
            starportDM -= 1;

        var starportRoll = dice.D(2, 6) + starportDM;
        if (starportRoll <= 2)
            starportCode = 'X';
        else if (starportRoll == 3 || starportRoll == 4)
            starportCode = 'E';
        else if (starportRoll == 5 || starportRoll == 6)
            starportCode = 'D';
        else if (starportRoll == 7 || starportRoll == 8)
            starportCode = 'C';
        else if (starportRoll == 9 || starportRoll == 10)
            starportCode = 'B';
        else
            starportCode = 'A';

        var techDM = 0;
        if (starportCode == 'A')
            techDM += 6;
        else if (starportCode == 'B')
            techDM += 4;
        else if (starportCode == 'C')
            techDM += 2;
        else if (starportCode == 'X')
            techDM -= 4;

        if (sizeCode <= 1)
            techDM += 2;
        else if (sizeCode <= 4)
            techDM += 1;

        if (atmosphereCode <= 3)
            techDM += 1;
        else if (atmosphereCode >= 10)
            techDM += 1;

        if (hydrographicsCode == 0)
            techDM += 1;
        else if (hydrographicsCode == 9)
            techDM += 1;
        else if (hydrographicsCode == 10)
            techDM += 2;

        if (populationCode.Between(1, 5))
            techDM += 1;
        else if (populationCode == 9)
            techDM += 1;
        else if (populationCode == 9)
            techDM += 2;
        else if (populationCode == 10)
            techDM += 4;

        if (governmentCode == 0)
            techDM += 1;
        else if (governmentCode == 5)
            techDM += 1;
        else if (governmentCode == 7)
            techDM += 2;
        else if (governmentCode == 13 || governmentCode == 14)
            techDM += -2;

        techCode = Max(dice.D(1, 6) + techDM, 0);

        if (atmosphereCode.Between(0, 1) && techCode < 8)
            techCode = 8;
        else if (atmosphereCode.Between(2, 3) && techCode < 5)
            techCode = 5;
        else if ((atmosphereCode == 4 || atmosphereCode == 7 || atmosphereCode == 9) && techCode < 3)
            techCode = 3;
        else if (atmosphereCode == 10 && techCode < 8)
            techCode = 8;
        else if (atmosphereCode == 11 && techCode < 9)
            techCode = 9;
        else if (atmosphereCode == 12 && techCode < 10)
            techCode = 10;
        else if ((atmosphereCode == 13 || atmosphereCode == 14) && techCode < 5)
            techCode = 5;
        else if (atmosphereCode == 15 && techCode < 8)
            techCode = 8;

        var uwp = $"{starportCode}{sizeCode}{atmosphereCode}{hydrographicsCode}{populationCode}{governmentCode}{lawCode}-{techCode}";
        return new World(uwp, "Origin", 0, TasZone.Green);
    }
    */

    public override PassengerList Passengers(World origin, World destination, Dice random, bool variablePrice)
    {
        var baseDM = 0;
        var lowDM = 1;
        var basicDM = 0;
        var middleDM = 0;
        var highDM = -4;

        if (origin.PopulationCode.Value <= 1)
            baseDM += -4;
        else if (origin.PopulationCode.Value == 6 || origin.PopulationCode.Value == 7)
            baseDM += 1;
        else if (origin.PopulationCode.Value >= 8)
            baseDM += 3;

        switch (origin.StarportCode.ToString())
        {
            case "A": baseDM += 2; break;
            case "B": baseDM += 1; break;
            case "E": baseDM += -1; break;
            case "X": baseDM += -3; break;
        }

        if (origin.ContainsRemark("A")) baseDM += 1;
        if (origin.ContainsRemark("R")) baseDM += -4;

        var result = new PassengerList();
        result.LowPassengers = random.D(PassengerTraffic(baseDM + lowDM, random));
        result.BasicPassengers = random.D(PassengerTraffic(baseDM + basicDM, random));
        result.MiddlePassengers = random.D(PassengerTraffic(baseDM + middleDM, random));
        result.HighPassengers = random.D(PassengerTraffic(baseDM + highDM, random));

        var highTicket = destination.JumpDistance switch
        {
            1 => 8_500M,
            2 => 12_000M,
            3 => 20_000M,
            4 => 41_000M,
            5 => 45_000M,
            _ => 470_000M,
        };
        var middleTicket = destination.JumpDistance switch
        {
            1 => 6_200M,
            2 => 9_000M,
            3 => 15_000M,
            4 => 31_000M,
            5 => 34_000M,
            _ => 350_000M,
        };
        var basicTicket = destination.JumpDistance switch
        {
            1 => 2_200M,
            2 => 2_900M,
            3 => 4_400M,
            4 => 8_600M,
            5 => 9_400M,
            _ => 93_000M,
        };
        var lowTicket = destination.JumpDistance switch
        {
            1 => 700M,
            2 => 1_300M,
            3 => 2_200M,
            4 => 4_300M,
            5 => 13_000M,
            _ => 96_000M,
        };

        for (var i = 0; i < result.HighPassengers; i++)
            result.Passengers.Add(PassengerDetail(random, "High", highTicket, variablePrice));
        for (var i = 0; i < result.MiddlePassengers; i++)
            result.Passengers.Add(PassengerDetail(random, "Middle", middleTicket, variablePrice));
        for (var i = 0; i < result.BasicPassengers; i++)
            result.Passengers.Add(PassengerDetail(random, "Basic", basicTicket, variablePrice));
        for (var i = 0; i < result.LowPassengers; i++)
            result.Passengers.Add(PassengerDetail(random, "Low", lowTicket, variablePrice));

        return result;
    }

    override protected decimal PurchasePriceModifier(Dice random, int purchaseBonus, int brokerScore, out int roll)
    {
        if (random == null)
            throw new ArgumentNullException(nameof(random), $"{nameof(random)} is null.");

        roll = random.D(3, 6) + purchaseBonus + brokerScore;
        if (roll < 0)
            return 2M;

        return roll switch
        {
            0 => 1.75M,
            1 => 1.5M,
            2 => 1.35M,
            3 => 1.25M,
            4 => 1.20M,
            5 => 1.15M,
            6 => 1.1M,
            7 => 1.05M,
            8 => 1M,
            9 => .95M,
            10 => .9M,
            11 => 0.85M,
            12 => 0.8M,
            13 => 0.75M,
            14 => 0.7M,
            15 => 0.65M,
            16 => 0.60M,
            17 => 0.55M,
            18 => 0.50M,
            19 => 0.45M,
            20 => 0.4M,
            21 => 0.35M,
            22 => 0.30M,
            _ => 0.25M,
        };
    }

    protected override decimal SalePriceModifier(int roll)
    {
        return roll switch
        {
            < 0 => 0.30M,
            0 => 0.4M,
            1 => 0.45M,
            2 => 0.50M,
            3 => 0.55M,
            4 => 0.60M,
            5 => 0.65M,
            6 => 0.70M,
            7 => 0.75M,
            8 => 0.80M,
            9 => 0.85M,
            10 => 0.9M,
            11 => 1.0M,
            12 => 1.05M,
            13 => 1.10M,
            14 => 1.15M,
            15 => 1.20M,
            16 => 1.25M,
            17 => 1.30M,
            18 => 1.35M,
            19 => 1.4M,
            20 => 1.45M,
            21 => 1.50M,
            22 => 1.55M,
            _ => 1.60M,
        };
    }

    override protected decimal SalePriceModifier(Dice random, int saleBonus, int brokerScore, out int roll)
    {
        if (random == null)
            throw new ArgumentNullException(nameof(random), $"{nameof(random)} is null.");

        roll = random.D(3, 6) + saleBonus + brokerScore;

        return SalePriceModifier(roll);
    }

    static int FreightCost(int distance)
    {
        return distance switch
        {
            1 => 1000,
            2 => 1600,
            3 => 3000,
            4 => 7000,
            5 => 7700,
            6 => 86000,
            _ => 0,
        };
    }

    static string FreightTraffic(int modifier, Dice random)
    {
        var roll = random.D(2, 6) + modifier;

        return roll switch
        {
            <= 1 => "0",
            <= 3 => "1D",
            <= 5 => "2D",
            <= 8 => "3D",
            <= 11 => "4D",
            <= 14 => "5D",
            16 => "6D",
            17 => "7D",
            18 => "8D",
            19 => "9D",
            _ => "10D"
        };
    }

    static string PassengerTraffic(int modifier, Dice random)
    {
        var roll = random.D(2, 6) + modifier;

        return roll switch
        {
            <= 1 => "0",
            <= 3 => "1D",
            <= 6 => "2D",
            <= 10 => "3D",
            <= 13 => "4D",
            <= 15 => "5D",
            16 => "6D",
            17 => "7D",
            18 => "8D",
            19 => "9D",
            _ => "10D"
        };
    }
}
