# Installation

## Requirements

```bash
dotnet tool install --global dotnet-ef
```

```bash
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.2
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.2
dotnet add package Microsoft.EntityFrameworkCore.Relational --version 8.0.2
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.2
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.0
dotnet add package Microsoft.AspNetCore.OpenApi --version 8.0.10
dotnet add package Swashbuckle.AspNetCore --version 7.1.0
dotnet add package DotNetEnv
```

## Database

Run the sql scripts (creation_table.sql then insert_data.sql) in the database to create the tables and insert the data.
Then you can apply the migrations to the database.
```bash
dotnet ef database update
```

