using Grauenwolf.TravellerTools.Characters.Careers.Precareers;

namespace Grauenwolf.TravellerTools.Characters.Careers.ImperiumDolphin;

class DolphinMilitaryAcademy(CharacterBuilder characterBuilder) : MilitaryAcademy("Dolphin Military Academy", characterBuilder)
{
    protected override string Branch => "Dolphin Military";
    protected override string QualifyAttribute => "Edu";
    protected override int QualifyTarget => 6;

    protected override SkillTemplateCollection GetBasicSkills(Character character)
    {
        var skillList = new SkillTemplateCollection();

        skillList.AddRange(SpecialtiesFor(character, "Athletics"));
        skillList.Add("Vacc Suit");
        skillList.AddRange(SpecialtiesFor(character, "Electronics"));
        skillList.AddRange(SpecialtiesFor(character, "Gun Combat"));
        skillList.Add("Stealth");

        return skillList;
    }
}
