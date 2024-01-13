namespace Grauenwolf.TravellerTools.Characters.Careers.AelYael;

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

class Marine_GroundAssault(CharacterBuilder characterBuilder) : Humaniti.Marine_GroundAssault(characterBuilder)
{
    protected override int QualifyDM => -1;
}

class Marine_StarMarine(CharacterBuilder characterBuilder) : Humaniti.Marine_StarMarine(characterBuilder)
{
    protected override int QualifyDM => -1;
}

class Marine_Support(CharacterBuilder characterBuilder) : Humaniti.Marine_Support(characterBuilder)
{
    protected override int QualifyDM => -1;
}

class Navy_EngineerGunner(CharacterBuilder characterBuilder) : Humaniti.Navy_EngineerGunner(characterBuilder)
{
    protected override int QualifyDM => -1;
}

class Navy_Flight(CharacterBuilder characterBuilder) : Humaniti.Navy_Flight(characterBuilder)
{
    protected override int QualifyDM => -1;
}

class Navy_LineCrew(CharacterBuilder characterBuilder) : Humaniti.Navy_LineCrew(characterBuilder)
{
    protected override int QualifyDM => -1;
}

class Scout_Courier(CharacterBuilder characterBuilder) : Humaniti.Scout_Courier(characterBuilder)
{
    protected override int QualifyDM => 2;
}

class Scout_Explorer(CharacterBuilder characterBuilder) : Humaniti.Scout_Explorer(characterBuilder)
{
    protected override int QualifyDM => 2;
}

class Scout_Surveyor(CharacterBuilder characterBuilder) : Humaniti.Scout_Surveyor(characterBuilder)
{
    protected override int QualifyDM => 2;
}
