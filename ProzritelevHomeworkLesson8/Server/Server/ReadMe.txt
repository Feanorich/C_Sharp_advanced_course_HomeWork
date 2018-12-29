Прозрителев Александр

ДЛЯ СЕРВЕРА:

Создать в базе данных две таблицы:

CREATE TABLE [dbo].[Workers] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [FIO]        NVARCHAR (MAX) COLLATE Cyrillic_General_CI_AS NOT NULL,
    [Department] INT            NULL,
    CONSTRAINT [PK_dbo.Workers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Departments] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) COLLATE Cyrillic_General_CI_AS NOT NULL,    
    CONSTRAINT [PK_dbo.Departments] PRIMARY KEY CLUSTERED ([Id] ASC)
);

ДЛЯ КЛИЕНТА:

Кнопка "Заполнить" активирует процедуру заполнения (если таблицы пустые)

1. Сотрудники: 
добавляются кнопкой "добавить" (открывается модальное окно)
редактируются дабл кликом (открывается то же самое модальное окно)
удаляются кнопкой "удалить"
2. Отделы: 
добавляются дабл кликом на пустом месте в списке
редактируются изменением имени прямо в списке
удаляются даблкликом на строчке отдела, левее имени