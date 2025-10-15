#!/bin/bash

if ! command -v node &> /dev/null; then
    echo "Node.js nie jest zainstalowany!"
    exit 1
fi

if [ ! -d "node_modules" ]; then
    npm install
fi

npm run dev
