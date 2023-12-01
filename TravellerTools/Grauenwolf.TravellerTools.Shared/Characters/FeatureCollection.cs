namespace Grauenwolf.TravellerTools.Characters;

public class FeatureCollection : List<Feature>
{
    public void Add(string text)
    {
        if (this.Any(x => x.Text == text))
            return;
        Add(new Feature(text));
    }
}
