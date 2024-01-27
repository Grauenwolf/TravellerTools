using Grauenwolf.TravellerTools.Characters.Careers.Humaniti;
using Grauenwolf.TravellerTools.Names;

namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

/// <summary>
/// This class is uses when you want to use the Humaniti exactly. It prevents subclasses from overriding the career list and sets the CareersFrom property.
/// </summary>
public abstract class ImperiumCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilder characterBuilder) : HumanitiCharacterBuilder(dataPath, nameGenerator, characterBuilder)
{
    public sealed override string? CareersFrom => "Humaniti";

    protected sealed override CareerLists CreateCareerList() => base.CreateCareerList();
}
