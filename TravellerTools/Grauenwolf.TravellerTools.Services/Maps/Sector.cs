namespace Grauenwolf.TravellerTools.Maps
{
    public class Sector
    {
        int m_X;
        int m_Y;
        private string m_Hex;

        public int X
        {
            get => m_X; set
            {
                m_X = value;
                m_Hex = null;
            }
        }

        public int Y
        {
            get => m_Y; set
            {
                m_Y = value;
                m_Hex = null;
            }
        }

        public string Tags { get; set; }
        public Name[] Names { get; set; }
        public string Abbreviation { get; set; }

        public string Name { get { return Names[0].Text; } }

        public string Hex
        {
            get
            {
                if (m_Hex == null)
                    m_Hex = X + "," + Y;
                return m_Hex;
            }
        }
    }
}
