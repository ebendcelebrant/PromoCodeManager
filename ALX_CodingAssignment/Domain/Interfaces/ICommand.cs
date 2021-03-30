using System.Collections.Generic;
using ALX_CodingAssignment.Domain.Responders;

namespace ALX_CodingAssignment.Domain.Interfaces
{
    public interface ICommand : IRequest<Response> { }

    public interface ICommandValidator<C> where C : ICommand
    {
        IEnumerable<IResponseMessage> Validate(C command);
    }

    public interface ICommandHandler<C> : IRequestHandler<C, Response> where C : ICommand
    {
    }
}
