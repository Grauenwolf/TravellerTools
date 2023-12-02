using Grauenwolf.TravellerTools.Characters;

namespace Grauenwolf.TravellerTools.Web.Data;

public class ContactsModel
{
    public List<Contact> Contacts { get; set; } = new List<Contact>();
    public int Seed { get; set; }
}
