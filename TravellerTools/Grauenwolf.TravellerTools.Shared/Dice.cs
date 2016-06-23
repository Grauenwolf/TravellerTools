using System;
using System.Linq;
using Tortuga.Anchor;

namespace Grauenwolf.TravellerTools
{

    public class Dice : RandomExtended
    {
        public int D(int count, int die)
        {
            var result = 0;
            for (var i = 0; i < count; i++)
                result += Next(1, die + 1);

            return result;
        }

        public int D(int die)
        {
            return D(1, die);
        }

        public int D66()
        {
            return (Next(1, 7) * 10) + Next(1, 7);
        }

        public int D(string dieCode)
        {
            if (string.IsNullOrWhiteSpace(dieCode))
                return 0;
            try
            {
                var result = 0;
                var array = dieCode.ToUpperInvariant().Replace("-", "+-").Split(new[] { '+' });
                foreach (var expression in array)
                {
                    if (expression.Contains("D"))
                    {
                        var isNegative = 1;
                        var fixedExpression = expression;


                        if (expression.StartsWith("-"))
                        {
                            isNegative = -1;
                            fixedExpression = expression.Substring(1);
                        }

                        var parts = fixedExpression.Split(new[] { 'D' }, StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i)).ToArray();

                        if (parts.Length == 1)
                            result += D(parts[0], 6) * isNegative;
                        else if (parts[1] == 66)
                            result += D66() * isNegative;
                        else
                            result += D(parts[0], parts[1]) * isNegative;
                    }
                    else if (expression == "")
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
    }

}
