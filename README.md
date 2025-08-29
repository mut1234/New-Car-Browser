🚗 Vehicle Browser – NHTSA (API Only)

A small .NET 8 Web API that allows clients to explore car data from the official NHTSA VPIC API
:

List all car makes (with optional search, sort, and paging)

List vehicle types for a given make

List models for a given make + year (with optional vehicle type, search, sort, and paging)

✨ Features

3 endpoints only (no frontend UI)

MediatR for clean CQRS-style handlers

Typed HttpClient + options binding for external API calls

Result<> pattern for safe error handling

Runs locally with Swagger UI (no Docker required)

📦 Prerequisites

.NET 8 SDK

(Optional) Postman
 or curl for manual testing

Internet access (to call the NHTSA VPIC API)
