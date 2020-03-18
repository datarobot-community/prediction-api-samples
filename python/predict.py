import requests
import json
import os
from dotenv import load_dotenv
from pathlib import Path  # python3 only
load_dotenv(dotenv_path=Path('../common') / '.env')

with open('../common/payload_to_predict.json', 'r') as payload_file:
  PAYLOAD = payload_file.read()

PREDICTION_SERVER = os.getenv("PREDICTION_SERVER")
DEPLOYMENT_ID = os.getenv("DEPLOYMENT_ID")
PREDICTION_ENDPOINT = PREDICTION_SERVER + "/predApi/v1.0/deployments/" + DEPLOYMENT_ID + "/predictions"
API_KEY = os.getenv("API_KEY")
DATAROBOT_KEY = os.getenv("DATAROBOT_KEY")

# Set HTTP headers. The charset should match the contents of the file.
headers = {'Content-Type': 'application/json; charset=UTF-8', 'datarobot-key': DATAROBOT_KEY, 'Authorization': 'Bearer ' + API_KEY}

# Make API request for predictions
predictions_response = requests.post(
    PREDICTION_ENDPOINT,
    data=PAYLOAD,
    headers=headers,
)

print(predictions_response.status_code)
print(predictions_response.json())
