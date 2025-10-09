# SortListApp-Project-ASPNetCore-Vue

A full-stack application demonstrating REST API, sorting, and filtering capabilities with ASP.NET Core backend and Vue.js frontend. This project was created as part of the Internet Applications 2 subject.

## 📁 Project Structure

```
SortListApp-Project-ASPNetCore-Vue/
├── Backend/                    # ASP.NET Core Web API
│   └── SortListApi/
│       ├── Controllers/       # API Controllers
│       │   └── ProductsController.cs
│       ├── Models/           # Data Models
│       │   └── Product.cs
│       └── Program.cs        # Application entry point
│
├── Frontend/                  # Vue.js Application
│   ├── src/
│   │   ├── components/       # Vue Components
│   │   │   └── ProductList.vue
│   │   ├── services/         # API Services
│   │   │   └── api.js
│   │   ├── App.vue          # Main App Component
│   │   ├── main.js          # Application entry point
│   │   └── style.css        # Global styles
│   ├── index.html
│   ├── vite.config.js       # Vite configuration
│   └── package.json
│
└── README.md
```

## 🚀 Features

### Backend (ASP.NET Core)
- ✅ RESTful API with full CRUD operations
- ✅ Product management (Create, Read, Update, Delete)
- ✅ Advanced sorting by multiple fields (ID, Name, Category, Price, Stock, Date)
- ✅ Filtering capabilities:
  - Search by product name
  - Filter by category
  - Filter by price range (min/max)
- ✅ CORS configuration for frontend communication
- ✅ OpenAPI/Swagger support

### Frontend (Vue.js 3)
- ✅ Modern, responsive UI with gradient design
- ✅ Real-time product table with sorting
- ✅ Advanced filtering controls
- ✅ Statistics dashboard (Total Products, Total Value, Categories)
- ✅ Add/Delete product functionality
- ✅ Interactive table with hover effects
- ✅ Modal dialogs for data entry
- ✅ Error handling and loading states

## 🛠️ Technologies Used

### Backend
- **ASP.NET Core 9.0** - Web API framework
- **C#** - Programming language
- **LINQ** - Data querying
- **OpenAPI** - API documentation

### Frontend
- **Vue.js 3** - Progressive JavaScript framework
- **Vite** - Build tool and development server
- **Axios** - HTTP client for API calls
- **CSS3** - Styling with gradients and animations

## 📋 Prerequisites

- **.NET SDK 9.0 or later** - [Download](https://dotnet.microsoft.com/download)
- **Node.js 18+ and npm** - [Download](https://nodejs.org/)

## 🏃 Running the Application

### 1. Start the Backend (ASP.NET Core API)

```bash
# Navigate to the backend directory
cd Backend/SortListApi

# Restore dependencies (if needed)
dotnet restore

# Run the API
dotnet run
```

The API will start on `http://localhost:5000`

### 2. Start the Frontend (Vue.js)

Open a new terminal:

```bash
# Navigate to the frontend directory
cd Frontend

# Install dependencies (first time only)
npm install

# Start the development server
npm run dev
```

The frontend will be available at `http://localhost:5173`

## 🌐 API Endpoints

### Products
- `GET /api/products` - Get all products (with optional filters)
  - Query parameters:
    - `sortBy`: Field to sort by (id, name, category, price, stock, createdDate)
    - `sortOrder`: Sort order (asc, desc)
    - `category`: Filter by category
    - `minPrice`: Minimum price filter
    - `maxPrice`: Maximum price filter
    - `search`: Search by product name
- `GET /api/products/{id}` - Get product by ID
- `POST /api/products` - Create a new product
- `PUT /api/products/{id}` - Update a product
- `DELETE /api/products/{id}` - Delete a product
- `GET /api/products/categories` - Get all unique categories

### Example Requests

**Get all products sorted by price (descending):**
```
GET http://localhost:5000/api/products?sortBy=price&sortOrder=desc
```

**Get products in Electronics category with price between $50 and $500:**
```
GET http://localhost:5000/api/products?category=Electronics&minPrice=50&maxPrice=500
```

**Search for products containing "Laptop":**
```
GET http://localhost:5000/api/products?search=Laptop
```

## 📊 Sample Data

The application comes pre-loaded with 10 sample products:
- Electronics: Laptop, Mouse, Keyboard, Monitor, Headphones, Webcam
- Furniture: Desk Chair, Desk
- Stationery: Notebook, Pen

## 🎨 Features Demo

### Sorting
Click on any table column header to sort by that field. Click again to reverse the sort order.

### Filtering
- **Search**: Type in the search box to filter products by name
- **Category**: Select a category from the dropdown
- **Price Range**: Set minimum and/or maximum price filters
- **Reset**: Clear all filters with the "Reset Filters" button

### CRUD Operations
- **Create**: Click "Add Product" button to open the form
- **Read**: View all products in the table
- **Delete**: Click "Delete" button on any row (with confirmation)

## 🔧 Build for Production

### Backend
```bash
cd Backend/SortListApi
dotnet publish -c Release -o ./publish
```

### Frontend
```bash
cd Frontend
npm run build
```

The production build will be in the `Frontend/dist` directory.

## 📝 Notes

- The backend uses an in-memory data store for simplicity. Data will reset when the server restarts.
- CORS is configured to allow requests from `http://localhost:5173` and `http://localhost:3000`
- The frontend proxy is configured to forward `/api` requests to `http://localhost:5000`

## 🤝 Contributing

This project was created for educational purposes. Feel free to fork and modify as needed.

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.
