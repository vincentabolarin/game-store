namespace GameStore.Contracts
{
    public record CreateGameContract(
        string Name,
        string Genre,
        decimal Price,
        DateOnly ReleaseDate
    );
}
