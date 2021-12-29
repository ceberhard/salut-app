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
            AttributeApplyPercent = 50,
            RetryTolerance = 10
        });

        return new() {
            GameSystemId = gs.Id,
            CreatedDTTM = DateTime.UtcNow,
            Components = _gameComponents
        };
    }

    public async Task<GameSystem> CreateGameSystem(GameSystem gameSystem) {
        using (GameSystemRepo gameSystemRepo = new()) {
            _ = await gameSystemRepo.GameSystem.AddAsync(gameSystem);
            _ = await gameSystemRepo.SaveChangesAsync();
            return gameSystem;
        }
    }

    private async IAsyncEnumerable<ComponentAttribute> FetchComponentTypeAttributes(IEnumerable<ComponentAttribute> atts) { foreach(var att in atts.Where(x => x.Type == ComponentAttributeType.AppendComponentType)) yield return att; }

    private Component[] GatherChildComponents(GameSystem gs, long parentComponentId) => gs.Components.Where(c => c.ParentComponentId.HasValue && c.ParentComponentId.Value == parentComponentId).ToArray();

    private Component[] GatherComponents(GameSystem gs, long componentTypeId, Component currentComponent, params GameInstanceComponent[] checkComponents) {
        List<Component> gathered = new();
        var comps = gs.Components.Where(c => c.ComponentTypeId == componentTypeId);
        foreach(Component comp in comps) {
            var addComp = false;
            var atts = comp.Attributes?.Where(att => att.Type == ComponentAttributeType.ComponentRestriction).ToArray() ?? new ComponentAttribute[0];
            addComp = (!atts.Any() || atts.All(att => CheckComponentRestrictions((long)att.Value, checkComponents)));
            
            if (addComp) {
                var attRestricts = comp.Attributes?.Where(x => x.Type == ComponentAttributeType.AttributeRestriction).ToArray() ?? new ComponentAttribute[0];
                addComp = (!attRestricts.Any() || (currentComponent != null && CheckAttributeRestrictions(attRestricts, currentComponent.Attributes)));
            }
            
            if (addComp) gathered.Add(comp);
        }
        return gathered.ToArray();
    }

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
            /*
            foreach (PlayerSetupStep playerStep in gameSystem.PlayerConfig.Steps.OrderBy(st => st.StepOrder)) {
                List<GameInstanceComponent> playerComponents = new();

                do {
                    if (!playerComponents.Any() || playerComponents.Count() < playerStep.SelectionCount) {
                        // Pull Component for the step Component Type
                        
                        Component[] components = GatherComponents(gameSystem, playerStep.ComponentTypeId, null, playerComponents.ToArray());
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
            */
        }
    }

    private async Task<List<GameInstanceComponent>> SelectAttributes(GameSystem gs, Component currentComponent, List<GameInstanceComponent> components) {
        List<GameInstanceComponent> componentAttributes = new();
        await foreach(var att in FetchComponentTypeAttributes(currentComponent.Attributes)) {
            if (Util.RandomUtil.CheckPercent(_gameSystemOpts.AttributeApplyPercent)) {
                Component[] attComponents = GatherComponents(gs, (int)att.Value, currentComponent, components.ToArray());
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
            (bool isValid, int points) = VerifyComponent(selectedComponent, new List<GameInstanceComponent> { parentComponent });
            if (isValid) {
                GameInstanceComponent c = new(selectedComponent, points, 0); // rollupPoints);
                List<GameInstanceComponent> addComponents = await SelectChildComponents(gs, c);
                if (selectedComponent.Attributes?.Any() ?? false) {
                    var checkComponents = new List<GameInstanceComponent> { parentComponent };
                    checkComponents.AddRange(childComponents);
                    addComponents.AddRange(await SelectAttributes(gs, selectedComponent, checkComponents));
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
            if (components.Count() == 1) return components.First();

            int selectionIndex = RandomUtil.Get(0, components.Count());
            return components.Skip(selectionIndex).First();
        }
        return null;
    }

    private GameInstanceComponent[] BuildCheckList(List<GameInstanceComponent> addtlComponents) {
        var checkList = new List<GameInstanceComponent>();
        checkList.AddRange(_gameComponents);
        checkList.AddRange(addtlComponents ?? new());
        return checkList.ToArray();
    }

    private (bool IsValid, int PointValue) VerifyComponent(Component component, List<GameInstanceComponent> addtlComponents = null) {
        if (component == null) return (false, 0);

        var checkList = BuildCheckList(addtlComponents);

        if ((!component.InstanceLimit.HasValue) || GetCurrentInstanceCount(component.Id, checkList) < component.InstanceLimit.Value) {
            // Check Any Component Restrictions
            /*
            var restrictions = component.Attributes?.Where(c => c.Type == ComponentAttributeType.ComponentRestriction);
            foreach(ComponentAttribute restriction in restrictions ?? new ComponentAttribute[0]) {
                if (!restrictions.All(r => CheckComponentRestrictions(r.Id, checkList))) {
                    return (false, 0);
                }
            }
            */


            // if (!component.Attributes?.Where(c => c.Type == ComponentAttributeType.ComponentRestriction).All(att => checkList.Any(c => c.ComponentId == att.Id)) ?? false) {
            //    return (false, 0);
            //}
            
            // Get Component Point Cost
            int pointCost = (int) (component.Attributes?.FirstOrDefault(a => a.Type == ComponentAttributeType.PointCost)?.Value ?? 0);

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

    private bool CheckComponentRestrictions(long checkId, IEnumerable<GameInstanceComponent> checkList) {
        foreach(GameInstanceComponent c in checkList) {
            if (c.ComponentId == checkId) return true;
            if (CheckComponentRestrictions(checkId, c.Children)) return true;
        }
        return false;
    }

    private bool CheckAttributeRestrictions(ComponentAttribute[] checkAtts, IEnumerable<ComponentAttribute> attList) {
        return checkAtts.All(ca => attList.Any(x => x.Type == ComponentAttributeType.Descriptive && x.Value == ca.Value));
    }
}
