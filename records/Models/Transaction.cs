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

public static class TransactionMock
{
    public static List<Transaction> Data = new()
    {
        new Transaction(Guid.NewGuid().ToString(), TransactionType.Withdrawal, 300, new DateTime(2022, 10, 11)),
        new Transaction(Guid.NewGuid().ToString(), TransactionType.Deposit, 5000, new DateTime(2022, 03, 14)),
        new Transaction(Guid.NewGuid().ToString(), TransactionType.Deposit, 652, new DateTime(2022, 06, 28)),
        new Transaction(Guid.NewGuid().ToString(), TransactionType.Withdrawal, 780.50f, new DateTime(2022, 03, 03)),
        new Transaction(Guid.NewGuid().ToString(), TransactionType.Deposit, 10000, new DateTime(2022, 09, 09)),
        new Transaction(Guid.NewGuid().ToString(), TransactionType.Withdrawal, 8000, new DateTime(2022, 10, 10)),
    };
}