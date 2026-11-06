# 📚 Dokumentacja Systemu Help Desk

<div align="center">

**Kompleksowa dokumentacja techniczna i użytkowa**

[Architektura](#-architektura) • [API](#-dokumentacja-api) • [Baza danych](#-baza-danych) • [Frontend](#-frontend) • [Deployment](#-wdrożenie)

</div>

---

## 📋 Spis treści

1. [Wprowadzenie](#wprowadzenie)
2. [Architektura](#-architektura)
3. [Dokumentacja API](#-dokumentacja-api)
4. [Baza danych](#-baza-danych)
5. [Frontend](#-frontend)
6. [Bezpieczeństwo](#-bezpieczeństwo)
7. [Wdrożenie](#-wdrożenie)
8. [Troubleshooting](#-troubleshooting)

---

## Wprowadzenie

System Zarządzania Zgłoszeniami IT to nowoczesna aplikacja webowa typu Help Desk, zaprojektowana do efektywnego zarządzania zgłoszeniami technicznymi w organizacji. System składa się z dwóch głównych komponentów:

- **Backend API** - RESTful API zbudowane na ASP.NET Core 9.0
- **Frontend SPA** - Single Page Application w Vue.js 3

### Kluczowe założenia projektowe

- **Modularność** - każdy komponent jest niezależny i łatwy do wymiany
- **Skalowalność** - architektura pozwala na łatwe skalowanie
- **Wydajność** - optymalizacja zapytań do bazy danych, lazy loading
- **Bezpieczeństwo** - walidacja danych, CORS, prepared statements
- **UX/UI** - intuicyjny interfejs, responsywność, accessibility

---

## 🏗️ Architektura

### Diagram architektury

```
┌─────────────────────────────────────────────────────────────┐
│                         Frontend (Vue.js)                    │
│  ┌──────────┐  ┌──────────┐  ┌──────────┐  ┌──────────┐   │
│  │  Pages   │  │Components│  │  Stores  │  │ Services │   │
│  └────┬─────┘  └────┬─────┘  └────┬─────┘  └────┬─────┘   │
│       │             │              │             │          │
│       └─────────────┴──────────────┴─────────────┘          │
│                          │                                   │
└──────────────────────────┼───────────────────────────────────┘
                           │ HTTP/REST
                           │ (JSON)
┌──────────────────────────┼───────────────────────────────────┐
│                          │                                   │
│                    ┌─────▼─────┐                            │
│                    │   CORS    │                            │
│                    │Middleware │                            │
│                    └─────┬─────┘                            │
│                          │                                   │
│              ┌───────────▼────────────┐                     │
│              │   Controllers Layer    │                     │
│              │  (TicketsController)   │                     │
│              └───────────┬────────────┘                     │
│                          │                                   │
│              ┌───────────▼────────────┐                     │
│              │      DTOs Layer        │                     │
│              │   (Data Validation)    │                     │
│              └───────────┬────────────┘                     │
│                          │                                   │
│              ┌───────────▼────────────┐                     │
│              │    DbContext (EF)      │                     │
│              │   (LINQ Queries)       │                     │
│              └───────────┬────────────┘                     │
│                          │                                   │
│                    Backend (ASP.NET)                         │
└──────────────────────────┼───────────────────────────────────┘
                           │ MySQL Protocol
┌──────────────────────────▼───────────────────────────────────┐
│                    MySQL Database 8.0                        │
│  ┌─────────┐  ┌─────────┐  ┌─────────┐  ┌─────────┐        │
│  │  Users  │  │ Tickets │  │Comments │  │  ...    │        │
│  └─────────┘  └─────────┘  └─────────┘  └─────────┘        │
└──────────────────────────────────────────────────────────────┘
```

### Backend - Warstwa kontrolerów

#### TicketsController

```csharp
[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    // GET /api/tickets - List with SFWP
    // GET /api/tickets/{id} - Details
    // POST /api/tickets - Create
    // PUT /api/tickets/{id} - Aktualizuj
    // DELETE /api/tickets/{id} - Delete
    // GET /api/tickets/{id}/comments - Komentarze
    // POST /api/tickets/{id}/comments - Add a comment
}
```

**Funkcjonalności SFWP:**

- **Sort** - `sortBy`, `sortOrder` (asc/desc)
- **Filter** - `status`, `priority`, `category`, `assignedToId`, `isOverdue`
- **Search** - `searchTerm` (tytuł, opis, użytkownicy)
- **Pagination** - `page`, `pageSize`

### Frontend - Architektura komponentów

```
App.vue (Root)
│
├── navigation-global.vue (Nawigacja)
│
├── Router View
│   │
│   ├── Dashboard Page
│   │   ├── Statistics Cards
│   │   ├── Charts (Priority/Category)
│   │   └── Recent Tickets List
│   │
│   ├── Tickets Page
│   │   ├── SearchInput.vue
│   │   ├── Filters (Status, Priority, Category)
│   │   ├── Tickets Table
│   │   │   └── StatusBadge.vue
│   │   └── Pagination.vue
│   │
│   ├── Ticket Detail Page
│   │   ├── Ticket Info
│   │   ├── Status/Priority Badges
│   │   ├── Edit Form
│   │   └── Comments Section
│   │
│   └── Statistics Page
│       ├── Summary Cards
│       ├── Priority Chart
│       └── Category Chart
│
└── footer-global.vue (Stopka)
```

### State Management (Pinia)

#### ticketStore.ts

```typescript
export const useTicketStore = defineStore('tickets', {
  state: () => ({
    tickets: [],
    currentTicket: null,
    filters: {
      searchTerm: '',
      status: null,
      priority: null,
      // ...
    },
    pagination: {
      currentPage: 1,
      pageSize: 10,
      totalCount: 0
    },
    isLoading: false
  }),

  actions: {
    async fetchTickets(),
    async fetchTicketById(id),
    async createTicket(data),
    async updateTicket(id, data),
    async deleteTicket(id)
  }
})
```

---

## 📡 Dokumentacja API

### Endpointy zgłoszeń

#### 1. GET /api/tickets - Lista zgłoszeń z SFWP

**Query Parameters:**

| Parametr       | Typ    | Wymagany | Opis                                                        |
| -------------- | ------ | -------- | ----------------------------------------------------------- |
| `page`         | int    | Nie      | Numer strony (domyślnie: 1)                                 |
| `pageSize`     | int    | Nie      | Rozmiar strony (domyślnie: 10)                              |
| `sortBy`       | string | Nie      | Pole sortowania (title, status, priority, createdAt)        |
| `sortOrder`    | string | Nie      | Kierunek (asc/desc)                                         |
| `searchTerm`   | string | Nie      | Wyszukiwanie pełnotekstowe                                  |
| `status`       | string | Nie      | Filtr po statusie (New, Open, InProgress, Resolved, Closed) |
| `priority`     | string | Nie      | Filtr po priorytecie (Low, Medium, High, Critical)          |
| `category`     | string | Nie      | Filtr po kategorii                                          |
| `assignedToId` | int    | Nie      | ID przypisanego technika                                    |
| `isOverdue`    | bool   | Nie      | Tylko przeterminowane                                       |

**Przykładowe zapytanie:**

```bash
GET /api/tickets?page=1&pageSize=10&status=Open&priority=High&sortBy=createdAt&sortOrder=desc
```

**Odpowiedź 200 OK:**

```json
{
  "items": [
    {
      "id": 1,
      "title": "Problem z drukarką",
      "description": "Drukarka nie drukuje w kolorze",
      "status": "Open",
      "priority": "High",
      "category": "Hardware",
      "createdAt": "2025-01-09T10:30:00Z",
      "updatedAt": "2025-01-09T14:20:00Z",
      "dueDate": "2025-01-12T10:30:00Z",
      "isOverdue": false,
      "createdBy": {
        "id": 5,
        "firstName": "Jan",
        "lastName": "Kowalski",
        "email": "jan.kowalski@company.com"
      },
      "assignedTo": {
        "id": 2,
        "firstName": "Anna",
        "lastName": "Nowak",
        "email": "anna.nowak@company.com"
      }
    }
  ],
  "currentPage": 1,
  "pageSize": 10,
  "totalCount": 125,
  "totalPages": 13
}
```

#### 2. GET /api/tickets/{id} - Szczegóły zgłoszenia

**Parametry URL:**

- `id` (int) - ID zgłoszenia

**Odpowiedź 200 OK:**

```json
{
  "id": 1,
  "title": "Problem z drukarką",
  "description": "Drukarka nie drukuje w kolorze. Próbowałem wymienić tusze...",
  "status": "Open",
  "priority": "High",
  "category": "Hardware",
  "createdAt": "2025-01-09T10:30:00Z",
  "updatedAt": "2025-01-09T14:20:00Z",
  "dueDate": "2025-01-12T10:30:00Z",
  "isOverdue": false,
  "createdBy": {
    "id": 5,
    "firstName": "Jan",
    "lastName": "Kowalski",
    "email": "jan.kowalski@company.com",
    "role": "User",
    "department": "Accounting"
  },
  "assignedTo": {
    "id": 2,
    "firstName": "Anna",
    "lastName": "Nowak",
    "email": "anna.nowak@company.com",
    "role": "Technician",
    "department": "IT"
  },
  "comments": [
    {
      "id": 10,
      "content": "Sprawdzę to jutro rano",
      "isInternal": false,
      "createdAt": "2025-01-09T14:20:00Z",
      "createdBy": {
        "id": 2,
        "firstName": "Anna",
        "lastName": "Nowak"
      }
    }
  ]
}
```

**Odpowiedź 404 Not Found:**

```json
{
  "error": "Ticket not found"
}
```

#### 3. POST /api/tickets - Utwórz zgłoszenie

**Request Body:**

```json
{
  "title": "Brak dostępu do VPN",
  "description": "Nie mogę połączyć się z VPN od wczoraj. Próbowałem restart komputera.",
  "priority": "Medium",
  "category": "Network",
  "createdById": 7
}
```

**Walidacja:**

- `title` - wymagane, 5-200 znaków
- `description` - wymagane, minimum 10 znaków
- `priority` - wymagane (Low, Medium, High, Critical)
- `category` - wymagane (Hardware, Software, Network, Access, Email, Other)
- `createdById` - wymagane, istniejący użytkownik

**Odpowiedź 201 Created:**

```json
{
  "id": 126,
  "title": "Brak dostępu do VPN",
  "description": "Nie mogę połączyć się z VPN...",
  "status": "New",
  "priority": "Medium",
  "category": "Network",
  "createdAt": "2025-01-09T15:45:00Z",
  "dueDate": "2025-01-11T15:45:00Z",
  "createdBy": {
    "id": 7,
    "firstName": "Marek",
    "lastName": "Wiśniewski"
  }
}
```

**Odpowiedź 400 Bad Request:**

```json
{
  "errors": {
    "title": ["Title is required"],
    "description": ["Description must be at least 10 characters"]
  }
}
```

#### 4. PUT /api/tickets/{id} - Aktualizuj zgłoszenie

**Request Body:**

```json
{
  "title": "Problem z drukarką HP (zaktualizowane)",
  "description": "Drukarka HP LaserJet nie drukuje w kolorze...",
  "status": "InProgress",
  "priority": "High",
  "category": "Hardware",
  "assignedToId": 3
}
```

**Odpowiedź 200 OK:** (Zaktualizowany obiekt)

**Odpowiedź 404 Not Found:**

```json
{
  "error": "Ticket not found"
}
```

#### 5. DELETE /api/tickets/{id} - Usuń zgłoszenie

**Odpowiedź 204 No Content**

**Odpowiedź 404 Not Found:**

```json
{
  "error": "Ticket not found"
}
```

#### 6. GET /api/tickets/{id}/comments - Lista komentarzy

**Odpowiedź 200 OK:**

```json
[
  {
    "id": 10,
    "content": "Sprawdzę to jutro rano",
    "isInternal": false,
    "createdAt": "2025-01-09T14:20:00Z",
    "createdBy": {
      "id": 2,
      "firstName": "Anna",
      "lastName": "Nowak",
      "email": "anna.nowak@company.com"
    }
  },
  {
    "id": 11,
    "content": "Problem z tonerem, nie z ustawieniami",
    "isInternal": true,
    "createdAt": "2025-01-09T14:25:00Z",
    "createdBy": {
      "id": 2,
      "firstName": "Anna",
      "lastName": "Nowak"
    }
  }
]
```

#### 7. POST /api/tickets/{id}/comments - Dodaj komentarz

**Request Body:**

```json
{
  "content": "Wymieniłem toner, proszę sprawdzić czy działa",
  "isInternal": false,
  "createdById": 2
}
```

**Odpowiedź 201 Created:** (Nowy komentarz)

### Endpointy użytkowników

#### GET /api/users - Lista użytkowników

**Query Parameters:**

- `role` (string) - filtr po roli

**Odpowiedź 200 OK:**

```json
[
  {
    "id": 1,
    "firstName": "Jan",
    "lastName": "Kowalski",
    "email": "jan.kowalski@company.com",
    "role": "Admin",
    "department": "IT",
    "isActive": true
  }
]
```

#### GET /api/users/{id} - Szczegóły użytkownika

**Odpowiedź 200 OK:**

```json
{
  "id": 1,
  "firstName": "Jan",
  "lastName": "Kowalski",
  "email": "jan.kowalski@company.com",
  "role": "Admin",
  "department": "IT",
  "phoneNumber": "+48 123 456 789",
  "isActive": true,
  "createdAt": "2024-01-01T00:00:00Z",
  "createdTickets": [],
  "assignedTickets": []
}
```

---

## 💾 Baza danych

### Schema

```sql
-- Tabela Users
CREATE TABLE Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE,
    Role ENUM('Admin', 'Technician', 'User') NOT NULL,
    Department VARCHAR(100),
    PhoneNumber VARCHAR(20),
    IsActive BOOLEAN DEFAULT TRUE,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    INDEX idx_email (Email),
    INDEX idx_role (Role)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabela Tickets
CREATE TABLE Tickets (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(200) NOT NULL,
    Description TEXT NOT NULL,
    Status ENUM('New', 'Open', 'InProgress', 'Resolved', 'Closed') NOT NULL DEFAULT 'New',
    Priority ENUM('Low', 'Medium', 'High', 'Critical') NOT NULL,
    Category ENUM('Hardware', 'Software', 'Network', 'Access', 'Email', 'Other') NOT NULL,
    CreatedById INT NOT NULL,
    AssignedToId INT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    DueDate DATETIME NULL,
    ResolvedAt DATETIME NULL,
    FOREIGN KEY (CreatedById) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (AssignedToId) REFERENCES Users(Id) ON DELETE SET NULL,
    INDEX idx_status (Status),
    INDEX idx_priority (Priority),
    INDEX idx_category (Category),
    INDEX idx_created_by (CreatedById),
    INDEX idx_assigned_to (AssignedToId),
    INDEX idx_created_at (CreatedAt),
    FULLTEXT INDEX idx_search (Title, Description)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabela Comments
CREATE TABLE Comments (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Content TEXT NOT NULL,
    IsInternal BOOLEAN DEFAULT FALSE,
    TicketId INT NOT NULL,
    CreatedById INT NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (TicketId) REFERENCES Tickets(Id) ON DELETE CASCADE,
    FOREIGN KEY (CreatedById) REFERENCES Users(Id) ON DELETE CASCADE,
    INDEX idx_ticket (TicketId),
    INDEX idx_created_by (CreatedById)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
```

### Relacje

```
Users (1) ----< (N) Tickets (CreatedBy)
Users (1) ----< (N) Tickets (AssignedTo)
Users (1) ----< (N) Comments (CreatedBy)
Tickets (1) ----< (N) Comments
```

### Indeksy i optymalizacja

**Indeksy:**

- Primary Keys: AUTO_INCREMENT dla wydajności
- Foreign Keys: Automatyczne indeksy
- Email: UNIQUE + INDEX dla szybkiego wyszukiwania
- Status, Priority, Category: INDEX dla filtrowania
- Dates: INDEX dla sortowania
- FULLTEXT: Na Title i Description dla wyszukiwania pełnotekstowego

**Optymalizacje:**

- `InnoDB` engine - transakcje, foreign keys, row-level locking
- `utf8mb4_unicode_ci` - pełne wsparcie Unicode (emoji)
- `ON DELETE CASCADE` - automatyczne czyszczenie powiązanych rekordów
- Denormalizacja: brak, preferujemy JOIN dla integralności

---

## 🎨 Frontend

### Routing

```typescript
const routes = [
  {
    path: "/",
    name: "Dashboard",
    component: DashboardPage,
  },
  {
    path: "/tickets",
    name: "Tickets",
    component: TicketsPage,
  },
  {
    path: "/tickets/create",
    name: "CreateTicket",
    component: CreateTicketPage,
  },
  {
    path: "/tickets/:id",
    name: "TicketDetail",
    component: TicketDetailPage,
  },
  {
    path: "/statistics",
    name: "Statistics",
    component: StatisticsPage,
  },
  {
    path: "/:pathMatch(.*)*",
    name: "NotFound",
    component: NotFoundPage,
  },
];
```

### Komponenty reusable

#### StatusBadge.vue

Wyświetla kolorowe badge dla statusów i priorytetów.

**Props:**

- `type` (string): 'status' | 'priority'
- `value` (string): wartość do wyświetlenia

**Przykład użycia:**

```vue
<StatusBadge type="status" value="Open" />
<StatusBadge type="priority" value="High" />
```

**Kolory:**

- Status:
  - New: żółty (#FFC700)
  - Open: niebieski
  - InProgress: pomarańczowy
  - Resolved: zielony
  - Closed: szary
- Priority:
  - Low: zielony
  - Medium: żółty
  - High: pomarańczowy
  - Critical: czerwony

#### SearchInput.vue

Input z debounce dla wyszukiwania.

**Props:**

- `modelValue` (string): wartość
- `placeholder` (string): tekst placeholder
- `delay` (number): opóźnienie debounce (ms)

**Events:**

- `update:modelValue`: emitowane po debounce

**Przykład:**

```vue
<SearchInput
  v-model="searchTerm"
  placeholder="Szukaj zgłoszeń..."
  :delay="500" />
```

#### Pagination.vue

Komponent paginacji z nawigacją stron.

**Props:**

- `currentPage` (number)
- `totalPages` (number)
- `totalCount` (number)

**Events:**

- `page-changed`: emitowane przy zmianie strony

**Przykład:**

```vue
<Pagination
  :currentPage="pagination.currentPage"
  :totalPages="pagination.totalPages"
  :totalCount="pagination.totalCount"
  @page-changed="handlePageChange" />
```

### State Management

#### Stores

**ticketStore.ts** - zarządzanie zgłoszeniami:

- `tickets` - lista zgłoszeń
- `currentTicket` - aktualnie wyświetlane zgłoszenie
- `filters` - filtry SFWP
- `pagination` - informacje o paginacji
- `isLoading` - stan ładowania

**userStore.ts** - zarządzanie użytkownikami:

- `users` - lista użytkowników
- `technicians` - lista techników (do przypisywania)
- `currentUser` - zalogowany użytkownik (future feature)

### Services

#### api.ts

Axios client z konfiguracją:

```typescript
const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || "http://localhost:5000/api",
  headers: {
    "Content-Type": "application/json",
  },
});

// Interceptors for error handling
api.interceptors.response.use(
  (response) => response,
  (error) => {
    // Global error handling
    return Promise.reject(error);
  }
);
```

#### ticketService.ts

Metody API:

- `getTickets(params)` - lista z SFWP
- `getTicketById(id)` - szczegóły
- `createTicket(data)` - tworzenie
- `updateTicket(id, data)` - aktualizacja
- `deleteTicket(id)` - usuwanie
- `getComments(ticketId)` - komentarze
- `addComment(ticketId, data)` - dodawanie komentarza

### Style

**TailwindCSS** z custom konfigracją:

```javascript
// tailwind.config.js
module.exports = {
  theme: {
    extend: {
      colors: {
        "k-main": "#FFC700", // Primary color
        "k-bg": "#1a1a1a",
        "k-card": "#2a2a2a",
      },
    },
  },
};
```

---

## 🔒 Bezpieczeństwo

### Backend

#### CORS

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",
            "http://localhost:5174",
            "http://localhost:3000"
        )
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
```

#### Walidacja danych

```csharp
[Required(ErrorMessage = "Title is required")]
[StringLength(200, MinimumLength = 5)]
public string Title { get; set; }

[Required(ErrorMessage = "Description is required")]
[MinLength(10, ErrorMessage = "Description must be at least 10 characters")]
public string Description { get; set; }
```

#### SQL Injection Protection

Entity Framework używa **parametryzowanych zapytań**:

```csharp
// ✅ Safe - parameterised
var tickets = await _context.Tickets
    .Where(t => t.Status == status)
    .ToListAsync();

// ❌ DO NOT DO THIS - string interpolation
// var sql = $"SELECT * FROM Tickets WHERE Status = '{status}'";
```

### Frontend

#### XSS Protection

Vue.js automatycznie escapuje:

```vue
<!-- ✅ Safe - Vue escapes it -->
<div>{{ ticket.description }}</div>

<!-- ❌ Niebezpieczne - raw HTML -->
<div v-html="ticket.description"></div>
```

#### CSRF Protection

W przyszłości: tokeny CSRF w headerach.

---

## 🚀 Wdrożenie

### Development

```bash
# Backend
cd backend
dotnet watch run

# Frontend
cd frontend
npm run dev
```

### Production Build

#### Backend

```bash
cd backend
dotnet publish -c Release -o ./publish

# Uruchom
cd publish
dotnet HelpDeskAPI.dll
```

**Konfiguracja produkcyjna** (`appsettings.Production.json`):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=prod-server;Database=helpdesk_db;User=prod_user;Password=***;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  }
}
```

#### Frontend

```bash
cd frontend
npm run build

# Generated files in: frontend/dist/
```

**Nginx config:**

```nginx
server {
    listen 80;
    server_name helpdesk.company.com;
    root /var/www/helpdesk/dist;
    index index.html;

    location / {
        try_files $uri $uri/ /index.html;
    }

    location /api {
        proxy_pass http://localhost:5000;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
    }
}
```

### Docker (Opcjonalnie)

#### docker-compose.yml

```yaml
version: "3.8"

services:
  mysql:
    image: mysql:8.0
    environment:
      MYSQL_ROOT_PASSWORD: rootpass
      MYSQL_DATABASE: helpdesk_db
      MYSQL_USER: helpdesk_user
      MYSQL_PASSWORD: HelpDesk2024!
    ports:
      - "3306:3306"
    volumes:
      - mysql_data:/var/lib/mysql

  backend:
    build: ./backend
    ports:
      - "5000:80"
    depends_on:
      - mysql
    environment:
      ConnectionStrings__DefaultConnection: "Server=mysql;Database=helpdesk_db;User=helpdesk_user;Password=HelpDesk2024!;"

  frontend:
    build: ./frontend
    ports:
      - "80:80"
    depends_on:
      - backend

volumes:
  mysql_data:
```

#### Backend Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["HelpDeskAPI.csproj", "./"]
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelpDeskAPI.dll"]
```

#### Frontend Dockerfile

```dockerfile
FROM node:18 AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
```

---

## 🔧 Troubleshooting

### Problem: Backend nie startuje

**Błąd:** `Unable to connect to MySQL`

**Rozwiązanie:**

1. Sprawdź czy MySQL działa:

   ```bash
   sudo systemctl status mysql  # Linux
   brew services list           # macOS
   ```

2. Sprawdź connection string w `appsettings.json`

3. Testuj połączenie:
   ```bash
   mysql -u helpdesk_user -p -h localhost
   ```

### Problem: Migracje nie działają

**Błąd:** `Build failed`

**Rozwiązanie:**

```bash
# Install dotnet-ef
dotnet tool install --global dotnet-ef

# Check that it works
dotnet ef --version

# Recreate the migration
dotnet ef migrations add InitialCreate --force

# Aplikuj
dotnet ef database update
```

### Problem: CORS errors w przeglądarce

**Błąd:** `Access to XMLHttpRequest blocked by CORS policy`

**Rozwiązanie:**

1. Sprawdź czy backend ma skonfigurowany CORS w `Program.cs`
2. Sprawdź czy port frontendu jest w `AllowedOrigins`
3. Hard refresh przeglądarki (Ctrl+Shift+R)
4. Sprawdź w Console jakie origin próbuje się połączyć

### Problem: Frontend nie łączy się z backend

**Błąd:** `Network Error`

**Rozwiązanie:**

1. Sprawdź `.env`:

   ```
   VITE_API_URL=http://localhost:5000/api
   ```

2. Sprawdź czy backend działa:

   ```bash
   curl http://localhost:5000/api/tickets
   ```

3. Sprawdź czy nie ma problemu z HTTPS:
   - Backend: skomentuj `app.UseHttpsRedirection()`
   - Frontend: użyj `http://` nie `https://`

### Problem: Baza danych jest pusta

**Rozwiązanie:**

1. Sprawdź czy seeder został uruchomiony:

   ```csharp
   // Program.cs should contain:
   using (var scope = app.Services.CreateScope())
   {
       var context = scope.ServiceProvider.GetRequiredService<HelpDeskContext>();
       DbSeeder.Seed(context);
   }
   ```

2. Usuń bazę i utwórz od nowa:
   ```bash
   dotnet ef database drop
   dotnet ef database update
   ```

### Problem: Frontend pokazuje błędy TypeScript

**Rozwiązanie:**

```bash
# Remove node_modules and the cache
rm -rf node_modules package-lock.json
npm cache clean --force
npm install

# Check tsconfig.json
npx tsc --noEmit
```

---

## 📊 Monitoring i Logi

### Backend Logging

```csharp
// W kontrolerze
private readonly ILogger<TicketsController> _logger;

public TicketsController(ILogger<TicketsController> logger)
{
    _logger = logger;
}

[HttpGet]
public async Task<ActionResult> GetTickets()
{
    _logger.LogInformation("Fetching tickets with filters");
    try
    {
        // ...
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error fetching tickets");
        return StatusCode(500, "Internal server error");
    }
}
```

### Frontend Error Handling

```typescript
// W ticketStore.ts
async fetchTickets() {
  this.isLoading = true
  this.error = null

  try {
    const response = await ticketService.getTickets(this.filters)
    this.tickets = response.data.items
  } catch (error) {
    this.error = 'Nie udało się pobrać zgłoszeń'
    console.error('Error fetching tickets:', error)
  } finally {
    this.isLoading = false
  }
}
```

---

## 📞 Wsparcie

W przypadku problemów:

1. **Sprawdź dokumentację** - większość odpowiedzi znajdziesz tutaj
2. **Logi aplikacji** - sprawdź console (frontend) i terminal (backend)
3. **Swagger** - testuj API bezpośrednio http://localhost:5000/swagger
4. **GitHub Issues** - otwórz issue z opisem problemu

---

## 🎓 Dalszy rozwój

### Planowane funkcjonalności

- [ ] **Autentykacja JWT** - logowanie użytkowników
- [ ] **Autoryzacja** - permissions per role
- [ ] **Powiadomienia Email** - automatyczne przy nowych zgłoszeniach
- [ ] **Załączniki** - upload plików do zgłoszeń
- [ ] **SLA Management** - monitoring terminów
- [ ] **Export danych** - CSV, PDF, Excel
- [ ] **Raporty** - zaawansowane statystyki
- [ ] **Wyszukiwanie fulltext** - MySQL FULLTEXT lub Elasticsearch
- [ ] **Real-time** - SignalR dla powiadomień
- [ ] **Mobile App** - React Native lub Flutter
- [ ] **Testy** - Unit tests (xUnit), Integration tests, E2E (Cypress)
- [ ] **CI/CD** - GitHub Actions, Azure DevOps

---

<div align="center">

**Dokumentacja v1.0.0**

[⬆ Powrót do góry](#-dokumentacja-systemu-help-desk)

</div>
