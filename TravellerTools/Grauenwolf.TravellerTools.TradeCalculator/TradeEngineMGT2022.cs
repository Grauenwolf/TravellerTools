﻿using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Names;

namespace Grauenwolf.TravellerTools.TradeCalculator;

public class TradeEngineMgt2022(TravellerMapService mapService, string dataPath, NameGenerator nameGenerator, Characters.CharacterBuilder characterBuilder) : TradeEngine(mapService, dataPath, nameGenerator, characterBuilder)
{
    protected override string DataFileName => "TradeGoods-MGT2.xml";

    protected override bool UseCounterpartyScore => true;

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

        traffic += origin.PopulationCode.Value switch
        {
            <= 1 => -4,
            6 => 2,
            7 => 2,
            >= 8 => 4,
            _ => 0
        };

        traffic += origin.TechCode.Value switch
        {
            <= 6 => -1,
            >= 9 => 2,
            _ => 0
        };

        if (origin.ContainsRemark("R")) traffic += -6;
        else if (origin.ContainsRemark("A")) traffic += -2;

        traffic += 1 - destination.JumpDistance;

        result.Incidental = dice.D(FreightTraffic(traffic + incidentalDM, dice));
        result.Minor = dice.D(FreightTraffic(traffic + minorDM, dice));
        result.Major = dice.D(FreightTraffic(traffic + majorDM, dice));

        var lots = new List<FreightLot>();
        for (var i = 0; i < result.Incidental; i++)
        {
            var size = dice.D("1D6");
            var value = FreightCost(destination.JumpDistance) * size;
            lots.Add(new FreightLot(size, value));
        }

        for (var i = 0; i < result.Minor; i++)
        {
            var size = dice.D("1D6") * 5;
            var value = FreightCost(destination.JumpDistance) * size;
            lots.Add(new FreightLot(size, value));
        }

        for (var i = 0; i < result.Major; i++)
        {
            var size = dice.D("1D6") * 10;
            var value = FreightCost(destination.JumpDistance) * size;
            lots.Add(new FreightLot(size, value));
        }

        AddLotDetails(origin, destination, dice, lots, variableFees);

        result.Lots.Add(GenerateMail(origin, dice, traffic));

        result.Lots.AddRange(lots.OrderByDescending(f => f.Size));

        return result;
    }

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
            1 => 9_000M,
            2 => 14_000M,
            3 => 21_000M,
            4 => 34_000M,
            5 => 60_000M,
            _ => 210_000M,
        };
        var middleTicket = destination.JumpDistance switch
        {
            1 => 6_500M,
            2 => 10_000M,
            3 => 14_000M,
            4 => 23_000M,
            5 => 40_000M,
            _ => 130_000M,
        };
        var basicTicket = destination.JumpDistance switch
        {
            1 => 2_000M,
            2 => 3_000M,
            3 => 5_000M,
            4 => 8_000M,
            5 => 14_000M,
            _ => 55_000M,
        };
        var lowTicket = destination.JumpDistance switch
        {
            1 => 700M,
            2 => 1_300M,
            3 => 2_200M,
            4 => 3_900M,
            5 => 7_200M,
            _ => 27_000M,
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
            <= -3 => 3.00M,
            -2 => 2.50M,
            -1 => 2.00M,
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
            23 => 0.25M,
            24 => 0.20M,
            _ => 0.15M,
        };
    }

    override protected decimal SalePriceModifier(Dice random, int saleBonus, int brokerScore, out int roll)
    {
        if (random == null)
            throw new ArgumentNullException(nameof(random), $"{nameof(random)} is null.");

        roll = random.D(3, 6) + saleBonus + brokerScore;

        return SalePriceModifier(roll);
    }

    override protected decimal SalePriceModifier(int roll)
    {
        return roll switch
        {
            <= -3 => 0.10M,
            -2 => 0.20M,
            -1 => 0.30M,
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
            18 => 1.40M,
            19 => 1.50M,
            20 => 1.60M,
            21 => 1.75M,
            22 => 2.00M,
            23 => 2.50M,
            24 => 3.00M,
            _ => 4.00M,
        };
    }

    static int FreightCost(int distance)
    {
        return distance switch
        {
            1 => 1000,
            2 => 1600,
            3 => 2600,
            4 => 4400,
            5 => 8500,
            6 => 32000,
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
