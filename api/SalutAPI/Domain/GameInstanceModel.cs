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
    public long ComponentAttributeId { get; set; }

    public int PointValue { get; set; }

    public int RollupPointValue { get; set; }

}

public class GameInstanceSummary {
    public int? PointTotal { get; set; }
}