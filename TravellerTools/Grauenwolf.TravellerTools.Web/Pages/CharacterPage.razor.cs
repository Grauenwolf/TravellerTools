using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Names;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Web.Pages
{
    partial class CharacterPage
    {
        [Inject] INameService NameService { get; set; } = null!;
        [Inject] CharacterBuilder CharacterBuilder { get; set; } = null!;

        //public int? Seed { get => Get<int?>(); set => Set(value); }

        protected override void Initialized()
        {
            //if (Navigation.TryGetQueryString("seed", out int seed))
            //    Seed = seed;
            //else
            //    Seed = (new Random()).Next();

            //Options.FromQueryString(Navigation.ParsedQueryString());

            //if (Navigation.TryGetQueryString("tasZone", out string tasZone))
            //    TasZone = tasZone;

            Model = new CharacterOptions(CharacterBuilder);
        }

        protected async Task GenerateCharacter()
        {
            if (Model == null) //this shouldn't happen.
                return;

            var dice = new Dice();
            var options = new CharacterBuilderOptions();

            var desiredSkills = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            if (!string.IsNullOrEmpty(Model.SkillA))
                desiredSkills.Add(Model.SkillA);
            if (!string.IsNullOrEmpty(Model.SkillB))
                desiredSkills.Add(Model.SkillB);
            if (!string.IsNullOrEmpty(Model.SkillC))
                desiredSkills.Add(Model.SkillC);
            if (!string.IsNullOrEmpty(Model.SkillD))
                desiredSkills.Add(Model.SkillD);

            //if (!string.IsNullOrEmpty(name))
            //{
            //    options.Name = name;
            //    options.Gender = gender;
            //}
            //else
            //{
            var temp = await NameService.CreateRandomPersonAsync(dice);
            options.Name = temp.FullName;
            options.Gender = temp.Gender;
            //}

            int? minAge = (Model.Terms > 0) ? 18 + (Model.Terms * 4) : null;
            int? maxAge = (Model.Terms > 0) ? 18 + (Model.Terms * 4) + 3 : null;
            if (minAge.HasValue && minAge == maxAge)
                options.MaxAge = maxAge;
            else if (minAge.HasValue && maxAge.HasValue)
                options.MaxAge = minAge.Value + dice.D(1, maxAge.Value - minAge.Value);
            else if (maxAge.HasValue)
                options.MaxAge = 12 + dice.D(1, maxAge.Value - 12);
            else
                options.MaxAge = 12 + dice.D(1, 60);

            options.FirstCareer = Model.FirstCareer;
            options.FirstAssignment = Model.FirstAssignment;

            //options.Seed = seed;

            if ((!string.IsNullOrEmpty(Model.FinalCareer) || !string.IsNullOrEmpty(Model.FinalAssignment) || desiredSkills.Count > 0))
            {
                //create a lot of characters, then pick the best fit.

                var preferredCharacters = new List<Character>(); //better matches
                var characters = new List<Character>(); //all

                for (int i = 0; i < 500; i++)
                {
                    var candidateCharacter = CharacterBuilder.Build(options);
                    if (!candidateCharacter.IsDead)
                    {
                        if (!string.IsNullOrEmpty(Model.FinalAssignment))
                        { //looking for a particular assignment
                            if (string.Equals(candidateCharacter.LastCareer?.Assignment, Model.FinalAssignment, StringComparison.InvariantCultureIgnoreCase)) //found that assignment
                                preferredCharacters.Add(candidateCharacter);
                        }
                        else if (!string.IsNullOrEmpty(Model.FinalCareer))
                        {
                            if (string.Equals(candidateCharacter.LastCareer?.Career, Model.FinalCareer, StringComparison.InvariantCultureIgnoreCase))
                                preferredCharacters.Add(candidateCharacter); //found the career
                        }
                    }

                    characters.Add(candidateCharacter);
                }

                if (preferredCharacters.Count > 0)
                {
                    //choose the one with the most skills
                    var sortedList = preferredCharacters.Select(c => new
                    {
                        Character = c,
                        Suitability =
                        c.Skills.Where(s => desiredSkills.Contains(s.Name) || desiredSkills.Contains(s.Specialty)).Count()
                        + (c.IsDead ? -100.00 : 0.00)
                    }).OrderByDescending(x => x.Suitability).ToList();

                    Characters.Insert(0, sortedList.First().Character);
                }
                else
                {
                    //No character's last career was the requested one. Choose the one who spend the most time in the desired career.
                    var sortedList = characters.Select(c => new
                    {
                        Character = c,
                        Suitability =
                        (c.CareerHistory.Where(ch => string.Equals(ch.Assignment, Model.FinalAssignment, StringComparison.InvariantCultureIgnoreCase)).Sum(ch => ch.Terms * 5.0) / c.CurrentTerm)
                        + (c.CareerHistory.SingleOrDefault(ch => string.Equals(ch.Career, Model.FinalCareer, StringComparison.InvariantCultureIgnoreCase))?.Terms * 1.0 ?? 0.00) / c.CurrentTerm
                        + c.Skills.Where(s => desiredSkills.Contains(s.Name) || desiredSkills.Contains(s.Specialty)).Count()
                        + (c.IsDead ? -100.00 : 0.00)
                    }).OrderByDescending(x => x.Suitability).ToList();

                    Characters.Insert(0, sortedList.First().Character);
                }
            }
            else
                Characters.Insert(0, CharacterBuilder.Build(options));
        }

        List<Character> Characters { get; set; } = new List<Character>();
    }
}
