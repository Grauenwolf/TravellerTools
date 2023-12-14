namespace Grauenwolf.TravellerTools.Characters.Careers.Precareers;

class NavalAcademy(CharacterBuilder characterBuilder) : MilitaryAcademy("Naval Academy", characterBuilder)
{
    protected override string Branch => "Navy";
    protected override string QualifyAttribute => "Int";
    protected override int QualifyTarget => 8;

    protected override SkillTemplateCollection GetBasicSkills()
    {
        var skillList = new SkillTemplateCollection();

        skillList.AddRange(SpecialtiesFor("Pilot"));
        skillList.Add("Vacc Suit");
        skillList.AddRange(SpecialtiesFor("Athletics"));
        skillList.AddRange(SpecialtiesFor("Gunner"));
        skillList.Add("Mechanic");
        skillList.AddRange(SpecialtiesFor("Gun Combat"));

        return skillList;
    }
}
