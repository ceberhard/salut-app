using System;
using System.IO;
using System.Text;
using ClosedXML.Excel;

namespace LoadXWing {
    public class LoadFactions {
        public async static Task LoadAsync() {
            StringBuilder components = new StringBuilder();

            // Create Rebel Faction
            components.AppendLine(BuildComponent(Constants.COMPONENT_FACTION_REBELS, "Rebels"));
            // Create Imperial Faction
            components.AppendLine(BuildComponent(Constants.COMPONENT_FACTION_IMPERIALS, "Imperials"));
            // Create Scum Faction
            components.AppendLine(BuildComponent(Constants.COMPONENT_FACTION_SCUM, "Scum & Villainy"));

            // Console.WriteLine(components.ToString());
            await File.WriteAllTextAsync("factions.txt", components.ToString());
            Console.WriteLine("Created factions.txt...");
        }

        private static string BuildComponent(int id, string name) {
            StringBuilder c = new StringBuilder($"new Component {{ Id = {id}, Name = \"{name}\", ");
            c.Append($"ComponentTypeId = {Constants.COMPONENT_FACTION_TYPE}, ");
            c.Append($"ComponentType = new() {{ Id = {Constants.COMPONENT_FACTION_TYPE}, Name = \"Faction\" }} }},");
            return c.ToString();
        }

    }
}