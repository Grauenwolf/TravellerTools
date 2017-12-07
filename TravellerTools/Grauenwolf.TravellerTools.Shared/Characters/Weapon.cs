using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Characters
{
    public class Weapon : EditableObjectModelBase
    {
        public string Name { get => Get<string>(); set => Set(value); }
        public string Damage { get => Get<string>(); set => Set(value); }

    }

    
}
