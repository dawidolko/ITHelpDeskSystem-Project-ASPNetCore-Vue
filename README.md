# ITHelpDeskSystem-Project-ASPNetCore-Vue 

> 🚀 **Modern IT Help Desk Ticketing System** - Build enterprise-grade support platforms with ASP.NET Core REST API, Vue.js SPA, and advanced ticket management

<div align="center">

![Vue.js](https://img.shields.io/badge/Vue.js-3.3-4FC08D?style=for-the-badge&logo=vue.js&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-9.0-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![MySQL](https://img.shields.io/badge/MySQL-8.0-4479A1?style=for-the-badge&logo=mysql&logoColor=white)
![TypeScript](https://img.shields.io/badge/TypeScript-5.0-3178C6?style=for-the-badge&logo=typescript&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?style=for-the-badge&logo=docker&logoColor=white)

Full-featured IT Help Desk system with REST API backend and Vue.js SPA frontend.

[Quick Start](#-quick-start) • [Docker Setup](#-docker-deployment-recommended) • [Documentation](docs/README.md) • [API Documentation (Swagger)](http://localhost:5000/swagger)

</div>

---

## 📋 Description

Welcome to the **IT Help Desk System** repository! This comprehensive ticketing solution manages technical support requests in organizations with a modern full-stack architecture. Built with ASP.NET Core 9.0 REST API backend and Vue 3 + TypeScript SPA frontend, the system provides efficient ticket lifecycle management, role-based access control, powerful search and filtering capabilities, and an intuitive user interface.

The platform features advanced SFWP (Sort, Filter, Search, Pagination) capabilities, JWT authentication, real-time dashboard statistics, public and internal comments, and responsive design. Perfect for learning modern web development practices, RESTful API design, and enterprise application architecture.

## 📁 Repository Structure

```

ITHelpDeskSystem-Project-ASPNetCore-Vue/
├── 📁 backend/ # ASP.NET Core REST API
│ ├── 📁 Controllers/
│ │ ├── 🔐 AuthController.cs # Authentication endpoints
│ │ ├── 🎫 TicketsController.cs # Ticket management
│ │ ├── 👥 UsersController.cs # User management
│ │ ├── 💬 CommentsController.cs # Comment operations
│ │ └── 📊 StatsController.cs # Statistics endpoints
│ ├── 📁 Data/
│ │ ├── 🗄️ HelpDeskContext.cs # EF Core DbContext
│ │ └── 🌱 DbSeeder.cs # Database seeding
│ ├── 📁 DTOs/
│ │ ├── AuthDtos.cs # Authentication DTOs
│ │ ├── TicketDtos.cs # Ticket DTOs
│ │ ├── UserDtos.cs # User DTOs
│ │ └── CommentDtos.cs # Comment DTOs
│ ├── 📁 Models/
│ │ ├── 🎫 Ticket.cs # Ticket entity
│ │ ├── 👤 User.cs # User entity
│ │ ├── 💬 Comment.cs # Comment entity
│ │ ├── 🏢 Department.cs # Department entity
│ │ └── 📋 Enums.cs # Status, Priority, Role enums
│ ├── 📁 Services/
│ │ ├── IAuthService.cs
│ │ ├── ITicketService.cs
│ │ └── IEmailService.cs
│ ├── 📁 Migrations/ # EF Core migrations
│ ├── ⚙️ appsettings.json # Configuration
│ ├── 🚀 Program.cs # Application entry point
│ ├── 🔧 start.sh # Linux/macOS startup script
│ └── 🔧 start.bat # Windows startup script
├── 📁 frontend/ # Vue.js SPA
│ ├── 📁 src/
│ │ ├── 📁 components/
│ │ │ ├── TicketList.vue
│ │ │ ├── TicketForm.vue
│ │ │ ├── CommentSection.vue
│ │ │ └── UserManagement.vue
│ │ ├── 📁 pages/
│ │ │ ├── 🏠 Dashboard.vue # Dashboard with stats
│ │ │ ├── 🎫 Tickets.vue # Ticket listing
│ │ │ ├── 📝 TicketDetail.vue # Ticket details
│ │ │ ├── 🔐 Login.vue # Login page
│ │ │ ├── 📝 Register.vue # Registration page
│ │ │ └── 👥 Users.vue # User management
│ │ ├── 📁 routes/
│ │ │ └── index.ts # Vue Router config
│ │ ├── 📁 services/
│ │ │ ├── api.ts # Axios instance
│ │ │ ├── authService.ts # Auth API calls
│ │ │ ├── ticketService.ts # Ticket API calls
│ │ │ └── userService.ts # User API calls
│ │ ├── 📁 stores/
│ │ │ ├── auth.ts # Pinia auth store
│ │ │ ├── tickets.ts # Pinia ticket store
│ │ │ └── users.ts # Pinia user store
│ │ ├── 📁 types/
│ │ │ ├── ticket.ts # TypeScript interfaces
│ │ │ ├── user.ts
│ │ │ └── api.ts
│ │ ├── 💻 App.vue # Root component
│ │ └── 🚀 main.ts # Vue entry point
│ ├── 📦 package.json # Node.js dependencies
│ ├── ⚙️ vite.config.ts # Vite configuration
│ ├── 🎨 tailwind.config.js # Tailwind CSS config
│ ├── 📝 tsconfig.json # TypeScript config
│ ├── 🔧 start.sh # Linux/macOS startup script
│ └── 🔧 start.bat # Windows startup script
├── 📁 docs/ # Project documentation
│ ├── 📖 README.md # Detailed documentation
│ ├── 📊 API.md # API reference
│ └── 🔧 SETUP.md # Setup guide
├── 📁 .tools/ # Development tools
│ └── 📁 docker/ # Docker deployment
│     ├── 🐳 docker-compose.yml # Docker orchestration
│     ├── 📋 Dockerfile.backend # Backend container
│     ├── 📋 Dockerfile.frontend # Frontend container
│     ├── ⚙️ .env.example # Environment variables
│     └── 📖 README.md # Docker documentation
└── 📖 README.md # Main documentation

```

## 🚀 Quick Start

### Backend Setup

#### Linux/macOS:

```bash
cd backend
./start.sh
```

#### Windows:

```bash
cd backend
start.bat
```

#### Manual Setup:

```bash
cd backend
dotnet tool install --global dotnet-ef
dotnet restore
dotnet ef database update
dotnet run
```

### Frontend Setup

#### Linux/macOS:

```bash
cd frontend
./start.sh
```

#### Windows:

```bash
cd frontend
start.bat
```

#### Manual Setup:

```bash
cd frontend
npm install
npm run dev
```

### Access URLs

- **Frontend (SPA):** [http://localhost:5173](http://localhost:5173)
- **Backend API:** [http://localhost:5000](http://localhost:5000)
- **Swagger UI:** [http://localhost:5000/swagger](http://localhost:5000/swagger)

## 🐳 Docker Deployment (Recommended)

The easiest way to run the entire application stack (database, backend, and frontend) is using Docker:

```bash
cd .tools/docker
docker-compose up -d
```

**Access the application:**

- **Frontend:** [http://localhost:8080](http://localhost:8080)
- **Backend API:** [http://localhost:5001](http://localhost:5001)
- **Swagger UI:** [http://localhost:5001/swagger](http://localhost:5001/swagger)

**📚 Full Docker documentation and troubleshooting guide:** [.tools/docker/README.md](.tools/docker/README.md)

**Docker features:**

- ✅ One command setup - no manual installation needed
- ✅ Isolated environment - no conflicts with system packages
- ✅ Automatic database migrations
- ✅ Pre-configured networking between services
- ✅ Health checks for all services
- ✅ Easy cleanup and restart

**Requirements:** Docker 20.10+ and Docker Compose 2.0+

---

## ⚙️ System Requirements

### **Essential Tools:**

- **.NET SDK** 9.0 or higher
- **Node.js** 18+ (20+ recommended)
- **npm** 9+ (10+ recommended)
- **MySQL** 8.0 or higher
- **Git** for version control

### **Installation by Platform:**

#### macOS (Homebrew):

```bash
brew install --cask dotnet-sdk
brew install node
brew install mysql
```

#### Windows:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/)
- [MySQL](https://dev.mysql.com/downloads/installer/)

#### Linux (Ubuntu/Debian):

```bash
wget https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y dotnet-sdk-9.0 nodejs mysql-server
```

### **Development Environment:**

- **IDE:** Visual Studio 2022, VS Code, Rider
- **Database Tool:** MySQL Workbench, DBeaver, phpMyAdmin
- **API Testing:** Postman, Insomnia, or Swagger UI
- **Browser DevTools** for frontend debugging

### **Recommended Extensions:**

- **VS Code:**
  - C# Dev Kit
  - Volar (Vue 3 support)
  - ESLint
  - Prettier
  - MySQL extension

## ✨ Key Features

### **🎫 Ticket Management**

- **Full CRUD Operations** for tickets
- **Assignment System:**
  - Manual assignment to technicians
  - Auto-assignment ready
- **Ticket Statuses:** New, Open, InProgress, Resolved, Closed
- **Priority Levels:** Low, Medium, High, Critical
- **Categories:** Hardware, Software, Network, Access, Email, Other
- **Due Date Management** with overdue detection
- **Ticket History** and audit trail

### **🔍 Advanced Search & Filtering (SFWP)**

- **Full-Text Search:** Title, description, user names
- **Multi-Criteria Filtering:**
  - Status (New, Open, InProgress, Resolved, Closed)
  - Priority (Low, Medium, High, Critical)
  - Category (Hardware, Software, Network, etc.)
  - Assigned technician
  - Overdue status
  - Date ranges
- **Flexible Sorting:** Sort by any field (title, status, priority, createdAt, updatedAt)
- **Smart Pagination:** Page size control, total pages/count

### **🔐 Authentication & Authorization**

- **JWT-Based Authentication** with secure token management
- **Role-Based Access Control (RBAC):**
  - **User:** Create tickets, view own tickets, add public comments
  - **Technician:** View all tickets, update status, add internal notes
  - **Admin:** Full system access, user management, role changes
- **Public Registration** (User role by default)
- **Secure Password Hashing** with BCrypt
- **Token Refresh** mechanism

### **👥 User Management**

- **User Profiles** with department assignment
- **Admin Panel** for user administration
- **Role Management:** Change user roles (Admin only)
- **Technician Directory** for ticket assignment
- **Department Organization** for better structure
- **User Activity Tracking**

### **💬 Comment System**

- **Public Comments:** Visible to all users involved
- **Internal Notes:** Technician and Admin only
- **Chronological Timeline** of all interactions
- **Rich Text Support** for formatted comments
- **Comment Notifications** (email integration ready)

### **📊 Dashboard & Statistics**

- **Key Metrics Display:**
  - Total tickets by status
  - Average resolution time
  - Tickets by priority distribution
  - Category breakdown
  - Resolution rate percentage
- **Visual Charts** with data visualization
- **Real-Time Updates** via API polling
- **Customizable Date Ranges** for statistics

### **🎨 Modern User Interface**

- **Responsive Design** with TailwindCSS
- **Dark Mode** support (optional)
- **Intuitive Navigation** with Vue Router
- **Loading States** and error handling
- **Toast Notifications** for user feedback
- **Mobile-First** approach

### **⚡ Performance & Development**

- **Hot Module Replacement (HMR)** with Vite
- **Fast Backend** with ASP.NET Core 9.0
- **Optimized Database Queries** with EF Core
- **Lazy Loading** for frontend components
- **API Response Caching** where appropriate

## 🛠️ Technologies Used

### **Backend Stack:**

- **ASP.NET Core 9.0** - Modern web framework
- **Entity Framework Core 8.0** - ORM for database access
- **MySQL 8.0** - Relational database (Pomelo provider)
- **JWT Authentication** - Secure token-based auth
- **Swashbuckle/Swagger** - API documentation
- **AutoMapper** - Object-object mapping
- **FluentValidation** - Input validation

### **Frontend Stack:**

- **Vue 3.3** - Progressive JavaScript framework
- **TypeScript 5.0** - Type-safe JavaScript
- **Pinia** - State management
- **Vue Router** - Client-side routing
- **Axios** - HTTP client
- **TailwindCSS** - Utility-first CSS framework
- **Vite** - Next generation build tool

## 🔧 Configuration

### 1. Database Setup

Create MySQL database and user:

```sql
CREATE DATABASE helpdesk_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
CREATE USER 'helpdesk_user'@'localhost' IDENTIFIED BY 'HelpDesk2024!';
GRANT ALL PRIVILEGES ON helpdesk_db.* TO 'helpdesk_user'@'localhost';
FLUSH PRIVILEGES;
```

### 2. Backend Configuration

Edit `backend/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=helpdesk_db;User=helpdesk_user;Password=HelpDesk2024!;Port=3306;"
  },
  "Jwt": {
    "Key": "your-secret-key-min-32-characters",
    "Issuer": "HelpDeskAPI",
    "Audience": "HelpDeskClient",
    "ExpiryMinutes": 60
  }
}
```

### 3. Frontend Configuration

Create/edit `frontend/.env`:

```env
VITE_API_URL=http://localhost:5000/api
```

### 4. CORS Configuration (Optional)

In `backend/Program.cs`, add additional origins if needed:

```csharp
policy.WithOrigins(
    "http://localhost:5173",
    "http://localhost:5174",
    "http://localhost:8080"
);
```

## 💻 Development

### Backend Development Commands

```bash
cd backend

# Run with hot reload
dotnet watch run

# Entity Framework migrations
dotnet ef migrations add <MigrationName>
dotnet ef database update
dotnet ef migrations remove
dotnet ef migrations script

# Build for production
dotnet build --configuration Release
dotnet publish --configuration Release
```

### Frontend Development Commands

```bash
cd frontend

# Development server with HMR
npm run dev

# Build for production
npm run build

# Preview production build
npm run preview

# Lint code
npm run lint

# Type check
npm run type-check
```

### API Testing Examples

Using curl:

```bash
# Get tickets with filtering
curl "http://localhost:5000/api/tickets?status=Open&priority=High&sortBy=createdAt&sortOrder=desc"

# Login
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@firma.pl","password":"Admin123!"}'
```

## 🔌 API Overview

### Authentication Endpoints (Public)

- `POST /api/auth/register` - Register new user (User role by default)
- `POST /api/auth/login` - Login and receive JWT token
- `POST /api/auth/refresh` - Refresh access token

### Ticket Endpoints (Requires Auth)

- `GET /api/tickets` - List tickets with SFWP parameters
- `GET /api/tickets/{id}` - Get ticket details
- `POST /api/tickets` - Create new ticket
- `PUT /api/tickets/{id}` - Update ticket
- `DELETE /api/tickets/{id}` - Delete ticket (Admin only)
- `GET /api/tickets/statistics` - Get dashboard statistics

### Comment Endpoints (Requires Auth)

- `GET /api/tickets/{id}/comments` - Get ticket comments
- `POST /api/tickets/{id}/comments` - Add comment to ticket

### User Endpoints (Requires Auth)

- `GET /api/users` - List users (Admin/Technician)
- `GET /api/users/technicians` - Get technician list
- `GET /api/users/{id}` - Get user details
- `PUT /api/users/{id}/role` - Change user role (Admin only)
- `DELETE /api/users/{id}` - Delete user (Admin only)

### SFWP Query Parameters

- `sortBy` - Field to sort by (title, status, priority, createdAt, updatedAt)
- `sortOrder` - Sort direction (asc, desc)
- `status` - Filter by status
- `priority` - Filter by priority
- `category` - Filter by category
- `assignedToId` - Filter by assigned technician
- `isOverdue` - Filter overdue tickets
- `searchTerm` - Full-text search
- `page` - Page number (default: 1)
- `pageSize` - Items per page (default: 10)

## 📊 Sample Data

The system includes comprehensive seed data for testing:

### Demo Users (18 total)

| Email           | Password  | Role       | Description             |
| --------------- | --------- | ---------- | ----------------------- |
| admin@firma.pl  | Admin123! | Admin      | Primary administrator   |
| admin1@firma.pl | Admin123! | Admin      | Secondary administrator |
| tech@firma.pl   | Tech123!  | Technician | Lead technician         |
| tech1@firma.pl  | Tech123!  | Technician | Support technician      |
| user@firma.pl   | User123!  | User       | Regular user            |
| user1@firma.pl  | User123!  | User       | Test user               |

_Additional users (3 Admins, 5 Technicians, 10 Users) are also seeded._

### Sample Tickets (20 total)

- Varied statuses (New, Open, InProgress, Resolved, Closed)
- Multiple priorities (Low, Medium, High, Critical)
- Different categories (Hardware, Software, Network, Access, Email, Other)
- Some tickets with due dates and overdue status
- Assigned to different technicians

### Sample Comments (15+ total)

- Mix of public and internal comments
- Chronological timeline on tickets
- Demonstrates comment threading

## 🧰 Troubleshooting

### Backend Issues

**MySQL Connection Failed:**

```bash
# Check if MySQL is running
# macOS:
brew services list
brew services start mysql

# Linux:
sudo systemctl status mysql
sudo systemctl start mysql

# Test connection:
mysql -u helpdesk_user -p -h localhost
```

**Entity Framework Issues:**

```bash
# Install EF tools
dotnet tool install --global dotnet-ef

# Check version
dotnet ef --version

# Recreate migrations
dotnet ef migrations add InitialCreate --force
dotnet ef database update

# Reset database
dotnet ef database drop
dotnet ef database update
```

### Frontend Issues

**CORS Errors:**

- Verify backend CORS configuration allows frontend origin
- Check browser DevTools Console for actual origin
- Hard refresh browser (Cmd+Shift+R or Ctrl+Shift+R)

**API Connection Failed:**

- Verify `frontend/.env` has correct `VITE_API_URL`
- Check backend is running: `curl http://localhost:5000/api/tickets`
- Ensure both servers are on http:// during development

**TypeScript Errors:**

```bash
# Clean and reinstall
rm -rf node_modules package-lock.json
npm cache clean --force
npm install

# Type check
npx tsc --noEmit
```

### Database Issues

**Empty Database:**

- Ensure seeding is called in `Program.cs`
- Check `DbSeeder.cs` runs successfully
- Drop and recreate database:

```bash
dotnet ef database drop --force
dotnet ef database update
```

**Migration Conflicts:**

```bash
# Remove all migrations
dotnet ef migrations remove

# Create fresh migration
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## 🤝 Contributing

Contributions are highly welcomed! Here's how you can help:

- 🐛 **Report bugs** - Found an issue? Let us know!
- 💡 **Suggest improvements** - Have ideas for better features?
- 🔧 **Submit pull requests** - Share your enhancements and solutions
- 📖 **Improve documentation** - Help make the project clearer

Feel free to open issues or reach out through GitHub for any questions or suggestions.

## 👨‍💻 Author

Created by **Dawid Olko** - Part of the full-stack web development series.

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

---
