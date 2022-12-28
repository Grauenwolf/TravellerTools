﻿using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Names;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tortuga.Anchor;

namespace Grauenwolf.TravellerTools.Web.Pages
{
    partial class CharacterPage
    {
        [Inject] NameGenerator NameGenerator { get; set; } = null!;
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

        protected void GenerateCharacter()
        {
            if (Model == null) //this shouldn't happen.
                return;
            try
            {
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
                if (Model.Gender.IsNullOrEmpty())
                {
                    var temp = NameGenerator.CreateRandomPerson(dice);
                    options.Name = temp.FullName;
                    options.Gender = temp.Gender;
                }
                else
                {
                    var temp = NameGenerator.CreateRandomPerson(dice, Model.Gender == "M");
                    options.Name = temp.FullName;
                    options.Gender = temp.Gender;
                }
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

                bool hasDesiredStats =
                    !Model.Str.IsNullOrEmpty() || !Model.Dex.IsNullOrEmpty() || !Model.End.IsNullOrEmpty() ||
                    !Model.Int.IsNullOrEmpty() || !Model.Edu.IsNullOrEmpty() || !Model.Soc.IsNullOrEmpty();

                //options.Seed = seed;

                if ((!string.IsNullOrEmpty(Model.FinalCareer) || !string.IsNullOrEmpty(Model.FinalAssignment) || desiredSkills.Count > 0) || hasDesiredStats)
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

                    double AttributeSuitability(string? setting, int dm)
                    {
                        setting = setting?.ToUpperInvariant();

                        switch (setting)
                        {
                            case "HIGH":
                                if (dm > 0)
                                    return 10 + dm;
                                else
                                    return dm; //it's now a penalty
                            case "LOW":
                                if (dm < 0)
                                    return 10 - dm; //the - makes this into a bonus
                                else
                                    return -1 * dm; //it's now a penalty
                        }
                        return 0; //setting is blank or dm is 0
                    }

                    double Suitability(Character item, bool includeCareers)
                    {
                        var baseValue = 0.00;
                        if (item.IsDead)
                            baseValue -= 1000.0;

                        //Add a large boost for the existence of the skill
                        baseValue += item.Skills.Where(s => desiredSkills!.Contains(s.Name) || desiredSkills.Contains(s.Specialty!)).Select(s => s.Level).Count() * 10;

                        //Add a small boost for the value of the skills
                        baseValue += item.Skills.Where(s => desiredSkills!.Contains(s.Name) || desiredSkills.Contains(s.Specialty!)).Select(s => s.Level).Sum();

                        baseValue += AttributeSuitability(Model.Str, item.StrengthDM);
                        baseValue += AttributeSuitability(Model.Dex, item.DexterityDM);
                        baseValue += AttributeSuitability(Model.End, item.EnduranceDM);
                        baseValue += AttributeSuitability(Model.Int, item.IntellectDM);
                        baseValue += AttributeSuitability(Model.Edu, item.EducationDM);
                        baseValue += AttributeSuitability(Model.Soc, item.SocialStandingDM);

                        if (includeCareers)
                        {
                            baseValue += (item.CareerHistory.Where(ch => string.Equals(ch.Assignment, Model.FinalAssignment, StringComparison.InvariantCultureIgnoreCase)).Sum(ch => ch.Terms * 50.0) / item.CurrentTerm);
                            baseValue += (item.CareerHistory.SingleOrDefault(ch => string.Equals(ch.Career, Model.FinalCareer, StringComparison.InvariantCultureIgnoreCase))?.Terms * 10.0 ?? 0.00) / item.CurrentTerm;
                        }

                        return baseValue;
                    }

                    if (preferredCharacters.Count > 0)
                    {
                        //choose the one with the most skills
                        var sortedList = preferredCharacters.Select(c => new
                        {
                            Character = c,
                            Suitability = Suitability(c, false)
                        }).OrderByDescending(x => x.Suitability).ToList();

                        Characters.Insert(0, sortedList.First().Character);
                    }
                    else
                    {
                        //No character's last career was the requested one. Choose the one who spend the most time in the desired career.
                        var sortedList = characters.Select(c => new
                        {
                            Character = c,
                            Suitability = Suitability(c, true)
                        }).OrderByDescending(x => x.Suitability).ToList();

                        Characters.Insert(0, sortedList.First().Character);
                    }
                }
                else
                    Characters.Insert(0, CharacterBuilder.Build(options));
            }
            catch (Exception ex)
            {
                LogError(ex, $"Error in creating character using {nameof(GenerateCharacter)}.");
            }
        }

        List<Character> Characters { get; set; } = new List<Character>();
    }
}