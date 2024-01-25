namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

abstract class Spacer(string assignment, CharacterBuilder characterBuilder) : NormalCareer("Spacer", assignment, characterBuilder) {
    internal override bool RankCarryover => true;
}
