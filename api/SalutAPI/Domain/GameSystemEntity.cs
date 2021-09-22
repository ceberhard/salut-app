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
                ComponentPointLimit = 50,
                AttributeApplyPercent = 50,
                RetryTolerance = 10
            })
        };
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
                        Component component = SelectComponent(components);
                        // Attach Selected Component to the game instance
                        if (CheckPointCount(component)) {
                            component.Children = await SelectChildComponents(gameSystem, component);
                            playerComponents.Add(component);
                        }

                        


                    } else {
                        // Loop through each player component



                    }
                    retryCount++;
                } while(_pointTotal < (_gameSystemOpts.ComponentPointLimit ?? 0) || playerComponents.Count() < playerStep.SelectionCount || retryCount < _gameSystemOpts.RetryTolerance);
                

                returnComponents.AddRange(playerComponents);

                
            }
        }
        
        return returnComponents;
    }

    

    private async Task<List<Component>> SelectChildComponents(GameSystem gs, Component parentComponent) {
        List<Component> childComponents = new();
        Component[] children = GatherChildComponents(gs, parentComponent.Id);
        if (children.Any()) {
            Component c = SelectComponent(children);
            if (CheckPointCount(c)) {
                List<Component> addComponents = await SelectChildComponents(gs, c);
                if (c.Attributes?.Any() ?? false) {
                    addComponents.AddRange(await SelectAttributes(gs, c));
                }

                c.Children = addComponents;
                childComponents.Add(c);
            }
        }
        return childComponents;
    }

    private async Task<List<Component>> SelectAttributes(GameSystem gs, Component srcComponent) {
        List<Component> attributes = new();
        await foreach(var att in FetchComponentTypeAttributes(srcComponent)) {
            Component[] attComponents = GatherComponents(gs, (int)att.Value);
            Component attComp = SelectComponent(attComponents);
            if (CheckPointCount(attComp).IsValid) {
                attributes.Add(attComp);
            }
        }
        return attributes;
    }

    private (bool IsValid, int PointValue, int RollupPointValue) CheckPointCount(Component component) {



        // Get Component Point Cost
        int pointCost = (int) (component?.Attributes?.FirstOrDefault(a => a.Type == ComponentAttributeType.PointCost)?.Value ?? 0);
        
        // Compare to Running Point Total
        if ((pointCost + _pointTotal) <= _gameSystemOpts.ComponentPointLimit) {
            _pointTotal += pointCost;

            // Get Rollup PointCost

            return (true, pointCost, 0);
        } else {
            return (false, pointCost, 0);
        }
    }

    private async IAsyncEnumerable<ComponentAttribute> FetchComponentTypeAttributes(Component c) { foreach(var att in c.Attributes.Where(x => x.Type == ComponentAttributeType.ComponentType)) yield return att; }

    private Component SelectComponent(IEnumerable<Component> components) {
        int selectionIndex = RandomUtil.Get(0, components.Count());
        return components.Skip(selectionIndex).First();
    }

    private Component[] GatherComponents(GameSystem gs, long componentTypeId) => gs.Components.Where(c => c.ComponentTypeId == componentTypeId).ToArray();

    private Component[] GatherChildComponents(GameSystem gs, long parentComponentId) => gs.Components.Where(c => c.ParentComponentId.HasValue && c.ParentComponentId.Value == parentComponentId).ToArray();
}
