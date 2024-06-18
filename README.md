
# RPG Game

Welcome to the RPG Game project! This README will guide you through setting up and running the application.

## Prerequisites

Before you begin, ensure you have the following installed on your system:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)

## Getting Started

Follow these steps to set up and run the application:

### 1. Clone the Repository

Clone this repository to your local machine using Git.

```sh
git clone <your-repo-url>
cd <your-repo-directory>
2. Initialize User Secrets
Navigate to the RPG.Data project directory and initialize user secrets.

sh
Copy code
cd RPG.Data
dotnet user-secrets init
3. Set the Connection String Secret
Set the connection string secret for the RPG.Data project.

sh
Copy code
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=tcp:rpg-game.database.windows.net,1433;Initial Catalog=RPGGame;Persist Security Info=False;User ID=rpgadmin;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
4. Restore Dependencies
Navigate to the main project directory (RPG) and restore the dependencies.

sh
Copy code
cd ../RPG
dotnet restore
5. Build the Project
Build the main project.

sh
Copy code
dotnet build
6. Run the Application
Run the application.

sh
Copy code
dotnet run --project RPG
Makefile (Optional)
For convenience, you can use a Makefile to automate the setup and run process. Here’s how to use it:

Using the Makefile
Initialize User Secrets:

sh
Copy code
make init-secrets
Set the Connection String:

sh
Copy code
make set-connection-string
Restore Dependencies:

sh
Copy code
make restore
Build the Project:

sh
Copy code
make build
Run the Project:

sh
Copy code
make run
Setup and Run (All-in-One):

sh
Copy code
make setup-and-run
