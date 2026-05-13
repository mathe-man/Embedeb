using System.Text;

namespace EmbeDebInterpreter.Communication;

public class ParsedCommunication
{
    // First bytes of the communication
    public readonly byte[] MagicNumber;

    // Name of the board who sent the communication
    public readonly string BoardName;

    public readonly string[] Messages;

    public static string defaultMessageSeparator = "|";

    public ParsedCommunication(string source, string? messageSeparator = null)
    {
        if (source == null) throw new ArgumentNullException("source");
        if (string.IsNullOrEmpty(messageSeparator)) messageSeparator = defaultMessageSeparator;

        // Split the source by the message separator, the first part will be the magic number, the second part will be the board name and the rest will be the messages
        var splitedSource = source.Split(messageSeparator, StringSplitOptions.RemoveEmptyEntries);

        // If the source is empty, we return an empty communication
        if (splitedSource.Length == 0) {
            MagicNumber = new Byte[0];
            BoardName = "";
            Messages = new string[0];
            return;
        }

        // In case the source only contains the magic number:
        if (splitedSource.Length == 1) {
            MagicNumber = Encoding.ASCII.GetBytes(splitedSource[0]);
            BoardName = "";
            Messages = new string[0];
            return;
        }

        // In case the source contains the magic number and the board name:
        if (splitedSource.Length == 2) {
            MagicNumber = Encoding.ASCII.GetBytes(splitedSource[0]);
            BoardName = splitedSource[1];
            Messages = new string[0];
            return;
        }


        // In case the source contains the magic number, the board name and some messages:
        MagicNumber = Encoding.ASCII.GetBytes(splitedSource[0]);
        BoardName = splitedSource[1];

        Messages = new string[splitedSource.Length - 2];
        Array.Copy(splitedSource, 2, Messages, 0, Messages.Length); // Copy the messages from the splited source to the Messages array
    }

    public string Build(string? messageSeparator = null)
    {
        if (string.IsNullOrEmpty(messageSeparator)) messageSeparator = defaultMessageSeparator;

        var sb = new StringBuilder();
        sb.Append(Encoding.ASCII.GetString(MagicNumber));
        sb.Append(messageSeparator);
        sb.Append(BoardName);
        sb.Append(messageSeparator);

        foreach (var message in Messages) {
            sb.Append(message);
            sb.Append(messageSeparator);
        }

        return sb.ToString();
    }

    public override string ToString()
        => Build();
}