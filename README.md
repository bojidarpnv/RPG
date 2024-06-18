# RPG Game
Welcome to the RPG Game project!

## Getting Started
Follow these steps to set up and run the application:

### 1. Clone the repository
```sh
git clone https://github.com/bojidarpnv/RPG.git
cd <your repo directory>
```

### 2. Initialize User Secrets
Navigate to the `Rpg.Data` project directory and initialize user secrets through the terminal.
```sh
cd RPG.Data
dotnet user-secrets init
```

### 3. Set the Connection String Secret
Set the connection string secret for the `RPG.Data` project
```sh
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=tcp:rpg-game.database.windows.net,1433;Initial Catalog=RPGGame;Persist Security Info=False;User ID=rpgadmin;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

```
### 4. Restore Dependencies
Navigate to the main project directory `RPG` and restore the dependencies.
```sh
cd ../RPG
dotnet restore
```

### 5. Build the project
Build the main project
```sh
dotnet build
```

#### Run the Application
Set the `RPG` project as the startup and run it
or run a Terminal and write
```sh
cd <yourDirectory>://RPG/RPG
dotnet run
```


