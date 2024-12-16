namespace MG.BuildingBlock.Application.Exceptions;

public class UnauthorizedException(string error) : Exception
{
    public string Error { get; } = error;
}