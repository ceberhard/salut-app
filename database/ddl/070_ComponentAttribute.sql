USE SalutApp;
CREATE TABLE IF NOT EXISTS ComponentAttribute
(
    Id BIGINT AUTO_INCREMENT PRIMARY KEY,
	ComponentAttributeTypeId BIGINT NOT NULL,
	ComponentId BIGINT NOT NULL,
	Value BIGINT NOT NULL,
	FOREIGN KEY (ComponentAttributeTypeId) REFERENCES ComponentAttributeType(Id),
	FOREIGN KEY (ComponentId) REFERENCES Component(Id)
);