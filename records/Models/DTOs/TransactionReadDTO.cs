namespace records.Models.DTOs;

public class TransactionReadDTO
{
    public String? Id { get; }
    public String? Type { get; }
    public float? Amount { get; }

    public TransactionReadDTO(String? id, String? type, float? amount)
    {
        Id = id;
        Type = type;
        Amount = amount;
    }

    public Transaction ToType()
    {
        if (Type == null || Amount == null)
            throw new Exception("TransactionReadDTO: Null Properties");

        return new(Id ?? Guid.NewGuid().ToString(), Type, Amount.Value);
    }
}
