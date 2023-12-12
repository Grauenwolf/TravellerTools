namespace Grauenwolf.TravellerTools.Characters.Careers.Precareers;

class ArmyAcademy(CharacterBuilder characterBuilder) : MilitaryAcademy("Army Academy", characterBuilder)
{
    protected override string Branch => "Army";
    protected override string QualifyAttribute => "End";
    protected override int QualifyTarget => 7;

    protected override SkillTemplateCollection GetBasicSkills()
    {
        var skillList = new SkillTemplateCollection();

        skillList.AddRange(SpecialtiesFor("Drive"));
        skillList.Add("Vacc Suit");
        skillList.AddRange(SpecialtiesFor("Athletics"));
        skillList.AddRange(SpecialtiesFor("Gun Combat"));
        skillList.Add("Recon");
        skillList.AddRange(SpecialtiesFor("Melee"));
        skillList.AddRange(SpecialtiesFor("Heavy Weapons"));

        return skillList;
    }
}
