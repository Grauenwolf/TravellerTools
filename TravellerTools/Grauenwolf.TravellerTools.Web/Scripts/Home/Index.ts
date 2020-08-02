/// <reference path="../typings/jquery/jquery.d.ts" />

interface ISector {
    Name: string;
    X: number;
    Y: number;
}

interface ISubsector {
    Name: string;
    Index: string;
}

interface ICareer {
    Career: string;
    Assignment: string;
}

interface IWorldLocation {
    Name: string;
    Hex: string;
}

//Calling REST endpoints
//https://visualstudiomagazine.com/articles/2013/10/01/calling-web-services-with-typescript.aspx

function readValue(controlName: string): string {
    var control = <HTMLSelectElement>$('#' + controlName)[0];
    return control.value;
}

function readChecked(controlName: string): boolean {
    var control = <HTMLInputElement>$('#' + controlName)[0];
    return control.checked;
}

function MilieuChanged(milieu: string, sector: HTMLSelectElement): void {
    $(sector).empty();

    $.getJSON("/WorldApi/Sectors?milieu=" + milieu,
        cs => {
            var myList = <ISector[]>cs;

            sector.appendChild(new Option("", ""));

            for (var i = 0; i < myList.length; i++) {
                var opt = new Option(myList[i].Name, myList[i].X + ',' + myList[i].Y);
                sector.appendChild(opt);
            }
        });
}

function SectorChanged(sectorCoordinates: string, subsector: HTMLSelectElement, milieu: string): void {
    $(subsector).empty();

    $.getJSON("/WorldApi/Subsectors?sectorCoordinates=" + sectorCoordinates + "&milieu=" + milieu,
        cs => {
            var myList = <ISubsector[]>cs;

            subsector.appendChild(new Option("", ""));

            for (var i = 0; i < myList.length; i++) {
                var opt = new Option(myList[i].Name, myList[i].Index);
                subsector.appendChild(opt);
            }
        });
}

function CareerChanged(career: string, assignment: HTMLSelectElement, allowRandom: boolean): void {
    $(assignment).empty();

    $.getJSON("/WorldApi/Assignments?career=" + career,
        cs => {
            var myList = <ICareer[]>cs;

            if (allowRandom)
                assignment.appendChild(new Option("(Random)", "", true));

            for (var i = 0; i < myList.length; i++) {
                var opt = new Option(myList[i].Assignment, myList[i].Assignment, (i == 0 && !allowRandom));
                assignment.appendChild(opt);
            }
        });
}

function SubsectorChanged(sectorCoordinates: string, subsectorIndex: string, world: HTMLSelectElement, milieu: string): void {
    $(world).empty();

    $.getJSON("/WorldApi/WorldsInSubsector?sectorCoordinates=" + sectorCoordinates + "&subsectorIndex=" + subsectorIndex + "&milieu=" + milieu,
        cs => {
            var myList = <IWorldLocation[]>cs;

            world.appendChild(new Option("", ""));

            for (var i = 0; i < myList.length; i++) {
                var opt = new Option(myList[i].Name, myList[i].Hex);
                world.appendChild(opt);
            }
        });
}

function WorldChanged(originUwp: HTMLInputElement, distinationUwp: HTMLInputElement, button: HTMLInputElement, label: HTMLElement): void {
    originUwp.value = '';
    distinationUwp.value = '';
    //button.style.display = '';
    //label.style.display = 'none';
}

function UwpChanged(originUwp: string, distinationUwp: string, button: HTMLInputElement, label: HTMLElement): void {
    //if (originUwp != null && originUwp.length > 0 && distinationUwp != null && distinationUwp.length > 0) {
    //    button.style.display = '';
    //    label.style.display = 'none';
    //}
}

function GenerateCharacter(firstAssignment: string, finalCareer: string, finalAssignment: string, terms: number, skills: string[]) {
    var minAge = (terms > 0) ? 18 + (terms * 4) : "";
    var maxAge = (terms > 0) ? 18 + (terms * 4) + 3 : "";

    var fa = (firstAssignment == undefined) ? "" : firstAssignment;
    var c = (finalCareer == undefined) ? "" : finalCareer;
    var a = (finalAssignment == undefined) ? "" : finalAssignment;
    var s = "";

    for (var i = 0; i < skills.length; i++) {
        if (skills[i] != "")
            s += "&skills=" + skills[i];
    }

    window.location.href = "/Home/Character?minAge=" + minAge + "&maxAge=" + maxAge + "&firstAssignment=" + fa + "&finalCareer=" + c + "&finalAssignment=" + a + s;
}

