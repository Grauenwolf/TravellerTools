﻿using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Names;

namespace Grauenwolf.TravellerTools.TradeCalculator;

public class TradeEngineMgt : TradeEngine
{
    public TradeEngineMgt(TravellerMapService mapService, string dataPath, NameGenerator nameGenerator, Characters.CharacterBuilder characterBuilder) : base(mapService, dataPath, nameGenerator, characterBuilder)
    {
    }

    protected override string DataFileName => "TradeGoods-MGT.xml";

    public override FreightList Freight(World origin, World destination, Dice dice, bool variableFees)
    {
        if (origin == null)
            throw new ArgumentNullException(nameof(origin), $"{nameof(origin)} is null.");

        if (destination == null)
            throw new ArgumentNullException(nameof(destination), $"{nameof(destination)} is null.");

        if (dice == null)
            throw new ArgumentNullException(nameof(dice), $"{nameof(dice)} is null.");

        var result = new FreightList();

        Action<string, string, string> SetValues = (low, middle, high) =>
        {
            result.Incidental = dice.D(low);
            result.Minor = dice.D(middle);
            result.Major = dice.D(high);
        };

        var traffic = origin.PopulationCode.Value;

        if (origin.ContainsRemark("Ag")) traffic += 2;
        if (origin.ContainsRemark("Ai")) traffic += -3;
        if (origin.ContainsRemark("Ba")) traffic += -99999;
        if (origin.ContainsRemark("De")) traffic += -3;
        if (origin.ContainsRemark("Fl")) traffic += -3;
        if (origin.ContainsRemark("Ga")) traffic += 2;
        if (origin.ContainsRemark("Hi")) traffic += 2;
        //if (origin.ContainsRemark("Ht")) traffic += 0;
        if (origin.ContainsRemark("IC")) traffic += -3;
        if (origin.ContainsRemark("In")) traffic += 3;
        if (origin.ContainsRemark("Lo")) traffic += -5;
        //if (origin.ContainsRemark("Lt")) traffic += 0;
        if (origin.ContainsRemark("Na")) traffic += -3;
        if (origin.ContainsRemark("NI")) traffic += -3;
        if (origin.ContainsRemark("Po")) traffic += -3;
        if (origin.ContainsRemark("Ri")) traffic += 2;
        //if (origin.ContainsRemark("Va")) traffic += 0;
        if (origin.ContainsRemark("Wa")) traffic += -3;
        if (origin.ContainsRemark("A")) traffic += 5;
        if (origin.ContainsRemark("R")) traffic += -5;

        if (destination.ContainsRemark("Ag")) traffic += 1;
        if (destination.ContainsRemark("Ai")) traffic += 1;
        if (destination.ContainsRemark("Ba")) traffic += -5;
        if (destination.ContainsRemark("De")) traffic += 0;
        if (destination.ContainsRemark("Fl")) traffic += 0;
        if (destination.ContainsRemark("Ga")) traffic += 1;
        if (destination.ContainsRemark("Hi")) traffic += 0;
        //if (destination.ContainsRemark("Ht")) traffic += 0;
        if (destination.ContainsRemark("IC")) traffic += 0;
        if (destination.ContainsRemark("In")) traffic += 2;
        if (destination.ContainsRemark("Lo")) traffic += 0;
        //if (destination.ContainsRemark("Lt")) traffic += 0;
        if (destination.ContainsRemark("Na")) traffic += 1;
        if (destination.ContainsRemark("NI")) traffic += 1;
        if (destination.ContainsRemark("Po")) traffic += -3;
        if (destination.ContainsRemark("Ri")) traffic += 2;
        //if (destination.ContainsRemark("Va")) traffic += 0;
        if (destination.ContainsRemark("Wa")) traffic += 0;
        if (destination.ContainsRemark("A")) traffic += -5;
        if (destination.ContainsRemark("R")) traffic += -99999;

        var tlDiff = Math.Abs(origin.TechCode.Value - destination.TechCode.Value);
        if (tlDiff > 5) tlDiff = 5;
        traffic -= tlDiff;

        if (traffic <= 0) SetValues("0", "0", "0");
        if (traffic == 1) SetValues("0", "1D-4", "1D-4");
        if (traffic == 2) SetValues("0", "1D-1", "1D-2");
        if (traffic == 3) SetValues("0", "1D", "1D-1");
        if (traffic == 4) SetValues("0", "1D+1", "1D");
        if (traffic == 5) SetValues("0", "1D+2", "1D+1");
        if (traffic == 6) SetValues("0", "1D+3", "1D+2");
        if (traffic == 7) SetValues("0", "1D+4", "1D+3");
        if (traffic == 8) SetValues("0", "1D+5", "1D+4");
        if (traffic == 9) SetValues("1D-2", "1D+6", "1D+5");
        if (traffic == 10) SetValues("1D", "1D+7", "1D+6");
        if (traffic == 11) SetValues("1D+1", "1D+8", "1D+7");
        if (traffic == 12) SetValues("1D+2", "1D+9", "1D+8");
        if (traffic == 13) SetValues("1D+3", "1D+10", "1D+9");
        if (traffic == 14) SetValues("1D+4", "1D+12", "1D+10");
        if (traffic == 15) SetValues("1D+5", "1D+14", "1D+11");
        if (traffic >= 16) SetValues("1D+6", "1D+16", "1D+12");

        if (result.Incidental < 0) result.Incidental = 0;
        if (result.Minor < 0) result.Minor = 0;
        if (result.Major < 0) result.Major = 0;

        var lots = new List<FreightLot>();
        for (var i = 0; i < result.Incidental; i++)
        {
            int size = dice.D("1D6");
            int value = (1000 + (destination.JumpDistance - 1 * 200)) * size;
            lots.Add(new FreightLot(size, value));
        }

        for (var i = 0; i < result.Minor; i++)
        {
            int size = dice.D("1D6") * 5;
            int value = (1000 + (destination.JumpDistance - 1 * 200)) * size;
            lots.Add(new FreightLot(size, value));
        }

        for (var i = 0; i < result.Major; i++)
        {
            int size = dice.D("1D6") * 10;
            int value = (1000 + ((destination.JumpDistance - 1) * 200)) * size;
            lots.Add(new FreightLot(size, value));
        }

        AddLotDetails(origin, destination, dice, lots, variableFees);

        result.Lots.Add(GenerateMail(origin, dice, traffic));
        result.Lots.AddRange(lots.OrderByDescending(f => f.Size));

        return result;
    }

