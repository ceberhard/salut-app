using System;

namespace SalutAPI.Domain;

public class GameInstance
{
    public long GameSystemId { get; set; }

    public DateTime CreatedDTTM { get; set; }

    public IEnumerable<Component> Components { get;set; }
}

public class GameComponent
{
    public long GameComponentId { get; set; }

    public IEnumerable<Component> Components { get;set; }
}