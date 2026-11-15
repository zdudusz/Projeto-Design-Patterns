using SistemaDeNFe.Models;
using SistemaDeNFe.Patterns.Singleton;

namespace SistemaDeNFe.Patterns.Factory;

/// <summary>
/// PADRÃO: Factory Method
///
/// Define a estrutura (esqueleto) de uma Nota Fiscal e delega para as
/// subclasses a responsabilidade de preencher o conteúdo específico
/// (produtos ou serviço). Isso evita que o código do menu precise saber
/// como construir cada tipo de nota — basta chamar o método de fábrica.
///
/// Hierarquia:
///   NotaFiscal          ← produto do Factory Method (classe abstrata)
///   ├── NotaFiscalProduto  ← ConcreteProduct
///   └── NotaFiscalServico  ← ConcreteProduct
///
///   NotaFiscalFactory      ← Creator abstrato
///   ├── FabricaNotaProduto ← ConcreteCreator
///   └── FabricaNotaServico ← ConcreteCreator
/// </summary>
public abstract class NotaFiscal
{
    // Número sequencial gerado automaticamente
    public int     Numero       { get; } = GerarNumero();
    public DateTime DataEmissao { get; } = DateTime.Now;

    // Dados do emissor vêm do Singleton — não criamos new EmpresaEmissora()
    public EmpresaEmissora Emissor { get; } = EmpresaEmissora.Instancia;

    // Dados do destinatário
    public Cliente Cliente { get; set; } = null!;

    // Valor total da nota (calculado por cada subclasse)
    public decimal Total { get; protected set; }

    // Contador estático para numeração sequencial das notas
    private static int _contador = 1;
    private static int GerarNumero() => _contador++;

    /// <summary>
    /// Método abstrato que cada subclasse implementa para exibir
    /// os itens específicos da nota (produtos ou serviço).
    /// </summary>
    public abstract void ExibirCorpo();

    /// <summary>Exibe a nota completa no terminal.</summary>
    public void ExibirNota()
    {
        Console.WriteLine();
        Console.WriteLine(new string('=', 55));
        Console.WriteLine($"           NOTA FISCAL Nº {Numero:D6}");
        Console.WriteLine(new string('=', 55));

        Console.WriteLine("\n[ DADOS DO EMISSOR ]");
        Emissor.ExibirDados();

        Console.WriteLine("\n[ DADOS DO CLIENTE ]");
        Console.WriteLine(Cliente.ToString());
        Console.WriteLine($"  Data de Emissão: {DataEmissao:dd/MM/yyyy HH:mm}");

        Console.WriteLine();
        ExibirCorpo(); // delegado à subclasse

        Console.WriteLine(new string('-', 55));
        Console.WriteLine($"  TOTAL: R$ {Total:F2}");
        Console.WriteLine(new string('=', 55));
    }
}
