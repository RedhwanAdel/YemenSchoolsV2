IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] uniqueidentifier NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Citys] (
    [Id] uniqueidentifier NOT NULL,
    [NameAr] nvarchar(100) NOT NULL,
    [NameEn] nvarchar(100) NOT NULL,
    [ImageUrl] nvarchar(max) NULL,
    CONSTRAINT [PK_Citys] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    [UserId1] uniqueidentifier NOT NULL,
    [RoleId1] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId1] FOREIGN KEY ([RoleId1]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId1] FOREIGN KEY ([UserId1]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Parent] (
    [Id] uniqueidentifier NOT NULL,
    [NameAr] nvarchar(100) NOT NULL,
    [NameEn] nvarchar(max) NULL,
    [Phone] nvarchar(max) NULL,
    [Email] nvarchar(100) NULL,
    [Address] nvarchar(max) NULL,
    [NationalId] nvarchar(max) NULL,
    [Gender] int NOT NULL,
    [BirthDate] datetime2 NULL,
    [JobTitle] nvarchar(max) NULL,
    [UserId] uniqueidentifier NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Parent] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Parent_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Regions] (
    [Id] uniqueidentifier NOT NULL,
    [NameAr] nvarchar(100) NOT NULL,
    [NameEn] nvarchar(100) NOT NULL,
    [ImageUrl] nvarchar(max) NULL,
    [CityId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Regions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Regions_Citys_CityId] FOREIGN KEY ([CityId]) REFERENCES [Citys] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Schools] (
    [Id] uniqueidentifier NOT NULL,
    [NameAr] nvarchar(100) NOT NULL,
    [NameEn] nvarchar(100) NOT NULL,
    [DescriptionAr] nvarchar(500) NULL,
    [DescriptionEn] nvarchar(500) NULL,
    [AddressAr] nvarchar(200) NOT NULL,
    [AddressEn] nvarchar(200) NOT NULL,
    [Logo] nvarchar(max) NULL,
    [CoverImage] nvarchar(max) NULL,
    [PostalCode] nvarchar(max) NULL,
    [MainPhone] nvarchar(max) NULL,
    [Email] nvarchar(max) NOT NULL,
    [IsActive] bit NOT NULL,
    [DeactivatedDate] datetime2 NULL,
    [GenderType] int NOT NULL,
    [SchoolType] int NOT NULL,
    [CurriculumType] int NOT NULL,
    [SchoolLevel] int NOT NULL,
    [CityId] uniqueidentifier NOT NULL,
    [RegionId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Schools] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Schools_Citys_CityId] FOREIGN KEY ([CityId]) REFERENCES [Citys] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Schools_Regions_RegionId] FOREIGN KEY ([RegionId]) REFERENCES [Regions] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [SchoolNews] (
    [Id] uniqueidentifier NOT NULL,
    [SchoolId] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [MainPhoto] nvarchar(max) NULL,
    [CreatedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_SchoolNews] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SchoolNews_Schools_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [Schools] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [schoolPhones] (
    [Id] uniqueidentifier NOT NULL,
    [SchoolId] uniqueidentifier NOT NULL,
    [PhoneNumber] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_schoolPhones] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_schoolPhones_Schools_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [Schools] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [SchoolPhotos] (
    [Id] uniqueidentifier NOT NULL,
    [SchoolId] uniqueidentifier NOT NULL,
    [PhotoUrl] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_SchoolPhotos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SchoolPhotos_Schools_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [Schools] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [SchoolRatings] (
    [Id] uniqueidentifier NOT NULL,
    [SchoolId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_SchoolRatings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SchoolRatings_Schools_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [Schools] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Stage] (
    [Id] uniqueidentifier NOT NULL,
    [StageName] nvarchar(max) NOT NULL,
    [SchoolId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Stage] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Stage_Schools_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [Schools] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Subjects] (
    [Id] uniqueidentifier NOT NULL,
    [NameAr] nvarchar(100) NOT NULL,
    [NameEn] nvarchar(100) NOT NULL,
    [SchoolId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
    CONSTRAINT [PK_Subjects] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Subjects_Schools_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [Schools] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Teachers] (
    [Id] uniqueidentifier NOT NULL,
    [NameAr] nvarchar(100) NOT NULL,
    [NameEn] nvarchar(100) NOT NULL,
    [Email] nvarchar(150) NOT NULL,
    [PhoneNumber] nvarchar(20) NOT NULL,
    [Address] nvarchar(250) NOT NULL,
    [Gender] nvarchar(max) NOT NULL,
    [HireDate] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
    [SchoolId] uniqueidentifier NOT NULL,
    [Specialization] nvarchar(100) NOT NULL,
    [EmploymentStatus] nvarchar(50) NULL,
    [ProfilePictureUrl] nvarchar(300) NULL,
    [UserId] uniqueidentifier NULL,
    CONSTRAINT [PK_Teachers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Teachers_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_Teachers_Schools_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [Schools] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [NewsPhotos] (
    [Id] uniqueidentifier NOT NULL,
    [NewsId] uniqueidentifier NOT NULL,
    [PhotoUrl] nvarchar(max) NOT NULL,
    [UploadedDate] datetime2 NOT NULL,
    [SchoolNewsId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_NewsPhotos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_NewsPhotos_SchoolNews_SchoolNewsId] FOREIGN KEY ([SchoolNewsId]) REFERENCES [SchoolNews] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AcademicYears] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    [StageId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AcademicYears] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AcademicYears_Stage_StageId] FOREIGN KEY ([StageId]) REFERENCES [Stage] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Terms] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [AcademicYearId] uniqueidentifier NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Terms] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Terms_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYears] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Grades] (
    [Id] uniqueidentifier NOT NULL,
    [TermId] uniqueidentifier NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    CONSTRAINT [PK_Grades] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Grades_Terms_TermId] FOREIGN KEY ([TermId]) REFERENCES [Terms] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Sections] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [GradeId] uniqueidentifier NOT NULL,
    [RoomNumber] int NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    CONSTRAINT [PK_Sections] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Sections_Grades_GradeId] FOREIGN KEY ([GradeId]) REFERENCES [Grades] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [SubjectGrades] (
    [Id] uniqueidentifier NOT NULL,
    [SubjectId] uniqueidentifier NOT NULL,
    [GradeId] uniqueidentifier NOT NULL,
    [MinPassMark] decimal(5,2) NOT NULL,
    [MaxMark] decimal(5,2) NOT NULL,
    CONSTRAINT [PK_SubjectGrades] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SubjectGrades_Grades_GradeId] FOREIGN KEY ([GradeId]) REFERENCES [Grades] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SubjectGrades_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subjects] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AssignedSubjects] (
    [Id] uniqueidentifier NOT NULL,
    [TeacherId] uniqueidentifier NOT NULL,
    [SubjectId] uniqueidentifier NOT NULL,
    [SectionId] uniqueidentifier NOT NULL,
    [AssignedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_AssignedSubjects] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AssignedSubjects_Sections_SectionId] FOREIGN KEY ([SectionId]) REFERENCES [Sections] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AssignedSubjects_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subjects] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AssignedSubjects_Teachers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teachers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Student] (
    [Id] uniqueidentifier NOT NULL,
    [RegisterNo] nvarchar(50) NOT NULL,
    [NameEn] nvarchar(100) NOT NULL,
    [NameAr] nvarchar(100) NOT NULL,
    [BirthDate] datetime2 NOT NULL,
    [ProfileImage] nvarchar(max) NULL,
    [Gender] int NOT NULL,
    [Nationality] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [Address] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [UserId] uniqueidentifier NULL,
    [SectionId] uniqueidentifier NOT NULL,
    [CreatedTime] datetime2 NOT NULL,
    [SchoolId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Student] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Student_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Student_Sections_SectionId] FOREIGN KEY ([SectionId]) REFERENCES [Sections] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ParentStudent] (
    [ParentId] uniqueidentifier NOT NULL,
    [StudentId] uniqueidentifier NOT NULL,
    [RelationType] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_ParentStudent] PRIMARY KEY ([ParentId], [StudentId]),
    CONSTRAINT [FK_ParentStudent_Parent_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [Parent] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ParentStudent_Student_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Student] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_AcademicYears_StageId] ON [AcademicYears] ([StageId]);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId1] ON [AspNetUserRoles] ([RoleId1]);
