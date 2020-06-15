#!/bin/bash
echo "Adicionar Migration"

dotnet ef migrations add $1 --context DesafioContext --project ../DesafioNibo.Database/Desafio.Database.csproj --startup-project ../DesafioNibo/Desafio.csproj --verbose
