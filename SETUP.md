# Setup Guide for GamePlatform 🎮

This guide will help you set up and run the online gaming platform on your local machine.

## Prerequisites

Before you begin, ensure you have the following installed:

### Required
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) - For backend API
- [Node.js 20+](https://nodejs.org/) - For frontend React app
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQL Server LocalDB

### Optional
- [Docker Desktop](https://www.docker.com/products/docker-desktop) - For containerized deployment
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/) - For development
- [Postman](https://www.postman.com/) - For API testing

## Option 1: Manual Setup (Recommended for Development)

### Step 1: Clone the Repository

```bash
git clone https://github.com/gitfcankaya/let-s-play-online-games.git
cd let-s-play-online-games
```

### Step 2: Backend Setup

#### 2.1 Configure Database Connection

Navigate to `backend/GamePlatform.API/appsettings.json` and update the connection string:

For **SQL Server LocalDB** (Windows):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GamePlatformDb;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

For **SQL Server**:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=GamePlatformDb;User Id=sa;Password=YourPassword;TrustServerCertificate=true;"
  }
}
```

#### 2.2 Install EF Core Tools

```bash
dotnet tool install --global dotnet-ef
```

#### 2.3 Create Database and Apply Migrations

```bash
cd backend/GamePlatform.Data

# Create initial migration (if not exists)
dotnet ef migrations add InitialCreate --startup-project ../GamePlatform.API

# Apply migration to database
dotnet ef database update --startup-project ../GamePlatform.API
```

#### 2.4 Run the Backend

```bash
cd ../GamePlatform.API
dotnet run
```

The API will start at:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`

Visit `https://localhost:5001/swagger` to view API documentation.

### Step 3: Frontend Setup

#### 3.1 Install Dependencies

```bash
cd frontend
npm install
```

#### 3.2 Configure Environment

Create a `.env` file in the `frontend` directory:

```bash
cp .env.example .env
```

Update the `.env` file:
```
REACT_APP_API_URL=http://localhost:5000/api
```

#### 3.3 Run the Frontend

```bash
npm start
```

The React app will start at `http://localhost:3000`

### Step 4: Access the Application

1. Open your browser and go to `http://localhost:3000`
2. You should see the homepage with featured games
3. Register a new account or use the admin credentials (after setting them up)

## Option 2: Docker Setup (Quick Start)

### Prerequisites
- Docker Desktop installed and running

### Steps

1. Clone the repository:
```bash
git clone https://github.com/gitfcankaya/let-s-play-online-games.git
cd let-s-play-online-games
```

2. Start all services with Docker Compose:
```bash
docker-compose up -d
```

3. Wait for services to start (may take a few minutes on first run)

4. Access the application:
   - Frontend: `http://localhost:3000`
   - Backend API: `http://localhost:5000`
   - Swagger UI: `http://localhost:5000/swagger`

5. To stop all services:
```bash
docker-compose down
```

## Database Migrations

### Creating a New Migration

When you make changes to entity models:

```bash
cd backend/GamePlatform.Data
dotnet ef migrations add YourMigrationName --startup-project ../GamePlatform.API
```

### Applying Migrations

```bash
dotnet ef database update --startup-project ../GamePlatform.API
```

### Reverting Migrations

To rollback to a previous migration:

```bash
dotnet ef database update PreviousMigrationName --startup-project ../GamePlatform.API
```

### Removing Last Migration

```bash
dotnet ef migrations remove --startup-project ../GamePlatform.API
```

## Seeding Data

The database is automatically seeded with:
- 8 game categories (Action, Puzzle, Racing, Sports, etc.)
- 5 sample games
- 1 admin user

### Default Admin User

After database creation, update the admin user's password:

```sql
-- Connect to your database and run:
UPDATE Users 
SET PasswordHash = 'HashedPasswordHere' 
WHERE Email = 'admin@gameplatform.com';
```

