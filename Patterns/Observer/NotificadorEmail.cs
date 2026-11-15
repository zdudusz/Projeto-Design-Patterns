using SistemaDeNFe.Patterns.Factory;

namespace SistemaDeNFe.Patterns.Observer;

/// <summary>
/// ConcreteObserver do Observer.
///
/// Reage ao evento de emissão de nota simulando o envio de um e-mail
/// para o cliente. Em um sistema real, aqui estaria a chamada a um
/// serviço de e-mail (SMTP, SendGrid, etc.).
/// </summary>
public class NotificadorEmail : IObservadorNfe
{
    /// <summary>
    /// Executado automaticamente pelo Subject (EmissorDeNotas)
    /// sempre que uma nova nota for emitida.
    /// </summary>
    public void AoEmitirNota(NotaFiscal nota)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine();
        Console.WriteLine("  [E-MAIL] ✉  Simulação de envio de e-mail:");
        Console.WriteLine($"             E-mail enviado para: {nota.Cliente.Email}");
        Console.WriteLine($"             Assunto: Sua Nota Fiscal Nº {nota.Numero:D6} foi emitida.");
        Console.ResetColor();
    }
}
