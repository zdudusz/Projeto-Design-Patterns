namespace SistemaDeNFe.Patterns.Factory;

/// <summary>
/// Creator abstrato do Factory Method.
///
/// Define o método de fábrica CriarNota() que as subclasses devem implementar.
/// O código cliente (menus) trabalha sempre com esta interface,
/// sem precisar conhecer qual tipo concreto de nota será criado.
/// </summary>
public abstract class NotaFiscalFactory
{
    /// <summary>
    /// Método de fábrica: cada subclasse decide QUAL tipo de nota instanciar.
    /// </summary>
    public abstract NotaFiscal CriarNota();
}

// ─────────────────────────────────────────────
// ConcreteCreator 1: fábrica de Nota de Produto
// ─────────────────────────────────────────────

/// <summary>
/// Fábrica concreta que produz NotaFiscalProduto.
/// O cliente chama CriarNota() e recebe um objeto já pronto para uso.
/// </summary>
public class FabricaNotaProduto : NotaFiscalFactory
{
    public override NotaFiscal CriarNota() => new NotaFiscalProduto();
}

// ─────────────────────────────────────────────
// ConcreteCreator 2: fábrica de Nota de Serviço
// ─────────────────────────────────────────────

/// <summary>
/// Fábrica concreta que produz NotaFiscalServico.
/// </summary>
public class FabricaNotaServico : NotaFiscalFactory
{
    public override NotaFiscal CriarNota() => new NotaFiscalServico();
}
