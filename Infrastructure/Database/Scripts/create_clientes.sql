CREATE TABLE clientes (
    id              UUID PRIMARY KEY,
    nome            VARCHAR(200),
    email           VARCHAR(200),
    data_nascimento DATE NOT NULL,
    telefone_ddd    VARCHAR(3) NOT NULL,
    telefone_numero VARCHAR(20) NOT NULL,
    cpf             CHAR(11) NOT NULL,
    logradouro      VARCHAR(200),
    endereco_numero VARCHAR(20),
    complemento     VARCHAR(100),
    cep             VARCHAR(8),
    uf              CHAR(2) NOT NULL,
    renda_anual     NUMERIC(18,2) NOT NULL,
    criado_em       TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    atualizado_em   TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE UNIQUE INDEX ux_clientes_cpf ON clientes (cpf);