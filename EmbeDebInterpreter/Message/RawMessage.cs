
namespace EmbeDebInterpreter.Message;

public abstract class Message
{ }


public class RawMessage
{
    public readonly string Type;
    public readonly string Content;
    public RawMessage(string type, string content)
    {
        Type = type;   
        Content = content;
    }
    public RawMessage(string source)
    {
        if (string.IsNullOrEmpty(source)) throw new ArgumentNullException("source");
        
        var splitedSource = source.Split('=');

        Type = splitedSource[0];
        Content = splitedSource[1];
    }
}