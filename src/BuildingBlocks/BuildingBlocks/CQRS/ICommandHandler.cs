using MediatR;

namespace BuildingBlocks.CQRS;

// For Commands that return NOTHING (Unit)
public interface ICommandHandler<in TCommand>
    : IRequestHandler<TCommand, Unit>
    where TCommand : ICommand<Unit> // Match the Unit type
{
}

// For Commands that return a RESPONSE (TResponse)
public interface ICommandHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, TResponse> // FIX: Change 'Unit' to 'TResponse'
    where TCommand : ICommand<TResponse>    // FIX: Match the response type
    where TResponse : notnull
{
}