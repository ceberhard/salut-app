USE SalutApp;
CREATE TABLE IF NOT EXISTS PlayerSetupStep
(
    Id BIGINT AUTO_INCREMENT PRIMARY KEY,
    PlayerConfigId BIGINT NOT NULL,
	ComponentTypeId BIGINT NOT NULL,
	StepOrder INT NOT NULL,
	SelectionCount INT NOT NULL,
	Name VARCHAR(256) NOT NULL,
	FOREIGN KEY (PlayerConfigId) REFERENCES PlayerConfig(Id),
	FOREIGN KEY (ComponentTypeId) REFERENCES ComponentType(Id)
);