using System.Reflection;
using EmbeDebInterpreter.Message.MessageHandlers;

namespace EmbeDebInterpreter.Message;

public class MessageDispatcher
{
    private Dictionary<string, MethodInfo> _handlers = new();

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
        var handlerTypes = assembly.GetTypes().Where(t => t.GetCustomAttributes(typeof(MessageHandler), true).Length > 0);
        foreach (var handlerType in handlerTypes)
        {
            var handlerAttributes = handlerType.GetCustomAttributes(typeof(MessageHandler), true) as MessageHandler[];
            foreach (var handlerAttribute in handlerAttributes)
            {
                var methodInfo = handlerType.GetMethod("Handle", BindingFlags.Public | BindingFlags.Static);
                if (methodInfo == null) throw new Exception($"The handler {handlerType.FullName} does not have a public static Handle method.");
                _handlers[handlerAttribute.MessageId] = methodInfo;
            }
        }
    }

    public Message? Dispatch(RawMessage rawMessage)
    {
        if (_handlers.TryGetValue(rawMessage.Type, out var handler))
        {
            return (Message?)handler.Invoke(null, [rawMessage.Content]);
        }
        else
        {
            // TODO Not throwning an error but just ignore when there is no handler, then return a null value
            throw new Exception($"No handler found for message ID: {rawMessage.Type}");
        }
    }
}