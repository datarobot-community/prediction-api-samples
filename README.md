# DataRobot Prediction API Samples

Samples of DataRobot prediction API calls in various programming languages, to help you get started using our APIs.

To run you will require access to DataRobot application.

## Setup

Before running any scripts you **MUST** create and populate your environment variable. Each language specific script will look for it in `common/.env`.

1. Create a file `common/.env`
2. Populate the file with the following:

```
PREDICTION_SERVER=YOUR_PREDICTION_SERVER_ENDPOINT
DEPLOYMENT_ID=YOUR_DEPLOYMENT_ID
DATAROBOT_KEY=YOUR_DATAROBOT_KEY
API_KEY=YOUR_API_KEY
```

You can find these in deployments / integrations tab in your DataRobot application.

## Usage

This repository contains language-specific examples in `python`, `node`, `ruby`, `go`, and `java` directories. 
Common data is in `common` directory.

Follow the steps in **Setup**, and in each language specific directory's README file.

## Development and Contributing

If you'd like to report an issue or bug, suggest improvements, or contribute code to this project, please refer to [CONTRIBUTING.md](CONTRIBUTING.md).


# Code of Conduct

This project has adopted the Contributor Covenant for its Code of Conduct. 
See [CODE_OF_CONDUCT.md](CODE_OF_CONDUCT.md) to read it in full.

# License

Licensed under the Apache License 2.0. 
See [LICENSE](LICENSE) to read it in full.


