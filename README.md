# IT Help Desk System

<div align="center">

![Vue.js](https://img.shields.io/badge/Vue.js-3.3-4FC08D?style=for-the-badge&logo=vue.js&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-9.0-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![MySQL](https://img.shields.io/badge/MySQL-8.0-4479A1?style=for-the-badge&logo=mysql&logoColor=white)
![TypeScript](https://img.shields.io/badge/TypeScript-5.0-3178C6?style=for-the-badge&logo=typescript&logoColor=white)

Modern IT Help Desk system with a REST API backend and Vue.js SPA frontend.

[Quick Start](#-quick-start) • [Docs](docs/README.md) • [API (Swagger)](http://localhost:5000/swagger)

</div>

---

## Table of contents

- About
- Features
- Tech stack
- Quick start
- Project structure
- Requirements
- Configuration
- Development
- API overview
- Sample data
- Troubleshooting
- License

---

## 🎯 About

The IT Help Desk System is a full-featured ticketing solution for managing technical requests in an organization. It consists of:

- ASP.NET Core 9.0 REST API (backend)
- Vue 3 + TypeScript SPA (frontend)
- MySQL 8.0 database

It provides efficient ticket lifecycle management, role-based access, powerful search/filter/sort/pagination, and an intuitive UI.

### Highlights

- Full REST API + SPA architecture
- SFWP: Sort, Filter, Search, Pagination
- Role-based access control (Admin, Technician, User)
- Dashboard with statistics
- Public and internal comments
- Responsive UI, HMR for fast development
- Interactive Swagger/OpenAPI docs

---

## ✨ Features

- Tickets:
  - CRUD operations
  - Assign to technicians (manual/auto-ready)
  - Statuses: New, Open, InProgress, Resolved, Closed
  - Priorities: Low, Medium, High, Critical
  - Categories: Hardware, Software, Network, Access, Email, Other
  - Due dates and overdue detection
- SFWP:
  - Full-text search (title, description, user names)
  - Multi-criteria filtering (status, priority, category, assignee, overdue)
  - Sort by any field (title, status, priority, createdAt)
  - Pagination with total pages/count
- Users and Auth:
  - JWT authentication (token-based)
  - Roles: User, Technician, Admin (granular permissions)
  - Public registration (User role by default)
  - Admin panel for user management and role changes
  - Departments for organizational grouping
- Comments:
  - Public and internal (technician-only) notes
  - Chronological timeline
- Stats:
  - Dashboard with key metrics
  - Priority and category breakdown
  - Resolution metrics (avg time to resolve, resolution rate)

---

## 🛠 Tech stack

- Backend:
  - ASP.NET Core 9.0
  - Entity Framework Core 8.0
  - MySQL 8.0 (Pomelo provider)
  - Swashbuckle/Swagger
  - CORS
- Frontend:
  - Vue 3.3 + TypeScript 5
  - Pinia (state)
  - Vue Router
  - Axios
  - TailwindCSS
  - Vite

---

## 🚀 Quick start

Below are minimal steps for macOS/Linux and Windows. See docs/README.md for more details.

### 1) Backend

macOS/Linux:

- From project root: cd backend
- Run: ./start.sh

Windows:

- From project root: cd backend
- Run: start.bat

Manual steps (alternative):

- cd backend
- dotnet tool install --global dotnet-ef
- dotnet restore
- dotnet ef database update
- dotnet run

### 2) Frontend

macOS/Linux:

- From project root: cd frontend
- Run: ./start.sh

Windows:

- From project root: cd frontend
- Run: start.bat

Manual steps (alternative):

- cd frontend
- npm install
- npm run dev

### URLs

- Frontend (SPA): http://localhost:5173
- Backend API: http://localhost:5000
- Swagger UI: http://localhost:5000/swagger

---

## 🗂 Project structure

```
ITHelpDeskSystem-Project-ASPNetCore-Vue/
├── README.md
├── backend/
│   ├── Program.cs
│   ├── appsettings.json
│   ├── start.sh / start.bat
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── TicketsController.cs
│   │   └── UsersController.cs
│   ├── Data/
│   │   ├── HelpDeskContext.cs
│   │   └── DbSeeder.cs
│   ├── DTOs/
│   │   ├── AuthDtos.cs
│   │   └── TicketDtos.cs
│   ├── Migrations/
│   └── Models/
│       ├── Ticket.cs
│       ├── User.cs
│       ├── Comment.cs
│       └── Enums.cs
├── frontend/
│   ├── start.sh / start.bat
│   ├── package.json
│   ├── vite.config.ts
│   └── src/
│       ├── main.ts
│       ├── App.vue
│       ├── components/
│       ├── pages/
│       ├── routes/
│       ├── services/
│       ├── stores/
│       └── types/
└── docs/
    └── README.md
```

---

## 📋 Requirements

- .NET SDK: 9.0+
- Node.js: 18+ (20+ recommended)
- npm: 9+ (10+ recommended)
- MySQL: 8.0+

macOS (Homebrew):

```bash
brew install --cask dotnet-sdk
brew install node
brew install mysql
```

Windows:

- .NET SDK https://dotnet.microsoft.com/download
- Node.js https://nodejs.org/
- MySQL https://dev.mysql.com/downloads/installer/

Linux (Ubuntu/Debian):

```bash
wget https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y dotnet-sdk-9.0 nodejs mysql-server
```

---

## ⚙️ Configuration

1. MySQL database (create DB and user)

```sql
CREATE DATABASE helpdesk_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
CREATE USER 'helpdesk_user'@'localhost' IDENTIFIED BY 'HelpDesk2024!';
GRANT ALL PRIVILEGES ON helpdesk_db.* TO 'helpdesk_user'@'localhost';
FLUSH PRIVILEGES;
```

2. Backend connection string

Edit `backend/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=helpdesk_db;User=helpdesk_user;Password=HelpDesk2024!;Port=3306;"
  }
}
```

3. Frontend API URL

Create/edit `frontend/.env`:

```
VITE_API_URL=http://localhost:5000/api
```

4. CORS (optional extra origins)

In `backend/Program.cs`:

```csharp
policy.WithOrigins(
    "http://localhost:5173",
    "http://localhost:5174"
);
```

---

## 🔧 Development

Backend:

```bash
cd backend
dotnet watch run            # auto-reload during development
dotnet ef migrations add <Name>
dotnet ef database update
dotnet ef migrations remove
dotnet ef migrations script
```

Frontend:

```bash
cd frontend
npm run dev
npm run build
npm run preview
npm run lint
```

API testing:

- Swagger UI: http://localhost:5000/swagger
- Example request:
  - GET http://localhost:5000/api/tickets?status=Open&priority=High&sortBy=createdAt&sortOrder=desc

---

## 🔌 API overview

Auth (public):

- POST /api/auth/register — register (role User by default)
- POST /api/auth/login — login (returns JWT)

Tickets (requires auth; visibility/actions depend on role):

- GET /api/tickets — list with SFWP
- GET /api/tickets/{id} — details
- POST /api/tickets — create
- PUT /api/tickets/{id} — update
- DELETE /api/tickets/{id} — delete (Admin only)
- GET /api/tickets/{id}/comments — comments
- POST /api/tickets/{id}/comments — add comment
- GET /api/tickets/statistics — statistics

Users (requires auth):

- GET /api/users — list (Admin/Technician)
- GET /api/users/technicians — technicians
- GET /api/users/{id} — details
- PUT /api/users/{id}/role — change role (Admin only)
- DELETE /api/users/{id} — delete user (Admin only)

SFWP parameters (tickets list):

- sortBy, sortOrder (asc/desc)
- status, priority, category, assignedToId, isOverdue
- searchTerm
- page, pageSize

---

## 📊 Sample data

On first run, the database is seeded with demo data:

- 18 users (3 Admins, 5 Technicians, 10 Users)
- 20 tickets with varied statuses/priorities/categories
- 15+ comments (public and internal)

Test accounts:

| Email           | Password  | Role       |
| --------------- | --------- | ---------- |
| admin@firma.pl  | Admin123! | Admin      |
| tech@firma.pl   | Tech123!  | Technician |
| user@firma.pl   | User123!  | User       |
| admin1@firma.pl | Admin123! | Admin      |
| tech1@firma.pl  | Tech123!  | Technician |
| user1@firma.pl  | User123!  | User       |

Public registration is enabled; new users get the User role. Admin can change roles.

---

## 🧰 Troubleshooting

Backend can’t connect to MySQL:

- Ensure MySQL is running (brew services list on macOS)
- Verify connection string in appsettings.json
- Test connection: mysql -u helpdesk_user -p -h localhost

EF migrations issues:

- Install EF tools: dotnet tool install --global dotnet-ef
- Check version: dotnet ef --version
- Recreate migration: dotnet ef migrations add InitialCreate --force
- Update DB: dotnet ef database update

CORS errors in browser:

- Ensure backend CORS allows your frontend origin/port
- Hard refresh (Cmd+Shift+R)
- Check the actual origin in DevTools Console

Frontend can’t reach API:

- Check frontend/.env: VITE_API_URL=http://localhost:5000/api
- Verify backend is running: curl http://localhost:5000/api/tickets
- Prefer http:// during local development (disable HTTPS redirection if necessary)

Empty database:

- Ensure seeding is called in Program.cs at startup
- Drop and recreate DB via EF

TypeScript errors:

- Remove node_modules and package-lock.json, npm cache clean --force, reinstall
- Validate TS config: npx tsc --noEmit

---

## � License

MIT License

<div align="center">

Built with ❤️ using ASP.NET Core and Vue.js

</div>
