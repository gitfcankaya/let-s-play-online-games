# Contributing to GamePlatform

Thank you for your interest in contributing to GamePlatform! This document provides guidelines and instructions for contributing.

## Code of Conduct

- Be respectful and inclusive
- Welcome newcomers and help them get started
- Focus on constructive feedback
- Respect differing opinions and experiences

## How to Contribute

### Reporting Bugs

1. Check if the bug has already been reported in [Issues](https://github.com/gitfcankaya/let-s-play-online-games/issues)
2. Create a new issue with:
   - Clear title describing the bug
   - Steps to reproduce
   - Expected behavior
   - Actual behavior
   - Screenshots (if applicable)
   - Environment details (OS, browser, .NET version, etc.)

### Suggesting Features

1. Check existing issues for similar suggestions
2. Create a new issue with:
   - Clear feature description
   - Use case and benefits
   - Possible implementation approach
   - Examples from other platforms (if applicable)

### Pull Requests

1. **Fork the repository**

2. **Create a branch** from `main`:
```bash
git checkout -b feature/your-feature-name
# or
git checkout -b fix/bug-description
```

3. **Make your changes** following our coding standards

4. **Test your changes** thoroughly

5. **Commit your changes** with clear messages:
```bash
git commit -m "Add: feature description"
# or
git commit -m "Fix: bug description"
```

6. **Push to your fork**:
```bash
git push origin feature/your-feature-name
```

7. **Create a Pull Request** with:
   - Clear title and description
   - Link to related issues
   - Screenshots/videos for UI changes
   - Test results

## Development Guidelines

### Backend (.NET Core)

#### Coding Standards
- Follow C# naming conventions
- Use meaningful variable and method names
- Add XML documentation comments for public APIs
- Keep methods small and focused
- Use async/await for I/O operations

#### Example:
```csharp
/// <summary>
/// Gets a game by its unique identifier
/// </summary>
/// <param name="id">The game ID</param>
/// <returns>The game if found, null otherwise</returns>
public async Task<GameDto?> GetGameByIdAsync(int id)
{
    var game = await _context.Games
        .Include(g => g.Category)
        .FirstOrDefaultAsync(g => g.Id == id && !g.IsDeleted);
    
    return game == null ? null : MapToGameDto(game);
}
```

#### Project Structure
- **GamePlatform.Core**: Domain models, entities, enums
- **GamePlatform.Data**: DbContext, migrations, configurations
- **GamePlatform.API**: Controllers, DTOs, services

#### Adding New Features
1. Create entity in `GamePlatform.Core/Entities`
2. Add DbSet in `GamePlatformDbContext`
3. Create migration: `dotnet ef migrations add YourFeature`
4. Create DTO in `GamePlatform.API/DTOs`
5. Create service in `GamePlatform.API/Services`
6. Create controller in `GamePlatform.API/Controllers`
7. Register service in `Program.cs`

### Frontend (React + TypeScript)

#### Coding Standards
- Use TypeScript for type safety
- Follow React best practices
- Use functional components and hooks
- Keep components small and reusable
- Use meaningful component and variable names

#### Example:
```typescript
interface GameCardProps {
  game: Game;
}

const GameCard: React.FC<GameCardProps> = ({ game }) => {
  return (
    <Link to={`/game/${game.slug}`} className="game-card">
      <img src={game.thumbnailUrl} alt={game.title} />
      <h3>{game.title}</h3>
    </Link>
  );
};
```

#### Component Structure
```
components/
├── layout/         # Header, Footer, Navigation
├── game/           # Game-related components
├── admin/          # Admin panel components
└── common/         # Reusable UI components
```

#### State Management
- Use Context API for global state (auth, theme)
- Use local state for component-specific data
- Use services for API calls

#### Styling
- Use CSS modules or separate CSS files
- Mobile-first responsive design
- Follow existing color scheme and spacing

### Testing

#### Backend Tests
```bash
cd backend
dotnet test
```

Create tests in `GamePlatform.Tests` project:
```csharp
[Fact]
public async Task GetGameByIdAsync_ReturnsGame_WhenExists()
{
    // Arrange
    var gameId = 1;
    
    // Act
    var result = await _gameService.GetGameByIdAsync(gameId);
    
    // Assert
    Assert.NotNull(result);
    Assert.Equal(gameId, result.Id);
}
```

#### Frontend Tests
```bash
cd frontend
npm test
```

Create tests alongside components:
```typescript
describe('GameCard', () => {
  it('renders game title', () => {
    const game = { id: 1, title: 'Test Game', ... };
    render(<GameCard game={game} />);
    expect(screen.getByText('Test Game')).toBeInTheDocument();
  });
});
```

### Database Migrations

When modifying entity models:

1. **Create migration:**
```bash
cd backend/GamePlatform.Data
dotnet ef migrations add DescriptiveName --startup-project ../GamePlatform.API
```

2. **Review generated migration** in `Migrations` folder

3. **Test migration:**
```bash
dotnet ef database update --startup-project ../GamePlatform.API
```

4. **Rollback if needed:**
```bash
dotnet ef database update PreviousMigration --startup-project ../GamePlatform.API
```

### API Documentation

Update Swagger documentation when adding endpoints:

```csharp
/// <summary>
/// Gets all games with optional filtering
/// </summary>
/// <param name="categoryId">Filter by category ID</param>
/// <param name="isFeatured">Filter featured games</param>
/// <returns>List of games</returns>
[HttpGet]
[ProducesResponseType(typeof(List<GameDto>), 200)]
public async Task<ActionResult<List<GameDto>>> GetGames(
    [FromQuery] int? categoryId = null,
    [FromQuery] bool? isFeatured = null)
{
    // Implementation
}
```

## Commit Message Guidelines

Use clear, descriptive commit messages:

### Format:
```
Type: Short description

Longer description if needed

Fixes #issue-number
```

### Types:
- `Add:` New feature
- `Fix:` Bug fix
- `Update:` Modify existing feature
- `Refactor:` Code refactoring
- `Docs:` Documentation changes
- `Style:` Formatting, missing semicolons, etc.
- `Test:` Add or update tests
- `Chore:` Maintenance tasks

### Examples:
```
Add: User profile page with avatar upload

Implemented user profile page allowing users to:
- View their profile information
- Upload and update avatar
- Edit profile details

Fixes #42
```

```
Fix: Game leaderboard not loading on mobile

The leaderboard table was overflowing on mobile devices.
Added responsive styles and horizontal scroll.

Fixes #58
```

## Pull Request Process

1. **Update documentation** if you change functionality
2. **Update API.md** if you modify endpoints
3. **Add tests** for new features
4. **Ensure all tests pass**
5. **Update README.md** if needed
6. **Request review** from maintainers
7. **Address review comments**
8. **Squash commits** if requested

## Review Criteria

Your PR will be reviewed for:
- **Functionality**: Does it work as intended?
- **Code Quality**: Is it clean and maintainable?
- **Tests**: Are there adequate tests?
- **Documentation**: Is it documented?
- **Security**: Are there security concerns?
- **Performance**: Does it impact performance?
- **Compatibility**: Does it break existing features?

## Getting Help

- **Questions**: Open an issue with the `question` label
- **Discussions**: Use GitHub Discussions
- **Chat**: Join our community chat (if available)

## Development Setup

See [SETUP.md](SETUP.md) for detailed setup instructions.

### Quick Start
```bash
# Backend
cd backend/GamePlatform.API
dotnet run

# Frontend
cd frontend
npm install
npm start
```

## Project Priorities

Current focus areas:
1. **Admin Panel**: Complete admin dashboard UI
2. **React Native**: Mobile app development
3. **Testing**: Increase test coverage
4. **Performance**: Optimize database queries
5. **Security**: Enhance security measures
6. **Documentation**: Improve documentation

## Recognition

Contributors will be:
- Listed in README.md
- Mentioned in release notes
- Credited in commit history

Thank you for contributing to GamePlatform! 🎮🚀
