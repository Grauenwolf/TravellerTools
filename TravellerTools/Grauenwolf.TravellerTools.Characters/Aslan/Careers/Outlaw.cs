namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

abstract class Outlaw(string assignment, CharacterBuilder characterBuilder) : NormalCareer("Outlaw", assignment, characterBuilder) {
    internal override bool RankCarryover => true;
    public override string? Source => "Aliens of Charted Space 1, page 40";


}
