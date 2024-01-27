namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

abstract class SpaceOfficer(string assignment, CharacterBuilder characterBuilder) : NormalCareer("Space Officer", assignment, characterBuilder) {
    public override string? Source => "Aliens of Charted Space 1, page 36";

}
