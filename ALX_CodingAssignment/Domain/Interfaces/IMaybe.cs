using System;
using System.Collections.Generic;

namespace ALX_CodingAssignment.Domain.Interfaces
{
    public interface IMaybe<T> { }

    public class Nothing { }


    public class Nothing<T>
            : Nothing
            , IMaybe<T>
    { }


    public class Just<T>
            : IMaybe<T>
    {
        public T Value { get; private set; }

        public Just(T value)
        {
            Value = value;
        }


        public static implicit operator T(Just<T> to_convert)
        {

            return to_convert.Value;
        }
    }

    public static class MaybeExtensions
    {

        public static IMaybe<T> apply<T>
                    (this IMaybe<T> apply_to
                    , Func<T, IMaybe<T>> function)
        {

            if (apply_to == null)
            {
                return new Nothing<T>();
            }

            if (apply_to is Nothing<T>)
            {
                return apply_to;
            }

            if (apply_to is Just<T>)
            {
                return function(((Just<T>)apply_to).Value);
            }

            throw new Exception("Unhandled situation");
        }

        public static void Match<T>
                  (this IMaybe<T> maybe
                  , Action<T> has_value
                  , Action nothing)
        {

            maybe
              .Match(

                has_value:
                  value => { has_value(value); return new Unit(); },

                nothing:
                  () => { nothing(); return new Unit(); }

              );
        }

        public static Q Match<P, Q>
                  (this IMaybe<P> maybe
                  , Func<P, Q> has_value
                  , Func<Q> nothing)
        {

            if (maybe is Just<P>)
            {
                var value = (Just<P>)maybe;
                return has_value(value.Value);

            }

            if (maybe is Nothing<P>)
            {
                return nothing();
            }

            throw new Exception("Unmatched case");
        }

        public static P GetValueOrDefault<P>(this IMaybe<P> maybe)
        {
            return GetValueOrDefault(maybe, default(P));
        }

        public static P GetValueOrDefault<P>(this IMaybe<P> maybe
                          , P default_value)
        {
            if (maybe is Just<P>)
            {
                var value = (Just<P>)maybe;
                return value.Value;
            }

            if (maybe is Nothing<P>)
            {
                return default_value;
            }

            throw new Exception("Unmatched case");
        }

        public static bool has_been_decided<T>
                  (this IMaybe<T> maybe)
        {

            return maybe != null;
        }

        public static IEnumerable<T> SelectJustValues<T>
                        (this IEnumerable<IMaybe<T>> source)
        {

            foreach (var entry in source)
            {
                T next_value = default(T);
                var has_value = false;

                entry.Match(

                  has_value: value =>
                  {
                      has_value = true;
                      next_value = value;
                  },

                  nothing: () =>
                  {
                      has_value = false;
                  }
                );

                if (has_value)
                {
                    yield return next_value;
                }
            }
        }

        public static IMaybe<T> to_maybe<T>
                    (this T source)
                    where T : class
        {

            return source != null
                ? new Just<T>(source) as IMaybe<T>
                : new Nothing<T>();
        }

    }

    public sealed class Unit
    {
        public Unit() { }
    }
}
