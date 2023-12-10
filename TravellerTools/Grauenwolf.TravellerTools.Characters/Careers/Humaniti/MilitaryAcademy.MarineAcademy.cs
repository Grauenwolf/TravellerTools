namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class MarineAcademy : MilitaryAcademy
{
    public MarineAcademy(CharacterBuilder characterBuilder) : base("Marine Academy", characterBuilder)
    {
        Stub = new MarineStub(characterBuilder);
    }

    protected override string QualifyAttribute => "End";
    protected override int QualifyTarget => 8;
    protected override MilitaryCareer Stub { get; }

    class MarineStub(CharacterBuilder characterBuilder) : Marine("", characterBuilder)
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
