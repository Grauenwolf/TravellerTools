namespace Grauenwolf.TravellerTools.Maps;

public class Sector
{
    private string? m_Hex;
    int m_X;
    int m_Y;
    public string? Abbreviation { get; set; }

    public string Hex
    {
        get
        {
            if (m_Hex == null)
                m_Hex = X + "," + Y;
            return m_Hex;
        }
    }

    public string? Name => Names?[0].Text;

    public Name[]? Names { get; set; }

    public string? Tags { get; set; }

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
}