GO

CREATE INDEX [IX_AspNetUserRoles_UserId1] ON [AspNetUserRoles] ([UserId1]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_AssignedSubjects_SectionId] ON [AssignedSubjects] ([SectionId]);
GO

CREATE INDEX [IX_AssignedSubjects_SubjectId] ON [AssignedSubjects] ([SubjectId]);
GO

CREATE INDEX [IX_AssignedSubjects_TeacherId] ON [AssignedSubjects] ([TeacherId]);
GO

CREATE INDEX [IX_Grades_TermId] ON [Grades] ([TermId]);
GO

CREATE INDEX [IX_NewsPhotos_SchoolNewsId] ON [NewsPhotos] ([SchoolNewsId]);
GO

CREATE UNIQUE INDEX [IX_Parent_UserId] ON [Parent] ([UserId]) WHERE [UserId] IS NOT NULL;
GO

CREATE INDEX [IX_ParentStudent_StudentId] ON [ParentStudent] ([StudentId]);
GO

CREATE INDEX [IX_Regions_CityId] ON [Regions] ([CityId]);
GO

CREATE INDEX [IX_SchoolNews_SchoolId] ON [SchoolNews] ([SchoolId]);
GO

CREATE INDEX [IX_schoolPhones_SchoolId] ON [schoolPhones] ([SchoolId]);
GO

CREATE INDEX [IX_SchoolPhotos_SchoolId] ON [SchoolPhotos] ([SchoolId]);
GO

CREATE INDEX [IX_SchoolRatings_SchoolId] ON [SchoolRatings] ([SchoolId]);
GO

CREATE INDEX [IX_Schools_CityId] ON [Schools] ([CityId]);
GO

CREATE INDEX [IX_Schools_RegionId] ON [Schools] ([RegionId]);
GO

CREATE INDEX [IX_Sections_GradeId] ON [Sections] ([GradeId]);
GO

CREATE INDEX [IX_Stage_SchoolId] ON [Stage] ([SchoolId]);
GO

CREATE INDEX [IX_Student_SectionId] ON [Student] ([SectionId]);
GO

CREATE UNIQUE INDEX [IX_Student_UserId] ON [Student] ([UserId]) WHERE [UserId] IS NOT NULL;
GO

CREATE INDEX [IX_SubjectGrades_GradeId] ON [SubjectGrades] ([GradeId]);
GO

CREATE INDEX [IX_SubjectGrades_SubjectId] ON [SubjectGrades] ([SubjectId]);
GO

CREATE INDEX [IX_Subjects_SchoolId] ON [Subjects] ([SchoolId]);
GO

CREATE INDEX [IX_Teachers_SchoolId] ON [Teachers] ([SchoolId]);
GO

CREATE INDEX [IX_Teachers_UserId] ON [Teachers] ([UserId]);
GO

