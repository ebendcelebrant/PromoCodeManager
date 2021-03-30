using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALX_CodingAssignment.Domain.Validations
{
    public static class Guard
    {
        public static void IsNotNull(object argument, string argument_name)
        {

            if (argument == null)
            {
                throw new ArgumentNullException(argument_name);
            }
        }

        public static S IsNotNull<S>(S argument, string argument_name)
        {

            if (argument == null)
            {
                throw new ArgumentNullException(argument_name);
            }
            return argument;
        }


        public static void IsNull(object argument, string argument_name)
        {

            if (argument != null)
            {
                throw new ArgumentNullException(argument_name);
            }
        }

        public static string NotNullOrWhiteSpace(this string value, string argumentName)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                throw new NullReferenceException($"{argumentName} is required");

            return value;
        }

        public static IEnumerable<T> NotEmpty<T>(this IEnumerable<T> value, string argumentName)
        {
            if (!value.Any())
                throw new InvalidOperationException($"Please add at least one {argumentName}");

            return value;
        }

        public static Guid NotEmptyOrDefault(this Guid value, string argumentName)
        {
            if (value == Guid.Empty)
                throw new ArgumentException(nameof(value), $"{argumentName} is required");

            return value;
        }

        public static void EnforcePredicate(bool predicate, string error_message)
        {
            if (!predicate)
            {
                throw new ArgumentException(error_message);
            }

        }
    }
}