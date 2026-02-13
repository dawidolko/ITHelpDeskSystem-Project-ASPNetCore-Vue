#!/bin/bash
set -e

echo "Starting IT Help Desk Backend..."

# Wait for MySQL to be ready
echo "Waiting for MySQL database to be ready..."
max_attempts=30
attempt=0

until nc -z mysql 3306 2>/dev/null; do
  attempt=$((attempt + 1))
  if [ $attempt -ge $max_attempts ]; then
    echo "Failed to connect to MySQL after $max_attempts attempts"
    exit 1
  fi
  echo "MySQL not ready yet, waiting... (attempt $attempt/$max_attempts)"
  sleep 2
done

echo "MySQL is ready! Starting application..."
echo "Database migrations will be performed automatically by the application."

# Start the application
cd /app
exec dotnet HelpDeskAPI.dll
