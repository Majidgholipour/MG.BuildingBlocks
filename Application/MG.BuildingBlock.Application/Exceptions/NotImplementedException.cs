namespace MG.BuildingBlock.Application.Exceptions;

public class NotImplementedException(string error) : Exception
{
    public string Error { get; } = error;
}