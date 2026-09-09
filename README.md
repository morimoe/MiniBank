# MiniBank – Banking Transaction Simulator

Educational project. No real banking data, no real financial transactions.

MiniBank is a training project for practicing full-stack web development: a React + TypeScript frontend talking to an ASP.NET Core Web API backend, with a relational database accessed through Entity Framework Core.

## Project purpose

MiniBank simulates a simplified online banking application. An authenticated user can:

- view their account and balance
- consult their transaction history
- perform a fictional transfer to another account

The project does not connect to real banking systems, does not process real money, and does not use real customer data. It is for learning purposes only.

## Tech stack

| Layer | Technology |
| --- | --- |
| Frontend | React + TypeScript (planned, not yet implemented) |
| Backend | C# + ASP.NET Core Web API |
| Data access | Entity Framework Core |
| Database | SQLite |
| Auth | JWT (JSON Web Tokens), BCrypt password hashing |
| API docs | Swagger / OpenAPI (Swashbuckle.AspNetCore) |
| Version control | Git |

## Architecture

```
React Frontend (planned)
      ↓
  HTTP / JSON
      ↓
ASP.NET Core Web API  (MiniBank.API)
      ↓
Business logic layer  (MiniBank.BusinessLogic)
      ↓
Data access layer / EF Core (MiniBank.DataAccess)
      ↓
SQLite
```

## Project structure

```
MiniBank/
├── MiniBank.API/              # ASP.NET Core Web API (controllers, Program.cs, Swagger config)
├── MiniBank.BusinessLogic/    # Services, DTOs, ports (interfaces), transfer logic
├── MiniBank.DataAccess/       # EF Core DbContext, entities, migrations
├── MiniBank/                  # Solution folder (frontend planned, not yet implemented)
├── MiniBank.sln
└── README.md
```

## Data model

| Entity | Fields |
| --- | --- |
| **User** | Id, Name, Email, PasswordHash |
| **Account** | Id, UserId, AccountNumber, Currency, Balance |
| **Transaction** | Id, FromAccountId, ToAccountId, Amount, Currency, CreatedAt, Status, Description |

A user can have one or more accounts; an account can participate in multiple transactions.

## API endpoints

| Method | Endpoint | Description | Auth required |
| --- | --- | --- | --- |
| POST | `/api/auth/login` | Authenticate a user, returns a JWT token | No |
| GET | `/api/users/me` | Get the authenticated user's profile | Yes |
| GET | `/api/accounts` | List the authenticated user's accounts | Yes |
| GET | `/api/accounts/{id}` | Get details of a specific account (owner only) | Yes |
| GET | `/api/transactions?accountId={id}` | Get transaction history for a specific account (owner only) | Yes |
| POST | `/api/transactions/transfer` | Transfer money to another account by account number | Yes |

`POST /api/transactions/transfer` body:

```json
{
  "fromAccountId": 1,
  "toAccountNumber": "MB-000002",
  "amount": 100,
  "description": "optional note"
}
```

Full interactive documentation is available via Swagger UI once the API is running (see below).

## Prerequisites

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- Git

## Getting started

### 1. Clone the repository

```
git clone https://github.com/morimoe/MiniBank.git
cd MiniBank
```

### 2. Run the backend

```
# Apply migrations (run from the DataAccess project, pointing at the API as startup project)
cd MiniBank.DataAccess
dotnet ef database update --startup-project ../MiniBank.API

# Run the API
cd ../MiniBank.API
dotnet run
```

By default the API will be available at:

- `https://localhost:7132` (or the port shown in the console)
- Swagger UI: `https://localhost:7132/swagger`

> If `dotnet ef` is not recognized, install the tool once with: `dotnet tool install --global dotnet-ef`

### 3. Frontend

The React + TypeScript frontend is planned but not yet implemented. Currently the API can be tested directly via Swagger UI.

## Test users

| Email | Password | Notes |
| --- | --- | --- |
| `TODO` | `TODO` | Has 2 accounts, sample transactions |
| `TODO` | `TODO` | Has 1 account, sample transactions |

## Security notes

- Only authenticated users can access account data.
- A user cannot access another user's account, even by guessing/changing an account ID in the request.
- Passwords are hashed (BCrypt), never stored in plain text.
- All transfer input is validated (positive amount, sufficient balance, valid destination account, no self-transfer to the same account).

## Testing

Testing performed manually via Swagger UI. Scenarios covered:

| Scenario | Expected result |
| --- | --- |
| Login with valid credentials | User is authenticated, receives a JWT token |
| Login with invalid credentials | Authentication is rejected |
| Valid transfer | Balances and transaction history are updated |
| Transfer with insufficient balance | Transfer is rejected |
| Zero / negative amount | Transfer is rejected |
| Access another user's account | Access is rejected (404, to avoid leaking existence) |
| Access without authentication | Access is rejected (401) |

## Mandatory vs optional scope

**Implemented:** authentication (JWT), account & balance view, transaction history, money transfer with validation, ownership-based authorization.

**Not yet implemented:** React frontend (dashboard, login page, transfer UI), automated tests.

**Out of scope:** real bank integrations, real transactions, real customer data, production systems, card processing, full banking system, fraud detection, MFA/CI-CD/cloud infra.

## License

Educational project — for training purposes only. Not intended for production use.
