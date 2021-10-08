using System.Text;
using LoadXWing;

string excelDoc = @"C:\source\resources\salut-api\Upgrades_September2021.xlsx";
string rebelPilotsDoc = @"C:\source\resources\salut-api\Rebels_September2021.xlsx";
string imperialPilotsDoc = @"C:\source\resources\salut-api\Imperials_September2021.xlsx";
string scumPilotsDoc = @"C:\source\resources\salut-api\Scum_September2021.xlsx";

await LoadFactions.LoadAsync();
await LoadUpgrades.LoadAsync(excelDoc);
await LoadPilots.LoadAsync(rebelPilotsDoc, Constants.COMPONENT_FACTION_REBELS, Constants.PILOTS_REBEL_START, Constants.PILOTS_REBEL_ATRIBUTE_START, "rebels.txt");
await LoadPilots.LoadAsync(imperialPilotsDoc, Constants.COMPONENT_FACTION_IMPERIALS, Constants.PILOTS_IMPERIAL_START, Constants.PILOTS_IMPERIAL_ATRIBUTE_START, "imperials.txt");
await LoadPilots.LoadAsync(scumPilotsDoc, Constants.COMPONENT_FACTION_SCUM, Constants.PILOTS_SCUM_START, Constants.PILOTS_SCUM_ATRIBUTE_START, "scum.txt");

