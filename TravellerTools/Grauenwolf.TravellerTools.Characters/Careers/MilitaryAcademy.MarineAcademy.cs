namespace Grauenwolf.TravellerTools.Characters.Careers;

class MarineAcademy(Book book) : MilitaryAcademy("Marine Academy", book)
{
    protected override string QualifyAttribute => "End";
    protected override int QualifyTarget => 8;
    protected override MilitaryCareer Stub { get; } = new MarineStub(book);

    class MarineStub(Book book) : Marine("", book)
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
