using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Grauenwolf.TravellerTools;

[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
public class OddsTable<T> : List<OddsRow<T>>
{
    public void Add(T value, int odds) => Add(new OddsRow<T>(value, odds));

    /// <summary>
    /// Chooses an value from the list without removing it.
    /// </summary>
    public T Choose(Dice dice)
    {
        if (dice == null)
            throw new ArgumentNullException(nameof(dice), $"{nameof(dice)} is null.");

        if (Count == 0)
            throw new InvalidOperationException("The list is empty.");

        var max = this.Sum(option => option.Odds);
        var roll = dice.Next(1, max + 1);
        foreach (var option in this)
        {
            roll -= option.Odds;
            if (roll <= 0)
                return option.Value;
        }
        throw new InvalidOperationException("This cannot happen");
    }

    /// <summary>
    /// Picks a value from the list and removes it.
    /// </summary>
    public T Pick(Dice dice)
    {
        if (dice == null)
            throw new ArgumentNullException(nameof(dice), $"{nameof(dice)} is null.");

        if (Count == 0)
            throw new InvalidOperationException("The list is empty.");

        var max = this.Sum(option => option.Odds);
        var roll = dice.Next(1, max + 1);
        foreach (var option in this)
        {
            roll -= option.Odds;
            if (roll <= 0)
            {
                Remove(option);
                return option.Value;
            }
        }
        throw new InvalidOperationException("This cannot happen");
    }
}
