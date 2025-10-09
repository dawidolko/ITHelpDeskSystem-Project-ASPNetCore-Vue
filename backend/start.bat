@echo off
echo ==================================
echo IT Help Desk - Backend Setup
echo ==================================
echo.

where dotnet >nul 2>nul
if %errorlevel% neq 0 (
    echo .NET SDK nie jest zainstalowany!
    echo Pobierz: https://dotnet.microsoft.com/download
    exit /b 1
)

echo Przywracam pakiety NuGet...
dotnet restore

echo.
echo Sprawdzam dotnet-ef...
dotnet tool list -g | findstr dotnet-ef >nul
if %errorlevel% neq 0 (
    echo Instaluje dotnet-ef...
    dotnet tool install --global dotnet-ef
)

echo.
echo Aplikuje migracje...
dotnet ef database update

if %errorlevel% neq 0 (
    echo Blad migracji
    exit /b 1
)

echo.
echo ==================================
echo Backend gotowy!
echo ==================================
echo.
echo Uruchamiam serwer...
echo API: http://localhost:5000
echo Swagger: http://localhost:5000/swagger
echo.

dotnet run
