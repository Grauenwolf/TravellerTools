using Grauenwolf.TravellerTools.Characters.Careers.Imperium;

namespace Grauenwolf.TravellerTools.Characters.Trexen;

public class VargrImperiumCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Species => "Vargr, Imperium";

    public override string SpeciesGroup => "Vargr";

    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Vargr";

    protected override void InitialCharacterStats(Dice dice, Character character)
    {
        base.InitialCharacterStats(dice, character);
        character.Strength += -2;
        character.Dexterity += 1;
        character.Endurance += -1;
    }
}

/*
public class VargrCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : SpeciesCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Source => "Aliens of Charted Space Vol. 1, page 179";
    public override string Faction => "Vargr Extents";

    //Copy from humans
    public override ImmutableArray<Gender> Genders => throw new NotImplementedException();

    public override string Species => "Vargr";
    public override string SpeciesGroup => "Vargr";

    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Vargr";
    protected override bool AllowPsionics => true;

    protected override CareerLists CreateCareerList() => throw new NotImplementedException();

    protected override void InitialCharacterStats(Dice dice, Character character)
    {
        base.InitialCharacterStats(dice, character);
        character.Strength += -2;
        character.Dexterity += 1;
        character.Endurance += -1;
        character.SocialStanding = dice.D(6) + 2;
        character.CharismaReplacesSocialStanding = true;

        //TODO: Any place that can increase Social Standing, have it also support Charisma. Also check for advancement/commission attributes.
    }

    //Copy from humans
    protected override int RollForPsi(Character character, Dice dice) => throw new NotImplementedException();

    //TODO: increase frequency of switching jobs

    //TODO: They get their own table for this.
    public override void LifeEvent(Character character, Dice dice, CareerBase career) => base.LifeEvent(character, dice, career);

    //TODO: How do packs work?
}

*/