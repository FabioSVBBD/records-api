namespace records.Model.DTO;

public class UserDTO
{
    public UserDTO(User user)
    {
        Id = user.Id;
        Transactions = user.Transactions;
    }

    public string Id { get; set; } = null!;
    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}

