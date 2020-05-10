CREATE TABLE PhotoServices
(
	[PhotoId] INT IDENTITY (1, 1) NOT NULL ,
	[Name]      NVARCHAR (100) NOT NULL,
	[PhotoFormat]      NVARCHAR (20) NOT NULL,
	[Description]      NVARCHAR (150) NOT NULL,
	[ColorType]      NVARCHAR (50) NOT NULL,
	[Price]      DECIMAL(16,2) NOT NULL,
	PRIMARY KEY CLUSTERED ([PhotoId] ASC)
);

INSERT INTO PhotoServices (Name,PhotoFormat,Description,ColorType,Price)
VALUES
	(N'Обычная печать', N'jpg, png, TIFF', N'Для ценителей оригинальности, фото просто будет отдано на печать', N'Цветные, Монохромные.', 50.00),
	(N'Удалить красные глаза', N'jpg, png, bmp', N'Перед печатью - наши специалисты устранят еффект красных глаз.', N'Цветные', 111.00),
	(N'Устранение шумов', N'jpg, png, bmp', N'Перед печатью - наши специалисты устранят еффект размытости или шумов.', N'Цветные', 150.00),
	(N'Убрать задний фон', N'jpg, png, bmp', N'Перед печатью - наши специалисты заменят задний фон на белый цвет или любой на выбор. Пожалуйста, согласуйте это с менеджером.', N'Цветные, Монохромные', 120.00),
	(N'Размытие заднего фона', N'jpg, png, bmp', N'Перед печатью - наши специалисты добавят размытие заднего фона.', N'Цветные, Монохромные', 70.00),
	(N'Вложить фото в рамку', N'jpg, png, bmp', N'После печати фото будет вложено в чёрную рамку. Тип рамки согласовуется с менеджером.', N'Цветные, Монохромные', 250.00);
