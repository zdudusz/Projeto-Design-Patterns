using SistemaDeNFe.Models;
using SistemaDeNFe.Repositories;

namespace SistemaDeNFe.Menus;

/// <summary>Menu de gerenciamento de clientes (Adicionar, Listar, Remover).</summary>
public class MenuClientes
{
    private readonly ClienteRepository _repo;

    public MenuClientes(ClienteRepository repo) => _repo = repo;

    public void Exibir()
    {
        bool executando = true;

        while (executando)
        {
            Console.WriteLine();
            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║         CLIENTES             ║");
            Console.WriteLine("╠══════════════════════════════╣");
            Console.WriteLine("║  1 - Adicionar cliente       ║");
            Console.WriteLine("║  2 - Listar clientes         ║");
            Console.WriteLine("║  3 - Remover cliente         ║");
            Console.WriteLine("║  4 - Sair                    ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.Write("  Opção: ");

            switch (Console.ReadLine()?.Trim())
            {
                case "1": AdicionarCliente(); break;
                case "2": ListarClientes();   break;
                case "3": RemoverCliente();   break;
                case "4": executando = false; break;
                default:
                    Console.WriteLine("  Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    private void AdicionarCliente()
    {
        Console.WriteLine("\n  -- Novo Cliente --");
        Console.Write("  Nome : "); var nome  = Console.ReadLine() ?? "";
        Console.Write("  CPF  : "); var cpf   = Console.ReadLine() ?? "";
        Console.Write("  Email: "); var email = Console.ReadLine() ?? "";

        _repo.Adicionar(new Cliente(nome, cpf, email));
        Console.WriteLine("  ✔ Cliente cadastrado com sucesso!");
    }

    private void ListarClientes()
    {
        var lista = _repo.ListarTodos();

        if (lista.Count == 0)
        {
            Console.WriteLine("  Nenhum cliente cadastrado.");
            return;
        }

        Console.WriteLine($"\n  {lista.Count} cliente(s) cadastrado(s):\n");
        foreach (var c in lista)
        {
            Console.WriteLine(c.ToString());
            Console.WriteLine(new string('-', 30));
        }
    }

    private void RemoverCliente()
    {
        Console.Write("\n  Informe o CPF do cliente a remover: ");
        var cpf = Console.ReadLine() ?? "";

        bool removido = _repo.Remover(cpf);
        Console.WriteLine(removido
            ? "  ✔ Cliente removido com sucesso!"
            : "  ✘ Cliente não encontrado.");
    }
}
