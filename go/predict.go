package main

import (
	"bytes"
	"fmt"
	"io/ioutil"
	"log"
	"net/http"
	"os"

	"github.com/joho/godotenv"
)

func main() {
	err := godotenv.Load("../common/.env")
	if err != nil {
		log.Fatal("Error loading .env file")
	}

	PREDICTION_SERVER := os.Getenv("PREDICTION_SERVER")
	DEPLOYMENT_ID := os.Getenv("DEPLOYMENT_ID")
	PREDICTION_ENDPOINT := PREDICTION_SERVER + "/predApi/v1.0/deployments/" + DEPLOYMENT_ID + "/predictions"
	API_KEY := os.Getenv("API_KEY")
	DATAROBOT_KEY := os.Getenv("DATAROBOT_KEY")

	// Read entire file content, giving us little control but
	// making it very simple. No need to close the file.
	content, err := ioutil.ReadFile("../common/payload_to_predict.json")
	if err != nil {
		log.Fatal(err)
	}
	contentReader := bytes.NewReader(content)
	// text := string(content)

	// fmt.Println(text)

	client := &http.Client{}

	// resp, err := client.Post("http://example.com")
	// ...

	req, err := http.NewRequest("POST", PREDICTION_ENDPOINT, contentReader)

	req.Header.Add("Content-Type", "application/json")
	req.Header.Add("datarobot-key", DATAROBOT_KEY)
	req.Header.Add("Authorization", "Bearer "+API_KEY)

	resp, err := client.Do(req)

	if err != nil {
		print(err)
	}
	defer resp.Body.Close()
	body, err := ioutil.ReadAll(resp.Body)
	if err != nil {
		print(err)
	}

	fmt.Println(resp.Status)
	fmt.Println(string(body))

}
