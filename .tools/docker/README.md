# Docker Deployment Guide

This directory contains Docker configuration files for running the IT Help Desk System in containers.

## 📋 Prerequisites

- [Docker](https://docs.docker.com/get-docker/) (version 20.10 or higher)
- [Docker Compose](https://docs.docker.com/compose/install/) (version 2.0 or higher)
- At least 4GB of available RAM
- At least 10GB of available disk space

## 🚀 Quick Start

### 1. Navigate to Docker Directory

```bash
cd .tools/docker
```

### 2. Quick Start with Script (Recommended)

**Linux/macOS:**

```bash
./start.sh
```

**Windows:**

```bash
start.bat
```

The script will automatically:

- Check if Docker is installed and running
- Create `.env` file from `.env.example` if needed
- Pull and build all required images
- Start all services
- Display access URLs and test credentials

### 3. Manual Start

**Configure Environment Variables (Optional)**

Copy the example environment file and customize if needed:

```bash
cp .env.example .env
```

Edit `.env` file to change default passwords, ports, or other settings.

**Start All Services**

```bash
docker-compose up -d
```

This command will:

- Pull required Docker images (MySQL, .NET, Node, Nginx)
- Build the backend and frontend containers
- Create a MySQL database
- Run database migrations automatically
- Start all services in the background

### 4. Verify Services are Running

```bash
docker-compose ps
```

All services should show as "Up" or "healthy".

### 5. Access the Application

- **Frontend**: http://localhost:8080
- **Backend API**: http://localhost:5001
- **Swagger Documentation**: http://localhost:5001/swagger

**Note:** Default ports are set to avoid conflicts with local development servers:

- Frontend: 8080 (instead of 3000)
- Backend: 5001 (instead of 5000, which is often used by macOS AirPlay)
- MySQL: 3307 (instead of 3306, which may conflict with local MySQL)

## 🔐 Default Test Accounts

After the first startup, you can log in with these test accounts:

- **Admin**: `admin@firma.pl` / `Admin123!`
- **Technician**: `tech@firma.pl` / `Tech123!`
- **User**: `user@firma.pl` / `User123!`

## 🛠️ Common Commands

### View Logs

```bash
# All services
docker-compose logs -f

# Specific service
docker-compose logs -f backend
docker-compose logs -f frontend
docker-compose logs -f mysql
```

### Stop All Services

```bash
docker-compose down
```

### Stop and Remove All Data

```bash
docker-compose down -v
```

**Warning**: This will delete the database and all data!

### Restart a Specific Service

```bash
docker-compose restart backend
```

### Rebuild After Code Changes

```bash
# Rebuild and restart all services
docker-compose up -d --build

# Rebuild specific service
docker-compose up -d --build backend
```

### Access Container Shell

```bash
# Backend
docker exec -it ithelpdesk-backend bash

# Frontend
docker exec -it ithelpdesk-frontend sh

# MySQL
docker exec -it ithelpdesk-mysql mysql -u helpdesk_user -p
```

## 📊 Service Health Checks

All services include health checks:

```bash
# Check backend health
curl http://localhost:5001/api/health

# Check frontend health
curl http://localhost:8080/health

# Check MySQL health
docker exec ithelpdesk-mysql mysqladmin ping -h localhost -u root -p
```

## 🐛 Troubleshooting

### Port Already in Use

If you see "port already in use" errors (especially for MySQL port 3306), you have two options:

1. **Stop your local MySQL service** (recommended for Docker usage):

   ```bash
   # macOS
   brew services stop mysql

   # Linux
   sudo systemctl stop mysql
   ```

2. **Change the Docker MySQL port** in `.env` file:
   ```env
   MYSQL_PORT=3307  # or any other available port
   ```
   Then restart: `docker compose down && docker compose up -d`

**Note:** The default MySQL port in Docker is set to 3307 to avoid conflicts with local installations.

### Services Won't Start

1. Check if ports are already in use:

   ```bash
   lsof -i :8080  # Frontend
   lsof -i :5001  # Backend
   lsof -i :3307  # MySQL (Docker default)
   ```

2. Change ports in `.env` file if needed

3. Check logs for errors:
   ```bash
   docker-compose logs
   ```

### Database Connection Issues

1. Verify MySQL is healthy:

   ```bash
   docker-compose ps mysql
   ```

2. Check MySQL logs:

   ```bash
   docker-compose logs mysql
   ```

3. Wait for MySQL to fully initialize (first startup can take 30-60 seconds)

### Backend Migration Fails

1. Check backend logs:

   ```bash
   docker-compose logs backend
   ```

2. Manually run migrations:
   ```bash
   docker exec -it ithelpdesk-backend bash
   cd /src/backend
   dotnet ef database update
   ```

### Frontend Can't Connect to Backend

1. Ensure backend is running:

   ```bash

   ```

# Check backend health

curl http://localhost:5001/api/health

2. Check CORS settings in backend code

3. Verify `VITE_API_URL` in `.env` file

### Clean Start

If you encounter persistent issues, perform a clean restart:

```bash
# Stop and remove everything
docker-compose down -v

# Remove images
docker-compose rm -f
docker rmi ithelpdesk-project-backend ithelpdesk-project-frontend

# Start fresh
docker-compose up -d --build
```

## 🔧 Configuration

### Changing Ports

Edit `.env` file:

```env
BACKEND_PORT=5001
FRONTEND_PORT=8080
MYSQL_PORT=3307
```

Then restart:

```bash
docker-compose down
docker-compose up -d
```

### Changing Database Credentials

Edit `.env` file with new credentials, then:

```bash
docker-compose down -v  # Remove old database
docker-compose up -d    # Start with new credentials
```

### Using External Database

If you want to use an external MySQL database instead of the Docker container:

1. Comment out the `mysql` service in `docker-compose.yml`
2. Update `ConnectionStrings__DefaultConnection` in the `backend` service environment
3. Restart: `docker-compose up -d --build backend`

## 📁 File Structure

```
.tools/docker/
├── docker-compose.yml           # Main orchestration file
├── Dockerfile.backend           # Backend container definition
├── Dockerfile.frontend          # Frontend container definition
├── docker-entrypoint-backend.sh # Backend startup script
├── nginx.conf                   # Nginx configuration for frontend
├── start.sh                     # Quick start script (Linux/macOS)
├── start.bat                    # Quick start script (Windows)
├── .env.example                 # Environment variables template
├── .gitignore                   # Git ignore rules
└── README.md                    # This file
```

## 🔒 Production Deployment

For production use, consider these additional steps:

1. **Change all default passwords** in `.env` file
2. **Use secrets management** instead of `.env` file
3. **Enable HTTPS** with proper SSL certificates
4. **Set up reverse proxy** (Nginx, Traefik) in front of services
5. **Configure proper backup** for MySQL database volume
6. **Set resource limits** in docker-compose.yml
7. **Use production-grade JWT keys**
8. **Enable monitoring** and logging solutions
9. **Review security settings** and harden configurations

### Example Resource Limits

Add to each service in `docker-compose.yml`:

```yaml
deploy:
  resources:
    limits:
      cpus: "1.0"
      memory: 1G
    reservations:
      cpus: "0.5"
      memory: 512M
```

## 📚 Additional Resources

- [Docker Documentation](https://docs.docker.com/)
- [Docker Compose Documentation](https://docs.docker.com/compose/)
- [MySQL Docker Hub](https://hub.docker.com/_/mysql)
- [.NET Docker Hub](https://hub.docker.com/_/microsoft-dotnet)
- [Nginx Docker Hub](https://hub.docker.com/_/nginx)

## 🆘 Support

If you encounter issues not covered in this guide:

1. Check Docker logs: `docker-compose logs`
2. Verify system requirements are met
3. Ensure Docker daemon is running
4. Check firewall settings
5. Review main project [README](../../README.md)

## 📝 License

This Docker configuration is part of the IT Help Desk System project.
