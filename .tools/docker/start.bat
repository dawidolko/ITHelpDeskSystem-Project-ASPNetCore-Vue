@echo off
REM IT Help Desk System - Docker Quick Start Script

echo.
echo IT Help Desk System - Docker Setup
echo ======================================
echo.

REM Check if Docker is installed
where docker >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    echo Error: Docker is not installed!
    echo Please install Docker from: https://docs.docker.com/get-docker/
    exit /b 1
)

REM Check if Docker Compose is installed
where docker-compose >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    docker compose version >nul 2>nul
    if %ERRORLEVEL% NEQ 0 (
        echo Error: Docker Compose is not installed!
        echo Please install Docker Compose from: https://docs.docker.com/compose/install/
        exit /b 1
    )
)

REM Check if Docker daemon is running
docker info >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    echo Error: Docker daemon is not running!
    echo Please start Docker Desktop.
    exit /b 1
)

REM Navigate to docker directory
cd /d "%~dp0"

REM Check if .env file exists, if not copy from example
if not exist .env (
    echo Creating .env file from .env.example...
    copy .env.example .env >nul
    echo .env file created. You can customize it if needed.
    echo.
)

REM Pull images first to show progress
echo Pulling Docker images...
docker-compose pull

echo.
echo Building application images...
docker-compose build

echo.
echo Starting services...
docker-compose up -d

echo.
echo Waiting for services to be healthy...
timeout /t 10 /nobreak >nul

echo.
echo.
echo IT Help Desk System is running!
echo ======================================
echo.
echo Access the application:
echo    Frontend:  http://localhost:8080
echo    Backend:   http://localhost:5001
echo    Swagger:   http://localhost:5001/swagger
echo.
echo Test accounts:
echo    Admin:      admin@firma.pl / Admin123!
echo    Technician: tech@firma.pl / Tech123!
echo    User:       user@firma.pl / User123!
echo.
echo Useful commands:
echo    View logs:        docker-compose logs -f
echo    Stop services:    docker-compose down
echo    Restart services: docker-compose restart
echo.
echo Full documentation: README.md
echo.
pause
