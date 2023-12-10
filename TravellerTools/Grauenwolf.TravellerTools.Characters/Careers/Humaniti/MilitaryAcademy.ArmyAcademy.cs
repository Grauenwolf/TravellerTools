namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class ArmyAcademy : MilitaryAcademy
{
    public ArmyAcademy(CharacterBuilder characterBuilder) : base("Army Academy", characterBuilder)
    {
        Stub = new ArmyStub(characterBuilder);
    }

    protected override string QualifyAttribute => "End";
    protected override int QualifyTarget => 7;
    protected override MilitaryCareer Stub { get; }

    class ArmyStub(CharacterBuilder characterBuilder) : Army("", characterBuilder)
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
