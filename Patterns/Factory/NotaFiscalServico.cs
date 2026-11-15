namespace SistemaDeNFe.Patterns.Factory;

/// <summary>
/// ConcreteProduct do Factory Method.
/// Nota fiscal de serviço — contém apenas o valor do serviço prestado.
/// </summary>
public class NotaFiscalServico : NotaFiscal
{
    public string DescricaoServico { get; set; } = "Serviço prestado";

    /// <summary>Define o valor do serviço e atualiza o total da nota.</summary>
    public void DefinirValorServico(decimal valor, string descricao = "Serviço prestado")
    {
        Total             = valor;
        DescricaoServico  = descricao;
    }

    /// <summary>
    /// Implementação do método abstrato: exibe a descrição e o valor do serviço.
    /// </summary>
    public override void ExibirCorpo()
    {
        Console.WriteLine("[ SERVIÇO ]");
        Console.WriteLine($"  Descrição: {DescricaoServico}");
        Console.WriteLine($"  Valor do serviço prestado: R$ {Total:F2}");
    }
}
