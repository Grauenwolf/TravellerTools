namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

abstract class Outcast(string assignment, CharacterBuilder characterBuilder) : NormalCareer("Outcast", assignment, characterBuilder) {

    public override string? Source => "Aliens of Charted Space 1, page 38";
}
