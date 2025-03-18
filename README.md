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
dotnet add package Stripe.net
```

## Database

Run the sql scripts (creation_table.sql then insert_data.sql) in the database to create the tables and insert the data.
Then you can apply the migrations to the database.
```bash
dotnet ef database update
```

### Migration example 
I needed to add a description field to the table product.

First I added the field to the model Product

```csharp
public string Description { get; set; } = null!;
```

⚠︎ If you create the migration just after adding the field, the naming will be wrong in the database ("Description" instead of "description")

To fix this, you need to add a line like this in the dbcontext file

```csharp
entity.Property(e => e.Description).HasColumnName("description");
```

Then you can create the migration

```bash
dotnet ef migrations add AddProductDescription
```

And apply the migration to the database

```bash
dotnet ef database update
```



# Stripe API

To use the Stripe API, you need to create an account on the Stripe website and get your API keys.
Then you can add them to the .env file

# Stripe CLI

To use the Stripe CLI, you need to install it on your computer and login with your Stripe account.
Then you can create a webhook to listen to the events from the Stripe API.

[Stripe CLI documentation](https://stripe.com/docs/stripe-cli)

```bash
stripe listen --forward-to {API_URL}/stripe-webhook --skip-verify
```

> Ready! You are using Stripe API Version [2025-02-24.acacia]. Your webhook signing secret is whsec_...

Then you can add the webhook signing secret to the .env file
