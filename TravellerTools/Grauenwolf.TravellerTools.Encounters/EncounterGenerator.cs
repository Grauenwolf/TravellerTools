using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.TradeCalculator;

namespace Grauenwolf.TravellerTools.Encounters;

public partial class EncounterGenerator(CharacterBuilder characterBuilder, TradeEngine tradeEngine)
{
    public CharacterBuilder CharacterBuilder { get; } = characterBuilder;

    public TradeEngine TradeEngine { get; } = tradeEngine;

    TradeGoodDetail CommonTradeGood(Dice dice) => dice.Choose(TradeEngine.CommonTradeGoods).ChooseRandomDetail(dice);

    TradeGoodDetail IllegalTradeGood(Dice dice) => dice.Choose(TradeEngine.IllegalTradeGoods).ChooseRandomDetail(dice);

    TradeGoodDetail LegalTradeGood(Dice dice) => dice.Choose(TradeEngine.LegalTradeGoods).ChooseRandomDetail(dice);

    TradeGoodDetail TradeGood(Dice dice) => dice.Choose(TradeEngine.TradeGoods).ChooseRandomDetail(dice);

    TradeGoodDetail UncommonTradeGood(Dice dice) => dice.Choose(TradeEngine.UncommonTradeGoods).ChooseRandomDetail(dice);
}
