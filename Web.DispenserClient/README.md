# Dispenser Client
Client to the dispenser service, running on the Raspberry PI

### Enpoints
#### Get Printer Status
/printer/status - GET
Gets the status of the printer {NO_CONNECTION, IDLE, PRINTING}

#### Initialize Print Job
/printer/job - POST
QUERY: ?jobId 
BODY: List of ODFTablets

Returns the ETA in yyyy-MM-ddThh-MM-ss format

Initializes the print job with the specified JobId

#### Start Print Job
/printer/job/start - GET
QUERY ?jobId

Starts the specified Print Job

#### Unlock Dispenser
/dispenser/unlock - GET