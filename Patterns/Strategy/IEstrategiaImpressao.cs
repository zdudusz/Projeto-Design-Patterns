using SistemaDeNFe.Patterns.Factory;

namespace SistemaDeNFe.Patterns.Strategy;

/// <summary>
/// PADRÃO: Strategy
///
/// Define o contrato para as diferentes estratégias de impressão de nota.
/// Permite trocar o algoritmo de impressão em tempo de execução,
/// sem alterar a classe que solicita a impressão.
///
/// Papéis:
///   IEstrategiaImpressao  ← Strategy (interface)
///   ImpressaoTxt          ← ConcreteStrategy A
///   ImpressaoJson         ← ConcreteStrategy B
///   ContextoImpressao     ← Context (usa a estratégia escolhida)
/// </summary>
public interface IEstrategiaImpressao
{
    /// <summary>Imprime (ou exibe) a nota no formato desta estratégia.</summary>
    void Imprimir(NotaFiscal nota);
}