CREATE INDEX [IX_Terms_AcademicYearId] ON [Terms] ([AcademicYearId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250501211459_initDataBase', N'8.0.15');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AcademicYears] DROP CONSTRAINT [FK_AcademicYears_Stage_StageId];
GO

ALTER TABLE [Parent] DROP CONSTRAINT [FK_Parent_AspNetUsers_UserId];
GO

ALTER TABLE [ParentStudent] DROP CONSTRAINT [FK_ParentStudent_Parent_ParentId];
GO

ALTER TABLE [ParentStudent] DROP CONSTRAINT [FK_ParentStudent_Student_StudentId];
GO

ALTER TABLE [Stage] DROP CONSTRAINT [FK_Stage_Schools_SchoolId];
GO

ALTER TABLE [Student] DROP CONSTRAINT [FK_Student_AspNetUsers_UserId];
GO

ALTER TABLE [Student] DROP CONSTRAINT [FK_Student_Sections_SectionId];
GO

ALTER TABLE [Student] DROP CONSTRAINT [PK_Student];
GO

ALTER TABLE [Stage] DROP CONSTRAINT [PK_Stage];
GO

ALTER TABLE [Parent] DROP CONSTRAINT [PK_Parent];
GO

EXEC sp_rename N'[Student]', N'Students';
GO

EXEC sp_rename N'[Stage]', N'Stages';
GO

EXEC sp_rename N'[Parent]', N'Parents';
GO

EXEC sp_rename N'[Students].[IX_Student_UserId]', N'IX_Students_UserId', N'INDEX';
GO

EXEC sp_rename N'[Students].[IX_Student_SectionId]', N'IX_Students_SectionId', N'INDEX';
GO

EXEC sp_rename N'[Stages].[StageName]', N'Name', N'COLUMN';
GO

EXEC sp_rename N'[Stages].[IX_Stage_SchoolId]', N'IX_Stages_SchoolId', N'INDEX';
GO

EXEC sp_rename N'[Parents].[IX_Parent_UserId]', N'IX_Parents_UserId', N'INDEX';
GO

ALTER TABLE [Students] ADD CONSTRAINT [PK_Students] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Stages] ADD CONSTRAINT [PK_Stages] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Parents] ADD CONSTRAINT [PK_Parents] PRIMARY KEY ([Id]);
GO

ALTER TABLE [AcademicYears] ADD CONSTRAINT [FK_AcademicYears_Stages_StageId] FOREIGN KEY ([StageId]) REFERENCES [Stages] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [Parents] ADD CONSTRAINT [FK_Parents_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [ParentStudent] ADD CONSTRAINT [FK_ParentStudent_Parents_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [Parents] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [ParentStudent] ADD CONSTRAINT [FK_ParentStudent_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [Stages] ADD CONSTRAINT [FK_Stages_Schools_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [Schools] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [Students] ADD CONSTRAINT [FK_Students_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [Students] ADD CONSTRAINT [FK_Students_Sections_SectionId] FOREIGN KEY ([SectionId]) REFERENCES [Sections] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250502104802_editStageName', N'8.0.15');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SubjectGrades] DROP CONSTRAINT [PK_SubjectGrades];
GO

DROP INDEX [IX_SubjectGrades_SubjectId] ON [SubjectGrades];
GO

ALTER TABLE [AssignedSubjects] DROP CONSTRAINT [PK_AssignedSubjects];
GO

DROP INDEX [IX_AssignedSubjects_SubjectId] ON [AssignedSubjects];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SubjectGrades]') AND [c].[name] = N'Id');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [SubjectGrades] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [SubjectGrades] DROP COLUMN [Id];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AssignedSubjects]') AND [c].[name] = N'Id');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AssignedSubjects] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [AssignedSubjects] DROP COLUMN [Id];
GO

ALTER TABLE [SubjectGrades] ADD CONSTRAINT [PK_SubjectGrades] PRIMARY KEY ([SubjectId], [GradeId]);
GO

ALTER TABLE [AssignedSubjects] ADD CONSTRAINT [PK_AssignedSubjects] PRIMARY KEY ([SubjectId], [SectionId], [TeacherId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250505182451_editSubjectId', N'8.0.15');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUsers] ADD [FirstName] nvarchar(max) NULL;
GO

ALTER TABLE [AspNetUsers] ADD [LastName] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250622140557_updteAppuserTabel', N'8.0.15');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUsers] ADD [SchoolId] uniqueidentifier NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250717233531_addschoolIdForAppuser', N'8.0.15');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AcademicYears] DROP CONSTRAINT [FK_AcademicYears_Stages_StageId];
GO

ALTER TABLE [Grades] DROP CONSTRAINT [FK_Grades_Terms_TermId];
GO

ALTER TABLE [schoolPhones] DROP CONSTRAINT [FK_schoolPhones_Schools_SchoolId];
GO

ALTER TABLE [Sections] DROP CONSTRAINT [FK_Sections_Grades_GradeId];
GO

ALTER TABLE [Stages] DROP CONSTRAINT [FK_Stages_Schools_SchoolId];
GO

ALTER TABLE [Students] DROP CONSTRAINT [FK_Students_Sections_SectionId];
GO

ALTER TABLE [Subjects] DROP CONSTRAINT [FK_Subjects_Schools_SchoolId];
GO

DROP TABLE [AssignedSubjects];
GO

DROP TABLE [SubjectGrades];
GO

DROP INDEX [IX_Subjects_SchoolId] ON [Subjects];
GO

DROP INDEX [IX_Stages_SchoolId] ON [Stages];
GO

ALTER TABLE [schoolPhones] DROP CONSTRAINT [PK_schoolPhones];
GO

DROP INDEX [IX_Grades_TermId] ON [Grades];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Terms]') AND [c].[name] = N'CreatedAt');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Terms] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Terms] DROP COLUMN [CreatedAt];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Terms]') AND [c].[name] = N'IsActive');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Terms] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Terms] DROP COLUMN [IsActive];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Terms]') AND [c].[name] = N'UpdatedAt');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Terms] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Terms] DROP COLUMN [UpdatedAt];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Subjects]') AND [c].[name] = N'CreatedAt');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Subjects] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Subjects] DROP COLUMN [CreatedAt];
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Subjects]') AND [c].[name] = N'NameAr');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Subjects] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Subjects] DROP COLUMN [NameAr];
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Subjects]') AND [c].[name] = N'SchoolId');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Subjects] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Subjects] DROP COLUMN [SchoolId];
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Subjects]') AND [c].[name] = N'UpdatedAt');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Subjects] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Subjects] DROP COLUMN [UpdatedAt];
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Stages]') AND [c].[name] = N'SchoolId');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Stages] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Stages] DROP COLUMN [SchoolId];
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sections]') AND [c].[name] = N'CreatedAt');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Sections] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Sections] DROP COLUMN [CreatedAt];
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sections]') AND [c].[name] = N'IsActive');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Sections] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [Sections] DROP COLUMN [IsActive];
GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sections]') AND [c].[name] = N'RoomNumber');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Sections] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [Sections] DROP COLUMN [RoomNumber];
GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sections]') AND [c].[name] = N'UpdatedAt');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Sections] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [Sections] DROP COLUMN [UpdatedAt];
GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Grades]') AND [c].[name] = N'CreatedAt');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Grades] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [Grades] DROP COLUMN [CreatedAt];
GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Grades]') AND [c].[name] = N'IsActive');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Grades] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [Grades] DROP COLUMN [IsActive];
GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Grades]') AND [c].[name] = N'TermId');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Grades] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [Grades] DROP COLUMN [TermId];
GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Grades]') AND [c].[name] = N'UpdatedAt');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Grades] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [Grades] DROP COLUMN [UpdatedAt];
GO

