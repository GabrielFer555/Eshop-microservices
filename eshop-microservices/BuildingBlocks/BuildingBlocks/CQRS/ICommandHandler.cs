using MediatR;

namespace BuildingBlocks.CQRS
{

    // if it doesn´t return response this will be used
    public interface ICommandHanlder<in TCommand>:ICommandHandler<TCommand, Unit> //Unit = void return
        where TCommand : ICommand<Unit>
    {

    }
    // if it returns response this interface will be used
    public interface ICommandHandler<in  TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse> 
        where TResponse: notnull
    {
    }
}
