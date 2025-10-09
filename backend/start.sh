#!/bin/bash

echo "=================================="
echo "🚀 IT Help Desk - Backend Setup"
echo "=================================="
echo ""

if ! command -v dotnet &> /dev/null; then
    echo "❌ .NET SDK nie jest zainstalowany!"
    echo "Zainstaluj: brew install --cask dotnet-sdk"
    exit 1
fi

if ! command -v mysql &> /dev/null && ! [ -f /usr/local/mysql-8.0.42-macos15-arm64/bin/mysql ]; then
    echo "❌ MySQL nie jest zainstalowany!"
    exit 1
fi

echo "✅ .NET SDK: $(dotnet --version)"
echo ""

if ! command -v dotnet-ef &> /dev/null; then
    echo "📦 Instaluję dotnet-ef..."
    dotnet tool install --global dotnet-ef
    export PATH="$PATH:$HOME/.dotnet/tools"
fi

echo "📦 Przywracam pakiety NuGet..."
dotnet restore

MYSQL_CMD=""
if command -v mysql &> /dev/null; then
    MYSQL_CMD="mysql"
else
    MYSQL_CMD="/usr/local/mysql-8.0.42-macos15-arm64/bin/mysql"
fi

echo ""
echo "🗄️  Sprawdzam bazę danych..."
if ! $MYSQL_CMD -u helpdesk_user -p'HelpDesk2024!' helpdesk_db -e "SELECT 1" &> /dev/null; then
    echo "⚠️  Baza danych nie istnieje. Tworzę..."
    
    $MYSQL_CMD -u root << 'EOF'
CREATE DATABASE IF NOT EXISTS helpdesk_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
DROP USER IF EXISTS 'helpdesk_user'@'localhost';
CREATE USER 'helpdesk_user'@'localhost' IDENTIFIED BY 'HelpDesk2024!';
GRANT ALL PRIVILEGES ON helpdesk_db.* TO 'helpdesk_user'@'localhost';
FLUSH PRIVILEGES;
EOF
    
    if [ $? -eq 0 ]; then
        echo "✅ Baza danych utworzona"
    else
        echo "❌ Błąd tworzenia bazy danych"
        exit 1
    fi
fi

echo "🔄 Aplikuję migracje..."
dotnet ef database update

if [ $? -ne 0 ]; then
    echo "❌ Błąd migracji"
    exit 1
fi

echo ""
echo "=================================="
echo "✅ Backend gotowy!"
echo "=================================="
echo ""
echo "🚀 Uruchamiam serwer..."
echo "📍 API: http://localhost:5000"
echo "📍 Swagger: http://localhost:5000/swagger"
echo ""

dotnet run
