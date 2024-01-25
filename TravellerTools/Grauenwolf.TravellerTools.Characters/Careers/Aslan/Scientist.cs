namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

abstract class Scientist(string assignment, CharacterBuilder characterBuilder) : NormalCareer("Scientist", assignment, characterBuilder) {
    internal override bool RankCarryover => true;
}
