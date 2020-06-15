#!/bin/bash
echo "Update database"

dotnet ef database update --context DesafioContext --project ../DesafioNibo.Database/Desafio.Database.csproj --startup-project ../DesafioNibo/Desafio.csproj --verbose $1
