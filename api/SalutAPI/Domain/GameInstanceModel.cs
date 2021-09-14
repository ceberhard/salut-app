using System;
using System.Collections.Generic;

namespace SalutAPI.Domain;

public class GameInstance
{
    public long GameSystemId { get; set; }

    public int PlayerCount { get; set; }

    public DateTime CreatedDTTM { get; set; }

    public IEnumerable<Component> Components { get;set; } = new List<Component>();
}

public class GameComponent
{
    public long GameComponentId { get; set; }

    public IEnumerable<Component> Components { get;set; } = new List<Component>();
}