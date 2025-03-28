using GameStore.Contracts;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameContract> games = [
    new (
            1,
            "FIFA 2025",
            "Football",
            29.99M,
            new DateOnly(1999, 3, 22)
        ),
    new (
            2,
            "Street Fighter",
            "Fighting",
            9.99M,
            new DateOnly(1995, 5, 21)
        ),
    new (
            3,
            "Need For Speed",
            "Racing",
            59.99M,
            new DateOnly(2004, 9, 13)
        ),
    ];

// GET /games
app.MapGet("/games", () => games);

// GET /games/{id}
app.MapGet("/games/{id}", (int id) =>
{
    GameContract? game = games.Find(game => game.Id == id);

    return game is null ? Results.NotFound() : Results.Ok(game);
}).WithName(GetGameEndpointName);

// POST /games
app.MapPost("/games", (CreateGameContract newGame) =>
{
    GameContract game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );

    games.Add( game );

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});

// PUT /games/{id}
app.MapPut("/games/{id}", (int id, UpdateGameContract updatedGame) =>
{
    var index = games.FindIndex(games => games.Id == id);

    games[index] = new GameContract(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );

    return Results.NoContent();
});

// DELETE /games/{id}
app.MapDelete("/games/{id}", (int id) =>
{
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});

app.Run();
