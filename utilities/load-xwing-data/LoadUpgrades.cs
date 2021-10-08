using System;
using System.IO;
using System.Text;
using ClosedXML.Excel;

namespace LoadXWing {
    public class LoadUpgrades {
        private const int COL_UPGRADE_NAME = 1;
        private const int COL_UPGRADE_TYPE = 2;
        private const int COL_UPGRADE_COST = 3;

        public async static Task LoadAsync(string upgradeFilePath) {
            using (XLWorkbook wb = new(upgradeFilePath)) {
                var ws = wb.Worksheet(1);
                var row = ws.Row(2);

                StringBuilder components = new StringBuilder();
                int componentId = Constants.UPGRADE_SEQ_START;
                int attCostId = Constants.UPGRADE_ATTRIBUTE_COST_SEQ_START;

                var upgradeUsed = ws.Range(row.Cell(1).Address, ws.LastCellUsed().Address).RangeUsed();
                foreach(var r in upgradeUsed.Rows()) {
                    var name = r.Cell(COL_UPGRADE_NAME).GetString();
                    var type = r.Cell(COL_UPGRADE_TYPE).GetString();
                    var cost = r.Cell(COL_UPGRADE_COST).GetString();
                    var typeId = GetComponentTypeId(type);

                    components.Append($"new Component {{ Id = {componentId}, ");
                    components.Append($"Name = \"{name.Replace("\"", "\\\"")}\", ");
                    components.Append($"ComponentTypeId = {typeId}, ");
                    components.Append($"ComponentType = new() {{ Id = {typeId}, Name = \"{type} Upgrade\" }}, ");
                    components.Append($"Attributes = new List<ComponentAttribute> {{ new() {{ Id = {attCostId}, Value = {cost}, Type = ComponentAttributeType.PointCost }} }} }},");
                    components.AppendLine();

                    componentId++;
                    attCostId++;
                }

                // Console.WriteLine(components.ToString());
                await File.WriteAllTextAsync("upgrades.txt", components.ToString());
                Console.WriteLine("Created upgrades.txt...");
            }
        }

        private static int GetComponentTypeId(string typeText) => typeText switch {
            "Astromech" => Constants.COMPONENT_UPGRADE_TYPE_ASTROMECH,
            "Cannon" => Constants.COMPONENT_UPGRADE_TYPE_CANNON,
            "Configuration" => Constants.COMPONENT_UPGRADE_TYPE_CONFIGURATION,
            "Crew" => Constants.COMPONENT_UPGRADE_TYPE_CREW,
            "Force Power" => Constants.COMPONENT_UPGRADE_TYPE_FORCE,
            "Gunner" => Constants.COMPONENT_UPGRADE_TYPE_GUNNER,
            "Illicit" => Constants.COMPONENT_UPGRADE_TYPE_ILLICIT,
            "Missile" => Constants.COMPONENT_UPGRADE_TYPE_MISSILE,
            "Modification" => Constants.COMPONENT_UPGRADE_TYPE_MODIFICATION,
            "Payload" => Constants.COMPONENT_UPGRADE_TYPE_PAYLOAD,
            "Sensor" => Constants.COMPONENT_UPGRADE_TYPE_SENSOR,
            "Talent" => Constants.COMPONENT_UPGRADE_TYPE_TALENT,
            "Tech" => Constants.COMPONENT_UPGRADE_TYPE_TECH,
            "Torpedo" => Constants.COMPONENT_UPGRADE_TYPE_TORPEDO,
            "Turret" => Constants.COMPONENT_UPGRADE_TYPE_TURRET,
            "Title" => Constants.COMPONENT_UPGRADE_TYPE_TITLE,
            _ => throw new ArgumentException($"Not supported Upgrade Type: {typeText}"),
        };
    }
}