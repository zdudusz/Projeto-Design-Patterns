namespace SistemaDeNFe.Patterns.Singleton;

/// <summary>
/// PADRÃO: Singleton
///
/// Garante que exista apenas UMA instância dos dados da empresa durante
/// toda a execução do programa. Sempre que precisarmos dos dados do emissor
/// (ex: ao montar uma nota fiscal), chamamos EmpresaEmissora.Instancia
/// em vez de criar um new EmpresaEmissora() — que geraria objetos duplicados.
///
/// Como funciona:
///   - Construtor privado → impede "new EmpresaEmissora()" de fora.
///   - Campo estático _instancia → guarda o único objeto criado.
///   - Propriedade estática Instancia → cria o objeto na primeira chamada
///     e devolve sempre o mesmo nas chamadas seguintes (lazy initialization).
/// </summary>
public class EmpresaEmissora
{
    // --- Singleton: campo que armazena a única instância ---
    private static EmpresaEmissora? _instancia;

    // Dados fixos da empresa (simulando um cadastro real)
    public string RazaoSocial { get; } = "TechStore Comércio de Eletrônicos Ltda.";
    public string Cnpj        { get; } = "12.345.678/0001-99";
    public string Endereco    { get; } = "Rua das Inovações, 42 – Surubim/PE";
    public string Email       { get; } = "contato@techstore.com.br";

    // --- Singleton: construtor privado ---
    private EmpresaEmissora() { }

    /// <summary>
    /// Ponto de acesso global à instância única da empresa.
    /// Cria o objeto apenas na primeira chamada (lazy).
    /// </summary>
    public static EmpresaEmissora Instancia
    {
        get
        {
            // Só cria se ainda não existir
            _instancia ??= new EmpresaEmissora();
            return _instancia;
        }
    }

    /// <summary>Exibe os dados do emissor formatados para a nota fiscal.</summary>
    public void ExibirDados()
    {
        Console.WriteLine($"  Razão Social : {RazaoSocial}");
        Console.WriteLine($"  CNPJ         : {Cnpj}");
        Console.WriteLine($"  Endereço     : {Endereco}");
        Console.WriteLine($"  E-mail       : {Email}");
    }
}
