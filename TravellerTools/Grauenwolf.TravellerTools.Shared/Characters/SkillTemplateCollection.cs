using System;
using System.Collections.Generic;
using System.Linq;
using Tortuga.Anchor.Collections;

namespace Grauenwolf.TravellerTools.Characters
{
    public class SkillTemplateCollection : ObservableCollectionExtended<SkillTemplate>
    {

        public SkillTemplateCollection(IEnumerable<SkillTemplate> randomSkills) : base(randomSkills)
        {
        }

        public SkillTemplateCollection()
        {
        }

        public SkillTemplate this[string name]
        {
            get { return this.FirstOrDefault(x => x.Name == name); }
        }

        public SkillTemplate this[string name, string specialty]
        {
            get { return this.FirstOrDefault(x => x.Name == name && x.Specialty == specialty); }
        }

        public void AddRange(string name, IEnumerable<string> specialties)
        {
            if (specialties == null)
                throw new System.ArgumentNullException(nameof(specialties));

            foreach (var specialty in specialties)
                Add(name, specialty);
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


        public void AddRange(IEnumerable<string> skills)
        {
            if (skills == null)
                throw new ArgumentNullException(nameof(skills), $"{nameof(skills)} is null.");

            foreach (var skill in skills)
                Add(skill);
        }

        /// <summary>
        /// Adds a skill if it doesn't already exist with the indicated minimum level.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="minLevel">The minimum level.</param>
        public void Add(string name)
        {
            var skill = this[name];
            if (skill == null)
                Add(new SkillTemplate(name));
        }

        /// <summary>
        /// Removes the overlapping skills so you don't pick something that you already have.
        /// </summary>
        /// <param name="skills">The skills.</param>
        /// <param name="level">The level.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void RemoveOverlap(SkillCollection skills, int level)
        {
            if (level == 0)
            {
                for (var i = Count - 1; i >= 0; i--)
                    if (skills.Any(s => this[i].Name == s.Name))
                        RemoveAt(i);
            }
            else
            {
                for (var i = Count - 1; i >= 0; i--)
                    if (skills.Any(s => this[i].Name == s.Name && this[i].Specialty == s.Specialty && s.Level >= level))
                        RemoveAt(i);
            }
        }


        /// <summary>
        /// Adds a skill if it doesn't already exist with the indicated minimum level.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="minLevel">The minimum level.</param>
        public void Add(string name, string specialty)
        {
            var skill = this[name, specialty];
            if (skill == null)
                Add(new SkillTemplate(name, specialty));
        }

    }
}
