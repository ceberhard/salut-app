using System;
using System.Collections.Generic;

namespace SalutAPI.Domain;

public class GameInstance
{
    public long GameSystemId { get; set; }

    public int PlayerCount { get; set; }

    public DateTime CreatedDTTM { get; set; }

    public GameInstanceSummary Summary { get; set; } = new();

    public IEnumerable<GameInstanceComponent> Components { get;set; } = new List<GameInstanceComponent>();
}

public class GameInstanceComponent : Component
{
    public GameInstanceComponent() { }

    public GameInstanceComponent(Component component, long componentAttributeId, int pointValue, int rollupPointValue)
    {   
        SetComponentValues(component);
        this.Id = component.Id;
        this.ComponentAttributeId = componentAttributeId;
        this.PointValue = pointValue;
        this.RollupPointValue = rollupPointValue;
    }

    public GameInstanceComponent(Component component, int pointValue, int rollupPointValue)
    {
        SetComponentValues(component);
        this.Id = component.Id;
        this.PointValue = pointValue;
        this.RollupPointValue = rollupPointValue;
    }

    public long ComponentId;

    public int? InstanceLimit { get; set; }

    public long ComponentAttributeId { get; set; }

    public int PointValue { get; set; }

    public int RollupPointValue { get; set; }

    public new List<GameInstanceComponent> Children { get; set; } = new List<GameInstanceComponent>();

    public void SetComponentValues(Component src) {
        this.GameSystemId = src.GameSystemId;
        this.ComponentId = src.Id;
        this.InstanceLimit = src.InstanceLimit;
        this.ComponentTypeId = src.ComponentTypeId;
        this.ParentComponentId = src.ParentComponentId;
        this.Name = src.Name;
        this.Description = src.Description;
        this.Attributes = src.Attributes;
    }
}

public class GameInstanceSummary {
    public int? PointTotal { get; set; }
}