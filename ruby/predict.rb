require 'net/http'
require 'net/https'
require 'uri'
require 'json'
require 'dotenv'

Dotenv.load('../common/.env')

PREDICTION_SERVER = ENV['PREDICTION_SERVER']
DEPLOYMENT_ID = ENV['DEPLOYMENT_ID']
PREDICTION_ENDPOINT = PREDICTION_SERVER + "/predApi/v1.0/deployments/" + DEPLOYMENT_ID + "/predictions"
API_KEY = ENV['API_KEY']
DATAROBOT_KEY = ENV['DATAROBOT_KEY']

PAYLOAD = File.read('../common/payload_to_predict.json')


headers = {
    'Content-Type': 'application/json',
    'charset': 'UTF-8', 
    'datarobot-key': DATAROBOT_KEY, 
    'Authorization': 'Bearer ' + API_KEY
}

uri = URI.parse(PREDICTION_ENDPOINT)
https = Net::HTTP.new(uri.host,uri.port)
https.use_ssl = true

request = Net::HTTP::Post.new(uri.path)

request['Content-Type'] = 'application/json'
request['datarobot-key'] = DATAROBOT_KEY
request['Authorization'] = 'Bearer '+API_KEY

request.body = PAYLOAD

response = https.request(request)

puts response.code
puts response.body