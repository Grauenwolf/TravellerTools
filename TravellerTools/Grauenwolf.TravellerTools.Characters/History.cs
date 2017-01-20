using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Characters
{
    public class History : EditableObjectModelBase
    {
        public History() { }

        public History(int term, string text)
        {
            Term = term;
            Text = text;
        }

        public string Text { get { return Get<string>(); } set { Set(value); } }
        public int Term { get { return Get<int>(); } set { Set(value); } }

        public override string ToString()
        {
            return Text;
        }
    }
}
