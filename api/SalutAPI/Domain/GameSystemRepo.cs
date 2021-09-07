using System.Threading;

namespace SalutAPI.Domain;

public class GameSystemRepo {

    public async Task<GameSystem> FindById(long id) {
        return new GameSystem(1001, "X-Wind 2nd Edition") {
            Components = new List<Component>() {
                new Component {
                    Id = 101,
                    Name = "Rebels",
                    ComponentType = new() {
                        Id = 20,
                        Name = "Faction"
                    }
                },new Component {
                    Id = 102,
                    Name = "Imperials",
                    ComponentType = new() {
                        Id = 20,
                        Name = "Faction"
                    }
                },new Component {
                    Id = 103,
                    Name = "Scum & Villainy",
                    ComponentType = new() {
                        Id = 20,
                        Name = "Faction"
                    }
                },new Component {
                    Id = 104,
                    Name = "Resistance",
                    ComponentType = new() {
                        Id = 20,
                        Name = "Faction"
                    }
                },new Component {
                    Id = 105,
                    Name = "First Order",
                    ComponentType = new() {
                        Id = 20,
                        Name = "Faction"
                    }
                },new Component {
                    Id = 106,
                    Name = "Republic",
                    ComponentType = new() {
                        Id = 20,
                        Name = "Faction"
                    }
                },new Component {
                    Id = 107,
                    Name = "Seperatists",
                    ComponentType = new() {
                        Id = 20,
                        Name = "Faction"
                    }
                }
            }
        };
    }

}
