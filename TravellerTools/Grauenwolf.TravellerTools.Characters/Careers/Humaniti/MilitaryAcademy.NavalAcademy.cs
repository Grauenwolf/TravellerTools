namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class NavalAcademy : MilitaryAcademy
{
    public NavalAcademy(CharacterBuilder characterBuilder) : base("Naval Academy", characterBuilder)
    {
        Stub = new NavyStub(characterBuilder);
    }

    protected override string QualifyAttribute => "Int";
    protected override int QualifyTarget => 8;
    protected override MilitaryCareer Stub { get; }

    class NavyStub(CharacterBuilder characterBuilder) : Navy("", characterBuilder)
    {
        protected override string AdvancementAttribute => throw new NotImplementedException();

        protected override int AdvancementTarget => throw new NotImplementedException();

        protected override string SurvivalAttribute => throw new NotImplementedException();

        protected override int SurvivalTarget => throw new NotImplementedException();

        internal override void AssignmentSkills(Character character, Dice dice)
        {
            throw new NotImplementedException();
        }
    }
}
