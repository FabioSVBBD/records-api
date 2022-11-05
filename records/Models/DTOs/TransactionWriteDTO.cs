namespace records.Models.DTOs;

public class TransactionWriteDTO
{
    public String? Type { get; }
    public float? Amount { get; }

    public TransactionWriteDTO(string? type, float? amount)
    {
        Type = type;
        Amount = amount;
    }

    public Transaction ToType()
    {
        if (Type == null || Amount == null)
            throw new Exception("TransactionWriteDTO: Null Properties");

        return new(Guid.NewGuid().ToString(), Type, Amount.Value);
    }
}
