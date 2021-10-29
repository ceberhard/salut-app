using System;
using System.IO;
using System.Text;
using ClosedXML.Excel;

namespace LoadXWing {
    public class LoadPilots {
        private const int COL_PILOT_NAME = 1;
        private const int COL_PILOT_SUBTITLE = 2;
        private const int COL_PILOT_COST = 3;
        private const int COL_PILOT_INSTANCE_LIMIT = 4;
        private const int COL_PILOT_UPGRADE_ASTROMECH = 5;
        private const int COL_PILOT_UPGRADE_CANNON = 6;
        private const int COL_PILOT_UPGRADE_CARGO = 7;
        private const int COL_PILOT_UPGRADE_COMMAND = 8;
        private const int COL_PILOT_UPGRADE_CONFIGURATION = 9;
        private const int COL_PILOT_UPGRADE_CREW = 10;
        private const int COL_PILOT_UPGRADE_PAYLOAD = 11;
        private const int COL_PILOT_UPGRADE_FORCE = 12;
        private const int COL_PILOT_UPGRADE_GUNNER = 13;
        private const int COL_PILOT_UPGRADE_HARDPOINT = 14;
        private const int COL_PILOT_UPGRADE_ILLICIT = 15;
        private const int COL_PILOT_UPGRADE_MISSILE = 16;
        private const int COL_PILOT_UPGRADE_MODIFICATION = 17;
        private const int COL_PILOT_UPGRADE_SENSOR = 18;
        private const int COL_PILOT_UPGRADE_TACTICAL = 19;
        private const int COL_PILOT_UPGRADE_TALENT = 20;
        private const int COL_PILOT_UPGRADE_TEAM = 21;
        private const int COL_PILOT_UPGRADE_TECH = 22;
        private const int COL_PILOT_UPGRADE_TITLE = 23;
        private const int COL_PILOT_UPGRADE_TORPEDO = 24;
        private const int COL_PILOT_UPGRADE_TURRET = 25;
        private const int COL_PILOT_ATTRIBUTE1 = 27;
        private const int COL_PILOT_ATTRIBUTE2 = 28;
        private const int COL_PILOT_ATTRIBUTE3 = 29;
        private const int COL_PILOT_ATTRIBUTE4 = 30;
        private const int COL_PILOT_ATTRIBUTE5 = 31;

        public async static Task LoadAsync(string pilotFilePath, int parentComponentId, int componentSeqStart, int attributeSeqStart, string fileName) {
            using (XLWorkbook wb = new(pilotFilePath)) {
                var ws = wb.Worksheet(1);
                var row = ws.Row(2);

                StringBuilder components = new StringBuilder();
                int componentId = componentSeqStart;
                int attributeId = attributeSeqStart;

                var upgradeUsed = ws.Range(row.Cell(1).Address, ws.LastCellUsed().Address).RangeUsed();
                foreach(var r in upgradeUsed.Rows()) {
                    var name = r.Cell(COL_PILOT_NAME).GetString();
                    var description = r.Cell(COL_PILOT_SUBTITLE).GetString();
                    var cost = r.Cell(COL_PILOT_COST).GetString();
                    var instanceLimit = r.Cell(COL_PILOT_INSTANCE_LIMIT).GetString();
                    
                    components.Append($"new Component {{ Id = {componentId}, ");
                    components.Append($"ParentComponentId = {parentComponentId}, ");
                    components.Append($"Name = \"{name.Replace("\"", "\\\"")}\", ");
                    components.Append($"InstanceLimit = {instanceLimit}, ");
                    components.Append($"Description = \"{description.Replace("\"", "\\\"")}\", ");
                    components.Append($"ComponentTypeId = {Constants.COMPONENT_PILOT_SHIP_TYPE}, ");
                    components.Append($"ComponentType = new() {{ Id = {Constants.COMPONENT_PILOT_SHIP_TYPE}, Name = \"Pilot (Ship)\" }}, ");
                    components.Append($"Attributes = new List<ComponentAttribute> {{ new() {{ Id = {attributeId}, Value = {cost}, Type = ComponentAttributeType.PointCost }}");

