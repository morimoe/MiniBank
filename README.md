# MiniBank – Banking Transaction Simulator

Educational project. No real banking data, no real financial transactions.

MiniBank is a training project for practicing full-stack web development: a React + TypeScript frontend talking to an ASP.NET Core Web API backend, with a relational database accessed through Entity Framework Core.

## Project purpose

MiniBank simulates a simplified online banking application. An authenticated user can:

- view their profile, accounts and balances
- consult their transaction history
- perform a fictional transfer to another account

The project does not connect to real banking systems, does not process real money, and does not use real customer data. It is for learning purposes only.

## Tech stack

| Layer | Technology |
| --- | --- |
| Frontend | React + TypeScript (Vite), React Router |
| Backend | C# + ASP.NET Core Web API |
| Data access | Entity Framework Core |
| Database | SQLite |
| Auth | JWT (JSON Web Tokens), BCrypt password hashing |
| API docs | Swagger / OpenAPI (Swashbuckle.AspNetCore) |
| Version control | Git |

## Architecture

The backend follows a layered architecture (API → BusinessLogic → DataAccess) with ports/adapters (hexagonal-style) inside the business layer: `BusinessLogic` defines repository interfaces ("ports"), and `DataAccess` provides the concrete implementations ("adapters"), wired together via dependency injection in `Program.cs`.

```
React + TypeScript Frontend
      ↓
  HTTP / JSON  (SPA proxy during development)
      ↓
ASP.NET Core Web API   (MiniBank.API)
      ↓
Business logic layer   (MiniBank.BusinessLogic)   — services, DTOs, ports (interfaces), entities
      ↓
Data access layer      (MiniBank.DataAccess)      — EF Core DbContext, repositories (adapters), migrations
      ↓
SQLite
```

During development, the ASP.NET Core server proxies any non-`/api`, non-`/swagger` request to the Vite dev server, so the whole app is reachable from a single origin. In production, the frontend is built as static files and served directly by ASP.NET Core.

## Project structure

```
MiniBank/
├── MiniBank.API/
│   ├── Controllers/            # Auth, User, Account, Transaction controllers
│   ├── ClientApp/              # React + TypeScript frontend (Vite)
│   │   └── src/
│   │       ├── pages/          # LoginPage, DashboardPage, TransferPage
│   │       ├── components/     # Header, ProtectedRoute, AnonymousRoute
│   │       ├── context/        # AuthContext (JWT token, login/logout)
│   │       ├── functions/      # API calls (authApi, accountApi, userApi, transactionApi)
│   │       └── types/          # Shared TypeScript interfaces (mirroring backend DTOs)
│   └── Program.cs
├── MiniBank.BusinessLogic/     # Services, DTOs, Ports (interfaces), Entities
├── MiniBank.DataAccess/        # EF Core DbContext, Adapters (repositories), Migrations
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
| POST | `/api/auth/login` | Authenticate with email **or** username + password, returns a JWT token | No |
| GET | `/api/user/me` | Get the authenticated user's profile | Yes |
| GET | `/api/account` | List the authenticated user's accounts | Yes |
| GET | `/api/account/{id}` | Get details of a specific account (owner only) | Yes |
| GET | `/api/transaction?accountNumber={accountNumber}` | Get transaction history for a specific account (owner only) | Yes |
| POST | `/api/transaction/transfer` | Transfer money to another account by account number | Yes |

`POST /api/auth/login` body:

```json
{
  "identifier": "user@minibank.com",
  "password": "..."
}
```

`identifier` accepts either the user's email or their username (name).

`POST /api/transaction/transfer` body:

```json
{
  "fromAccountNumber": "MB-000001",
  "toAccountNumber": "MB-000002",
  "amount": 100,
  "description": "optional note"
}
```

Accounts are always referenced by their account number, never by their internal database Id, so the frontend and API consumers never need to know or guess internal Ids.

Full interactive documentation is available via Swagger UI once the API is running (see below).

## Prerequisites

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/) and npm
- Git

## Getting started

### 1. Clone the repository

```
git clone https://github.com/morimoe/MiniBank.git
cd MiniBank
```

### 2. Set up the database

```
cd MiniBank.DataAccess
dotnet ef database update --startup-project ../MiniBank.API
```

> If `dotnet ef` is not recognized, install the tool once with: `dotnet tool install --global dotnet-ef`

### 3. Install frontend dependencies

```
cd ../MiniBank.API/ClientApp
npm install
```

### 4. Run the app

Start the frontend dev server first (in `MiniBank.API/ClientApp`):

```
npm run dev
```

Then, in a separate terminal, start the backend (from `MiniBank.API`):

```
dotnet run
```

Open the app at:

- `https://localhost:7132` — the app itself (proxies to the React dev server automatically)
- `https://localhost:7132/swagger` — Swagger UI, for testing the API directly

