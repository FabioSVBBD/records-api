using Microsoft.AspNetCore.Mvc;
using records.Model;
using records.Model.DTO;
using records.Utilities;

namespace records.Controllers;

[ApiController]
[Route("{userid}/transactions")]
public class TransactionController : ControllerBase
{
    private readonly RecordsDbContext Context = new();

    [HttpGet]
    public IActionResult GetAllTransactions([FromRoute] String userid, [FromQuery] String? id, [FromQuery] int? year, [FromQuery] int? month, [FromQuery] int? day)
    {
        User? user = Context.Users.Find(userid);

        if (user == null)
            return BadRequest(new Message($"user with id {userid} does not exist"));

        if (id == null)
        {
            List<Transaction> matches = Context.Transactions.Where(t => (t.UserId == user.Id) && 
            (year == null || t.Date.Year == year) &&
            (month == null || t.Date.Month == month) &&
            (day == null || t.Date.Day == day)).ToList();

            return Ok(new Response<IEnumerable<TransactionReadDTO>>("All Transactions", matches.Select(match => new TransactionReadDTO(match))));
        }

        Transaction? match = Context.Transactions.Where(transaction => transaction.Id == id && transaction.UserId == userid).FirstOrDefault();

        if (match == null)
            return NotFound(new Message($"Transaction with id {id} not found"));

        return Ok(new TransactionReadDTO(match));
    }

    [HttpPost]
    public IActionResult AddTransactions([FromRoute] String userid, [FromBody] IEnumerable<TransactionWriteDTO> transactions)
    {
        User? user = Context.Users.Find(userid);

        if (user == null)
            return BadRequest(new Message($"user with id {userid} does not exist"));

        List<Transaction> postedTransactions = new();

        foreach (TransactionWriteDTO transaction in transactions)
        {
            if (transaction == null || transaction.Type == null || Context.TransactionTypes.Where(type => type.Description == transaction.Type).FirstOrDefault() == null)
                return BadRequest(new Message("Fields type and amount required."));

            postedTransactions.Add(transaction.ToTransaction(user.Id));
        }

        postedTransactions.ForEach(user.Transactions.Add);
        Context.Update(user);
        Context.SaveChanges();

        return Ok(new Response<IEnumerable<TransactionReadDTO>>("Added transactions successfully", postedTransactions.Select((transaction) => new TransactionReadDTO(transaction))));
    }

    [HttpDelete]
    public IActionResult DeleteTransaction([FromRoute] String userid, [FromQuery] String id)
    {
        User? user = Context.Users.Where(user => user.Id == userid).FirstOrDefault();

        if (user == null)
            return BadRequest(new Message($"user with id {userid} does not exist"));

        Transaction? match = Context.Transactions.Where(t => t.Id == id && t.UserId == user.Id).FirstOrDefault();

        if (match == null)
            return NotFound(new Message($"Transaction with id {id} not found"));

        Context.Remove(match);
        Context.SaveChanges();

        return Ok(new Response<Transaction>($"Transaction with id {id} removed", match));
    }

    [HttpPut]
    public IActionResult PutTransaction([FromRoute] String userid, [FromQuery] String id, [FromBody] TransactionWriteDTO put)
    {
        Console.WriteLine(put.Date);


        User? user = Context.Users.Where((user) => user.Id == userid).FirstOrDefault();

        if (user == null)
            return BadRequest(new Message($"user with id {userid} does not exist"));

        Transaction? match = Context.Transactions.Where((transaction) => transaction.Id == id && transaction.UserId == userid).FirstOrDefault();

        if (match == null)
            return NotFound(new Message($"Transaction with id {id} not found"));

        if (put.Type == null)
            return BadRequest(new Message("Fields type, amount and date required"));

        TransactionType? matchedType = Context.TransactionTypes.Where((t) => t.Description == put.Type).FirstOrDefault();

        if (matchedType == null)
            return BadRequest(new Message($"The matched type ${put.Type} is invalid"));

        match.Amount = put.Amount;
        match.TypeId = matchedType.Id;

        Console.WriteLine(match.Date);
        Console.WriteLine(put.Date);
        
        match.Date = put.Date;
        match.Description = put.Description;

        Console.WriteLine(match.Date);

        Context.Update(match);
        Context.SaveChanges();

        return Ok(new Response<TransactionReadDTO>($"Transaction with id {id} updated", new TransactionReadDTO(match)));
    }
}
