using System;
using System.Collections.Generic;

namespace records.Model;

public partial class Transaction
{
    public Transaction() { }

    public Transaction(string id, string userId, int typeId, int amount, string description, DateTime date, DateTime timestamp, TransactionType type, User user)
    {
        Id = id;
        UserId = userId;
        TypeId = typeId;
        Amount = amount;
        Description = description;
        Date = date;
        Timestamp = timestamp;
        //Type = type;
        //User = user;
    }

    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public int TypeId { get; set; }

    public int Amount { get; set; }

    public string Description { get; set; } = null!;

    public DateTime Date { get; set; }

    public DateTime Timestamp { get; set; }

    public virtual TransactionType Type { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
