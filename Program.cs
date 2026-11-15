using SistemaDeNFe.Menus;
using SistemaDeNFe.Repositories;

/*
 * ══════════════════════════════════════════════════════════════
 *  SISTEMA DE EMISSÃO DE NOTAS FISCAIS — Projeto 2° VA
 *  Disciplina: Programação III | Professor: Jheklos
 * ══════════════════════════════════════════════════════════════
 *
 *  Design Patterns aplicados:
 *  ┌──────────────────┬────────────┬────────────────────────────────────────┐
 *  │ Padrão           │ Classe PDF │ Onde é usado                           │
 *  ├──────────────────┼────────────┼────────────────────────────────────────┤
 *  │ Factory Method   │ A          │ Criação de NotaFiscalProduto/Servico    │
 *  │ Observer         │ A          │ Notificação de e-mail ao emitir nota    │
 *  │ Singleton        │ B          │ Instância única de EmpresaEmissora      │
 *  │ Strategy         │ B          │ Impressão da nota em TXT ou JSON        │
 *  └──────────────────┴────────────┴────────────────────────────────────────┘
 */

// Repositórios compartilhados entre os menus (mantêm os dados em memória)
var clienteRepo = new ClienteRepository();
var nfeRepo     = new NfeRepository();

// Instâncias dos menus
var menuNfe      = new MenuNfe(nfeRepo, clienteRepo);
var menuClientes = new MenuClientes(clienteRepo);

bool rodando = true;

while (rodando)
{
    Console.Clear();
    Console.WriteLine("╔══════════════════════════════════╗");
    Console.WriteLine("║   SISTEMA DE EMISSÃO DE NF-e     ║");
    Console.WriteLine("╠══════════════════════════════════╣");
    Console.WriteLine("║  1 - Notas Fiscais               ║");
    Console.WriteLine("║  2 - Clientes                    ║");
    Console.WriteLine("║  0 - Sair                        ║");
    Console.WriteLine("╚══════════════════════════════════╝");
    Console.Write("  Opção: ");

    switch (Console.ReadLine()?.Trim())
    {
        case "1": menuNfe.Exibir();      break;
        case "2": menuClientes.Exibir(); break;
        case "0": rodando = false;        break;
        default:
            Console.WriteLine("  Opção inválida. Pressione Enter para continuar.");
            Console.ReadLine();
            break;
    }
}

Console.WriteLine("\n  Sistema encerrado. Até logo!");
