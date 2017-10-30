/// <reference path="../typings/jquery/jquery.d.ts" />
//Calling REST endpoints
//https://visualstudiomagazine.com/articles/2013/10/01/calling-web-services-with-typescript.aspx
function readValue(controlName) {
    var control = $('#' + controlName)[0];
    return control.value;
}
function readChecked(controlName) {
    var control = $('#' + controlName)[0];
    return control.checked;
}
function SectorChanged(sectorCoordinates, subsector) {
    $(subsector).empty();
    $.getJSON("/WorldApi/Subsectors?sectorCoordinates=" + sectorCoordinates, function (cs) {
        var myList = cs;
        subsector.appendChild(new Option("", ""));
        for (var i = 0; i < myList.length; i++) {
            var opt = new Option(myList[i].Name, myList[i].Index);
            subsector.appendChild(opt);
        }
    });
}
function SubsectorChanged(sectorCoordinates, subsectorIndex, world) {
    $(world).empty();
    $.getJSON("/WorldApi/WorldsInSubsector?sectorCoordinates=" + sectorCoordinates + "&subsectorIndex=" + subsectorIndex, function (cs) {
        var myList = cs;
        world.appendChild(new Option("", ""));
        for (var i = 0; i < myList.length; i++) {
            var opt = new Option(myList[i].Name, myList[i].Hex);
            world.appendChild(opt);
        }
    });
}
function WorldChanged(originUwp, distinationUwp, button, label) {
    originUwp.value = '';
    distinationUwp.value = '';
    button.style.display = '';
    label.style.display = 'none';
}
function UwpChanged(originUwp, distinationUwp, button, label) {
    if (originUwp != null && originUwp.length > 0 && distinationUwp != null && distinationUwp.length > 0) {
        button.style.display = '';
        label.style.display = 'none';
    }
}
function GenerateTradeInfo(sectorCoordinates, worldCoordinates, advancedMode, illegalGoods, maxJumpDistance, brokerScore, mongoose2, advancedCharacters, streetwiseScore, raffle, originUwp, distinationUwp, jumpDistance) {
    var a = sectorCoordinates.split(",");
    var b = worldCoordinates.substring(0, 2);
    var c = worldCoordinates.substring(2, 4);
    var am = advancedMode ? "true" : "false";
    var ig = illegalGoods ? "true" : "false";
    var ac = advancedCharacters ? "true" : "false";
    var r = raffle ? "true" : "false";
    var edition = mongoose2 ? 2016 : 2008;
    if (originUwp != null && originUwp.length > 0 && distinationUwp != null && distinationUwp.length > 0) {
        window.location.href = "/Home/QuickTradeInfo?originUwp=" + originUwp + "&destinationUwp=" + distinationUwp + "&jumpDistance=" + jumpDistance + "&brokerScore=" + brokerScore + "&advancedMode=" + am + "&illegalGoods=" + ig + "&edition=" + edition + "&advancedCharacters=" + ac + "&streetwiseScore=" + streetwiseScore + "&raffle=" + raffle;
    }
    else {
        window.location.href = "/Home/TradeInfo?sectorX=" + a[0] + "&sectorY=" + a[1] + "&hexX=" + b + "&hexY=" + c + "&maxJumpDistance=" + maxJumpDistance + "&brokerScore=" + brokerScore + "&advancedMode=" + am + "&illegalGoods=" + ig + "&edition=" + edition + "&advancedCharacters=" + ac + "&streetwiseScore=" + streetwiseScore + "&raffle=" + raffle;
    }
}
function GenerateAnimals(terrain, animalType) {
    window.location.href = "/Home/Animals?terrainType=" + encodeURIComponent(terrain) + "&animalType=" + encodeURIComponent(animalType);
}
function GenerateAnimalEncounters(sectorCoordinates, worldCoordinates, terrain, animalClass) {
    "use strict";
    if (worldCoordinates != null && worldCoordinates != "") {
        var a = sectorCoordinates.split(",");
        var b = worldCoordinates.substring(0, 2);
        var c = worldCoordinates.substring(2, 4);
        window.location.href = "/Home/AnimalEncounters?sectorX=" + a[0] + "&sectorY=" + a[1] + "&hexX=" + b + "&hexY=" + c + "&terrainType=" + encodeURIComponent(terrain) + "&animalClass=" + encodeURIComponent(animalClass);
    }
    else {
        window.location.href = "/Home/AnimalEncounters?terrainType=" + encodeURIComponent(terrain) + "&animalClass=" + encodeURIComponent(animalClass);
    }
}
function GenerateStoreInfo(lawLevel, population, roll, starport, techLevel, tradeCodes, name, brokerScore, streetwiseScore) {
    var r = roll ? "true" : "false";
    window.location.href = "/Home/Store?lawLevel=" + lawLevel + "&population=" + population + "&roll=" + r + "&starport=" + starport + "&techLevel=" + techLevel + "&tradeCodes=" + encodeURIComponent(tradeCodes) + "&name=" + encodeURIComponent(name) + "&brokerScore=" + brokerScore + "&streetwiseScore=" + streetwiseScore;
}
//# sourceMappingURL=Index.js.map