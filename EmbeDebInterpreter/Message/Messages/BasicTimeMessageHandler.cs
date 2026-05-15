
namespace EmbeDebInterpreter.Message.MessageHandlers;

public class TimeMessage : Message
{
    public int Time { get; }
    public TimeMessage(int time)
    {
        Time = time;
        RaiseObjectCreated();
    }
}

public class BasicTimeMessageHandler {

    [MessageHandler("TIME")]
    public static Message Handle(RawMessage me)
    {
        var time = int.Parse(me.Content);
        return new TimeMessage(time);
    }
}