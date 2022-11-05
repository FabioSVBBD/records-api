package transactions

import (
	"time"

	"github.com/google/uuid"
)

var transactions = []Transaction{
	{ 
		Type: "Withdrawal", 
		Amount: 630, 
		Timestamp: time.Date(2022, 10, 10, 14, 30, 30, 10, time.UTC), 
		ID: uuid.New().String(),
	},
	{ 
		Type: "Deposit", 
		Amount: 700, 
		Timestamp: time.Date(2022, 10, 10, 14, 30, 30, 10, time.UTC), 
		ID: uuid.New().String(),
	},
}