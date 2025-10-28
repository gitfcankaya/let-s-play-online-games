# Quick Reference Card 🎮

## 🚀 Start Commands

### Backend
```bash
cd backend/GamePlatform.API
dotnet run                    # Start API
dotnet watch run              # Start with hot reload
dotnet build                  # Build only
dotnet test                   # Run tests
```

### Frontend
```bash
cd frontend
npm start                     # Development server
npm run build                 # Production build
npm test                      # Run tests
```

### Docker
```bash
docker-compose up             # Start all services
docker-compose down           # Stop all services
docker-compose up --build     # Rebuild and start
```

## 🗄️ Database Commands

```bash
cd backend/GamePlatform.Data

# Create migration
dotnet ef migrations add MigrationName --startup-project ../GamePlatform.API

# Apply migration
dotnet ef database update --startup-project ../GamePlatform.API

# Rollback
dotnet ef database update PreviousMigration --startup-project ../GamePlatform.API

# Remove last migration
dotnet ef migrations remove --startup-project ../GamePlatform.API
```

## 📡 API Endpoints

### Base URL
```
http://localhost:5000/api
```

### Auth
```
POST   /auth/register        - Register user
POST   /auth/login           - Login user
```

### Games
```
GET    /games                - Get all games
GET    /games/{id}           - Get game by ID
GET    /games/slug/{slug}    - Get game by slug
GET    /games/search         - Search games
POST   /games                - Create game (Admin)
PUT    /games/{id}           - Update game (Admin)
DELETE /games/{id}           - Delete game (Admin)
POST   /games/{id}/play      - Track play
```

### Categories
```
GET    /categories           - Get all categories
GET    /categories/{id}      - Get category
```

### Comments
```
GET    /comments/game/{id}   - Get game comments
POST   /comments             - Add comment (Auth)
DELETE /comments/{id}        - Delete comment (Auth)
```

### Scores
```
GET    /scores/game/{id}     - Get leaderboard
POST   /scores               - Submit score (Auth)
GET    /scores/game/{id}/my-best - Get best score
```

### Statistics
```
GET    /statistics           - Platform stats
GET    /statistics/countries - Top countries
GET    /statistics/top-games - Top games
```

## 🔑 Environment Variables

### Backend (.env or appsettings.json)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=GamePlatformDb;..."
  },
  "Jwt": {
    "Key": "YourSecretKey",
    "Issuer": "GamePlatformAPI",
    "Audience": "GamePlatformClients"
  }
}
```

### Frontend (.env)
```
REACT_APP_API_URL=http://localhost:5000/api
```

## 🛠️ Common Tasks

### Add New Entity
1. Create entity in `GamePlatform.Core/Entities`
2. Add DbSet in `GamePlatformDbContext`
3. Create migration: `dotnet ef migrations add AddEntity`
4. Update database: `dotnet ef database update`
5. Create DTO in `GamePlatform.API/DTOs`
6. Create service in `GamePlatform.API/Services`
7. Create controller in `GamePlatform.API/Controllers`

### Add New Frontend Page
1. Create component in `frontend/src/pages`
2. Add route in `App.tsx`
3. Add navigation link in `Header.tsx`
4. Create service if needed in `services/`

### Add New API Endpoint
1. Add method to service interface
2. Implement method in service class
3. Add controller action
4. Test in Swagger

## 🧪 Testing

### Backend
```bash
# Run all tests
dotnet test

# Run specific test
dotnet test --filter "TestName"

# With coverage
dotnet test /p:CollectCoverage=true
```

### Frontend
```bash
# Run tests
npm test

# With coverage
npm test -- --coverage

# Watch mode
npm test -- --watch
```

## 🐛 Debugging

### Backend
```bash
# With debugger
dotnet run --configuration Debug

# View logs
tail -f logs/app.log
```

### Frontend
```bash
# Enable React DevTools
# Install extension in Chrome/Firefox

# View console logs
# Open browser DevTools (F12)
```

## 📝 Quick Curl Examples

### Register
```bash
curl -X POST http://localhost:5000/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{"username":"user","email":"user@test.com","password":"pass123"}'
```

### Login
```bash
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"user@test.com","password":"pass123"}'
```

### Get Games
```bash
curl http://localhost:5000/api/games
```

### Create Game (Admin)
```bash
curl -X POST http://localhost:5000/api/games \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -d '{"title":"Game","slug":"game","description":"...",...}'
```

## 🔍 Useful URLs

- Frontend: http://localhost:3000
- Backend API: http://localhost:5000
- Swagger UI: http://localhost:5000/swagger
- SQL Server: localhost:1433

## 📦 Package Management

### Backend
```bash
# Add package
dotnet add package PackageName

# Remove package
dotnet remove package PackageName

# Update packages
dotnet restore
```

### Frontend
```bash
# Add package
npm install package-name

# Add dev dependency
npm install -D package-name

# Remove package
npm uninstall package-name

# Update packages
npm update
```

## 🔐 Security Checklist

- [ ] Change JWT secret key
- [ ] Update database password
- [ ] Enable HTTPS
- [ ] Configure CORS for production
- [ ] Implement rate limiting
- [ ] Add input validation
- [ ] Enable logging
- [ ] Set up monitoring

## 📊 Database Schema

```
Users
├── Id (PK)
├── Username (Unique)
├── Email (Unique)
├── PasswordHash
└── IsAdmin

Games
├── Id (PK)
├── Title
├── Slug (Unique)
├── CategoryId (FK)
└── ...metadata

Categories
├── Id (PK)
├── Name
└── Slug (Unique)

Comments
├── Id (PK)
├── GameId (FK)
├── UserId (FK)
└── Content

GameScores
├── Id (PK)
├── GameId (FK)
├── UserId (FK)
└── Score

GameStatistics
├── Id (PK)
├── GameId (FK)
├── UserId (FK)
└── ...analytics
```

## 🚨 Troubleshooting

### Port in use
```bash
# Windows
netstat -ano | findstr :5000
taskkill /PID <PID> /F

# Linux/Mac
lsof -ti:5000 | xargs kill
```

### Database connection failed
- Check SQL Server is running
- Verify connection string
- Check firewall settings

### Frontend can't connect to API
- Verify API is running
- Check CORS configuration
- Verify REACT_APP_API_URL in .env

### Migration failed
- Check entity configurations
- Verify database connection
- Review migration code

## 📚 Documentation Links

- Full Setup: [SETUP.md](SETUP.md)
- API Docs: [API.md](API.md)
- Contributing: [CONTRIBUTING.md](CONTRIBUTING.md)
- Main README: [README.md](README.md)

---

Keep this card handy for quick reference! 🎮✨
