namespace records.Models.DTOs;

public class UserDTO
{
    public int Id { get; }
    public List<Transaction>? Transactions { get; }

    public UserDTO(int id)
    {
        Id = id;
        Transactions = new();
    }
}
