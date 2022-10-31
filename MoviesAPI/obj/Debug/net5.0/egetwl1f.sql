BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Genres]') AND [c].[name] = N'Name');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Genres] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Genres] ALTER COLUMN [Name] nvarchar(50) NULL;
GO

CREATE TABLE [Movies] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(75) NOT NULL,
    [Summary] nvarchar(max) NULL,
    [Trailer] nvarchar(max) NULL,
    [InTheaters] bit NOT NULL,
    [ReleaseDate] datetime2 NOT NULL,
    [Poster] nvarchar(max) NULL,
    CONSTRAINT [PK_Movies] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [MoviesActors] (
    [ActorId] int NOT NULL,
    [MovieId] int NOT NULL,
    [Character] nvarchar(75) NULL,
    [Order] int NOT NULL,
    CONSTRAINT [PK_MoviesActors] PRIMARY KEY ([ActorId], [MovieId]),
    CONSTRAINT [FK_MoviesActors_Actors_ActorId] FOREIGN KEY ([ActorId]) REFERENCES [Actors] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MoviesActors_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [MoviesGenres] (
    [GenreId] int NOT NULL,
    [MovieId] int NOT NULL,
    CONSTRAINT [PK_MoviesGenres] PRIMARY KEY ([MovieId], [GenreId]),
    CONSTRAINT [FK_MoviesGenres_Genres_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genres] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MoviesGenres_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [MovieTheatersMovies] (
    [MovieTheaterId] int NOT NULL,
    [MovieId] int NOT NULL,
    CONSTRAINT [PK_MovieTheatersMovies] PRIMARY KEY ([MovieId], [MovieTheaterId]),
    CONSTRAINT [FK_MovieTheatersMovies_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MovieTheatersMovies_MovieTheaters_MovieTheaterId] FOREIGN KEY ([MovieTheaterId]) REFERENCES [MovieTheaters] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_MoviesActors_MovieId] ON [MoviesActors] ([MovieId]);
GO

CREATE INDEX [IX_MoviesGenres_GenreId] ON [MoviesGenres] ([GenreId]);
GO

CREATE INDEX [IX_MovieTheatersMovies_MovieTheaterId] ON [MovieTheatersMovies] ([MovieTheaterId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221018221319_MovieAndFriends', N'5.0.17');
GO

COMMIT;
GO

