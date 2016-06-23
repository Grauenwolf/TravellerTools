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


	$.getJSON("WorldApi/Subsectors?sectorCoordinates=" + sectorCoordinates,
		cs => {
			var myList = <ISubsector[]>cs;

			subsector.appendChild(new Option("",""));

			for (var i = 0; i < myList.length; i++) {
				var opt = new Option(myList[i].Name, myList[i].Index);
				subsector.appendChild(opt);
			}
		});



}


function SubsectorChanged(sectorCoordinates: string, subsectorIndex: string, world: HTMLSelectElement): void {
	"use strict";

	$(world).empty();

	$.getJSON("WorldApi/WorldsInSubsector?sectorCoordinates=" + sectorCoordinates + "&subsectorIndex=" + subsectorIndex,
		cs => {
			var myList = <IWorldLocation[]>cs;

			world.appendChild(new Option("", ""));

			for (var i = 0; i < myList.length; i++) {
				var opt = new Option(myList[i].Name, myList[i].Hex);
				world.appendChild(opt);
			}
		});
}

function WorldChanged(sectorCoordinates: string, worldCoordinates: string, button: HTMLInputElement): void {
	"use strict";

	button.style.visibility = 'visible';
}

function GenerateTradeInfo(sectorCoordinates: string, worldCoordinates: string, advancedMode:boolean, illegalGoods: boolean, jumpDistance: number, brokerScore:number): void {
	"use strict";
	var a = sectorCoordinates.split(",");
	var b = worldCoordinates.substring(0, 2);
	var c = worldCoordinates.substring(2, 4);
	var am = advancedMode ? "true" : "false";
	var ig = illegalGoods ? "true" : "false";
	window.location.href = "Home/TradeInfo?sectorX=" + a[0] + "&sectorY=" + a[1] + "&hexX=" + b + "&hexY=" + c + "&maxJumpDistance=" + jumpDistance + "&brokerScore=" + brokerScore + "&advancedMode=" + am + "&illegalGoods=" + ig;

	//window.open("Home/TradeInfo?sectorX=" + a[0] + "&sectorY=" + a[1] + "&hexX=" + b + "&hexY=" + b + "&maxJumpDistance=3");
}