EXEC sp_rename N'[schoolPhones]', N'SchoolPhones';
GO

EXEC sp_rename N'[Subjects].[NameEn]', N'Name', N'COLUMN';
GO

EXEC sp_rename N'[Sections].[GradeId]', N'SchoolGradeId', N'COLUMN';
GO

EXEC sp_rename N'[Sections].[IX_Sections_GradeId]', N'IX_Sections_SchoolGradeId', N'INDEX';
GO

EXEC sp_rename N'[SchoolPhones].[IX_schoolPhones_SchoolId]', N'IX_SchoolPhones_SchoolId', N'INDEX';
GO

EXEC sp_rename N'[AcademicYears].[StageId]', N'SchoolId', N'COLUMN';
GO

EXEC sp_rename N'[AcademicYears].[IsActive]', N'IsCurrentYear', N'COLUMN';
GO

EXEC sp_rename N'[AcademicYears].[IX_AcademicYears_StageId]', N'IX_AcademicYears_SchoolId', N'INDEX';
GO

ALTER TABLE [Sections] ADD [AcademicYearId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
GO

ALTER TABLE [Sections] ADD [Capacity] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [SchoolPhones] ADD CONSTRAINT [PK_SchoolPhones] PRIMARY KEY ([Id]);
GO

CREATE TABLE [StageGrade] (
    [Id] uniqueidentifier NOT NULL,
    [StageId] uniqueidentifier NOT NULL,
    [GradeId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_StageGrade] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_StageGrade_Grades_GradeId] FOREIGN KEY ([GradeId]) REFERENCES [Grades] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_StageGrade_Stages_StageId] FOREIGN KEY ([StageId]) REFERENCES [Stages] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [SchoolGrade] (
    [Id] uniqueidentifier NOT NULL,
    [SchoolId] uniqueidentifier NOT NULL,
    [StageGradeId] uniqueidentifier NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_SchoolGrade] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SchoolGrade_Schools_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [Schools] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SchoolGrade_StageGrade_StageGradeId] FOREIGN KEY ([StageGradeId]) REFERENCES [StageGrade] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [GradeSubject] (
    [Id] uniqueidentifier NOT NULL,
    [SchoolGradeId] uniqueidentifier NOT NULL,
    [SubjectId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_GradeSubject] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_GradeSubject_SchoolGrade_SchoolGradeId] FOREIGN KEY ([SchoolGradeId]) REFERENCES [SchoolGrade] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_GradeSubject_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subjects] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [SectionSubject] (
    [Id] uniqueidentifier NOT NULL,
    [SectionId] uniqueidentifier NOT NULL,
    [GradeSubjectId] uniqueidentifier NOT NULL,
    [TermId] uniqueidentifier NOT NULL,
    [TeacherId] uniqueidentifier NULL,
    CONSTRAINT [PK_SectionSubject] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SectionSubject_GradeSubject_GradeSubjectId] FOREIGN KEY ([GradeSubjectId]) REFERENCES [GradeSubject] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SectionSubject_Sections_SectionId] FOREIGN KEY ([SectionId]) REFERENCES [Sections] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SectionSubject_Teachers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teachers] ([Id]),
    CONSTRAINT [FK_SectionSubject_Terms_TermId] FOREIGN KEY ([TermId]) REFERENCES [Terms] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Sections_AcademicYearId] ON [Sections] ([AcademicYearId]);
GO

CREATE INDEX [IX_GradeSubject_SchoolGradeId] ON [GradeSubject] ([SchoolGradeId]);
GO

CREATE INDEX [IX_GradeSubject_SubjectId] ON [GradeSubject] ([SubjectId]);
GO

CREATE INDEX [IX_SchoolGrade_SchoolId] ON [SchoolGrade] ([SchoolId]);
GO

CREATE INDEX [IX_SchoolGrade_StageGradeId] ON [SchoolGrade] ([StageGradeId]);
GO

CREATE INDEX [IX_SectionSubject_GradeSubjectId] ON [SectionSubject] ([GradeSubjectId]);
GO

CREATE INDEX [IX_SectionSubject_SectionId] ON [SectionSubject] ([SectionId]);
GO

CREATE INDEX [IX_SectionSubject_TeacherId] ON [SectionSubject] ([TeacherId]);
GO

CREATE INDEX [IX_SectionSubject_TermId] ON [SectionSubject] ([TermId]);
GO

CREATE INDEX [IX_StageGrade_GradeId] ON [StageGrade] ([GradeId]);
GO

CREATE INDEX [IX_StageGrade_StageId] ON [StageGrade] ([StageId]);
GO

ALTER TABLE [AcademicYears] ADD CONSTRAINT [FK_AcademicYears_Schools_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [Schools] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [SchoolPhones] ADD CONSTRAINT [FK_SchoolPhones_Schools_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [Schools] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Sections] ADD CONSTRAINT [FK_Sections_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYears] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [Sections] ADD CONSTRAINT [FK_Sections_SchoolGrade_SchoolGradeId] FOREIGN KEY ([SchoolGradeId]) REFERENCES [SchoolGrade] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [Students] ADD CONSTRAINT [FK_Students_Sections_SectionId] FOREIGN KEY ([SectionId]) REFERENCES [Sections] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250721120636_editScoolMangmentAnlaysis', N'8.0.15');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Grades]'))
    SET IDENTITY_INSERT [Grades] ON;
