namespace SistemaDeNFe.Models;

/// <summary>Representa um produto disponível no estoque da loja.</summary>
public class Produto
{
    public int    Id    { get; set; }
    public string Nome  { get; set; } = string.Empty;
    public decimal Preco { get; set; }

    public Produto(int id, string nome, decimal preco)
    {
        Id    = id;
        Nome  = nome;
        Preco = preco;
    }

    public override string ToString() =>
        $"[{Id}] {Nome} - R$ {Preco:F2}";
}
