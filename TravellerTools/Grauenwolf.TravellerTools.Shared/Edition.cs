using System.Diagnostics.CodeAnalysis;

namespace Grauenwolf.TravellerTools;

[SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")]
public enum Edition
{
    Classic = 1997,
    MegaTraveller = 1986,
    NewEra = 1992,
    T4 = 1996,
    Gurps = 1998,
    T20 = 2006,
    Hero = 2007,
    Mongoose = 2008,
    T5 = 2013,
    Mongoose2 = 2016,
    Mongoose2022 = 2022,

    CT = 1997,
    MT = 1986,

    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TNE")]
    TNE = 1992,

    GT = 1998,
    TH = 2007,

    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "MGT")]
    MGT = 2008,

    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "MGT")]
    MGT2 = 2016,

    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "MGT")]
    MGT2022 = 2022
}