INSERT INTO [Grades] ([Id], [Name])
VALUES ('11111111-1111-1111-1111-111111111111', N'الصف الأول'),
('22222222-2222-2222-2222-222222222222', N'الصف الثاني'),
('33333333-3333-3333-3333-333333333333', N'الصف الثالث'),
('44444444-4444-4444-4444-444444444444', N'الصف الرابع'),
('44444444-4444-4444-4444-444444444445', N'KG'),
('55555555-5555-5555-5555-555555555555', N'الصف الخامس'),
('66666666-6666-6666-6666-666666666666', N'الصف السادس'),
('77777777-7777-7777-7777-777777777777', N'الصف السابع'),
('88888888-8888-8888-8888-888888888888', N'الصف الثامن'),
('99999999-9999-9999-9999-999999999999', N'الصف التاسع');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Grades]'))
    SET IDENTITY_INSERT [Grades] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Stages]'))
    SET IDENTITY_INSERT [Stages] ON;
INSERT INTO [Stages] ([Id], [Name])
VALUES ('11111111-1111-1111-1111-111111111112', N'المرحلة الإبتدائية'),
('22222222-2222-2222-2222-222222222223', N'المرحلة الإعدادية'),
('33333333-3333-3333-3333-333333333334', N'المرحلة الثانوية');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Stages]'))
    SET IDENTITY_INSERT [Stages] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Subjects]'))
    SET IDENTITY_INSERT [Subjects] ON;
INSERT INTO [Subjects] ([Id], [Name])
VALUES ('10000000-0000-0000-0000-000000000001', N'القرآن الكريم'),
('10000000-0000-0000-0000-000000000002', N'التربية الإسلامية'),
('10000000-0000-0000-0000-000000000003', N'اللغة العربية'),
('10000000-0000-0000-0000-000000000004', N'الرياضيات'),
('10000000-0000-0000-0000-000000000005', N'العلوم'),
('10000000-0000-0000-0000-000000000006', N'الاجتماعيات'),
('10000000-0000-0000-0000-000000000007', N'اللغة الإنجليزية'),
('10000000-0000-0000-0000-000000000008', N'التاريخ'),
('10000000-0000-0000-0000-000000000009', N'الجغرافيا'),
('10000000-0000-0000-0000-000000000010', N'الوطنية'),
('10000000-0000-0000-0000-000000000011', N'الجبر'),
('10000000-0000-0000-0000-000000000012', N'الهندسة'),
('10000000-0000-0000-0000-000000000013', N'الكيمياء'),
('10000000-0000-0000-0000-000000000014', N'الأحياء'),
('10000000-0000-0000-0000-000000000015', N'الفيزياء'),
('10000000-0000-0000-0000-000000000016', N'الرسم'),
('10000000-0000-0000-0000-000000000017', N'الحاسوب');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Subjects]'))
    SET IDENTITY_INSERT [Subjects] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250721144517_seedSubjectGradeAndStageData', N'8.0.15');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'GradeId', N'StageId') AND [object_id] = OBJECT_ID(N'[StageGrade]'))
    SET IDENTITY_INSERT [StageGrade] ON;
INSERT INTO [StageGrade] ([Id], [GradeId], [StageId])
VALUES ('aaaa1111-0000-0000-0000-000000000002', '11111111-1111-1111-1111-111111111111', '11111111-1111-1111-1111-111111111112'),
('aaaa1111-0000-0000-0000-000000000003', '22222222-2222-2222-2222-222222222222', '11111111-1111-1111-1111-111111111112'),
('aaaa1111-0000-0000-0000-000000000004', '33333333-3333-3333-3333-333333333333', '11111111-1111-1111-1111-111111111112'),
('aaaa1111-0000-0000-0000-000000000005', '44444444-4444-4444-4444-444444444444', '11111111-1111-1111-1111-111111111112'),
('aaaa1111-0000-0000-0000-000000000006', '55555555-5555-5555-5555-555555555555', '11111111-1111-1111-1111-111111111112'),
('aaaa1111-0000-0000-0000-000000000007', '66666666-6666-6666-6666-666666666666', '11111111-1111-1111-1111-111111111112'),
('aaaa1111-0000-0000-0000-000000000008', '77777777-7777-7777-7777-777777777777', '22222222-2222-2222-2222-222222222223'),
('aaaa1111-0000-0000-0000-000000000009', '88888888-8888-8888-8888-888888888888', '22222222-2222-2222-2222-222222222223'),
('aaaa1111-0000-0000-0000-000000000010', '99999999-9999-9999-9999-999999999999', '22222222-2222-2222-2222-222222222223');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'GradeId', N'StageId') AND [object_id] = OBJECT_ID(N'[StageGrade]'))
    SET IDENTITY_INSERT [StageGrade] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Stages]'))
    SET IDENTITY_INSERT [Stages] ON;
INSERT INTO [Stages] ([Id], [Name])
VALUES ('22222222-2222-2222-2222-222222222783', N'الروضة');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Stages]'))
    SET IDENTITY_INSERT [Stages] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'GradeId', N'StageId') AND [object_id] = OBJECT_ID(N'[StageGrade]'))
    SET IDENTITY_INSERT [StageGrade] ON;
INSERT INTO [StageGrade] ([Id], [GradeId], [StageId])
VALUES ('aaaa1111-0000-0000-0000-000000000001', '44444444-4444-4444-4444-444444444445', '22222222-2222-2222-2222-222222222783');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'GradeId', N'StageId') AND [object_id] = OBJECT_ID(N'[StageGrade]'))
    SET IDENTITY_INSERT [StageGrade] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250721150141_seedStageGradData', N'8.0.15');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [ParentStudent] DROP CONSTRAINT [FK_ParentStudent_Parents_ParentId];
GO

ALTER TABLE [ParentStudent] DROP CONSTRAINT [FK_ParentStudent_Students_StudentId];
GO

ALTER TABLE [Students] DROP CONSTRAINT [FK_Students_Sections_SectionId];
GO

DROP INDEX [IX_Students_UserId] ON [Students];
GO

DROP INDEX [IX_Parents_UserId] ON [Parents];
GO

ALTER TABLE [ParentStudent] DROP CONSTRAINT [PK_ParentStudent];
GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Parents]') AND [c].[name] = N'Phone');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Parents] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [Parents] DROP COLUMN [Phone];
GO

