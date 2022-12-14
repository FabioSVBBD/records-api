namespace records.Models;
public class User
{
    public String Id { get; }
    public List<Transaction> Transactions { get; }
    private DateTime Timestamp { get; }

    public User(String id, List<Transaction> transactions)
    {
        Id = id;
        Transactions = transactions;
        Timestamp = DateTime.Now;
    }

    public User() : this(Guid.NewGuid().ToString(), new List<Transaction>()) { }
}

public static class Mock
{
    public static List<User> Users = new()
    {
        new User("mock", new(){
            new Transaction(Guid.NewGuid().ToString(), TransactionType.Withdrawal, 300, "Groceries", new DateTime(2022, 10, 11)),
        new Transaction(Guid.NewGuid().ToString(), TransactionType.Deposit, 5000, "An Expensive date", new DateTime(2022, 03, 14)),
        new Transaction(Guid.NewGuid().ToString(), TransactionType.Deposit, 652, "A Cheap date", new DateTime(2022, 06, 28)),
        new Transaction(Guid.NewGuid().ToString(), TransactionType.Withdrawal, 780.50f, "Bought some trousers", new DateTime(2022, 03, 03)),
        new Transaction(Guid.NewGuid().ToString(), TransactionType.Deposit, 10000, "Rent", new DateTime(2022, 09, 09)),
        new Transaction(Guid.NewGuid().ToString(), TransactionType.Withdrawal, 8000, "Rent but less", new DateTime(2022, 10, 10)),
        })
    };
}