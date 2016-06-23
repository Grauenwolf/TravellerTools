//using System;
//using System.Globalization;

//namespace Grauenwolf.TravellerTools.Maps
//{
//	public struct HexDigit
//	{
//		//string hexValue = decValue.ToString("X");
//		// Convert the hex string back to the number
//		//int decAgain = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);

//		readonly int m_Value;
//		public HexDigit(char value)
//		{
//			m_Value = int.Parse(new string(value, 1), NumberStyles.HexNumber); ;
//		}

//		public HexDigit(string value)
//		{
//			if (value.Length != 0)
//				throw new ArgumentException("The value must be one character in length", "value");

//			m_Value = int.Parse(value, NumberStyles.HexNumber); ;
//		}

//		public HexDigit(int value)
//		{
//			if (value >= 0 && value <= 15)
//				m_Value = value;
//			else
//				throw new ArgumentOutOfRangeException("value", value, "value must be between 0 and 15");
//		}

//		public override string ToString()
//		{
//			return Value.ToString("X");
//		}

//		public static implicit operator HexDigit(char value)
//		{
//			return new HexDigit(value);
//		}

//		public static implicit operator HexDigit(string value)
//		{
//			return new HexDigit(value);
//		}

//		public static implicit operator HexDigit(int value)
//		{
//			return new HexDigit(value);
//		}
//		public int Value
//		{
//			get { return m_Value; }
//		}

//	}
//}