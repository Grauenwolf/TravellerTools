namespace Grauenwolf.TravellerTools.Characters.Careers.ImperiumDolphin;

class ArmySupport(CharacterBuilder characterBuilder) : Humaniti.ArmySupport(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class Artist(CharacterBuilder characterBuilder) : Humaniti.Artist(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class Cavalry(CharacterBuilder characterBuilder) : Humaniti.Cavalry(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class Colonist(CharacterBuilder characterBuilder) : Humaniti.Colonist(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class Corporate(CharacterBuilder characterBuilder) : Humaniti.Corporate(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class CorporateAgent(CharacterBuilder characterBuilder) : Humaniti.CorporateAgent(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class Courier(CharacterBuilder characterBuilder) : Humaniti.Courier(characterBuilder)
{
    protected override int QualifyDM => 1;

    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class Enforcer(CharacterBuilder characterBuilder) : Humaniti.Enforcer(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class EngineerGunner(CharacterBuilder characterBuilder) : Humaniti.EngineerGunner(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class FieldResearcher(CharacterBuilder characterBuilder) : Humaniti.FieldResearcher(characterBuilder)
{
    protected override int QualifyDM => 1;

    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class Flight(CharacterBuilder characterBuilder) : Humaniti.Flight(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class GroundAssault(CharacterBuilder characterBuilder) : Humaniti.GroundAssault(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class HolyWarrior(CharacterBuilder characterBuilder) : Humaniti.HolyWarrior(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class Infantry(CharacterBuilder characterBuilder) : Humaniti.Infantry(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class Intelligence(CharacterBuilder characterBuilder) : Humaniti.Intelligence(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class Journalist(CharacterBuilder characterBuilder) : Humaniti.Journalist(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class LawEnforcement(CharacterBuilder characterBuilder) : Humaniti.LawEnforcement(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class LineCrew(CharacterBuilder characterBuilder) : Humaniti.LineCrew(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class MainstreamBeliever(CharacterBuilder characterBuilder) : Humaniti.MainstreamBeliever(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class MarineSupport(CharacterBuilder characterBuilder) : Humaniti.MarineSupport(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class Missionary(CharacterBuilder characterBuilder) : Humaniti.Missionary(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class Performer(CharacterBuilder characterBuilder) : Humaniti.Performer(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class Physician(CharacterBuilder characterBuilder) : Humaniti.Physician(characterBuilder)
{
    protected override int QualifyDM => 1;

    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class Pirate(CharacterBuilder characterBuilder) : Humaniti.Pirate(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class Scientist(CharacterBuilder characterBuilder) : Humaniti.Scientist(characterBuilder)
{
    protected override int QualifyDM => 1;

    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class StarMarine(CharacterBuilder characterBuilder) : Humaniti.StarMarine(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class Surveyor(CharacterBuilder characterBuilder) : Humaniti.Surveyor(characterBuilder)
{
    protected override int QualifyDM => 1;

    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class Thief(CharacterBuilder characterBuilder) : Humaniti.Thief(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class University(CharacterBuilder characterBuilder) : Precareers.University(characterBuilder)
{
    protected override int QualifyDM => -1;
}

class Worker(CharacterBuilder characterBuilder) : Humaniti.Worker(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

class Explorer(CharacterBuilder characterBuilder) : Humaniti.Explorer(characterBuilder)
{
    protected override int QualifyDM => 1;

    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}

/*
class Truther(CharacterBuilder characterBuilder) : Humaniti.Truther(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (!character.Skills.Contains("Vacc Suit"))
            return false;
        return base.Qualify(character, dice, isPrecheck);
    }
}
*/