## Test users

| Email | Username | Password | Notes |
| --- | --- | --- | --- |
| `test1@minibank.com` | `Test User1` | `1234` | Has 2 accounts (`MB-000001`, `MB-000002`), sample transactions |
| `test2@minibank.com` | `Test User2` | `1234` | Has 1 account (`MB-000003`), sample transactions |

## Generating password hashes for seed data

Passwords in the database are hashed with BCrypt and can never be reversed back into plain text — the hash stored via `HasData(...)` in `MiniBankDbContext.OnModelCreating` only allows *verifying* a password at login time, not recovering the original value from it.

To generate a new hash (for example, to add another seed user), temporarily add this line near the top of `Program.cs`, run the project once to print the hash to the console, then remove the line:

```csharp
Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("your-password-here"));
```

Copy the printed hash into the `PasswordHash` field of the new `User` entry in `MiniBankDbContext.OnModelCreating`, then create and apply a new EF Core migration as usual:

```
dotnet ef migrations add AddNewSeedUser --startup-project ../MiniBank.API
dotnet ef database update --startup-project ../MiniBank.API
```

## Security notes

- Only authenticated users can access account data (JWT required via the `Authorization: Bearer <token>` header).
- A user cannot access another user's account, even by guessing/changing an account number or account id in the request — ownership is checked against the authenticated user's id from the token, not from client-supplied input.
- Passwords are hashed with BCrypt, never stored in plain text.
- Login accepts email or username but never reveals which one (or whether the account exists at all) on failure — the same generic error message is returned either way.
- All transfer input is validated (positive amount, sufficient balance, valid destination account, no self-transfer to the same account).

## Testing

Testing performed manually via the frontend and Swagger UI. Scenarios covered:

| Scenario | Expected result |
| --- | --- |
| Login with valid credentials (email or username) | User is authenticated, receives a JWT token |
| Login with invalid credentials | Authentication is rejected |
| Valid transfer | Balances and transaction history are updated |
| Transfer with insufficient balance | Transfer is rejected |
| Zero / negative amount | Transfer is rejected |
| Transfer to the same account | Transfer is rejected |
| Access another user's account | Access is rejected |
| Access without authentication | Access is rejected (401), and the frontend redirects to the login page |
| Direct URL access to a protected page while logged out | Redirected to `/login` |

## Mandatory vs optional scope

**Implemented (mandatory):** React + TypeScript frontend (login, dashboard, transfer), JWT authentication with email/username login, account & balance view, transaction history grouped by account, money transfer with validation, ownership-based authorization, protected routes on the frontend.

**Possible future improvements (optional, from the assignment's extension list):**

- Transaction search and filtering (by date, amount, status, counterparty)
- Pagination for transaction history
- Further responsive UI polish for smaller screens
- Automated unit tests (currently testing is manual only, see above)
- Simple audit log (tracking who did what and when)
- Minimal administration page
- Docker setup for easier deployment
- Support for multiple currencies (currently all accounts use MDL)

**Out of scope:** real bank integrations, real transactions, real customer data, production systems, card processing, full banking system, fraud detection, MFA/CI-CD/cloud infra.

## License

Educational project — for training purposes only. Not intended for production use.
