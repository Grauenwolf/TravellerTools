using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Names;
using Grauenwolf.TravellerTools.TradeCalculator;
using System.Collections.Concurrent;

namespace Grauenwolf.TravellerTools.Web;

public class TradeEngineLocator(TravellerMapServiceLocator mapService, string dataPath, NameGenerator nameGenerator, CharacterBuilderLocator characterBuilderLocator)
{
    readonly ConcurrentDictionary<(string, int), TradeEngine> TradeEngines = new ConcurrentDictionary<(string, int), TradeEngine>();

    public CharacterBuilderLocator CharacterBuilderLocator { get; } = characterBuilderLocator;
    public string DataPath { get; } = dataPath;

    public TravellerMapServiceLocator MapService { get; } = mapService;

    public NameGenerator NameGenerator { get; } = nameGenerator;

    public TradeEngine GetTradeEngine(string milieu, Edition edition)
    {
        if (TradeEngines.TryGetValue((milieu, (int)edition), out var engine))
            return engine;

        switch (edition)
        {
            case Edition.Mongoose:
                engine = new TradeEngineMgt(MapService.GetMapService(milieu), DataPath, NameGenerator, CharacterBuilderLocator);
                break;

            case Edition.Mongoose2:
                engine = new TradeEngineMgt2(MapService.GetMapService(milieu), DataPath, NameGenerator, CharacterBuilderLocator);
                break;

            case Edition.Mongoose2022:
                engine = new TradeEngineMgt2022(MapService.GetMapService(milieu), DataPath, NameGenerator, CharacterBuilderLocator);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(edition));
        }

        TradeEngines[(milieu, (int)edition)] = engine;
        return engine;
    }
}
