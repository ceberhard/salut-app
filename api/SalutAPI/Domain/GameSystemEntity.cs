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
            Component[] components = GatherComponents(gameSystem, playerStep.ComponentTypeId);

            Component c = SelectComponent(components);

            c.Children = await SelectChildComponents(gameSystem, c);

            // Attach Selected Component to the game instance
            returnComponents.Add(c);
        }

        return returnComponents;
    }

    private async Task<List<Component>> SelectChildComponents(GameSystem gs, Component parentComponent) {
        List<Component> childComponents = new();
        Component[] children = GatherChildComponents(gs, parentComponent.Id);
        if (children.Any()) {
            Component c = SelectComponent(children);
            List<Component> addComponents = await SelectChildComponents(gs, c);
            if (c.Attributes?.Any() ?? false) {
                addComponents.AddRange(await SelectAttributes(gs, c));
            }

            c.Children = addComponents;
            childComponents.Add(c);
        }
        return childComponents;
    }

    private async Task<List<Component>> SelectAttributes(GameSystem gs, Component srcComponent) {
        List<Component> attributes = new();
        await foreach(var att in FetchComponentTypeAttributes(srcComponent)) {
            Component[] attComponents = GatherComponents(gs, (int)att.Value);
            attributes.Add(SelectComponent(attComponents));
        }
        return attributes;
    }

    private async IAsyncEnumerable<ComponentAttribute> FetchComponentTypeAttributes(Component c) { foreach(var att in c.Attributes.Where(x => x.Type == ComponentAttributeType.ComponentType))  yield return att; }

    private Component SelectComponent(IEnumerable<Component> components) {
        int selectionIndex = RandomUtil.Get(0, components.Count());
        return components.Skip(selectionIndex).First();
    }

    private Component[] GatherComponents(GameSystem gs, long componentTypeId) => gs.Components.Where(c => c.ComponentTypeId == componentTypeId).ToArray();

    private Component[] GatherChildComponents(GameSystem gs, long parentComponentId) => gs.Components.Where(c => c.ParentComponentId.HasValue && c.ParentComponentId.Value == parentComponentId).ToArray();
}
