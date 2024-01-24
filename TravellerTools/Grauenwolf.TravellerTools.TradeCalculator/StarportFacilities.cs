namespace Grauenwolf.TravellerTools.TradeCalculator;

public class StarportFacilities
{
    public List<Accomodation>? Accomodations;

    public EHex StarportCode;
    public int CargoStorageCost { get; set; }
    public string CargoStorageSecurity => Tables.StarportCargoStorageSecurity(StarportCode);
    public StarportDetails? DownportDetails { get; set; }
    public StarportDetails? HighportDetails { get; set; }
    public EHex LawCode { get; set; }
    public string LawLevel => Tables.LawLevelDescription(LawCode);
    public string PortEnforcement => Tables.PortEnforcement(PortEnforcementCode);
    public int PortEnforcementCode { get; set; }
    public string PortEnforcementDetails => Tables.PortEnforcementDetails(PortEnforcementCode);
    public string? StarportDescription => Tables.StarportDescription(StarportCode);
}
