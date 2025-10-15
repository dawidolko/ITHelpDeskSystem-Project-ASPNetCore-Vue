@echo off

where node >nul 2>nul
if %errorlevel% neq 0 (
    echo Node.js nie jest zainstalowany!
    exit /b 1
)

if not exist "node_modules\" (
    call npm install
)

npm run dev
