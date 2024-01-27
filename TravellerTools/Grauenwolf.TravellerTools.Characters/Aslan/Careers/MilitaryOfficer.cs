namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

abstract class MilitaryOfficer(string assignment, CharacterBuilder characterBuilder) : NormalCareer("Military Officer", assignment, characterBuilder) {

    public override string? Source => "Aliens of Charted Space 1, page 30";

}