EXEC sp_rename N'[ParentStudent]', N'ParentStudents';
GO

EXEC sp_rename N'[Students].[SectionId]', N'CurrentSectionId', N'COLUMN';
GO

EXEC sp_rename N'[Students].[BirthDate]', N'DateOfBirth', N'COLUMN';
GO

EXEC sp_rename N'[Students].[IX_Students_SectionId]', N'IX_Students_CurrentSectionId', N'INDEX';
GO

EXEC sp_rename N'[Parents].[BirthDate]', N'DateOfBirth', N'COLUMN';
GO

EXEC sp_rename N'[ParentStudents].[IX_ParentStudent_StudentId]', N'IX_ParentStudents_StudentId', N'INDEX';
GO

DECLARE @var19 sysname;
SELECT @var19 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Students]') AND [c].[name] = N'UserId');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Students] DROP CONSTRAINT [' + @var19 + '];');
UPDATE [Students] SET [UserId] = '00000000-0000-0000-0000-000000000000' WHERE [UserId] IS NULL;
ALTER TABLE [Students] ALTER COLUMN [UserId] uniqueidentifier NOT NULL;
ALTER TABLE [Students] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [UserId];
GO

DECLARE @var20 sysname;
SELECT @var20 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Students]') AND [c].[name] = N'ProfileImage');
IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [Students] DROP CONSTRAINT [' + @var20 + '];');
ALTER TABLE [Students] ALTER COLUMN [ProfileImage] nvarchar(250) NULL;
GO

DECLARE @var21 sysname;
SELECT @var21 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Students]') AND [c].[name] = N'PhoneNumber');
IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Students] DROP CONSTRAINT [' + @var21 + '];');
ALTER TABLE [Students] ALTER COLUMN [PhoneNumber] nvarchar(20) NULL;
GO

DECLARE @var22 sysname;
SELECT @var22 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Students]') AND [c].[name] = N'Nationality');
IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [Students] DROP CONSTRAINT [' + @var22 + '];');
UPDATE [Students] SET [Nationality] = N'' WHERE [Nationality] IS NULL;
ALTER TABLE [Students] ALTER COLUMN [Nationality] nvarchar(50) NOT NULL;
ALTER TABLE [Students] ADD DEFAULT N'' FOR [Nationality];
GO

DECLARE @var23 sysname;
SELECT @var23 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Students]') AND [c].[name] = N'Email');
IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [Students] DROP CONSTRAINT [' + @var23 + '];');
ALTER TABLE [Students] ALTER COLUMN [Email] nvarchar(100) NULL;
GO

DECLARE @var24 sysname;
SELECT @var24 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Students]') AND [c].[name] = N'Address');
IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [Students] DROP CONSTRAINT [' + @var24 + '];');
UPDATE [Students] SET [Address] = N'' WHERE [Address] IS NULL;
ALTER TABLE [Students] ALTER COLUMN [Address] nvarchar(250) NOT NULL;
ALTER TABLE [Students] ADD DEFAULT N'' FOR [Address];
GO

ALTER TABLE [Students] ADD [CurrentAcademicYearId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
GO

DECLARE @var25 sysname;
SELECT @var25 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Parents]') AND [c].[name] = N'UserId');
IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [Parents] DROP CONSTRAINT [' + @var25 + '];');
UPDATE [Parents] SET [UserId] = '00000000-0000-0000-0000-000000000000' WHERE [UserId] IS NULL;
ALTER TABLE [Parents] ALTER COLUMN [UserId] uniqueidentifier NOT NULL;
ALTER TABLE [Parents] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [UserId];
GO

DECLARE @var26 sysname;
SELECT @var26 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Parents]') AND [c].[name] = N'NationalId');
IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [Parents] DROP CONSTRAINT [' + @var26 + '];');
UPDATE [Parents] SET [NationalId] = N'' WHERE [NationalId] IS NULL;
ALTER TABLE [Parents] ALTER COLUMN [NationalId] nvarchar(50) NOT NULL;
ALTER TABLE [Parents] ADD DEFAULT N'' FOR [NationalId];
GO

DECLARE @var27 sysname;
SELECT @var27 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Parents]') AND [c].[name] = N'NameEn');
IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [Parents] DROP CONSTRAINT [' + @var27 + '];');
UPDATE [Parents] SET [NameEn] = N'' WHERE [NameEn] IS NULL;
ALTER TABLE [Parents] ALTER COLUMN [NameEn] nvarchar(100) NOT NULL;
ALTER TABLE [Parents] ADD DEFAULT N'' FOR [NameEn];
GO

DECLARE @var28 sysname;
SELECT @var28 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Parents]') AND [c].[name] = N'JobTitle');
IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [Parents] DROP CONSTRAINT [' + @var28 + '];');
ALTER TABLE [Parents] ALTER COLUMN [JobTitle] nvarchar(100) NULL;
GO

DECLARE @var29 sysname;
SELECT @var29 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Parents]') AND [c].[name] = N'Address');
IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [Parents] DROP CONSTRAINT [' + @var29 + '];');
UPDATE [Parents] SET [Address] = N'' WHERE [Address] IS NULL;
ALTER TABLE [Parents] ALTER COLUMN [Address] nvarchar(250) NOT NULL;
ALTER TABLE [Parents] ADD DEFAULT N'' FOR [Address];
GO

ALTER TABLE [Parents] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Parents] ADD [PhoneNumber] nvarchar(20) NOT NULL DEFAULT N'';
GO

DECLARE @var30 sysname;
SELECT @var30 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'LastName');
IF @var30 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var30 + '];');
ALTER TABLE [AspNetUsers] ALTER COLUMN [LastName] nvarchar(100) NULL;
GO

DECLARE @var31 sysname;
SELECT @var31 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'FirstName');
IF @var31 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var31 + '];');
ALTER TABLE [AspNetUsers] ALTER COLUMN [FirstName] nvarchar(100) NULL;
GO

