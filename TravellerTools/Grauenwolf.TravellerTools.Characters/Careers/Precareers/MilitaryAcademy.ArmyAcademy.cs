namespace Grauenwolf.TravellerTools.Characters.Careers.Precareers;

class ArmyAcademy(SpeciesCharacterBuilder speciesCharacterBuilder) : MilitaryAcademy("Army Academy", speciesCharacterBuilder)
{
    protected override string Branch => "Army";
    protected override string QualifyAttribute => "End";
    protected override int QualifyTarget => 7;

    protected override SkillTemplateCollection GetBasicSkills(Character character)
    {
        var skillList = new SkillTemplateCollection();

        skillList.AddRange(SpecialtiesFor(character, "Drive"));
        skillList.Add("Vacc Suit");
        skillList.AddRange(SpecialtiesFor(character, "Athletics"));
        skillList.AddRange(SpecialtiesFor(character, "Gun Combat"));
        skillList.Add("Recon");
        skillList.AddRange(SpecialtiesFor(character, "Melee"));
        skillList.AddRange(SpecialtiesFor(character, "Heavy Weapons"));

        return skillList;
    }
}
