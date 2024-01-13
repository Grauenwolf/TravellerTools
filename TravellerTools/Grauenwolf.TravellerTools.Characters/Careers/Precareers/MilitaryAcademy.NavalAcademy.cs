namespace Grauenwolf.TravellerTools.Characters.Careers.Precareers;

class NavalAcademy(CharacterBuilder characterBuilder) : MilitaryAcademy("Naval Academy", characterBuilder)
{
    protected override string Branch => "Navy";
    protected override string QualifyAttribute => "Int";
    protected override int QualifyTarget => 8;

    protected override SkillTemplateCollection GetBasicSkills(Character character)
    {
        var skillList = new SkillTemplateCollection();

        skillList.AddRange(SpecialtiesFor(character, "Pilot"));
        skillList.Add("Vacc Suit");
        skillList.AddRange(SpecialtiesFor(character, "Athletics"));
        skillList.AddRange(SpecialtiesFor(character, "Gunner"));
        skillList.Add("Mechanic");
        skillList.AddRange(SpecialtiesFor(character, "Gun Combat"));

        return skillList;
    }
}
