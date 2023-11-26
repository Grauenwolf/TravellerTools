using Grauenwolf.TravellerTools.Shared;
using System;
using System.Collections.Concurrent;

namespace Grauenwolf.TravellerTools.Maps;

/// <summary>
/// TravellerMapServiceLocator is used to get TravellerMapService objects. It handles the necessary caching.
/// </summary>
/// <remarks>Thread-safe. Only create one or the caching won't work.</remarks>
public class TravellerMapServiceLocator
{
    ConcurrentDictionary<string, TravellerMapService> s_Cache = new ConcurrentDictionary<string, TravellerMapService>();

    /// <summary>
    /// Initializes a new instance of the <see cref="TravellerMapServiceLocator"/> class.
    /// </summary>
    /// <param name="filterUnpopulatedSectors">if set to <c>true</c>, startup takes longer. Recommend using false for testing.</param>
    public TravellerMapServiceLocator(bool filterUnpopulatedSectors)
    {
        FilterUnpopulatedSectors = filterUnpopulatedSectors;
    }

    public bool FilterUnpopulatedSectors { get; }

    public TravellerMapService GetMapService(Milieu milieu)
    {
        if (milieu == null)
            throw new ArgumentNullException(nameof(milieu), $"{nameof(milieu)} is null.");

        return GetMapService(milieu.Code);
    }

    public TravellerMapService GetMapService(string milieuCode)
    {
        if (string.IsNullOrEmpty(milieuCode))
            throw new ArgumentException($"{nameof(milieuCode)} is null or empty.", nameof(milieuCode));

        return s_Cache.GetOrAdd(milieuCode, mc => new TravellerMapService(FilterUnpopulatedSectors, mc));
    }
}