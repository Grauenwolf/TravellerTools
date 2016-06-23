using System;

namespace Grauenwolf.TravellerTools
{

	public struct EHex
	{


		readonly int m_Value;

		public EHex(char value)
		{
			m_Value = Parse(value);
		}

		static int Parse(char value)
		{
			switch (char.ToUpperInvariant(value))
			{

				case '0': return 0;
				case '1': return 1;
				case '2': return 2;
				case '3': return 3;
				case '4': return 4;
				case '5': return 5;
				case '6': return 6;
				case '7': return 7;
				case '8': return 8;
				case '9': return 9;
				case 'A': return 10;
				case 'B': return 11;
				case 'C': return 12;
				case 'D': return 13;
				case 'E': return 14;
				case 'F': return 15;
				case 'G': return 16;
				case 'H': return 17;
				case 'J': return 18;
				case 'K': return 19;
				case 'L': return 20;
				case 'M': return 21;
				case 'N': return 22;
				case 'P': return 23;
				case 'Q': return 24;
				case 'R': return 25;
				case 'S': return 26;
				case 'T': return 27;
				case 'U': return 28;
				case 'V': return 29;
				case 'W': return 30;
				case 'X': return 31;
				case 'Y': return 32;
				case 'Z': return 33;
				default: throw new ArgumentOutOfRangeException();
			}
		}

		public EHex(string value)
		{
			if (value.Length != 0)
				throw new ArgumentException("The value must be one character in length", "value");

			m_Value = Parse(value[0]); ;
		}

		public EHex(int value)
		{
			//if (value >= 0 && value <= 33)
			m_Value = value;
			//else
			//	throw new ArgumentOutOfRangeException("value", value, "value must be less than 33");
		}

		public override string ToString()
		{
			switch (m_Value)
			{
				case 0: return "0";
				case 1: return "1";
				case 2: return "2";
				case 3: return "3";
				case 4: return "4";
				case 5: return "5";
				case 6: return "6";
				case 7: return "7";
				case 8: return "8";
				case 9: return "9";
				case 10: return "A";
				case 11: return "B";
				case 12: return "C";
				case 13: return "D";
				case 14: return "E";
				case 15: return "F";
				case 16: return "G";
				case 17: return "H";
				case 18: return "J";
				case 19: return "K";
				case 20: return "L";
				case 21: return "M";
				case 22: return "N";
				case 23: return "P";
				case 24: return "Q";
				case 25: return "R";
				case 26: return "S";
				case 27: return "T";
				case 28: return "U";
				case 29: return "V";
				case 30: return "W";
				case 31: return "X";
				case 32: return "Y";
				case 33: return "Z";
				default: return "_";
			}
		}

		public static implicit operator EHex(char value)
		{
			return new EHex(value);
		}

		public static implicit operator EHex(string value)
		{
			return new EHex(value);
		}

		public static implicit operator EHex(int value)
		{
			return new EHex(value);
		}
		public int Value
		{
			get { return m_Value; }
		}

		public static EHex operator +(EHex left, int right)
		{
			return new EHex(left.Value + right);
		}

		public static EHex operator -(EHex left, int right)
		{
			return new EHex(left.Value - right);
		}

	}
}