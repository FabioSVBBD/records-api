namespace records.Models.DTOs;

public class TransactionDTO
{
    private String? Id { get; }
    public String? Type { get; }
    public float? Amount { get; }


    public TransactionDTO(String? id, String? type, float? amount)
    {
        Id = id;
        Amount = amount;
        Type = type;
    }

    public Transaction ToType()
    {
        if (Type == null || Amount == null)
        {
            throw new Exception("TransactionDTO: Null Properties");
        }

        return new(Id ?? Guid.NewGuid().ToString(), Type, Amount.Value);
    }
}
