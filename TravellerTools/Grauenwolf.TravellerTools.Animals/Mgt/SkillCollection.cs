using System;
using System.Linq;
using Tortuga.Anchor.Collections;

namespace Grauenwolf.TravellerTools.Animals.Mgt
{
    public class SkillCollection : ObservableCollectionExtended<Skill>
    {
        public Skill this[string name]
        {
            get { return this.FirstOrDefault(x => x.Name == name); }
        }

        public void Increase(string name, int levels)
        {
            var skill = this[name];
            if (skill == null)
                Add(new Skill(name, levels));
            else
                skill.Level += levels;
        }

        public void Add(string name, int minLevel = 0)
        {
            var skill = this[name];
            if (skill == null)
                Add(new Skill(name, minLevel));
            else
                skill.Level = Math.Max(skill.Level, minLevel);
        }
    }
}