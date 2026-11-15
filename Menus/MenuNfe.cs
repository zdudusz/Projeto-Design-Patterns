using SistemaDeNFe.Models;
using SistemaDeNFe.Patterns.Factory;
using SistemaDeNFe.Patterns.Observer;
using SistemaDeNFe.Patterns.Strategy;
using SistemaDeNFe.Repositories;

namespace SistemaDeNFe.Menus;

/// <summary>
/// Menu de Notas Fiscais.
/// Orquestra o uso de todos os quatro padrões:
///   - Factory Method (Emitir NFe)
///   - Observer       (notificação ao emitir)
///   - Strategy       (imprimir em TXT ou JSON)
///   - Singleton      (dados do emissor — usados dentro de NotaFiscal)
/// </summary>
public class MenuNfe
{
    private readonly NfeRepository    _nfeRepo;
    private readonly ClienteRepository _clienteRepo;
    private readonly EmissorDeNotas   _emissor; // Subject do Observer

    public MenuNfe(NfeRepository nfeRepo, ClienteRepository clienteRepo)
    {
        _nfeRepo      = nfeRepo;
        _clienteRepo  = clienteRepo;

        // Configura o Observer: registra o notificador de e-mail
        _emissor = new EmissorDeNotas();
        _emissor.Registrar(new NotificadorEmail());
    }

