namespace records.Models.DTOs;

public class TransactionWriteDTO
{
    public String? Type { get; }
    public float? Amount { get; }
    public String? Description { get; }
    public DateTime? Date { get; }

    public TransactionWriteDTO() { }

    public TransactionWriteDTO(string? type, float? amount, String? description, DateTime? date)
    {
        Type = type;
        Amount = amount;
        Description = description;
        Date = date;
    }

    public Transaction ToType()
    {
        if (Type == null || Amount == null ||Description == null || Date == null)
            throw new Exception("TransactionWriteDTO: Null Properties");

        return new(Guid.NewGuid().ToString(), Type, Amount.Value, Description, Date.Value);
    }
}
