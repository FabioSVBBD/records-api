namespace records.Models.DTOs;

public class TransactionWriteDTO
{
    public String? Type { get; }
    public float? Amount { get; }
    public DateTime? Date { get; }

    public TransactionWriteDTO(string? type, float? amount, DateTime? date)
    {
        Type = type;
        Amount = amount;
        Date = date;
    }

    public Transaction ToType()
    {
        if (Type == null || Amount == null || Date == null)
            throw new Exception("TransactionWriteDTO: Null Properties");

        return new(Guid.NewGuid().ToString(), Type, Amount.Value, Date.Value);
    }
}
