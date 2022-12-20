using System;
using System.Collections.Generic;

namespace records.Model;

public partial class User
{
    public User ()
    {
        Id = Guid.NewGuid().ToString();
        Timestamp = DateTime.Now;
    }

    public User (string userid)
    {
        Id = userid;
        Timestamp = DateTime.Now;
    }


    public string Id { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}
