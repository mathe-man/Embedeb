using System.Reflection;
using EmbeDebInterpreter.Message.MessageHandlers;

namespace EmbeDebInterpreter.Message;

public class MessageDispatcher
{
    private MessageHandlerRegister _handlers = new();

    public MessageDispatcher(bool registerCurrentAssembly = true)
    {
        if (registerCurrentAssembly)
        {
            // Register handlers from the current assembly
            RegisterHandlers(Assembly.GetExecutingAssembly());
        }
    }
    public void RegisterHandlers(Assembly assembly)
    {
        var assemblyHandlers = assembly.GetTypes()
            .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.Static))
            .Select(m => new {
                Method = m,
                Attr = m.GetCustomAttribute<MessageHandler>()
            })
            .Where(x => x.Attr != null)
            .ToDictionary(
                x => x.Attr.MessageId, // Key: ID defined in the attribute
                x => x.Method          // Value: the method to call
            );


        foreach (var handler in assemblyHandlers)
            _handlers.AddHandler(handler.Key, handler.Value);
    }

    public int Dispatch(RawMessage rawMessage)
        => _handlers.CallHandlers(rawMessage);
}