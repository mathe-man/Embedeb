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


        _handlers = _handlers.Concat(assemblyHandlers)
            .GroupBy(d => d.Key)
            .ToDictionary(
                g => g.Key,
                g => g.Last().Value // In case of duplicates, take the last one found
            );
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