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
        public void Increase(string name, int levels)
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
        public void Increase(string name, string specialty, int levels)
        {
            var skill = this[name, specialty];
            if (skill == null)
                Add(new Skill(name, specialty, levels));
            else
                skill.Level += levels;
        }

        public void Increase(SkillTemplate skillA, int levels)
        {
            Increase(skillA.Name, skillA.Specialty, levels);
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
    }
}
