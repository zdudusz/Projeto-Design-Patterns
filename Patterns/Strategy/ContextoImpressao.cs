using SistemaDeNFe.Patterns.Factory;

namespace SistemaDeNFe.Patterns.Strategy;

/// <summary>
/// Context do padrão Strategy.
///
/// Mantém uma referência à estratégia atual e delega a impressão a ela.
/// O usuário pode trocar a estratégia em tempo de execução (TXT ↔ JSON)
/// sem que o restante do código precise ser alterado.
/// </summary>
public class ContextoImpressao
{
    // Estratégia atualmente selecionada (pode ser trocada a qualquer momento)
    private IEstrategiaImpressao _estrategia;

    public ContextoImpressao(IEstrategiaImpressao estrategia)
    {
        _estrategia = estrategia;
    }

    /// <summary>Troca a estratégia de impressão em tempo de execução.</summary>
    public void DefinirEstrategia(IEstrategiaImpressao estrategia) =>
        _estrategia = estrategia;

    /// <summary>Executa a impressão usando a estratégia configurada.</summary>
    public void Executar(NotaFiscal nota) =>
        _estrategia.Imprimir(nota);
}
