The ServiceCloud is a prototype of a custom Service Host with many WCF services.  Each of these services is accessible on their own.  There exists a Gateway service that accepts requests to call any of the ICloudService(s) in a specific order.  This allows you call many WCF services with 1 client call.  

This project is experimental and subject to change.  It is merely offered as a prototype and proof-of-concept for the "Service Cloud".

The subsequent calls to other services are NOT currently recursive (although they could be very easily).  The services do not have to live within the one Service Host included in the project.  The Cloud of Services could exist on many different machines on many different environments, so long as they all implement the ICloudService interface.

- Mark Kockerbeck
http://www.kockerbeck.com/