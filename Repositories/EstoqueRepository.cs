using SistemaDeNFe.Models;

namespace SistemaDeNFe.Repositories;

/// <summary>
/// Mantém o estoque fixo de 4 produtos da loja.
/// Os preços são: Produto 1 = R$20, Produto 2 = R$30,
///                Produto 3 = R$40, Produto 4 = R$50.
/// </summary>
public static class EstoqueRepository
{
    public static readonly List<Produto> Produtos = new()
    {
        new Produto(1, "Produto 1", 20.00m),
        new Produto(2, "Produto 2", 30.00m),
        new Produto(3, "Produto 3", 40.00m),
        new Produto(4, "Produto 4", 50.00m),
    };

    /// <summary>Retorna um produto pelo ID, ou null se não existir.</summary>
    public static Produto? BuscarPorId(int id) =>
        Produtos.FirstOrDefault(p => p.Id == id);
}
