
namespace EmbeDebInterpreter.Message;

public class Message
{
    public readonly string Sender;
    public readonly string Content;
    public Message(string sender, string content)
    {
        Sender = sender;
        Content = content;
    }
    public Message(string source)
    {
        if (string.IsNullOrEmpty(source)) throw new ArgumentNullException("source");
        
        var splitedSource = source.Split('=');

        Sender = splitedSource[0];
        Content = splitedSource[1];
    }
}