using Grauenwolf.TravellerTools.Characters;

namespace Grauenwolf.TravellerTools.Encounters;

public class Encounter
{
    public Encounter()
    {
    }

    public Encounter(string description)
    {
        Description = description;
    }

    public List<EncounterCharacter> Characters { get; } = new();

    public string? Description { get; set; }

    public string? Title { get; set; }

    public void Add(Encounter encounterToMerge)
    {
        if (encounterToMerge.Description != null)
            Add(encounterToMerge.Description);
        foreach (var item in encounterToMerge.Characters)
            Add(item);
    }

    public void Add(string newContent, Encounter encounterToMerge)
    {
        Add(newContent);
        Add(encounterToMerge);
    }

    public void Add(string encounterRoll, Character character)
    {
        if (character is null) throw new ArgumentNullException(nameof(character));

        Add(new EncounterCharacter(encounterRoll, character));
    }

    public void Add(EncounterCharacter encounterCharacter)
    {
        if (encounterCharacter is null) throw new ArgumentNullException(nameof(encounterCharacter));
        if (encounterCharacter.Character is null) throw new ArgumentNullException(nameof(encounterCharacter.Character));

        Characters.Add(encounterCharacter);
    }

    public void Add(string newContent)
    {
        if (string.IsNullOrEmpty(Description))
            Description = newContent;
        else if (Description.EndsWith(" "))
            Description += newContent;
        else
            Description += " " + newContent;
    }

    public void Add(string newContent, string encounterRole, Character character)
    {
        Add(newContent);
        Add(encounterRole, character);
    }

    public void Merge(string newContent, Encounter encounterToMerge)
    {
        Add(newContent);

        if (encounterToMerge.Description != null)
            Add(encounterToMerge.Description);
        foreach (var item in encounterToMerge.Characters)
        {
            Add(item.EncounterRole);
            Add(item);
        }
    }
}
