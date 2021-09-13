using System;

namespace SalutAPI.Domain;

public class GameSystemEntity {
    public async Task<GameInstance> BuildGameInstance(long gameSystemId) {
        GameSystemRepo gsRepo = new();
        GameSystem gs = await gsRepo.FindByIdAsync(gameSystemId);

        return new() {
            GameSystemId = gs.Id,
            CreatedDTTM = DateTime.UtcNow,
            Components = new List<Component>()
        };
    }

    private async Task<List<Component>> RunPlayerConfigSteps(GameSystem gameSystem) {
        

    }
}
