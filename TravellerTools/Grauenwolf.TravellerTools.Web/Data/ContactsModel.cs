﻿using Grauenwolf.TravellerTools.Characters;

namespace Grauenwolf.TravellerTools.Web.Data;

public class ContactsModel
{
    public List<Contact> Contacts { get; set; } = new();
    public int Seed { get; set; }
}