ALTER TABLE [AspNetUsers] ADD [EntityId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
GO

ALTER TABLE [AspNetUsers] ADD [ParentEntityId] uniqueidentifier NULL;
GO

ALTER TABLE [AspNetUsers] ADD [StudentEntityId] uniqueidentifier NULL;
GO

ALTER TABLE [AspNetUsers] ADD [UserType] nvarchar(50) NOT NULL DEFAULT N'';
GO

ALTER TABLE [ParentStudents] ADD [Id] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
GO

ALTER TABLE [ParentStudents] ADD [IsPrimaryContact] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [ParentStudents] ADD CONSTRAINT [PK_ParentStudents] PRIMARY KEY ([Id]);
GO

CREATE INDEX [IX_Students_CurrentAcademicYearId] ON [Students] ([CurrentAcademicYearId]);
GO

CREATE UNIQUE INDEX [IX_Students_RegisterNo] ON [Students] ([RegisterNo]);
GO

CREATE INDEX [IX_Students_SchoolId] ON [Students] ([SchoolId]);
GO

CREATE UNIQUE INDEX [IX_Students_UserId] ON [Students] ([UserId]);
GO

CREATE UNIQUE INDEX [IX_Parents_NationalId] ON [Parents] ([NationalId]);
GO

CREATE UNIQUE INDEX [IX_Parents_UserId] ON [Parents] ([UserId]);
GO

CREATE INDEX [IX_AspNetUsers_ParentEntityId] ON [AspNetUsers] ([ParentEntityId]);
GO

CREATE INDEX [IX_AspNetUsers_StudentEntityId] ON [AspNetUsers] ([StudentEntityId]);
GO

CREATE UNIQUE INDEX [IX_ParentStudents_ParentId_StudentId] ON [ParentStudents] ([ParentId], [StudentId]);
GO

ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Parents_ParentEntityId] FOREIGN KEY ([ParentEntityId]) REFERENCES [Parents] ([Id]);
GO

ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Students_StudentEntityId] FOREIGN KEY ([StudentEntityId]) REFERENCES [Students] ([Id]);
GO

ALTER TABLE [ParentStudents] ADD CONSTRAINT [FK_ParentStudents_Parents_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [Parents] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [ParentStudents] ADD CONSTRAINT [FK_ParentStudents_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Students] ADD CONSTRAINT [FK_Students_AcademicYears_CurrentAcademicYearId] FOREIGN KEY ([CurrentAcademicYearId]) REFERENCES [AcademicYears] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [Students] ADD CONSTRAINT [FK_Students_Schools_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [Schools] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [Students] ADD CONSTRAINT [FK_Students_Sections_CurrentSectionId] FOREIGN KEY ([CurrentSectionId]) REFERENCES [Sections] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250730200958_editStudentAndParentEnityAndRelation', N'8.0.15');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_Parents_ParentEntityId];
GO

ALTER TABLE [AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_Students_StudentEntityId];
GO

DROP INDEX [IX_AspNetUsers_ParentEntityId] ON [AspNetUsers];
GO

DROP INDEX [IX_AspNetUsers_StudentEntityId] ON [AspNetUsers];
GO

DECLARE @var32 sysname;
SELECT @var32 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'ParentEntityId');
IF @var32 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var32 + '];');
ALTER TABLE [AspNetUsers] DROP COLUMN [ParentEntityId];
GO

DECLARE @var33 sysname;
SELECT @var33 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'StudentEntityId');
IF @var33 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var33 + '];');
ALTER TABLE [AspNetUsers] DROP COLUMN [StudentEntityId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250808201336_removeParentEntityIdAndStudentEntityIdFromUserTabel', N'8.0.15');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Sections] ADD [ClassTeacherId] uniqueidentifier NULL;
GO

CREATE TABLE [Attendance] (
    [Id] uniqueidentifier NOT NULL,
    [Date] datetime2 NOT NULL,
    [IsDayOff] bit NOT NULL,
    [SectionId] uniqueidentifier NOT NULL,
    [ClassTeacherId] uniqueidentifier NOT NULL,
    [AcademicYearId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Attendance] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Attendance_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYears] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Attendance_Sections_SectionId] FOREIGN KEY ([SectionId]) REFERENCES [Sections] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Attendance_Teachers_ClassTeacherId] FOREIGN KEY ([ClassTeacherId]) REFERENCES [Teachers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AttendanceDetail] (
    [Id] uniqueidentifier NOT NULL,
    [AttendanceId] uniqueidentifier NOT NULL,
    [StudentId] uniqueidentifier NOT NULL,
    [Status] int NOT NULL,
    [Notes] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_AttendanceDetail] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AttendanceDetail_Attendance_AttendanceId] FOREIGN KEY ([AttendanceId]) REFERENCES [Attendance] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AttendanceDetail_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Sections_ClassTeacherId] ON [Sections] ([ClassTeacherId]);
GO

CREATE INDEX [IX_Attendance_AcademicYearId] ON [Attendance] ([AcademicYearId]);
GO

CREATE INDEX [IX_Attendance_ClassTeacherId] ON [Attendance] ([ClassTeacherId]);
GO

CREATE INDEX [IX_Attendance_SectionId] ON [Attendance] ([SectionId]);
GO

CREATE INDEX [IX_AttendanceDetail_AttendanceId] ON [AttendanceDetail] ([AttendanceId]);
GO

CREATE INDEX [IX_AttendanceDetail_StudentId] ON [AttendanceDetail] ([StudentId]);
GO

ALTER TABLE [Sections] ADD CONSTRAINT [FK_Sections_Teachers_ClassTeacherId] FOREIGN KEY ([ClassTeacherId]) REFERENCES [Teachers] ([Id]) ON DELETE SET NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250814064905_Attendance', N'8.0.15');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Attendance] DROP CONSTRAINT [FK_Attendance_AcademicYears_AcademicYearId];
GO

ALTER TABLE [Attendance] DROP CONSTRAINT [FK_Attendance_Sections_SectionId];
GO

ALTER TABLE [Attendance] DROP CONSTRAINT [FK_Attendance_Teachers_ClassTeacherId];
GO

ALTER TABLE [AttendanceDetail] DROP CONSTRAINT [FK_AttendanceDetail_Attendance_AttendanceId];
GO

ALTER TABLE [AttendanceDetail] DROP CONSTRAINT [FK_AttendanceDetail_Students_StudentId];
GO

