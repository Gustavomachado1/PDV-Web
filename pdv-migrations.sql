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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE TABLE [AssinaturasHistorico] (
        [Id] uniqueidentifier NOT NULL,
        [EmpresaId] uniqueidentifier NOT NULL,
        [AssinaturaId] uniqueidentifier NOT NULL,
        [Descricao] nvarchar(500) NOT NULL,
        [ValoresAnteriores] nvarchar(max) NULL,
        [ValoresPosteriores] nvarchar(max) NULL,
        [AlteradoPorUsuarioId] uniqueidentifier NULL,
        [AlteradoEmUtc] datetime2 NOT NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_AssinaturasHistorico] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE TABLE [Auditoria] (
        [Id] uniqueidentifier NOT NULL,
        [EmpresaId] uniqueidentifier NOT NULL,
        [FilialId] uniqueidentifier NULL,
        [UsuarioId] uniqueidentifier NULL,
        [AutorizadorId] uniqueidentifier NULL,
        [Operacao] nvarchar(100) NOT NULL,
        [Entidade] nvarchar(100) NOT NULL,
        [EntidadeId] nvarchar(100) NOT NULL,
        [ValoresAnteriores] nvarchar(max) NULL,
        [ValoresPosteriores] nvarchar(max) NULL,
        [Justificativa] nvarchar(500) NULL,
        [OcorridoEmUtc] datetime2 NOT NULL,
        [RecebidoEmUtc] datetime2 NOT NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_Auditoria] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE TABLE [Autorizacoes] (
        [Id] uniqueidentifier NOT NULL,
        [EmpresaId] uniqueidentifier NOT NULL,
        [FilialId] uniqueidentifier NULL,
        [SolicitanteId] uniqueidentifier NOT NULL,
        [AutorizadorId] uniqueidentifier NULL,
        [Operacao] nvarchar(100) NOT NULL,
        [Referencia] nvarchar(100) NULL,
        [Justificativa] nvarchar(500) NOT NULL,
        [Resultado] int NOT NULL,
        [SolicitadaEmUtc] datetime2 NOT NULL,
        [DecididaEmUtc] datetime2 NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_Autorizacoes] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE TABLE [Empresas] (
        [Id] uniqueidentifier NOT NULL,
        [Codigo] nvarchar(30) NOT NULL,
        [RazaoSocial] nvarchar(200) NOT NULL,
        [NomeFantasia] nvarchar(200) NULL,
        [Cnpj] nvarchar(14) NOT NULL,
        [Ativa] bit NOT NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_Empresas] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE TABLE [LogsAutenticacao] (
        [Id] uniqueidentifier NOT NULL,
        [EmpresaId] uniqueidentifier NOT NULL,
        [UsuarioId] uniqueidentifier NULL,
        [Login] nvarchar(100) NOT NULL,
        [Sucesso] bit NOT NULL,
        [MotivoFalha] nvarchar(200) NULL,
        [Ip] nvarchar(64) NULL,
        [OcorridoEmUtc] datetime2 NOT NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_LogsAutenticacao] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE TABLE [Perfis] (
        [Id] uniqueidentifier NOT NULL,
        [EmpresaId] uniqueidentifier NOT NULL,
        [Nome] nvarchar(100) NOT NULL,
        [Tipo] int NOT NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_Perfis] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE TABLE [Permissoes] (
        [Id] uniqueidentifier NOT NULL,
        [Codigo] nvarchar(100) NOT NULL,
        [Descricao] nvarchar(300) NOT NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_Permissoes] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE TABLE [Planos] (
        [Id] uniqueidentifier NOT NULL,
        [Nome] nvarchar(100) NOT NULL,
        [MaxFiliais] int NOT NULL,
        [MaxCaixas] int NOT NULL,
        [MaxUsuarios] int NOT NULL,
        [Ativo] bit NOT NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_Planos] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE TABLE [Sessoes] (
        [Id] uniqueidentifier NOT NULL,
        [EmpresaId] uniqueidentifier NOT NULL,
        [UsuarioId] uniqueidentifier NOT NULL,
        [FilialId] uniqueidentifier NULL,
        [TokenHash] nvarchar(200) NOT NULL,
        [Dispositivo] nvarchar(200) NULL,
        [ExpiraEmUtc] datetime2 NOT NULL,
        [EncerradaEmUtc] datetime2 NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_Sessoes] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE TABLE [UsuariosFiliais] (
        [Id] uniqueidentifier NOT NULL,
        [EmpresaId] uniqueidentifier NOT NULL,
        [UsuarioId] uniqueidentifier NOT NULL,
        [FilialId] uniqueidentifier NOT NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_UsuariosFiliais] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE TABLE [UsuariosPermissoes] (
        [Id] uniqueidentifier NOT NULL,
        [EmpresaId] uniqueidentifier NOT NULL,
        [UsuarioId] uniqueidentifier NOT NULL,
        [PermissaoId] uniqueidentifier NOT NULL,
        [Concedida] bit NOT NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_UsuariosPermissoes] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE TABLE [Filiais] (
        [Id] uniqueidentifier NOT NULL,
        [EmpresaId] uniqueidentifier NOT NULL,
        [Codigo] nvarchar(30) NOT NULL,
        [Nome] nvarchar(200) NOT NULL,
        [Ativa] bit NOT NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_Filiais] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Filiais_Empresas_EmpresaId] FOREIGN KEY ([EmpresaId]) REFERENCES [Empresas] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE TABLE [Usuarios] (
        [Id] uniqueidentifier NOT NULL,
        [EmpresaId] uniqueidentifier NOT NULL,
        [Nome] nvarchar(200) NOT NULL,
        [Login] nvarchar(100) NOT NULL,
        [Email] nvarchar(200) NULL,
        [SenhaHash] nvarchar(500) NOT NULL,
        [PinHash] nvarchar(500) NULL,
        [Status] int NOT NULL,
        [TentativasFalhas] int NOT NULL,
        [BloqueadoAteUtc] datetime2 NULL,
        [PerfilId] uniqueidentifier NOT NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Usuarios_Perfis_PerfilId] FOREIGN KEY ([PerfilId]) REFERENCES [Perfis] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE TABLE [PerfisPermissoes] (
        [Id] uniqueidentifier NOT NULL,
        [EmpresaId] uniqueidentifier NOT NULL,
        [PerfilId] uniqueidentifier NOT NULL,
        [PermissaoId] uniqueidentifier NOT NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_PerfisPermissoes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PerfisPermissoes_Perfis_PerfilId] FOREIGN KEY ([PerfilId]) REFERENCES [Perfis] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PerfisPermissoes_Permissoes_PermissaoId] FOREIGN KEY ([PermissaoId]) REFERENCES [Permissoes] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE TABLE [Assinaturas] (
        [Id] uniqueidentifier NOT NULL,
        [EmpresaId] uniqueidentifier NOT NULL,
        [PlanoId] uniqueidentifier NOT NULL,
        [Situacao] int NOT NULL,
        [MaxFiliais] int NOT NULL,
        [MaxCaixas] int NOT NULL,
        [MaxUsuarios] int NOT NULL,
        [ValorMensal] decimal(18,2) NOT NULL,
        [CondicoesComerciais] nvarchar(1000) NULL,
        [InicioEmUtc] datetime2 NOT NULL,
        [FimEmUtc] datetime2 NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_Assinaturas] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Assinaturas_Planos_PlanoId] FOREIGN KEY ([PlanoId]) REFERENCES [Planos] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE INDEX [IX_Assinaturas_EmpresaId] ON [Assinaturas] ([EmpresaId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE INDEX [IX_Assinaturas_PlanoId] ON [Assinaturas] ([PlanoId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE INDEX [IX_AssinaturasHistorico_AssinaturaId_AlteradoEmUtc] ON [AssinaturasHistorico] ([AssinaturaId], [AlteradoEmUtc]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE INDEX [IX_Auditoria_EmpresaId_Entidade_EntidadeId] ON [Auditoria] ([EmpresaId], [Entidade], [EntidadeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE INDEX [IX_Auditoria_EmpresaId_OcorridoEmUtc] ON [Auditoria] ([EmpresaId], [OcorridoEmUtc]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE INDEX [IX_Autorizacoes_EmpresaId_SolicitadaEmUtc] ON [Autorizacoes] ([EmpresaId], [SolicitadaEmUtc]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Empresas_Cnpj] ON [Empresas] ([Cnpj]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Empresas_Codigo] ON [Empresas] ([Codigo]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Filiais_EmpresaId_Codigo] ON [Filiais] ([EmpresaId], [Codigo]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE INDEX [IX_LogsAutenticacao_EmpresaId_OcorridoEmUtc] ON [LogsAutenticacao] ([EmpresaId], [OcorridoEmUtc]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Perfis_EmpresaId_Nome] ON [Perfis] ([EmpresaId], [Nome]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE UNIQUE INDEX [IX_PerfisPermissoes_PerfilId_PermissaoId] ON [PerfisPermissoes] ([PerfilId], [PermissaoId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE INDEX [IX_PerfisPermissoes_PermissaoId] ON [PerfisPermissoes] ([PermissaoId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Permissoes_Codigo] ON [Permissoes] ([Codigo]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Planos_Nome] ON [Planos] ([Nome]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Sessoes_TokenHash] ON [Sessoes] ([TokenHash]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Usuarios_EmpresaId_Login] ON [Usuarios] ([EmpresaId], [Login]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE INDEX [IX_Usuarios_PerfilId] ON [Usuarios] ([PerfilId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE UNIQUE INDEX [IX_UsuariosFiliais_UsuarioId_FilialId] ON [UsuariosFiliais] ([UsuarioId], [FilialId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    CREATE UNIQUE INDEX [IX_UsuariosPermissoes_UsuarioId_PermissaoId] ON [UsuariosPermissoes] ([UsuarioId], [PermissaoId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261112223615_Inicial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261112223615_Inicial', N'10.0.15');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116152952_CatalogoInicial'
)
BEGIN
    CREATE TABLE [Departamentos] (
        [Id] uniqueidentifier NOT NULL,
        [EmpresaId] uniqueidentifier NOT NULL,
        [Nome] nvarchar(100) NOT NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_Departamentos] PRIMARY KEY ([Id]),
        CONSTRAINT [AK_Departamentos_EmpresaId_Id] UNIQUE ([EmpresaId], [Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116152952_CatalogoInicial'
)
BEGIN
    CREATE TABLE [Categorias] (
        [Id] uniqueidentifier NOT NULL,
        [EmpresaId] uniqueidentifier NOT NULL,
        [DepartamentoId] uniqueidentifier NOT NULL,
        [Nome] nvarchar(100) NOT NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_Categorias] PRIMARY KEY ([Id]),
        CONSTRAINT [AK_Categorias_EmpresaId_Id] UNIQUE ([EmpresaId], [Id]),
        CONSTRAINT [FK_Categorias_Departamentos_EmpresaId_DepartamentoId] FOREIGN KEY ([EmpresaId], [DepartamentoId]) REFERENCES [Departamentos] ([EmpresaId], [Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116152952_CatalogoInicial'
)
BEGIN
    CREATE TABLE [Produtos] (
        [Id] uniqueidentifier NOT NULL,
        [EmpresaId] uniqueidentifier NOT NULL,
        [CategoriaId] uniqueidentifier NOT NULL,
        [Descricao] nvarchar(200) NOT NULL,
        [UnidadeMedida] nvarchar(30) NOT NULL,
        [Status] int NOT NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_Produtos] PRIMARY KEY ([Id]),
        CONSTRAINT [AK_Produtos_EmpresaId_Id] UNIQUE ([EmpresaId], [Id]),
        CONSTRAINT [FK_Produtos_Categorias_EmpresaId_CategoriaId] FOREIGN KEY ([EmpresaId], [CategoriaId]) REFERENCES [Categorias] ([EmpresaId], [Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116152952_CatalogoInicial'
)
BEGIN
    CREATE TABLE [CodigosBarrasProdutos] (
        [Id] uniqueidentifier NOT NULL,
        [EmpresaId] uniqueidentifier NOT NULL,
        [ProdutoId] uniqueidentifier NOT NULL,
        [Codigo] nvarchar(100) NOT NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_CodigosBarrasProdutos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CodigosBarrasProdutos_Produtos_EmpresaId_ProdutoId] FOREIGN KEY ([EmpresaId], [ProdutoId]) REFERENCES [Produtos] ([EmpresaId], [Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116152952_CatalogoInicial'
)
BEGIN
    CREATE INDEX [IX_Categorias_EmpresaId_DepartamentoId_Nome] ON [Categorias] ([EmpresaId], [DepartamentoId], [Nome]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116152952_CatalogoInicial'
)
BEGIN
    CREATE UNIQUE INDEX [IX_CodigosBarrasProdutos_EmpresaId_Codigo] ON [CodigosBarrasProdutos] ([EmpresaId], [Codigo]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116152952_CatalogoInicial'
)
BEGIN
    CREATE INDEX [IX_CodigosBarrasProdutos_EmpresaId_ProdutoId] ON [CodigosBarrasProdutos] ([EmpresaId], [ProdutoId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116152952_CatalogoInicial'
)
BEGIN
    CREATE INDEX [IX_Departamentos_EmpresaId_Nome] ON [Departamentos] ([EmpresaId], [Nome]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116152952_CatalogoInicial'
)
BEGIN
    CREATE INDEX [IX_Produtos_EmpresaId_CategoriaId] ON [Produtos] ([EmpresaId], [CategoriaId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116152952_CatalogoInicial'
)
BEGIN
    CREATE INDEX [IX_Produtos_EmpresaId_Descricao] ON [Produtos] ([EmpresaId], [Descricao]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116152952_CatalogoInicial'
)
BEGIN
    IF EXISTS (SELECT 1 FROM Perfis WHERE Tipo = 1)
    BEGIN
        DECLARE @PermissaoId uniqueidentifier;
        SELECT @PermissaoId = Id
        FROM Permissoes
        WHERE Codigo = N'catalogo.gerenciar';

        IF @PermissaoId IS NULL
        BEGIN
            SET @PermissaoId = NEWID();
            INSERT INTO Permissoes (Id, Codigo, Descricao, CriadoEmUtc)
            VALUES (@PermissaoId, N'catalogo.gerenciar', N'Gerenciar catálogo de produtos', SYSUTCDATETIME());
        END;

        INSERT INTO PerfisPermissoes (Id, EmpresaId, PerfilId, PermissaoId, CriadoEmUtc)
        SELECT NEWID(), perfil.EmpresaId, perfil.Id, @PermissaoId, SYSUTCDATETIME()
        FROM Perfis AS perfil
        WHERE perfil.Tipo = 1
          AND NOT EXISTS (
            SELECT 1
            FROM PerfisPermissoes AS vinculo
            WHERE vinculo.PerfilId = perfil.Id
              AND vinculo.PermissaoId = @PermissaoId
          );
    END;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116152952_CatalogoInicial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261116152952_CatalogoInicial', N'10.0.15');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116154656_HierarquiaCatalogo'
)
BEGIN
    ALTER TABLE [Produtos] DROP CONSTRAINT [FK_Produtos_Categorias_EmpresaId_CategoriaId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116154656_HierarquiaCatalogo'
)
BEGIN
    DROP INDEX [IX_Departamentos_EmpresaId_Nome] ON [Departamentos];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116154656_HierarquiaCatalogo'
)
BEGIN
    DROP INDEX [IX_Categorias_EmpresaId_DepartamentoId_Nome] ON [Categorias];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116154656_HierarquiaCatalogo'
)
BEGIN
    EXEC sp_rename N'[Produtos].[CategoriaId]', N'SubcategoriaId', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116154656_HierarquiaCatalogo'
)
BEGIN
    EXEC sp_rename N'[Produtos].[IX_Produtos_EmpresaId_CategoriaId]', N'IX_Produtos_EmpresaId_SubcategoriaId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116154656_HierarquiaCatalogo'
)
BEGIN
    ALTER TABLE [Produtos] ADD [UsoInterno] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116154656_HierarquiaCatalogo'
)
BEGIN
    ALTER TABLE [Departamentos] ADD [Segmento] nvarchar(100) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116154656_HierarquiaCatalogo'
)
BEGIN
    CREATE TABLE [Subcategorias] (
        [Id] uniqueidentifier NOT NULL,
        [EmpresaId] uniqueidentifier NOT NULL,
        [CategoriaId] uniqueidentifier NOT NULL,
        [Nome] nvarchar(100) NOT NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_Subcategorias] PRIMARY KEY ([Id]),
        CONSTRAINT [AK_Subcategorias_EmpresaId_Id] UNIQUE ([EmpresaId], [Id]),
        CONSTRAINT [FK_Subcategorias_Categorias_EmpresaId_CategoriaId] FOREIGN KEY ([EmpresaId], [CategoriaId]) REFERENCES [Categorias] ([EmpresaId], [Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116154656_HierarquiaCatalogo'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Departamentos_EmpresaId_Nome] ON [Departamentos] ([EmpresaId], [Nome]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116154656_HierarquiaCatalogo'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Categorias_EmpresaId_DepartamentoId_Nome] ON [Categorias] ([EmpresaId], [DepartamentoId], [Nome]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116154656_HierarquiaCatalogo'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Subcategorias_EmpresaId_CategoriaId_Nome] ON [Subcategorias] ([EmpresaId], [CategoriaId], [Nome]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116154656_HierarquiaCatalogo'
)
BEGIN
    ALTER TABLE [Produtos] ADD CONSTRAINT [FK_Produtos_Subcategorias_EmpresaId_SubcategoriaId] FOREIGN KEY ([EmpresaId], [SubcategoriaId]) REFERENCES [Subcategorias] ([EmpresaId], [Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116154656_HierarquiaCatalogo'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261116154656_HierarquiaCatalogo', N'10.0.15');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116155406_HistoricoCodigosBarras'
)
BEGIN
    ALTER TABLE [CodigosBarrasProdutos] ADD [Ativo] bit NOT NULL DEFAULT CAST(1 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116155406_HistoricoCodigosBarras'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261116155406_HistoricoCodigosBarras', N'10.0.15');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116164917_EstoquePorFilial'
)
BEGIN
    DECLARE @var nvarchar(max);
    SELECT @var = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Autorizacoes]') AND [c].[name] = N'Justificativa');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [Autorizacoes] DROP CONSTRAINT ' + @var + ';');
    ALTER TABLE [Autorizacoes] ALTER COLUMN [Justificativa] nvarchar(500) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116164917_EstoquePorFilial'
)
BEGIN
    ALTER TABLE [Filiais] ADD CONSTRAINT [AK_Filiais_EmpresaId_Id] UNIQUE ([EmpresaId], [Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116164917_EstoquePorFilial'
)
BEGIN
    ALTER TABLE [Autorizacoes] ADD CONSTRAINT [AK_Autorizacoes_EmpresaId_Id] UNIQUE ([EmpresaId], [Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116164917_EstoquePorFilial'
)
BEGIN
    CREATE TABLE [InventariosEstoque] (
        [Id] uniqueidentifier NOT NULL,
        [EmpresaId] uniqueidentifier NOT NULL,
        [FilialId] uniqueidentifier NOT NULL,
        [UsuarioId] uniqueidentifier NOT NULL,
        [Tipo] int NOT NULL,
        [Justificativa] nvarchar(500) NULL,
        [ConcluidoEmUtc] datetime2 NOT NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_InventariosEstoque] PRIMARY KEY ([Id]),
        CONSTRAINT [AK_InventariosEstoque_EmpresaId_Id] UNIQUE ([EmpresaId], [Id]),
        CONSTRAINT [FK_InventariosEstoque_Filiais_EmpresaId_FilialId] FOREIGN KEY ([EmpresaId], [FilialId]) REFERENCES [Filiais] ([EmpresaId], [Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116164917_EstoquePorFilial'
)
BEGIN
    CREATE TABLE [SaldosEstoque] (
        [Id] uniqueidentifier NOT NULL,
        [EmpresaId] uniqueidentifier NOT NULL,
        [FilialId] uniqueidentifier NOT NULL,
        [ProdutoId] uniqueidentifier NOT NULL,
        [Quantidade] decimal(18,6) NOT NULL,
        [EstoqueMinimo] decimal(18,6) NULL,
        [EstoqueIdeal] decimal(18,6) NULL,
        [Disponivel] bit NOT NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_SaldosEstoque] PRIMARY KEY ([Id]),
        CONSTRAINT [AK_SaldosEstoque_EmpresaId_Id] UNIQUE ([EmpresaId], [Id]),
        CONSTRAINT [FK_SaldosEstoque_Filiais_EmpresaId_FilialId] FOREIGN KEY ([EmpresaId], [FilialId]) REFERENCES [Filiais] ([EmpresaId], [Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SaldosEstoque_Produtos_EmpresaId_ProdutoId] FOREIGN KEY ([EmpresaId], [ProdutoId]) REFERENCES [Produtos] ([EmpresaId], [Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116164917_EstoquePorFilial'
)
BEGIN
    CREATE TABLE [ItensInventarioEstoque] (
        [Id] uniqueidentifier NOT NULL,
        [EmpresaId] uniqueidentifier NOT NULL,
        [FilialId] uniqueidentifier NOT NULL,
        [InventarioEstoqueId] uniqueidentifier NOT NULL,
        [ProdutoId] uniqueidentifier NOT NULL,
        [SaldoAnterior] decimal(18,6) NOT NULL,
        [QuantidadeContada] decimal(18,6) NOT NULL,
        [Diferenca] decimal(18,6) NOT NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_ItensInventarioEstoque] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ItensInventarioEstoque_InventariosEstoque_EmpresaId_InventarioEstoqueId] FOREIGN KEY ([EmpresaId], [InventarioEstoqueId]) REFERENCES [InventariosEstoque] ([EmpresaId], [Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ItensInventarioEstoque_Produtos_EmpresaId_ProdutoId] FOREIGN KEY ([EmpresaId], [ProdutoId]) REFERENCES [Produtos] ([EmpresaId], [Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116164917_EstoquePorFilial'
)
BEGIN
    CREATE TABLE [MovimentosEstoque] (
        [Id] uniqueidentifier NOT NULL,
        [EmpresaId] uniqueidentifier NOT NULL,
        [FilialId] uniqueidentifier NOT NULL,
        [ProdutoId] uniqueidentifier NOT NULL,
        [InventarioEstoqueId] uniqueidentifier NULL,
        [AutorizacaoOperacaoId] uniqueidentifier NULL,
        [SolicitanteId] uniqueidentifier NOT NULL,
        [AutorizadorId] uniqueidentifier NULL,
        [Tipo] int NOT NULL,
        [Situacao] int NOT NULL,
        [Quantidade] decimal(18,6) NOT NULL,
        [SaldoAnterior] decimal(18,6) NULL,
        [SaldoResultante] decimal(18,6) NULL,
        [Justificativa] nvarchar(500) NULL,
        [SolicitadoEmUtc] datetime2 NOT NULL,
        [ConcluidoEmUtc] datetime2 NULL,
        [CriadoEmUtc] datetime2 NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_MovimentosEstoque] PRIMARY KEY ([Id]),
        CONSTRAINT [AK_MovimentosEstoque_EmpresaId_Id] UNIQUE ([EmpresaId], [Id]),
        CONSTRAINT [FK_MovimentosEstoque_Autorizacoes_EmpresaId_AutorizacaoOperacaoId] FOREIGN KEY ([EmpresaId], [AutorizacaoOperacaoId]) REFERENCES [Autorizacoes] ([EmpresaId], [Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_MovimentosEstoque_Filiais_EmpresaId_FilialId] FOREIGN KEY ([EmpresaId], [FilialId]) REFERENCES [Filiais] ([EmpresaId], [Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_MovimentosEstoque_InventariosEstoque_EmpresaId_InventarioEstoqueId] FOREIGN KEY ([EmpresaId], [InventarioEstoqueId]) REFERENCES [InventariosEstoque] ([EmpresaId], [Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_MovimentosEstoque_Produtos_EmpresaId_ProdutoId] FOREIGN KEY ([EmpresaId], [ProdutoId]) REFERENCES [Produtos] ([EmpresaId], [Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116164917_EstoquePorFilial'
)
BEGIN
    CREATE INDEX [IX_InventariosEstoque_EmpresaId_FilialId_ConcluidoEmUtc] ON [InventariosEstoque] ([EmpresaId], [FilialId], [ConcluidoEmUtc]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116164917_EstoquePorFilial'
)
BEGIN
    CREATE UNIQUE INDEX [IX_ItensInventarioEstoque_EmpresaId_InventarioEstoqueId_ProdutoId] ON [ItensInventarioEstoque] ([EmpresaId], [InventarioEstoqueId], [ProdutoId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116164917_EstoquePorFilial'
)
BEGIN
    CREATE INDEX [IX_ItensInventarioEstoque_EmpresaId_ProdutoId] ON [ItensInventarioEstoque] ([EmpresaId], [ProdutoId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116164917_EstoquePorFilial'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_MovimentosEstoque_AutorizacaoOperacaoId] ON [MovimentosEstoque] ([AutorizacaoOperacaoId]) WHERE [AutorizacaoOperacaoId] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116164917_EstoquePorFilial'
)
BEGIN
    CREATE INDEX [IX_MovimentosEstoque_EmpresaId_AutorizacaoOperacaoId] ON [MovimentosEstoque] ([EmpresaId], [AutorizacaoOperacaoId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116164917_EstoquePorFilial'
)
BEGIN
    CREATE INDEX [IX_MovimentosEstoque_EmpresaId_FilialId_ProdutoId_SolicitadoEmUtc] ON [MovimentosEstoque] ([EmpresaId], [FilialId], [ProdutoId], [SolicitadoEmUtc]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116164917_EstoquePorFilial'
)
BEGIN
    CREATE INDEX [IX_MovimentosEstoque_EmpresaId_InventarioEstoqueId] ON [MovimentosEstoque] ([EmpresaId], [InventarioEstoqueId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116164917_EstoquePorFilial'
)
BEGIN
    CREATE INDEX [IX_MovimentosEstoque_EmpresaId_ProdutoId] ON [MovimentosEstoque] ([EmpresaId], [ProdutoId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116164917_EstoquePorFilial'
)
BEGIN
    CREATE UNIQUE INDEX [IX_SaldosEstoque_EmpresaId_FilialId_ProdutoId] ON [SaldosEstoque] ([EmpresaId], [FilialId], [ProdutoId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116164917_EstoquePorFilial'
)
BEGIN
    CREATE INDEX [IX_SaldosEstoque_EmpresaId_ProdutoId] ON [SaldosEstoque] ([EmpresaId], [ProdutoId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116164917_EstoquePorFilial'
)
BEGIN
    IF EXISTS (SELECT 1 FROM [Perfis])
    BEGIN
        DECLARE @Permissoes TABLE (
            [Id] uniqueidentifier NOT NULL,
            [Codigo] nvarchar(100) NOT NULL,
            [Descricao] nvarchar(300) NOT NULL);

        INSERT INTO @Permissoes ([Id], [Codigo], [Descricao]) VALUES
            ('a0000000-0000-7000-8000-000000000001', N'estoque.consultar', N'Consultar estoque e movimentações'),
            ('a0000000-0000-7000-8000-000000000002', N'estoque.configurar', N'Configurar estoque por filial'),
            ('a0000000-0000-7000-8000-000000000003', N'estoque.movimentar.solicitar', N'Solicitar movimentação manual de estoque'),
            ('a0000000-0000-7000-8000-000000000004', N'estoque.movimento.entrada.autorizar', N'Autorizar entradas manuais de estoque'),
            ('a0000000-0000-7000-8000-000000000005', N'estoque.movimento.saida.autorizar', N'Autorizar saídas manuais de estoque'),
            ('a0000000-0000-7000-8000-000000000006', N'estoque.inventario.gerenciar', N'Realizar e concluir inventários');

        INSERT INTO [Permissoes] ([Id], [Codigo], [Descricao], [CriadoEmUtc])
        SELECT nova.[Id], nova.[Codigo], nova.[Descricao], SYSUTCDATETIME()
        FROM @Permissoes AS nova
        WHERE NOT EXISTS (
            SELECT 1 FROM [Permissoes] AS existente WHERE existente.[Codigo] = nova.[Codigo]);

        INSERT INTO [PerfisPermissoes] ([Id], [EmpresaId], [PerfilId], [PermissaoId], [CriadoEmUtc])
        SELECT NEWID(), perfil.[EmpresaId], perfil.[Id], permissoes.[Id], SYSUTCDATETIME()
        FROM [Perfis] AS perfil
        CROSS JOIN @Permissoes AS permissoes
        WHERE (
            perfil.[Tipo] = 1 OR
            (perfil.[Tipo] = 2 AND permissoes.[Codigo] IN (
                N'estoque.consultar', N'estoque.movimentar.solicitar',
                N'estoque.movimento.entrada.autorizar', N'estoque.movimento.saida.autorizar')) OR
            (perfil.[Tipo] = 4 AND permissoes.[Codigo] IN (
                N'estoque.consultar', N'estoque.configurar',
                N'estoque.movimentar.solicitar', N'estoque.inventario.gerenciar')))
          AND EXISTS (
            SELECT 1 FROM [Permissoes] AS existente
            WHERE existente.[Id] = permissoes.[Id] AND existente.[Codigo] = permissoes.[Codigo])
          AND NOT EXISTS (
            SELECT 1 FROM [PerfisPermissoes] AS existente
            WHERE existente.[PerfilId] = perfil.[Id]
              AND existente.[PermissaoId] = permissoes.[Id]);
    END
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116164917_EstoquePorFilial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261116164917_EstoquePorFilial', N'10.0.15');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    ALTER TABLE [InventariosEstoque] DROP CONSTRAINT [FK_InventariosEstoque_Filiais_EmpresaId_FilialId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    ALTER TABLE [ItensInventarioEstoque] DROP CONSTRAINT [FK_ItensInventarioEstoque_InventariosEstoque_EmpresaId_InventarioEstoqueId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    ALTER TABLE [MovimentosEstoque] DROP CONSTRAINT [FK_MovimentosEstoque_Filiais_EmpresaId_FilialId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    ALTER TABLE [MovimentosEstoque] DROP CONSTRAINT [FK_MovimentosEstoque_InventariosEstoque_EmpresaId_InventarioEstoqueId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    ALTER TABLE [SaldosEstoque] DROP CONSTRAINT [FK_SaldosEstoque_Filiais_EmpresaId_FilialId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    DROP INDEX [IX_MovimentosEstoque_EmpresaId_InventarioEstoqueId] ON [MovimentosEstoque];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    ALTER TABLE [Filiais] DROP CONSTRAINT [AK_Filiais_EmpresaId_Id];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    ALTER TABLE [Usuarios] ADD CONSTRAINT [AK_Usuarios_EmpresaId_Id] UNIQUE ([EmpresaId], [Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    ALTER TABLE [InventariosEstoque] ADD CONSTRAINT [AK_InventariosEstoque_EmpresaId_FilialId_Id] UNIQUE ([EmpresaId], [FilialId], [Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    CREATE INDEX [IX_SaldosEstoque_FilialId] ON [SaldosEstoque] ([FilialId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    CREATE INDEX [IX_MovimentosEstoque_EmpresaId_AutorizadorId] ON [MovimentosEstoque] ([EmpresaId], [AutorizadorId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    CREATE INDEX [IX_MovimentosEstoque_EmpresaId_FilialId_InventarioEstoqueId] ON [MovimentosEstoque] ([EmpresaId], [FilialId], [InventarioEstoqueId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    CREATE INDEX [IX_MovimentosEstoque_EmpresaId_SolicitanteId] ON [MovimentosEstoque] ([EmpresaId], [SolicitanteId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    CREATE INDEX [IX_MovimentosEstoque_FilialId] ON [MovimentosEstoque] ([FilialId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    CREATE INDEX [IX_ItensInventarioEstoque_EmpresaId_FilialId_InventarioEstoqueId] ON [ItensInventarioEstoque] ([EmpresaId], [FilialId], [InventarioEstoqueId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    CREATE INDEX [IX_InventariosEstoque_EmpresaId_UsuarioId] ON [InventariosEstoque] ([EmpresaId], [UsuarioId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    CREATE INDEX [IX_InventariosEstoque_FilialId] ON [InventariosEstoque] ([FilialId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    ALTER TABLE [InventariosEstoque] ADD CONSTRAINT [FK_InventariosEstoque_Filiais_FilialId] FOREIGN KEY ([FilialId]) REFERENCES [Filiais] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    ALTER TABLE [InventariosEstoque] ADD CONSTRAINT [FK_InventariosEstoque_Usuarios_EmpresaId_UsuarioId] FOREIGN KEY ([EmpresaId], [UsuarioId]) REFERENCES [Usuarios] ([EmpresaId], [Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    ALTER TABLE [ItensInventarioEstoque] ADD CONSTRAINT [FK_ItensInventarioEstoque_InventariosEstoque_EmpresaId_FilialId_InventarioEstoqueId] FOREIGN KEY ([EmpresaId], [FilialId], [InventarioEstoqueId]) REFERENCES [InventariosEstoque] ([EmpresaId], [FilialId], [Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    ALTER TABLE [MovimentosEstoque] ADD CONSTRAINT [FK_MovimentosEstoque_Filiais_FilialId] FOREIGN KEY ([FilialId]) REFERENCES [Filiais] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    ALTER TABLE [MovimentosEstoque] ADD CONSTRAINT [FK_MovimentosEstoque_InventariosEstoque_EmpresaId_FilialId_InventarioEstoqueId] FOREIGN KEY ([EmpresaId], [FilialId], [InventarioEstoqueId]) REFERENCES [InventariosEstoque] ([EmpresaId], [FilialId], [Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    ALTER TABLE [MovimentosEstoque] ADD CONSTRAINT [FK_MovimentosEstoque_Usuarios_EmpresaId_AutorizadorId] FOREIGN KEY ([EmpresaId], [AutorizadorId]) REFERENCES [Usuarios] ([EmpresaId], [Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    ALTER TABLE [MovimentosEstoque] ADD CONSTRAINT [FK_MovimentosEstoque_Usuarios_EmpresaId_SolicitanteId] FOREIGN KEY ([EmpresaId], [SolicitanteId]) REFERENCES [Usuarios] ([EmpresaId], [Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    ALTER TABLE [SaldosEstoque] ADD CONSTRAINT [FK_SaldosEstoque_Filiais_FilialId] FOREIGN KEY ([FilialId]) REFERENCES [Filiais] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261116165250_IntegridadeFilialEstoque'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261116165250_IntegridadeFilialEstoque', N'10.0.15');
END;

COMMIT;
GO

