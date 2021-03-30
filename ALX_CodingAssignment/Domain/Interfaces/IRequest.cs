using System;
namespace ALX_CodingAssignment.Domain.Interfaces
{
  public interface IRequest : IRequest<Unit> { }


  public interface IRequest<out TResponse> { }

  public interface IRequestHandler<in TRequest, out TResponse> where TRequest : IRequest<TResponse>
  {
    TResponse Handle(TRequest message);
  }
}
