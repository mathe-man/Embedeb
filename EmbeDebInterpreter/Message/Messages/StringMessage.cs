
namespace EmbeDebInterpreter.Message.MessageHandlers;

public class StringMessage : Message
{
    public string Value { get; }
    public StringMessage(string value)
    {
        Value = value;
        RaiseObjectCreated();
    }

    public override string ToString()
        => Value;
}

public class StringMessageHandler
{
    [MessageHandler("String")]

    public static Message Handle(RawMessage me)
        => new StringMessage(me.Content);
}