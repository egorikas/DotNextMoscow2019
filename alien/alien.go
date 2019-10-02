package main

import (
	"fmt"
	"net/http"
)

func main() {
	println("alien на 8000 порту")
	http.HandleFunc("/api/number", AlienServer)
	http.ListenAndServe(":8000", nil)
}

func AlienServer(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintf(w, "alien")
}
