using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Characters
{

    public class Feature : EditableObjectModelBase
    {
        public Feature(string text)
        {
            Text = text;
        }

        public string Text { get => Get<string>(); set => Set(value); }


    }


}
