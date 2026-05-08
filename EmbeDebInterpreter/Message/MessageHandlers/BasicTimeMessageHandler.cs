
namespace EmbeDebInterpreter.Message.MessageHandlers;

public class TimeMessage : Message
{
    public int Time { get; }
    public TimeMessage(int time)
        => Time = time;
}

[MessageHandler("TIME")]
public class BasicTimeMessageHandler {
    public static Message Handle(string content)
    {
        var time = int.Parse(content);
        return new TimeMessage(time);
    }
}