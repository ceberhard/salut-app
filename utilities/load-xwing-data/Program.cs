using System.Text;
using LoadXWing;

string excelDoc = @"C:\source\resources\salut-api\Upgrades_September2021.xlsx";

await LoadUpgrades.LoadAsync(excelDoc);


Console.WriteLine("Testing 123");

