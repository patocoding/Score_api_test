using System.Data.Common;
using Microsoft.Data.SqlClient;
using Teste.ScoreAPI.Application.Exceptions;
using Teste.ScoreAPI.Domain.Entities;
using Teste.ScoreAPI.Domain.Interfaces;
using Teste.ScoreAPI.Infrastructure.Database;

namespace Teste.ScoreAPI.Infrastructure.Repositories;

public sealed class SqlCustomerRepository : ICustomerRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public SqlCustomerRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> ExistsByCpfAsync(string cpf, CancellationToken cancellationToken = default)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = CreateCommand(connection, CustomerQueries.ExistsByCpf);
        AddParameter(command, "@cpf", cpf);

        var result = await command.ExecuteScalarAsync(cancellationToken);
        return result is not null;
    }

    public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = CreateCommand(connection, CustomerQueries.Insert);
        AddCustomerParameters(command, customer);

        try
        {
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
        catch (SqlException ex) when (IsUniqueConstraintViolation(ex))
        {
            throw new ConflictException("CPF já cadastrado.");
        }
    }

    public async Task<bool> UpdateByCpfAsync(string cpf, Customer customer, CancellationToken cancellationToken = default)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = CreateCommand(connection, CustomerQueries.UpdateByCpf);
        AddCustomerParameters(command, customer);
        AddParameter(command, "@cpf", cpf);

        var rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);
        return rowsAffected > 0;
    }

    public async Task<Customer?> GetByCpfAsync(string cpf, CancellationToken cancellationToken = default)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = CreateCommand(connection, CustomerQueries.SelectByCpf);
        AddParameter(command, "@cpf", cpf);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        return await reader.ReadAsync(cancellationToken)
            ? CustomerMapper.Map(reader)
            : null;
    }

    public async Task<IReadOnlyCollection<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = CreateCommand(connection, CustomerQueries.SelectAll);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        var customers = new List<Customer>();
        while (await reader.ReadAsync(cancellationToken))
        {
            customers.Add(CustomerMapper.Map(reader));
        }

        return customers;
    }

    private static DbCommand CreateCommand(DbConnection connection, string sql)
    {
        var command = connection.CreateCommand();
        command.CommandText = sql;
        return command;
    }

    private static void AddCustomerParameters(DbCommand command, Customer customer)
    {
        AddParameter(command, "@id", customer.Id);
        AddParameter(command, "@nome", customer.Name);
        AddParameter(command, "@email", customer.Email);
        AddParameter(command, "@dataNascimento", customer.BirthDate.ToDateTime(TimeOnly.MinValue));
        AddParameter(command, "@ddd", customer.Phone.Ddd);
        AddParameter(command, "@numero", customer.Phone.Number);
        AddParameter(command, "@cpf", customer.Cpf);
        AddParameter(command, "@logradouro", customer.Address.Street);
        AddParameter(command, "@enderecoNumero", customer.Address.Number);
        AddParameter(command, "@complemento", customer.Address.Complement);
        AddParameter(command, "@cep", customer.Address.ZipCode);
        AddParameter(command, "@uf", customer.Address.State);
        AddParameter(command, "@rendaAnual", customer.AnnualIncome);
    }

    private static void AddParameter(DbCommand command, string name, object? value)
    {
        var parameter = command.CreateParameter();
        parameter.ParameterName = name;
        parameter.Value = value ?? DBNull.Value;
        command.Parameters.Add(parameter);
    }

    private static bool IsUniqueConstraintViolation(SqlException exception)
    {
        return exception.Number is 2601 or 2627;
    }
}
