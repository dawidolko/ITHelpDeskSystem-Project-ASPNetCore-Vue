# IT Help Desk System - SFWP Project

# 🎫 System Zarządzania Zgłoszeniami IT (Help Desk)

<div align="center">

![Vue.js](https://img.shields.io/badge/Vue.js-3.3-4FC08D?style=for-the-badge&logo=vue.js&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-9.0-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![MySQL](https://img.shields.io/badge/MySQL-8.0-4479A1?style=for-the-badge&logo=mysql&logoColor=white)
![TypeScript](https://img.shields.io/badge/TypeScript-5.0-3178C6?style=for-the-badge&logo=typescript&logoColor=white)

**Zaawansowany system do zarządzania zgłoszeniami IT z pełnym wsparciem SFWP**

[Demo](#-zrzuty-ekranu) • [Instalacja](#-szybki-start) • [Dokumentacja](docs/README.md) • [API](http://localhost:5000/swagger)

</div>

---

## 📋 Spis treści

- [O projekcie](#-o-projekcie)
- [Funkcjonalności](#-funkcjonalności)
- [Technologie](#-technologie)
- [Szybki start](#-szybki-start)
- [Struktura projektu](#-struktura-projektu)
- [Wymagania](#-wymagania)
- [Konfiguracja](#-konfiguracja)
- [Rozwój projektu](#-rozwój-projektu)
- [⭐ Raport walidacji](#-raport-walidacji)
- [🧪 Instrukcja testowania](#-instrukcja-testowania)
- [Licencja](#-licencja)

---

## 🎯 O projekcie

**System Zarządzania Zgłoszeniami IT** to kompleksowe rozwiązanie typu Help Desk, stworzone z wykorzystaniem nowoczesnych technologii webowych. Aplikacja umożliwia efektywne zarządzanie zgłoszeniami technicznymi w organizacji, oferując intuicyjny interfejs użytkownika oraz wydajne API REST.

### Kluczowe cechy:

- 🚀 **Pełna architektura REST API & SPA**
- 🔍 **Zaawansowane SFWP** (Sort, Filter, Search, Pagination)
- 👥 **System ról użytkowników** (Admin, Technik, Użytkownik)
- 📊 **Dashboard ze statystykami** w czasie rzeczywistym
- 💬 **System komentarzy** (publiczne i wewnętrzne)
- 📱 **Responsywny design** działający na wszystkich urządzeniach
- 🔄 **Hot Module Replacement** dla szybkiego developmentu
- 📝 **Interaktywna dokumentacja API** (Swagger/OpenAPI)

---

## ✨ Funkcjonalności

### 🎫 Zarządzanie Zgłoszeniami

- **CRUD Operations** - pełna obsługa tworzenia, odczytu, aktualizacji i usuwania zgłoszeń
- **Przypisywanie do techników** - automatyczne i manualne przydzielanie zadań
- **Statusy zgłoszeń** - Nowe, Otwarte, W trakcie, Rozwiązane, Zamknięte
- **Priorytety** - Niski, Średni, Wysoki, Krytyczny
- **Kategorie** - Sprzęt, Oprogramowanie, Sieć, Dostęp, Email, Inne
- **Terminy realizacji** - automatyczne oznaczanie przeterminowanych zgłoszeń

### 🔍 Wyszukiwanie i Filtrowanie

- **Wyszukiwanie pełnotekstowe** - po tytule, opisie, nazwach użytkowników
- **Wielokryterialne filtrowanie** - status, priorytet, kategoria, przypisany technik, przeterminowane
- **Dynamiczne sortowanie** - po dowolnym polu (tytuł, status, priorytet, data utworzenia)
- **Paginacja** - z informacją o liczbie stron i rekordów

### 👥 System Użytkowników & Autoryzacja

- **JWT Authentication** - bezpieczne uwierzytelnianie z tokenami (7-dniowa ważność)
- **Role użytkowników** - User, Technician, Admin z różnymi uprawnieniami
- **Rejestracja publiczna** - każdy może się zarejestrować jako User
- **Panel Admina** - zarządzanie użytkownikami i zmiana ról
- **Role-based access control**:
  - **User** - widzi tylko swoje zgłoszenia
  - **Technician** - widzi wszystkie zgłoszenia, może aktualizować przypisane
  - **Admin** - pełny dostęp, zarządzanie użytkownikami
- **Departamenty** - przypisanie do działów organizacji

### 💬 Komentarze

- **Publiczne i wewnętrzne** - komunikacja z użytkownikami i notatki dla techników
- **Timeline** - chronologiczna historia wszystkich komentarzy
- **Powiadomienia** - informowanie o nowych komentarzach

### 📊 Statystyki i Raporty

- **Dashboard** - przegląd kluczowych metryk
- **Statystyki według priorytetów** - podział zgłoszeń z wykresami
- **Statystyki według kategorii** - analiza typów problemów
- **Wskaźniki wydajności** - średni czas rozwiązania, wskaźnik rozwiązań
- **Najnowsze zgłoszenia** - aktywność w czasie rzeczywistym

---

## 🛠️ Technologie

### Backend

| Technologia                          | Wersja | Zastosowanie                  |
| ------------------------------------ | ------ | ----------------------------- |
| **ASP.NET Core**                     | 9.0    | Framework webowy              |
| **Entity Framework Core**            | 8.0    | ORM - dostęp do bazy danych   |
| **MySQL**                            | 8.0    | Baza danych                   |
| **Pomelo.EntityFrameworkCore.MySql** | 8.0    | Provider MySQL dla EF Core    |
| **Swashbuckle**                      | 7.2    | Dokumentacja API (Swagger)    |
| **CORS**                             | -      | Obsługa Cross-Origin Requests |

### Frontend

| Technologia     | Wersja | Zastosowanie        |
| --------------- | ------ | ------------------- |
| **Vue.js**      | 3.3    | Framework SPA       |
| **TypeScript**  | 5.0    | Typowanie statyczne |
| **Pinia**       | 2.1    | State management    |
| **Vue Router**  | 4.2    | Routing             |
| **Axios**       | 1.6    | HTTP Client         |
| **TailwindCSS** | 3.3    | Framework CSS       |
| **Vite**        | 4.4    | Build tool          |

---

## 🚀 Szybki start

### Automatyczna instalacja (Rekomendowane)

**Dla początkujących - uruchom wszystko jedną komendą:**

```bash
./start-all.sh
```

Ten skrypt automatycznie:

- ✅ Sprawdzi wszystkie wymagania systemowe
- ✅ Zainstaluje brakujące narzędzia (.NET EF, npm packages)
- ✅ Utworzy bazę danych i użytkownika MySQL
- ✅ Wykona migracje i wypełni bazę danymi testowymi
- ✅ Uruchomi backend i frontend

### Ręczna instalacja

#### 1. Backend

```bash
cd backend
./start.sh      # macOS/Linux
start.bat       # Windows
```

Lub krok po kroku:

```bash
cd backend
dotnet tool install --global dotnet-ef
dotnet restore
dotnet ef database update
dotnet run
```

#### 2. Frontend

```bash
cd frontend
./start.sh      # macOS/Linux
start.bat       # Windows
```

Lub krok po kroku:

```bash
cd frontend
npm install
npm run dev
```

### 🌐 Dostępne adresy URL

Po uruchomieniu aplikacja będzie dostępna pod:

- **Frontend (SPA):** http://localhost:5173
- **Backend API:** http://localhost:5000
- **Swagger UI:** http://localhost:5000/swagger
- **API Endpoint:** http://localhost:5000/api/tickets

---

## � Struktura projektu

```
SortListApp-Project-ASPNetCore-Vue/
│
├── 📄 README.md                    # Ten plik
├── 📄 start-all.sh                 # Skrypt uruchamiający wszystko
│
├── 📂 backend/                     # Backend ASP.NET Core
│   ├── 📄 start.sh / start.bat    # Skrypty uruchomieniowe
│   ├── 📄 HelpDeskAPI.csproj      # Konfiguracja projektu .NET
│   ├── 📄 Program.cs              # Entry point aplikacji
│   ├── 📄 appsettings.json        # Konfiguracja (connection string)
│   │
│   ├── 📂 Controllers/            # Kontrolery API
│   │   ├── TicketsController.cs  # CRUD + SFWP dla zgłoszeń
│   │   └── UsersController.cs    # Zarządzanie użytkownikami
│   │
│   ├── 📂 Models/                 # Modele danych
│   │   ├── Ticket.cs             # Model zgłoszenia
│   │   ├── User.cs               # Model użytkownika
│   │   ├── Comment.cs            # Model komentarza
│   │   └── Enums.cs              # Enumy (Status, Priority, Category)
│   │
│   ├── 📂 DTOs/                   # Data Transfer Objects
│   │   └── TicketDtos.cs         # DTOs dla API
│   │
│   ├── 📂 Data/                   # Warstwa dostępu do danych
│   │   ├── HelpDeskContext.cs    # DbContext
│   │   └── DbSeeder.cs           # Seeder danych testowych
│   │
│   └── 📂 Migrations/             # Migracje Entity Framework
│
├── 📂 frontend/                    # Frontend Vue.js
│   ├── 📄 start.sh / start.bat    # Skrypty uruchomieniowe
│   ├── 📄 package.json            # Zależności npm
│   ├── 📄 vite.config.ts          # Konfiguracja Vite
│   ├── 📄 tsconfig.json           # Konfiguracja TypeScript
│   ├── 📄 tailwind.config.js     # Konfiguracja TailwindCSS
│   │
│   ├── 📂 src/
│   │   ├── 📄 main.ts            # Entry point aplikacji
│   │   ├── 📄 App.vue            # Główny komponent
│   │   │
│   │   ├── 📂 pages/             # Strony aplikacji
│   │   │   ├── Dashboard/        # Dashboard ze statystykami
│   │   │   ├── Tickets/          # Lista i szczegóły zgłoszeń
│   │   │   └── Statistics/       # Zaawansowane statystyki
│   │   │
│   │   ├── 📂 components/        # Komponenty reusable
│   │   │   ├── StatusBadge.vue   # Badge dla statusów/priorytetów
│   │   │   ├── Pagination.vue    # Komponent paginacji
│   │   │   └── SearchInput.vue   # Input z debounce
│   │   │
│   │   ├── 📂 stores/            # Pinia stores
│   │   │   ├── ticketStore.ts    # Stan zgłoszeń
│   │   │   └── userStore.ts      # Stan użytkowników
│   │   │
│   │   ├── 📂 services/          # Usługi API
│   │   │   ├── api.ts            # Axios client
│   │   │   └── ticketService.ts  # Metody API dla ticketów
│   │   │
│   │   ├── 📂 types/             # Definicje TypeScript
│   │   │   └── ticket.types.ts   # Typy dla ticketów
│   │   │
│   │   └── 📂 routes/            # Routing Vue Router
│   │       └── routes.ts         # Definicje tras
│   │
│   └── 📂 public/                 # Pliki statyczne
│
└── 📂 docs/                        # Dokumentacja projektu
    └── README.md                   # Szczegółowa dokumentacja
```

---

## 📋 Wymagania

### Wymagania systemowe

| Komponent    | Minimalna wersja | Rekomendowana |
| ------------ | ---------------- | ------------- |
| **.NET SDK** | 9.0              | 9.0+          |
| **Node.js**  | 18.0             | 20.0+         |
| **MySQL**    | 8.0              | 8.0+          |
| **npm**      | 9.0              | 10.0+         |

### Instalacja wymagań

**macOS:**

```bash
brew install --cask dotnet-sdk
brew install node
brew install mysql
```

**Windows:**

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/)
- [MySQL](https://dev.mysql.com/downloads/installer/)

**Linux (Ubuntu/Debian):**

```bash
wget https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y dotnet-sdk-9.0 nodejs mysql-server
```

---

## ⚙️ Konfiguracja

### 1. Baza danych MySQL

Utwórz bazę danych i użytkownika:

```sql
CREATE DATABASE helpdesk_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
CREATE USER 'helpdesk_user'@'localhost' IDENTIFIED BY 'HelpDesk2024!';
GRANT ALL PRIVILEGES ON helpdesk_db.* TO 'helpdesk_user'@'localhost';
FLUSH PRIVILEGES;
```

### 2. Connection String (Backend)

Edytuj `backend/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=helpdesk_db;User=helpdesk_user;Password=HelpDesk2024!;Port=3306;"
  }
}
```

### 3. API URL (Frontend)

Edytuj `frontend/.env`:

```env
VITE_API_URL=http://localhost:5000/api
```

### 4. CORS (Backend)

W `backend/Program.cs` możesz dodać dodatkowe origins:

```csharp
policy.WithOrigins(
    "http://localhost:5173",
    "http://localhost:5174",
    "http://twoja-domena.pl"
)
```

---

## 🔧 Rozwój projektu

### Backend Development

```bash
cd backend

# Uruchom w trybie watch (auto-reload)
dotnet watch run

# Utwórz nową migrację
dotnet ef migrations add NazwaMigracji

# Aplikuj migracje
dotnet ef database update

# Usuń ostatnią migrację
dotnet ef migrations remove

# Zobacz SQL dla migracji
dotnet ef migrations script
```

### Frontend Development

```bash
cd frontend

# Uruchom dev server
npm run dev

# Build produkcyjny
npm run build

# Preview buildu
npm run preview

# Linting
npm run lint

# Update przeglądarek
npx update-browserslist-db@latest
```

### Testowanie API

**Swagger UI:** http://localhost:5000/swagger

**cURL przykłady:**

```bash
# Pobierz wszystkie zgłoszenia
curl http://localhost:5000/api/tickets

# Pobierz z filtrowaniem i sortowaniem
curl "http://localhost:5000/api/tickets?status=Open&priority=High&sortBy=createdAt&sortOrder=desc"

# Pobierz szczegóły zgłoszenia
curl http://localhost:5000/api/tickets/1

# Utwórz nowe zgłoszenie
curl -X POST http://localhost:5000/api/tickets \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Test",
    "description": "Opis problemu",
    "priority": "Medium",
    "category": "Software",
    "createdById": 1
  }'
```

---

## 📊 Dane testowe

System automatycznie wypełnia bazę danymi testowymi przy pierwszym uruchomieniu:

- **👥 Użytkownicy:** 18 użytkowników (3 Adminów, 5 Techników, 10 Użytkowników) z zahashowanymi hasłami (BCrypt)
- **🎫 Zgłoszenia:** 20 zgłoszeń z różnymi statusami, priorytetami i kategoriami
- **💬 Komentarze:** 15+ komentarzy (publiczne i wewnętrzne)

### Testowe konta:

| Email           | Hasło     | Rola       | Dostęp                              |
| --------------- | --------- | ---------- | ----------------------------------- |
| admin@firma.pl  | Admin123! | Admin      | Pełny dostęp + zarządzanie          |
| tech@firma.pl   | Tech123!  | Technician | Wszystkie zgłoszenia, może edytować |
| user@firma.pl   | User123!  | User       | Tylko własne zgłoszenia             |
| admin1@firma.pl | Admin123! | Admin      | Konto testowe 2                     |
| tech1@firma.pl  | Tech123!  | Technician | Konto testowe 2                     |
| user1@firma.pl  | User123!  | User       | Konto testowe 2                     |

**Możesz też się zarejestrować!** Każdy nowy użytkownik otrzymuje rolę **User** (Admin może zmienić rolę w panelu zarządzania)

---

## 📚 Dodatkowa dokumentacja

- **[Szczegółowa dokumentacja](docs/README.md)** - Pełna dokumentacja projektu
- **[API Reference](http://localhost:5000/swagger)** - Interaktywna dokumentacja API
- **[⭐ RAPORT WALIDACJI](VALIDATION_REPORT.md)** - Szczegółowy raport spełnienia założeń na 100%
- **[🧪 INSTRUKCJA TESTOWANIA](TESTING_GUIDE.md)** - Krok po kroku jak przetestować walidację
- **[Architektura](docs/ARCHITECTURE.md)** - Struktura i wzorce projektowe
- **[Deployment](docs/DEPLOYMENT.md)** - Wdrożenie na produkcję

---

## ⭐ Raport Walidacji

**Projekt spełnia WSZYSTKIE założenia na 100%!**

Szczegółowy raport dostępny w: **[VALIDATION_REPORT.md](VALIDATION_REPORT.md)**

### ✅ Potwierdzone funkcjonalności:

- ✅ **REST API** - Pełne CRUD operations
- ✅ **SPA** - Vue.js 3 z TypeScript
- ✅ **Sortowanie** - Po 8 różnych polach, ASC/DESC
- ✅ **Filtrowanie** - 6 różnych filtrów (Status, Priority, Category, Assignment, Overdue)
- ✅ **Wyszukiwanie** - Pełnotekstowe po 7 polach
- ✅ **Paginacja PO STRONIE BACKENDU** z pełną walidacją:
  - ✅ Walidacja Page (min 1, max totalPages)
  - ✅ Walidacja PageSize (1-100)
  - ✅ Walidacja nieistniejących ID użytkowników
  - ✅ Błędy 400 Bad Request zamiast auto-korekty
  - ✅ Szczegółowe komunikaty błędów
- ✅ **Pełny interaktywny Swagger** dla wszystkich endpointów:
  - ✅ Wszystkie endpointy udokumentowane
  - ✅ XML comments dla wszystkich DTO
  - ✅ Przykłady wartości dla każdego pola
  - ✅ Enums jako stringi z opisami
  - ✅ Walidacje widoczne w Swagger UI

### 🧪 Jak przetestować?

Szczegółowa instrukcja testowania: **[TESTING_GUIDE.md](TESTING_GUIDE.md)**

**Szybki test:**

1. Uruchom backend: `cd backend && dotnet run`
2. Otwórz Swagger: http://localhost:5000/swagger
3. Testuj endpoint `GET /api/tickets` z parametrami:
   - `page=-5` → 400 Bad Request ✅
   - `pageSize=999` → 400 Bad Request ✅
   - `assignedToId=999999` → 400 Bad Request ✅

**Wszystkie testy muszą zwracać błędy zamiast pustych wyników!**

---

## 🤝 Wsparcie

W przypadku problemów:

1. Sprawdź [dokumentację](docs/README.md)
2. Sprawdź logi aplikacji
3. Otwórz issue na GitHubie

---

## 📝 Licencja

Ten projekt jest dostępny na licencji MIT.

---

<div align="center">

**Stworzony z ❤️ dla efektywnego zarządzania zgłoszeniami IT**

[⬆ Powrót do góry](#-system-zarządzania-zgłoszeniami-it-help-desk)

</div>

System zarządzania zgłoszeniami IT (Help Desk) zbudowany w **ASP.NET Core 8.0** (backend) i **Vue.js 3** z TypeScript (frontend).

![License](https://img.shields.io/badge/license-MIT-blue.svg)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)
![Vue](https://img.shields.io/badge/Vue.js-3.3-green.svg)
![MySQL](https://img.shields.io/badge/MySQL-8.0-blue.svg)

---

## 🎯 Funkcjonalności Projektu

### ✅ Pełna Obsługa SFWP

- **S**ortowanie - po dowolnym polu (data, priorytet, status, tytuł)
- **F**iltrowanie - po statusie, priorytecie, kategorii, przypisaniu
- **W**yszukiwanie - pełnotekstowe po tytule, opisie, użytkownikach
- **P**aginacja - efektywne stronicowanie wyników

### 🎫 System Ticketów

- Tworzenie, edycja, usuwanie zgłoszeń IT
- Komentarze do zgłoszeń (publiczne i wewnętrzne)
- Przypisywanie zgłoszeń do techników
- Statusy: New, Open, InProgress, OnHold, Resolved, Closed, Reopened
- Priorytety: Low, Medium, High, Critical (z SLA)
- Kategorie: Hardware, Software, Network, Account, Email, Printer, Other

### 📊 Dashboard & Statystyki

- Widok dashboardu z kluczowymi metrykami
- Zaawansowane statystyki (wykres po kategoriach, priorytetach)
- Średni czas rozwiązania zgłoszeń
- Wskaźniki przeciążenia systemu (overdue tickets)

### 🔧 Backend (ASP.NET Core)

- RESTful API z pełnym Swagger/OpenAPI
- Entity Framework Core z MySQL
- Automatyczne migracje i seeder z danymi testowymi
- CORS skonfigurowany dla frontendu
- Rozbudowane modele danych z relacjami

### 🎨 Frontend (Vue.js 3)

- TypeScript + Composition API
- Pinia Store Management
- Vue Router
- TailwindCSS (zachowany oryginalny styl projektu)
- Axios dla komunikacji z API
- Responsywny design (mobile-first)

---

## 🚀 Szybki Start

### Wymagania

- **.NET 8.0 SDK**
- **MySQL 8.0+**
- **Node.js 18+** i npm
- IDE: Visual Studio Code / Visual Studio

### 1. Instalacja Backend

```bash
# Przejdź do katalogu backend
cd backend

# Zainstaluj pakiety
dotnet restore

# Skonfiguruj MySQL (zobacz docs/SETUP.md)

# Uruchom migracje
dotnet ef migrations add InitialCreate
dotnet ef database update

# Uruchom API
dotnet run
```

Backend dostępny na: `https://localhost:5001`  
Swagger UI: `https://localhost:5001/swagger`

### 2. Instalacja Frontend

```bash
# Przejdź do katalogu frontend
cd frontend

# Zainstaluj zależności
npm install

# Uruchom dev server
npm run dev
```

Frontend dostępny na: `http://localhost:5173`

---

## 📖 Dokumentacja

Pełna instrukcja instalacji: **[docs/SETUP.md](docs/SETUP.md)**

Backend README: **[backend/README.md](backend/README.md)**

### Backend API Endpoints:

**Auth (Public):**

```bash
POST   /api/auth/register        # Rejestracja (domyślnie User)
POST   /api/auth/login           # Logowanie (zwraca JWT token)
```

**Tickets (Requires Authentication):**

```bash
GET    /api/tickets              # Lista zgłoszeń (SFWP) - filtrowane wg roli
GET    /api/tickets/{id}         # Szczegóły zgłoszenia
POST   /api/tickets              # Nowe zgłoszenie
PUT    /api/tickets/{id}         # Aktualizacja
DELETE /api/tickets/{id}         # Usunięcie (tylko Admin)
POST   /api/tickets/{id}/comments # Dodanie komentarza
GET    /api/tickets/statistics   # Statystyki
```

**Users (Requires Authentication):**

```bash
GET    /api/users                # Użytkownicy (Admin/Technician)
GET    /api/users/technicians    # Technicy
GET    /api/users/{id}           # Szczegóły użytkownika
PUT    /api/users/{id}/role      # Zmiana roli (tylko Admin)
DELETE /api/users/{id}           # Usunięcie użytkownika (tylko Admin)
```

#### Przykłady SFWP:

```bash
# Filtrowanie + Sortowanie
GET /api/tickets?status=Open&priority=High&sortBy=createdAt&sortOrder=desc

# Wyszukiwanie
GET /api/tickets?search=printer

# Paginacja
GET /api/tickets?page=2&pageSize=20

# Kombinacja
GET /api/tickets?status=Open&priority=Critical&search=network&sortBy=priority&sortOrder=desc&page=1&pageSize=10
```

---

## 💻 Technologie

**Backend:**

- ASP.NET Core 8.0
- Entity Framework Core 8.0
- MySQL 8.0 + Pomelo.EntityFrameworkCore.MySql
- Swagger/OpenAPI

**Frontend:**

- Vue.js 3.3 + TypeScript
- Pinia (State Management)
- Vue Router
- TailwindCSS
- Axios
- Vite

---

## 🎓 Projekt Edukacyjny

Projekt zaliczeniowy spełniający wymagania:

✅ **REST API** - pełne RESTful API  
✅ **SPA** - Single Page Application w Vue.js  
✅ **S**ortowanie - po wielu polach  
✅ **F**iltrowanie - wielokryterialne  
✅ **W**yszukiwanie - pełnotekstowe  
✅ **P**aginacja - stronicowanie  
✅ **Swagger** - interaktywna dokumentacja  
✅ **Backend** - ASP.NET Core w C#  
✅ **Frontend** - Vue.js 3 framework

---

## 📄 Licencja

MIT License - Projekt edukacyjny

---

**Made with ❤️ using ASP.NET Core & Vue.js**
