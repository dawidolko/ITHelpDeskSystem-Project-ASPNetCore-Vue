@echo off

where dotnet >nul 2>nul
if %errorlevel% neq 0 (
    echo .NET SDK nie jest zainstalowany!
    exit /b 1
)

dotnet restore
dotnet ef database update
dotnet run
