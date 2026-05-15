using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmbeDebInterpreter.Message;


[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class MessageHandler : Attribute
{
    public List<string> MessageId { get; set; }
    public MessageHandler(string messageId)
        => MessageId = new List<string> { messageId };
    public MessageHandler(params string[] messageIds)
        => MessageId = messageIds.ToList();
}