function GenerateTradeInfo(sectorCoordinates: string, worldCoordinates: string, advancedMode: boolean, illegalGoods: boolean, maxJumpDistance: number, brokerScore: number, mongoose2: boolean, advancedCharacters: boolean, streetwiseScore: number, raffle: boolean, originUwp: string, destinationUwp: string, jumpDistance: number, milieu: string, originTasZone: string, destinationTasZone: string): void {
    var a = sectorCoordinates.split(",");
    var b = worldCoordinates.substring(0, 2);
    var c = worldCoordinates.substring(2, 4);
    var am = advancedMode ? "true" : "false";
    var ig = illegalGoods ? "true" : "false";
    var ac = advancedCharacters ? "true" : "false";
    var r = raffle ? "true" : "false";
    var edition = mongoose2 ? 2016 : 2008;

    if (originUwp != null && originUwp.length > 0) {
        window.location.href = "/Home/QuickTradeInfo?originUwp=" + originUwp + "&destinationUwp=" + destinationUwp + "&maxJumpDistance=" + jumpDistance + "&brokerScore=" + brokerScore + "&advancedMode=" + am + "&illegalGoods=" + ig + "&edition=" + edition + "&advancedCharacters=" + ac + "&streetwiseScore=" + streetwiseScore + "&raffle=" + raffle + "&milieu=" + milieu + "&originTasZone=" + originTasZone + "&destinationTasZone=" + destinationTasZone;
    }
    else if (worldCoordinates.length > 0) {
        window.location.href = "/Home/TradeInfo?sectorX=" + a[0] + "&sectorY=" + a[1] + "&hexX=" + b + "&hexY=" + c + "&maxJumpDistance=" + maxJumpDistance + "&brokerScore=" + brokerScore + "&advancedMode=" + am + "&illegalGoods=" + ig + "&edition=" + edition + "&advancedCharacters=" + ac + "&streetwiseScore=" + streetwiseScore + "&raffle=" + raffle + "&milieu=" + milieu;
    } else {
        alert("You must select a world or enter an origin URL to be able to generate trade info.")
    }
}

function GenerateRandomWorld(advancedMode: boolean, illegalGoods: boolean, maxJumpDistance: number, brokerScore: number, mongoose2: boolean, advancedCharacters: boolean, streetwiseScore: number, raffle: boolean, milieu: string): void {
    var am = advancedMode ? "true" : "false";
    var ig = illegalGoods ? "true" : "false";
    var ac = advancedCharacters ? "true" : "false";
    var r = raffle ? "true" : "false";
    var edition = mongoose2 ? 2016 : 2008;

    window.location.href = "/Home/RandomWorld?brokerScore=" + brokerScore + "&advancedMode=" + am + "&illegalGoods=" + ig + "&edition=" + edition + "&advancedCharacters=" + ac + "&streetwiseScore=" + streetwiseScore + "&raffle=" + raffle + "&milieu=" + milieu;
}

function GenerateAnimals(terrain: string, animalType: string): void {
    window.location.href = "/Home/Animals?terrainType=" + encodeURIComponent(terrain) + "&animalType=" + encodeURIComponent(animalType);
}

function GenerateAnimalEncounters(sectorCoordinates: string, worldCoordinates: string, terrain: string, animalClass: string, milieu: string): void {
    "use strict";

    if (worldCoordinates != null && worldCoordinates != "") {
        var a = sectorCoordinates.split(",");
        var b = worldCoordinates.substring(0, 2);
        var c = worldCoordinates.substring(2, 4);

        window.location.href = "/Home/AnimalEncounters?sectorX=" + a[0] + "&sectorY=" + a[1] + "&hexX=" + b + "&hexY=" + c + "&terrainType=" + encodeURIComponent(terrain) + "&animalClass=" + encodeURIComponent(animalClass) + "&milieu=" + milieu;
    }
    else {
        window.location.href = "/Home/AnimalEncounters?terrainType=" + encodeURIComponent(terrain) + "&animalClass=" + encodeURIComponent(animalClass);
    }
}

function GenerateStoreInfo(lawLevel: string, population: string, roll: boolean, starport: string, techLevel: string, tradeCodes: string, name: string, brokerScore: number, streetwiseScore: number, milieu: string): void {
    var r = roll ? "true" : "false";

    window.location.href = "/Home/Store?lawLevel=" + lawLevel + "&population=" + population + "&roll=" + r + "&starport=" + starport + "&techLevel=" + techLevel + "&tradeCodes=" + encodeURIComponent(tradeCodes) + "&name=" + encodeURIComponent(name) + "&brokerScore=" + brokerScore + "&streetwiseScore=" + streetwiseScore + "&milieu=" + milieu;
}
