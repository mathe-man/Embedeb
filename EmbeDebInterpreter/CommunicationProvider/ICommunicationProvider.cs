namespace EmbeDebInterpreter.CommunicationProvider;

public interface ICommunicationProvider
{
    public event EventHandler<string>? OnCommunicationReceived;
}
