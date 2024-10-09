# FashionClothesAndTrends

![Image 1](Screenshots/Screen1.png)

FashionClothesAndTrends is a comprehensive web application designed to help users discover, purchase, and manage the latest fashion trends. The application offers a wide range of features including browsing clothing items, managing wishlists, applying discount coupons, user authentication, and much more.

## Features

- **User Authentication and Authorization**: Secure login and registration.
- **Clothing Items Management**: Browse, search, and manage clothing items.
- **Wishlist Management**: Add items to wishlist and manage them.
- **Comments and Reviews**: Add and view comments on clothing items.
- **Favorites**: Mark clothing items as favorites and manage them.
- **Likes and Dislikes**: Like or dislike comments.
- **Order Management**: Create and manage orders.
- **Order History**: View order history.
- **Payments**: Handle payments and payment intents.
- **Notifications**: Notify users about discounts on wishlist items.
- **Photo Management**: Upload and manage photos for clothing items.
- **Ratings**: Rate clothing items.

## Technology Stack
1. Backend Architecture: The backend of our application is structured using a Clean Architecture, ensuring a clean separation of concerns and a maintainable codebase.

2. Frontend Framework: Angular, a robust platform for building web applications, is used for the frontend. It allows us to structure our codebase in a modular and maintainable manner.

3. UI Components: For the user interface, we have chosen Angular Material. It's a UI component library that adheres to Material Design principles and offers a wide array of ready-to-use components.

4. Caching: Redis, an open-source in-memory data structure store, is used for caching. It serves as a database, cache, and message broker.

- ASP.NET Core 8: A framework for building web applications and APIs.
- Entity Framework Core 8: An ORM for working with relational databases.
- RESTful API: An architectural style for creating APIs.
- Relational Database: SQL Server: A relational database for data storage.
- Distributed Cache: Redis: A distributed cache for improving performance and scalability.
- Clean Architecture: An architectural approach that ensures separation of concerns and framework independence.
- S.O.L.I.D. Principles: Object-oriented design principles for creating flexible and maintainable systems.
- Unit of Work and Repository Pattern: Patterns for managing transactions and abstracting data access.
- AutoMapper: A library for automatic object mapping.
- SignalR: A library for adding real-time functionality such as notifications.
- Client-side: Angular with Angular Material: A framework and UI component library for building client-side applications.

## Running the Application

To run this project, you need to create an appsettings.json file in the WebAPI section with the following structure:

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultDockerDbConnection": "Server=sql_server2022,1433;Database=FashionClothesAndTrendsDB;User Id=sa;Password=MyPass@word90_;MultipleActiveResultSets=true;TrustServerCertificate=True",
    "DefaultLocalDbConnection": "Server=(localdb)\\MSSQLLocalDB;Database=FashionClothesAndTrendsDB;MultipleActiveResultSets=true",
    "Redis": "redis:6379,abortConnect=false",
    "RedisLocalDb": "localhost:6379,abortConnect=false"
  },
  "Token": {
    "Key": "<......>",
    "Issuer": "https://localhost:5001",
    "Audience": "https://localhost:5001"
  },
  "CloudinarySettings": {
    "CloudName": "<.....>",
    "ApiKey": "<.....>",
    "ApiSecret": "<.....>"
  },
  "StripeSettings": {
    "PublishableKey": "<.....>",
    "SecretKey": "<.....>",
    "WhSecret": "<.....>"
  }
}
```
Replace "<Example>" with your actual data respectively.

## Configuration
### Using Local Database
The application allows you to choose between running with a local database (`localdb`) or fully within Docker using SQL Server.

If you want to use `localdb` instead of Docker for the database, follow these steps:

1. Open the `appsettings.json` file in the `WebAPI` project.
2. Set the connection string for `DefaultLocalDbConnection` example:

   ```json
   "ConnectionStrings": {
     "DefaultLocalDbConnection": "Server=(localdb)\\MSSQLLocalDB;Database=FashionClothesAndTrendsDB;MultipleActiveResultSets=true",
     "RedisLocalDb": "localhost:6379,abortConnect=false"
   }
