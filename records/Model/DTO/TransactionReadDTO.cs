namespace records.Model.DTO;

public class TransactionReadDTO
{
    private readonly RecordsDbContext Context = new();
    public TransactionReadDTO(Transaction transaction)
    {
        TransactionType? type = Context.TransactionTypes.Where(type => type.Id == transaction.TypeId).FirstOrDefault();

        if (type == null)
            throw new Exception("Type invalid");

        Id = transaction.Id;
        Type = type.Description;
        Amount = transaction.Amount;
        Description = transaction.Description;
        Date = transaction.Date;
    }

    public string Id { get; set; } = null!;
    public string Type { get; set; } = null!;
    public int Amount { get; set; }
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
}
