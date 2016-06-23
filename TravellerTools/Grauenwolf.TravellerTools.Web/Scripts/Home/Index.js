/// <reference path="../typings/jquery/jquery.d.ts" />
//Calling REST endpoints
//https://visualstudiomagazine.com/articles/2013/10/01/calling-web-services-with-typescript.aspx
function SectorChanged(sectorCoordinates, subsector) {
    "use strict";
    $(subsector).empty();
    $.getJSON("WorldApi/Subsectors?sectorCoordinates=" + sectorCoordinates, function (cs) {
        var myList = cs;
        subsector.appendChild(new Option("", ""));
        for (var i = 0; i < myList.length; i++) {
            var opt = new Option(myList[i].Name, myList[i].Index);
            subsector.appendChild(opt);
        }
    });
}
function SubsectorChanged(sectorCoordinates, subsectorIndex, world) {
    "use strict";
    $(world).empty();
    $.getJSON("WorldApi/WorldsInSubsector?sectorCoordinates=" + sectorCoordinates + "&subsectorIndex=" + subsectorIndex, function (cs) {
        var myList = cs;
        world.appendChild(new Option("", ""));
        for (var i = 0; i < myList.length; i++) {
            var opt = new Option(myList[i].Name, myList[i].Hex);
            world.appendChild(opt);
        }
    });
}
function WorldChanged(sectorCoordinates, worldCoordinates, button) {
    "use strict";
    button.style.visibility = 'visible';
}
function GenerateTradeInfo(sectorCoordinates, worldCoordinates, advancedMode, illegalGoods, jumpDistance, brokerScore) {
    "use strict";
    var a = sectorCoordinates.split(",");
    var b = worldCoordinates.substring(0, 2);
    var c = worldCoordinates.substring(2, 4);
    var am = advancedMode ? "true" : "false";
    var ig = illegalGoods ? "true" : "false";
    window.location.href = "Home/TradeInfo?sectorX=" + a[0] + "&sectorY=" + a[1] + "&hexX=" + b + "&hexY=" + c + "&maxJumpDistance=" + jumpDistance + "&brokerScore=" + brokerScore + "&advancedMode=" + am + "&illegalGoods=" + ig;
    //window.open("Home/TradeInfo?sectorX=" + a[0] + "&sectorY=" + a[1] + "&hexX=" + b + "&hexY=" + b + "&maxJumpDistance=3");
}
//# sourceMappingURL=Index.js.map