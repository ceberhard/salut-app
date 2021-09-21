using System.Threading;

namespace SalutAPI.Domain;

public class GameSystemRepo {

    public async Task<GameSystem> FindByIdAsync(long id) {
        return new GameSystem(1001, "X-Wind 2nd Edition") {
            SystemConfig = new GameSystemConfig {
                Id = 8801,
                PlayerMinCount = 2,
                PlayerMaxCount = 2
            },
            PlayerConfig = new PlayerConfig {
                Id = 9901,
                PointsAllowed = 200,
                Steps = new List<PlayerSetupStep> {
                    new PlayerSetupStep {
                        Id = 721,
                        Name = "Select Faction",
                        ComponentTypeId = 20,
                        SelectionCount = 1,
                        StepOrder = 1
                    }
                }
            },
            Components = new List<Component>() {
                new Component { Id = 101, Name = "Rebels", ComponentTypeId = 20, ComponentType = new() { Id = 20, Name = "Faction" } },
                new Component { Id = 102, Name = "Imperials", ComponentTypeId = 20, ComponentType = new() { Id = 20, Name = "Faction" } },
                new Component {
                    Id = 109,
                    ParentComponentId = 101,
                    Name = "Hera Syndulla (A/SF-01 B-wing)",
                    ComponentTypeId = 21,
                    ComponentType = new () { Id = 21, Name = "Pilot (Ship)" },
                    Attributes = new List<ComponentAttribute>() {
                        new() { Id = 771, Value = 50, Type = ComponentAttributeType.ComponentType },
                        new() { Id = 772, Value = 51, Type = ComponentAttributeType.ComponentType },
                        new() { Id = 773, Value = 51, Type = ComponentAttributeType.ComponentType },
                        new() { Id = 774, Value = 52, Type = ComponentAttributeType.ComponentType },
                        new() { Id = 798, Value = 55, Type = ComponentAttributeType.PointCost }
                    }
                },new Component {
                    Id = 110,
                    ParentComponentId = 101,
                    Name = "Jek Porkins (T-65 X-wing)",
                    ComponentTypeId = 21,
                    ComponentType = new () { Id = 21, Name = "Pilot (Ship)" },
                    Attributes = new List<ComponentAttribute>() {
                        new() { Id = 775, Value = 50, Type = ComponentAttributeType.ComponentType },
                        new() { Id = 776, Value = 52, Type = ComponentAttributeType.ComponentType },
                        new() { Id = 777, Value = 53, Type = ComponentAttributeType.ComponentType },
                        new() { Id = 799, Value = 44, Type = ComponentAttributeType.PointCost }
                    }
                },new Component {
                    Id = 111,
                    ParentComponentId = 101,
                    Name = "Pheonix Squadron Pilot (RZ-1 A-wing)",
                    ComponentTypeId = 21,
                    ComponentType = new () { Id = 21, Name = "Pilot (Ship)" },
                    Attributes = new List<ComponentAttribute>() {
                        new() { Id = 778, Value = 50, Type = ComponentAttributeType.ComponentType },
                        new() { Id = 800, Value = 28, Type = ComponentAttributeType.PointCost }
                    }
                },new Component {
                    Id = 112,
                    ParentComponentId = 102,
                    Name = "Major Rhymer (TIE/sa Bomber)",
                    ComponentTypeId = 21,
                    ComponentType = new () { Id = 21, Name = "Pilot (Ship)" },
                    Attributes = new List<ComponentAttribute>() {
                        new() { Id = 779, Value = 50, Type = ComponentAttributeType.ComponentType },
                        new() { Id = 780, Value = 52, Type = ComponentAttributeType.ComponentType },
                        new() { Id = 781, Value = 54, Type = ComponentAttributeType.ComponentType },
                        new() { Id = 782, Value = 54, Type = ComponentAttributeType.ComponentType },
                        new() { Id = 801, Value = 37, Type = ComponentAttributeType.PointCost }
                    }
                },new Component {
                    Id = 112,
                    ParentComponentId = 102,
                    Name = "Academy Pilot (TIE/ln Fighter)",
                    ComponentTypeId = 21,
                    ComponentType = new () { Id = 21, Name = "Pilot (Ship)" },
                    Attributes = new List<ComponentAttribute>() {
                        new() { Id = 802, Value = 22, Type = ComponentAttributeType.PointCost }
                    }
                },
                new Component { Id = 113, Name = "Daredevil", ComponentTypeId = 50, ComponentType = new() { Id = 50, Name = "Talent Upgrade" },
                    Attributes = new List<ComponentAttribute> { new() { Id = 784, Value = 2, Type = ComponentAttributeType.PointCost } } },
                new Component { Id = 114, Name = "Intimidation", ComponentTypeId = 50, ComponentType = new() { Id = 50, Name = "Talent Upgrade" },
                    Attributes = new List<ComponentAttribute> { new() { Id = 785, Value = 3, Type = ComponentAttributeType.PointCost } } },
                new Component { Id = 115, Name = "Swarm Tactics", ComponentTypeId = 50, ComponentType = new() { Id = 50, Name = "Talent Upgrade" },
                    Attributes = new List<ComponentAttribute> { new() { Id = 786, Value = 0, Type = ComponentAttributeType.PointCost } } },
                new Component { Id = 116, Name = "Trick Shot", ComponentTypeId = 50, ComponentType = new() { Id = 50, Name = "Talent Upgrade" },
                    Attributes = new List<ComponentAttribute> { new() { Id = 787, Value = 4, Type = ComponentAttributeType.PointCost } } },
                new Component { Id = 117, Name = "Autoblasters", ComponentTypeId = 51, ComponentType = new() { Id = 51, Name = "Cannon Upgrade" },
                    Attributes = new List<ComponentAttribute> { new() { Id = 788, Value = 3, Type = ComponentAttributeType.PointCost } } },
                new Component { Id = 118, Name = "Ion Cannon", ComponentTypeId = 51, ComponentType = new() { Id = 51, Name = "Cannon Upgrade" },
                    Attributes = new List<ComponentAttribute> { new() { Id = 789, Value = 6, Type = ComponentAttributeType.PointCost } } },
                new Component { Id = 119, Name = "Tractor Beam", ComponentTypeId = 51, ComponentType = new() { Id = 51, Name = "Cannon Upgrade" },
                    Attributes = new List<ComponentAttribute> { new() { Id = 790, Value = 4, Type = ComponentAttributeType.PointCost } } },
                new Component { Id = 120, Name = "Ion Torpedoes", ComponentTypeId = 52, ComponentType = new() { Id = 52, Name = "Torpedo Upgrade" },
                    Attributes = new List<ComponentAttribute> { new() { Id = 791, Value = 4, Type = ComponentAttributeType.PointCost } } },
                new Component { Id = 121, Name = "Plasma Tropedoes", ComponentTypeId = 52, ComponentType = new() { Id = 52, Name = "Torpedo Upgrade" },
                    Attributes = new List<ComponentAttribute> { new() { Id = 792, Value = 7, Type = ComponentAttributeType.PointCost } } },
                new Component { Id = 122, Name = "R2 Astromech", ComponentTypeId = 53, ComponentType = new() { Id = 53, Name = "Astromech Upgrade" },
                    Attributes = new List<ComponentAttribute> { new() { Id = 793, Value = 0, Type = ComponentAttributeType.PointCost } } },
                new Component { Id = 123, Name = "R5 Astromech", ComponentTypeId = 53, ComponentType = new() { Id = 53, Name = "Astromech Upgrade" },
                    Attributes = new List<ComponentAttribute> { new() { Id = 794, Value = 4, Type = ComponentAttributeType.PointCost } } },
                new Component { Id = 124, Name = "Conner Nets", ComponentTypeId = 54, ComponentType = new() { Id = 54, Name = "Device Upgrade" },
                    Attributes = new List<ComponentAttribute> { new() { Id = 795, Value = 3, Type = ComponentAttributeType.PointCost } } },
                new Component { Id = 125, Name = "Proton Bombs", ComponentTypeId = 54, ComponentType = new() { Id = 54, Name = "Device Upgrade" },
                    Attributes = new List<ComponentAttribute> { new() { Id = 796, Value = 4, Type = ComponentAttributeType.PointCost } } },
                new Component { Id = 126, Name = "Thermal Detonators", ComponentTypeId = 54, ComponentType = new() { Id = 54, Name = "Device Upgrade" },
                    Attributes = new List<ComponentAttribute> { new() { Id = 797, Value = 3, Type = ComponentAttributeType.PointCost } } }
            }
        };
    }

}
