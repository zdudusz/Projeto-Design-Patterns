using SistemaDeNFe.Patterns.Factory;

namespace SistemaDeNFe.Patterns.Observer;

/// <summary>
/// PADRÃO: Observer
///
/// Define o contrato para todos os "observadores" de emissão de nota fiscal.
/// Qualquer classe que queira ser notificada quando uma nota for emitida
/// deve implementar esta interface.
///
/// Papéis:
///   IObservadorNfe         ← Observer (interface)
///   NotificadorEmail       ← ConcreteObserver
///   EmissorDeNotas         ← Subject (gerencia os observadores e dispara eventos)
/// </summary>
public interface IObservadorNfe
{
    /// <summary>Chamado pelo Subject sempre que uma nova nota é emitida.</summary>
    void AoEmitirNota(NotaFiscal nota);
}
