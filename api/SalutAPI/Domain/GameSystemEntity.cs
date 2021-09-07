namespace SalutAPI.Domain;

public class GameSystem {
    public GameSystem(long id, string name) { this.Id = id; this.Name = name; }

    public long Id { get; init; }
    public string Name { get; set; }

    public GameSystemConfig SystemConfig { get; set; }

    public IEnumerable<Component> Components { get; set; } = new List<Component>();
}

public class GameSystemConfig {
    public long GameSystemId { get; set; }
    
    public int PlayerMinCount { get; set; }
    public int PlayerMaxCount { get; set; }
}

public class PlayerConfig {
    public long Id { get; set; }
    public long GameSystemId { get; set; }
    
}


public class ComponentType {
    public long Id { get; init; }
    public string Name { get; set; }
}

public class Component {

    public long Id { get; init; }
    public long GameSystemId { get; set; }
    public long ComponentTypeId { get; set; }
    public string Name { get; set; }

    public ComponentType ComponentType { get; set; }

    public IEnumerable<ComponentAttribute> Attributes { get; set;}
}

public class ComponentAttributeType {
    public long Id { get; init; }
    public string Name { get; set; }
}

public class ComponentAttribute {
    public long Id { get; init; }
    public long ComponentId { get; set; }
    public long ComponentAttrbuteTypeId { get; set; }
    public string Name { get; set; }

    public ComponentAttributeType AttributeType { get; set; }
}
