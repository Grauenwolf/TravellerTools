namespace Grauenwolf.TravellerTools.Characters;

public class History(int term, int age, string text)
{
    public int Age { get; set; } = age;
    public int Term { get; set; } = term;
    public string Text { get; set; } = text;

    public override string ToString() => Text;
}