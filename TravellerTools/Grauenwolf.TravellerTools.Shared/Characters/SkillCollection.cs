using System;
using System.Collections.Generic;
using System.Linq;
using Tortuga.Anchor.Collections;

namespace Grauenwolf.TravellerTools.Characters
{
	public class SkillCollection : ObservableCollectionExtended<Skill>
	{
		public Skill this[string name]
		{
			get { return this.FirstOrDefault(x => x.Name == name); }
		}

		public Skill this[string name, string specialty]
		{
			get { return this.FirstOrDefault(x => x.Name == name && x.Specialty == specialty); }
		}

		/// <summary>
		/// Adds or improves the indicated skill.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="levels">The levels.</param>
		public void Increase(string name, int levels = 1)
		{
			var skill = this[name, null];
			if (skill == null)
				Add(new Skill(name, levels));
			else
				skill.Level += levels;
		}

		/// <summary>
		/// Adds or improves the indicated skill.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="levels">The levels.</param>
		public void Increase(string name, string specialty, int levels = 1)
		{
			var skill = this[name, specialty];
			if (skill == null)
				Add(new Skill(name, specialty, levels));
			else
				skill.Level += levels;
		}

		public void Increase(SkillTemplate skill, int levels = 1)
		{
			if (skill == null)
				throw new ArgumentNullException(nameof(skill), $"{nameof(skill)} is null.");

			Increase(skill.Name, skill.Specialty, levels);
		}

		/// <summary>
		/// Adds a skill if it doesn't already exist with the indicated minimum level.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="minLevel">The minimum level.</param>
		public void Add(string name, int minLevel = 0)
		{
			var skill = this[name];
			if (skill == null)
				Add(new Skill(name, minLevel));
			else
				skill.Level = Math.Max(skill.Level, minLevel);
		}

		/// <summary>
		/// Adds a skill if it doesn't already exist with the indicated minimum level.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="minLevel">The minimum level.</param>
		public void Add(string name, string specialty, int minLevel = 0)
		{
			var skill = this[name, specialty];
			if (skill == null)
				Add(new Skill(name, specialty, minLevel));
			else
				skill.Level = Math.Max(skill.Level, minLevel);
		}

		public void Add(SkillTemplate skill, int minLevel = 0)
		{
			if (skill == null)
				throw new ArgumentNullException(nameof(skill), $"{nameof(skill)} is null.");

			Add(skill.Name, skill.Specialty, minLevel);
		}

		public void Remove(string name, string specialty)
		{
			var skill = this[name, specialty];
			if (skill != null)
				Remove(skill);
		}

		public void Remove(string name)
		{
			var skill = this[name, null];
			if (skill != null)
				Remove(skill);
		}

		public int BestSkillLevel(params string[] skillNames)
		{
			if (skillNames == null || skillNames.Length == 0)
				throw new ArgumentException($"{nameof(skillNames)} is null or empty.", nameof(skillNames));

			var bestScore = -3; //unskilled penalty
			foreach (var skill in this)
			{
				foreach (var name in skillNames)
					if (skill.Name == name)
						bestScore = Math.Max(bestScore, skill.Level);

				if (skill.Name == "Jack-of-All-Trades" && bestScore < 0)
					bestScore = skill.Level - 3;
			}

			return bestScore;
		}

		public int GetLevel(string name, string specialty = null)
		{
			var skill = this[name, specialty];
			if (skill != null)
				return skill.Level;

			skill = this["Jack-of-All-Trades"];
			if (skill != null)
				return skill.Level - 3;

			return -3;
		}

		public void AddRange(List<SkillTemplate> skillList, int minLevel = 0)
		{
			if (skillList == null)
				throw new ArgumentNullException(nameof(skillList));

			foreach (var skill in skillList)
				Add(skill, minLevel);
		}

		public void Collapse()
		{
			//Remove level 0 skills if there are other skills with the same name
			var tempList = this.Where(s => s.Level == 0).ToList();
			foreach (var skill in tempList)
				if (this.Any(s => s != skill && s.Name == skill.Name))
					this.Remove(skill);
		}

		public bool Contains(string name)
		{
			return this.Any(s => s.Name == name);
		}
	}
}