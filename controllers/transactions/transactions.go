package transactions

import (
	"fmt"
	"net/http"

	"github.com/gin-gonic/gin"
)

func GetTransaction(c *gin.Context) {
	id := c.Param("id")

	for _, transaction := range transactions {
		if (transaction.ID == id) {
			c.JSON(http.StatusOK, transaction)
			return
		}
	}

	c.JSON(http.StatusNotFound, gin.H{ "message": "transaction not found"});
}

func GetTransactions(c *gin.Context) {
	fmt.Println(transactions)

	c.JSON(http.StatusOK, transactions)
}

func AddTransactions(c * gin.Context) {
	dtos := new(TransactionDTOList)

	if err := c.BindJSON(dtos); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{ "message": "An error occurred" })
		return
	}

	var newTransactions []Transaction = []Transaction{}
	for _, transaction := range dtos.Transactions {
		newTransactions = append(newTransactions, transaction.ToType(""))
	}

	transactions = append(transactions, newTransactions...)
	c.JSON(http.StatusOK, gin.H{"message": "Transactions added successfully", "transactions": newTransactions })
}

func OverwriteTransaction(c *gin.Context) {
	id := c.Param("id")

	for i, transaction := range transactions {
		if transaction.ID == id {
			var dto TransactionDTO
			if err := c.BindJSON(&dto); err == nil {
				transactions[i] = dto.ToType(transaction.ID)
				c.JSON(http.StatusOK, gin.H{ "message": "Updated", "transaction": transactions[i].ToDTO()})
				return
			}

			c.JSON(http.StatusBadRequest, gin.H{ "message": "Payload mismatch" })
			return
		}
	}

	c.JSON(http.StatusNotFound, gin.H{ "message": "transaction not found" })
}

func EditTransaction(c *gin.Context) {
	id := c.Param("id")

	for i, transaction := range transactions {
		if transaction.ID == id {

			var dto TransactionDTO
			if err := c.BindJSON(&dto); err != nil {
				c.JSON(http.StatusBadRequest, gin.H{ "message": "Payload mismatch" })
				return
			}

			transactions[i] = dto.ToType(transaction.ID)
			c.JSON(http.StatusOK, gin.H{ "message": "Updated", "transaction": transactions[i].ToDTO()})
			return
		}
	}

	c.JSON(http.StatusBadRequest, gin.H{ "message": "Not found" })
	return;	
}