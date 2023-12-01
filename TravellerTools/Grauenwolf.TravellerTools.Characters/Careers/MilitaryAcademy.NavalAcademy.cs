namespace Grauenwolf.TravellerTools.Characters.Careers;

class NavalAcademy(Book book) : MilitaryAcademy("Naval Academy", book)
{
    protected override string QualifyAttribute => "Int";
    protected override int QualifyTarget => 8;
    protected override MilitaryCareer Stub { get; } = new NavyStub(book);

    class NavyStub(Book book) : Navy("", book)
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
