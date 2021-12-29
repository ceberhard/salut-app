namespace SalutAPI.Domain.Model;

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
