using System.Text.Json.Serialization;

namespace records.Utilities;

public class Message
{
    [JsonPropertyName("message")]
    public String MessageStr { get; }

    public Message(string message)
    {
        MessageStr = message;
    }
}
