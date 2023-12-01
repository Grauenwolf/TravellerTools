using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Shared;

public class Milieu
{
    public static ImmutableArray<Milieu> MilieuList = ImmutableArray.Create(
        new Milieu("-2422 – The Interstellar Wars", "IW", -2422),
        new Milieu("0 – Early Imperium", "M0", 0),
        new Milieu("990 – Solomani Rim War", "M990", 990),
        new Milieu("1105 – The Golden Age", "M1105", 1105),
        new Milieu("1120 – The Rebellion", "M1120", 1120),
        new Milieu("1201 – The New Era", "M1201", 1201),
        new Milieu("1248 – The New, New Era", "M1248", 1248),
        new Milieu("1900 – The Far Far Future", "M1900", 1900));

    private Milieu(string name, string code, int year)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Year = year;
    }

    public static Milieu Custom { get; } = new Milieu("Custom", "UWP", -1);

    public static Milieu DefaultMilieu { get; } = FromYear(1105)!;

    public string Code { get; }

    public string Name { get; }

    public int Year { get; }

    public static Milieu? FromCode(string code) => MilieuList.FirstOrDefault(m => m.Code == code);

    public static Milieu? FromYear(int year) => MilieuList.FirstOrDefault(m => m.Year == year);
}
