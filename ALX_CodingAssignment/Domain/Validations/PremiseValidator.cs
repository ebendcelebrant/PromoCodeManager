using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALX_CodingAssignment.Domain.Responders;

namespace ALX_CodingAssignment.Domain.Validations
{
    public class PremiseValidator
    {
        public PremiseValidator For(object the_property)
        {
            property = the_property;

            return this;
        }

        public PremiseValidator Must<S>(Func<object, bool> predicate) where S : IResponseMessage, new()
        {
            if (!predicate(property))
            {
                responseBuilder.AddMessage<S>();
            }

            return this;
        }

        public IEnumerable<IResponseMessage> GetFailures()
        {
            return responseBuilder.Build();
        }

        private readonly ResponseMessagesBuilder responseBuilder = new ResponseMessagesBuilder();

        private object property;
    }

    public static class CreditCardPremises
    {
        public static PremiseValidator TextIsCreditCardNumber<S>(this PremiseValidator context) where S : IResponseMessage, new()
        {
            return context.Must<S>(value =>
            {
                int sum = 0; 
                int mul = 1; 
                int count = (value as string).Count();
                for (int i = 0; i < count; i++)
                {
                    var digit = (value as string).Substring(count - i - 1, count - i);
                    var product = int.Parse(digit) * mul;
                    sum += (product >= 10) ? ((product % 10) + 1) : (product);
                    if (mul == 1)
                        mul++;
                    else
                        mul--;
                }
                if ((sum % 10) == 0)
                    return (true);
                else
                    return (false);
            });
        }
    }

    public static class BasicPremises
    {
        public static PremiseValidator TextIsNotEmpty<S>(this PremiseValidator context) where S : IResponseMessage, new()
        {
            return context.Must<S>(value => !string.IsNullOrWhiteSpace(value as string));
        }

        public static PremiseValidator TextIsWithin<S>(this PremiseValidator context, int lower, int upper = 99999999) where S : IResponseMessage, new()
        {
            return context.Must<S>(value =>
            {
                var count = (value as string).Count();

                return count >= lower && count <= upper;
            });
        }

        public static PremiseValidator TextIsAlphabetic<S>(this PremiseValidator context) where S : IResponseMessage, new()
        {
            return context.Must<S>(value => value == null || (value as string).All(char.IsLetter));
        }

        public static PremiseValidator TextIsNumeric<S>(this PremiseValidator context) where S : IResponseMessage, new()
        {
            return context.Must<S>(value => value == null || (value as string).All(char.IsNumber));
        }

        public static PremiseValidator TextIsAlphaNumeric<S>(this PremiseValidator context) where S : IResponseMessage, new()
        {
            return context.Must<S>(value => value != null ? (value as string).Replace(" ", "").All(char.IsLetterOrDigit) : false);
        }
    }
    public static class DatePremises
    {
        public static PremiseValidator DateIsInFuture<S>(this PremiseValidator context) where S : IResponseMessage, new()
        {
            return context.Must<S>(value => (value as DateTime?) > DateTime.Now);
        }
        public static PremiseValidator DateIsInPast<S>(this PremiseValidator context) where S : IResponseMessage, new()
        {
            return context.Must<S>(value => (value as DateTime?) < DateTime.Now);
        }
    }
}