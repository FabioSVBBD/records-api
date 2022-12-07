using System;
using System.Collections.Generic;

namespace records.Model;

public partial class TransactionType
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}
