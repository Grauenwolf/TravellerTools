namespace Grauenwolf.TravellerTools.Characters.Careers.Bwap;

class Army_Cavalry(CharacterBuilder characterBuilder) : Humaniti.Army_Cavalry(characterBuilder)
{
    protected override int QualifyDM => -1;
}

class Army_Infantry(CharacterBuilder characterBuilder) : Humaniti.Army_Infantry(characterBuilder)
{
    protected override int QualifyDM => -1;
}

class Army_Support(CharacterBuilder characterBuilder) : Humaniti.Army_Support(characterBuilder)
{
    protected override int QualifyDM => -1;
}

class Citizen_Corporate(CharacterBuilder characterBuilder) : Humaniti.Citizen_Corporate(characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck) => true;
}

class Marine_GroundAssault(CharacterBuilder characterBuilder) : Humaniti.Marine_GroundAssault(characterBuilder)
{
    protected override int QualifyDM => -3;
}

class Marine_StarMarine(CharacterBuilder characterBuilder) : Humaniti.Marine_StarMarine(characterBuilder)
{
    protected override int QualifyDM => -3;
}

class Marine_Support(CharacterBuilder characterBuilder) : Humaniti.Marine_Support(characterBuilder)
{
    protected override int QualifyDM => -3;
}

class Merchant_FreeTrader(CharacterBuilder characterBuilder) : Humaniti.Merchant_FreeTrader(characterBuilder)
{
    protected override int QualifyDM => 2;
}

class Merchant_MerchantMarine(CharacterBuilder characterBuilder) : Humaniti.Merchant_MerchantMarine(characterBuilder)
{
    protected override int QualifyDM => 2;
}

class Scout_Courier(CharacterBuilder characterBuilder) : Humaniti.Scout_Courier(characterBuilder)
{
    protected override int QualifyDM => 2;
}

class Scout_Explorer(CharacterBuilder characterBuilder) : Humaniti.Scout_Explorer(characterBuilder)
{
    protected override int QualifyDM => 1;
}

class Scout_Surveyor(CharacterBuilder characterBuilder) : Humaniti.Scout_Surveyor(characterBuilder)
{
    protected override int QualifyDM => 1;
}

class Merchant_Broker(CharacterBuilder characterBuilder) : Humaniti.Merchant_Broker(characterBuilder)
{
    protected override int QualifyDM => 1;
}
