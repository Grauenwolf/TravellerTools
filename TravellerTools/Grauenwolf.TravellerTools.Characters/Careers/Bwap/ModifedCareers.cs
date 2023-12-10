namespace Grauenwolf.TravellerTools.Characters.Careers.Bwap;

class ArmySupport(CharacterBuilder characterBuilder) : Humaniti.ArmySupport(characterBuilder)
{
    protected override int QualifyDM => -1;
}

class Cavalry(CharacterBuilder characterBuilder) : Humaniti.Cavalry(characterBuilder)
{
    protected override int QualifyDM => -1;
}

class Corporate(CharacterBuilder characterBuilder) : Humaniti.Corporate(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice) => true;
}

class Courier(CharacterBuilder characterBuilder) : Humaniti.Courier(characterBuilder)
{
    protected override int QualifyDM => 2;
}

class Explorer(CharacterBuilder characterBuilder) : Humaniti.Explorer(characterBuilder)
{
    protected override int QualifyDM => 1;
}

class FreeTrader(CharacterBuilder characterBuilder) : Humaniti.FreeTrader(characterBuilder)
{
    protected override int QualifyDM => 2;
}

class GroundAssault(CharacterBuilder characterBuilder) : Humaniti.GroundAssault(characterBuilder)
{
    protected override int QualifyDM => -3;
}

class Infantry(CharacterBuilder characterBuilder) : Humaniti.Infantry(characterBuilder)
{
    protected override int QualifyDM => -1;
}

class MarineSupport(CharacterBuilder characterBuilder) : Humaniti.MarineSupport(characterBuilder)
{
    protected override int QualifyDM => -3;
}

class MerchantMarine(CharacterBuilder characterBuilder) : Humaniti.MerchantMarine(characterBuilder)
{
    protected override int QualifyDM => 2;
}

class StarMarine(CharacterBuilder characterBuilder) : Humaniti.StarMarine(characterBuilder)
{
    protected override int QualifyDM => -3;
}

class Surveyor(CharacterBuilder characterBuilder) : Humaniti.Surveyor(characterBuilder)
{
    protected override int QualifyDM => 1;
}

class Broker(CharacterBuilder characterBuilder) : Humaniti.Broker(characterBuilder)
{
    protected override int QualifyDM => 1;
}
