#!/bin/bash

echo "========================================="
echo "🎫 IT Help Desk System - Auto Setup"
echo "========================================="
echo ""

PROJECT_DIR="$(cd "$(dirname "$0")" && pwd)"

echo "📍 Katalog projektu: $PROJECT_DIR"
echo ""

echo "1️⃣  Sprawdzam wymagania..."
echo ""

if ! command -v dotnet &> /dev/null; then
    echo "❌ .NET SDK nie jest zainstalowany!"
    echo "   Zainstaluj: brew install --cask dotnet-sdk"
    exit 1
fi
echo "✅ .NET SDK: $(dotnet --version)"

if ! command -v node &> /dev/null; then
    echo "❌ Node.js nie jest zainstalowany!"
    echo "   Zainstaluj: brew install node"
    exit 1
fi
echo "✅ Node.js: $(node --version)"

if ! command -v mysql &> /dev/null && ! [ -f /usr/local/mysql-8.0.42-macos15-arm64/bin/mysql ]; then
    echo "❌ MySQL nie jest zainstalowany!"
    exit 1
fi
echo "✅ MySQL zainstalowany"

echo ""
echo "2️⃣  Konfiguruję backend..."
echo ""

cd "$PROJECT_DIR/backend"

if ! command -v dotnet-ef &> /dev/null; then
    echo "📦 Instaluję dotnet-ef..."
    dotnet tool install --global dotnet-ef
    export PATH="$PATH:$HOME/.dotnet/tools"
fi

dotnet restore > /dev/null 2>&1
echo "✅ Pakiety NuGet przywrócone"

MYSQL_CMD=""
if command -v mysql &> /dev/null; then
    MYSQL_CMD="mysql"
else
    MYSQL_CMD="/usr/local/mysql-8.0.42-macos15-arm64/bin/mysql"
fi

if ! $MYSQL_CMD -u helpdesk_user -p'HelpDesk2024!' helpdesk_db -e "SELECT 1" &> /dev/null; then
    echo "🗄️  Tworzę bazę danych..."
    
    $MYSQL_CMD -u root << 'EOF' 2>/dev/null
CREATE DATABASE IF NOT EXISTS helpdesk_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
DROP USER IF EXISTS 'helpdesk_user'@'localhost';
CREATE USER 'helpdesk_user'@'localhost' IDENTIFIED BY 'HelpDesk2024!';
GRANT ALL PRIVILEGES ON helpdesk_db.* TO 'helpdesk_user'@'localhost';
FLUSH PRIVILEGES;
EOF
    
    if [ $? -eq 0 ]; then
        echo "✅ Baza danych utworzona"
    else
        echo "⚠️  Baza może już istnieć lub potrzebujesz hasła root MySQL"
    fi
fi

dotnet ef database update > /dev/null 2>&1
echo "✅ Migracje zastosowane"

echo ""
echo "3️⃣  Konfiguruję frontend..."
echo ""

cd "$PROJECT_DIR/frontend"

if [ ! -d "node_modules" ]; then
    echo "📦 Instaluję zależności npm..."
    npm install > /dev/null 2>&1
    echo "✅ Zależności zainstalowane"
else
    echo "✅ Zależności już zainstalowane"
fi

echo ""
echo "========================================="
echo "✅ Projekt skonfigurowany!"
echo "========================================="
echo ""
echo "🚀 Uruchamianie..."
echo ""
echo "📍 Backend:  http://localhost:5000"
echo "📍 Swagger:  http://localhost:5000/swagger"
echo "📍 Frontend: http://localhost:5173"
echo ""
echo "⏳ Proszę czekać..."
echo ""

cd "$PROJECT_DIR/backend"
dotnet run &
BACKEND_PID=$!

sleep 5

cd "$PROJECT_DIR/frontend"
npm run dev &
FRONTEND_PID=$!

echo ""
echo "========================================="
echo "✅ System uruchomiony!"
echo "========================================="
echo ""
echo "🌐 Otwórz: http://localhost:5173"
echo ""
echo "⚠️  Aby zatrzymać, naciśnij Ctrl+C"
echo ""

wait
