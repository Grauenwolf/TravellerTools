namespace Grauenwolf.TravellerTools.Characters.Careers.AelYael;

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

class Marine_GroundAssault(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Marine_GroundAssault(speciesCharacterBuilder)
{
    protected override int QualifyDM => -1;
}

class Marine_StarMarine(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Marine_StarMarine(speciesCharacterBuilder)
{
    protected override int QualifyDM => -1;
}

class Marine_Support(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Marine_Support(speciesCharacterBuilder)
{
    protected override int QualifyDM => -1;
}

class Navy_EngineerGunner(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Navy_EngineerGunner(speciesCharacterBuilder)
{
    protected override int QualifyDM => -1;
}

class Navy_Flight(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Navy_Flight(speciesCharacterBuilder)
{
    protected override int QualifyDM => -1;
}

class Navy_LineCrew(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Navy_LineCrew(speciesCharacterBuilder)
{
    protected override int QualifyDM => -1;
}

class Scout_Courier(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Scout_Courier(speciesCharacterBuilder)
{
    protected override int QualifyDM => 2;
}

class Scout_Explorer(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Scout_Explorer(speciesCharacterBuilder)
{
    protected override int QualifyDM => 2;
}

class Scout_Surveyor(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Scout_Surveyor(speciesCharacterBuilder)
{
    protected override int QualifyDM => 2;
}
