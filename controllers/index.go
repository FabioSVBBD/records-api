package controllers

import (
	"main/controllers/transactions"

	"github.com/gin-gonic/gin"
)

func Init (router *gin.Engine) {
	TransactionsInit(router)
}

func TransactionsInit(router *gin.Engine) {
	router.GET("/transactions", transactions.GetTransactions)
	router.GET("/transactions/:id", transactions.GetTransaction)
	router.POST("/transactions", transactions.AddTransactions)
	router.PUT("/transactions/:id", transactions.OverwriteTransaction)
	router.PATCH("/transactions/:id", transactions.EditTransaction)
}


// Init other controllers here