ALTER TABLE [AttendanceDetail] DROP CONSTRAINT [PK_AttendanceDetail];
GO

ALTER TABLE [Attendance] DROP CONSTRAINT [PK_Attendance];
GO

EXEC sp_rename N'[AttendanceDetail]', N'AttendanceDetails';
GO

EXEC sp_rename N'[Attendance]', N'Attendances';
GO

EXEC sp_rename N'[AttendanceDetails].[IX_AttendanceDetail_StudentId]', N'IX_AttendanceDetails_StudentId', N'INDEX';
GO

EXEC sp_rename N'[AttendanceDetails].[IX_AttendanceDetail_AttendanceId]', N'IX_AttendanceDetails_AttendanceId', N'INDEX';
GO

EXEC sp_rename N'[Attendances].[IX_Attendance_SectionId]', N'IX_Attendances_SectionId', N'INDEX';
GO

EXEC sp_rename N'[Attendances].[IX_Attendance_ClassTeacherId]', N'IX_Attendances_ClassTeacherId', N'INDEX';
GO

EXEC sp_rename N'[Attendances].[IX_Attendance_AcademicYearId]', N'IX_Attendances_AcademicYearId', N'INDEX';
GO

ALTER TABLE [AttendanceDetails] ADD CONSTRAINT [PK_AttendanceDetails] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Attendances] ADD CONSTRAINT [PK_Attendances] PRIMARY KEY ([Id]);
GO

ALTER TABLE [AttendanceDetails] ADD CONSTRAINT [FK_AttendanceDetails_Attendances_AttendanceId] FOREIGN KEY ([AttendanceId]) REFERENCES [Attendances] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [AttendanceDetails] ADD CONSTRAINT [FK_AttendanceDetails_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [Attendances] ADD CONSTRAINT [FK_Attendances_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYears] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Attendances] ADD CONSTRAINT [FK_Attendances_Sections_SectionId] FOREIGN KEY ([SectionId]) REFERENCES [Sections] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Attendances] ADD CONSTRAINT [FK_Attendances_Teachers_ClassTeacherId] FOREIGN KEY ([ClassTeacherId]) REFERENCES [Teachers] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250814094617_editNameOfAttences', N'8.0.15');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Marks] (
    [Id] uniqueidentifier NOT NULL,
    [StudentId] uniqueidentifier NOT NULL,
    [SectionSubjectId] uniqueidentifier NOT NULL,
    [Score] float NOT NULL,
    [MaxScore] float NOT NULL,
    [AssessmentType] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Marks] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Marks_SectionSubject_SectionSubjectId] FOREIGN KEY ([SectionSubjectId]) REFERENCES [SectionSubject] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Marks_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Marks_SectionSubjectId] ON [Marks] ([SectionSubjectId]);
GO

CREATE INDEX [IX_Marks_StudentId] ON [Marks] ([StudentId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250815155152_addMarksTabel', N'8.0.15');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Messages] (
    [Id] uniqueidentifier NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    [DateRead] datetime2 NULL,
    [MessageSent] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
    [SenderDeleted] bit NOT NULL,
    [RecipientDeleted] bit NOT NULL,
    [SenderId] uniqueidentifier NOT NULL,
    [RecipientId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Messages_AspNetUsers_RecipientId] FOREIGN KEY ([RecipientId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Messages_AspNetUsers_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Messages_MessageSent] ON [Messages] ([MessageSent]);
GO

CREATE INDEX [IX_Messages_RecipientId] ON [Messages] ([RecipientId]);
GO

CREATE INDEX [IX_Messages_SenderId] ON [Messages] ([SenderId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250826102045_Messages', N'8.0.15');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var34 sysname;
SELECT @var34 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'FirstName');
IF @var34 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var34 + '];');
ALTER TABLE [AspNetUsers] DROP COLUMN [FirstName];
GO

EXEC sp_rename N'[AspNetUsers].[LastName]', N'Name', N'COLUMN';
GO

ALTER TABLE [AspNetUsers] ADD [ImageUrl] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250826142041_appuserEditNAme', N'8.0.15');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [SchoolRatings];
GO

CREATE TABLE [schoolReviews] (
    [Id] uniqueidentifier NOT NULL,
    [SchoolId] uniqueidentifier NOT NULL,
    [UserId] nvarchar(max) NOT NULL,
    [UserId1] uniqueidentifier NOT NULL,
    [Rating] int NOT NULL,
    [Comment] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    CONSTRAINT [PK_schoolReviews] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_schoolReviews_AspNetUsers_UserId1] FOREIGN KEY ([UserId1]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_schoolReviews_Schools_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [Schools] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_schoolReviews_SchoolId] ON [schoolReviews] ([SchoolId]);
GO

CREATE INDEX [IX_schoolReviews_UserId1] ON [schoolReviews] ([UserId1]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250831180907_SchoolReviews', N'8.0.15');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [schoolReviews] DROP CONSTRAINT [FK_schoolReviews_AspNetUsers_UserId1];
GO

DROP INDEX [IX_schoolReviews_UserId1] ON [schoolReviews];
GO

DECLARE @var35 sysname;
SELECT @var35 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[schoolReviews]') AND [c].[name] = N'UserId1');
IF @var35 IS NOT NULL EXEC(N'ALTER TABLE [schoolReviews] DROP CONSTRAINT [' + @var35 + '];');
ALTER TABLE [schoolReviews] DROP COLUMN [UserId1];
GO

DECLARE @var36 sysname;
SELECT @var36 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[schoolReviews]') AND [c].[name] = N'UserId');
IF @var36 IS NOT NULL EXEC(N'ALTER TABLE [schoolReviews] DROP CONSTRAINT [' + @var36 + '];');
ALTER TABLE [schoolReviews] ALTER COLUMN [UserId] uniqueidentifier NOT NULL;
GO

CREATE INDEX [IX_schoolReviews_UserId] ON [schoolReviews] ([UserId]);
GO

ALTER TABLE [schoolReviews] ADD CONSTRAINT [FK_schoolReviews_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250831182140_SchoolReviewsEditStringToGuid', N'8.0.15');
GO

COMMIT;
GO

