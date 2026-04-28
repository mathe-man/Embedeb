

namespace EmbeDebInterpreter.CommunicationProvider;

public class ConsoleCommunicationProvider : ICommunicationProvider
{
    public event EventHandler<string>? OnCommunicationReceived;
    public ConsoleCommunicationProvider()
    {
        Task.Run(() => ListenToConsoleInput());
    }
    private void ListenToConsoleInput()
    {
        while (true)
        {
            string input = Console.ReadLine() ?? string.Empty;
            OnCommunicationReceived?.Invoke(this, input);
        }
    }
}
