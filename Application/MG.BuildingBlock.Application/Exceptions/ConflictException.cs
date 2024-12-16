﻿namespace MG.BuildingBlock.Application.Exceptions;

public class ConflictException(string error) : Exception
{
    public string Error { get; } = error;
}