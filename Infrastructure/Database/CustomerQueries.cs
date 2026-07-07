namespace Teste.ScoreAPI.Infrastructure.Database;

internal static class CustomerQueries
{
    private const string SelectColumns = """
        id,
        nome,
        email,
        data_nascimento,
        telefone_ddd,
        telefone_numero,
        cpf,
        logradouro,
        endereco_numero,
        complemento,
        cep,
        uf,
        renda_anual
        """;

    internal const string ExistsByCpf = """
        SELECT 1
        FROM clientes
        WHERE cpf = @cpf
        """;

    internal const string SelectByCpf = $"""
        SELECT {SelectColumns}
        FROM clientes
        WHERE cpf = @cpf
        """;

    internal const string SelectAll = $"""
        SELECT {SelectColumns}
        FROM clientes
        ORDER BY nome
        """;

    internal const string Insert = """
        INSERT INTO clientes (
            id,
            nome,
            email,
            data_nascimento,
            telefone_ddd,
            telefone_numero,
            cpf,
            logradouro,
            endereco_numero,
            complemento,
            cep,
            uf,
            renda_anual
        )
        VALUES (
            @id,
            @nome,
            @email,
            @dataNascimento,
            @ddd,
            @numero,
            @cpf,
            @logradouro,
            @enderecoNumero,
            @complemento,
            @cep,
            @uf,
            @rendaAnual
        )
        """;

    internal const string UpdateByCpf = """
        UPDATE clientes
        SET
            nome = @nome,
            email = @email,
            data_nascimento = @dataNascimento,
            telefone_ddd = @ddd,
            telefone_numero = @numero,
            logradouro = @logradouro,
            endereco_numero = @enderecoNumero,
            complemento = @complemento,
            cep = @cep,
            uf = @uf,
            renda_anual = @rendaAnual,
            atualizado_em = SYSUTCDATETIME()
        WHERE cpf = @cpf
        """;
}
