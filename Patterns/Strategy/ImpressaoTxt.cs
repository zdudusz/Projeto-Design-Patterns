using SistemaDeNFe.Patterns.Factory;

namespace SistemaDeNFe.Patterns.Strategy;

/// <summary>
/// ConcreteStrategy A: formata e exibe a nota como texto simples (TXT).
/// Simula o que seria salvo/impresso em um arquivo .txt.
/// </summary>
public class ImpressaoTxt : IEstrategiaImpressao
{
    public void Imprimir(NotaFiscal nota)
    {
        Console.WriteLine();
        Console.WriteLine("  [IMPRESSÃO - FORMATO TXT]");
        Console.WriteLine(new string('-', 55));

        // Bloco de texto que imitaria o conteúdo de um arquivo .txt
        Console.WriteLine($"NOTA FISCAL Nº: {nota.Numero:D6}");
        Console.WriteLine($"DATA EMISSAO  : {nota.DataEmissao:dd/MM/yyyy HH:mm}");
        Console.WriteLine();
        Console.WriteLine("EMISSOR");
        Console.WriteLine($"  Razao Social : {nota.Emissor.RazaoSocial}");
        Console.WriteLine($"  CNPJ         : {nota.Emissor.Cnpj}");
        Console.WriteLine();
        Console.WriteLine("CLIENTE");
        Console.WriteLine($"  Nome  : {nota.Cliente.Nome}");
        Console.WriteLine($"  CPF   : {nota.Cliente.Cpf}");
        Console.WriteLine($"  Email : {nota.Cliente.Email}");
        Console.WriteLine();

        // Exibe corpo específico (produtos ou serviço)
        nota.ExibirCorpo();

        Console.WriteLine();
        Console.WriteLine($"TOTAL: R$ {nota.Total:F2}");
        Console.WriteLine(new string('-', 55));
    }
}
