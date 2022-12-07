using Microsoft.AspNetCore.Mvc;
using records.Model;
using records.Model.DTO;
//using records.Models;
//using records.Models.DTOs;
using records.Utilities;

namespace records.Controllers;

[ApiController]
[Route("{userid}/transactions")]
public class TransactionController : ControllerBase
{
    private readonly RecordsDbContext Context = new();

    [HttpGet]
    public IActionResult GetAllTransactions([FromRoute] String userid, [FromQuery] String? id)
    {
        User? user = Context.Users.Find(userid);

        if (user == null)
            return BadRequest(new Message($"user with id {userid} does not exist"));

        if (id == null)
            return Ok(new Response<IEnumerable<TransactionReadDTO>>("All Transactions", Context.Transactions.Where(t => t.UserId == user.Id).Select(transaction => new TransactionReadDTO(transaction))));

        Transaction? match = user.Transactions.Where(transaction => transaction.Id == id).FirstOrDefault();

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

    //[HttpPatch]
    //public IActionResult PatchTransaction([FromRoute] String userid, [FromQuery] String id, [FromBody] TransactionWriteDTO patch)
    //{
    //    User? user = Mock.Users.Find((user) => user.Id == userid);

    //    if (user == null)
    //        return BadRequest(new Message($"user with id {userid} does not exist"));

    //    Transaction? match = user.Transactions.Find((transaction) => transaction.Id == id);

    //    if (match == null)
    //        return NotFound(new Message($"Transaction with id {id} not found"));

    //    if (patch.Type != null)
    //        match.Type = patch.Type;

    //    if (patch.Amount != null)
    //        match.Amount = patch.Amount.Value;

    //    if (patch.Date != null)
    //        match.Date = patch.Date.Value;

    //    return Ok(new Response<TransactionReadDTO>($"Transaction with id {id} updated", match.ToReadDTO()));
    //}

    //[HttpPut]
    //public IActionResult PutTransaction([FromRoute] String userid, [FromQuery] String id, [FromBody] TransactionWriteDTO put)
    //{
    //    User? user = Mock.Users.Find((user) => user.Id == userid);

    //    if (user == null)
    //        return BadRequest(new Message($"user with id {userid} does not exist"));

    //    Transaction? match = user.Transactions.Find((transaction) => transaction.Id == id);

    //    if (match == null)
    //        return NotFound(new Message($"Transaction with id {id} not found"));

    //    if (put.Amount == null || put.Type == null || put.Date == null)
    //        return BadRequest(new Message("Fields type, amount and date required"));

    //    match.Amount = put.Amount.Value;
    //    match.Type = put.Type;
    //    match.Date = put.Date.Value;

    //    return Ok(new Response<TransactionReadDTO>($"Transaction with id {id} updated", match.ToReadDTO()));
    //}
}
