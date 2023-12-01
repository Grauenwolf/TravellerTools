using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Characters;

public class Contact(ContactType contactType, CharacterBuilderOptions characterStub)
{
    int m_Affinity;
    int m_Enmity;
    int m_Influence;
    int m_Power;

    public int Affinity { get => m_Affinity; set => m_Affinity = LimitValue(value); }

    public string AffinityDescription
    {
        get
        {
            return Affinity switch
            {
                0 => "None",
                1 => "Vaguely Well Inclined",
                2 => "Positively Inclined ",
                3 => "Very Positively Inclined",
                4 => "Loyal Friend",
                5 => "Love",
                6 => "Fanatical",
                _ => ""
            };
        }
    }

    public CharacterBuilderOptions CharacterStub { get; set; } = characterStub;

    public ContactType ContactType { get; set; } = contactType;

    public int Enmity { get => m_Enmity; set => m_Enmity = LimitValue(value); }

    public string EnmityDescription
    {
        get
        {
            return Enmity switch
            {
                0 => "None",
                1 => "Mistrustful",
                2 => "Negatively Inclined",
                3 => "Very Negatively Inclined",
                4 => "Hatred",
                5 => "Bitter Hatred",
                6 => "Blinded by Hate",
                _ => ""
            };
        }
    }

    public List<string> History { get; } = new();

    public int Influence { get => m_Influence; set => m_Influence = LimitValue(value); }

    public string InfluenceDescription
    {
        get
        {
            return Influence switch
            {
                0 => "No Influence",
                1 => "Little Influence",
                2 => "Some Influence",
                3 => "Influential",
                4 => "Highly Influential",
                5 => "Extremely Influential",
                6 => "Kingmaker",
                _ => ""
            };
        }
    }

    public int Power { get => m_Power; set => m_Power = LimitValue(value); }

    public string PowerDescription
    {
        get
        {
            return Power switch
            {
                0 => "Powerless",
                1 => "Weak",
                2 => "Useful",
                3 => "Moderately Powerful",
                4 => "Powerful",
                5 => "Very Powerful",
                6 => "Major Player",
                _ => ""
            };
        }
    }

    int LimitValue(int value)
    {
        return value switch
        {
            < 0 => 0,
            > 6 => 6,
            _ => value
        };
    }
}
