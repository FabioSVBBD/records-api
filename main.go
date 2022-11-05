package main

import (
	"main/controllers"

	"github.com/gin-gonic/gin"
)

func main() {
	router := gin.Default()
	router.SetTrustedProxies([]string{ "localhost:3000" })

	controllers.Init(router);
		
	router.Run(":8000")
}