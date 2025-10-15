#!/bin/bash

if ! command -v dotnet &> /dev/null; then
    echo ".NET SDK nie jest zainstalowany!"
    exit 1
fi

if ! command -v dotnet-ef &> /dev/null; then
    dotnet tool install --global dotnet-ef
    export PATH="$PATH:$HOME/.dotnet/tools"
fi

dotnet restore
dotnet ef database update
dotnet run
