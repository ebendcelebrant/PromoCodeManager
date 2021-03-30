using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALX_CodingAssignment.Domain.Responders
{
    public class ResponseMessagesBuilder
    {
        private readonly HashSet<IResponseMessage> responseMessages =
          new HashSet<IResponseMessage>(new ResponseMessageComparer());

        public ResponseMessagesBuilder AddMessage<T>() where T : IResponseMessage, new()
        {
            AddMessage(new T());
            return this;
        }

        public ResponseMessagesBuilder AddMessage(IResponseMessage message)
        {
            if (message == null) throw new ArgumentNullException($"{message} is null");

            responseMessages.Add(message);
            return this;
        }

        public ResponseMessagesBuilder AddMessages(IEnumerable<IResponseMessage> messages)
        {
            if (messages == null) throw new ArgumentNullException($"{messages} is null");

            foreach (var message in messages)
            {
                AddMessage(message);
            }
            return this;
        }

        public bool HasErrors
        {
            get { return responseMessages.HasErrors(); }
        }

        public IEnumerable<IResponseMessage> Build()
        {
            return responseMessages;
        }

        private class ResponseMessageComparer : IEqualityComparer<IResponseMessage>
        {
            public bool Equals(IResponseMessage x, IResponseMessage y)
            {
                return x.GetType() == y.GetType();
            }

            public int GetHashCode(IResponseMessage obj)
            {
                return obj.GetType().GetHashCode();
            }
        }
    }
}