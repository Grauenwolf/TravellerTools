namespace Grauenwolf.TravellerTools.Characters.Careers.Precareers;

class MarineAcademy(CharacterBuilder characterBuilder) : MilitaryAcademy("Marine Academy", characterBuilder)
{
    protected override string Branch => "Marine";
    protected override string QualifyAttribute => "End";
    protected override int QualifyTarget => 8;

    protected override SkillTemplateCollection GetBasicSkills()
    {
        var skillList = new SkillTemplateCollection();

        skillList.AddRange(SpecialtiesFor("Athletics"));
        skillList.Add("Vacc Suit");
        skillList.AddRange(SpecialtiesFor("Tactics"));
        skillList.AddRange(SpecialtiesFor("Heavy Weapons"));
        skillList.AddRange(SpecialtiesFor("Gun Combat"));
        skillList.Add("Stealth");

        return skillList;
    }
}
