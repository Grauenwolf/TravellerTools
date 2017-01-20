using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Characters
{
    public class History : EditableObjectModelBase
    {
        public History(string text)
        {
            Text = text;
        }

        public string Text { get { return Get<string>(); } set { Set(value); } }

        public override string ToString()
        {
            return Text;
        }
    }
}
