using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmbeDebInterpreter.Message;

// TODO: make this attribute support multiple message IDs for a single handler

// TODO: make this attribute target method instead of class,
//          then we can have multiple handlers in a single class.
//          Also change the MessageDispatcher to support this new design.

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class MessageHandler : Attribute
{
    public List<string> MessageId { get; set; }
    public MessageHandler(string messageId)
        => MessageId = new List<string> { messageId };
    public MessageHandler(params string[] messageIds)
        => MessageId = messageIds.ToList();
}
