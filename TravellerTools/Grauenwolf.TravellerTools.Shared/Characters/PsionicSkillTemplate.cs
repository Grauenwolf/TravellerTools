namespace Grauenwolf.TravellerTools.Characters
{
	public class PsionicSkillTemplate : SkillTemplate
	{
		public PsionicSkillTemplate(string name, int learningDM) : base(name)
		{
			LearningDM = learningDM;
		}

		public int LearningDM { get; }
	}
}