# Let's Play Online Games 🎮

A comprehensive online gaming platform similar to CrazyGames, Poki, Playhop, and 1001Games. Built with .NET Core Web API backend, React frontend, and MSSQL database.

## 🌟 Features

### User Features
- **Browse Games**: Explore thousands of free online games across multiple categories
- **Search & Filter**: Find games by title, category, tags, or developer
- **Play Games**: Instant play with HTML5, Unity, and embedded iframe games
- **User Authentication**: Register and login with JWT-based authentication
- **Comments & Ratings**: Rate games and leave comments
- **Leaderboards**: Compete with other players and track high scores
- **Social Sharing**: Share your favorite games on social media
- **Mobile Responsive**: Fully optimized for mobile and tablet devices

### Admin Features
- **Game Management**: Add, edit, and delete games from admin panel
- **Category Management**: Organize games into categories
- **User Management**: Monitor and manage user accounts
- **Statistics Dashboard**: View analytics by country, category, and age groups

### Technical Features
- **RESTful API**: Clean and documented API endpoints
- **JWT Authentication**: Secure token-based authentication
- **Entity Framework Core**: Database access with MSSQL
- **Seed Data**: Pre-populated with sample games and categories
- **CORS Enabled**: Cross-origin support for frontend integration
- **Ad Infrastructure**: Ready for Google AdSense and Yandex integration
- **Analytics Tracking**: Comprehensive statistics by country, device, and user demographics

## 🏗️ Project Structure

```
.
├── backend/                 # .NET Core Web API
│   ├── GamePlatform.API/   # API Controllers and Services
│   ├── GamePlatform.Core/  # Domain Models and Entities
│   └── GamePlatform.Data/  # Database Context and Migrations
├── frontend/               # React TypeScript Application
│   ├── src/
│   │   ├── components/    # Reusable React components
│   │   ├── contexts/      # React Context providers
│   │   ├── pages/         # Page components
│   │   ├── services/      # API service layer
│   │   └── types/         # TypeScript type definitions
└── README.md
```

## 🚀 Getting Started

### Prerequisites

- .NET 9.0 SDK
- Node.js 20+ and npm
- SQL Server or SQL Server LocalDB

### Backend Setup

1. Navigate to the backend directory:
```bash
cd backend
```

2. Restore NuGet packages:
```bash
dotnet restore
```

3. Update the connection string in `GamePlatform.API/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GamePlatformDb;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

4. Create and migrate the database:
```bash
cd GamePlatform.Data
dotnet ef migrations add InitialCreate
dotnet ef database update
```

5. Run the API:
```bash
cd ../GamePlatform.API
dotnet run
```

The API will be available at `https://localhost:5001` (or `http://localhost:5000`)

### Frontend Setup

1. Navigate to the frontend directory:
```bash
cd frontend
```

2. Install dependencies:
```bash
npm install
```

3. Create a `.env` file:
```
REACT_APP_API_URL=http://localhost:5000/api
```

4. Start the development server:
```bash
npm start
```

The frontend will be available at `http://localhost:3000`

## 📚 API Documentation

Once the backend is running, visit:
- Swagger UI: `https://localhost:5001/swagger`

### Main Endpoints

#### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login user

#### Games
- `GET /api/games` - Get all games
- `GET /api/games/{id}` - Get game by ID
- `GET /api/games/slug/{slug}` - Get game by slug
- `GET /api/games/search?query={query}` - Search games
- `POST /api/games` - Create game (Admin)
- `PUT /api/games/{id}` - Update game (Admin)
- `DELETE /api/games/{id}` - Delete game (Admin)
- `POST /api/games/{id}/play` - Track game play

#### Categories
- `GET /api/categories` - Get all categories
- `GET /api/categories/{id}` - Get category by ID

#### Comments
- `GET /api/comments/game/{gameId}` - Get game comments
- `POST /api/comments` - Create comment (Authenticated)
- `DELETE /api/comments/{id}` - Delete comment (Authenticated)

#### Scores
- `GET /api/scores/game/{gameId}` - Get game leaderboard
- `POST /api/scores` - Submit score (Authenticated)
- `GET /api/scores/game/{gameId}/my-best` - Get user's best score

#### Statistics
- `GET /api/statistics` - Get platform statistics
- `GET /api/statistics/countries` - Get top countries
- `GET /api/statistics/top-games` - Get top games

## 🔒 Default Admin Credentials

- **Email**: admin@gameplatform.com
- **Password**: (Set during seed - needs proper hashing)

## 🎨 Frontend Features

- **React 18** with TypeScript
- **React Router** for navigation
- **Axios** for API requests
- **Context API** for state management
- **Responsive CSS** with mobile-first design
- **Component-based architecture**

## 🛠️ Technologies Used

### Backend
- .NET 9.0
- ASP.NET Core Web API
- Entity Framework Core 9
- SQL Server
- JWT Authentication
- BCrypt for password hashing
- Swagger/OpenAPI

### Frontend
- React 18
- TypeScript
- React Router DOM
- Axios
- CSS3

## 📱 React Native Support

The project structure is designed to support React Native:
- Shared TypeScript types
- API service layer can be reused
- Component architecture ready for React Native adaptation

## 🔧 Configuration

### JWT Settings
Update in `appsettings.json`:
```json
{
  "Jwt": {
    "Key": "YourSecretKey",
    "Issuer": "GamePlatformAPI",
    "Audience": "GamePlatformClients"
  }
}
```

### CORS Configuration
Currently allows all origins. Update in `Program.cs` for production.

## 📊 Database Schema

Main entities:
- **Users** - User accounts and authentication
- **Games** - Game information and metadata
- **Categories** - Game categories
- **Comments** - User comments and ratings
- **GameScores** - Player scores and leaderboards
- **GameStatistics** - Analytics and tracking data

## 🚢 Deployment

### Using Docker (Coming Soon)

```bash
docker-compose up
```

### Manual Deployment

1. Build the frontend:
```bash
cd frontend
npm run build
```

2. Publish the backend:
```bash
cd backend/GamePlatform.API
dotnet publish -c Release
```

3. Deploy to your hosting provider (Azure, AWS, etc.)

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License.

## 🙏 Acknowledgments

- Inspired by CrazyGames, Poki, Playhop, and 1001Games
- Built with modern web technologies
- Designed for scalability and performance

## 📞 Support

For issues and questions, please open an issue on GitHub.

---

Made with ❤️ for gamers worldwide 🎮
# let-s-play-online-games
let's play online games

## Dokümantasyon / Documentation

- [Teknik Ekipmanlar / Technical Equipment](TECHNICAL_EQUIPMENT.md) - Yazılım geliştirme sürecinde kullanılan tüm teknik araçlar ve ekipmanlar
