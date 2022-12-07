namespace records.Models.DTOs;

public class TransactionReadDTO
{
    public String? Id { get; }
    public String? Type { get; }
    public float? Amount { get; }
    public String? Description { get; }

    public DateTime? Date { get; }

    public TransactionReadDTO(String? id, String? type, float? amount, String? description, DateTime? date)
    {
        Id = id;
        Type = type;
        Amount = amount;
        Description = description;
        Date = date;
    }

    public Transaction ToType()
    {
        if (Type == null || Amount == null || Description == null || Date == null)
            throw new Exception("TransactionReadDTO: Null Properties");

        return new(Id ?? Guid.NewGuid().ToString(), Type, Amount.Value, Description, Date.Value);
    }
}
