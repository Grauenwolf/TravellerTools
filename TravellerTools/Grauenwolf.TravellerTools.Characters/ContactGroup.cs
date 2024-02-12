namespace Grauenwolf.TravellerTools.Characters;

public class ContactGroup : IContactGroup
{
    public List<Contact> Contacts { get; } = new();

    Queue<ContactType> IContactGroup.UnusedContacts => UnusedContacts;
    internal Queue<ContactType> UnusedContacts { get; } = new();

    public void AddAlly(int count = 1)
    {
        for (var i = 0; i < count; i++)
            UnusedContacts.Enqueue(ContactType.Ally);
    }

    public void AddContact(int count = 1)
    {
        for (var i = 0; i < count; i++)
            UnusedContacts.Enqueue(ContactType.Contact);
    }

    public void AddEnemy(int count = 1)
    {
        for (var i = 0; i < count; i++)
            UnusedContacts.Enqueue(ContactType.Enemy);
    }

    public void AddRival(int count = 1)
    {
        for (var i = 0; i < count; i++)
            UnusedContacts.Enqueue(ContactType.Rival);
    }
}
