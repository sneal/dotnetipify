# .NET IP-ify

Simple .NET Core app to get the public IP addresses of the client and server.

## Runing in CloudFoundry

Clone this repository onto your workstation, can be any OS with the CF CLI installed. Target your CloudFoundry API and ensure you're logged in, then run: `cf push`. The .NET core buildpack will handle restoring dependencies and building the app.

## Server IP

The Server IP is the address of the server as seen by a client on the internet. Typically this is the IP of the load balancer in front of the web server. This is gathered by hitting a public API for querying your public internet routable IP address.

## Client IP

The client IP is the address of the client or browser as seen by the web server. Typically this is the IP address of the client's entry point to the internet, for example a corporate proxy etc. This code attempts to read the client's IP using multiple methods with fallback, starting with the X-Forwarded-For header that is typically added by the load balancer sitting in front of the web server.
