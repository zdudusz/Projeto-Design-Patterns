using SistemaDeNFe.Patterns.Factory;

namespace SistemaDeNFe.Patterns.Strategy;

/// <summary>
/// ConcreteStrategy B: formata e exibe a nota no padrão JSON.
/// Simula o que seria serializado e enviado para uma API ou arquivo .json.
/// </summary>
public class ImpressaoJson : IEstrategiaImpressao
{
    public void Imprimir(NotaFiscal nota)
    {
        Console.WriteLine();
        Console.WriteLine("  [IMPRESSÃO - FORMATO JSON]");
        Console.WriteLine(new string('-', 55));

        // Montamos o JSON manualmente para manter o projeto sem dependências externas
        Console.WriteLine("{");
        Console.WriteLine($"  \"numero\": {nota.Numero},");
        Console.WriteLine($"  \"dataEmissao\": \"{nota.DataEmissao:yyyy-MM-ddTHH:mm:ss}\",");

        Console.WriteLine("  \"emissor\": {");
        Console.WriteLine($"    \"razaoSocial\": \"{nota.Emissor.RazaoSocial}\",");
        Console.WriteLine($"    \"cnpj\": \"{nota.Emissor.Cnpj}\",");
        Console.WriteLine($"    \"email\": \"{nota.Emissor.Email}\"");
        Console.WriteLine("  },");

        Console.WriteLine("  \"cliente\": {");
        Console.WriteLine($"    \"nome\": \"{nota.Cliente.Nome}\",");
        Console.WriteLine($"    \"cpf\": \"{nota.Cliente.Cpf}\",");
        Console.WriteLine($"    \"email\": \"{nota.Cliente.Email}\"");
        Console.WriteLine("  },");

        // Dados específicos de cada tipo de nota
        if (nota is NotaFiscalProduto notaProduto)
        {
            Console.WriteLine("  \"tipo\": \"produto\",");
            Console.WriteLine("  \"produtos\": [");
            for (int i = 0; i < notaProduto.Produtos.Count; i++)
            {
                var p = notaProduto.Produtos[i];
                bool ultimo = i == notaProduto.Produtos.Count - 1;
                Console.WriteLine($"    {{ \"nome\": \"{p.Nome}\", \"preco\": {p.Preco:F2} }}{(ultimo ? "" : ",")}");
            }
            Console.WriteLine("  ],");
        }
        else if (nota is NotaFiscalServico notaServico)
        {
            Console.WriteLine("  \"tipo\": \"servico\",");
            Console.WriteLine($"  \"descricaoServico\": \"{notaServico.DescricaoServico}\",");
        }

        Console.WriteLine($"  \"total\": {nota.Total:F2}");
        Console.WriteLine("}");
        Console.WriteLine(new string('-', 55));
    }
}
