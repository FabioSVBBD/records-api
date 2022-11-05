using Microsoft.AspNetCore.Mvc;
using records.Models;
using records.Models.DTOs;
using records.Utilities;

namespace records.Controllers;

[ApiController]
[Route("transactions")]
public class TransactionController: ControllerBase
{
    [HttpGet]
    public IActionResult GetAllTransactions([FromQuery] String? id)
    {
        if (id == null)
            return Ok(new Response<IEnumerable<TransactionReadDTO>>("All Transactions", TransactionMock.Data.Select((transaction) => transaction.ToReadDTO())));

        Transaction? match = TransactionMock.Data.Find((transaction) => transaction.Id == id);

        if (match == null)
            return NotFound(new Message($"Transaction with id {id} not found"));

        return Ok(match);
    }

    [HttpPost]
    public IActionResult AddTransactions([FromBody] IEnumerable<TransactionWriteDTO> transactions)
    {
        List<Transaction> postedTransactions = new();

        foreach(TransactionWriteDTO transaction in transactions)
        {
            if (transaction == null || transaction.Type == null || transaction.Amount == null)
                return BadRequest(new Message("Fields type and amount required."));

            postedTransactions.Add(transaction.ToType());
        }

        TransactionMock.Data.AddRange(postedTransactions);
        return Ok(new Response<IEnumerable<TransactionReadDTO>>("Added transactions successfully", postedTransactions.Select((transaction) => transaction.ToReadDTO())));
    }

    [HttpDelete]
    public IActionResult DeleteTransaction([FromQuery] String id)
    {
        Transaction? match = TransactionMock.Data.Find((transaction) => transaction.Id == id);

        if (match == null)
            return NotFound(new Message($"Transaction with id {id} not found"));

        TransactionMock.Data.Remove(match);
        return Ok(new Response<Transaction>($"Transaction with id {id} removed", match));
    }

    [HttpPatch]
    public IActionResult PatchTransaction([FromQuery] String id, [FromBody] TransactionWriteDTO patch)
    {
        Transaction? match = TransactionMock.Data.Find((transaction) => transaction.Id == id);

        if (match == null)
            return NotFound(new Message($"Transaction with id {id} not found"));

        if (patch.Type != null)
            match.Type = patch.Type;

        if (patch.Amount != null)
            match.Amount = patch.Amount.Value;

        return Ok(new Response<TransactionReadDTO>($"Transaction with id {id} updated", match.ToReadDTO()));
    }

    [HttpPut]
    public IActionResult PutTransaction([FromQuery] String id, [FromBody] TransactionWriteDTO put)
    {
        Transaction? match = TransactionMock.Data.Find((transaction) => transaction.Id == id);

        if (match == null)
            return NotFound(new Message($"Transaction with id {id} not found"));

        if (put.Amount == null || put.Type == null)
            return BadRequest(new Message("Fields type and amount required"));

        match.Amount = put.Amount.Value;
        match.Type = put.Type;

        return Ok(new Response<TransactionReadDTO>($"Transaction with id {id} updated", match.ToReadDTO()));
    }
}
