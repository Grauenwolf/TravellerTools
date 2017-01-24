/// <reference path="../typings/jquery/jquery.d.ts" />

interface ISubsector {
    Name: string;
    Index: string;
}

interface IWorldLocation {
    Name: string;
    Hex: string;
}

//Calling REST endpoints
//https://visualstudiomagazine.com/articles/2013/10/01/calling-web-services-with-typescript.aspx

function SectorChanged(sectorCoordinates: string, subsector: HTMLSelectElement): void {
    "use strict";

    $(subsector).empty();


    $.getJSON("/WorldApi/Subsectors?sectorCoordinates=" + sectorCoordinates,
        cs => {
            var myList = <ISubsector[]>cs;

            subsector.appendChild(new Option("", ""));

            for (var i = 0; i < myList.length; i++) {
                var opt = new Option(myList[i].Name, myList[i].Index);
                subsector.appendChild(opt);
            }
        });



}


function SubsectorChanged(sectorCoordinates: string, subsectorIndex: string, world: HTMLSelectElement): void {
    "use strict";

    $(world).empty();

    $.getJSON("/WorldApi/WorldsInSubsector?sectorCoordinates=" + sectorCoordinates + "&subsectorIndex=" + subsectorIndex,
        cs => {
            var myList = <IWorldLocation[]>cs;

            world.appendChild(new Option("", ""));

            for (var i = 0; i < myList.length; i++) {
                var opt = new Option(myList[i].Name, myList[i].Hex);
                world.appendChild(opt);
            }
        });
}

function WorldChanged(sectorCoordinates: string, worldCoordinates: string, button: HTMLInputElement, label: HTMLElement): void {
    "use strict";

    button.style.display = '';
    label.style.display = 'none';
}

function GenerateTradeInfo(sectorCoordinates: string, worldCoordinates: string, advancedMode: boolean, illegalGoods: boolean, jumpDistance: number, brokerScore: number, mongoose2: boolean, advancedCharacters: boolean): void {
    "use strict";
    var a = sectorCoordinates.split(",");
    var b = worldCoordinates.substring(0, 2);
    var c = worldCoordinates.substring(2, 4);
    var am = advancedMode ? "true" : "false";
    var ig = illegalGoods ? "true" : "false";
    var ac = advancedCharacters ? "true" : "false";
    var edition = mongoose2 ? 2016 : 2008;
    window.location.href = "/Home/TradeInfo?sectorX=" + a[0] + "&sectorY=" + a[1] + "&hexX=" + b + "&hexY=" + c + "&maxJumpDistance=" + jumpDistance + "&brokerScore=" + brokerScore + "&advancedMode=" + am + "&illegalGoods=" + ig + "&edition=" + edition + "&advancedCharacters=" + ac ;

}

function GenerateAnimals(terrain: string, animalType: string): void {
    "use strict";

    window.location.href = "/Home/Animals?terrainType=" + encodeURIComponent(terrain) + "&animalType=" + encodeURIComponent(animalType);
}


function GenerateAnimalEncounters(sectorCoordinates: string, worldCoordinates: string, terrain: string, animalClass: string): void {
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
