using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALX_CodingAssignment.Domain.Validations;

namespace ALX_CodingAssignment.Domain.Responders
{
    public class Response
    {
        public readonly IEnumerable<IResponseMessage> ResponseMessages;

        public Response(IEnumerable<IResponseMessage> messages)
        {
            ResponseMessages = Guard.IsNotNull(messages, nameof(messages));
        }

        public static Response Success()
        {
            return Create(new OperationCompletedSuccessfully());
        }

        public static Response Create(params IResponseMessage[] messages)
        {
            return new Response(messages);
        }
    }

    public sealed class QueryResponse<Q> : Response where Q : class
    {
        public readonly Q Payload;

        public QueryResponse(Q result, IEnumerable<IResponseMessage> messages = null)
        : base(messages ?? Enumerable.Empty<IResponseMessage>())
        {
            Payload = Guard.IsNotNull(result, nameof(result));
        }

        public static QueryResponse<Q> Success(Q payload)
        {
            return Create(payload, new OperationCompletedSuccessfully());
        }

        public static QueryResponse<Q> Create(Q payload, params IResponseMessage[] messages)
        {
            return new QueryResponse<Q>(payload, messages);
        }
    }

    public class OperationCompletedSuccessfully : IResponseMessage
    {
        public virtual string Message => nameof(OperationCompletedSuccessfully);
    }

    public class ExceptionWasThrown : AResponseErrorMessage
    {
        public override string Message { get; }

        public ExceptionWasThrown(Exception theException)
        {
            Message = $"{theException.GetType().Name} : {theException.Message}";

            if (theException.InnerException != null)
            {
                Message = $"{Message}.\n InnerException: {theException.InnerException.GetType().Name} : {theException?.InnerException.Message}";
            }
        }
    }

    public abstract class AResponseErrorMessage : IResponseMessage
    {
        public abstract string Message { get; }
    }

    public interface IResponseMessage
    {
        string Message { get; }
    }


    public static class ResponseExtensions
    {
        public static bool HasErrors(this Response response)
        {
            Guard.IsNotNull(response, "response");

            return response.ResponseMessages.HasErrors();
        }

        public static bool HasErrors(this IEnumerable<IResponseMessage> messages)
        {
            Guard.IsNotNull(messages, nameof(messages));

            return messages.Any(s => s is AResponseErrorMessage);
        }

        public static bool HasMessage<T>(this Response response) where T : IResponseMessage
        {
            Guard.IsNotNull(response, nameof(response));

            return response.ResponseMessages.HasMessage<T>();
        }

        public static bool HasMessage<T>(this IEnumerable<IResponseMessage> messages) where T : IResponseMessage
        {
            Guard.IsNotNull(messages, nameof(messages));

            return messages.Any(s => s is T);
        }

        public static string FormatToString(this IEnumerable<IResponseMessage> messages)
        {
            return messages.Aggregate(string.Empty, (seed, acc) => $"{seed} {acc.Message}\n");
        }
    }

}