Or register a new admin user through the API and manually set `IsAdmin = true` in the database.

## Common Issues and Solutions

### Issue 1: Database Connection Failed

**Solution:**
- Verify SQL Server is running
- Check connection string in `appsettings.json`
- Ensure SQL Server accepts TCP/IP connections
- For LocalDB, ensure it's installed with Visual Studio

### Issue 2: Port Already in Use

**Backend:**
```bash
# Change port in launchSettings.json
backend/GamePlatform.API/Properties/launchSettings.json
```

**Frontend:**
```bash
# Use different port
PORT=3001 npm start
```

### Issue 3: CORS Errors

**Solution:**
- Ensure backend is running
- Check `REACT_APP_API_URL` in frontend `.env`
- Verify CORS policy in `Program.cs`

### Issue 4: EF Migrations Not Found

**Solution:**
```bash
# Install EF Core tools globally
dotnet tool install --global dotnet-ef --version 9.0.0

# Verify installation
dotnet ef --version
```

### Issue 5: NPM Package Vulnerabilities

**Solution:**
```bash
cd frontend
npm audit fix
# Or for breaking changes
npm audit fix --force
```

## Development Tips

### Backend Development

1. **Hot Reload:**
```bash
cd backend/GamePlatform.API
dotnet watch run
```

2. **Run Tests:**
```bash
cd backend
dotnet test
```

3. **View Logs:**
   - Check console output
   - Logs are in `backend/GamePlatform.API/bin/Debug/net9.0/logs/`

### Frontend Development

1. **Auto Reload:**
   - Already enabled by default with `npm start`

2. **Build for Production:**
```bash
npm run build
```

3. **Test Production Build:**
```bash
npm install -g serve
serve -s build
```

## API Testing

### Using Swagger UI

1. Navigate to `https://localhost:5001/swagger`
2. Try the endpoints interactively
3. Copy JWT token from login response
4. Click "Authorize" button and paste token

### Using Postman

1. Import the Swagger definition: `https://localhost:5001/swagger/v1/swagger.json`
2. Create environment with `BASE_URL = http://localhost:5000`
3. Add `Authorization: Bearer {token}` header for protected endpoints

### Sample API Calls

**Register User:**
```bash
curl -X POST http://localhost:5000/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "username": "testuser",
    "email": "test@example.com",
    "password": "Test123!",
    "fullName": "Test User"
  }'
```

**Login:**
```bash
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "Test123!"
  }'
```

**Get All Games:**
```bash
curl http://localhost:5000/api/games
```

## Production Deployment

### Backend Deployment

1. **Update appsettings.json:**
   - Use production database connection string
   - Update JWT secret key
   - Set `ASPNETCORE_ENVIRONMENT=Production`

2. **Publish:**
```bash
cd backend/GamePlatform.API
dotnet publish -c Release -o ./publish
```

3. **Deploy to hosting:**
   - Azure App Service
   - AWS Elastic Beanstalk
   - Digital Ocean
   - Heroku

### Frontend Deployment

1. **Build:**
```bash
cd frontend
npm run build
```

2. **Deploy build folder to:**
   - Netlify
   - Vercel
   - AWS S3 + CloudFront
   - Azure Static Web Apps
   - GitHub Pages

### Database Deployment

1. **Azure SQL Database**
2. **AWS RDS**
3. **Managed SQL Server**

Remember to:
- Set up SSL certificates
- Configure CORS for production domain
- Enable logging and monitoring
- Set up backups
- Configure CDN for static assets

## Additional Resources

- [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [React Documentation](https://reactjs.org/docs/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Docker Documentation](https://docs.docker.com/)

## Support

For issues or questions:
1. Check the [GitHub Issues](https://github.com/gitfcankaya/let-s-play-online-games/issues)
2. Create a new issue with detailed description
3. Include error messages and steps to reproduce

Happy coding! 🎮🚀
