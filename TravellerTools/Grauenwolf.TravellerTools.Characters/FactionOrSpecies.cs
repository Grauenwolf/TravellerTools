namespace Grauenwolf.TravellerTools.Characters;

public class FactionOrSpecies(string key, string displayText, bool isFaction)
{
    public string DisplayText { get; } = displayText;
    public bool IsFaction { get; } = isFaction;
    public string Key { get; } = key;
}
