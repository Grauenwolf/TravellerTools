using Grauenwolf.TravellerTools.Characters;
using System.Collections.Generic;
using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Web.Data;

public class ContactOptions : ModelBase
{
    public int Allies { get => Get<int>(); set => Set(value); }
    public int Contacts { get => Get<int>(); set => Set(value); }
    public int Enemies { get => Get<int>(); set => Set(value); }
    public int Rivals { get => Get<int>(); set => Set(value); }
}

public class ContactsModel
{
    public List<Contact> Contacts { get; set; } = new List<Contact>();
    public int Seed { get; set; }
}
