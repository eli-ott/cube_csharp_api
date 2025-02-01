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

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```