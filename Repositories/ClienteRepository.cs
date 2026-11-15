using SistemaDeNFe.Models;

namespace SistemaDeNFe.Repositories;

/// <summary>Repositório em memória para gerenciar clientes cadastrados.</summary>
public class ClienteRepository
{
    private readonly List<Cliente> _clientes = new();

    public void Adicionar(Cliente cliente) => _clientes.Add(cliente);

    public IReadOnlyList<Cliente> ListarTodos() => _clientes.AsReadOnly();

    /// <summary>Remove pelo CPF. Retorna true se encontrou e removeu.</summary>
    public bool Remover(string cpf)
    {
        var cliente = _clientes.FirstOrDefault(c => c.Cpf == cpf);
        if (cliente is null) return false;
        _clientes.Remove(cliente);
        return true;
    }

    /// <summary>Busca um cliente pelo CPF.</summary>
    public Cliente? BuscarPorCpf(string cpf) =>
        _clientes.FirstOrDefault(c => c.Cpf == cpf);
}
