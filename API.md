# API Documentation

Base URL: `http://localhost:5000/api`

## Authentication

All authenticated endpoints require a JWT token in the Authorization header:
```
Authorization: Bearer <your-jwt-token>
```

Get token from `/auth/login` or `/auth/register` endpoints.

## Endpoints

### Authentication

#### Register User
```
POST /auth/register
```

**Request Body:**
```json
{
  "username": "string",
  "email": "string",
  "password": "string",
  "fullName": "string (optional)",
  "dateOfBirth": "2000-01-01 (optional)",
  "country": "string (optional)"
}
```

**Response (200 OK):**
```json
{
  "token": "jwt-token-string",
  "user": {
    "id": 1,
    "username": "string",
    "email": "string",
    "fullName": "string",
    "country": "string",
    "isAdmin": false
  }
}
```

#### Login
```
POST /auth/login
```

**Request Body:**
```json
{
  "email": "string",
  "password": "string"
}
```

**Response (200 OK):**
```json
{
  "token": "jwt-token-string",
  "user": {
    "id": 1,
    "username": "string",
    "email": "string",
    "fullName": "string",
    "country": "string",
    "isAdmin": false
  }
}
```

### Games

#### Get All Games
```
GET /games?categoryId={id}&isFeatured={boolean}
```

**Query Parameters:**
- `categoryId` (optional): Filter by category ID
- `isFeatured` (optional): Filter featured games

**Response (200 OK):**
```json
[
  {
    "id": 1,
    "title": "string",
    "slug": "string",
    "description": "string",
    "thumbnailUrl": "string",
    "fullImageUrl": "string",
    "gameUrl": "string",
    "gameType": 0,
    "categoryId": 1,
    "categoryName": "string",
    "isActive": true,
    "isFeatured": false,
    "viewCount": 0,
    "playCount": 0,
    "averageRating": 0.0,
    "ratingCount": 0,
    "developer": "string",
    "tags": "string",
    "ageRating": 0,
    "width": 800,
    "height": 600,
    "isMobileCompatible": true,
    "createdAt": "2025-01-01T00:00:00Z"
  }
]
```

#### Get Game by ID
```
GET /games/{id}
```

**Response (200 OK):** Same as game object above

#### Get Game by Slug
```
GET /games/slug/{slug}
```

**Response (200 OK):** Same as game object above

#### Search Games
```
GET /games/search?query={searchTerm}
```

**Query Parameters:**
- `query` (required): Search term

**Response (200 OK):** Array of game objects

#### Track Game Play
```
POST /games/{id}/play
```

**Request Body:**
```json
{
  "gameId": 1,
  "deviceType": "string",
  "browser": "string",
  "os": "string",
  "ageGroup": 0,
  "gender": "string",
  "referrerUrl": "string"
}
```

**Response (200 OK):** Empty

#### Create Game (Admin Only)
```
POST /games
Authorization: Bearer <token>
```

**Request Body:**
```json
{
  "title": "string",
  "slug": "string",
  "description": "string",
  "thumbnailUrl": "string",
  "fullImageUrl": "string",
  "gameUrl": "string",
  "gameType": 0,
  "categoryId": 1,
  "isActive": true,
  "isFeatured": false,
  "developer": "string",
  "tags": "string",
  "ageRating": 0,
  "width": 800,
  "height": 600,
  "isMobileCompatible": true
}
```

**Response (201 Created):** Game object

#### Update Game (Admin Only)
```
PUT /games/{id}
Authorization: Bearer <token>
```

**Request Body:** Same as Create Game

**Response (200 OK):** Updated game object

#### Delete Game (Admin Only)
```
DELETE /games/{id}
Authorization: Bearer <token>
```

**Response (204 No Content):** Empty

### Categories

#### Get All Categories
```
GET /categories
```

**Response (200 OK):**
```json
[
  {
    "id": 1,
    "name": "string",
    "slug": "string",
    "description": "string",
    "iconUrl": "string",
    "displayOrder": 1,
    "gamesCount": 10
  }
]
```

#### Get Category by ID
```
GET /categories/{id}
```

**Response (200 OK):** Category object

#### Get Category by Slug
```
GET /categories/slug/{slug}
```

**Response (200 OK):** Category object

### Comments

#### Get Game Comments
```
GET /comments/game/{gameId}
```

