namespace LoadXWing;

public static class Conversions {
    internal static int GetAttributeFeatureId(string feature) => feature switch {
        "Aggressor" => (int)Enums.DescriptiveAttribute.Aggressor,
        "Attack shuttle" => (int)Enums.DescriptiveAttribute.AttackShuttle,
        "A-wing" => (int)Enums.DescriptiveAttribute.Awing,
        "Bounty Hunter" => (int)Enums.DescriptiveAttribute.BountyHunter,
        "B-wing" => (int)Enums.DescriptiveAttribute.Bwing,
        "Clone" => (int)Enums.DescriptiveAttribute.Clone,
        "Dark Side" => (int)Enums.DescriptiveAttribute.DarkSide,
        "Droid" => (int)Enums.DescriptiveAttribute.Droid,
        "Firespray" => (int)Enums.DescriptiveAttribute.Firespray,
        "Freighter" => (int)Enums.DescriptiveAttribute.Freighter,
        "G-1A" => (int)Enums.DescriptiveAttribute.G1A,
        "HWK-290" => (int)Enums.DescriptiveAttribute.HWK290,
        "Jedi" => (int)Enums.DescriptiveAttribute.Jedi,
        "JumpMaster 5000" => (int)Enums.DescriptiveAttribute.JumpMaster5000,
        "Lancer" => (int)Enums.DescriptiveAttribute.Lancer,
        "Light Side" => (int)Enums.DescriptiveAttribute.LightSide,
        "Mandalorian" => (int)Enums.DescriptiveAttribute.Mandalorian,
        "Partisan" => (int)Enums.DescriptiveAttribute.Partisan,
        "Scurrg" => (int)Enums.DescriptiveAttribute.Scurrg,
        "Sith" => (int)Enums.DescriptiveAttribute.Sith,
        "Spectre" => (int)Enums.DescriptiveAttribute.Spectre,
        "StarViper" => (int)Enums.DescriptiveAttribute.StarViper,
        "Star Wing" => (int)Enums.DescriptiveAttribute.StarWing,
        "T-4a Shuttle" => (int)Enums.DescriptiveAttribute.T4aShuttle,
        "TIE" => (int)Enums.DescriptiveAttribute.TIE,
        "TIE/D" => (int)Enums.DescriptiveAttribute.TIE_D,
        "TIE/rb" => (int)Enums.DescriptiveAttribute.TIE_rb,
        "U-wing" => (int)Enums.DescriptiveAttribute.Uwing,
        "VCX-100" => (int)Enums.DescriptiveAttribute.VCX100,
        "VT-49" => (int)Enums.DescriptiveAttribute.VT49,
        "X-wing" => (int)Enums.DescriptiveAttribute.Xwing,
        "YT-1300" => (int)Enums.DescriptiveAttribute.YT1300,
        "YT-2400" => (int)Enums.DescriptiveAttribute.YT2400,
        "YV-666" => (int)Enums.DescriptiveAttribute.YV666,
        "Y-wing" => (int)Enums.DescriptiveAttribute.Ywing,
        _ => throw new ArgumentException($"Not supported Feature: {feature}")
    };

    internal static int GetComponentTypeId(string typeText) => typeText switch {
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

    internal static int GetFactionId(string faction) => faction switch {
        "Rebels" => Constants.COMPONENT_FACTION_REBELS,
        "Imperials" => Constants.COMPONENT_FACTION_IMPERIALS,
        "Scum & Villainy" => Constants.COMPONENT_FACTION_SCUM,
        _ => throw new ArgumentException($"Not supported Faction: {faction}"),
    };
}