    public override PassengerList Passengers(World origin, World destination, Dice random, bool variablePrice)
    {
        var result = new PassengerList();

        void SetValues(string low, string middle, string high)
        {
            result.LowPassengers = random.D(low);
            result.MiddlePassengers = random.D(middle);
            result.HighPassengers = random.D(high);
        }

        var traffic = origin.PopulationCode.Value;

        if (origin.ContainsRemark("Ag")) traffic += 0;
        if (origin.ContainsRemark("Ai")) traffic += 1;
        if (origin.ContainsRemark("Ba")) traffic += -5;
        if (origin.ContainsRemark("De")) traffic += -1;
        if (origin.ContainsRemark("Fl")) traffic += 0;
        if (origin.ContainsRemark("Ga")) traffic += 2;
        if (origin.ContainsRemark("Hi")) traffic += 0;
        //if (origin.ContainsRemark("Ht")) traffic += 0;
        if (origin.ContainsRemark("IC")) traffic += 1;
        if (origin.ContainsRemark("In")) traffic += 2;
        if (origin.ContainsRemark("Lo")) traffic += 0;
        //if (origin.ContainsRemark("Lt")) traffic += 0;
        if (origin.ContainsRemark("Na")) traffic += 0;
        if (origin.ContainsRemark("NI")) traffic += 0;
        if (origin.ContainsRemark("Po")) traffic += -2;
        if (origin.ContainsRemark("Ri")) traffic += -1;
        //if (origin.ContainsRemark("Va")) traffic += 0;
        if (origin.ContainsRemark("Wa")) traffic += 0;
        if (origin.ContainsRemark("A")) traffic += 2;
        if (origin.ContainsRemark("R")) traffic += 4;

        if (destination.ContainsRemark("Ag")) traffic += 0;
        if (destination.ContainsRemark("Ai")) traffic += -1;
        if (destination.ContainsRemark("Ba")) traffic += -5;
        if (destination.ContainsRemark("De")) traffic += -1;
        if (destination.ContainsRemark("Fl")) traffic += 0;
        if (destination.ContainsRemark("Ga")) traffic += 2;
        if (destination.ContainsRemark("Hi")) traffic += 4;
        //if (destination.ContainsRemark("Ht")) traffic += 0;
        if (destination.ContainsRemark("IC")) traffic += -1;
        if (destination.ContainsRemark("In")) traffic += 1;
        if (destination.ContainsRemark("Lo")) traffic += -4;
        //if (destination.ContainsRemark("Lt")) traffic += 0;
        if (destination.ContainsRemark("Na")) traffic += 0;
        if (destination.ContainsRemark("NI")) traffic += -1;
        if (destination.ContainsRemark("Po")) traffic += -1;
        if (destination.ContainsRemark("Ri")) traffic += 2;
        //if (destination.ContainsRemark("Va")) traffic += 0;
        if (destination.ContainsRemark("Wa")) traffic += 0;
        if (destination.ContainsRemark("A")) traffic += -2;
        if (destination.ContainsRemark("R")) traffic += -4;

        if (traffic <= 0) SetValues("0", "0", "0");
        if (traffic == 1) SetValues("2D-6", "1D-2", "0");
        if (traffic == 2) SetValues("2D", "1D", "1D-1D");
        if (traffic == 3) SetValues("2D", "2D-1D", "2D-2D");
        if (traffic == 4) SetValues("3D-1D", "2D-1D", "2D-1D");
        if (traffic == 5) SetValues("3D-1D", "3D-2D", "2D-1D");
        if (traffic == 6) SetValues("3D", "3D-2D", "3D-2D");
        if (traffic == 7) SetValues("3D", "3D-2D", "3D-2D");
        if (traffic == 8) SetValues("4D", "3D-1D", "3D-1D");
        if (traffic == 9) SetValues("4D", "3D", "3D-1D");
        if (traffic == 10) SetValues("5D", "3D", "3D-1D");
        if (traffic == 11) SetValues("5D", "4D", "3D");
        if (traffic == 12) SetValues("6D", "4D", "3D");
        if (traffic == 13) SetValues("6D", "4D", "4D");
        if (traffic == 14) SetValues("7D", "5D", "4D");
        if (traffic == 15) SetValues("8D", "5D", "4D");
        if (traffic >= 16) SetValues("9D", "6D", "5D");

        var highTicket = destination.JumpDistance switch
        {
            1 => 6_000M,
            2 => 12_000M,
            3 => 20_000M,
            4 => 30_000M,
            5 => 40_000M,
            _ => 50_000M,
        };
        var middleTicket = destination.JumpDistance switch
        {
            1 => 3_000M,
            2 => 6_000M,
            3 => 10_000M,
            4 => 15_000M,
            5 => 20_000M,
            _ => 25_000M,
        };

        var lowTicket = destination.JumpDistance switch
        {
            1 => 1_000M,
            2 => 1_200M,
            3 => 1_400M,
            4 => 1_600M,
            5 => 1_800M,
            _ => 2_000M,
        };

        for (var i = 0; i < result.HighPassengers; i++)
            result.Passengers.Add(PassengerDetail(random, "High", highTicket, variablePrice));
        for (var i = 0; i < result.MiddlePassengers; i++)
            result.Passengers.Add(PassengerDetail(random, "Middle", middleTicket, variablePrice));
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
            return 4M;

        return roll switch
        {
            0 => 3M,
            1 => 2M,
            2 => 1.75M,
            3 => 1.5M,
            4 => 1.35M,
            5 => 1.25M,
            6 => 1.2M,
            7 => 1.15M,
            8 => 1.1M,
            9 => 1.05M,
            10 => 1M,
            11 => 0.95M,
            12 => 0.9M,
            13 => 0.85M,
            14 => 0.8M,
            15 => 0.75M,
            16 => 0.7M,
            17 => 0.65M,
            18 => 0.55M,
            19 => 0.5M,
            20 => 0.4M,
            _ => 0.25M,
        };
    }

    protected override decimal SalePriceModifier(int roll)
    {
        return roll switch
        {
            < 0 => 0.25M,
            0 => 0.45M,
            1 => 0.50M,
            2 => 0.55M,
            3 => 0.60M,
            4 => 0.65M,
            5 => 0.75M,
            6 => 0.80M,
            7 => 0.85M,
            8 => 0.90M,
            9 => 0.95M,
            10 => 1M,
            11 => 1.05M,
            12 => 1.1M,
            13 => 1.15M,
            14 => 1.2M,
            15 => 1.25M,
            16 => 1.35M,
            17 => 1.5M,
            18 => 1.75M,
            19 => 2M,
            20 => 3M,
            _ => 4M,
        };
    }

    override protected decimal SalePriceModifier(Dice random, int saleBonus, int brokerScore, out int roll)
    {
        if (random == null)
            throw new ArgumentNullException(nameof(random), $"{nameof(random)} is null.");

        roll = random.D(3, 6) + saleBonus + brokerScore;

        return SalePriceModifier(roll);
    }
}
