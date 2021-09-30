using System;
using SalutAPI.Util;

namespace SalutAPI.Domain;

public class GameSystemEntity {
    private GameSystemOptions _gameSystemOpts = new();
    private int _pointTotal = 0;

    public async Task<GameInstance> BuildGameInstanceAsync(long gameSystemId) {
        GameSystemRepo gsRepo = new();
        GameSystem gs = await gsRepo.FindByIdAsync(gameSystemId);

        return new() {
            GameSystemId = gs.Id,
            CreatedDTTM = DateTime.UtcNow,
            Components = await RunPlayerSetupStepsAsync(gs, () => new GameSystemOptions {
                PlayerCount = 2,
                ComponentPointLimit = 100,
                AttributeApplyPercent = 50,
                RetryTolerance = 10
            })
        };
    }

    private (bool IsValid, int PointValue) CheckPointCount(Component component) {

        // Get Component Point Cost
        int pointCost = (int) (component?.Attributes?.FirstOrDefault(a => a.Type == ComponentAttributeType.PointCost)?.Value ?? 0);

        // Compare to Running Point Total
        if ((pointCost + _pointTotal) <= _gameSystemOpts.ComponentPointLimit) {
            _pointTotal += pointCost;

            return (true, pointCost);
        } else {
            return (false, pointCost);
        }
    }

    private async IAsyncEnumerable<ComponentAttribute> FetchComponentTypeAttributes(IEnumerable<ComponentAttribute> atts) { foreach(var att in atts.Where(x => x.Type == ComponentAttributeType.ComponentType)) yield return att; }

    private Component[] GatherChildComponents(GameSystem gs, long parentComponentId) => gs.Components.Where(c => c.ParentComponentId.HasValue && c.ParentComponentId.Value == parentComponentId).ToArray();

    private Component[] GatherComponents(GameSystem gs, long componentTypeId) => gs.Components.Where(c => c.ComponentTypeId == componentTypeId).ToArray();

    private int GetRollupCost(GameInstanceComponent instanceComponent) {
        int? rollUp = null;
        return GetRollupCostLocal(instanceComponent, ref rollUp);

        int GetRollupCostLocal(GameInstanceComponent instanceComponent, ref int? rc) {
            if (rc == null) rc = 0;
            rc += instanceComponent.PointValue;
            foreach(GameInstanceComponent child in instanceComponent.Children) {
                GetRollupCostLocal(child, ref rc);
            }
            return rc.Value;
        }
    }

    private async Task<List<GameInstanceComponent>> RunPlayerSetupStepsAsync(GameSystem gameSystem, Func<GameSystemOptions>? gameSystemOptions = null) {
        if (gameSystemOptions != null) _gameSystemOpts = gameSystemOptions();

        List<GameInstanceComponent> returnComponents = new();
        
        for(int playerItem = 0; playerItem < _gameSystemOpts.PlayerCount; playerItem++) {
            _pointTotal = 0;
            int retryCount = 0;

            foreach (PlayerSetupStep playerStep in gameSystem.PlayerConfig.Steps.OrderBy(st => st.StepOrder)) {
                List<GameInstanceComponent> playerComponents = new();

                do {
                    if (!playerComponents.Any() || playerComponents.Count() < playerStep.SelectionCount) {
                        // Pull Component for the step Component Type
                        Component[] components = GatherComponents(gameSystem, playerStep.ComponentTypeId);
                        GameInstanceComponent component = new(SelectComponent(components), 0, 0);
                        // Attach Selected Component to the game instance
                        (bool isValid, int points) = CheckPointCount(component);
                        if (isValid) {
                            component.PointValue = points;
                            component.Children = await SelectChildComponents(gameSystem, component);
                            component.RollupPointValue = GetRollupCost(component);
                            playerComponents.Add(component);
                        }
                    } else {
                        // Loop through each player component
                        




                    }
                    retryCount++;
                } while((_pointTotal < (_gameSystemOpts.ComponentPointLimit ?? 0) || playerComponents.Count() < playerStep.SelectionCount) && retryCount < _gameSystemOpts.RetryTolerance);

                returnComponents.AddRange(playerComponents);
            }
        }
        
        return returnComponents;
    }

    private async Task<List<GameInstanceComponent>> SelectAttributes(GameSystem gs, IEnumerable<ComponentAttribute> attributes) {
        List<GameInstanceComponent> componentAttributes = new();
        await foreach(var att in FetchComponentTypeAttributes(attributes)) {
            Component[] attComponents = GatherComponents(gs, (int)att.Value);
            Component attComp = SelectComponent(attComponents);
            (bool isValid, int points) = CheckPointCount(attComp);
            if (isValid) {
                componentAttributes.Add(new GameInstanceComponent(attComp, attComp.Id, points, 0)); //rollupPoints));
            }
        }
        return componentAttributes;
    }

    private async Task<List<GameInstanceComponent>> SelectChildComponents(GameSystem gs, GameInstanceComponent parentComponent) {
        List<GameInstanceComponent> childComponents = new();
        Component[] children = GatherChildComponents(gs, parentComponent.ComponentId);
        if (children.Any()) {
            Component selectedComponent = SelectComponent(children);
            (bool isValid, int points) = CheckPointCount(selectedComponent);
            if (isValid) {
                GameInstanceComponent c = new(selectedComponent, points, 0); // rollupPoints);
                List<GameInstanceComponent> addComponents = await SelectChildComponents(gs, c);
                if (selectedComponent.Attributes?.Any() ?? false) {
                    addComponents.AddRange(await SelectAttributes(gs, selectedComponent.Attributes));
                }

                c.Children = addComponents;
                c.RollupPointValue = GetRollupCost(c);
                childComponents.Add(c);
            }
        }
        return childComponents;
    }

    private Component SelectComponent(IEnumerable<Component> components) {
        int selectionIndex = RandomUtil.Get(0, components.Count());
        return components.Skip(selectionIndex).First();
    }
}
