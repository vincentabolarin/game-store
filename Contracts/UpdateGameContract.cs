namespace GameStore.Contracts
{
    public record UpdateGameContract(
        string Name,
        string Genre,
        decimal Price,
        DateOnly ReleaseDate
    );
}
