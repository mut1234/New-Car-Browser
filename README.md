Vehicle Browser – NHTSA (API Only)

A small .NET 8 Web API that lets clients:

List all car makes (with optional search/sort/paging)

List vehicle types for a given make

List models for a given make + year (optional vehicle type, with search/sort/paging)

All data is retrieved from the public NHTSA VPIC API.

Features

3 endpoints only (no UI)

MediatR for CQRS-style handlers

Typed HttpClient + options binding

Result<> pattern for safe error handling

Runs locally with Swagger (no Docker)

Prerequisites

.NET 8 SDK

(Optional) Postman / curl for testing

Internet access (calls NHTSA API)
