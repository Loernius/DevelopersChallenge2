# Nibo Backend Challenge

This is a simple system that parses the ofx data of multiple files passed by the user and maps it to the domain. Aside from one page that lists with simple styling for a more intuitive analysis.

## Archtecture

The solution was developed in a blazor application utilizing the server-side rendering version, mixing it with a DDD (Domain-driven Design) archtecture. The application was develop on top of a blazor DDD template for a blog I've developed with knowledge from other projects I've worked on, aside from been updated to dotnet core 3.1. The database I've choose was sqlite for simplicity, without the need to install some sql server on the computer. It was used migration for mapping the table with the Entity from code. See migration below. The entity was automapped to the domain using Automapper.

## Commands

Firstly, is needed to install dotnet core 3.1 [here](https://dotnet.microsoft.com/download/dotnet-core/3.1)

Then, you need to install dotnet-ef with the following command:

	dotnet tool install --global dotnet-ef

After that, you need to run de migrations with the following:
	
	cd bash/

	./migration-update.sh

**Remember to use git bash on windows**

Then, you need to export the Environment variable declaring that you are running on development.

For git bash or terminal, run the following command:

	export ASPNETCORE_ENVIRONMENT=Development

And finally, you run the presentation project with:

	cd ../DesafioNibo/

	dotnet run --launch-profile "IIS Express"

The project should run on https://localhost:5001
