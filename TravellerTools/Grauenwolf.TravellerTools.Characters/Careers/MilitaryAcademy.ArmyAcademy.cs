namespace Grauenwolf.TravellerTools.Characters.Careers;

class ArmyAcademy(Book book) : MilitaryAcademy("Army Academy", book)
{
    protected override string QualifyAttribute => "End";
    protected override int QualifyTarget => 7;
    protected override MilitaryCareer Stub { get; } = new ArmyStub(book);

    class ArmyStub(Book book) : Army("", book)
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
