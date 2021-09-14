using System;
using SalutAPI.Util;

namespace SalutAPI.Domain;

public class GameSystemEntity {
    public async Task<GameInstance> BuildGameInstance(long gameSystemId) {
        GameSystemRepo gsRepo = new();
        GameSystem gs = await gsRepo.FindByIdAsync(gameSystemId);

        return new() {
            GameSystemId = gs.Id,
            CreatedDTTM = DateTime.UtcNow,
            Components = await RunPlayerSetupSteps(gs)
        };
    }

    private async Task<List<Component>> RunPlayerSetupSteps(GameSystem gameSystem) {
        List<Component> returnComponents = new();
        
        foreach (PlayerSetupStep playerStep in gameSystem.PlayerConfig.Steps.OrderBy(st => st.StepOrder)) {
            // Pull Component for the step Component Type
            Component[] components = gameSystem.Components.Where(com => com.ComponentTypeId == playerStep.ComponentTypeId).ToArray();

            // Determine component option count
            int optionCount = components.Count();

            // Select random component for selection count
            int selectionIndex = RandomUtil.Get(0, optionCount - 1);

            // Attach Selected Component to the game instance
            returnComponents.Add(components[selectionIndex]);
        }

        return returnComponents;
    }
}
