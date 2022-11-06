using records.Models.DTOs;

namespace records.Models;

public class Transaction
{
    public String Id { get; }
    public string Type { get; set; }
    public float Amount { get; set; }
    public DateTime Date { get; set; }
    public DateTime Timestamp { get; private set; }

    public Transaction(string id, string type, float amount, DateTime date)
    {
        Id = id;
        Type = type;
        Amount = amount >= 0 ? amount : 0;
        Date = date;
        Timestamp = DateTime.Now;
    }

    public TransactionReadDTO ToReadDTO() => new(Id, Type, Amount, Date);
    public TransactionWriteDTO ToWriteDTO() => new(Type, Amount, Date);
}

public static class TransactionType
{
    public static String Withdrawal => "Withdrawal";
    public static String Deposit => "Deposit";
}
