using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmbeDebInterpreter.Message;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class MessageHandler : Attribute
{
    public string MessageId { get; set; }
    public MessageHandler(string messageId)
        => MessageId = messageId;
}
