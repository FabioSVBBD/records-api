using Microsoft.AspNetCore.Mvc;
using records.Models;
using records.Models.DTOs;

namespace records.Controllers;

[ApiController]
[Route("transactions")]
public class TransactionController: ControllerBase
{
    [HttpGet]
    public IActionResult GetAllTransactions([FromQuery]String? id)
    {
        Transaction? match = TransactionMock.Data.Find((transaction) => transaction.Id == id);

        if (match == null)
            return Ok(TransactionMock.Data.Select((transaction) => transaction.ToDTO()));

        return Ok(match);
    }

    [HttpPost]
    public IActionResult AddTransactions([FromBody] IEnumerable<TransactionDTO> transactions)
    {
        List<Transaction> postedTransactions = new();

        foreach(TransactionDTO transaction in transactions)
        {
            if (transaction == null || transaction.Type == null || transaction.Amount == null)
                return BadRequest();

            postedTransactions.Add(transaction.ToType());
        }

        TransactionMock.Data.AddRange(postedTransactions);
        return Ok(postedTransactions.Select((transaction) => transaction.ToDTO()));
    }
}
