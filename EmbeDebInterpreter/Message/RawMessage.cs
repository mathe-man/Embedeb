
namespace EmbeDebInterpreter.Message;

public abstract class Message
{ }


public class RawMessage
{
    public readonly string Sender;
    public readonly string Content;
    public RawMessage(string sender, string content)
    {
        Sender = sender;   
        Content = content;
    }
    public RawMessage(string source)
    {
        if (string.IsNullOrEmpty(source)) throw new ArgumentNullException("source");
        
        var splitedSource = source.Split('=');

        Sender = splitedSource[0];
        Content = splitedSource[1];
    }
}