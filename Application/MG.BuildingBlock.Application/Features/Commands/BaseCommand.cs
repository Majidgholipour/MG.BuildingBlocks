// using FluentValidation.Results;
// using MG.BuildingBlock.Domain.Events;
//
// namespace MG.BuildingBlock.Application.Features.Commands;
//
// public abstract class BaseCommand : Message
// {
//     public DateTime Timestamp { get; private set; }
//
//     public ValidationResult ValidationResult { get; set; }
//
//     protected BaseCommand()
//     {
//         Timestamp = DateTime.Now;
//     }
//
//     public abstract bool IsValid();
// }
