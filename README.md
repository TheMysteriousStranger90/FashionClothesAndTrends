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
1. Backend Architecture: The backend of our application is structured using a Clean Architecture (built an ASP.NET Core and Entity Framework Core), ensuring a clean separation of concerns and a maintainable codebase.

2. Frontend Framework: Angular, a robust platform for building web applications, is used for the frontend. It allows us to structure our codebase in a modular and maintainable manner.

3. UI Components: For the user interface, we have chosen Angular Material. It's a UI component library that adheres to Material Design principles and offers a wide array of ready-to-use components.

4. Caching: Redis, an open-source in-memory data structure store, is used for caching. It serves as a database, cache, and message broker.

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
    "DefaultDockerDbConnection": "<......>",
    "DefaultConnection": "<......>",
    "Redis": "localhost:6379,abortConnect=false"
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

