namespace records.Utilities;

public class Response<T> : Message
{
    public T? Value { get; }

    public Response(string message, T? value)
        : base(message)
    {
        Value = value;
    }
}