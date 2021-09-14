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
                    },
                    new PlayerSetupStep {
                        Id = 722,
                        Name = "Build Ship List",
                        ComponentTypeId = 21,
                        SelectionCount = 1,
                        StepOrder = 2
                    }
                }
            },
            Components = new List<Component>() {
                new Component {
                    Id = 101,
                    Name = "Rebels",
                    ComponentTypeId = 20,
                    ComponentType = new() {
                        Id = 20,
                        Name = "Faction"
                    }
                },new Component {
                    Id = 102,
                    Name = "Imperials",
                    ComponentTypeId = 20,
                    ComponentType = new() {
                        Id = 20,
                        Name = "Faction"
                    }
                },new Component {
                    Id = 103,
                    Name = "Scum & Villainy",
                    ComponentTypeId = 20,
                    ComponentType = new() {
                        Id = 20,
                        Name = "Faction"
                    }
                },new Component {
                    Id = 104,
                    Name = "Resistance",
                    ComponentTypeId = 20,
                    ComponentType = new() {
                        Id = 20,
                        Name = "Faction"
                    }
                },new Component {
                    Id = 105,
                    Name = "First Order",
                    ComponentTypeId = 20,
                    ComponentType = new() {
                        Id = 20,
                        Name = "Faction"
                    }
                },new Component {
                    Id = 106,
                    Name = "Republic",
                    ComponentTypeId = 20,
                    ComponentType = new() {
                        Id = 20,
                        Name = "Faction"
                    }
                },new Component {
                    Id = 107,
                    Name = "Seperatists",
                    ComponentTypeId = 20,
                    ComponentType = new() {
                        Id = 20,
                        Name = "Faction"
                    }
                },new Component {
                    Id = 109,
                    Name = "Hera Syndulla (A/SF-01 B-wing)",
                    ComponentTypeId = 21,
                    ComponentType = new () {
                        Id = 21,
                        Name = "Pilot (Ship)"
                    }
                },new Component {
                    Id = 110,
                    Name = "Jek Porkins (T-65 X-wing)",
                    ComponentTypeId = 21,
                    ComponentType = new () {
                        Id = 21,
                        Name = "Pilot (Ship)"
                    }
                },new Component {
                    Id = 111,
                    Name = "Pheonix Squadron Pilot (RZ-1 A-wing)",
                    ComponentTypeId = 21,
                    ComponentType = new () {
                        Id = 21,
                        Name = "Pilot (Ship)"
                    }
                },new Component {
                    Id = 112,
                    Name = "Major Rhymer (TIE/sa Bomber)",
                    ComponentTypeId = 21,
                    ComponentType = new () {
                        Id = 21,
                        Name = "Pilot (Ship)"
                    }
                },new Component {
                    Id = 112,
                    Name = "Academy Pilot (TIE/ln Fighter)",
                    ComponentTypeId = 21,
                    ComponentType = new () {
                        Id = 21,
                        Name = "Pilot (Ship)"
                    }
                }
            }
        };
    }

}
