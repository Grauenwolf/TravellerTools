using System.Diagnostics.CodeAnalysis;
using Tortuga.Anchor;

namespace Grauenwolf.TravellerTools;

public class Dice : RandomExtended
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Dice"/> class.
    /// </summary>
    public Dice()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Dice"/> class.
    /// </summary>
    /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence. If a negative number is specified, the absolute value of the number is used.</param>
    public Dice(int seed) : base(seed)
    {
    }

    /// <summary>
    /// Chooses an item from the list. Each item must implement ITablePick.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list">The list.</param>
    /// <param name="dieCode">The die code.</param>
    /// <returns>T.</returns>
    /// <exception cref="System.Exception"></exception>
    public T ChooseByRoll<T>(ICollection<T> list, string dieCode) where T : ITablePick
    {
        var roll = D(dieCode);

        var result = list.SingleOrDefault(x => x.IsMatch(roll));
        if (result == null)
            throw new InvalidOperationException($"No entry for a roll of {roll}");
        return result;
    }

    /// <summary>
    /// Chooses an item from the list. Each item must implement IHasOdds.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list">The list.</param>
    /// <returns>T.</returns>
    /// <exception cref="System.Exception">This cannot happen</exception>
    public T ChooseWithOdds<T>(ICollection<T> list) where T : IHasOdds
    {
        if (list == null)
            throw new ArgumentNullException(nameof(list));

        var max = list.Sum(option => option.Odds);
        var roll = Next(1, max + 1);
        foreach (var option in list)
        {
            roll -= option.Odds;
            if (roll <= 0)
                return option;
        }
        throw new InvalidOperationException("This cannot happen");
    }

    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "D")]
    public int D(int count, int die)
    {
        var result = 0;
        for (var i = 0; i < count; i++)
            result += Next(1, die + 1);

        return result;
    }

    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "D")]
    public int D(int die) => D(1, die);

    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "D")]
    public int D(string dieCode)
    {
        if (string.IsNullOrWhiteSpace(dieCode))
            return 0;
        if (string.Equals(dieCode, "D66", StringComparison.OrdinalIgnoreCase))
            return D66();

        try
        {
            var result = 0;
            var array = dieCode.ToUpperInvariant().Replace("-", "+-").Split(new[] { '+' });
            foreach (var expression in array)
            {
                if (expression.StartsWith("D"))
                {
                    result += D(1, int.Parse(expression.Substring(1)));
                }
                else if (expression.Contains("D"))
                {
                    var isNegative = 1;
                    var fixedExpression = expression;
                    var multiplier = 1;

                    if (expression.StartsWith("-"))
                    {
                        isNegative = -1;
                        fixedExpression = expression.Substring(1);
                    }

                    if (fixedExpression.Contains("*") || fixedExpression.Contains("X"))
                    {
                        var mParts = fixedExpression.Split(new[] { '*', 'X' }, StringSplitOptions.RemoveEmptyEntries);
                        fixedExpression = mParts[0];
                        multiplier = int.Parse(mParts[1]);
                    }

                    var parts = fixedExpression.Split(new[] { 'D' }, StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i)).ToArray();

                    if (parts.Length == 1)
                        result += D(parts[0], 6) * isNegative * multiplier;
                    else if (parts[1] == 66)
                        result += D66() * isNegative * multiplier;
                    else
                        result += D(parts[0], parts[1]) * isNegative * multiplier;
                }
                else if (expression.Length == 0)
                {
                    //skip
                }
                else
                {
                    result += int.Parse(expression);
                }
            }

            return result;
        }
        catch (FormatException ex)
        {
            throw new ArgumentException(string.Format("Cannot parse '{0}'", dieCode), "dieCode", ex);
        }
    }

    public int D66() => (Next(1, 7) * 10) + Next(1, 7);

    /// <summary>
    /// Returns the next boolean.
    /// </summary>
    /// <returns></returns>
    public bool NextBoolean() => D(2) == 2;

    /// <summary>
    /// Rolls 2D6, returns true if equal or greater than target.
    /// </summary>
    /// <param name="target">The target.</param>
    public bool RollHigh(int target) => D(2, 6) >= target;

    /// <summary>
    /// Rolls 2D6, returns true if equal or greater than target.
    /// </summary>
    /// <param name="dm">The dm.</param>
    /// <param name="target">The target.</param>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "dm")]
    public bool RollHigh(int dm, int target) => D(2, 6) + dm >= target;

    /// <summary>
    /// Rolls 2D6, returns true if equal or greater than target.
    /// </summary>
    /// <param name="dm">The dm.</param>
    /// <param name="target">The target.</param>
    /// <param name="isPrecheck">If true, use 8 instead of rolling.</param>
    public bool RollHigh(int dm, int target, bool isPrecheck)
    {
        if (isPrecheck)
            return 8 + dm >= target;
        else
            return D(2, 6) + dm >= target;
    }
}
