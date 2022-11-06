using Microsoft.AspNetCore.Mvc;
using records.Models;
using records.Models.DTOs;
using records.Utilities;

namespace records.Controllers;

[ApiController]
[Route("{userid}/transactions")]
public class TransactionController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllTransactions([FromRoute] String userid, [FromQuery] String? id)
    {
        User? user = Mock.Users.Find((user) => user.Id == userid);

        if (user == null)
            return BadRequest(new Message($"user with id {userid} does not exist"));

        if (id == null)
            return Ok(new Response<IEnumerable<TransactionReadDTO>>("All Transactions", user.Transactions.Select((transaction) => transaction.ToReadDTO())));

        Transaction? match = user.Transactions.Find((transaction) => transaction.Id == id);

        if (match == null)
            return NotFound(new Message($"Transaction with id {id} not found"));

        return Ok(match.ToReadDTO());
    }

    [HttpPost]
    public IActionResult AddTransactions([FromRoute] String userid, [FromBody] IEnumerable<TransactionWriteDTO> transactions)
    {
        User? user = Mock.Users.Find((user) => user.Id == userid);

        if (user == null)
            return BadRequest(new Message($"user with id {userid} does not exist"));

        List<Transaction> postedTransactions = new();

        foreach (TransactionWriteDTO transaction in transactions)
        {
            if (transaction == null || transaction.Type == null || transaction.Amount == null)
                return BadRequest(new Message("Fields type and amount required."));

            postedTransactions.Add(transaction.ToType());
        }

        user.Transactions.AddRange(postedTransactions);
        return Ok(new Response<IEnumerable<TransactionReadDTO>>("Added transactions successfully", postedTransactions.Select((transaction) => transaction.ToReadDTO())));
    }

    [HttpDelete]
    public IActionResult DeleteTransaction([FromRoute] String userid, [FromQuery] String id)
    {
        User? user = Mock.Users.Find((user) => user.Id == userid);

        if (user == null)
            return BadRequest(new Message($"user with id {userid} does not exist"));

        Transaction? match = user.Transactions.Find((transaction) => transaction.Id == id);

        if (match == null)
            return NotFound(new Message($"Transaction with id {id} not found"));

        user.Transactions.Remove(match);
        return Ok(new Response<Transaction>($"Transaction with id {id} removed", match));
    }

    [HttpPatch]
    public IActionResult PatchTransaction([FromRoute] String userid, [FromQuery] String id, [FromBody] TransactionWriteDTO patch)
    {
        User? user = Mock.Users.Find((user) => user.Id == userid);

        if (user == null)
            return BadRequest(new Message($"user with id {userid} does not exist"));

        Transaction? match = user.Transactions.Find((transaction) => transaction.Id == id);

        if (match == null)
            return NotFound(new Message($"Transaction with id {id} not found"));

        if (patch.Type != null)
            match.Type = patch.Type;

        if (patch.Amount != null)
            match.Amount = patch.Amount.Value;

        if (patch.Date != null)
            match.Date = patch.Date.Value;

        return Ok(new Response<TransactionReadDTO>($"Transaction with id {id} updated", match.ToReadDTO()));
    }

    [HttpPut]
    public IActionResult PutTransaction([FromRoute] String userid, [FromQuery] String id, [FromBody] TransactionWriteDTO put)
    {
        User? user = Mock.Users.Find((user) => user.Id == userid);

        if (user == null)
            return BadRequest(new Message($"user with id {userid} does not exist"));

        Transaction? match = user.Transactions.Find((transaction) => transaction.Id == id);

        if (match == null)
            return NotFound(new Message($"Transaction with id {id} not found"));

        if (put.Amount == null || put.Type == null || put.Date == null)
            return BadRequest(new Message("Fields type, amount and date required"));

        match.Amount = put.Amount.Value;
        match.Type = put.Type;
        match.Date = put.Date.Value;

        return Ok(new Response<TransactionReadDTO>($"Transaction with id {id} updated", match.ToReadDTO()));
    }
}
