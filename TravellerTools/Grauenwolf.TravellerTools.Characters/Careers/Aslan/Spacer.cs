namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

abstract class Spacer(string assignment, CharacterBuilder characterBuilder) : NormalCareer("Spacer", assignment, characterBuilder) {
    internal override bool RankCarryover => true;
    public override string? Source => "Aliens of Charted Space 1, page 34";

}
