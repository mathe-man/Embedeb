namespace EmbeDebInterpreter.Communication.CommunicationProvider;

public class ConsoleCommunicationProvider : ICommunicationProvider
{
    public event EventHandler<string>? OnCommunicationReceived;
    
    public void ListenToConsoleInput()
    {
        while (true)
        {
            string input = Console.ReadLine() ?? string.Empty;
            OnCommunicationReceived?.Invoke(this, input);
        }
    }
}
