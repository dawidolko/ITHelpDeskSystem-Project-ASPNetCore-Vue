@echo off
echo ==================================
echo IT Help Desk - Frontend Setup
echo ==================================
echo.

where node >nul 2>nul
if %errorlevel% neq 0 (
    echo Node.js nie jest zainstalowany!
    echo Pobierz: https://nodejs.org/
    exit /b 1
)

where npm >nul 2>nul
if %errorlevel% neq 0 (
    echo npm nie jest zainstalowany!
    exit /b 1
)

echo Node.js: 
node --version

echo npm:
npm --version

echo.

if not exist "node_modules\" (
    echo Instaluje zaleznosci...
    call npm install
) else (
    echo Zaleznosci juz zainstalowane
)

echo.
echo ==================================
echo Frontend gotowy!
echo ==================================
echo.
echo Uruchamiam dev server...
echo Frontend: http://localhost:5173
echo.
echo Upewnij sie ze backend dziala!
echo.

npm run dev
