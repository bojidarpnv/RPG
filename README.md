# Makefile for initializing and setting user secrets for RPG.Data project

# Directory where the RPG.Data project is located
DATA_PROJECT_DIR = RPG.Data

# Secret key and connection string
SECRET_KEY = "ConnectionStrings:DefaultConnection"
CONNECTION_STRING = "Server=tcp:rpg-game.database.windows.net,1433;Initial Catalog=RPGGame;Persist Security Info=False;User ID=rpgadmin;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

# Target to initialize user secrets
init-secrets:
	cd $(DATA_PROJECT_DIR) && dotnet user-secrets init

# Target to set the connection string secret
set-connection-string:
	cd $(DATA_PROJECT_DIR) && dotnet user-secrets set $(SECRET_KEY) "$(CONNECTION_STRING)"

# Combined target to run both tasks
setup-secrets: init-secrets set-connection-string
