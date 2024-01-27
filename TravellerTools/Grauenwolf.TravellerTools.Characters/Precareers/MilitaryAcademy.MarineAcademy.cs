namespace Grauenwolf.TravellerTools.Characters.Careers.Precareers;

class MarineAcademy(SpeciesCharacterBuilder speciesCharacterBuilder) : MilitaryAcademy("Marine Academy", speciesCharacterBuilder)
{
    protected override string Branch => "Marine";
    protected override string QualifyAttribute => "End";
    protected override int QualifyTarget => 8;

    protected override SkillTemplateCollection GetBasicSkills(Character character)
    {
        var skillList = new SkillTemplateCollection();

        skillList.AddRange(SpecialtiesFor(character, "Athletics"));
        skillList.Add("Vacc Suit");
        skillList.AddRange(SpecialtiesFor(character, "Tactics"));
        skillList.AddRange(SpecialtiesFor(character, "Heavy Weapons"));
        skillList.AddRange(SpecialtiesFor(character, "Gun Combat"));
        skillList.Add("Stealth");

        return skillList;
    }
}
