#!/usr/bin/env bash
set -euo pipefail

# Prosty, odporny skrypt do uruchamiania frontendu (Vite)
# - sprawdza node/npm
# - instaluje zależności, jeśli brakuje node_modules
# - uruchamia `npm run dev` i przekazuje sygnały

RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

check_command() {
  if ! command -v "$1" &> /dev/null; then
    echo -e "${RED}❌ '$1' nie jest zainstalowany!${NC}"
    return 1
  fi
  return 0
}

install_dependencies() {
  echo -e "${BLUE}📦 Instalowanie zależności (npm install)...${NC}"
  if npm install; then
    echo -e "${GREEN}✅ Zależności zainstalowane${NC}"
  else
    echo -e "${RED}❌ Błąd podczas instalacji zależności${NC}"
    exit 1
  fi
}

start_dev_server() {
  echo -e "${BLUE}🔥 Uruchamiam serwer deweloperski...${NC}"
  echo -e "${YELLOW}📍 Frontend: http://localhost:5173${NC}"
  echo -e "${YELLOW}🔁 Ctrl+C aby zatrzymać${NC}"

  # Używamy exec by process otrzymywał sygnały bezpośrednio
  exec npm run dev
}

main() {
  echo "=================================="
  echo "🚀 IT Help Desk - Frontend Setup"
  echo "=================================="

  # Wymagane narzędzia
  if ! check_command node; then
    echo "   Zainstaluj Node.js: https://nodejs.org/ lub 'brew install node'"
    exit 1
  fi

  if ! check_command npm; then
    echo "   Zainstaluj npm (powinno być z Node.js)"
    exit 1
  fi

  echo -e "${GREEN}✅ Node.js: $(node --version)${NC}"
  echo -e "${GREEN}✅ npm: $(npm --version)${NC}"

  # Instalacja zależności jeśli potrzebne
  if [ ! -d "node_modules" ]; then
    install_dependencies
  else
    echo -e "${GREEN}✅ Zależności już zainstalowane (node_modules)${NC}"
  fi

  echo ""
  start_dev_server
}

# Obsługa Ctrl+C: przekazujemy sygnał do procesu potomnego za pomocą exec
trap 'echo -e "\n${YELLOW}👋 Zatrzymuję serwer...${NC}"; exit 0' SIGINT SIGTERM

main
