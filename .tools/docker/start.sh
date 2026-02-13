#!/bin/bash

# IT Help Desk System - Docker Quick Start Script

set -e

echo "🚀 IT Help Desk System - Docker Setup"
echo "======================================"
echo ""

# Check if Docker is installed
if ! command -v docker &> /dev/null; then
    echo "❌ Error: Docker is not installed!"
    echo "Please install Docker from: https://docs.docker.com/get-docker/"
    exit 1
fi

# Check if Docker Compose is installed
if ! command -v docker-compose &> /dev/null && ! docker compose version &> /dev/null; then
    echo "❌ Error: Docker Compose is not installed!"
    echo "Please install Docker Compose from: https://docs.docker.com/compose/install/"
    exit 1
fi

# Check if Docker daemon is running
if ! docker info &> /dev/null; then
    echo "❌ Error: Docker daemon is not running!"
    echo "Please start Docker Desktop or Docker daemon."
    exit 1
fi

# Navigate to docker directory
cd "$(dirname "$0")"

# Check if .env file exists, if not copy from example
if [ ! -f .env ]; then
    echo "📋 Creating .env file from .env.example..."
    cp .env.example .env
    echo "✅ .env file created. You can customize it if needed."
    echo ""
fi

# Pull images first to show progress
echo "📥 Pulling Docker images..."
docker-compose pull

echo ""
echo "🔨 Building application images..."
docker-compose build

echo ""
echo "🚀 Starting services..."
docker-compose up -d

echo ""
echo "⏳ Waiting for services to be healthy..."
sleep 5

# Wait for services to be healthy
echo "Checking service health..."
for i in {1..30}; do
    if docker-compose ps | grep -q "healthy"; then
        echo "✅ Services are healthy!"
        break
    fi
    if [ $i -eq 30 ]; then
        echo "⚠️  Services are taking longer than expected to start."
        echo "You can check logs with: docker-compose logs -f"
    fi
    sleep 2
    echo -n "."
done

echo ""
echo ""
echo "✅ IT Help Desk System is running!"
echo "======================================"
echo ""
echo "📱 Access the application:"
echo "   Frontend:  http://localhost:8080"
echo "   Backend:   http://localhost:5001"
echo "   Swagger:   http://localhost:5001/swagger"
echo ""
echo "🔐 Test accounts:"
echo "   Admin:      admin@firma.pl / Admin123!"
echo "   Technician: tech@firma.pl / Tech123!"
echo "   User:       user@firma.pl / User123!"
echo ""
echo "📊 Useful commands:"
echo "   View logs:        docker-compose logs -f"
echo "   Stop services:    docker-compose down"
echo "   Restart services: docker-compose restart"
echo ""
echo "📚 Full documentation: README.md"
echo ""