    public void Exibir()
    {
        bool executando = true;

        while (executando)
        {
            Console.WriteLine();
            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║       NOTAS FISCAIS          ║");
            Console.WriteLine("╠══════════════════════════════╣");
            Console.WriteLine("║  1 - Emitir NFe              ║");
            Console.WriteLine("║  2 - Listar NFes             ║");
            Console.WriteLine("║  3 - Imprimir NFe            ║");
            Console.WriteLine("║  0 - Voltar                  ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.Write("  Opção: ");

            switch (Console.ReadLine()?.Trim())
            {
                case "1": EmitirNfe();   break;
                case "2": ListarNfes();  break;
                case "3": ImprimirNfe(); break;
                case "0": executando = false; break;
                default:
                    Console.WriteLine("  Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    // ─────────────────────────────────────────────
    // Emitir NFe — usa Factory Method + Observer
    // ─────────────────────────────────────────────
    private void EmitirNfe()
    {
        // 1. Seleciona o cliente destinatário
        var cliente = SelecionarCliente();
        if (cliente is null) return;

        // 2. Escolhe o tipo de nota (submenu do Factory Method)
        Console.WriteLine();
        Console.WriteLine("  Tipo de nota:");
        Console.WriteLine("  1 - Nota Fiscal de Produto");
        Console.WriteLine("  2 - Nota Fiscal de Serviço");
        Console.Write("  Opção: ");
        string? tipoEscolhido = Console.ReadLine()?.Trim();

        NotaFiscal nota;

        if (tipoEscolhido == "1")
        {
            // Factory Method: a fábrica decide qual ConcreteProduct criar
            NotaFiscalFactory fabrica = new FabricaNotaProduto();
            var notaProduto = (NotaFiscalProduto)fabrica.CriarNota();
            notaProduto.Cliente = cliente;
            PreencherProdutos(notaProduto);
            nota = notaProduto;
        }
        else if (tipoEscolhido == "2")
        {
            // Factory Method: outra fábrica, outro ConcreteProduct
            NotaFiscalFactory fabrica = new FabricaNotaServico();
            var notaServico = (NotaFiscalServico)fabrica.CriarNota();
            notaServico.Cliente = cliente;
            PreencherServico(notaServico);
            nota = notaServico;
        }
        else
        {
            Console.WriteLine("  Opção inválida. Emissão cancelada.");
            return;
        }

        // 3. Salva a nota
        _nfeRepo.Salvar(nota);

        // 4. Exibe o resumo
        nota.ExibirNota();

        // 5. Observer: notifica todos os observadores registrados (e-mail, etc.)
        _emissor.Notificar(nota);

        Console.WriteLine("\n  ✔ Nota Fiscal emitida com sucesso!");
    }

    private Cliente? SelecionarCliente()
    {
        var lista = _clienteRepo.ListarTodos();

        if (lista.Count == 0)
        {
            Console.WriteLine("  Nenhum cliente cadastrado. Cadastre um cliente primeiro.");
            return null;
        }

        Console.WriteLine("\n  Clientes disponíveis:");
        for (int i = 0; i < lista.Count; i++)
            Console.WriteLine($"  {i + 1}. {lista[i].Nome} (CPF: {lista[i].Cpf})");

        Console.Write("  Escolha o número do cliente: ");
        if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= lista.Count)
            return lista[idx - 1];

        Console.WriteLine("  Seleção inválida.");
        return null;
    }

    private void PreencherProdutos(NotaFiscalProduto nota)
    {
        Console.WriteLine("\n  Produtos disponíveis:");
        foreach (var p in EstoqueRepository.Produtos)
            Console.WriteLine($"  {p}");

        Console.WriteLine("  Digite o ID do produto para adicionar (0 para finalizar):");

        while (true)
        {
            Console.Write("  > ");
            if (!int.TryParse(Console.ReadLine(), out int id)) continue;
            if (id == 0) break;

            var produto = EstoqueRepository.BuscarPorId(id);
            if (produto is null)
            {
                Console.WriteLine("  Produto não encontrado.");
                continue;
            }

            nota.AdicionarProduto(produto);
            Console.WriteLine($"  ✔ {produto.Nome} adicionado. Subtotal: R$ {nota.Total:F2}");
        }

        if (nota.Produtos.Count == 0)
            Console.WriteLine("  Nenhum produto adicionado. A nota será emitida com total R$ 0,00.");
    }

    private void PreencherServico(NotaFiscalServico nota)
    {
        Console.Write("\n  Descrição do serviço: ");
        var desc = Console.ReadLine() ?? "Serviço prestado";

        Console.Write("  Valor do serviço (R$): ");
        if (decimal.TryParse(Console.ReadLine()?.Replace(",", "."),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out decimal valor))
        {
            nota.DefinirValorServico(valor, desc);
        }
        else
        {
            Console.WriteLine("  Valor inválido. Definido como R$ 0,00.");
        }
    }

    // ─────────────────────────────────────────────
    // Listar NFes
    // ─────────────────────────────────────────────
    private void ListarNfes()
    {
        var notas = _nfeRepo.ListarTodas();

        if (notas.Count == 0)
        {
            Console.WriteLine("\n  Nenhuma nota fiscal emitida.");
            return;
        }

        Console.WriteLine($"\n  {notas.Count} nota(s) emitida(s):\n");
        foreach (var n in notas)
        {
            string tipo = n is NotaFiscalProduto ? "Produto" : "Serviço";
            Console.WriteLine($"  Nº {n.Numero:D6} | {n.DataEmissao:dd/MM/yyyy} | " +
                              $"Cliente: {n.Cliente.Nome} | Tipo: {tipo} | Total: R$ {n.Total:F2}");
        }
    }

    // ─────────────────────────────────────────────
    // Imprimir NFe — usa Strategy
    // ─────────────────────────────────────────────
    private void ImprimirNfe()
    {
        var notas = _nfeRepo.ListarTodas();

        if (notas.Count == 0)
        {
            Console.WriteLine("\n  Nenhuma nota fiscal emitida.");
            return;
        }

        Console.Write("\n  Informe o número da nota: ");
        if (!int.TryParse(Console.ReadLine(), out int num))
        {
            Console.WriteLine("  Número inválido.");
            return;
        }

        var nota = _nfeRepo.BuscarPorNumero(num);
        if (nota is null)
        {
            Console.WriteLine("  Nota não encontrada.");
            return;
        }

        // Escolha da estratégia de impressão (Strategy)
        Console.WriteLine("\n  Formato de impressão:");
        Console.WriteLine("  1 - TXT");
        Console.WriteLine("  2 - JSON");
        Console.Write("  Opção: ");
        string? formato = Console.ReadLine()?.Trim();

        // O Context recebe a estratégia escolhida pelo usuário em tempo de execução
        IEstrategiaImpressao estrategia = formato == "2"
            ? new ImpressaoJson()
            : new ImpressaoTxt();

        var contexto = new ContextoImpressao(estrategia);
        contexto.Executar(nota); // delega a impressão à estratégia
    }
}
