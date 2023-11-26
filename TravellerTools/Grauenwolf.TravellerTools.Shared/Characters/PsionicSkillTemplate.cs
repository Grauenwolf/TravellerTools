namespace Grauenwolf.TravellerTools.Characters;

public class PsionicSkillTemplate(string name, int learningDM) : SkillTemplate(name)
{
    public int LearningDM { get; } = learningDM;
}