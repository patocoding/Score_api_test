using System.Data.Common;
using Teste.ScoreAPI.Domain.Entities;

namespace Teste.ScoreAPI.Infrastructure.Database;

internal static class CustomerMapper
{
    internal static Customer Map(DbDataReader reader)
    {
        var idOrdinal = reader.GetOrdinal("id"); // getordinal pega o indice da coluna, nesse caso vai ser 0 pq id é a primeira coluna
        var nomeOrdinal = reader.GetOrdinal("nome"); // ñ precisa ta em ordem
        var emailOrdinal = reader.GetOrdinal("email");
        var birthDateOrdinal = reader.GetOrdinal("data_nascimento");
        var dddOrdinal = reader.GetOrdinal("telefone_ddd");
        var numeroOrdinal = reader.GetOrdinal("telefone_numero");
        var cpfOrdinal = reader.GetOrdinal("cpf");
        var logradouroOrdinal = reader.GetOrdinal("logradouro");
        var enderecoNumeroOrdinal = reader.GetOrdinal("endereco_numero");
        var complementoOrdinal = reader.GetOrdinal("complemento");
        var cepOrdinal = reader.GetOrdinal("cep");
        var ufOrdinal = reader.GetOrdinal("uf");
        var rendaAnualOrdinal = reader.GetOrdinal("renda_anual");

        return new Customer(
            reader.GetGuid(idOrdinal),// aqui ele faz getguid 0 - pq id é a primeira coluna - getordinal pega o indice da coluna
            reader.IsDBNull(nomeOrdinal) ? null : reader.GetString(nomeOrdinal), // lembrar de usar o is null p verificar nulo se a col for nullabe
            reader.IsDBNull(emailOrdinal) ? null : reader.GetString(emailOrdinal),
            DateOnly.FromDateTime(reader.GetDateTime(birthDateOrdinal)),
            new Phone(
                reader.GetString(dddOrdinal), // usa objt phone - customer.phone.xxxxxx
                reader.GetString(numeroOrdinal)),
            reader.GetString(cpfOrdinal), 
            new Address( // msm com address - customer.address.xxxxxx
                reader.IsDBNull(logradouroOrdinal) ? null : reader.GetString(logradouroOrdinal),
                reader.IsDBNull(enderecoNumeroOrdinal) ? null : reader.GetString(enderecoNumeroOrdinal),
                reader.IsDBNull(complementoOrdinal) ? null : reader.GetString(complementoOrdinal),
                reader.IsDBNull(cepOrdinal) ? null : reader.GetString(cepOrdinal),
                reader.GetString(ufOrdinal)),
            reader.GetDecimal(rendaAnualOrdinal));
    }
}
