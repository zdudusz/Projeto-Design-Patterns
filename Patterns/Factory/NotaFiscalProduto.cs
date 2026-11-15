using SistemaDeNFe.Models;

namespace SistemaDeNFe.Patterns.Factory;

/// <summary>
/// ConcreteProduct do Factory Method.
/// Nota fiscal que contém uma lista de produtos com seus respectivos preços.
/// </summary>
public class NotaFiscalProduto : NotaFiscal
{
    // Lista de produtos selecionados pelo usuário para esta nota
    public List<Produto> Produtos { get; } = new();

    /// <summary>
    /// Adiciona um produto à nota e recalcula o total.
    /// </summary>
    public void AdicionarProduto(Produto produto)
    {
        Produtos.Add(produto);
        Total += produto.Preco; // acumula o subtotal
    }

    /// <summary>
    /// Implementação do método abstrato: exibe os produtos e o subtotal.
    /// </summary>
    public override void ExibirCorpo()
    {
        Console.WriteLine("[ PRODUTOS ]");
        foreach (var p in Produtos)
            Console.WriteLine($"  - {p.Nome,-30} R$ {p.Preco:F2}");

        Console.WriteLine($"\n  Subtotal: R$ {Total:F2}");
    }
}
