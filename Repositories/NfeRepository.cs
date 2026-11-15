using SistemaDeNFe.Patterns.Factory;

namespace SistemaDeNFe.Repositories;

/// <summary>Repositório em memória para armazenar as notas fiscais emitidas.</summary>
public class NfeRepository
{
    private readonly List<NotaFiscal> _notas = new();

    public void Salvar(NotaFiscal nota) => _notas.Add(nota);

    public IReadOnlyList<NotaFiscal> ListarTodas() => _notas.AsReadOnly();

    /// <summary>Busca uma nota pelo número.</summary>
    public NotaFiscal? BuscarPorNumero(int numero) =>
        _notas.FirstOrDefault(n => n.Numero == numero);
}
