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

    public ComponentType ComponentType { get; set; }

    public IEnumerable<Component> Children { get; set; }

    public IEnumerable<ComponentAttribute> Attributes { get; set;}
}

public class ComponentLinkType {
    public long Id { get; set; }
    public string Name { get; set; }
}

public enum ComponentAttributeType {
    ComponentType = 1,
    PointCost = 2
}

public class ComponentAttribute {
    public long Id { get; init; }
    public object Value { get; set; }
    public ComponentAttributeType Type { get; set; }
}
