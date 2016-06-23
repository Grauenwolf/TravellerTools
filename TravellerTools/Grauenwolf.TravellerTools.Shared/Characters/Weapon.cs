using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Characters
{
    public class Weapon : EditableObjectModelBase
    {
        public string Name { get { return Get<string>(); } set { Set(value); } }
        public string Damage { get { return Get<string>(); } set { Set(value); } }

    }
}
