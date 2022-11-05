package transactions

import (
	"time"

	"github.com/google/uuid"
)

type TransactionDTO struct {
	Type string `json:"type"`
	Amount float64 `json:"amount"`
}

type TransactionDTOList struct {
	Transactions []TransactionDTO `json:"transactions" binding:"required"`
}

type Transaction struct {
	Type string `json:"type"`
	Amount float64 `json:"amount"`
	Timestamp time.Time
	ID string
}

func (dto TransactionDTO) ToType(id string) (Transaction) {
	var transaction Transaction

	transaction.Amount = dto.Amount
	transaction.Type = dto.Type
	transaction.Timestamp = time.Now()
	
	if (id == "") {
		transaction.ID = uuid.New().String()
	} else {
		transaction.ID = id;
	}

	return transaction
}

func (transaction Transaction) ToDTO() (TransactionDTO) {
	var dto TransactionDTO

	dto.Amount = transaction.Amount
	dto.Type = transaction.Type
	
	return dto
}
