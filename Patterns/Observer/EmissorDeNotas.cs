using SistemaDeNFe.Patterns.Factory;

namespace SistemaDeNFe.Patterns.Observer;

/// <summary>
/// Subject (Publicador) do padrão Observer.
///
/// Mantém uma lista de observadores registrados e os notifica
/// automaticamente toda vez que uma nova nota fiscal é emitida.
/// Os observadores não precisam "perguntar" se houve emissão —
/// eles são avisados pelo Subject assim que o evento ocorre.
/// </summary>
public class EmissorDeNotas
{
    // Lista dos observadores registrados (pode haver vários: e-mail, SMS, log, etc.)
    private readonly List<IObservadorNfe> _observadores = new();

    /// <summary>Registra um novo observador para receber notificações.</summary>
    public void Registrar(IObservadorNfe observador) =>
        _observadores.Add(observador);

    /// <summary>Remove um observador da lista de notificações.</summary>
    public void Remover(IObservadorNfe observador) =>
        _observadores.Remove(observador);

    /// <summary>
    /// Notifica TODOS os observadores registrados sobre a emissão de uma nota.
    /// Este método é chamado internamente após salvar a nota.
    /// </summary>
    public void Notificar(NotaFiscal nota)
    {
        foreach (var obs in _observadores)
            obs.AoEmitirNota(nota);
    }
}
