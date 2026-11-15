namespace SistemaDeNFe.Models;

/// <summary>Representa um cliente cadastrado no sistema.</summary>
public class Cliente
{
    public string Nome  { get; set; } = string.Empty;
    public string Cpf   { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public Cliente(string nome, string cpf, string email)
    {
        Nome  = nome;
        Cpf   = cpf;
        Email = email;
    }

    public override string ToString() =>
        $"  Nome : {Nome}\n  CPF  : {Cpf}\n  Email: {Email}";
}
