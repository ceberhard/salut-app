USE SalutApp;
CREATE TABLE IF NOT EXISTS Component
(
    Id BIGINT AUTO_INCREMENT PRIMARY KEY,
	GameSystemId BIGINT NOT NULL,
	ComponentTypeId BIGINT NOT NULL,
	ParentComponentId BIGINT NULL,
	Name VARCHAR(256) NOT NULL,
	Description VARCHAR(1028) NULL,
	InstanceLimit INT NULL,
	FOREIGN KEY (GameSystemId) REFERENCES GameSystem(Id),
	FOREIGN KEY (ComponentTypeId) REFERENCES ComponentType(Id),
	FOREIGN KEY (ParentComponentId) REFERENCES Component(Id)
);