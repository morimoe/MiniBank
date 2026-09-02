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
|---|---|
| Frontend | React + TypeScript |
| Backend | C# + ASP.NET Core Web API |
| Data access | Entity Framework Core |
| Database | SQLite (local/dev) — can be switched to PostgreSQL |
| API docs | Swagger / OpenAPI (Swashbuckle.AspNetCore) |
| Version control | Git |

## Architecture

```
React Frontend
      ↓
  HTTP / JSON
      ↓
ASP.NET Core Web API  (MiniBank.API)
      ↓
Business logic layer  (MiniBank.BusinessLogic)
      ↓
Data access layer / EF Core (MiniBank.DataAccess)
      ↓
SQLite / PostgreSQL
```

## Project structure

```
MiniBank/
├── MiniBank.API/              # ASP.NET Core Web API (controllers, Program.cs, Swagger config)
├── MiniBank.BusinessLogic/    # Services, validation, transfer logic
├── MiniBank.DataAccess/       # EF Core DbContext, entities, migrations
├── MiniBank/                  # React + TypeScript frontend
├── MiniBank.sln
└── README.md
```

## Data model

| Entity | Fields |
|---|---|
| **User** | Id, Name, Email, PasswordHash |
| **Account** | Id, UserId, AccountNumber, Currency, Balance |
| **Transaction** | Id, FromAccountId, ToAccountId, Amount, Currency, CreatedAt, Status, Description |

A user can have one or more accounts; an account can participate in multiple transactions.

## API endpoints

| Method | Endpoint | Description | Auth required |
|---|---|---|---|
| POST | `/api/auth/login` | Authenticate a user, returns a token | No |
| GET | `/api/accounts` | List the authenticated user's accounts | Yes |
| GET | `/api/accounts/{id}` | Get details of a specific account (owner only) | Yes |
| GET | `/api/transactions` | Get transaction history for the user's account(s) | Yes |
| POST | `/api/transactions/transfer` | Perform a transfer between accounts | Yes |

Full interactive documentation is available via Swagger UI once the API is running (see below).

## Prerequisites

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/) and npm
- Git

## Getting started

### 1. Clone the repository

```bash
git clone https://github.com/morimoe/MiniBank.git
cd MiniBank
```

### 2. Run the backend (MiniBank.API)

```bash
cd MiniBank.API
dotnet restore
dotnet ef database update      # applies EF Core migrations, creates the SQLite DB
dotnet run
```

By default the API will be available at:
- `https://localhost:5001` (or the port shown in the console)
- Swagger UI: `https://localhost:5001/swagger`

> If `dotnet ef` is not recognized, install the tool once with:
> `dotnet tool install --global dotnet-ef`

### 3. Run the frontend (MiniBank)

```bash
cd MiniBank
npm install
npm run dev
```

The frontend will be available at `http://localhost:5173` (Vite default) or `http://localhost:3000`, depending on the setup. Make sure the API base URL in the frontend `.env` file points to the backend address above.

## Test users

(Replace with your actual seeded users once implemented.)

| Email | Password | Notes |
|---|---|---|
| `alice@minibank.local` | `Test123!` | Has 1 account, sample transactions |
| `bob@minibank.local` | `Test123!` | Has 1 account, sample transactions |

## Security notes

- Only authenticated users can access account data.
- A user cannot access another user's account, even by guessing/changing an account ID in the request.
- Passwords are hashed, never stored in plain text.
- All transfer input is validated (positive amount, sufficient balance, valid destination account).

## Testing

Minimum scenarios covered:

| Scenario | Expected result |
|---|---|
| Login with valid credentials | User is authenticated |
| Login with invalid credentials | Authentication is rejected |
| Valid transfer | Balances and transaction history are updated |
| Transfer with insufficient balance | Transfer is rejected |
| Zero / negative amount | Transfer is rejected |
| Access another user's account | Access is rejected |
| Access without authentication | Access is rejected |

Run backend tests with:

```bash
dotnet test
```

## Mandatory vs optional scope

**Mandatory** (implemented): authentication, dashboard, account & balance view, transaction history, fictional transfer, input validation, basic authorization, Git history, README + docs, basic tests.

**Optional / if time allows**: transaction search & filtering, pagination, responsive UI polish, extra unit tests, simple audit log, minimal admin page, Docker, multi-currency support.

**Out of scope**: real bank integrations, real transactions, real customer data, production systems, card processing, full banking system, fraud detection, MFA/CI-CD/cloud infra.

## License

Educational project — for training purposes only. Not intended for production use.