                    components.Append(BuildUpgradeAttributes(r, COL_PILOT_UPGRADE_ASTROMECH, Constants.COMPONENT_UPGRADE_TYPE_ASTROMECH, ref attributeId));
                    components.Append(BuildUpgradeAttributes(r, COL_PILOT_UPGRADE_CANNON, Constants.COMPONENT_UPGRADE_TYPE_CANNON, ref attributeId));
                    components.Append(BuildUpgradeAttributes(r, COL_PILOT_UPGRADE_CARGO, Constants.COMPONENT_UPGRADE_TYPE_CARGO, ref attributeId));
                    components.Append(BuildUpgradeAttributes(r, COL_PILOT_UPGRADE_COMMAND, Constants.COMPONENT_UPGRADE_TYPE_COMMAND, ref attributeId));
                    components.Append(BuildUpgradeAttributes(r, COL_PILOT_UPGRADE_CONFIGURATION, Constants.COMPONENT_UPGRADE_TYPE_CONFIGURATION, ref attributeId));
                    components.Append(BuildUpgradeAttributes(r, COL_PILOT_UPGRADE_CREW, Constants.COMPONENT_UPGRADE_TYPE_CREW, ref attributeId));
                    components.Append(BuildUpgradeAttributes(r, COL_PILOT_UPGRADE_PAYLOAD, Constants.COMPONENT_UPGRADE_TYPE_PAYLOAD, ref attributeId));
                    components.Append(BuildUpgradeAttributes(r, COL_PILOT_UPGRADE_FORCE, Constants.COMPONENT_UPGRADE_TYPE_FORCE, ref attributeId));
                    components.Append(BuildUpgradeAttributes(r, COL_PILOT_UPGRADE_GUNNER, Constants.COMPONENT_UPGRADE_TYPE_GUNNER, ref attributeId));
                    components.Append(BuildUpgradeAttributes(r, COL_PILOT_UPGRADE_HARDPOINT, Constants.COMPONENT_UPGRADE_TYPE_HARDPOINT, ref attributeId));
                    components.Append(BuildUpgradeAttributes(r, COL_PILOT_UPGRADE_ILLICIT, Constants.COMPONENT_UPGRADE_TYPE_ILLICIT, ref attributeId));
                    components.Append(BuildUpgradeAttributes(r, COL_PILOT_UPGRADE_MISSILE, Constants.COMPONENT_UPGRADE_TYPE_MISSILE, ref attributeId));
                    components.Append(BuildUpgradeAttributes(r, COL_PILOT_UPGRADE_MODIFICATION, Constants.COMPONENT_UPGRADE_TYPE_MODIFICATION, ref attributeId));
                    components.Append(BuildUpgradeAttributes(r, COL_PILOT_UPGRADE_SENSOR, Constants.COMPONENT_UPGRADE_TYPE_SENSOR, ref attributeId));
                    components.Append(BuildUpgradeAttributes(r, COL_PILOT_UPGRADE_TACTICAL, Constants.COMPONENT_UPGRADE_TYPE_TACTICAL_RELAY, ref attributeId));
                    components.Append(BuildUpgradeAttributes(r, COL_PILOT_UPGRADE_TALENT, Constants.COMPONENT_UPGRADE_TYPE_TALENT, ref attributeId));
                    components.Append(BuildUpgradeAttributes(r, COL_PILOT_UPGRADE_TEAM, Constants.COMPONENT_UPGRADE_TYPE_TEAM, ref attributeId));
                    components.Append(BuildUpgradeAttributes(r, COL_PILOT_UPGRADE_TECH, Constants.COMPONENT_UPGRADE_TYPE_TECH, ref attributeId));
                    components.Append(BuildUpgradeAttributes(r, COL_PILOT_UPGRADE_TITLE, Constants.COMPONENT_UPGRADE_TYPE_TITLE, ref attributeId));
                    components.Append(BuildUpgradeAttributes(r, COL_PILOT_UPGRADE_TORPEDO, Constants.COMPONENT_UPGRADE_TYPE_TORPEDO, ref attributeId));
                    components.Append(BuildUpgradeAttributes(r, COL_PILOT_UPGRADE_TURRET, Constants.COMPONENT_UPGRADE_TYPE_TURRET, ref attributeId));

                    components.Append(BuildAttributeFeatures(r, COL_PILOT_ATTRIBUTE1, ref attributeId));
                    components.Append(BuildAttributeFeatures(r, COL_PILOT_ATTRIBUTE2, ref attributeId));
                    components.Append(BuildAttributeFeatures(r, COL_PILOT_ATTRIBUTE3, ref attributeId));
                    components.Append(BuildAttributeFeatures(r, COL_PILOT_ATTRIBUTE4, ref attributeId));
                    components.Append(BuildAttributeFeatures(r, COL_PILOT_ATTRIBUTE5, ref attributeId));

                    components.Append(" } },");

                    components.AppendLine();

                    componentId++;
                    attributeId++;
                }

                // Console.WriteLine(components.ToString());
                await File.WriteAllTextAsync(fileName, components.ToString());
                Console.WriteLine($"Created {fileName}...");
            }
        }

        private static string BuildUpgradeAttributes(IXLRangeRow row, int colIndex, int upgradeTypeId, ref int attributeId) {
            StringBuilder atts = new StringBuilder();
            double upgradeCount = (double)row.Cell(colIndex).Value;
            for (var i = 0; i < upgradeCount; i++) {
                attributeId++;
                atts.Append($", new () {{ Id = {attributeId}, Value = {upgradeTypeId}, Type = ComponentAttributeType.AppendComponentType }}");
            }
            return atts.ToString();
        }

        private static string BuildAttributeFeatures(IXLRangeRow row, int colIndex, ref int attributeId) {
            StringBuilder atts = new StringBuilder();
            string feature = row.Cell(colIndex).Value?.ToString();
            if (!string.IsNullOrEmpty(feature)) {
                attributeId++;
                atts.Append($", new () {{ Id = {attributeId}, Value = {Conversions.GetAttributeFeatureId(feature.Trim())}, Type = ComponentAttributeType.Descriptive }}");
            }
            return atts.ToString();
        }
    }
}