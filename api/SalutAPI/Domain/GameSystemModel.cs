namespace SalutAPI.Domain;

public class GameSystem {
    public GameSystem(long id, string name) { this.Id = id; this.Name = name; }

    public long Id { get; init; }
    public string Name { get; set; }

    public GameSystemConfig SystemConfig { get; set; }
    public PlayerConfig PlayerConfig { get; set; }

    public IEnumerable<Component> Components { get; set; } = new List<Component>();
}

public class GameSystemOptions {
    public int AttributeApplyPercent { get; set; } = 100;
    public int? ComponentPointLimit { get; set; } = null;
    public int PlayerCount { get; set; } = 2;
    public int RetryTolerance { get; set; } = 5;
}

public class GameSystemConfig {
    public long Id { get; set; }
    public long GameSystemId { get; set; }
    public int PlayerMinCount { get; set; }
    public int PlayerMaxCount { get; set; }
}

public class GameSystemConfigStep {
    public long Id { get; set; }
    public long GameSystemConfigId { get; set; }

    public long ComponentTypeId { get; set; }

    public int StepOrder { get; set; }

    public int SelectionCount { get; set; }

    public ComponentType ComponentType { get; set; }
}

public class PlayerConfig {
    public long Id { get; set; }
    public long GameSystemId { get; set; }

    public int PointsAllowed { get; set; }

    public IEnumerable<PlayerSetupStep> Steps { get; set; } = new List<PlayerSetupStep>();
}

public class PlayerSetupStep {
    public long Id { get; set; }
    public long PlayerConfigId { get; set; }

    public long ComponentTypeId { get; set; }

    public int StepOrder { get; set; }

    public int SelectionCount { get; set; }

    public string Name { get; set; }

    public ComponentType ComponentType { get; set; }
}

public class ComponentType {
    public long Id { get; init; }
    public string Name { get; set; }
}

public class Component {

    public long Id { get; init; }
    public long GameSystemId { get; set; }
    public long ComponentTypeId { get; set; }
    public long? ParentComponentId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = null;
    public int? InstanceLimit { get; set; }

    public ComponentType ComponentType { get; set; }

    public IEnumerable<Component> Children { get; set; }

    public IEnumerable<ComponentAttribute> Attributes { get; set;}
}

public class ComponentLinkType {
    public long Id { get; set; }
    public string Name { get; set; }
}

public enum ComponentAttributeType {
    AppendComponentType = 1,
    AttributeRestriction = 2,
    ComponentRestriction = 3,
    Descriptive = 4,
    PointCost = 5
}

public class ComponentAttribute {
    public long Id { get; init; }
    public long Value { get; set; }
    public ComponentAttributeType Type { get; set; }
}

public enum DescriptiveAttribute {
    Aggressor = 1,
    AttackShuttle = 2,
    Awing = 3,
    BountyHunter = 4,
    Bwing = 5,
    Clone = 6,
    DarkSide = 7,
    Droid = 8,
    Firespray = 9,
    Freighter = 10,
    G1A = 11,
    HWK290 = 12,
    Jedi = 13,
    JumpMaster5000 = 14,
    Lancer = 15,
    LightSide = 16,
    Mandalorian = 17,
    Partisan = 18,
    Scurrg = 19,
    Sith = 20,
    Spectre = 21,
    StarViper = 22,
    StarWing = 23,
    T4aShuttle = 24,
    TIE = 25,
    TIE_D = 26,
    TIE_rb = 27,
    Uwing = 28,
    VCX100 = 29,
    VT49 = 30,
    Xwing = 31,
    YT1300 = 32,
    YT2400 = 33,
    YV666 = 34,
    Ywing = 35
}
