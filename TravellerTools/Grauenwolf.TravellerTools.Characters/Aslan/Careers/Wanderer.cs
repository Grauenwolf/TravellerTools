namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

abstract class Wanderer(string assignment, CharacterBuilder characterBuilder) : NormalCareer("Aslan Wanderer", assignment, characterBuilder) {
    public override string? Source => "Aliens of Charted Space 1, page 42";

}
