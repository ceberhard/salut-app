using System;
using System.IO;
using System.Text;
using ClosedXML.Excel;

namespace LoadXWing {
    public class LoadUpgrades {
        private const int COL_UPGRADE_NAME = 1;
        private const int COL_UPGRADE_TYPE = 2;
        private const int COL_UPGRADE_COST = 3;
        private const int COL_UPGRADE_INSTANCE_LIMIT = 4;
        private const int COL_UPGRADE_FACTION_RESTRICTION = 5;
        private const int COL_ATTRIBUTE_RESTRICTION1 = 6;

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
                    var instanceLimit = r.Cell(COL_UPGRADE_INSTANCE_LIMIT).GetString();
                    var factionRestriction = r.Cell(COL_UPGRADE_FACTION_RESTRICTION).GetString();
                    var attributeRestriction1 = r.Cell(COL_ATTRIBUTE_RESTRICTION1).GetString();
                    var typeId = Conversions.GetComponentTypeId(type);

                    components.Append($"new Component {{ Id = {componentId}, ");
                    components.Append($"Name = \"{name.Replace("\"", "\\\"")}\", ");
                    components.Append($"InstanceLimit = {instanceLimit}, ");
                    components.Append($"ComponentTypeId = {typeId}, ");
                    components.Append($"ComponentType = new() {{ Id = {typeId}, Name = \"{type} Upgrade\" }}, ");
                    components.Append($"Attributes = new List<ComponentAttribute> {{ new() {{ Id = {attCostId}, Value = {cost}, Type = ComponentAttributeType.PointCost }}");

                    if (!string.IsNullOrWhiteSpace(factionRestriction)) {
                        attCostId++;
                        components.Append($", new() {{ Id = {attCostId}, Value = {Conversions.GetFactionId(factionRestriction)}, Type = ComponentAttributeType.ComponentRestriction }}");
                    }

                    if (!string.IsNullOrWhiteSpace(attributeRestriction1)) {
                        attCostId++;
                        components.Append($", new() {{ Id = {attCostId}, Value = {Conversions.GetAttributeFeatureId(attributeRestriction1)}, Type = ComponentAttributeType.AttributeRestriction }}");
                    }

                    components.Append(" } },");
                    components.AppendLine();

                    componentId++;
                    attCostId++;
                }

                // Console.WriteLine(components.ToString());
                await File.WriteAllTextAsync("upgrades.txt", components.ToString());
                Console.WriteLine("Created upgrades.txt...");
            }
        }
    }
}