**Response (200 OK):**
```json
[
  {
    "id": 1,
    "gameId": 1,
    "userId": 1,
    "username": "string",
    "content": "string",
    "rating": 5,
    "parentCommentId": null,
    "likesCount": 0,
    "createdAt": "2025-01-01T00:00:00Z",
    "replies": []
  }
]
```

#### Create Comment (Authenticated)
```
POST /comments
Authorization: Bearer <token>
```

**Request Body:**
```json
{
  "gameId": 1,
  "content": "string",
  "rating": 5,
  "parentCommentId": null
}
```

**Response (201 Created):** Comment object

#### Delete Comment (Authenticated)
```
DELETE /comments/{id}
Authorization: Bearer <token>
```

**Response (204 No Content):** Empty

### Scores

#### Get Game Leaderboard
```
GET /scores/game/{gameId}?limit={number}
```

**Query Parameters:**
- `limit` (optional): Number of scores to return (default: 100)

**Response (200 OK):**
```json
[
  {
    "id": 1,
    "gameId": 1,
    "userId": 1,
    "playerName": "string",
    "score": 1000,
    "country": "string",
    "createdAt": "2025-01-01T00:00:00Z"
  }
]
```

#### Submit Score (Authenticated)
```
POST /scores
Authorization: Bearer <token>
```

**Request Body:**
```json
{
  "gameId": 1,
  "score": 1000,
  "playerName": "string"
}
```

**Response (201 Created):** Score object

#### Get My Best Score (Authenticated)
```
GET /scores/game/{gameId}/my-best
Authorization: Bearer <token>
```

**Response (200 OK):**
```json
{
  "score": 1000
}
```

### Statistics

#### Get Platform Statistics
```
GET /statistics
```

**Response (200 OK):**
```json
{
  "totalGames": 100,
  "totalUsers": 500,
  "totalPlays": 10000,
  "totalViews": 50000,
  "topCountries": [
    {
      "country": "US",
      "count": 1000
    }
  ],
  "topGames": [
    {
      "gameId": 1,
      "gameTitle": "string",
      "playCount": 500,
      "viewCount": 1000
    }
  ],
  "categoryStats": [
    {
      "categoryId": 1,
      "categoryName": "Action",
      "gamesCount": 50,
      "totalPlays": 5000
    }
  ]
}
```

#### Get Top Countries
```
GET /statistics/countries?limit={number}
```

**Query Parameters:**
- `limit` (optional): Number of countries to return (default: 10)

**Response (200 OK):** Array of country statistics

#### Get Top Games
```
GET /statistics/top-games?limit={number}
```

**Query Parameters:**
- `limit` (optional): Number of games to return (default: 10)

**Response (200 OK):** Array of game statistics

## Enums

### GameType
```
0 = Html5
1 = Flash
2 = Unity
3 = Iframe
4 = External
```

### AgeRating
```
0 = Everyone
1 = Teen
2 = Mature
```

## Error Responses

### 400 Bad Request
```json
{
  "message": "Error description"
}
```

### 401 Unauthorized
```json
{
  "message": "Unauthorized"
}
```

### 404 Not Found
```json
{
  "message": "Resource not found"
}
```

### 500 Internal Server Error
```json
{
  "message": "An error occurred"
}
```

## Rate Limiting

Currently no rate limiting is implemented. Consider adding rate limiting for production use.

## Pagination

Currently, endpoints return all results. Consider implementing pagination for endpoints that return large datasets:
- `/games`
- `/comments/game/{gameId}`
- `/scores/game/{gameId}`

Suggested pagination parameters:
- `page`: Page number (starting from 1)
- `pageSize`: Number of items per page

## CORS

CORS is configured to allow all origins in development. Update `Program.cs` for production to restrict origins:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("Production", policy =>
    {
        policy.WithOrigins("https://yourdomain.com")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

## Security Considerations

1. **JWT Token Expiration:** Tokens expire after 7 days
2. **Password Requirements:** Minimum 6 characters (update in validation)
3. **HTTPS:** Use HTTPS in production
4. **SQL Injection:** Protected by Entity Framework parameterized queries
5. **XSS:** Frontend should sanitize user input
6. **CSRF:** Consider implementing CSRF tokens for state-changing operations

## Swagger Documentation

Interactive API documentation is available at: `http://localhost:5000/swagger`

Features:
- Try endpoints directly
- View request/response schemas
- Copy cURL commands
- Authentication support