3. Modify the ApplicationServicesExtensions.cs file (or the corresponding file where services are configured). Ensure that the application is using DefaultLocalDbConnection for SQL Server and RedisLocalDb for Redis.
4. Make sure Redis is running locally. You can run Redis using Docker (run the application using docker-compose.yml).
5. After configuring the appsettings.json file and ApplicationServicesExtensions.cs you need to run docker-compose.yml.

### Running the Application Fully in Docker
1. Open the appsettings.json file in the WebAPI project.

2. Set the connection strings for DefaultDockerDbConnection and Redis example:

   ```json
   "ConnectionStrings": {
     "DefaultDockerDbConnection": "Server=sql_server2022,1433;Database=FashionClothesAndTrendsDB;User Id=sa;Password=MyPass@word90_;MultipleActiveResultSets=true;TrustServerCertificate=True",
     "Redis": "redis:6379,abortConnect=false"
   }
3. Modify the ApplicationServicesExtensions.cs file (or the corresponding file where services are configured) to use DefaultDockerDbConnection for SQL Server and Redis for Redis:

```csharp
public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
{
    services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(config.GetConnectionString("DefaultDockerDbConnection"));
    });
    
    services.AddSingleton<IConnectionMultiplexer>(c =>
    {
        var redisOptions = ConfigurationOptions.Parse(config.GetConnectionString("Redis"), true);
        return ConnectionMultiplexer.Connect(redisOptions);
    });

    return services;
}
```
4. Run the application using Docker by executing the following command in your terminal:

```bash
docker-compose -f docker-compose.debug.yml up --build
```
This will start all the services, including SQL Server and Redis, in Docker containers.

5. Ensure your docker-compose.debug.yml file are properly configured to start the necessary services for SQL Server, Redis, and the WebAPI.
![Image 31](Screenshots/ScreenExampleDocker1.png)
![Image 32](Screenshots/ScreenExampleDocker2.png)
![Image 33](Screenshots/ScreenExampleDocker3.png)

## For Administrator Privileges
Email: admin@example.com

Password: Pa$$w0rd

## Database Diagram
![Image 1](Screenshots/FashionClothesAndTrendsDB.png)

## Screenshots
![Image 2](Screenshots/Screen2.png)
![Image 3](Screenshots/Screen3.png)
![Image 4](Screenshots/Screen4.png)
![Image 5](Screenshots/Screen5.png)
![Image 6](Screenshots/Screen6.png)
![Image 7](Screenshots/Screen7.png)
![Image 8](Screenshots/Screen8.png)
![Image 9](Screenshots/Screen9.png)
![Image 10](Screenshots/Screen10.png)
![Image 11](Screenshots/Screen11.png)
![Image 12](Screenshots/Screen12.png)
![Image 13](Screenshots/Screen13.png)
![Image 14](Screenshots/Screen14.png)
![Image 15](Screenshots/Screen15.png)
![Image 16](Screenshots/Screen16.png)
![Image 17](Screenshots/Screen17.png)
![Image 18](Screenshots/Screen18.png)
![Image 19](Screenshots/Screen19.png)
![Image 20](Screenshots/Screen20.png)
![Image 21](Screenshots/Screen21.png)
![Image 22](Screenshots/Screen22.png)
![Image 23](Screenshots/Screen23.png)
![Image 24](Screenshots/Screen24.png)
![Image 25](Screenshots/Screen25.png)
![Image 26](Screenshots/Screen26.png)
![Image 27](Screenshots/Screen27.png)
![Image 28](Screenshots/Screen28.png)
![Image 29](Screenshots/Screen29.png)
![Image 30](Screenshots/Screen30.png)

## Contributing

Contributions are welcome. Please fork the repository and create a pull request with your changes.

## Author

Bohdan Harabadzhyu

## License

[MIT](https://choosealicense.com/licenses/mit/)