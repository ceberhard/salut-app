using System;
using SalutAPI.Util;

namespace SalutAPI.Domain;

public class GameSystemEntity {
    private GameSystemOptions _gameSystemOpts = new();
    private List<GameInstanceComponent> _gameComponents = new();
    private int _pointTotal = 0;

    public async Task<GameInstance> BuildGameInstanceAsync(long gameSystemId) {
        GameSystemRepo gsRepo = new();
        GameSystem gs = await gsRepo.FindByIdAsync(gameSystemId);

        await RunPlayerSetupStepsAsync(gs, () => new GameSystemOptions {
            PlayerCount = 2,
            ComponentPointLimit = 200,
            AttributeApplyPercent = 40,
            RetryTolerance = 10
        });

        return new() {
            GameSystemId = gs.Id,
            CreatedDTTM = DateTime.UtcNow,
            Components = _gameComponents
        };
    }

    private async IAsyncEnumerable<ComponentAttribute> FetchComponentTypeAttributes(IEnumerable<ComponentAttribute> atts) { foreach(var att in atts.Where(x => x.Type == ComponentAttributeType.AppendComponentType)) yield return att; }

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

    private async Task RunPlayerSetupStepsAsync(GameSystem gameSystem, Func<GameSystemOptions>? gameSystemOptions = null) {
        if (gameSystemOptions != null) _gameSystemOpts = gameSystemOptions();

        // List<GameInstanceComponent> returnComponents = new();
        
        for(int playerItem = 0; playerItem < _gameSystemOpts.PlayerCount; playerItem++) {
            _pointTotal = 0;
            int retryCount = 0;

            foreach (PlayerSetupStep playerStep in gameSystem.PlayerConfig.Steps.OrderBy(st => st.StepOrder)) {
                List<GameInstanceComponent> playerComponents = new();

                do {
                    if (!playerComponents.Any() || playerComponents.Count() < playerStep.SelectionCount) {
                        // Pull Component for the step Component Type
                        Component[] components = GatherComponents(gameSystem, playerStep.ComponentTypeId);
                        if (components?.Any() ?? false) {
                            Component component = SelectComponent(components);
                            // Attach Selected Component to the game instance
                            (bool isValid, int points) = VerifyComponent(component, playerComponents);
                            if (isValid) {
                                GameInstanceComponent gComponent= new(component, points, 0);
                                gComponent.Children = await SelectChildComponents(gameSystem, gComponent);
                                gComponent.RollupPointValue = GetRollupCost(gComponent);
                                playerComponents.Add(gComponent);
                            }
                        }
                    } else {
                        // Loop through each player component
                        foreach(GameInstanceComponent component in playerComponents) {
                            component.Children.AddRange(await SelectChildComponents(gameSystem, component));
                            component.RollupPointValue = GetRollupCost(component);
                        }
                    }
                    retryCount++;
                } while((_pointTotal < (_gameSystemOpts.ComponentPointLimit ?? 0) || playerComponents.Count() < playerStep.SelectionCount) && retryCount < _gameSystemOpts.RetryTolerance);

                _gameComponents.AddRange(playerComponents);
            }
        }
    }

    private async Task<List<GameInstanceComponent>> SelectAttributes(GameSystem gs, IEnumerable<ComponentAttribute> attributes) {
        List<GameInstanceComponent> componentAttributes = new();
        await foreach(var att in FetchComponentTypeAttributes(attributes)) {
            if (Util.RandomUtil.CheckPercent(_gameSystemOpts.AttributeApplyPercent)) {
                Component[] attComponents = GatherComponents(gs, (int)att.Value);
                Component attComp = SelectComponent(attComponents);
                (bool isValid, int points) = VerifyComponent(attComp, componentAttributes);
                if (isValid) {
                    componentAttributes.Add(new GameInstanceComponent(attComp, attComp.Id, points, 0)); //rollupPoints));
                }
            }
        }
        return componentAttributes;
    }

    private async Task<List<GameInstanceComponent>> SelectChildComponents(GameSystem gs, GameInstanceComponent parentComponent) {
        List<GameInstanceComponent> childComponents = new();
        Component[] children = GatherChildComponents(gs, parentComponent.ComponentId);
        if (children.Any()) {
            Component selectedComponent = SelectComponent(children);
            (bool isValid, int points) = VerifyComponent(selectedComponent);
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
        if (components?.Any() ?? false) {
            int selectionIndex = RandomUtil.Get(0, components.Count());
            return components.Skip(selectionIndex).First();
        }
        return null;
    }

    private (bool IsValid, int PointValue) VerifyComponent(Component component, List<GameInstanceComponent> addtlComponents = null) {
        var checkList = _gameComponents.ToArray();
        checkList.AddRange(addtlComponents ?? new());

        if ((!component?.InstanceLimit.HasValue ?? false) || GetCurrentInstanceCount(component.Id, checkList) < component.InstanceLimit.Value) {
            // Get Component Point Cost
            int pointCost = (int) (component?.Attributes?.FirstOrDefault(a => a.Type == ComponentAttributeType.PointCost)?.Value ?? 0);

            // Compare to Running Point Total
            if ((pointCost + _pointTotal) <= _gameSystemOpts.ComponentPointLimit) {
                _pointTotal += pointCost;

                return (true, pointCost);
            } else {
                return (false, pointCost);
            }    
        } else {
            return (false, 0);
        }
    }

    private int GetCurrentInstanceCount(long componentId, GameInstanceComponent[] components) {
        int instanceCount = 0;
        foreach(GameInstanceComponent c in components) {
            instanceCount += (c.ComponentId == componentId) ? 1 : 0;
            instanceCount += GetCurrentInstanceCount(componentId, c.Children.ToArray());
        }
        return instanceCount;
    }
}
