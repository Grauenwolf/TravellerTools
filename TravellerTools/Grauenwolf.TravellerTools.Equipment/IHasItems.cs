﻿using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Equipment;

public interface IHasItems
{
    List<Item> Items { get; }
}