CREATE DATABASE ScoreAPI;
GO

USE ScoreAPI;
GO

CREATE TABLE dbo.clientes (
        id              UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
        nome            NVARCHAR(200) NULL,
        email           NVARCHAR(200) NULL,
        data_nascimento DATE NOT NULL,
        telefone_ddd    VARCHAR(3) NOT NULL,
        telefone_numero VARCHAR(20) NOT NULL,
        cpf             CHAR(11) NOT NULL,
        logradouro      NVARCHAR(200) NULL,
        endereco_numero NVARCHAR(20) NULL,
        complemento     NVARCHAR(100) NULL,
        cep             VARCHAR(8) NULL,
        uf              CHAR(2) NOT NULL,
        renda_anual     DECIMAL(18, 2) NOT NULL,
        criado_em       DATETIME2 NOT NULL CONSTRAINT DF_clientes_criado_em DEFAULT SYSUTCDATETIME(),
        atualizado_em   DATETIME2 NOT NULL CONSTRAINT DF_clientes_atualizado_em DEFAULT SYSUTCDATETIME()
    );


    CREATE UNIQUE INDEX ux_clientes_cpf
        ON dbo.clientes (cpf);