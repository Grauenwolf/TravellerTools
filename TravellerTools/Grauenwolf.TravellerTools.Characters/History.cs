using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Characters
{
    public class History : EditableObjectModelBase
    {

        public History(int term, int age, string text)
        {
            Term = term;
            Age = age;
            Text = text;
        }

        public string Text { get { return Get<string>(); } set { Set(value); } }
        public int Term { get { return Get<int>(); } set { Set(value); } }
        public int Age { get { return Get<int>(); } set { Set(value); } }

        public override string ToString()
        {
            return Text;
        }
    }
}
