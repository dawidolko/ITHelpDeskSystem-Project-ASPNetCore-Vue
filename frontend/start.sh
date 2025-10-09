#!/bin/bash#!/bin/bash



echo "=================================="# Description: Automated setup and start script for Linux/Mac

echo "🚀 IT Help Desk - Frontend Setup"

echo "=================================="echo "🚀 Starting WorkplayHub Vue TypeScript Project..."

echo ""echo "================================================="



if ! command -v node &> /dev/null; then# Colors for better output

    echo "❌ Node.js nie jest zainstalowany!"RED='\033[0;31m'

    echo "Zainstaluj: brew install node"GREEN='\033[0;32m'

    exit 1YELLOW='\033[1;33m'

fiBLUE='\033[0;34m'

NC='\033[0m' # No Color

if ! command -v npm &> /dev/null; then

    echo "❌ npm nie jest zainstalowany!"# Function to check if Node.js is installed

    exit 1check_node() {

fi    if ! command -v node &> /dev/null; then

        echo -e "${RED}❌ Node.js is not installed!${NC}"

echo "✅ Node.js: $(node --version)"        echo -e "${YELLOW}Please install Node.js from https://nodejs.org/${NC}"

echo "✅ npm: $(npm --version)"        exit 1

echo ""    else

        NODE_VERSION=$(node --version)

if [ ! -d "node_modules" ]; then        echo -e "${GREEN}✅ Node.js found: $NODE_VERSION${NC}"

    echo "📦 Instaluję zależności..."    fi

    npm install}

else

    echo "✅ Zależności już zainstalowane"# Function to check if npm is installed

ficheck_npm() {

    if ! command -v npm &> /dev/null; then

echo ""        echo -e "${RED}❌ npm is not installed!${NC}"

echo "=================================="        exit 1

echo "✅ Frontend gotowy!"    else

echo "=================================="        NPM_VERSION=$(npm --version)

echo ""        echo -e "${GREEN}✅ npm found: v$NPM_VERSION${NC}"

echo "🚀 Uruchamiam dev server..."    fi

echo "📍 Frontend: http://localhost:5173"}

echo ""

echo "⚠️  Upewnij się że backend działa!"# Function to install dependencies

echo ""install_dependencies() {

    echo -e "${BLUE}📦 Installing dependencies...${NC}"

npm run dev    if npm install; then

        echo -e "${GREEN}✅ Dependencies installed successfully!${NC}"
    else
        echo -e "${RED}❌ Failed to install dependencies!${NC}"
        exit 1
    fi
}

# Function to start development server
start_dev_server() {
    echo -e "${BLUE}🔥 Starting development server...${NC}"
    echo -e "${YELLOW}The application will be available at: http://localhost:5173${NC}"
    echo -e "${YELLOW}Press Ctrl+C to stop the server${NC}"
    echo "================================================="
    npm run dev
}

# Main execution
main() {
    echo -e "${BLUE}Checking system requirements...${NC}"
    check_node
    check_npm
    
    echo ""
    echo -e "${BLUE}Setting up project...${NC}"
    
    # Check if node_modules exists
    if [ ! -d "node_modules" ]; then
        install_dependencies
    else
        echo -e "${GREEN}✅ Dependencies already installed${NC}"
        echo -e "${YELLOW}💡 Run 'npm install' manually if you want to update dependencies${NC}"
    fi
    
    echo ""
    start_dev_server
}

# Handle Ctrl+C gracefully
trap 'echo -e "\n${YELLOW}👋 Stopping development server... Goodbye!${NC}"; exit 0' SIGINT

# Run main function
main