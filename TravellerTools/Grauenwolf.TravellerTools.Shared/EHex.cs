using System;
using System.Linq;

namespace Grauenwolf.TravellerTools;

public struct EHex : IEquatable<EHex>
{
    public static EHex Unknown = new('?');
    readonly int m_Value;

    public EHex(char value) => m_Value = Parse(value);

    public EHex(string value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));

        if (int.TryParse(value, out m_Value))
            return;

        if (value.Length != 1)
            throw new ArgumentException("The value must be a number or be one character in length", nameof(value));

        m_Value = Parse(value[0]);
    }

    public EHex(int value) => m_Value = value;

    public string FlexString => m_Value <= 9 ? ToString() : m_Value + "/" + ToString();

    public int Value => m_Value;

    public static implicit operator EHex(char? value) => value.HasValue ? new EHex(value.Value) : default;

    public static implicit operator EHex(char value) => new EHex(value);

    public static implicit operator EHex(int value) => new EHex(value);

    public static EHex operator -(EHex left, int right) => new EHex(left.Value - right);

    public static bool operator !=(EHex left, EHex right) => left.Value != right.Value;

    public static bool operator !=(EHex left, char right) => left != ((EHex)right);

    public static bool operator !=(EHex left, string right) => left != new EHex(right);

    public static bool operator !=(string left, EHex right) => new EHex(left) != right;

    public static EHex operator +(EHex left, int right) => new EHex(left.Value + right);

    public static bool operator <(int left, EHex right) => left < right.Value;

    public static bool operator <(EHex left, int right) => left.Value < right;

    public static bool operator <=(int left, EHex right) => left <= right.Value;

    public static bool operator <=(EHex left, int right) => left.Value <= right;

    public static bool operator ==(EHex left, EHex right) => left.Value == right.Value;

    public static bool operator ==(EHex left, char right) => left == ((EHex)right).Value;

    public static bool operator ==(EHex left, string right) => left == new EHex(right);

    public static bool operator ==(string left, EHex right) => new EHex(left) == right;

    public static bool operator >(int left, EHex right) => left > right.Value;

    public static bool operator >(EHex left, int right) => left.Value > right;

    public static bool operator >=(int left, EHex right) => left >= right.Value;

    public static bool operator >=(EHex left, int right) => left.Value >= right;

    public bool AnyOf(params int[] values) => values.Contains(Value);

    /// <summary>
    /// True if the value is within the specified range, inclusive.
    /// </summary>
    public bool Between(int low, int high) => low <= Value && Value <= high;

    public override bool Equals(object? obj) => obj is EHex eHex && Equals(eHex);

    public bool Equals(EHex other) => m_Value == other.m_Value;

    public override int GetHashCode() => m_Value.GetHashCode();

    public char ToChar()
    {
        return m_Value switch
        {
            0 => '0',
            1 => '1',
            2 => '2',
            3 => '3',
            4 => '4',
            5 => '5',
            6 => '6',
            7 => '7',
            8 => '8',
            9 => '9',
            10 => 'A',
            11 => 'B',
            12 => 'C',
            13 => 'D',
            14 => 'E',
            15 => 'F',
            16 => 'G',
            17 => 'H',
            18 => 'J',
            19 => 'K',
            20 => 'L',
            21 => 'M',
            22 => 'N',
            23 => 'P',
            24 => 'Q',
            25 => 'R',
            26 => 'S',
            27 => 'T',
            28 => 'U',
            29 => 'V',
            30 => 'W',
            31 => 'X',
            32 => 'Y',
            33 => 'Z',
            99 => '?',
            _ => '_',
        };
    }

    public override string ToString()
    {
        return m_Value switch
        {
            0 => "0",
            1 => "1",
            2 => "2",
            3 => "3",
            4 => "4",
            5 => "5",
            6 => "6",
            7 => "7",
            8 => "8",
            9 => "9",
            10 => "A",
            11 => "B",
            12 => "C",
            13 => "D",
            14 => "E",
            15 => "F",
            16 => "G",
            17 => "H",
            18 => "J",
            19 => "K",
            20 => "L",
            21 => "M",
            22 => "N",
            23 => "P",
            24 => "Q",
            25 => "R",
            26 => "S",
            27 => "T",
            28 => "U",
            29 => "V",
            30 => "W",
            31 => "X",
            32 => "Y",
            33 => "Z",
            99 => "?",
            _ => "_",
        };
    }

    static int Parse(char value)
    {
        return (char.ToUpperInvariant(value)) switch
        {
            '0' => 0,
            '1' => 1,
            '2' => 2,
            '3' => 3,
            '4' => 4,
            '5' => 5,
            '6' => 6,
            '7' => 7,
            '8' => 8,
            '9' => 9,
            'A' => 10,
            'B' => 11,
            'C' => 12,
            'D' => 13,
            'E' => 14,
            'F' => 15,
            'G' => 16,
            'H' => 17,
            'J' => 18,
            'K' => 19,
            'L' => 20,
            'M' => 21,
            'N' => 22,
            'P' => 23,
            'Q' => 24,
            'R' => 25,
            'S' => 26,
            'T' => 27,
            'U' => 28,
            'V' => 29,
            'W' => 30,
            'X' => 31,
            'Y' => 32,
            'Z' => 33,
            '?' => 99,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Cannot parse EHex value."),
        };
    }
}
