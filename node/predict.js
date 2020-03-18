const got = require('got')
const fs = require('fs')
require('dotenv').config({ path: '../common/.env' })

const PREDICTION_SERVER = process.env.PREDICTION_SERVER
const DEPLOYMENT_ID = process.env.DEPLOYMENT_ID
const PREDICTION_ENDPOINT = `${PREDICTION_SERVER}/predApi/v1.0/deployments/${DEPLOYMENT_ID}/predictions`
const API_KEY = process.env.API_KEY
const DATAROBOT_KEY = process.env.DATAROBOT_KEY

async function makePrediction(){

    let payload_to_predict = JSON.parse(fs.readFileSync('../common/payload_to_predict.json').toString())

    let { statusCode, body } = await got.post(PREDICTION_ENDPOINT, {
        headers: {
            "Authorization": `Bearer ${API_KEY}`,
            "datarobot-key": DATAROBOT_KEY
        },
        json: payload_to_predict,
        responseType: 'json'
    })

    console.log(`Response status: ${statusCode}`)
    console.log(body)
}

makePrediction()
