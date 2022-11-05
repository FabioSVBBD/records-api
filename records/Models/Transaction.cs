using records.Models.DTOs;

namespace records.Models;
public class Transaction
{
    public String Id { get; }
    public string Type { get; set; }
    public float Amount { get; set; }
    
    public DateTime Timestamp { get; private set; }

    public Transaction(string id, string type, float amount)
    {
        Id = id;
        Type = type;
        Amount = amount >= 0 ? amount : 0;
        Timestamp = DateTime.Now;
    }

    public TransactionReadDTO ToReadDTO() => new(Id, Type, Amount);
    public TransactionWriteDTO ToWriteDTO() => new(Type, Amount);
}

public static class TransactionType
{
    public static String Withdrawal => "Withdrawal";
    public static String Deposit => "Deposit";
}

public static class TransactionMock
{
    public static List<Transaction> Data = new()
    {
        new Transaction(Guid.NewGuid().ToString(), TransactionType.Withdrawal, 300),
        new Transaction(Guid.NewGuid().ToString(), TransactionType.Deposit, 5000),
        new Transaction(Guid.NewGuid().ToString(), TransactionType.Deposit, 652),
        new Transaction(Guid.NewGuid().ToString(), TransactionType.Withdrawal, 780.50f),
        new Transaction(Guid.NewGuid().ToString(), TransactionType.Deposit, 10000),
        new Transaction(Guid.NewGuid().ToString(), TransactionType.Withdrawal, 8000),
    };
}