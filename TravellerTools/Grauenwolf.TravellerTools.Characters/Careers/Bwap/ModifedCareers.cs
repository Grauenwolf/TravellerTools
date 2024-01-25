namespace Grauenwolf.TravellerTools.Characters.Careers.Bwap;

class Army_Cavalry(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Army_Cavalry(speciesCharacterBuilder)
{
    protected override int QualifyDM => -1;
}

class Army_Infantry(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Army_Infantry(speciesCharacterBuilder)
{
    protected override int QualifyDM => -1;
}

class Army_Support(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Army_Support(speciesCharacterBuilder)
{
    protected override int QualifyDM => -1;
}

class Citizen_Corporate(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Citizen_Corporate(speciesCharacterBuilder)
{
    internal override bool Qualify(Character character, Dice dice, bool isPrecheck) => true;
}

class Marine_GroundAssault(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Marine_GroundAssault(speciesCharacterBuilder)
{
    protected override int QualifyDM => -3;
}

class Marine_StarMarine(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Marine_StarMarine(speciesCharacterBuilder)
{
    protected override int QualifyDM => -3;
}

class Marine_Support(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Marine_Support(speciesCharacterBuilder)
{
    protected override int QualifyDM => -3;
}

class Merchant_FreeTrader(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Merchant_FreeTrader(speciesCharacterBuilder)
{
    protected override int QualifyDM => 2;
}

class Merchant_MerchantMarine(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Merchant_MerchantMarine(speciesCharacterBuilder)
{
    protected override int QualifyDM => 2;
}

class Scout_Courier(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Scout_Courier(speciesCharacterBuilder)
{
    protected override int QualifyDM => 2;
}

class Scout_Explorer(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Scout_Explorer(speciesCharacterBuilder)
{
    protected override int QualifyDM => 1;
}

class Scout_Surveyor(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Scout_Surveyor(speciesCharacterBuilder)
{
    protected override int QualifyDM => 1;
}

class Merchant_Broker(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Merchant_Broker(speciesCharacterBuilder)
{
    protected override int QualifyDM => 1;
}
