using System;
using System.Collections.Generic;

namespace LaYumba.Functional
{
  using static F;

  public static class F
  {
    public static Option<T> Some<T>(T value) => new Option.Some<T>(value);
    public static Option.None None => Option.None.Default;
  }

  public struct Option<T> : IEquatable<Option.None>, IEquatable<Option<T>>
  {
    private T Value { get; }

    private bool IsSome { get; }

    private bool IsNone => !IsSome;

    private Option(T value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof(value));

      Value = value;
      IsSome = true;
    }

    public static implicit operator Option<T>(Option.None _) => new Option<T>();

    public static implicit operator Option<T>(Option.Some<T> some) => new Option<T>(some.Value);

    public static implicit operator Option<T>(T value)
      => value == null ? None : Some(value);

    public TR Match<TR>(Func<TR> none, Func<T, TR> some)
      => IsSome ? some(Value) : none();

    public IEnumerable<T> AsEnumerable()
    {
      if (IsSome) yield return Value;
    }

    #region Equality Members

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      return obj is Option<T> && Equals((Option<T>) obj);
    }

    public bool Equals(Option<T> other)
      => IsSome == other.IsSome && (IsNone || other.IsNone);

    public bool Equals(Option.None _) => IsNone;

    public override int GetHashCode()
    {
      unchecked {
        return (EqualityComparer<T>.Default.GetHashCode(Value) * 397) ^ IsSome.GetHashCode();
      }
    }

    public static bool operator ==(Option<T> @this, Option<T> other) => @this.Equals(other);
    public static bool operator !=(Option<T> @this, Option<T> other) => !(@this == other);

    #endregion

    public override string ToString() => IsSome ? $"Some({Value})" :"None";
  }

  namespace Option
  {
    using System;

    public struct None
    {
      internal static readonly None Default = new None();
    }

    public struct Some<T>
    {
      internal T Value { get; }

      internal Some(T value)
      {
        if (value == null)
          throw new ArgumentNullException(nameof(value));

        Value = value;
      }
    }
  }

  public static class OptionExt
  {
    public static Option<TResult> Apply<T, TResult>
      (this Option<Func<T, TResult>> @this, Option<T> arg)
      => @this.Match(
        () => None,
        func => arg.Match(
          () => None,
          val => Some(func(val))));
  }
}