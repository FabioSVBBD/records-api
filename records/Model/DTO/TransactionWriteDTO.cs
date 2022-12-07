namespace records.Model.DTO;

public class TransactionWriteDTO
{
    private readonly RecordsDbContext Context = new();

    public TransactionWriteDTO() { }

    public TransactionWriteDTO(Transaction transaction)
    {
        Type = transaction.Type.Description;
        Amount = transaction.Amount;
        Description = transaction.Description;
        Date = transaction.Date;
    }

    public Transaction ToTransaction(string userid)
    {
        User? user = Context.Users.Where(user => user.Id == userid).FirstOrDefault();

        if (user == null)
            throw new Exception("User does not exist");

        TransactionType? type = Context.TransactionTypes.Where(type => type.Description == Type).FirstOrDefault();

        if (type == null)
            throw new Exception("Invalid Transaction Type");

        return new Transaction(Guid.NewGuid().ToString(), userid, type.Id, Amount, Description, Date, DateTime.Now, type, user);
    }

    public string Type { get; set; } = null!;
    public int Amount { get; set; }
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
}
