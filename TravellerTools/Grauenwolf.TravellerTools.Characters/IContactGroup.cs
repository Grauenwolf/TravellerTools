namespace Grauenwolf.TravellerTools.Characters;

public interface IContactGroup
{
    List<Contact> Contacts { get; }

    Queue<ContactType> UnusedContacts { get; }

    void AddAlly(int count = 1);

    void AddContact(int count = 1);

    void AddEnemy(int count = 1);

    void AddRival(int count = 1);
}
