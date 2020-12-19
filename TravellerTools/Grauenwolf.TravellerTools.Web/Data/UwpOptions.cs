using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Web.Data
{
    public class UwpOptions : ModelBase
    {
        public string? RawUwp { get => Get<string?>(); set => Set(value); }

        [CalculatedField(nameof(RawUwp))]
        public bool UwpNotSelected => false; //TODO: This is true if RawUwp cannot be parsed.
    }
}
